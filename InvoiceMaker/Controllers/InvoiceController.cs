using InvoiceMaker.Data;
using System.Collections.Generic;
using System.Web.Mvc;
using InvoiceMaker.Repositories;
using InvoiceMaker.Models;
using InvoiceMaker.FormModels.Invoice;

namespace InvoiceMaker.Controllers
{
    public class InvoiceController : Controller
    {
        private Context context;

        public InvoiceController()
        {
            context = new Context();
        }

        public ActionResult Index()
        {
            InvoiceRepository repo = new InvoiceRepository(context);
            List<Invoice> invoices = repo.GetInvoices();

            return View("Index", invoices);
        }

        public ActionResult Create()
        {
            // Populate DropDownList
            ClientRepository clientRepo = new ClientRepository(context);
            List<Client> clients = clientRepo.GetClients();

            CreateInvoice invoice = new CreateInvoice(clients);
            return View("Create", invoice);
        }

        [HttpPost]
        public ActionResult Create(CreateInvoice invoice)
        {
            InvoiceRepository invoiceRepo = new InvoiceRepository(context);

            // Get DropDownList values
            ClientRepository clientRepo = new ClientRepository(context);
            Client client = clientRepo.GetById(invoice.ClientId);

            // Create new invoice.
            Invoice newInvoice = new Invoice(0, invoice.InvoiceNumber, client, InvoiceStatus.Open);

            // If it's good, submit and go back to Index. 
            if (ModelState.IsValid)
            {
                invoiceRepo.Insert(newInvoice);
                return RedirectToAction("Index");
            }

            // Otherwise, repopulate list then generate page with error.
            List<Client> clients = clientRepo.GetClients();
            invoice.SetClientList(clients);

            return View("Create", invoice);
        }

        public ActionResult Edit(int? id)
        {
            // Kick them out if they don't offer an ID.
            if (id == null) { return RedirectToAction("Index"); }

            InvoiceRepository invoiceRepo = new InvoiceRepository(context);
            Invoice invoice = invoiceRepo.GetById(id.Value);

            // Kick them out if the try a non-existant ID.
            if (invoice == null) { return RedirectToAction("Index"); }

            // Populate DropDownList
            ClientRepository clientRepo = new ClientRepository(context);
            List<Client> clients = clientRepo.GetClients();

            EditInvoice editInvoice = new EditInvoice(clients);
            editInvoice.Id = id.Value;
            editInvoice.ClientId = invoice.Client.Id;
            editInvoice.InvoiceNumber = invoice.InvoiceNumber;
            editInvoice.OpenedOn = invoice.OpenedOn;
            editInvoice.Status = invoice.Status;

            return View("Edit", editInvoice);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditInvoice invoice)
        {
            InvoiceRepository invoiceRepo = new InvoiceRepository(context);
            ClientRepository clientRepo = new ClientRepository(context);

            if (ModelState.IsValid)
            {
                Client client = clientRepo.GetById(invoice.ClientId);
                Invoice newInvoice = invoiceRepo.GetById(id);
                    newInvoice.ClientId = invoice.ClientId;
                    newInvoice.InvoiceNumber = invoice.InvoiceNumber;
                    newInvoice.OpenedOn = invoice.OpenedOn;
                    newInvoice.Status = invoice.Status;

                // If it's good, submit and go back.
                invoiceRepo.Update(newInvoice);
                return RedirectToAction("Index");
            }

            List<Client> clients = clientRepo.GetClients();

            EditInvoice editInvoice = new EditInvoice(clients);
                editInvoice.ClientId = invoice.ClientId;
                editInvoice.InvoiceNumber = invoice.InvoiceNumber;
                editInvoice.OpenedOn = invoice.OpenedOn;
                editInvoice.Status = invoice.Status;

            // If it's not, show page again.
            return View("Edit", editInvoice);
        }
    }
}
