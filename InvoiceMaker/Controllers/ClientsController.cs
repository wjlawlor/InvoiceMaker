using InvoiceMaker.Data;
using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class ClientsController : Controller
    {
        private Context context;

        public ClientsController()
        {
            context = new Context();
        }

        public ActionResult Index()
        {
            ClientRepository repo = new ClientRepository(context);
            List<Client> clients = repo.GetClients();

            return View("Index", clients);
        }

        public ActionResult Create()
        {
            CreateClient client = new CreateClient();
            client.IsActivated = true;

            return View("Create", client);
        }

        [HttpPost]
        public ActionResult Create(CreateClient client)
        {
            ClientRepository repo = new ClientRepository(context);
            try
            {
                Client newClient = new Client(0, client.Name, client.IsActivated);
                repo.Insert(newClient);
                return RedirectToAction("Index");
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }

            return View("Create", client);
        }

        public ActionResult Edit(int id)
        {
            ClientRepository repo = new ClientRepository(context);
            Client client = repo.GetById(id);

            EditClient model = new EditClient();
            model.Id = client.Id;
            model.IsActivated = client.IsActive;
            model.Name = client.Name;
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditClient client)
        {
            ClientRepository repo = new ClientRepository(context);
            try
            {
                Client newClient = new Client(id, client.Name, client.IsActivated);
                repo.Update(newClient);
                return RedirectToAction("Index");
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
            return View("Edit", client);
        }

        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_disposed == true) { return; }
            if (disposing) { context.Dispose(); }
             _disposed = true;
            base.Dispose(disposing);
        }
    }
}