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
    public class LeaveAdjustmentController : Controller
    {
        public enum SortDirection { Ascending, Decending }

        //GET: /LeaveAdjustment/
        public ActionResult Index()
        {
            return View();
        }

        
        //GET: /LeaveAdjustment/LeaveAdjustment
        [HttpGet]
        [NoCache]
        public ActionResult LeaveAdjustment(int? page)
        {
            LeaveApplicationModels model = SetPaging(page);
            ModelState.Clear();
            HttpContext.Response.Clear();
            return View(model);
        }


        //POST: /LeaveAdjustment/LeaveAdjustment
        [HttpPost]
        [NoCache]
        public ActionResult LeaveAdjustment(int? page, LeaveApplicationModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "dtApplyDate";
            model.strSortType =LMS.Util.DataShortBy.DESC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            model.LeaveApplication = new LeaveApplication();

            LeaveApplication objSearch = model.LeaveApplication;

            SetSearchParamiters(objSearch, model);
            model.LstLeaveApplicationPaging = model.LstLeaveApplication.ToPagedList(currentPageIndex, AppConstant.PageSize);

            HttpContext.Response.Clear();

            return PartialView(LMS.Util.PartialViewName.LeaveAdjustment, model);
        }


        //GET: /LeaveAdjustment/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();
            try
            {
                GetAdjustmnetApplicationByID(model, Id);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(model);
        }


        //POST: /LeaveAdjustment/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                if (model.IsLeaveTypeApplicable(model) == true)
                {
                    i = model.SaveData(model, out strmsg);

                    if (strmsg.ToString().Length > 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(strmsg.ToString());
                    }
                    else
                    {
                        model.GetLeaveApplicationAll();
                        model.LstLeaveApplication1 = model.LstLeaveApplication.AsPagination(1, AppConstant.PageSize);

                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        ModelState.Clear();
                    }
                }
                else
                {
                    model.Message = Util.Messages.GetErroMessage("Selected leave type is not applicable for the employee.");
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }

            return View(model);
        }

        //GET: /LeaveAdjustment/LeaveAdjustmentAdd
        [HttpGet]
        [NoCache]
        public ActionResult LeaveAdjustmentAdd(string id)
        {

            LeaveApplicationModels model = new LeaveApplicationModels();
            model.Message = Util.Messages.GetSuccessMessage("");

            try
            {
                InitializeModel(model);

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveAdjustmentAdd, model);

        }

             
        //POST: /LeaveAdjustment/OnlineAdjustmentSubmit
        [HttpPost]
        [NoCache]
        public ActionResult OnlineAdjustmentSubmit(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                // TODO: Add insert logic here

                i = model.OnlineAdjustmentSubmit(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {

                    if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                    {
                        model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                        if (model.LeaveApplication.fltWithoutPayDuration > 0)
                        {
                            model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                        }
                    }

                    model.LstLeaveLedger = new List<LeaveLedger>();

                    model.Message = Util.Messages.GetErroMessage(strmsg);
                }
                else if (i > 0)
                {

                    model.Message = Util.Messages.GetSuccessMessage("Application submitted successfully");

                    i = int.Parse(model.LeaveApplication.intApplicationID.ToString());
                    model.LeaveApplication = new LeaveApplication();
                    GetAdjustmnetApplicationByID(model, i);

                    ModelState.Clear();
                    return PartialView(LMS.Util.PartialViewName.LeaveAdjustmentDetailsPreview, model);

                }
                else if (i < 0)
                {

                    if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                    {
                        model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                        if (model.LeaveApplication.fltWithoutPayDuration > 0)
                        {
                            model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                        }
                    }
                    model.LstLeaveLedger = new List<LeaveLedger>();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveAdjustmentDetails, model);
        }

        //POST: /LeaveAdjustment/OnlineCancel
        [HttpPost]
        [NoCache]
        public ActionResult OnlineCancel(LeaveApplicationModels model)
        {

            int i = 0;
            string strmsg = "";
            try
            {
                i = model.OnlineCancel(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = strmsg.ToString();
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage("Application canceled successfully");

                    i = int.Parse(model.LeaveApplication.intApplicationID.ToString());

                    model.LeaveApplication = new LeaveApplication();
                    GetAdjustmnetApplicationByID(model, i);
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveAdjustmentDetailsPreview, model);
        }


        //GET: /LeaveAdjustment/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //POST: /LeaveAdjustment/Edit/5
        [HttpPost]
        [NoCache]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        //GET: /LeaveAdjustment/GetLeaveYearInfo
        [NoCache]
        public JsonResult GetLeaveYearInfo(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            LeaveYear objLvYear = new LeaveYear();
            try
            {
                if (model.intSearchLeaveYearId > 0)
                {
                    objLvYear = model.GetLeaveYear(model.intSearchLeaveYearId);

                }
                list.Add(objLvYear.strStartDate);
                list.Add(objLvYear.strEndDate);
                list.Add(objLvYear.strYearTitle);

            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }

        //GET: /LeaveAdjustment/GetAuthorityResPersonLeaveStatus
        [NoCache]
        public JsonResult GetAuthorityResPersonLeaveStatus(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            List<LeaveApplication> objLvApps = new List<LeaveApplication>();
            LeaveApplication objLvApp = new LeaveApplication();
            try
            {
                objLvApps = model.GetEmployeeLeaveApplications(model.LeaveApplication.strSupervisorID, model.LeaveApplication.intLeaveYearID).Where(c => c.intAppStatusID == 1 && model.LeaveApplication.dtApplyDate >= c.dtApplyFromDate && model.LeaveApplication.dtApplyDate <= c.dtApplyToDate).OrderByDescending(c => c.dtApplyFromDate).ToList();

                if (objLvApps.Count > 0)
                {
                    list.Add("The authorized person is on leave from " + objLvApps[0].dtApplyFromDate.ToString(LMS.Util.DateTimeFormat.Date) + " to " + objLvApps[0].dtApplyToDate.ToString(LMS.Util.DateTimeFormat.Date) + ".\r\n Do you want to proceed?");
                }
            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }


        //GET: /LeaveAdjustment/GetAuthorityInfo
        [NoCache]
        public JsonResult GetAuthorityInfo(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            Employee objEmp = new Employee();
            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
            try
            {

                if (!string.IsNullOrEmpty(model.LeaveApplication.strSupervisorID))
                {
                    objEmp = model.GetEmployeeInfo(model.LeaveApplication.strSupervisorID);
                }

                list.Add(objEmp.strDesignationID);
                list.Add(objEmp.strDesignation);
                list.Add(objEmp.strDepartmentID);
                list.Add(objEmp.strDepartment);                
                list.Add(objEmp.strLocation);

            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }
        
        //GET: /LeaveAdjustment/CalcutateDuration_OLD
        [NoCache]
        public JsonResult CalcutateDuration_OLD(LeaveApplicationModels model)
        {
            double fltDuration = 0;
            try
            {
                BLL.LeaveApplicationBLL objBLL = new BLL.LeaveApplicationBLL();

                fltDuration = objBLL.GetDuration(model.LeaveApplication.strEmpID, model.LeaveApplication.intLeaveYearID, model.LeaveApplication.intLeaveTypeID, model.LeaveApplication.strApplicationType, model.LeaveApplication.dtApplyFromDate, model.LeaveApplication.dtApplyToDate, LoginInfo.Current.strCompanyID);

                model.LeaveApplication.fltDuration = fltDuration;
                model.LeaveApplication.fltWithPayDuration = fltDuration;
                model.LeaveApplication.fltWithoutPayDuration = 0;

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }

            return Json(fltDuration);
        }

        //GET: /LeaveAdjustment/CalcutateDuration
        [NoCache]
        public JsonResult CalcutateDuration(LeaveApplicationModels model)
        {
            double fltDuration = 0;
            try
            {
                BLL.LeaveApplicationBLL objBLL = new BLL.LeaveApplicationBLL();
                DateTime dtFromDateTime = model.LeaveApplication.dtApplyFromDate;
                DateTime dtToDateTime = model.LeaveApplication.dtApplyToDate;
                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.FullDay)
                {
                    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                    dtfi.ShortDatePattern =LMS.Util.DateTimeFormat.DateTime;
                    dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;

                    char[] sepAr = { ':', ' ' };

                    if (!string.IsNullOrEmpty(model.LeaveApplication.strApplyFromTime))
                    {
                        string[] time = model.LeaveApplication.strApplyFromTime.Split(sepAr);

                        try
                        {
                            string strDt = model.LeaveApplication.dtApplyFromDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                            dtFromDateTime = Convert.ToDateTime(strDt, dtfi);
                        }
                        catch (Exception ex)
                        { }
                    }
                    if (!string.IsNullOrEmpty(model.LeaveApplication.strApplyToTime))
                    {
                        string[] time = model.LeaveApplication.strApplyToTime.Split(sepAr);


                        try
                        {
                            string strDt = model.LeaveApplication.dtApplyToDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                            dtToDateTime = Convert.ToDateTime(strDt, dtfi);
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                fltDuration = objBLL.GetDuration(model.LeaveApplication.strEmpID, model.LeaveApplication.intLeaveYearID, model.LeaveApplication.intLeaveTypeID, model.LeaveApplication.strApplicationType, dtFromDateTime, dtToDateTime, LoginInfo.Current.strCompanyID);

                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                {
                    if (fltDuration > 0)
                    {
                        TimeSpan ts1 = dtToDateTime.Subtract(dtFromDateTime);
                        fltDuration = Math.Round(ts1.TotalHours, 2);

                        if (fltDuration > (double)LoginInfo.Current.fltOfficeTime)
                        {
                            fltDuration = (double)LoginInfo.Current.fltOfficeTime;
                        }
                    }
                }

                model.LeaveApplication.fltDuration = fltDuration;
                model.LeaveApplication.fltWithPayDuration = fltDuration;
                model.LeaveApplication.fltWithoutPayDuration = 0;


            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }

            return Json(fltDuration);
        }

        //GET: /LeaveAdjustment/IsAppliedForAdjustment
        [NoCache]
        public JsonResult IsAppliedForAdjustment(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            LeaveApplication objLVApp = new LeaveApplication();
            try
            {
                objLVApp = model.GetEmployeeLastApprovedApplication(LoginInfo.Current.strEmpID, LoginInfo.Current.intLeaveYearID);

                if (objLVApp != null && objLVApp.intApplicationID > 0)
                {
                    long intAppID = objLVApp.intApplicationID;

                    objLVApp = new LeaveApplication();
                    objLVApp = model.GetEmployeeLeaveApplications(LoginInfo.Current.strEmpID, LoginInfo.Current.intLeaveYearID, true).Where(c => c.intRefApplicationID == intAppID && (c.intAppStatusID == 1 || c.intAppStatusID == 4 || c.intAppStatusID == 6)).SingleOrDefault();

                    if (objLVApp != null)
                    {
                        list.Add("Your last approved leave application already applied for adjustment.");
                    }

                }
                else
                {
                    list.Add("No approved application has been found for adjustment.");
                }


                model.LeaveApplication.fltWithoutPayDuration = 0;

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }


            return Json(list);
        }



        private void InitializeModel(LeaveApplicationModels model)
        {

            //---[For Last Approved Leave]--------------------------------------

            model.RefLeaveApplication = model.GetEmployeeLastApprovedApplication(LoginInfo.Current.strEmpID, LoginInfo.Current.intLeaveYearID);

            if (model.RefLeaveApplication != null)
            {
                model.LeaveApplication.bitIsAdjustment = true;
                model.LeaveApplication.intLeaveTypeID = model.RefLeaveApplication.intLeaveTypeID;
                model.LeaveApplication.intRefApplicationID = model.RefLeaveApplication.intApplicationID;

            }

            //---[For New Adjustment Entry]--------------------------------------
            model.LeaveApplication.dtApplyDate = DateTime.Today;
            model.LeaveApplication.strApplyFromDate = model.RefLeaveApplication.strApplyFromDate;
            model.LeaveApplication.strApplyToDate = model.RefLeaveApplication.strApplyToDate;
            model.LeaveApplication.strApplicationType = model.RefLeaveApplication.strApplicationType;
            model.LeaveApplication.intDurationID = model.RefLeaveApplication.intDurationID;
            model.LeaveApplication.strHalfDayFor = model.RefLeaveApplication.strHalfDayFor;


            if (model.RefLeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.FullDay)
            {
                model.LeaveApplication.strApplyFromTime = model.RefLeaveApplication.strApplyFromTime;
                model.LeaveApplication.strApplyToTime = model.RefLeaveApplication.strApplyToTime;
                model.strHalfDayFromTime = model.RefLeaveApplication.intDurationID >0 ? model.RefLeaveApplication.strApplyFromTime : string.Empty;
                model.strHalfDayToTime = model.RefLeaveApplication.intDurationID > 0 ? model.RefLeaveApplication.strApplyToTime : string.Empty;
            }

            model.LeaveApplication.bitIsApprovalProcess = true;
            model.LeaveApplication.bitIsDiscard = false;

            if (model.RefLeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
            {
                model.LeaveApplication.IsFullDayHalfDay = false;
                model.LeaveApplication.IsHourly = true;
                model.RefLeaveApplication.IsHourly = true;
                model.LeaveApplication.IsFullDay = false;
            }
            else if (model.RefLeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
            {
                model.LeaveApplication.IsFullDayHalfDay = true;
                model.LeaveApplication.IsHourly = false;
                model.RefLeaveApplication.IsHourly = false;
                model.LeaveApplication.IsFullDay = false;
            }
            else
            {
                model.LeaveApplication.IsFullDayHalfDay = false;
                model.LeaveApplication.IsHourly = false;
                model.RefLeaveApplication.IsHourly = false;
                model.LeaveApplication.IsFullDay = true;
            }

            model.LeaveApplication.fltDuration = model.RefLeaveApplication.fltDuration;
            model.LeaveApplication.fltWithPayDuration = model.RefLeaveApplication.fltWithPayDuration;
            model.LeaveApplication.fltWithoutPayDuration = model.RefLeaveApplication.fltWithoutPayDuration;





            model.LeaveApplication.intAppStatusID = 6;  //Submit
            model.LeaveApplication.strEmpID = LoginInfo.Current.strEmpID;
            model.LeaveApplication.strEmpName = LoginInfo.Current.EmployeeName;






            Employee objEmp = new Employee();

            if (!string.IsNullOrEmpty(model.LeaveApplication.strEmpID))
            {
                objEmp = model.GetEmployeeInfo(model.LeaveApplication.strEmpID);

                model.LeaveApplication.strDepartmentID = objEmp.strDepartmentID;
                model.LeaveApplication.strDepartment = objEmp.strDepartment;

                model.LeaveApplication.strDesignationID = objEmp.strDesignationID;
                model.LeaveApplication.strDesignation = objEmp.strDesignation;
                model.LeaveApplication.strBranch = objEmp.strLocation;
            }

            model.LeaveApplication.intLeaveYearID = LoginInfo.Current.intLeaveYearID;
            model.LeaveApplication.strSupervisorID = "";

            model.intNodeID = LoginInfo.Current.intDestNodeID;
            model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID;

            model.LeaveApplication.intDestNodeID = LoginInfo.Current.intDestNodeID;


            model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

        }

        private void SetSearchParamiters(LeaveApplication objSearch, LeaveApplicationModels model)
        {

            if (!string.IsNullOrEmpty(model.strSearchID))
            {
                objSearch.strEmpID = model.strSearchID.Trim();
            }
            else
            {
                objSearch.strEmpID = model.strSearchID;
            }

            if (!string.IsNullOrEmpty(model.strSearchName))
            {
                objSearch.strEmpName = model.strSearchName.Trim();
            }
            else
            {
                objSearch.strEmpName = model.strSearchName;
            }
            objSearch.strApplyFromDate = model.StrSearchApplyFrom == null ? model.StrYearStartDate : model.StrSearchApplyFrom;
            objSearch.strApplyToDate = model.StrSearchApplyTo == null ? model.StrYearEndDate : model.StrSearchApplyTo;
            objSearch.intLeaveYearID = model.intSearchLeaveYearId;
            objSearch.intLeaveTypeID = model.intSearchLeaveTypeId;
            objSearch.intAppStatusID = model.IntSearchApplicationStatusId;
            objSearch.strApprovalProcess = Utils.ApprovalProcess.TRUE;
            objSearch.bitIsAdjustment = true;

            model.LstLeaveApplication = model.GetLeaveApplicationPaging(objSearch, false, model.IsSearch, false);
        }

        private LeaveApplicationModels SetPaging(int? page)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "dtApplyDate";
            model.strSortType = LMS.Util.DataShortBy.DESC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;


            model.LeaveApplication = new LeaveApplication();
            LeaveApplication objSearch = model.LeaveApplication;
            model.strSearchID = LoginInfo.Current.strEmpID;
            model.strSearchName = LoginInfo.Current.EmployeeName;

            model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID;
            model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.StrSearchApplyFrom = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.StrSearchApplyTo = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);

            SetSearchParamiters(objSearch, model);
            model.LstLeaveApplicationPaging = model.LstLeaveApplication.ToPagedList(currentPageIndex, AppConstant.PageSize);
            return model;
        }

        private void GetAdjustmnetApplicationByID(LeaveApplicationModels model, int intAppID)
        {
            model.LeaveApplication = model.GetLeaveApplication(intAppID);

            if (intAppID > 0)
            {
                model.ApprovalFlowList = model.GetApprovalFlowList(intAppID);
            }

            model.RefLeaveApplication = model.GetLeaveApplication(int.Parse(model.LeaveApplication.intRefApplicationID.ToString()));

            if (model.RefLeaveApplication != null)
            {
                model.LeaveApplication.bitIsAdjustment = true;
                model.LeaveApplication.intLeaveTypeID = model.RefLeaveApplication.intLeaveTypeID;
                model.LeaveApplication.intRefApplicationID = model.RefLeaveApplication.intApplicationID;

                //if (model.RefLeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                //{
                //    model.RefLeaveApplication.fltDuration = model.RefLeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
                //    model.RefLeaveApplication.fltWithPayDuration = model.RefLeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                //    model.RefLeaveApplication.fltWithoutPayDuration = model.RefLeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                //}

            }

            if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
            {
                model.LeaveApplication.IsFullDayHalfDay = false;
                model.LeaveApplication.IsHourly = true;
                model.LeaveApplication.IsFullDay = false;
            }
            else if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
            {
                model.LeaveApplication.IsFullDayHalfDay = true;
                model.LeaveApplication.IsHourly = false;
                model.LeaveApplication.IsFullDay = false;
            }
            else
            {
                model.LeaveApplication.IsFullDayHalfDay = false;
                model.LeaveApplication.IsHourly = false;
                model.LeaveApplication.IsFullDay = true;
            }

            ////---add by shaiful 02 May 2011---------------------------------------
            //if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
            //{
            //    model.LeaveApplication.fltSubmittedDuration = model.LeaveApplication.fltSubmittedDuration / LoginInfo.Current.fltOfficeTime;
            //    model.LeaveApplication.fltSubmittedWithPayDuration = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;
            //    model.LeaveApplication.fltSubmittedWithoutPayDuration = model.LeaveApplication.fltSubmittedWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
            //}
            ////---------------------------------------------------------------------

            //if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
            //{
            //    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
            //    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
            //    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
            //}

            model.intNodeID = model.LeaveApplication.intDestNodeID;
            model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, true);

        }

    
    }
}
