using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class LeaveEntitlementController : Controller
    {

        //GET: /LeaveEntitlement/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        //GET: /LeaveEntitlement/
        [HttpGet]
        [NoCache]
        public ActionResult LeaveEntitlement()
        {
            LeaveEntitlementModels model = new LeaveEntitlementModels();
            model.LeaveEntitlement.intLeaveYearID = LoginInfo.Current.intLeaveYearID;
            return View(LMS.Util.PartialViewName.LeaveEntitlementDetails, model);
        }

        //GET: /LeaveEntitlement/OptionWisePageRefresh
        [HttpGet]
        [NoCache]
        public ActionResult OptionWisePageRefresh(LeaveEntitlementModels model)
        {
            try
            {

            }
            catch (Exception ex)
            {
                //ViewData["vdMsg"] = Messages.GetErroMessage(ex.Message);
            }
            return View(model);
        }

        //POST: /LeaveEntitlement/SavLeaveEntitlement
        [HttpPost]
        [NoCache]
        public ActionResult SaveLeaveEntitlement(LeaveEntitlementModels model)
        {
            string strmessage = "";
            try
            {
                model.EntitlementProcess(model, out strmessage);

                if (strmessage.ToString() == "Successful")
                {
                    model.Message = Util.Messages.GetSuccessMessage("Processed Successfully");
                    model.LeaveEntitlement = new LeaveEntitlement();
                }
                else
                {
                    model.Message = Util.Messages.GetErroMessage(strmessage.ToString());
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred);
            }

            return View(LMS.Util.PartialViewName.LeaveEntitlementDetails, model);
        }

        //POST: /LeaveEntitlement/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(LeaveEntitlementModels model)
        {
            string strmessage = "";
            try
            {
                model.EntitlementRollback(model, out strmessage);

                if (strmessage.ToString() == "Successful")
                {
                    model.Message = Util.Messages.GetSuccessMessage("Rollback Successfully");
                    model.LeaveEntitlement = new LeaveEntitlement();
                }
                else
                {
                    model.Message = Util.Messages.GetErroMessage(strmessage.ToString());
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred);
            }
            return View(LMS.Util.PartialViewName.LeaveEntitlementDetails, model);
        }


    }
}
