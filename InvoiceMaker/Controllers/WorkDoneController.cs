using InvoiceMaker.Models;
using InvoiceMaker.FormModels;
using InvoiceMaker.Repositories;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Data.SqlClient;
using InvoiceMaker.Data;
using System.Diagnostics;

namespace InvoiceMaker.Controllers
{
    public class WorkDoneController : Controller
    {
        private Context context;

        public WorkDoneController()
        {
            context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
        }

        public ActionResult Index()
        {
            // Populate table from List of items
            WorkDoneRepository repo = new WorkDoneRepository(context);
            List<WorkDone> workDones = repo.GetWorkDones();
            return View("Index", workDones);
        }

        public ActionResult Create()
        {
            // Populate DropDownLists
            ClientRepository clientRepo = new ClientRepository();
            List<Client> clients = clientRepo.GetClients();
            WorkTypeRepository workTypeRepo = new WorkTypeRepository();
            List<WorkType> workTypes = workTypeRepo.GetWorkTypes();

            // Bind model
            CreateWorkDone workDone = new CreateWorkDone(clients, workTypes);
            return View("Create", workDone);
        }

        [HttpPost]
        public ActionResult Create(CreateWorkDone workDone)
        {
            WorkDoneRepository workDoneRepo = new WorkDoneRepository();

            // Get DropDownList values
            ClientRepository clientRepo = new ClientRepository();
            Client client = clientRepo.GetById(workDone.ClientId);
            WorkTypeRepository workTypeRepo = new WorkTypeRepository();
            WorkType workType = workTypeRepo.GetById(workDone.WorkTypeId);

            WorkDone newWorkDone = new WorkDone(0, client, workType, DateTimeOffset.Now);

            // If it's good, submit and go back to Index. 
            if (ModelState.IsValid)
            {
                workDoneRepo.Insert(newWorkDone);
                return RedirectToAction("Index");
            }

            // If it's not good, repost the page with errors.
            return View("Create", workDone);
        }

        public ActionResult Edit(int? id)
        {
            // Kick them out if they don't offer an ID.
            if (id == null) { return RedirectToAction("Index"); }

            WorkDoneRepository workDoneRepo = new WorkDoneRepository();
            WorkDone workDone = workDoneRepo.GetById(id.Value);

            // Kick them out if the try a non-existant ID.
            if (workDone == null) { return RedirectToAction("Index"); }

            // Populate DropDownLists
            ClientRepository clientRepo = new ClientRepository();
            List<Client> clients = clientRepo.GetClients();
            WorkTypeRepository workTypeRepo = new WorkTypeRepository();
            List<WorkType> workTypes = workTypeRepo.GetWorkTypes();

            // Bind the View Model
            EditWorkDone model = new EditWorkDone(clients, workTypes);
            model.Id = id.Value;
            model.ClientId = workDone.Client.Id;
            model.WorkTypeId = workDone.WorkType.Id;
            model.StartedOn = workDone.StartedOn;
            model.EndedOn = workDone.EndedOn;

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditWorkDone workDone)
        {
            WorkDoneRepository workDoneRepo = new WorkDoneRepository();

            // Populate DropDowns
            ClientRepository clientRepo = new ClientRepository();
            Client client = clientRepo.GetById(workDone.ClientId);
            WorkTypeRepository workTypeRepo = new WorkTypeRepository();
            WorkType workType = workTypeRepo.GetById(workDone.WorkTypeId);

            // Repopulate DropDown Lists
            ClientRepository clientsRepo = new ClientRepository();
            List<Client> clients = clientsRepo.GetClients();
            WorkTypeRepository workTypesRepo = new WorkTypeRepository();
            List<WorkType> workTypes = workTypesRepo.GetWorkTypes();

            WorkDone newWorkDone = new WorkDone(id, client, workType, workDone.StartedOn, workDone.EndedOn);

            // Populate View Model
            EditWorkDone editWorkDone = new EditWorkDone(clients, workTypes);
            editWorkDone.ClientId = workDone.ClientId;
            editWorkDone.WorkTypeId = workDone.WorkTypeId;
            editWorkDone.StartedOn = workDone.StartedOn;
            editWorkDone.EndedOn = workDone.EndedOn;

            // If it's good, submit and go back.
            if (ModelState.IsValid)
            {
                workDoneRepo.Update(newWorkDone);
                return RedirectToAction("Index");
            }

            // If it's not, show page again.
            return View("Edit", editWorkDone);
        }
    }
}
