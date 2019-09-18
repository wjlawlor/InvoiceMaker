using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class InvoicesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}