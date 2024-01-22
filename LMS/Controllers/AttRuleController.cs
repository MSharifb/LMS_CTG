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
    public class AttRuleController : Controller
    {
        //GET: /AttRule/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /AttRule/AttRule
        [HttpGet]
        [NoCache]
        public ActionResult AttRule(int? page)
        {
            AttRuleModels model = new AttRuleModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.Get(-1,"");
                model.LstATT_tblRulePaging = model.LstATT_tblRule.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /AttRule/AttRule
        [HttpPost]
        [NoCache]
        public ActionResult AttRule(int? page, AttRuleModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.Get(-1,"");
                model.LstATT_tblRulePaging = model.LstATT_tblRule.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.AttRule, model);
        }


        //GET: /AttRule/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            AttRuleModels model = new AttRuleModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.ATT_tblRule = model.Get(Id);
                if(model.ATT_tblRule.strEmpID == "")
                    model.IsIndividual = false;
                else model.IsIndividual = true;
                getEmployeeInformation(model);      
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /AttRule/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(AttRuleModels model)
        {
            try
            {
                if (CheckValidation(model, true) == true)
                {
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


        //POST: /AttRule/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(AttRuleModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.ATT_tblRule.intRuleID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.ATT_tblRule = new ATT_tblRule();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.AttRuleDetails, model);
        }


        //GET: /AttRule/AttRuleAdd
        [HttpGet]
        [NoCache]
        public ActionResult AttRuleAdd(string id)
        {
            AttRuleModels model = new AttRuleModels();
            model.ATT_tblRule.dtEffectiveDate = DateTime.Now.Date;
            model.IsNew = true;
            try
            {
                model.Message = Util.Messages.GetErroMessage("");
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /AttRule/AttRuleAdd
        [HttpPost]
        [NoCache]
        public ActionResult AttRuleAdd(AttRuleModels model)
        {
            try
            {
                if (CheckValidation(model, false) == true)
                {
                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.ATT_tblRule = new ATT_tblRule();
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


        //POST: /AttRule/SaveAttRule
        [HttpPost]
        [NoCache]
        public ActionResult SaveAttRule(AttRuleModels model)
        {
            try
            {
                if (CheckValidation(model, true) == true)
                {
                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.ATT_tblRule = new ATT_tblRule();
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


        //POST: /AttRule/OptionWisePageRefresh
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(AttRuleModels model)
        {
            try

            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        //[NoCache]
        //private bool CheckValidation(AttRuleModels model, bool IsEdit)
        //{
        //    bool isvalid = true;
        //    AttRuleModels tmpModel = new AttRuleModels();
        //    tmpModel.Get(-1, model.ATT_tblRule.dtEffectiveDate.ToString("yyyy-MM-dd"));
           
        //    if (tmpModel.LstATT_tblRule.Count > 0)
        //    {
        //        if (IsEdit)
        //        {
        //            if ((tmpModel.LstATT_tblRule.Count == 1) && (tmpModel.LstATT_tblRule[0].intRuleID == model.ATT_tblRule.intRuleID))
        //                return true;
        //        }
        //        model.Message = Util.Messages.GetErroMessage("Record already exists for this effective date.");
        //        return false;
        //    }
            
        //    return isvalid;
        //}

        [NoCache]
        private bool CheckValidation(AttRuleModels model, bool IsEdit)
        {
            bool isvalid = true;
            AttRuleModels tmpModel = new AttRuleModels();

            tmpModel.Get(-1,model.ATT_tblRule.dtEffectiveDate.ToString("yyyy-MM-dd")); // check uniq assignID

            if (tmpModel.LstATT_tblRule.Count > 0)
            {
                if (!string.IsNullOrEmpty(model.ATT_tblRule.strEmpID)) // check uniq employee ID
                {
                    tmpModel.LstATT_tblRule = tmpModel.LstATT_tblRule.Where(q => q.strEmpID == model.ATT_tblRule.strEmpID).ToList();
                    if (tmpModel.LstATT_tblRule.Count > 0)
                    {
                        if (IsEdit && tmpModel.LstATT_tblRule[0].intRuleID == model.ATT_tblRule.intRuleID)
                            return true;

                        model.Message = Util.Messages.GetErroMessage("This Employee already exists");
                        return false;
                    }
                }
                else // check duplicate designation
                {

                    string _strDesignationID = model.ATT_tblRule.strDesignationID == null ? "" : model.ATT_tblRule.strDesignationID;
                    string _strDepartmentID = model.ATT_tblRule.strDepartmentID == null ? "" : model.ATT_tblRule.strDepartmentID;
                    string _strCompanyID = model.ATT_tblRule.strCompanyID == null ? "" : model.ATT_tblRule.strCompanyID;
                    string _strLocationID = model.ATT_tblRule.strLocationID == null ? "" : model.ATT_tblRule.strLocationID;

                    tmpModel.LstATT_tblRule = tmpModel.LstATT_tblRule.Where(
                         q => q.strDesignationID == _strDesignationID
                         && q.strDepartmentID == _strDepartmentID
                         && q.strCompanyID == _strCompanyID
                         && q.strLocationID == _strLocationID
                         && q.intReligionID == model.ATT_tblRule.intReligionID
                         && q.intCategoryCode == model.ATT_tblRule.intCategoryCode).ToList();
                    if (tmpModel.LstATT_tblRule.Count > 0)
                    {
                        if (IsEdit && tmpModel.LstATT_tblRule[0].intRuleID == model.ATT_tblRule.intRuleID)
                            return true;

                        model.Message = Util.Messages.GetErroMessage("This data already exists");
                        return false;
                    }
                }
            }           

            return isvalid;
        }

        [HttpGet]
        public JsonResult getEmployeeInformation(AttRuleModels model)
        {
            Employee objEmp = new Employee();
            if (!string.IsNullOrEmpty(model.ATT_tblRule.strEmpID))
            {
                objEmp = model.GetEmployeeInfo(model.ATT_tblRule.strEmpID);

                model.ATT_tblRule.strEmpName = objEmp.strEmpName;
                model.ATT_tblRule.strDepartment = objEmp.strDepartment;
                model.ATT_tblRule.strDesignation = objEmp.strDesignation;
                model.ATT_tblRule.strLocation = model.Location.Where(q => q.Value == objEmp.strLocationID).First().Text;
                model.ATT_tblRule.strCompany = model.Company.Where(q => q.Value == objEmp.strCompanyID).First().Text;               

            }

            return Json(model.ATT_tblRule, JsonRequestBehavior.AllowGet);
        }
    
    }
}
