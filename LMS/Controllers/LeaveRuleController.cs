using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web;
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
    public class LeaveRuleController : Controller
    {
        //GET: /LeaveRule/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /LeaveRule/LeaveRule
        [HttpGet]
        [NoCache]
        public ActionResult LeaveRule(int? page)
        {
            LeaveRuleModels model = new LeaveRuleModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.GetLeaveRuleAll(model.intSearchLeaveTypeId);
                model.LstLeaveRulePaging = model.LstLeaveRule.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        
        //POST: /LeaveRule/LeaveRule
        [HttpPost]
        [NoCache]
        public ActionResult LeaveRule(int? page, LeaveRuleModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetLeaveRuleAll(model.intSearchLeaveTypeId);
                model.LstLeaveRulePaging = model.LstLeaveRule.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveRule, model);
        }


        //GET: /LeaveRule/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveRuleModels model = new LeaveRuleModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.LeaveRule = model.GetLeaveRule(Id);
                model.LeaveRule.hfstrAllowType=model.LeaveRule.strAllowType;
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /LeaveRule/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(LeaveRuleModels model)
        {
            try
            {
                if (CheckValidation(model) == true)
                {
                    model.LeaveRule.strAllowType = model.LeaveRule.hfstrAllowType;
                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());

                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);

        }


        //POST: /LeaveRule/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(LeaveRuleModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.LeaveRule.intRuleID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.LeaveRule = new LeaveRule();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.LeaveRuleDetails, model);
        }

        
        //GET: /LeaveRule/LeaveRuleAdd
        [HttpGet]
        [NoCache]
        public ActionResult LeaveRuleAdd(string id)
        {
            LeaveRuleModels model = new LeaveRuleModels();

            try
            {
                model.Message = Util.Messages.GetErroMessage("");
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /LeaveRule/LeaveRuleAdd
        [HttpPost]
        [NoCache]
        public ActionResult LeaveRuleAdd(LeaveRuleModels model)
        {
            try
            {
                if (CheckValidation(model) == true)
                {
                    model.LeaveRule.strAllowType = model.LeaveRule.hfstrAllowType;
                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.LeaveRule = new LeaveRule();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        //POST: /LeaveRule/SaveLeaveRule
        [HttpPost]
        [NoCache]
        public ActionResult SaveLeaveRule(LeaveRuleModels model)
        {
            try
            {
                if (CheckValidation(model) == true)
                {
                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.LeaveRule = new LeaveRule();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        //POST: /LeaveRule/OptionWisePageRefresh
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(LeaveRuleModels model)
        {
            try

            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        
        [NoCache]
        public JsonResult GetEncashment(LeaveRuleModels model)
        {
            bool IsEncashable = false;
            bool bitIsRecreationLeave = false;

            string strEntitlementType = "";

            ArrayList list = new ArrayList();
            LeaveType objLvType = new LeaveType();

            try
            {
                if (model.LeaveRule.intLeaveTypeID > 0)
                {
                    objLvType = model.GetEncashment(model.LeaveRule.intLeaveTypeID);
                    IsEncashable = objLvType.bitIsEncashable;
                    bitIsRecreationLeave = objLvType.bitIsRecreationLeave;
                    if (objLvType.bitIsEarnLeave == true)
                    {
                        strEntitlementType = objLvType.strEntitlementType;
                    }
                }

                list.Add(IsEncashable);
                list.Add(strEntitlementType);
                list.Add(bitIsRecreationLeave);
            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return Json(list);
        }



        [NoCache]
        private bool CheckValidation(LeaveRuleModels model)
        {
            bool isvalid = true;

            if (model.LeaveRule.bitIsEncashable == true)
            {
                if (model.LeaveRule.intMaxEncahDays == 0)
                {
                    model.Message = Util.Messages.GetErroMessage("Max. Days Encashable must be greater than zero.");
                    return false;
                }
            }

            if (model.LeaveRule.bitIsCarryForward == true)
            {
                if (model.LeaveRule.intMaxCarryForwardDays == 0)
                {
                    model.Message = Util.Messages.GetErroMessage("Max. Carryover in Days must be greater than zero.");
                    return false;
                }

                if (model.LeaveRule.strLeaveObsoluteMonth == null || model.LeaveRule.strLeaveObsoluteMonth == "")
                {
                    model.Message = Util.Messages.GetErroMessage("Please select obsolute carry forward leave month.");
                    return false;
                }
            }
            return isvalid;
        }


        [NoCache]
        public JsonResult GetLeaveTypeStatus(int Id)
        {
            var status = string.Empty;

            try
            {
                if (Id > 0)
                {
                    var leaveType = Common.fetchLeaveType().Where(y => y.intLeaveTypeID == Id).FirstOrDefault();
                    if (leaveType != null)
                    {
                        if (leaveType.isServiceLifeType == true)
                        {
                            status = "S";
                        }
                        else
                        {
                            status = "Y";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { status = status}, JsonRequestBehavior.AllowGet);
        }

    
    }
}
