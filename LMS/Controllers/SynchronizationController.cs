using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using System.Collections;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class SynchronizationController : Controller
    {

        // GET: /Synchronization/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        
        // GET: /Synchronization/Synchronize
        [HttpGet]
        [NoCache]
        public ActionResult Synchronize()
        {
            DataSynchronizationModel model = new DataSynchronizationModel();
            return PartialView(LMS.Util.PartialViewName.Synchronize, model);
        }

        
        // POST: /Synchronization/Synchronize
        [HttpPost]
        [NoCache]
        public ActionResult Synchronize(DataSynchronizationModel model)
        {
            try
            {
                int result = model.SynchronizeData();

                if (result <= 0)
                {
                    ModelState.AddModelError("InvalidProcess", "Sorry not able to synchronize data.");
                }
                else
                {
                    model.Message = Messages.GetSuccessMessage("Data synchronized successfully.");
                }
                return PartialView(LMS.Util.PartialViewName.Synchronize, model);
            }
            catch (Exception ex)
            {
                return View(LMS.Util.PartialViewName.Error);
            }

        }

    }
}
