using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using LMSEntity;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using MvcPaging;
using System.Collections;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class SystemInitializationController : Controller
    {
        //GET: /SystemInitialization/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        //GET: /SystemInitialization/Initialize
        [HttpGet]
        [NoCache]
        public ActionResult Initialize()
        {
            DataSynchronizationModel model = new DataSynchronizationModel();
            if (model.IsInitialized)
            {
                return PartialView(LMS.Util.PartialViewName.AlreadyInitialized, model);
            }
            else
            {
                return PartialView(LMS.Util.PartialViewName.Initialization, model);
            }
        }

        
        //POST: /SystemInitialization/Initialize
        [HttpPost]
        [NoCache]
        public ActionResult Initialize(DataSynchronizationModel model)
        {
            try
            {
                int result = model.InitializeData();

                if (result <= 0)
                {
                    ModelState.AddModelError("InvalidProcess", "Sorry not able to process data.");
                }
                else
                {
                    model.Message = Messages.GetSuccessMessage("Process completed successfully. Please log off and log in as a diffrent user.");
                }
                return PartialView(LMS.Util.PartialViewName.Initialization, model);
            }
            catch (Exception ex)
            {
                return View(LMS.Util.PartialViewName.Error);
            }
        }


    }
}
