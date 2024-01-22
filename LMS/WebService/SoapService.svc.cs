using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LMSEntity;
using LMS.Web.Models;
using LMS.BLL;
using System.Collections;
using System.Globalization;
using System.Web.Mvc;
using LMS.UserMgtService;

namespace LMS.Web.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SoapService" in code, svc and config file together.
    public class SoapService : ISoapService
    {
        public string DoLogin(string userName, string password)
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                msg = "The login Id or password can't be empty.";
            }

            CustomMembershipProvider member = new CustomMembershipProvider();
            bool result = member.ValidateUser(userName, password);

            if (!result)
            {
                msg = "Provided login Id or password is incorrect.";
            }
            else
            {
                User user = UserMgtAgent.GetUserByLoginId(userName);

                LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
                string strCompanyId = "";
                LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(user.EmpId, strCompanyId);

                msg = user.UserId.ToString() + "|" + user.LoginId + "|" + user.EmpId + "|" + loginDetails.strEmpName + "|" + loginDetails.intLeaveYearID.ToString() + "|" + loginDetails.fltDuration.ToString() + "|" + loginDetails.strDesignation + "|" + loginDetails.strDepartment;
                //msg = user.UserId.ToString() + "|" + user.LoginId + "|" + user.EmpId + "|" + loginDetails.strEmpName + "|" + loginDetails.intLeaveYearID.ToString() + "|" + loginDetails.fltDuration.ToString();
            }

            return msg;
        }

        public IList<MLeave> GetAppliedApplicationList(string empId, int leaveYearID)
        {
            LeaveApplication objSearch = new LeaveApplication();
            objSearch.strAuthorID = empId;
            LeaveApplicationModels model = new LeaveApplicationModels();
            objSearch.intLeaveYearID = model.intSearchLeaveYearId;


            int intAppFlowID = 0;
            string strEmpID = null;
            String strEmpName = null;
            int intLeaveYearID = leaveYearID;// 190;
            int intLeaveTypeID = 0;
            String strApplyFromDate = "";
            String strApplyToDate = "";
            String strApplicationType = null;
            int intAppStatusID = 0;
            bool bitIsDiscard = false;
            String strCompanyID = "1";
            String strDepartmentID = null;
            String strDesignationID = null;
            String strAuthorID = empId;// "00338";
            String strAppDirectionType = "Forward";
            String strSortBy = "dtApplyDate";
            String strSortType = "DESC";
            int _startRowIndex = 1;
            int maximumRows = 20;
            int Total = 0;

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            List<LeaveApplication> lstApp = objBll.RequestedLeaveApplicationGet(
                intAppFlowID,
                strEmpID,
                strEmpName,
                intLeaveYearID,
                intLeaveTypeID,
                strApplyFromDate,
                strApplyToDate,
                strApplicationType,
                intAppStatusID,
                bitIsDiscard,
                strCompanyID,
                strDepartmentID,
                strDesignationID,
                strAuthorID,
                strAppDirectionType,
                strSortBy,
                strSortType,
                _startRowIndex,
                maximumRows,
                out Total
                );
            // IList<LeaveApplication> lstApp = model.GetLeaveApplicationPaging(objSearch, true, model.IsSearch, model.IsBulkApprove);
            List<MLeave> lstMApp = new List<MLeave>();
            foreach (LeaveApplication app in lstApp)
            {
                MLeave obj = new MLeave();

                FromLeaveAppToMLeave(app, obj);

                lstMApp.Add(obj);
            }

            return lstMApp;
        }

        private static void FromLeaveAppToMLeave(LeaveApplication app, MLeave obj)
        {
            obj.strEmpID = app.strEmpID;
            obj.strEmpName = app.strEmpName;
            obj.intAppFlowID = app.intAppFlowID;
            obj.intApplicationID = (int)app.intApplicationID;
            obj.intAppStatusID = app.intAppStatusID;
            obj.strApplicationType = app.strApplicationType;
            obj.strDepartment = app.strDepartment;
            obj.strDesignation = app.strDesignation;
            obj.strLeaveType = app.strLeaveType;
            obj.strApplyDate = app.strApplyDate;
            obj.strApplyFromDate = app.strApplyFromDate;
            obj.strApplyToDate = app.strApplyToDate;
            obj.strApplyFromTime = app.strApplyFromTime;
            obj.strApplyToTime = app.strApplyToTime;

            obj.fltDuration = app.fltDuration;
            obj.fltWithPayDuration = app.fltWithPayDuration;
            obj.fltWithoutPayDuration = app.fltWithoutPayDuration;

            obj.strPurpose = app.strPurpose;
            obj.strContactAddress = app.strContactAddress;
            obj.strContactNo = app.strContactNo;
        }
        private static void FromAppFlowToMLeave(ApprovalFlow app, MLeave obj)
        {
            obj.intNodeID = app.intNodeID;
            obj.strComments = app.strComments;
        }
        public MLeave GetApplicationDetail(int intAppFlowID)
        {
            ApprovalFlowBLL objBLLAF = new ApprovalFlowBLL();
            ApprovalFlow objApprovalFlow = objBLLAF.ApprovalFlowGet(intAppFlowID);

            LeaveApplicationBLL objBLL = new LeaveApplicationBLL();
            LeaveApplication app = objBLL.LeaveApplicationGet(objApprovalFlow.intApplicationID);

            MLeave mLeave = new MLeave();

            FromLeaveAppToMLeave(app, mLeave);
            FromAppFlowToMLeave(objApprovalFlow, mLeave);
            ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
            try
            {
                mLeave.intNextNodeID = objApprovalPathBLL.ApprovalPathDetailsGet(-1, mLeave.intNodeID, -1, "1")[0].intNodeID;
            }
            catch (Exception ex)
            {

            }

            return mLeave;
        }

        public string SaveApprovalFlow(int intAppFlowID, int intAppStatusID, string LoginId, string EmpId, string AuthorComments)
        {
            int i = 0;
            string strmsg = "";
            ApprovalFlowModels model = new ApprovalFlowModels();

            ApprovalFlowBLL objBLLAF = new ApprovalFlowBLL();
            ApprovalFlow objApprovalFlow = objBLLAF.ApprovalFlowGet(intAppFlowID);
            model.ApprovalFlow = objApprovalFlow;
            if (objApprovalFlow != null)
            {
                model.intNodeID = objApprovalFlow.intNodeID;
            }
            LeaveApplicationBLL objBLL = new LeaveApplicationBLL();
            LeaveApplication app = objBLL.LeaveApplicationGet(objApprovalFlow.intApplicationID);
            model.LeaveApplication = app;


            try
            {
                model.ApprovalFlow.intAppStatusID = intAppStatusID;
                model.LeaveApplication.intAppStatusID = intAppStatusID;
                model.ApprovalFlow.strDestAuthorID = Approver(model.ApprovalFlow.intNodeID);
                model.ApprovalFlow.strComments = AuthorComments;

                i = SaveData(model, ref strmsg, LoginId, EmpId);

                if (strmsg.ToString().Length > 0)
                {

                }
                else
                {
                    if (model.ApprovalFlow.intAppStatusID == (int)Common.ApplicationStatus.Approved)
                    {
                        strmsg = "Application approved successfully";
                    }
                    else if (model.ApprovalFlow.intAppStatusID == (int)Common.ApplicationStatus.Rejected)
                    {
                        strmsg = "Application rejected successfully";
                    }
                    else
                    {
                        strmsg = "Application recommended successfully";
                    }



                }
            }
            catch (Exception ex)
            {
                strmsg = "Couldn't Saved Data.";
            }

            return strmsg;
        }

        private int SaveData(ApprovalFlowModels model, ref string strmsg, string LoginName, string EmpId)
        {
            int intNextNodeID = 0;
            int i = -1;
            string strCompanyID = "1";

            ApprovalFlowBLL objBll = new ApprovalFlowBLL();
            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
            LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(EmpId, strCompanyID);
            try
            {
                // TODO: Add insert logic here

                model.ApprovalFlow.strEUser = LoginName;
                model.ApprovalFlow.strIUser = LoginName;
                model.ApprovalFlow.strCompanyID = "1";

                model.ApprovalFlow.strDepartmentID = loginDetails.strDepartmentID;
                model.ApprovalFlow.strDesignationID = loginDetails.strDesignationID;

                model.ApprovalFlow.strApplicationType = model.LeaveApplication.strApplicationType;
                model.ApprovalFlow.dtApplyFromDate = model.LeaveApplication.dtApplyFromDate;
                model.ApprovalFlow.dtApplyToDate = model.LeaveApplication.dtApplyToDate;

                model.ApprovalFlow.strApplyFromTime = model.LeaveApplication.intDurationID > 0 ? model.strHalfDayFromTime : model.LeaveApplication.strApplyFromTime;
                model.ApprovalFlow.strApplyToTime = model.LeaveApplication.intDurationID > 0 ? model.strHalfDayToTime : model.LeaveApplication.strApplyToTime;

                model.ApprovalFlow.strHalfDayFor = model.LeaveApplication.strHalfDayFor;
                model.ApprovalFlow.intDurationID = model.LeaveApplication.intDurationID;

                //if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                //{
                //    model.ApprovalFlow.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                //    model.ApprovalFlow.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                //    model.ApprovalFlow.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                //}
                //else
                //{
                //    model.ApprovalFlow.fltDuration = model.LeaveApplication.fltDuration;
                //    model.ApprovalFlow.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration;
                //    model.ApprovalFlow.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration;
                //}

                model.ApprovalFlow.fltDuration = model.LeaveApplication.fltDuration;
                model.ApprovalFlow.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration;
                model.ApprovalFlow.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration;

                model.ApprovalFlow.intSourceNodeID = model.ApprovalFlow.intNodeID;
                model.ApprovalFlow.strSourceAuthorID = model.ApprovalFlow.strAuthorID;
                ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
                try
                {
                    model.intNextNodeID = intNextNodeID = objApprovalPathBLL.ApprovalPathDetailsGet(-1, model.intNodeID, -1, strCompanyID)[0].intNodeID;
                }
                catch (Exception ex)
                {
                }

                if (intNextNodeID == 0)
                {
                    model.ApprovalFlow.strDestAuthorID = "";  //For approval
                }

                model.ApprovalFlow.intDestNodeID = model.intNextNodeID;

                try
                {
                    //Check the submittion is for approval or recommander
                    if (objApprovalPathBLL.ApprovalPathDetailsGet(-1, intNextNodeID, -1, strCompanyID).Count <= 0)
                    {
                        model.LeaveApplication.IsForApproval = true;
                    }
                }
                catch (Exception ex)
                {
                    model.LeaveApplication.IsForApproval = false;
                }

                i = objBll.Add(model.ApprovalFlow, ref strmsg);

                if (intNextNodeID == 0)
                {
                    i = model.ApprovalFlow.intAppFlowID;
                }
                try
                {

                    if (loginDetails.strEmail.ToString().Length > 0)
                    {
                        model.LeaveApplication.strSupervisorID = model.ApprovalFlow.strDestAuthorID;
                        model.LeaveApplication.intAppStatusID = model.ApprovalFlow.intAppStatusID;
                        model.LeaveApplication.strCompanyID = model.ApprovalFlow.strCompanyID;
                        if (model.LeaveApplication.intDurationID > 0)
                        {
                            model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                            model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                        }

                        if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                        {
                            model.LeaveApplication.fltDuration /= (float)loginDetails.fltDuration;
                            model.LeaveApplication.fltWithPayDuration /= (float)loginDetails.fltDuration;
                            model.LeaveApplication.fltWithoutPayDuration /= (float)loginDetails.fltDuration;

                            model.LeaveApplication.fltSubmittedDuration /= (float)loginDetails.fltDuration;
                            model.LeaveApplication.fltSubmittedWithPayDuration /= (float)loginDetails.fltDuration;
                            model.LeaveApplication.fltSubmittedWithoutPayDuration /= (float)loginDetails.fltDuration;
                        }

                        Common.SendMail(model.LeaveApplication, i, (float)loginDetails.fltDuration, loginDetails.strEmail, loginDetails.strEmpName);
                    }

                    //SendMail(model.ApprovalFlow, model.LeaveApplication, i);
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
        private string Approver(int intNodeID)
        {
            int intNextNodeID = 0;
            string _Approver = string.Empty;
            string strCompanyID = "1";
            List<ApprovalAuthor> lstApprovalAuthor = new List<ApprovalAuthor>();
            ApprovalAuthorBLL objApprovalAuthorBLL = new ApprovalAuthorBLL();
            ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();

            try
            {
                try
                {
                    intNextNodeID = objApprovalPathBLL.ApprovalPathDetailsGet(-1, intNodeID, -1, strCompanyID)[0].intNodeID;
                }
                catch (Exception ex)
                {
                }

                if (intNextNodeID == 0)
                {
                    return "";  //For approval
                }

                lstApprovalAuthor = objApprovalAuthorBLL.ApprovalAuthorGet(-1, intNextNodeID, "", "", strCompanyID);
                lstApprovalAuthor.OrderBy(c => c.strAuthorType);
            }
            catch (Exception ex)
            {
            }
            try
            {
                _Approver = lstApprovalAuthor.FirstOrDefault().strAuthorID;
            }
            catch (Exception ex)
            {
            }
            return _Approver;

        }
        public class LeaveTypeCls
        {
            public int intLeaveTypeID;
            public string strLeaveType;
            public double fltCB;
            public double fltSubmitted;
        };
        public List<LeaveTypeCls> GetOnLineLeaveType(string strEmpID, int intLeaveYearID)
        {
            string strCompanyID = "1";
            //Dictionary<int, string> itemList = new Dictionary<int, string>();
            List<LeaveTypeCls> itemList = new List<LeaveTypeCls>();

            List<LeaveType> lstLeaveType = new List<LeaveType>();
            List<LeaveLedger> lstLeaveLedger = new List<LeaveLedger>();
            LeaveApplicationBLL objBLL = new LeaveApplicationBLL();
            lstLeaveLedger = objBLL.LeaveBalanceIndividualGet(0, intLeaveYearID, 0, 0.0, strEmpID, strCompanyID, "FullDay");


            foreach (LeaveLedger lt in lstLeaveLedger)
            {
                LeaveTypeCls obj = new LeaveTypeCls();
                obj.intLeaveTypeID = lt.intLeaveTypeID;
                obj.strLeaveType = lt.strLeaveType;
                obj.fltCB = lt.fltCB;
                obj.fltSubmitted = lt.fltSubmitted;
                itemList.Add(obj);
            }

            return itemList.OrderBy(c => c.strLeaveType).ToList();
        }
        public class ApproverCls
        {
            public string strEmpName;
            public string strAuthorID;
        }
        public List<ApproverCls> GetApproverList(string strEmpID)
        {
            string strCompanyID = "1";
            List<ApproverCls> itemList = new List<ApproverCls>();
            List<ApprovalAuthor> lstApprovalAuthor = new List<ApprovalAuthor>();

            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
            ApprovalAuthorBLL objApprovalAuthorBLL = new ApprovalAuthorBLL();
            LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(strEmpID, strCompanyID);

            lstApprovalAuthor = objApprovalAuthorBLL.ApprovalAuthorGet(-1, loginDetails.intNodeID, "", "", strCompanyID);

            lstApprovalAuthor.OrderBy(c => c.strAuthorType);

            foreach (ApprovalAuthor lt in lstApprovalAuthor)
            {
                ApproverCls item = new ApproverCls();
                item.strAuthorID = lt.strAuthorID;
                item.strEmpName = lt.strEmpName;
                itemList.Add(item);
            }

            return itemList;
        }

        public class CalcutateDurationCls
        {
            public double fltDuration = 0, dblNetBL = 0, dblWithPay = 0, dblWithoutPay = 0;
        };
        public CalcutateDurationCls CalcutateDuration(
            string strApplyFromDate, string strApplyToDate, string strApplyFromTime, string strApplyToTime,
            string strApplicationType, string strEmpID,
            int intLeaveYearID, int intLeaveTypeID, string strOfficeTime)
        {
            ArrayList list = new ArrayList();
            double fltDuration = 0, dblNetBL = 0, dblHours = 0, dblWithPay = 0, dblWithoutPay = 0;
            CalcutateDurationCls duration = new CalcutateDurationCls();
            LeaveLedger objLvLedger = new LeaveLedger();
            BLL.LeaveApplicationBLL objBLL = new BLL.LeaveApplicationBLL();
            string strCompanyID = "1";
            DateTime dtApplyFromDate = DateTime.ParseExact(strApplyFromDate, "dd-MMM-yyyy", null);
            DateTime dtApplyToDate = DateTime.ParseExact(strApplyToDate, "dd-MMM-yyyy", null);

            float fltOfficeTime = float.Parse(strOfficeTime);
            try
            {
                DateTime dtFromDateTime = dtApplyFromDate;
                DateTime dtToDateTime = dtApplyToDate;

                if (strApplicationType != LMS.Util.LeaveDurationType.FullDay)
                {
                    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                    dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
                    dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;
                    char[] sepAr = { ':', ' ' };

                    if (!string.IsNullOrEmpty(strApplyFromTime))
                    {
                        string[] time = strApplyFromTime.Split(sepAr);

                        try
                        {
                            string strDt = dtApplyFromDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                            dtFromDateTime = Convert.ToDateTime(strDt, dtfi);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    if (!string.IsNullOrEmpty(strApplyToTime))
                    {
                        string[] time = strApplyToTime.Split(sepAr);


                        try
                        {
                            string strDt = dtApplyToDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                            dtToDateTime = Convert.ToDateTime(strDt, dtfi);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                fltDuration = objBLL.GetDuration(strEmpID, intLeaveYearID, intLeaveTypeID, strApplicationType, dtFromDateTime, dtToDateTime, strCompanyID);

                if (strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                {
                    if (fltDuration > 0)
                    {
                        TimeSpan ts1 = dtToDateTime.Subtract(dtFromDateTime);
                        fltDuration = Math.Round(ts1.TotalHours, 2);

                        if (fltDuration > (double)fltOfficeTime)
                        {
                            fltDuration = (double)fltOfficeTime;
                        }
                    }
                }
                List<LeaveLedger> LstLeaveLedger = objBLL.LeaveBalanceIndividualGet(0, intLeaveYearID, 0, 0.0, strEmpID, strCompanyID, "FullDay");

                if (LstLeaveLedger != null)
                {
                    foreach (LeaveLedger obj in LstLeaveLedger)
                    {
                        obj.fltCB = obj.fltCB - obj.fltSubmitted;
                    }
                }
                if (intLeaveTypeID > 0 && LstLeaveLedger != null)
                {
                    objLvLedger = LstLeaveLedger.Where(c => c.intLeaveTypeID == intLeaveTypeID).SingleOrDefault();
                    if (objLvLedger != null)
                    {
                        if (strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                        {
                            dblHours = fltDuration / fltOfficeTime;
                        }
                        else
                        {
                            dblHours = fltDuration;
                        }


                        if (objLvLedger.fltCB >= dblHours)
                        {
                            dblWithPay = strApplicationType != LMS.Util.LeaveDurationType.Hourly ? dblHours : dblHours * fltOfficeTime;
                            dblWithoutPay = 0;
                            dblNetBL = Math.Round(objLvLedger.fltCB - dblHours, 2);
                        }
                        else
                        {
                            dblWithPay = objLvLedger.fltCB;
                            dblWithoutPay = strApplicationType != LMS.Util.LeaveDurationType.Hourly ? Math.Round(dblHours - objLvLedger.fltCB, 2) : Math.Round(dblHours - objLvLedger.fltCB, 2) * fltOfficeTime;
                            dblNetBL = 0;
                        }

                    }
                }

                list.Add(fltDuration);
                list.Add(dblWithPay);
                list.Add(dblWithoutPay);
                list.Add(dblNetBL);



                duration.fltDuration = fltDuration;
                duration.dblWithPay = dblWithPay;
                duration.dblWithoutPay = dblWithoutPay;
                duration.dblNetBL = dblNetBL;

            }
            catch (Exception ex)
            {
                // model.Message = Messages.GetErroMessage(ex.Message);
            }

            //return Json(list);
            return duration;
        }
        public string OnlineSubmit(
            string LoginName,
            string strEmpID,
            int intLeaveTypeID,
            string strApplyDate,
            string strApplyFromDate,
            string strApplyToDate,
            string strApplyFromTime,
            string strApplyToTime,
            string strApplicationType,
            string strDuration,
            string strWithPayDuration,
            string strtWithoutPayDuration,
            string strPurpose,
            string strContactAddress,
            string strRemarks,
            string strSupervisorID
            )
        {
            int i = -1;

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            string strmsg = string.Empty;

            string strCompanyID = "1";
            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
            LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(strEmpID, strCompanyID);

            LeaveApplicationModels model = InitializeModel(loginDetails);

            float fltOfficeTime = (float)loginDetails.fltDuration;

            try
            {
                DateTime dtApplyDate = DateTime.ParseExact(strApplyDate, "dd-MMM-yyyy", null);
                DateTime dtApplyFromDate = DateTime.ParseExact(strApplyFromDate, "dd-MMM-yyyy", null);
                DateTime dtApplyToDate = DateTime.ParseExact(strApplyToDate, "dd-MMM-yyyy", null);
                float fltDuration = float.Parse(strDuration);
                float fltWithPayDuration = float.Parse(strWithPayDuration);
                float fltWithoutPayDuration = float.Parse(strtWithoutPayDuration);
                string strContactNo = "";

                model.LeaveApplication.strEmpID = strEmpID;

                model.LeaveApplication.intLeaveTypeID = intLeaveTypeID;
                model.LeaveApplication.dtApplyDate = dtApplyDate;
                model.LeaveApplication.dtApplyFromDate = dtApplyFromDate;
                model.LeaveApplication.dtApplyToDate = dtApplyToDate;
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.strApplyFromTime = strApplyFromTime;
                    model.LeaveApplication.strApplyToTime = strApplyToTime;
                }

                model.LeaveApplication.strApplicationType = strApplicationType;
                model.LeaveApplication.fltDuration = fltDuration;
                model.LeaveApplication.fltWithPayDuration = fltWithPayDuration;
                model.LeaveApplication.fltWithoutPayDuration = fltWithoutPayDuration;
                model.LeaveApplication.strPurpose = strPurpose;
                model.LeaveApplication.strContactAddress = strContactAddress;
                model.LeaveApplication.strContactNo = strContactNo;
                model.LeaveApplication.strRemarks = strRemarks;

                model.LeaveApplication.strSupervisorID = strSupervisorID;

                model.LeaveApplication.strCompanyID = strCompanyID;


                model.LeaveApplication.bitIsApprovalProcess = true;



                model.LeaveApplication.strEUser = LoginName;
                model.LeaveApplication.strIUser = LoginName;
                model.LeaveApplication.strCompanyID = strCompanyID;


                if (model.IsLeaveTypeApplicable(model) == true)
                {

                    /*---[For FullDayHalfDay Leave]----------*/
                    if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay && model.LeaveApplication.intDurationID > 0)
                    {
                        model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                        model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                    }


                    /*---[For not Hourly Leave]----------*/
                    if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                    {
                        model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * fltOfficeTime;
                        model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * fltOfficeTime;
                        model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * fltOfficeTime;
                    }
                    // LeaveApplicationBLL objBLL = new LeaveApplicationBLL();
                    //model.LstLeaveLedger = objBLL.LeaveBalanceIndividualGet(0, intLeaveYearID, 0, 0.0, strEmpID, strCompanyID, "FullDay");

                    foreach (LeaveLedger obj in model.LstLeaveLedger)
                    {
                        if (obj.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID)
                        {
                            obj.fltApplied = model.LeaveApplication.fltWithPayDuration / fltOfficeTime;
                        }
                    }

                    List<ValidationMessage> messageList = LeaveApplicationValidationBLL.ValidateLeaveApplication(model.LeaveApplication);

                    if (messageList != null && messageList.Count > 0)
                    {
                        int counter = 1;
                        foreach (ValidationMessage obj in messageList)
                        {
                            strmsg = strmsg + (counter++).ToString() + ". " + obj.msg + "\n";
                        }
                        return strmsg;
                    }

                    /*---[add leave application to db--*/
                    i = objBll.Add(model.LeaveApplication, model.LstLeaveLedger, out strmsg);


                    /*---[if leave application added successfully--*/
                    if (i >= 0)
                    {
                        strmsg = "Application submitted successfully.";

                        ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                        ApprovalFlow appFlow = objApprovalFlowBLL.ApprovalFlowGet(-1, i, -1, "", strCompanyID).SingleOrDefault();
                        i = appFlow.intAppFlowID;

                        try
                        {
                            /*---[check the submittion is for approval or recommander--*/
                            ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
                            if (objApprovalPathBLL.ApprovalPathDetailsGet(-1, appFlow.intNodeID, -1, strCompanyID).Count <= 0)
                            { model.LeaveApplication.IsForApproval = true; }
                        }
                        catch (Exception ex)
                        { model.LeaveApplication.IsForApproval = false; }
                        try
                        {
                            if (loginDetails.strEmail.Length > 0)
                            {
                                Common.SendMail(model.LeaveApplication, i, fltOfficeTime, loginDetails.strEmail, loginDetails.strEmpName);
                            }
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                else
                {
                    strmsg = "Selected leave type is not applicable for the employee.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strmsg;
        }



        private LeaveApplicationModels InitializeModel(LMSEntity.LoginDetails loginDetails)
        {
            //string strEmpID, string EmployeeName, int intLeaveYearID, int intDestNodeID
            LeaveApplicationModels model = new LeaveApplicationModels();
            model.LeaveApplication.dtApplyDate = DateTime.Today;
            model.LeaveApplication.strApplyFromDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.strApplyToDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.bitIsApprovalProcess = true;
            model.LeaveApplication.bitIsDiscard = false;
            model.LeaveApplication.IsFullDay = true;
            model.LeaveApplication.strApplicationType = LMS.Util.LeaveDurationType.FullDay;

            model.LeaveApplication.intAppStatusID = 6;  //Submit
            model.LeaveApplication.strEmpID = loginDetails.strEmpID;
            model.LeaveApplication.strEmpName = loginDetails.strEmpName;

            LMSEntity.Employee objEmp = new LMSEntity.Employee();

            if (!string.IsNullOrEmpty(model.LeaveApplication.strEmpID))
            {
                objEmp = model.GetEmployeeInfo(model.LeaveApplication.strEmpID);

                model.LeaveApplication.strDepartmentID = objEmp.strDepartmentID;
                model.LeaveApplication.strDepartment = objEmp.strDepartment;

                model.LeaveApplication.strDesignationID = objEmp.strDesignationID;
                model.LeaveApplication.strDesignation = objEmp.strDesignation;
                model.LeaveApplication.strBranch = objEmp.strLocation;
            }

            model.LeaveApplication.intLeaveYearID = loginDetails.intLeaveYearID;
            model.LeaveApplication.strSupervisorID = "";

            model.intNodeID = loginDetails.intNodeID;
            model.intSearchLeaveYearId = loginDetails.intLeaveYearID;

            model.LeaveApplication.intDestNodeID = loginDetails.intNodeID;


            model.ActiveLeaveYear = model.GetLeaveYear(loginDetails.intLeaveYearID);
            model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            //model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);
            LeaveApplicationBLL objBLL = new LeaveApplicationBLL();
            model.LstLeaveLedger = objBLL.LeaveBalanceIndividualGet(0, loginDetails.intLeaveYearID, 0, 0.0, loginDetails.strEmpID, "1", "FullDay");

            return model;
        }
        public IList<rptLeaveEnjoyed> GetAvailedLeave(int intLeaveYearId, string strApplyFromDate, string strApplyToDate)
        {
            ReportsBLL objBLL = new ReportsBLL();

            int Total = 0;
            string strCompanyID = "1";
            DateTime dtApplyFromDate = DateTime.ParseExact(strApplyFromDate, "dd-MMM-yyyy", null);
            DateTime dtApplyToDate = DateTime.ParseExact(strApplyToDate, "dd-MMM-yyyy", null);

            ReportsModels model = new ReportsModels();
            model.IntLeaveYearId = intLeaveYearId;
            model.IsApplyDate = true;
            model.StrFromDate = dtApplyFromDate.ToString("dd-MM-yyyy");
            model.StrToDate = dtApplyToDate.ToString("dd-MM-yyyy");
            model.strSortBy = "strEmpID,strLeaveType,dtApplyFromDate";
            model.strSortType = LMS.Util.DataShortBy.ASC;

            // Added New Parameter Zone Id for Zone Wise Filtering 12-June-2017

            model.LstRptLeaveEnjoyed = objBLL.RptLeaveEnjoyedGetData(model.IntLeaveYearId, model.IsServiceLifeType, strCompanyID, model.StrEmpId, (model.EmpStatus - 1),
                                                                    model.StrDepartmentId, model.StrDesignationId, model.StrGender,
                                                                    model.StrLocationId, model.StrFromDate.ToString(), model.StrToDate.ToString(),
                                                                    model.IntCategoryId, model.IntLeaveTypeId, model.IsWithoutPay, model.IsApplyDate, LoginInfo.Current.LoggedZoneId,
                                                                    model.strSortBy, model.strSortType, model.startRowIndex, model.maximumRows, out Total);
            model.numTotalRows = Total;

            return model.LstRptLeaveEnjoyed;

        }


        public IList<AttendanceReport> GetAttendanceReport(string empId, string strFromDate, string strToDate)
        {
            AttendanceReportBLL objBLL = new AttendanceReportBLL();
            List<AttendanceReport> LstAttendanceReport;

            int Total = 0;
            string strReportType = "EAS";
            string StrCompanyID = "1";
            string StrDepartmentId = string.Empty; ;
            string StrDesignationId = string.Empty; ;
            string StrLocationId = string.Empty; ;
            int intCategoryCode = 0;
            string strSortBy = "strEmpID";
            string strSortType = "ASC";
            int startRowIndex = 1;
            int maximumRows = 20;

            LstAttendanceReport = objBLL.AttendanceReportGetData(strReportType, empId, StrCompanyID,
                                                                         StrDepartmentId, StrDesignationId,
                                                                         StrLocationId, intCategoryCode,
                                                                         Common.FormatDate(strFromDate, "dd-MMM-yyyy", "yyyy-MM-dd"),
                                                                         Common.FormatDate(strToDate, "dd-MMM-yyyy", "yyyy-MM-dd"),
                                                                         strSortBy, strSortType, startRowIndex,
                                                                         maximumRows, out Total);

            foreach (AttendanceReport obj in LstAttendanceReport)
            {
                try
                {
                    if (obj.strAttendanceStatus.Equals("absent", StringComparison.CurrentCultureIgnoreCase))
                    {
                        obj.intEarlyIN = "    ";
                        obj.intLateOUT = "    ";
                    }
                    else
                    {
                        obj.intEarlyIN = obj.dtFirstInTime.ToString("hh:mm tt");
                        obj.intLateOUT = obj.dtLastOutTime.ToString("hh:mm tt");
                    }
                    obj.strInTime = obj.dtInTime.ToString("hh:mm tt");
                    obj.strOutTime = obj.dtOutTime.ToString("hh:mm tt");

                }
                catch (Exception ex)
                {
                }
            }

            return LstAttendanceReport.OrderBy(c => c.dtAttendDateTime).Reverse().ToList(); ;
        }

        public IList<LMSEntity.Employee> GetEmployeeData()
        {

            BLL.EmployeeBLL objBLL = new EmployeeBLL();
            int numTotalRows1 = 0;
            string strEmpID = "";
            string strEmpName = "";
            string ActiveStatus = "";
            string strDepartmentID = "";
            string strDesignationID = "";
            string strDesignation = "";
            string strGender = "";
            string strReligionID = "";
            string strSearchType = "";
            string strCompanyID = "1";
            string strSortBy = "strEmpID";
            int startRowIndex = 0;
            int maximumRows = 100;
            string strSortType = "ASC";

            List<LMSEntity.Employee> Employees;


            Employees = objBLL.EmployeeGet("",strEmpID, strEmpName, ActiveStatus, strDepartmentID, strDesignationID, strDesignation, strGender, strReligionID
                , strCompanyID, strSearchType, strSortBy, strSortType, startRowIndex, maximumRows, out numTotalRows1);

            return Employees.OrderBy(c => c.strEmpName).ToList();


        }

        public string SaveMobileAttendance(string strUser, string strEmpID, string strAttDateTime, string strReason, string strLocation,
            string strLongitude, string strLatitude, string deviceID)
        {
            ManualIOBLL objBLL = new ManualIOBLL();
            MobileIO obj = new MobileIO();
            string strmsg=string.Empty;
            
            obj.strEmpID = strEmpID;
            // obj.intCardID = 0;
            // obj.strCardID = "";

            obj.dtAttenTime = DateTime.ParseExact(strAttDateTime, "dd-MMM-yyyy hh:mm tt", null);
            
            //  obj.intPort = 0;
            //  obj.strDeviceID = "";
            //  obj.RandomValue=0;
            //  obj.intShiftID=0;
            obj.strReason = strReason;
            
            obj.bitFromMobile = true;
            obj.strLocation = strLocation;
            obj.Longitude =Convert.ToDouble(strLongitude);
            obj.Latitude = Convert.ToDouble(strLatitude);
            obj.strDeviceID = deviceID;

            int i = 0;

            try
            {
                obj.strEUser = strUser;
                if (obj.intRowID > 0)
                {

                    //   i = objBLL.Edit(model.ManualIO, ref strmsg);
                }
                else
                {
                    obj.strIUser = strUser;

                    i = objBLL.AddForMobile(obj, ref strmsg);
                    if (i >= 0)
                    {
                        strmsg = "Attendance saved successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strmsg;
        }
        public IList<AttendanceReportForMobile> GetMobileAttendanceReport(string empId, string strFromDate, string strToDate)
        {
            AttendanceReportBLL objBLL = new AttendanceReportBLL();
            List<AttendanceReportForMobile> LstAttendanceReport;

            int Total = 0;
           
            LstAttendanceReport = objBLL.GetMobileAttendanceReport(empId,Common.FormatDate(strFromDate, "dd-MMM-yyyy", "yyyy-MM-dd"),
                                                                         Common.FormatDate(strToDate, "dd-MMM-yyyy", "yyyy-MM-dd"),
                                                                         out Total);

            foreach (AttendanceReportForMobile obj in LstAttendanceReport)
            {
                try
                {
                     
                    obj.strInTime = obj.dtFirstInTime.ToString("hh:mm tt");
                    obj.strOutTime = obj.dtLastOutTime.ToString("hh:mm tt");
                    obj.strAttendDate = obj.dtFirstInTime != null ? obj.dtFirstInTime.ToString("dd-MMM-yyyy") : "";
                }
                catch (Exception ex)
                {
                }
            }
            return LstAttendanceReport.OrderBy(c => c.dtFirstInTime).Reverse().ToList(); ;
        }
    }
    public class MLeave
    {
        public string strEmpID;
        public string strEmpName;
        public int intAppFlowID;
        public int intApplicationID;
        public int intAppStatusID;
        public string strApplicationType;
        public string strDepartment;
        public string strDesignation;
        public string strLeaveType;
        public string strApplyDate;
        public string strApplyFromDate;
        public string strApplyToDate;
        public string strApplyFromTime;
        public string strApplyToTime;
        public double fltDuration;
        public double fltWithPayDuration;
        public double fltWithoutPayDuration;

        public string strSubmittedApplicationType;
        public string strSubmittedApplyFromDate;
        public string strSubmittedApplyFromTime;
        public string strSubmittedApplyToDate;
        public string strSubmittedApplyToTime;
        public double fltSubmittedDuration;

        public string strPurpose;
        public string strContactAddress;
        public string strContactNo;

        public int intNextNodeID;
        // public int intApplicationID;
        // public int intAppStatusID;
        public int intNodeID;
        public string strAuthorID;
        // public int intAppFlowID;
        public string strComments;
    }
}
