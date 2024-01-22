using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string dfd = Convert.ToString(Session["IsFirstLogin"]);
            if (dfd == "No")
            {
                LoginInfo.Current.ShowMenus = true;
                return View();
            }
            else
            {
                return RedirectToAction("ChangePassword", "Account");
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
