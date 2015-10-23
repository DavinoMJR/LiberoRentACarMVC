using System.Web.Mvc;

namespace LiberoRentACarASPMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("IndexAdmin");
                }
                else if (User.IsInRole("Funcionario"))
                {
                    return RedirectToAction("IndexFuncionario");
                }
            }
            return View();
        }

        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult IndexFuncionario()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}