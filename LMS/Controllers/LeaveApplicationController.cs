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
using System.Net.Mail;
using System.Web.Configuration;
using System.Net.Configuration;
using LMS.BLL;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class LeaveApplicationController : Controller
    {
        LeaveTypeModels objLeaveType = new LeaveTypeModels();
        // GET: /LeaveApplication/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {

            return View();
        }

        // GET: /LeaveApplication/LeaveApplication
        [HttpGet]
        [NoCache]
        public ActionResult LeaveApplication(int? page)
        {
            LeaveApplicationModels model = SetPaging(page);
            ModelState.Clear();
            HttpContext.Response.Clear();
            return View(model);
        }

        // POST: /LeaveApplication/LeaveApplication
        [HttpPost]
        [NoCache]
        public ActionResult LeaveApplication(int? page, LeaveApplicationModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "dtApplyDate";
            model.strSortType = LMS.Util.DataShortBy.DESC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            model.LeaveApplication = new LeaveApplication();

            LeaveApplication objSearch = model.LeaveApplication;

            SetSearchParamiters(objSearch, model);

            model.LstLeaveApplicationPaging = model.LstLeaveApplication.ToPagedList(currentPageIndex, AppConstant.PageSize);

            //ModelState.Clear();
            HttpContext.Response.Clear();

            return PartialView(LMS.Util.PartialViewName.LeaveApplication, model);
        }

        // GET: /LeaveApplication/GetLedger
        [NoCache]
        public ActionResult GetLedger(LeaveApplicationModels model)
        {
            model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);
            model.LeaveApplication.fltNetBalance = 0;
            if (model.LeaveApplication.intLeaveTypeID > 0 && model.LstLeaveLedger != null)
            {
                LeaveLedger objLvLedger = new LeaveLedger();
                objLvLedger = model.LstLeaveLedger.Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
                if (objLvLedger != null)
                {
                    model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - (objLvLedger.fltSubmitted + objLvLedger.fltApplied);
                    if (model.LeaveApplication.fltNetBalance < 0 && model.LeaveApplication.fltWithoutPayDuration >0)
                    { model.LeaveApplication.fltNetBalance = 0; }
                }
            }
            if (model.LeaveApplication.strSupervisorID == null)
            {
                model.LeaveApplication.strSupervisorID = "";
            }
            //return PartialView(LMS.Util.PartialViewName.LeaveApplicationDetails, model);
            return PartialView(LMS.Util.PartialViewName.LeaveLedger, model);
        }
        
        // GET: /LeaveApplication/GetLedger
        //[NoCache]
        //public ActionResult GetLedgerOnline(LeaveApplicationModels model)
        //{
        //    model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);
        //    if (model.LeaveApplication.strSupervisorID == null)
        //    {
        //        model.LeaveApplication.strSupervisorID = "";
        //    }
        //    return PartialView(LMS.Util.PartialViewName.LeaveLedger, model);
        //}
        
        // POST: /LeaveApplication/getPagedData
        
        
        [HttpPost]
        [NoCache]
        public ActionResult getPagedData(FormCollection collection)
        {
            string strPageIndex = collection.Get("txtPageNo");

            int pageNo = 1;
            int.TryParse(strPageIndex, out pageNo);
            if (pageNo < 1)
            {
                pageNo = 1;
            }

            LeaveApplicationModels model = new LeaveApplicationModels();

            model.GetLeaveApplicationAll();
            model.LstLeaveApplication1 = model.LstLeaveApplication.AsPagination(pageNo, AppConstant.PageSize);
            
            return PartialView(LMS.Util.PartialViewName.LeaveApplication, model);
        }
       
        // GET: /LeaveApplication/Details/5
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();

            try
            {
                model = GetDetails(Id);

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(model);
        }

        // GET: /LeaveApplication/Details
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
        
        // GET: /LeaveApplication/LeaveApplicationAdd
        [HttpGet]
        [NoCache]
        public ActionResult LeaveApplicationAdd(string id)
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

            return PartialView(LMS.Util.PartialViewName.LeaveApplicationAdd, model);
        }

        // POST: /LeaveApplication/OnlineSubmit
        [HttpPost]
        [NoCache]
        public ActionResult OnlineSubmit(LeaveApplicationModels model)
        {
            //model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);
            int i = 0;
            string strmsg = "";
            try
            {
                if (model.IsLeaveTypeApplicable(model) == true)
                {                  
                        DateTime dtfrom = DateTime.MinValue;
                        DateTime dtTo = DateTime.MaxValue;

                        //if (model.LeaveApplication.strLeaveType.ToLower().Contains("without pay"))
                        //{
                        //    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltDuration;
                        //    model.LeaveApplication.fltWithPayDuration = 0;
                        //}

                        if (string.IsNullOrEmpty(model.StrYearStartDate)==false )
                        {
                            dtfrom = Convert.ToDateTime(model.StrYearStartDate);
                            dtTo = Convert.ToDateTime(model.StrYearEndDate);
                        }

                        if ((model.LeaveApplication.dtApplyFromDate >= dtfrom && model.LeaveApplication.dtApplyFromDate <= dtTo)
                            && (model.LeaveApplication.dtApplyToDate >= dtfrom && model.LeaveApplication.dtApplyToDate <= dtTo))
                        {

                            i = model.OnlineSubmit(model, out strmsg);

                            if (strmsg.ToString().Length > 0)
                            {

                                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                                {
                                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
                                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;

                                }
                                model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);

                                model.Message = Util.Messages.GetErroMessage(strmsg);
                            }
                            else if (i >= 0)
                            {

                                model.Message = Util.Messages.GetSuccessMessage("Application submitted successfully.");
                                model.LeaveApplication = new LeaveApplication();
                                InitializeModel(model);
                                ModelState.Clear();
                            }
                            else if (i < 0)
                            {

                                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                                {
                                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
                                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                                }
                                model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);
                            }
                        }
                        else
                        {
                            model.Message = Util.Messages.GetErroMessage("Leave start and end date must be within the active leave year.");
                        }
                    
                }
                else
                {
                    //RH#2015-12-21
                    model.Message = Util.Messages.GetErroMessage("Selected leave type is not applicable for the employee.");
                }

            }
            catch (Exception ex)
            {
                string msg="";

                if (!String.IsNullOrEmpty(ex.Message)) msg += ex.Message;
                if (ex.InnerException != null) msg += ex.InnerException.Message;

                if (!String.IsNullOrEmpty(msg)) model.Message = Util.Messages.GetErroMessage(msg);
            }

            return PartialView(LMS.Util.PartialViewName.LeaveApplicationDetails, model);
        }

        // POST: /LeaveApplication/OnlineCancel
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
                    //---InitializeModel(model);
                    model = this.GetDetails(int.Parse(model.LeaveApplication.intApplicationID.ToString()));
                    model.Message = Util.Messages.GetSuccessMessage("Application canceled successfully");

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(ex.Message);
            }

            return PartialView(LMS.Util.PartialViewName.LeaveApplicationDetailsPreview, model);
        }

        // POST: /LeaveApplication/OnlineDelete
        [HttpPost]
        [NoCache]
        public ActionResult OnlineDelete(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                model.OnlineDelete(model.LeaveApplication.intApplicationID, out strmsg);

                model = SetPaging(1);

                model.Message = Util.Messages.GetSuccessMessage("Application Deleted Successfully.");
                model.LeaveApplication = new LeaveApplication();
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveApplicationDetails, model);
        }

        // POST: /LeaveApplication/Create
        [HttpPost]
        [NoCache]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: /LeaveApplication/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: /LeaveApplication/Edit/5
        [HttpPost]
        [NoCache]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        
        
        //GET: /LeaveApplication/CalcutateDuration
        [NoCache]
        public JsonResult CalcutateDuration(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            double fltDuration = 0,dblNetBL = 0, dblHours = 0, dblWithPay = 0, dblWithoutPay = 0;
            int nodeID = default(int);
            LeaveLedger objLvLedger = new LeaveLedger();
            BLL.LeaveApplicationBLL objBLL = new BLL.LeaveApplicationBLL();
            string leaveYearDate = string.Empty;
            int? intLeaveYearID;
            int halfAvg = 0;
            string StrYearStartDate = string.Empty;
            string StrYearEndDate = string.Empty;

            try
            {
                DateTime dtFromDateTime = model.LeaveApplication.dtApplyFromDate;
                DateTime dtToDateTime = model.LeaveApplication.dtApplyToDate;

                //if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.FullDay)
                //{
                //    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                //    dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
                //    dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;
                //    char[] sepAr = { ':', ' ' };

                //    if (!string.IsNullOrEmpty(model.LeaveApplication.strApplyFromTime))
                //    {
                //        string[] time = model.LeaveApplication.strApplyFromTime.Split(sepAr);

                //        try
                //        {
                //            string strDt = model.LeaveApplication.dtApplyFromDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                //            dtFromDateTime = Convert.ToDateTime(strDt, dtfi);
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }
                //    if (!string.IsNullOrEmpty(model.LeaveApplication.strApplyToTime))
                //    {
                //        string[] time = model.LeaveApplication.strApplyToTime.Split(sepAr);


                //        try
                //        {
                //            string strDt = model.LeaveApplication.dtApplyToDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                //            dtToDateTime = Convert.ToDateTime(strDt, dtfi);
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }
                //}
                
                fltDuration = objBLL.GetDuration(model.LeaveApplication.strEmpID, model.LeaveApplication.intLeaveYearID, model.LeaveApplication.intLeaveTypeID, model.LeaveApplication.strApplicationType, dtFromDateTime, dtToDateTime, LoginInfo.Current.strCompanyID);
                nodeID = objBLL.GetNodeID(model.LeaveApplication.strEmpID, model.LeaveApplication.intLeaveTypeID);
                
                //if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)                
                //{
                //    if (fltDuration > 0)
                //    {
                //        TimeSpan ts1 = dtToDateTime.Subtract(dtFromDateTime);
                //        fltDuration = Math.Round(ts1.TotalHours, 2);

                //        if (fltDuration > (double)LoginInfo.Current.fltOfficeTime)
                //        {
                //            fltDuration = (double)LoginInfo.Current.fltOfficeTime;
                //        }
                //    }
                //}

                //model.LeaveApplication.fltDuration = fltDuration;
                //model.LeaveApplication.fltWithPayDuration = fltDuration;
                //model.LeaveApplication.fltWithoutPayDuration = 0;

                if (model.LeaveApplication.intLeaveTypeID > 0 && model.LstLeaveLedger != null)
                {
                    objLvLedger = model.LstLeaveLedger.Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
                    if (objLvLedger != null)
                    {
                        if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                        {
                            dblHours = fltDuration / LoginInfo.Current.fltOfficeTime;
                        }
                        else
                        {
                            dblHours = fltDuration;
                        }
                        
                        
                        //dblNetBL = objLvLedger.fltCB - (objLvLedger.fltSubmitted + dblHours);
                        if (objLvLedger.fltCB >= dblHours)
                        {
                            dblWithPay = model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly ? dblHours : dblHours * LoginInfo.Current.fltOfficeTime;
                            dblWithoutPay = 0;
                            dblNetBL = Math.Round(objLvLedger.fltCB - dblHours,2);
                        }                        
                        else
                        {
                            dblWithPay = objLvLedger.fltCB;
                            dblWithoutPay = model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly ? Math.Round(dblHours - objLvLedger.fltCB, 2) : Math.Round(dblHours - objLvLedger.fltCB, 2) * LoginInfo.Current.fltOfficeTime;
                            dblNetBL = 0;
                        }

                    }
                }

                #region Getting Leave Type ID

                var leaveType = objLeaveType.GetLeaveType(model.LeaveApplication.intLeaveTypeID);
                // Added For MPA
                if (leaveType != null)
                {
                    halfAvg = leaveType.intEarnLeaveType;
                }
                // END

                if (leaveType != null && leaveType.isServiceLifeType == false)
                {
                    model.ActiveLeaveYear = model.GetLeaveYearByLeaveYearType(leaveType.intLeaveYearTypeId, LoginInfo.Current.strCompanyID);
                    intLeaveYearID = model.ActiveLeaveYear.intLeaveYearID;
                    StrYearStartDate = model.ActiveLeaveYear.dtStartDate.ToString("MM/dd/yyyy");
                    StrYearEndDate = model.ActiveLeaveYear.dtEndDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    intLeaveYearID = 0;
                    StrYearStartDate = "";
                    StrYearEndDate = "";                  
                }

                #endregion

                list.Add(fltDuration);
                list.Add(dblWithPay);
                list.Add(dblWithoutPay);
                list.Add(dblNetBL);
                list.Add(intLeaveYearID);
                list.Add(objLvLedger.fltCB);
                list.Add(nodeID);
                list.Add(StrYearStartDate);
                list.Add(StrYearEndDate);
                list.Add(halfAvg);
                //list.Add(leaveType.isServiceLifeType);
                
                //if (dblNetBL < 0)
                //{                    
                //    list.Add(fltDuration);
                //    list.Add(fltDuration - Math.Abs(dblNetBL));
                //    list.Add(Math.Round(Math.Abs(dblNetBL),2));
                //    list.Add(0.00);
                //}
                //else
                //{
                //    list.Add(fltDuration);
                //    list.Add(fltDuration);
                //    list.Add(0);
                //    list.Add(dblNetBL);
                //}  
             

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }

            return Json(list);
        }
        
        //GET: /LeaveApplication/GetLeaveYearInfo
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

        
        //GET: /LeaveApplication/GetWorkingTimeInfo
        [NoCache]
        public JsonResult GetWorkingTimeInfo(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            OfficeTimeDetails objOffTime = new OfficeTimeDetails();
            try
            {
                if (model.LeaveApplication.intDurationID > 0)
                {
                    objOffTime = model.GetWorkingTime().Where(c=> c.intDurationID == model.LeaveApplication.intDurationID).FirstOrDefault();
                }

                list.Add(objOffTime.strStartTime);
                list.Add(objOffTime.strEndTime);
                list.Add(objOffTime.fltDuration);

            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }
        
        
        //GET: /LeaveApplication/GetLeaveYearInfo
        [NoCache]
        public JsonResult ValidateLeaveApplication(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            string strVMsg = "";
            try
            {
                /*---[For FullDayHalfDay Leave]----------*/
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay && model.LeaveApplication.intDurationID > 0)
                {
                    model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                    model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                }
                model.MessageList = model.ValidateLeaveApplication(model);
                if (model.MessageList.Count > 0)
                {
                    int j = 0;
                    foreach (LMSEntity.ValidationMessage vm in model.MessageList)
                    {
                        j = j + 1;
                        if (strVMsg.Length <= 0)
                        {
                            strVMsg = "\n" + j.ToString() + ". " + vm.msg.ToString();

                        }
                        else
                        {
                            strVMsg = strVMsg + "\n" + j.ToString() + ". " + vm.msg.ToString();
                        }
                    }
                }

                list.Add(strVMsg.ToString());
            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }

        
        //GET: /LeaveApplication/GetAuthorityResPersonLeaveStatus
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

                if (!string.IsNullOrEmpty(model.LeaveApplication.strResponsibleId))
                {
                    objLvApps = new List<LeaveApplication>();
                    objLvApps = model.GetEmployeeLeaveApplications(model.LeaveApplication.strResponsibleId, model.LeaveApplication.intLeaveYearID).Where(c => c.intAppStatusID != 2 && c.intAppStatusID != 3).OrderByDescending(c => c.dtApplyFromDate).ToList();

                    if (objLvApps.Count > 0)
                    {
                        DateTime dt = model.LeaveApplication.dtApplyFromDate;
                        for (int i = 1; i <= int.Parse(model.LeaveApplication.fltDuration.ToString()); i++)
                        {
                            if (i >= 2)
                            {
                                dt = dt.AddDays(1);
                            }
                            if (dt >= objLvApps[0].dtApplyFromDate && dt <= objLvApps[0].dtApplyToDate)
                            {
                                list.Add("The responsible person is already applied for leave.\r\n Do you want to proceed?");
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }

        //GET: /LeaveApplication/GetAuthorityInfo
        [NoCache]
        public JsonResult GetEmployeeInfo(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            Employee objEmp = new Employee();
            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
            try
            {

                if (!string.IsNullOrEmpty(model.StrEmpSearch))
                {
                    objEmp = model.GetEmployeeInfo(model.StrEmpSearch); //model.GetEmployeeInfoById(model.StrEmpSearch); //
                }

                if(objEmp == null)
                {
                    objEmp = new Employee();
                }

                list.Add(objEmp.strDesignationID);
                list.Add(objEmp.strDesignation);
                list.Add(objEmp.strDepartmentID);
                list.Add(objEmp.strDepartment);
                //list.Add(objEmp.strLocation);
                list.Add(objEmp.strEmpInitial);
                list.Add(objEmp.strEmpName);
                list.Add(objEmp.EmployeeId);

            }
            catch (Exception ex)
            {

            }
            return Json(list);
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
            model.strSearchInitial = LoginInfo.Current.strEmpID; //LoginInfo.Current.LoginName; Edited for MPA
            model.strSearchName = LoginInfo.Current.EmployeeName;


            //model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            //model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID;
            //model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            //model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            //model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.StrSearchApplyFrom = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.StrSearchApplyTo = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            SetSearchParamiters(objSearch, model);

            model.LstLeaveApplicationPaging = model.LstLeaveApplication.ToPagedList(currentPageIndex, AppConstant.PageSize);
            return model;

        }
        
        private LeaveApplicationModels GetDetails(int Id)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();
            try
            {

                model.LeaveApplication = model.GetLeaveApplication(Id);

                model.ApprovalFlowList = model.GetApprovalFlowList(Id);


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

                //if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                //{
                //    model.LeaveApplication.fltSubmittedDuration = model.LeaveApplication.fltSubmittedDuration / LoginInfo.Current.fltOfficeTime;
                //    model.LeaveApplication.fltSubmittedWithPayDuration = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;
                //    model.LeaveApplication.fltSubmittedWithoutPayDuration = model.LeaveApplication.fltSubmittedWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                //}

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
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }
            return model;

        }

        private void InitializeModel(LeaveApplicationModels model)
        {
            model.LeaveApplication.dtApplyDate = DateTime.Today;
            model.LeaveApplication.strApplyFromDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.strApplyToDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.bitIsApprovalProcess = true;
            model.LeaveApplication.bitIsDiscard = false;
            model.LeaveApplication.IsFullDay = true;
            model.LeaveApplication.strApplicationType = "FullDay";
            model.LeaveApplication.strApplicationType = LMS.Util.LeaveDurationType.FullDay;

            model.LeaveApplication.intAppStatusID = 6;  //Submit
            model.LeaveApplication.strEmpID = LoginInfo.Current.strEmpID;
            model.LeaveApplication.strEmpInitial = LoginInfo.Current.LoginName;
            model.LeaveApplication.strEmpName = LoginInfo.Current.EmployeeName;

            Employee objEmp = new Employee();

            if (!string.IsNullOrEmpty(model.LeaveApplication.strEmpID))
            {
                objEmp = model.GetEmployeeInfo(model.LeaveApplication.strEmpID);

                model.LeaveApplication.strDepartmentID = objEmp.strDepartmentID;
                model.LeaveApplication.strDepartment = objEmp.strDepartment;

                model.LeaveApplication.strDesignationID = objEmp.strDesignationID;
                model.LeaveApplication.strDesignation = objEmp.strDesignation;
                //model.LeaveApplication.strBranch = objEmp.strLocation;
            }

            model.strAuthorDepartment = "";
            model.strAuthorDesignation = "";
            model.strResDepartment = "";
            model.strResDesignation = "";
            model.LeaveApplication.strSupervisorID = "";

            //model.LeaveApplication.intLeaveYearID = LoginInfo.Current.intLeaveYearID;        
            model.intNodeID = LoginInfo.Current.intDestNodeID;  // Un Comments for MPA
            model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID;
            model.LeaveApplication.intDestNodeID = LoginInfo.Current.intDestNodeID; // Un Comments for MPA


            //model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            //model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            //model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            //model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);

            // Employee photo
            EmployeePhotoBLL empBLL = new EmployeePhotoBLL();
            EmployeePhoto ep = empBLL.EmployeePhotoGet(model.LeaveApplication.strEmpID);

            if (ep == null) 
                model.IsEmployeePhoto = false;
            else
                model.IsEmployeePhoto = true;
            //
        }

        private void SetSearchParamiters(LeaveApplication objSearch, 
                                         LeaveApplicationModels model)
        {

            //if (!string.IsNullOrEmpty(model.strSearchID))
            //{
            //    objSearch.strEmpID = model.strSearchID.Trim();
            //}
            //else
            //{
            //    objSearch.strEmpID = model.strSearchID;
            //}

            if (!string.IsNullOrEmpty(model.strSearchInitial))
            {
                objSearch.strEmpInitial = model.strSearchInitial.Trim();
            }
            else
            {
                objSearch.strEmpInitial = model.strSearchInitial;
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
            objSearch.bitIsAdjustment = false;
            model.LstLeaveApplication = model.GetLeaveApplicationPaging(objSearch, false, model.IsDateWiseSearch, false);
        }

        public FileContentResult GetImage(string Id)
        {
            EmployeePhotoBLL ebll = new EmployeePhotoBLL();
            EmployeePhoto employeePhoto = null;

            if (!String.IsNullOrEmpty(Id))
            {
                employeePhoto = ebll.EmployeePhotoGet(Id);
            }

            if (employeePhoto != null)
            {
                return File(employeePhoto.PhotoSignature, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
                
    }
}
