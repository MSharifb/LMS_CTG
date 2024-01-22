using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Util;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class SetupController : Controller
    {
        // GET: /Setup/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

    }
}
