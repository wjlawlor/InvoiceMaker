using InvoiceMaker.Data;
using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class WorkTypeController : Controller
    {
        private Context context;

        public WorkTypeController()
        {
            context = new Context();
        }

        public ActionResult Index()
        {
            WorkTypeRepository repo = new WorkTypeRepository(context);
            List<WorkType> workTypes = repo.GetWorkTypes();

            return View("Index", workTypes);
        }

        public ActionResult Create()
        {
            CreateWorkType workType = new CreateWorkType();
            return View("Create", workType);
        }

        [HttpPost]
        public ActionResult Create(CreateWorkType workType)
        {
            WorkTypeRepository repo = new WorkTypeRepository(context);
            try
            {
                WorkType newWorkType = new WorkType(0, workType.Name, workType.Rate);
                repo.Insert(newWorkType);
                return RedirectToAction("Index");
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }

            return View("Create", workType);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) { return RedirectToAction("Index"); }

            WorkTypeRepository repo = new WorkTypeRepository(context);
            WorkType workType = repo.GetById(id.Value);

            if (workType == null) { return RedirectToAction("Index"); }

            EditWorkType model = new EditWorkType();
            model.Id = workType.Id;
            model.Name = workType.Name;
            model.Rate = workType.Rate;

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditWorkType workType)
        {
            WorkTypeRepository repo = new WorkTypeRepository(context);
            try
            {
                WorkType newWorkType = new WorkType(id, workType.Name, workType.Rate);
                repo.Update(newWorkType);
                return RedirectToAction("Index");
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
            return View("Edit", workType);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) { return RedirectToAction("Index"); }

            WorkTypeRepository repo = new WorkTypeRepository(context);
            WorkType workType = repo.GetById(id.Value);

            if (workType == null) { return RedirectToAction("Index"); }

            DeleteWorkType model = new DeleteWorkType();
            model.Id = workType.Id;
            model.Name = workType.Name;

            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(int id, DeleteWorkType workType)
        {
            WorkTypeRepository repo = new WorkTypeRepository(context);

            WorkType newWorkType = new WorkType(id, workType.Name,0);
            repo.Delete(newWorkType);

            return RedirectToAction("Index");           
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