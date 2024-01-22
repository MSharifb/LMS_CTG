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
    public class SetOverTimeController : Controller
    {
        //GET: /SetOverTime/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /SetOverTime/SetOverTime
        [HttpGet]
        [NoCache]
        public ActionResult SetOverTime(int? page)
        {
            SetOverTimeModels model = new SetOverTimeModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.Get(-1, "", "", "", "", "", -1,  "", "");
                model.LstSetOverTimePaging = model.LstSetOverTime.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /SetOverTime/SetOverTime
        [HttpPost]
        [NoCache]
        public ActionResult SetOverTime(int? page, SetOverTimeModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                 model.Get(-1,model.SetOverTime.strEmpName == null ? "" : model.SetOverTime.strEmpID,
                    model.SetOverTime.strCompanyID == null ? "" : model.SetOverTime.strCompanyID,
                    model.SetOverTime.strLocationID == null ? "" : model.SetOverTime.strLocationID,
                    model.SetOverTime.strDesignationID == null ? "" : model.SetOverTime.strDesignationID,
                    model.SetOverTime.strDepartmentID == null ? "" : model.SetOverTime.strDepartmentID,                   
                    model.SetOverTime.intCategoryCode,
                    model.SetOverTime.strPeriodFrom == null ? "" : model.SetOverTime.strPeriodFrom,
                    model.SetOverTime.strPeriodTo == null ? "" : model.SetOverTime.strPeriodTo);
                                   
                model.LstSetOverTimePaging = model.LstSetOverTime.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.SetOverTime, model);
        }


        //GET: /SetOverTime/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            SetOverTimeModels model = new SetOverTimeModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.SetOverTime = model.Get(Id);
                if (model.SetOverTime.strEmpID == "")
                    model.SetOverTime.EntryType = 1;
                else model.SetOverTime.EntryType = 2;

                getEmployeeInformation(model);               
                
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /SetOverTime/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(SetOverTimeModels model)
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


        //POST: /SetOverTime/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(SetOverTimeModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.SetOverTime.intRowID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.SetOverTime = new SetOverTime();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.SetOverTimeDetails, model);
        }


        //GET: /SetOverTime/SetOverTimeAdd
        [HttpGet]
        [NoCache]
        public ActionResult SetOverTimeAdd(string id)
        {
            SetOverTimeModels model = new SetOverTimeModels();
            model.SetOverTime.EntryType = 1;
            model.SetOverTime.bitFromConfirmationDate = true;
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


        //POST: /SetOverTime/SetOverTimeAdd
        [HttpPost]
        [NoCache]
        public ActionResult SetOverTimeAdd(SetOverTimeModels model)
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
                        model.SetOverTime = new SetOverTime();
                        model.SetOverTime.EntryType = 1;
                        model.SetOverTime.bitFromConfirmationDate = true;
                        model.IsNew = true;
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


        //POST: /SetOverTime/SaveSetOverTime
        [HttpPost]
        [NoCache]
        public ActionResult SaveSetOverTime(SetOverTimeModels model)
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
                        model.SetOverTime = new SetOverTime();
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
               
        
        [NoCache]
        private bool CheckValidation(SetOverTimeModels model, bool IsEdit)
        {
            bool isvalid = true;
            SetOverTimeModels tmpModel = new SetOverTimeModels();

            tmpModel.Get(-1, "", "", "", "", "", -1, model.SetOverTime.strPeriodFrom, model.SetOverTime.strPeriodTo);

            if (tmpModel.LstSetOverTime.Count > 0)
            {
                if ( !string.IsNullOrEmpty(model.SetOverTime.strEmpID)) // check uniq employee ID
                {
                    tmpModel.LstSetOverTime = tmpModel.LstSetOverTime.Where(q => q.strEmpID == model.SetOverTime.strEmpID).ToList();
                    if (tmpModel.LstSetOverTime.Count > 0)
                    {
                        if (IsEdit && tmpModel.LstSetOverTime[0].intRowID == model.SetOverTime.intRowID)
                            return true;

                        model.Message = Util.Messages.GetErroMessage("This Employee already exists");
                        return false;
                    }
                }
                else // check duplicate designation
                {

                    string _strDesignationID = model.SetOverTime.strDesignationID == null ? "" : model.SetOverTime.strDesignationID;
                    string _strDepartmentID = model.SetOverTime.strDepartmentID == null ? "" : model.SetOverTime.strDepartmentID;
                    string _strCompanyID = model.SetOverTime.strCompanyID == null ? "" : model.SetOverTime.strCompanyID;
                    string _strLocationID = model.SetOverTime.strLocationID == null ? "" : model.SetOverTime.strLocationID;

                    tmpModel.LstSetOverTime = tmpModel.LstSetOverTime.Where(
                         q => q.strDesignationID == _strDesignationID
                         && q.strDepartmentID == _strDepartmentID
                         && q.strCompanyID == _strCompanyID
                         && q.strLocationID == _strLocationID
                         && q.intCategoryCode == model.SetOverTime.intCategoryCode).ToList();
                    if (tmpModel.LstSetOverTime.Count > 0)
                    {
                        if (IsEdit && tmpModel.LstSetOverTime[0].intRowID == model.SetOverTime.intRowID)
                            return true;

                        model.Message = Util.Messages.GetErroMessage("This data already exists");
                        return false;
                    }
                }
            }
           
            //if (tmpModel.LstSetOverTime.Count > 0)
            //{
            //    if (IsEdit)
            //    {
            //        if ((tmpModel.LstSetOverTime.Count == 1) && (tmpModel.LstSetOverTime[0].intRowID == model.SetOverTime.intRowID))
            //            return true;
            //    }
            //    model.Message = Util.Messages.GetErroMessage("Record already exists.");
            //    return false;
            //}
            
            return isvalid;
        }

        [HttpGet]
        public JsonResult getEmployeeInformation(SetOverTimeModels model)
        {
            Employee objEmp = new Employee();
            if (!string.IsNullOrEmpty(model.SetOverTime.strEmpID))
            {
                objEmp = model.GetEmployeeInfo(model.SetOverTime.strEmpID);
                               
                model.SetOverTime.strDepartment = objEmp.strDepartment;
                model.SetOverTime.strDesignation = objEmp.strDesignation;
                model.SetOverTime.strLocation = model.Location.Where(q => q.Value == objEmp.strLocationID).First().Text;
                model.SetOverTime.strCompany = model.Company.Where(q => q.Value == objEmp.strCompanyID).First().Text;

            }

            return Json(model.SetOverTime, JsonRequestBehavior.AllowGet);
        }
            
    }
}
