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
using LMS.BLL;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class OfflineLeaveApplicationController : Controller
    {
        LeaveTypeModels objLeaveType = new LeaveTypeModels();

        //GET: /OfflineLeaveApplication/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

       
        //GET: /OfflineLeaveApplication/OfflineLeaveApplication
        [HttpGet]
        [NoCache]
        public ActionResult OfflineLeaveApplication(int? page)
        {
            LeaveApplicationModels model = SetPaging(page);
            return View(model);
        }


        //POST: /OfflineLeaveApplication/OfflineLeaveApplication
        [HttpPost]
        [NoCache]
        public ActionResult OfflineLeaveApplication(int? page, LeaveApplicationModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "dtApplyDate";
            model.strSortType = LMS.Util.DataShortBy.DESC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            model.LeaveApplication = new LeaveApplication();

            LeaveApplication objSearch = model.LeaveApplication;

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
            objSearch.strDepartmentID = model.StrSearchDepartmentId;
            objSearch.strDesignationID = model.StrSearchDesignationId;
            objSearch.intLeaveTypeID = model.intSearchLeaveTypeId;
            objSearch.intAppStatusID = model.IntSearchApplicationStatusId;
            objSearch.strApprovalProcess = Utils.ApprovalProcess.FALSE;
            
            // Added For BEPZA
            if(LoginInfo.Current.LoggedZoneId > 0)
             objSearch.ZoneId = LoginInfo.Current.LoggedZoneId;

            model.LstLeaveApplication = model.GetLeaveApplicationPaging(objSearch, false);
            model.LstLeaveApplicationPaging = model.LstLeaveApplication.ToPagedList(currentPageIndex, AppConstant.PageSize);

            return PartialView(LMS.Util.PartialViewName.OfflineLeaveApplication, model);
        }


        //GET: /OfflineLeaveApplication/GetLedger
        [NoCache]
        public ActionResult GetLedger(LeaveApplicationModels model)
        {
            model.LstLeaveLedger = new List<LeaveLedger>();
            model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);
            model.LeaveApplication.fltNetBalance = 0;
            //if (model.LeaveApplication.intLeaveTypeID > 0 && model.LstLeaveLedger != null)
            if (model.LstLeaveLedger != null)
            {
                LeaveLedger objLvLedger = new LeaveLedger();
                objLvLedger = model.LstLeaveLedger.Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
                if (objLvLedger != null)
                {
                    model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - (objLvLedger.fltSubmitted + objLvLedger.fltApplied);
                    if (model.LeaveApplication.fltNetBalance < 0 && model.LeaveApplication.fltWithoutPayDuration > 0)
                    { model.LeaveApplication.fltNetBalance = 0; }
                }
            }

            return PartialView(LMS.Util.PartialViewName.LeaveLedger, model);
        }

        
        //POST: /OfflineLeaveApplication/getPagedData
        [HttpPost]
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
            
            return PartialView(LMS.Util.PartialViewName.OfflineLeaveApplication, model);
        }


        //GET: /OfflineLeaveApplication/Details/5
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();
            model.Message = Messages.GetErroMessage("");
            try
            {
                ModelState.Clear();
                model = GetDetails(Id);               
             }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        //POST: /OfflineLeaveApplication/Details
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
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
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
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        //POST: /OfflineLeaveApplication/OfflineDelete
        [HttpPost]
        [NoCache]
        public ActionResult OfflineDelete(LeaveApplicationModels model)
        {
            string strmsg = "";
            try
            {
                model.OfflineDelete(model.LeaveApplication.intApplicationID, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg);
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.LeaveApplication = new LeaveApplication();
                    InitializeModel(model);
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.OfflineLeaveApplicationDetails, model);
        }

        
        //GET: /OfflineLeaveApplication/OfflineLeaveApplicationAdd
        [HttpGet]
        [NoCache]
        public ActionResult OfflineLeaveApplicationAdd(string id)
        {

            LeaveApplicationModels model = new LeaveApplicationModels();
            model.Message = Messages.GetErroMessage("");

            try
            {
                InitializeModel(model);

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }
        

        //POST: /OfflineLeaveApplication/OfflineApprove
        [HttpPost]
        [NoCache]
        public ActionResult OfflineApprove(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                if (model.IsLeaveTypeApplicable(model) == true)
                {
                    i = model.OfflineApprove(model, out strmsg);
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
                        if (!string.IsNullOrEmpty(model.LeaveApplication.strSupervisorID))
                        {
                            model.Message = Util.Messages.GetSuccessMessage("Application submitted successfully.");
                        }
                        else
                        {
                            model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        }
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
                    model.Message = Util.Messages.GetErroMessage("Selected leave type is not applicable for the employee.");
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.OfflineLeaveApplicationDetails, model);
        }

        
        //POST: /OfflineLeaveApplication/OfflineUpdate
        [HttpPost]
        [NoCache]
        public ActionResult OfflineUpdate(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                if (model.IsLeaveTypeApplicable(model) == true)
                {
                    i = model.OfflineUpdate(model, out strmsg);

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
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
                        if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                        {
                            model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
                            model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                            model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                        }

                        if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                        { model.LeaveApplication.IsHourly = true; }
                        else if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDay)
                        { model.LeaveApplication.IsFullDay = true; }
                        else if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
                        { model.LeaveApplication.IsFullDayHalfDay = true; }

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
                    model.Message = Util.Messages.GetErroMessage("Selected leave type is not applicable for the employee.");
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.OfflineLeaveApplicationDetails, model);
        }


        // POST: /OfflineLeaveApplication/OfflineCancel
        [HttpPost]
        [NoCache]
        public ActionResult OfflineCancel(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                i = model.OfflineCancel(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = strmsg.ToString();
                }
                else
                {
                    model = this.GetDetails(int.Parse(model.LeaveApplication.intApplicationID.ToString()));
                    model.Message = Util.Messages.GetSuccessMessage("Application canceled successfully");
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetSuccessMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.OfflineLeaveApplicationDetailsPreview, model);
        }



        //POST: /OfflineLeaveApplication/SaveLeaveApplication
        [HttpPost]
        [NoCache]
        public ActionResult SaveLeaveApplication(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                if (model.IsLeaveTypeApplicable(model) == true)
                {
                    if (model.LeaveApplication.strLeaveType.ToLower ().Contains("without pay") )
                    {
                        model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltDuration;
                        model.LeaveApplication.fltWithPayDuration = 0;
                    }

                    // Added For BEPZA 30-Jan-2017
                    model.LeaveApplication.bitIsApprovalProcess = true;
                    model.LeaveApplication.intAppStatusID = 6; // Submited 
                    model.LeaveApplication.strPLID = model.LeaveApplication.strSupervisorID;
                    // END
                    i = model.SaveData(model, out strmsg);

                    if (strmsg.ToString().Length > 0)
                    {
                        model.Message = strmsg.ToString();
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.LeaveApplication = new LeaveApplication();
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
                model.Message = Util.Messages.GetSuccessMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(model);
        }

        
        //POST: /OfflineLeaveApplication/Create
        [HttpPost]
        [NoCache]
        public ActionResult Create(FormCollection collection)
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

        
        //GET: /OfflineLeaveApplication/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }


        //POST: /OfflineLeaveApplication/Edit/5
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


       
        
        //GET: /OfflineLeaveApplication/GetLeaveYearInfo  
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

        //GET: /OfflineLeaveApplication/ValidateLeaveApplication
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

        //GET: /OfflineLeaveApplication/GetAuthorityResPersonLeaveStatus
        [NoCache]
        public JsonResult GetAuthorityResPersonLeaveStatus(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            List<LeaveApplication> objLvApps = new List<LeaveApplication>();
            LeaveApplication objLvApp = new LeaveApplication();
            try
            {
                if(! string.IsNullOrEmpty(model.LeaveApplication.strOfflineApprovedById))
                {
                    objLvApps = model.GetEmployeeLeaveApplications(model.LeaveApplication.strOfflineApprovedById, model.LeaveApplication.intLeaveYearID).Where(c => c.intAppStatusID == 1 && model.LeaveApplication.dtApplyDate >= c.dtApplyFromDate && model.LeaveApplication.dtApplyDate <= c.dtApplyToDate).OrderByDescending(c => c.dtApplyFromDate).ToList();
                }
                else
                {
                    objLvApps = model.GetEmployeeLeaveApplications(model.LeaveApplication.strSupervisorID, model.LeaveApplication.intLeaveYearID).Where(c => c.intAppStatusID == 1 && model.LeaveApplication.dtApplyDate >= c.dtApplyFromDate && model.LeaveApplication.dtApplyDate <= c.dtApplyToDate).OrderByDescending(c => c.dtApplyFromDate).ToList();
                }

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

        //GET: /OfflineLeaveApplication/GetEmployeeInfo
        [NoCache]
        public JsonResult GetEmployeeInfo(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            Employee objEmp = new Employee();
            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
            try
            {               
                if (!string.IsNullOrEmpty(model.LeaveApplication.strEmpID) && model.StrEmpSearch.ToString()=="0")
                {
                    objEmp = model.GetEmployeeInfo(model.LeaveApplication.strEmpID);
                    if (objEmp != null)
                    {
                        LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(model.LeaveApplication.strEmpID, "");
                        model.intNodeID = loginDetails.intNodeID > 0 ? loginDetails.intNodeID : 0;
                        model.LeaveApplication.intDestNodeID = model.intNodeID;
                    }
                }
                else if (!string.IsNullOrEmpty(model.LeaveApplication.strOfflineApprovedById) && model.StrEmpSearch.ToString() == "1")
                {
                    objEmp = model.GetEmployeeInfo(model.LeaveApplication.strOfflineApprovedById);
                }
                else if (!string.IsNullOrEmpty(model.LeaveApplication.strResponsibleId) && model.StrEmpSearch.ToString() == "2")
                {
                    objEmp = model.GetEmployeeInfo(model.LeaveApplication.strResponsibleId);
                }
                else if (!string.IsNullOrEmpty(model.LeaveApplication.strSupervisorInitial) && model.StrEmpSearch.ToString() == "3")
                {
                    objEmp = model.GetEmployeeInfo(model.LeaveApplication.strSupervisorInitial);
                }
                if (objEmp == null)
                {
                    objEmp = new Employee();
                }

                list.Add(objEmp.strDesignationID);
                list.Add(objEmp.strDesignation);
                list.Add(objEmp.strDepartmentID);
                list.Add(objEmp.strDepartment);              
                list.Add(model.GetEmployeeLeaveType(model).Distinct().OrderBy(x=>x.Text));
                list.Add(model.GetApprovers(model));
                list.Add(model.intNodeID);
                list.Add(objEmp.strLocation);
                list.Add(objEmp.strEmpName);
                list.Add(model.Approver);
                list.Add(objEmp.EmployeeId);
            }
            catch (Exception ex)
            { }
            return Json(list);
        }

        /// <summary>
        /// Calcutate and Get Leave Duration 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NoCache]
        public JsonResult CalcutateDuration(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            double fltDuration = 0, dblNetBL = 0, dblHours = 0, dblWithPay = 0, dblWithoutPay = 0; 
            LeaveLedger objLvLedger = new LeaveLedger();
            BLL.LeaveApplicationBLL objBLL = new BLL.LeaveApplicationBLL();
            string leaveYearDate = string.Empty;
            int? intLeaveYearID;
            

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
                    //objLvLedger = model.LstLeaveLedger.Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
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
                            dblNetBL = Math.Round(objLvLedger.fltCB - dblHours, 2);
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
                if (leaveType != null && leaveType.isServiceLifeType == false)
                {
                    model.ActiveLeaveYear = model.GetLeaveYearByLeaveYearType(leaveType.intLeaveYearTypeId, LoginInfo.Current.strCompanyID);
                    intLeaveYearID = model.ActiveLeaveYear.intLeaveYearID;
                    //leaveYearDate = model.ActiveLeaveYear.dtStartDate.ToString("dd-MM-yyyy") + " To " + model.ActiveLeaveYear.dtEndDate.ToString("dd-MM-yyyy");
                }
                else
                {
                    intLeaveYearID = 0;
                    //leaveYearDate = "Not Applicable";
                }

                #endregion
                

                list.Add(fltDuration);
                list.Add(dblWithPay);
                list.Add(dblWithoutPay);
                list.Add(dblNetBL);
                list.Add(intLeaveYearID);
                list.Add(objLvLedger.fltCB);
            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage("Unable to calculate duration.");
            }

            return Json(list);
        }       
        
        [NoCache]
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

            //model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            //model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID;
            //model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            //model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            //model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.StrSearchApplyFrom = DateTime.Parse(DateTime.Today.Month.ToString() + "-01-" + DateTime.Today.Year.ToString()).ToString(LMS.Util.DateTimeFormat.Date);
            model.StrSearchApplyTo = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);

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
            objSearch.strDepartmentID = model.StrSearchDepartmentId;
            objSearch.strDesignationID = model.StrSearchDesignationId;
            objSearch.intLeaveTypeID = model.intSearchLeaveTypeId;
            objSearch.intAppStatusID = model.IntSearchApplicationStatusId;

            objSearch.strApprovalProcess = Utils.ApprovalProcess.FALSE;
            // Added For BEPZA
            objSearch.ZoneId = LoginInfo.Current.LoggedZoneId;

            model.LstLeaveApplication = model.GetLeaveApplicationPaging(objSearch, false);
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

                model.strHalfDayFromTime = model.LeaveApplication.intDurationID > 0 ? model.LeaveApplication.strApplyFromTime : string.Empty;
                model.strHalfDayToTime = model.LeaveApplication.intDurationID > 0 ? model.LeaveApplication.strApplyToTime : string.Empty;

                model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
                model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
                model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
                model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

                Employee objEmp = new Employee();

                if (!string.IsNullOrEmpty(model.LeaveApplication.strEmpID))
                {
                    objEmp = model.GetEmployeeInfo(model.LeaveApplication.strEmpID);
                    if (objEmp != null)
                    {
                        model.LeaveApplication.strEmpInitial = objEmp.strEmpInitial;
                    }
                }

                //---[get offline approved by info]-------------
                if (!string.IsNullOrEmpty(model.LeaveApplication.strOfflineApprovedById))
                {
                    objEmp = model.GetEmployeeInfo(model.LeaveApplication.strOfflineApprovedById);
                    //model.strOffAuthorDesignation = objEmp.strDesignation;
                    //model.strOffAuthorDepartment = objEmp.strDepartment;
                    model.LeaveApplication.strApprovedByInitial = objEmp.strEmpInitial;
                }

                //---[get responsible info]----------------------
                if (!string.IsNullOrEmpty(model.LeaveApplication.strResponsibleId))
                {
                    objEmp = model.GetEmployeeInfo(model.LeaveApplication.strResponsibleId);                    
                    //model.strResDesignation = objEmp.strDesignation;
                    //model.strResDepartment = objEmp.strDepartment;
                    model.LeaveApplication.strResponsibleInitial = objEmp.strEmpInitial;
                }

                //---[get net balance]----------------------
                LeaveLedger objLvLedger = new LeaveLedger();
                objLvLedger = model.GetLeaveStatus(model.LeaveApplication, false).Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
                if (objLvLedger != null)
                {
                    model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - objLvLedger.fltSubmitted;
                }   
                
                //---[get ledger history]---------------------
                model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, true);
            
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }
            return model;
        }
        
        [NoCache]
        private void InitializeModel(LeaveApplicationModels model)
        {
            model.LeaveApplication.dtApplyDate = DateTime.Today;
            model.LeaveApplication.strApplyFromDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.strApplyToDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.strOffLineAppvDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.bitIsApprovalProcess = true; // Change Status For BEPZA
            model.LeaveApplication.bitIsDiscard = false;
            model.LeaveApplication.strApplicationType = LMS.Util.LeaveDurationType.FullDay;
            model.LeaveApplication.IsFullDay = true;
            model.LeaveApplication.strApplicationType = "FullDay";
            model.LeaveApplication.intAppStatusID = 6;
            //model.LeaveApplication.intAppStatusID = 6;
            //model.LeaveApplication.intLeaveYearID = LoginInfo.Current.intLeaveYearID;
            model.LeaveApplication.strSupervisorID = "";

            model.intNodeID = 0; 
            model.LeaveApplication.intDestNodeID = 0; 
            model.strAuthorDesignation = "";
            model.strAuthorDepartment = "";
            model.strOffAuthorDesignation = "";
            model.strOffAuthorDepartment = "";

            //model.intNodeID = LoginInfo.Current.intDestNodeID;  // Added For BEPZA
            //model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID; // Added For BEPZA
            //model.LeaveApplication.intDestNodeID = LoginInfo.Current.intDestNodeID; // Added For BEPZA

            //model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            //model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            //model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            //model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.LstLeaveLedger = new List<LeaveLedger>();
        }

       

    }
}
