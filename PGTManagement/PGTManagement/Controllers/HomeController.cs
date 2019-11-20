using Microsoft.AspNetCore.Mvc;

namespace TccParecerFront.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (false)
            {
                return View("Adm");
            }
            return View("Prof");

           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}