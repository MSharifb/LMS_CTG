using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using LMS.Util;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class MyLeaveController : Controller
    {
        
        // GET: /MyLeave/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

    }
}
