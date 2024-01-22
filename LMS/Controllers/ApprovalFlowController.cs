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
using System.Collections;
using LMS.UserMgtService;
using LMS.BLL;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class ApprovalFlowController : Controller
    {
        //GET: /ApprovalFlow/
        [NoCache]
        [HttpGet]
        public ActionResult Index(string FID)
        {
            // return View();

            int _FID = 0;


            //if (FID.HasValue)
            //    _FID = (int)FID;
            if (!string.IsNullOrEmpty(FID))
            {
                FID = System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(FID));

                string strFID = Common.DecryptStringAES(FID, Common.GetEncryptionKey());
                _FID = Convert.ToInt32(strFID);
            }
            ApprovalFlowModels model = new ApprovalFlowModels();
            //if request come through url
            model.IsFromUrl = true;
            model.ApprovalFlow = model.GetApprovalFlow(_FID, -1, -1, "", "");


            if (Request["fromMail"] != null && Request["fromMail"].ToString().ToLower() == "true")
            { SetLoginInfo(model.ApprovalFlow.strAuthorID); }

            model.LeaveApplication = model.GetLeaveApplication(model.ApprovalFlow.intApplicationID);


            //1= Approved 2= Canceled 3=Rejected
            if (model.LeaveApplication.intAppStatusID == 1 || model.LeaveApplication.intAppStatusID == 2 || model.LeaveApplication.intAppStatusID == 3)
            {
                model.ShowPreview = true;
            }
            else
            {
                model.ShowPreview = false;
                LeaveLedger objLvLedger = new LeaveLedger();
                double fltDays = 0;
                double fltSubVal = 0;
                objLvLedger = model.GetLeaveStatus(model.LeaveApplication).Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
                if (objLvLedger != null)
                {
                    if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                    {
                        fltDays = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                        if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                        { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration; }
                        else
                        { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime; }
                    }
                    else
                    {
                        fltDays = model.LeaveApplication.fltWithPayDuration;
                        if (model.LeaveApplication.strSubmittedApplicationType == LMS.Util.LeaveDurationType.Hourly)
                        { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime; }
                        else
                        { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration; }
                    }
                    model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - ((objLvLedger.fltSubmitted - fltSubVal) + fltDays);
                    model.LeaveApplication.fltNetBalance = Math.Round(model.LeaveApplication.fltNetBalance, 2);
                }
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

            model.strHalfDayFromTime = model.LeaveApplication.strApplyFromTime;
            model.strHalfDayToTime = model.LeaveApplication.strApplyToTime;

            model.ActiveLeaveYear = model.GetLeaveYear(model.LeaveApplication.intLeaveYearID);
            model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;


            model.LstLeaveLedger = model.GetLeaveLedgerHistory(model.LeaveApplication);

            model.intNodeID = model.ApprovalFlow.intNodeID;

            model.GetApprovalFlowComments(-1, model.LeaveApplication.intApplicationID, -1);

            ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
            try
            {
                model.intNextNodeID = objApprovalPathBLL.ApprovalPathDetailsGet(-1, model.intNodeID, -1, LoginInfo.Current.strCompanyID)[0].intNodeID;
            }
            catch (Exception ex)
            {

            }

            if (!model.ApprovalFlow.bitIsNewArrival)
            {
                model.ShowPreview = true;
            }

            return View(model);

        }

        //GET: /ApprovalFlow/Details
        // Model.intNextNodeID For Next Recommender OR Approver 
        // m.ApprovalFlow.strDestAuthorID for Recommender or Approver ID
        // m.ApprovalFlow.strComments for Recommender or Approver Comments
        // Model.LstApprovalComments Will Show the Recommender or Approver Comments History
        // Model.Approver is the List of Recommender or Approver
        // To Set the ApprovalFlow Class 
        /* 
         * intApplicationID 
         * intAppStatusID
         * intNodeID
         * strAuthorID
         * intAppFlowID
         * */
        [NoCache]
        public ActionResult Details(int? FID)
        {
            // return View();

            int _FID = 0;

            if (FID.HasValue)
                _FID = (int)FID;

            ApprovalFlowModels model = new ApprovalFlowModels();
            //if request come through url
            model.IsFromUrl = false;

            // Added For Central Flow
            ApprovalFlow approvalFlow = new ApprovalFlow();
            approvalFlow.intApplicationID = _FID;
            approvalFlow.intNodeID = 1;

            model.ApprovalFlow = approvalFlow;

            // END
            // Block For Central Flow  model.ApprovalFlow = model.GetApprovalFlow(_FID, -1, -1, LoginInfo.Current.strEmpID, LoginInfo.Current.strCompanyID);

            model.LeaveApplication = model.GetLeaveApplication(model.ApprovalFlow.intApplicationID);

            //1= Approved 2= Canceled 3=Rejected
            if (model.LeaveApplication.intAppStatusID == 1 || model.LeaveApplication.intAppStatusID == 2 || model.LeaveApplication.intAppStatusID == 3)
            {
                model.ShowPreview = true;
            }
            else
            {
                model.ShowPreview = false;
                LeaveLedger objLvLedger = new LeaveLedger();
                double fltDays = 0;
                double fltSubVal = 0;
                objLvLedger = model.GetLeaveStatus(model.LeaveApplication).Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
                if (objLvLedger != null)
                {
                    if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                    {
                        fltDays = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                        if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                        { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration; }
                        else
                        { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime; }
                    }
                    else
                    {
                        fltDays = model.LeaveApplication.fltWithPayDuration;
                        if (model.LeaveApplication.strSubmittedApplicationType == LMS.Util.LeaveDurationType.Hourly)
                        { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime; }
                        else
                        { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration; }
                    }
                    model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - ((objLvLedger.fltSubmitted - fltSubVal) + fltDays);
                    model.LeaveApplication.fltNetBalance = Math.Round(model.LeaveApplication.fltNetBalance, 2);
                }
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


            model.strHalfDayFromTime = model.LeaveApplication.strApplyFromTime;
            model.strHalfDayToTime = model.LeaveApplication.strApplyToTime;

            model.ActiveLeaveYear = model.GetLeaveYear(model.LeaveApplication.intLeaveYearID);
            model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.LstLeaveLedger = model.GetLeaveLedgerHistory(model.LeaveApplication);
            model.intNodeID = model.ApprovalFlow.intNodeID;
            model.GetApprovalFlowComments(-1, model.LeaveApplication.intApplicationID, -1);

            ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
            ApprovalAuthorBLL objApprovalAuthorBLL = new ApprovalAuthorBLL();

            try
            {
                // model.intNextNodeID = objApprovalPathBLL.ApprovalPathDetailsGet(-1, model.intNodeID, -1, LoginInfo.Current.strCompanyID)[0].intNodeID;
                model.intNextNodeID = objApprovalAuthorBLL.ApprovalAuthorGet(-1, Convert.ToInt32(model.LeaveApplication.intApplicationID), "", "", LoginInfo.Current.strCompanyID)[0].intNodeID;
            }
            catch (Exception ex)
            {

            }

            /* Block For Temporary 
            if (!model.ApprovalFlow.bitIsNewArrival)
            {
                model.ShowPreview = true;
            }
            */
            return View(model);

        }

        //POST: /ApprovalFlow/SaveApprovalFlow
        [HttpPost]
        [NoCache]
        public ActionResult SaveApprovalFlow(ApprovalFlowModels model, int Id)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                model.ApprovalFlow.intAppStatusID = Id;
                i = model.SaveData(model, ref strmsg);

                if (i > 0 && model.ApprovalFlow.intAppStatusID == 1 && model.LeaveApplication.isServiceLifeType == true && model.LeaveApplication.dtApplyDateFrom <= System.DateTime.Now)
                {
                    Common.UpdateEmployeeStatus(model.LeaveApplication.strEmpID);
                }

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = strmsg.ToString();
                }
                else
                {
                    if (model.ApprovalFlow.intAppStatusID == (int)Common.ApplicationStatus.Approved)
                    {
                        #region Apply Individual Process
                        string strmessage = "";
                        LeaveEntitlementModels lvModel = new LeaveEntitlementModels();
                        lvModel.LeaveEntitlement.intLeaveYearID = model.LeaveApplication.intLeaveYearID;
                        lvModel.LeaveEntitlement.IsIndividual = true;
                        lvModel.LeaveEntitlement.strEmpID = model.LeaveApplication.strEmpID;

                        lvModel.EntitlementProcess(lvModel, out strmessage);

                        if (strmessage.ToString() == "Successful")
                        {
                            lvModel.Message = Util.Messages.GetSuccessMessage("Processed Successfully");
                        }
                        else
                        {
                            lvModel.Message = Util.Messages.GetErroMessage(strmessage.ToString());
                        }
                        #endregion

                        model.Message = Util.Messages.GetSuccessMessage("Application approved successfully");
                    }
                    else if (model.ApprovalFlow.intAppStatusID == (int)Common.ApplicationStatus.Rejected)
                    { 
                        model.Message = Util.Messages.GetSuccessMessage("Application rejected successfully");
                    }
                    else
                    { 
                        model.Message = Util.Messages.GetSuccessMessage("Application recommended successfully"); 
                    }

                    model.ShowPreview = true;
                    PopulateModel(model);
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetSuccessMessage("Couldn't Saved Data.");
            }

            if (model.ShowPreview == true)
            {
                return PartialView(LMS.Util.PartialViewName.ApprovalFlowDetailsPreview, model);
            }
            else
            {
                return PartialView(LMS.Util.PartialViewName.ApprovalFlowDetails, model);
            }
        }


        //GET: /ApprovalFlow/ApprovalFlowDetails
        //[HttpGet]
        //[NoCache]
        //public ActionResult ApprovalFlowDetails()
        //{
        //    ApprovalFlowModels model = GetModelWithData();
        //    return PartialView(LMS.Util.PartialViewName.ApprovalFlowDetails, model);
        //}


        //GET: /ApprovalFlow/Create
        [NoCache]
        public ActionResult Create()
        {
            return View();
        }


        //POST: /ApprovalFlow/Create
        [AcceptVerbs(HttpVerbs.Post)]
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


        //GET: /ApprovalFlow/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }


        //POST: /ApprovalFlow/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
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


        //GET: /ApprovalFlow/ValidateLeaveApplication
        [NoCache]
        public JsonResult ValidateLeaveApplication(ApprovalFlowModels model)
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

        //GET: /ApprovalFlow/CalculateDuration
        [NoCache]
        public JsonResult CalculateDuration(ApprovalFlowModels model)
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
                    dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
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
                        {
                        }
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
                        {
                        }
                    }
                }

                fltDuration = objBLL.GetDuration(model.LeaveApplication.strEmpID, model.LeaveApplication.intLeaveYearID, model.LeaveApplication.intLeaveTypeID, model.LeaveApplication.strApplicationType, dtFromDateTime, dtToDateTime, LoginInfo.Current.strCompanyID);

                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                {
                    if (fltDuration > 0)
                    {
                        TimeSpan ts = dtToDateTime.Subtract(dtFromDateTime);
                        fltDuration = Math.Round(ts.TotalHours, 2);

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

        //GET: /ApprovalFlow/CalculateNetBalance
        [NoCache]
        public JsonResult CalculateNetBalance(ApprovalFlowModels model)
        {
            double fltNetBalance = 0;
            try
            {
                LeaveLedger objLvLedger = new LeaveLedger();
                double fltDays = 0;
                double fltSubVal = 0;
                objLvLedger = model.GetLeaveStatus(model.LeaveApplication).Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
                if (objLvLedger != null)
                {
                    if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                    {
                        fltDays = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                        if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                        {
                            fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration;
                        }
                        else
                        {
                            fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;
                        }
                    }
                    else
                    {
                        fltDays = model.LeaveApplication.fltWithPayDuration;
                        if (model.LeaveApplication.strSubmittedApplicationType == LMS.Util.LeaveDurationType.Hourly)
                        {
                            fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;
                        }
                        else
                        {
                            fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration;
                        }
                    }

                    model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - ((objLvLedger.fltSubmitted - fltSubVal) + fltDays);

                    fltNetBalance = Math.Round(model.LeaveApplication.fltNetBalance, 2);
                }

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }

            return Json(fltNetBalance);
        }


        //GET: /ApprovalFlow/GetWorkingTimeInfo
        [NoCache]
        public JsonResult GetWorkingTimeInfo(ApprovalFlowModels model)
        {
            ArrayList list = new ArrayList();
            OfficeTimeDetails objOffTime = new OfficeTimeDetails();
            LeaveApplicationModels modelLA = new LeaveApplicationModels();
            try
            {
                if (model.LeaveApplication.intDurationID > 0)
                {
                    objOffTime = modelLA.GetWorkingTime().Where(c => c.intDurationID == model.LeaveApplication.intDurationID).FirstOrDefault();
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

        [NoCache]
        private void SetLoginInfo(string strEmpId)
        {
            string strCompanyId = "";

            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
            LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(strEmpId, strCompanyId, MyAppSession.LoggedInZoneId);

            if (loginDetails != null)
            {
                LoginInfo Current = new LoginInfo();
                try
                {
                    Current.strCompanyID = loginDetails.strCompanyID;
                    Current.strEmpID = loginDetails.strEmpID;

                    Current.EmployeeName = loginDetails.strEmpName;
                    Current.Archive = new System.Collections.Generic.List<string>();

                    User user = UserMgtAgent.GetUserByLoginId(loginDetails.strEmpID);
                    try
                    {
                        Current.UserId = user.UserId;// Convert.ToInt32(loginDetails.strEmpID);

                        // For BEPZA
                        Current.LoggedZoneId = user.ZoneId;
                        Current.ZoneName = loginDetails.LoggedInZoneName;
                    }
                    catch (Exception) { }


                    Current.strDesignationID = loginDetails.strDesignationID;
                    Current.strDesignation = loginDetails.strDesignation;
                    Current.strDepartmentID = loginDetails.strDepartmentID;
                    Current.fltOfficeTime = (float)loginDetails.fltDuration;
                    Current.intLeaveYearID = loginDetails.intLeaveYearID;
                    Current.EmailAddress = loginDetails.strEmail;
                    Current.strSupervisorId = loginDetails.strSupervisorID;

                    Current.intDestNodeID = loginDetails.intNodeID;
                    Current.strAllowHourlyLeave = loginDetails.strAllowHourlyLeave;
                    Current.StartOfficeTime = loginDetails.StartOfficeTime;
                    Current.EndOfficeTime = loginDetails.EndOfficeTime;


                }
                catch (Exception ex)
                {

                }
                Session["LoginInfo"] = Current;

            }
        }

        [NoCache]
        private void PopulateModel(ApprovalFlowModels model)
        {
            /* Block for Central Approval Flow
            model.ApprovalFlow = model.GetApprovalFlow(model.ApprovalFlow.intAppFlowID, -1, -1,
                                                       LoginInfo.Current.strEmpID,
                                                       LoginInfo.Current.strCompanyID);
            */

            model.LeaveApplication = model.GetLeaveApplication(model.ApprovalFlow.intApplicationID);
            model.LstLeaveLedger = model.GetLeaveLedgerHistory(model.LeaveApplication);

            model.intNodeID = model.ApprovalFlow.intNodeID;
            model.GetApprovalFlowComments(-1, model.LeaveApplication.intApplicationID, -1);
        }

        [NoCache]
        private ApprovalFlowModels GetModelWithData()
        {
            ApprovalFlowModels model = new ApprovalFlowModels();

            model.LeaveApplication = model.GetLeaveApplication(60);
            model.ApprovalFlow = model.GetApprovalFlow(42, 60, -1, LoginInfo.Current.strEmpID, LoginInfo.Current.strCompanyID);

            model.LstLeaveLedger = model.GetLeaveLedgerHistory(model.LeaveApplication);
            return model;
        }

        [NoCache]
        public JsonResult GetEmployeeInfo(ApprovalFlowModels model)
        {
            ArrayList list = new ArrayList();
            LMSEntity.Employee objEmp = new LMSEntity.Employee();
            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();

            LeaveApplicationModels tempModel = new LeaveApplicationModels();
            try
            {

                if (!string.IsNullOrEmpty(model.StrEmpSearch))
                {
                    objEmp = tempModel.GetEmployeeInfo(model.StrEmpSearch); //model.GetEmployeeInfoById(model.StrEmpSearch); //
                }

                if (objEmp == null)
                {
                    objEmp = new LMSEntity.Employee();
                }

                list.Add(objEmp.strDesignationID);
                list.Add(objEmp.strDesignation);
                list.Add(objEmp.strDepartmentID);
                list.Add(objEmp.strDepartment);
                list.Add(objEmp.strEmpInitial);
                list.Add(objEmp.strEmpName);
                list.Add(objEmp.EmployeeId);

            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }
    }
}
