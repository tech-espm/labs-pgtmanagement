using Microsoft.AspNetCore.Mvc;

namespace TccParecerFront.Controllers
{
    public class ParecerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Visualise()
        {
            //return View("view");
            return View("Review");
        }

    }
}