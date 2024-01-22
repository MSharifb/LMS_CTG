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
    public class WeekendHolidayWorkingHourController : Controller
    {
        //GET: /WeekendHolidayWorkingHour/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /WeekendHolidayWorkingHour/WeekendHolidayWorkingHour
        [HttpGet]
        [NoCache]
        public ActionResult WeekendHolidayWorkingHour(int? page)
        {
            WeekendHolidayWorkingHourModels model = new WeekendHolidayWorkingHourModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.Get(-1, "", "", "", "", "", -1, -1, "", "", "", -1);
                model.LstWeekendHolidayWorkingHourPaging = model.LstWeekendHolidayWorkingHour.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /WeekendHolidayWorkingHour/WeekendHolidayWorkingHour
        [HttpPost]
        [NoCache]
        public ActionResult WeekendHolidayWorkingHour(int? page, WeekendHolidayWorkingHourModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                 model.Get(-1,model.WeekendHolidayWorkingHour.strEmpName == null ? "" : model.WeekendHolidayWorkingHour.strEmpID,
                    model.WeekendHolidayWorkingHour.strCompanyID == null ? "" : model.WeekendHolidayWorkingHour.strCompanyID,
                    model.WeekendHolidayWorkingHour.strLocationID == null ? "" : model.WeekendHolidayWorkingHour.strLocationID,
                    model.WeekendHolidayWorkingHour.strDesignationID == null ? "" : model.WeekendHolidayWorkingHour.strDesignationID,
                    model.WeekendHolidayWorkingHour.strDepartmentID == null ? "" : model.WeekendHolidayWorkingHour.strDepartmentID,
                    model.WeekendHolidayWorkingHour.intReligionID,
                    model.WeekendHolidayWorkingHour.intCategoryCode,
                    model.WeekendHolidayWorkingHour.strPeriodFrom == null ? "" : model.WeekendHolidayWorkingHour.strPeriodFrom,
                    model.WeekendHolidayWorkingHour.strPeriodTo == null ? "" : model.WeekendHolidayWorkingHour.strPeriodTo,
                    model.WeekendHolidayWorkingHour.strWHType == null ? "" : model.WeekendHolidayWorkingHour.strWHType, 
                    model.WeekendHolidayWorkingHour.intShiftID);
                                   
                model.LstWeekendHolidayWorkingHourPaging = model.LstWeekendHolidayWorkingHour.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.WeekendHolidayWorkingHour, model);
        }


        //GET: /WeekendHolidayWorkingHour/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            WeekendHolidayWorkingHourModels model = new WeekendHolidayWorkingHourModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.WeekendHolidayWorkingHour = model.Get(Id);
                if (model.WeekendHolidayWorkingHour.strEmpID == "")
                    model.WeekendHolidayWorkingHour.EntryType = 1;
                else model.WeekendHolidayWorkingHour.EntryType = 2;

                getEmployeeInformation(model);               
                
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /WeekendHolidayWorkingHour/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(WeekendHolidayWorkingHourModels model)
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


        //POST: /WeekendHolidayWorkingHour/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(WeekendHolidayWorkingHourModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.WeekendHolidayWorkingHour.intRowID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.WeekendHolidayWorkingHour = new WeekendHolidayWorkingHour();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.WeekendHolidayWorkingHourDetails, model);
        }


        //GET: /WeekendHolidayWorkingHour/WeekendHolidayWorkingHourAdd
        [HttpGet]
        [NoCache]
        public ActionResult WeekendHolidayWorkingHourAdd(string id)
        {
            WeekendHolidayWorkingHourModels model = new WeekendHolidayWorkingHourModels();
            model.WeekendHolidayWorkingHour.EntryType = 1;
            model.WeekendHolidayWorkingHour.strWHType = "w";
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


        //POST: /WeekendHolidayWorkingHour/WeekendHolidayWorkingHourAdd
        [HttpPost]
        [NoCache]
        public ActionResult WeekendHolidayWorkingHourAdd(WeekendHolidayWorkingHourModels model)
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
                        model.WeekendHolidayWorkingHour = new WeekendHolidayWorkingHour();
                        model.WeekendHolidayWorkingHour.EntryType = 1;
                        model.WeekendHolidayWorkingHour.strWHType = "w";
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


        //POST: /WeekendHolidayWorkingHour/SaveWeekendHolidayWorkingHour
        [HttpPost]
        [NoCache]
        public ActionResult SaveWeekendHolidayWorkingHour(WeekendHolidayWorkingHourModels model)
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
                        model.WeekendHolidayWorkingHour = new WeekendHolidayWorkingHour();
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
        private bool CheckValidation(WeekendHolidayWorkingHourModels model, bool IsEdit)
        {
            bool isvalid = true;
            WeekendHolidayWorkingHourModels tmpModel = new WeekendHolidayWorkingHourModels();

            tmpModel.Get(-1, "", "", "", "", "", -1, -1, model.WeekendHolidayWorkingHour.strPeriodFrom, model.WeekendHolidayWorkingHour.strPeriodTo, "", -1); // check uniq assignID

            if (tmpModel.LstWeekendHolidayWorkingHour.Count > 0)
            {
                if ( !string.IsNullOrEmpty(model.WeekendHolidayWorkingHour.strEmpID)) // check uniq employee ID
                {
                    tmpModel.LstWeekendHolidayWorkingHour = tmpModel.LstWeekendHolidayWorkingHour.Where(q => q.strEmpID == model.WeekendHolidayWorkingHour.strEmpID).ToList();
                    if (tmpModel.LstWeekendHolidayWorkingHour.Count > 0)
                    {
                        if (IsEdit && tmpModel.LstWeekendHolidayWorkingHour[0].intRowID == model.WeekendHolidayWorkingHour.intRowID)
                            return true;

                        model.Message = Util.Messages.GetErroMessage("This Employee already exists");
                        return false;
                    }
                }
                else // check duplicate designation
                {

                    string _strDesignationID = model.WeekendHolidayWorkingHour.strDesignationID == null ? "" : model.WeekendHolidayWorkingHour.strDesignationID;
                    string _strDepartmentID = model.WeekendHolidayWorkingHour.strDepartmentID == null ? "" : model.WeekendHolidayWorkingHour.strDepartmentID;
                    string _strCompanyID = model.WeekendHolidayWorkingHour.strCompanyID == null ? "" : model.WeekendHolidayWorkingHour.strCompanyID;
                    string _strLocationID = model.WeekendHolidayWorkingHour.strLocationID == null ? "" : model.WeekendHolidayWorkingHour.strLocationID;

                    tmpModel.LstWeekendHolidayWorkingHour = tmpModel.LstWeekendHolidayWorkingHour.Where(
                         q => q.strDesignationID == _strDesignationID
                         && q.strDepartmentID == _strDepartmentID
                         && q.strCompanyID == _strCompanyID
                         && q.strLocationID == _strLocationID
                         && q.intReligionID == model.WeekendHolidayWorkingHour.intReligionID
                         && q.intCategoryCode == model.WeekendHolidayWorkingHour.intCategoryCode).ToList();
                    if (tmpModel.LstWeekendHolidayWorkingHour.Count > 0)
                    {
                        if (IsEdit && tmpModel.LstWeekendHolidayWorkingHour[0].intRowID == model.WeekendHolidayWorkingHour.intRowID)
                            return true;

                        model.Message = Util.Messages.GetErroMessage("This data already exists");
                        return false;
                    }
                }
            }
           
            //if (tmpModel.LstWeekendHolidayWorkingHour.Count > 0)
            //{
            //    if (IsEdit)
            //    {
            //        if ((tmpModel.LstWeekendHolidayWorkingHour.Count == 1) && (tmpModel.LstWeekendHolidayWorkingHour[0].intRowID == model.WeekendHolidayWorkingHour.intRowID))
            //            return true;
            //    }
            //    model.Message = Util.Messages.GetErroMessage("Record already exists.");
            //    return false;
            //}
            
            return isvalid;
        }

        [HttpGet]
        public JsonResult getEmployeeInformation(WeekendHolidayWorkingHourModels model)
        {
            Employee objEmp = new Employee();
            if (!string.IsNullOrEmpty(model.WeekendHolidayWorkingHour.strEmpID))
            {
                objEmp = model.GetEmployeeInfo(model.WeekendHolidayWorkingHour.strEmpID);
                               
                model.WeekendHolidayWorkingHour.strDepartment = objEmp.strDepartment;
                model.WeekendHolidayWorkingHour.strDesignation = objEmp.strDesignation;
                model.WeekendHolidayWorkingHour.strLocation = model.Location.Where(q => q.Value == objEmp.strLocationID).First().Text;
                model.WeekendHolidayWorkingHour.strCompany = model.Company.Where(q => q.Value == objEmp.strCompanyID).First().Text;

            }

            return Json(model.WeekendHolidayWorkingHour, JsonRequestBehavior.AllowGet);
        }
            
    }
}
