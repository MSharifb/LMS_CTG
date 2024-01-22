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

namespace LMS.Web.Controllers
{
    [NoCache]
    public class OOAApprovalFlowController : Controller
    {
        //GET: /ApprovalFlow/
        [NoCache]
        public ActionResult Index(int? FID)
        {
            // return View();

            int _FID = 0;

            if (FID.HasValue)
                _FID = (int)FID;
            ApprovalFlowModels model = new ApprovalFlowModels();
            //if request come through url
            model.IsFromUrl = true;
            model.ApprovalFlow = model.GetApprovalFlow(_FID, -1, -1, "", "");


            if (Request["fromMail"] != null && Request["fromMail"].ToString().ToLower() == "true")
            {SetLoginInfo(model.ApprovalFlow.strAuthorID);}

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
                            {fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration;}
                        else
                            {fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;}
                    }
                    else
                    {
                        fltDays = model.LeaveApplication.fltWithPayDuration;
                        if (model.LeaveApplication.strSubmittedApplicationType == LMS.Util.LeaveDurationType.Hourly)
                            {fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;}
                        else
                            {fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration;}
                    }
                    model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - ((objLvLedger.fltSubmitted - fltSubVal) + fltDays);
                    model.LeaveApplication.fltNetBalance = Math.Round(model.LeaveApplication.fltNetBalance, 2);
                }
            }


            /**/
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

            /**/

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


            model.ApprovalFlow = model.GetApprovalFlow(_FID, -1, -1, LoginInfo.Current.strEmpID, LoginInfo.Current.strCompanyID);

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
                            {fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration;}
                        else
                            {fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;}
                    }
                    else
                    {
                        fltDays = model.LeaveApplication.fltWithPayDuration;
                        if (model.LeaveApplication.strSubmittedApplicationType == LMS.Util.LeaveDurationType.Hourly)
                            {fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;}
                        else
                            {fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration;}
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

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = strmsg.ToString();
                }
                else
                {
                    if (model.ApprovalFlow.intAppStatusID == (int)Common.ApplicationStatus.Approved)
                        {model.Message = Util.Messages.GetSuccessMessage("Application approved successfully");}
                    else if (model.ApprovalFlow.intAppStatusID == (int)Common.ApplicationStatus.Rejected)
                        {model.Message = Util.Messages.GetSuccessMessage("Application rejected successfully");}
                    else
                        {model.Message = Util.Messages.GetSuccessMessage("Application recommended successfully");}

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
        [HttpGet]
        [NoCache]
        public ActionResult ApprovalFlowDetails()
        {
            ApprovalFlowModels model = GetModelWithData();
            return PartialView(LMS.Util.PartialViewName.ApprovalFlowDetails, model);
        }
        
       
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
                    char[] sepAr = { '.', ' ' };

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
                        fltDuration = ts.TotalHours;
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
                    Current.UserId = user.UserId;// Convert.ToInt32(loginDetails.strEmpID);
                    Current.LoggedZoneId = user.ZoneId;
                    Current.ZoneName = loginDetails.LoggedInZoneName;

                    Current.strDesignationID = loginDetails.strDesignationID;
                    Current.strDesignation = loginDetails.strDesignation;
                    Current.strDepartmentID = loginDetails.strDepartmentID;
                    Current.fltOfficeTime = (float)loginDetails.fltDuration;
                    Current.intLeaveYearID = loginDetails.intLeaveYearID;
                    Current.EmailAddress = loginDetails.strEmail;
                    Current.strSupervisorId = loginDetails.strSupervisorID;

                    Current.intDestNodeID = loginDetails.intNodeID;

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
            model.ApprovalFlow = model.GetApprovalFlow(model.ApprovalFlow.intAppFlowID, -1, -1,
                                                       LoginInfo.Current.strEmpID,
                                                       LoginInfo.Current.strCompanyID);

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


    }
}
