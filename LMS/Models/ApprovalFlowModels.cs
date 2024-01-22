using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LMS.BLL;
using LMSEntity;
using MvcContrib.Pagination;

/*
Revision History (RH):
		SL	    : 01
		Author	: Md. Amanullah
		Date	: 2016-Feb-18
        SCR     : MFS_IWM_PRM_LMS.doc (SCR#76)
		Desc.	: Common e-mail send after all final approval which is defined in System Configuration -> E-mail Notification to Admin
		---------
*/

namespace LMS.Web.Models
{
    public class ApprovalFlowModels
    {
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        int _intNodeID;   
        
        List<ApprovalComments> _LstApprovalComments;
        private string _strSortBy;
        private string _strSortType;
        private string _strDestAuthorID;         
        public string _Message;


        public SelectList _LeaveType;
        private SelectList _HalfDayFor;
        private SelectList _WorkingTime;
        public List<SelectListItem> _Approver;
        public List<LeaveLedger> _lstLeaveLedger;

        List<LeaveApplication> lstLeaveApplication;
        IPagination<LeaveApplication> lstLeaveApplication1;
        LeaveApplication _LeaveApplication;

        public int _intNextNodeID;
        public ApprovalFlow _ApprovalFlow;
        public List<ValidationMessage> MessageList { set; get; }
        private LeaveYear _ActiveLeaveYear;

        public string strSortBy
        {
            get { return _strSortBy; }
            set { _strSortBy = value; }
        }

        public string strDestAuthorID
        {
            get { return _strDestAuthorID; }
            set { _strDestAuthorID = value; }
        }

        public int intNodeID
        {
            get { return _intNodeID; }
            set { _intNodeID = value; }
        }
        public int intNextNodeID
        {
            get { return _intNextNodeID; }
            set { _intNextNodeID = value; }
        }       
        public int startRowIndex
        {
            get { return _startRowIndex; }
            set { _startRowIndex = value; }
        }
        public int maximumRows
        {
            get { return _maximumRows; }
            set { _maximumRows = value; }
        }

        public int numTotalRows
        {
            get { return _numTotalRows; }
            set { _numTotalRows = value; }
        }

        public string strSortType
        {
            get { return _strSortType; }
            set { _strSortType = value; }
        }
       
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public LeaveApplication LeaveApplication
        {
            get
            {
                if (this._LeaveApplication == null)
                {
                    this._LeaveApplication = new LeaveApplication();
                }
                return _LeaveApplication;
            }
            set { _LeaveApplication = value; }
        }
        public ApprovalFlow ApprovalFlow
        {
            get
            {
                if (this._ApprovalFlow == null)
                {
                    this._ApprovalFlow = new ApprovalFlow();
                }
                return _ApprovalFlow;
            }
            set { _ApprovalFlow = value; }
        }
       
        public List<LeaveApplication> LstLeaveApplication
        {
            get { return lstLeaveApplication; }
            set { lstLeaveApplication = value; }
        }

        public List<LeaveLedger> LstLeaveLedger
        {
            get { return _lstLeaveLedger; }
            set { _lstLeaveLedger = value; }
        }


        public List<ApprovalComments> LstApprovalComments
        {
            get { return _LstApprovalComments; }
            set { _LstApprovalComments = value; }
        }

        public IPagination<LeaveApplication> LstLeaveApplication1
        {
            get { return lstLeaveApplication1; }
            set { lstLeaveApplication1 = value; }
        }
        public bool ShowPreview { set; get; }
        public bool IsFromUrl { set; get; }

        private string strYearStartDate;
        public string StrYearStartDate
        {
            get { return strYearStartDate; }
            set { strYearStartDate = value; }
        }

        private string strYearEndDate;
        public string StrYearEndDate
        {
            get { return strYearEndDate; }
            set { strYearEndDate = value; }
        }

        private string _strHalfDayFromTime;
        public string strHalfDayFromTime
        {
            get { return _strHalfDayFromTime; }
            set { _strHalfDayFromTime = value; }
        }

        private string _strHalfDayToTime;
        public string strHalfDayToTime
        {
            get { return _strHalfDayToTime; }
            set { _strHalfDayToTime = value; }
        }
        private string strEmpSearch;
        public string StrEmpSearch
        {
            get { return strEmpSearch; }
            set { strEmpSearch = value; }
        }

        public LeaveYear ActiveLeaveYear
        {
            get { return _ActiveLeaveYear; }
            set { _ActiveLeaveYear = value; }
        }

        public List<SelectListItem> Approver
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<ApprovalAuthor> lstApprovalAuthor = new List<ApprovalAuthor>();
                ApprovalAuthorBLL objApprovalAuthorBLL = new ApprovalAuthorBLL();
                ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
                //try
                //{
                //    intNextNodeID = objApprovalPathBLL.ApprovalPathDetailsGet(-1, intNodeID, -1, LoginInfo.Current.strCompanyID)[0].intNodeID;
                //}
                //catch (Exception ex)
                //{

                //}

               // Block lstApprovalAuthor = objApprovalAuthorBLL.ApprovalAuthorGet(-1, intNextNodeID, "", "", LoginInfo.Current.strCompanyID);
                if(this.LeaveApplication != null)
                    lstApprovalAuthor = objApprovalAuthorBLL.ApprovalAuthorGet(-1, Convert.ToInt32( this.LeaveApplication.intApplicationID), "", "", LoginInfo.Current.strCompanyID);

               // Block lstApprovalAuthor.OrderBy(c => c.strAuthorType);

                foreach (ApprovalAuthor lt in lstApprovalAuthor)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strAuthorID.ToString();
                    item.Text = lt.strEmpName;
                    itemList.Add(item);

                    intNextNodeID = lt.intNodeID; // Added For Central Approval Flow System
                }
                this._Approver = itemList;
                return _Approver;
            }
            set { _Approver = value; }
        }


        public SelectList LeaveType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<LeaveType> lstLeaveType = new List<LeaveType>();
                lstLeaveType = Common.fetchLeaveType();

                foreach (LeaveType lt in lstLeaveType)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intLeaveTypeID.ToString();
                    item.Text = lt.strLeaveType;
                    itemList.Add(item);
                }
                this._LeaveType = new SelectList(itemList, "Value", "Text");
                return _LeaveType;
            }
            set { _LeaveType = value; }
        }
        
        public SelectList HalfDayFor
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                SelectListItem item = new SelectListItem();
                item.Value = "F";
                item.Text = "From Date";
                itemList.Add(item);
                item = new SelectListItem();

                item.Value = "T";
                item.Text = "To Date";
                itemList.Add(item);

                this._HalfDayFor = new SelectList(itemList, "Value", "Text");
                return _HalfDayFor;
            }
            set { _HalfDayFor = value; }
        }

        public SelectList WorkingTime
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<OfficeTimeDetails> lstWrkTime = new List<OfficeTimeDetails>();

                lstWrkTime = Common.fetchWorkingTime();

                foreach (OfficeTimeDetails wt in lstWrkTime)
                {

                    SelectListItem item = new SelectListItem();
                    item.Value = wt.intDurationID.ToString();
                    item.Text = wt.strDurationName;

                    itemList.Add(item);
                }
                this._WorkingTime = new SelectList(itemList, "Value", "Text");
                return _WorkingTime;
            }
            set { _WorkingTime = value; }
        }

        LeaveApplicationBLL objBLL = new LeaveApplicationBLL();


        /// <summary>
        /// Save
        /// </summary>
        /// 

        public int SaveData(ApprovalFlowModels model, ref string strmsg)
        {

            int i = -1;
            ApprovalFlowBLL objBll = new ApprovalFlowBLL();
            try
            {
                // TODO: Add insert logic here

                model.ApprovalFlow.strEUser = LoginInfo.Current.LoginName;
                model.ApprovalFlow.strIUser = LoginInfo.Current.LoginName;
                model.ApprovalFlow.strCompanyID = LoginInfo.Current.strCompanyID;

                model.ApprovalFlow.strDepartmentID = LoginInfo.Current.strDepartmentID;
                model.ApprovalFlow.strDesignationID = LoginInfo.Current.strDesignationID;

                model.ApprovalFlow.strApplicationType = model.LeaveApplication.strApplicationType;
                model.ApprovalFlow.dtApplyFromDate = model.LeaveApplication.dtApplyFromDate;
                model.ApprovalFlow.dtApplyToDate = model.LeaveApplication.dtApplyToDate;

                model.ApprovalFlow.strApplyFromTime = model.LeaveApplication.intDurationID > 0 ? model.strHalfDayFromTime : model.LeaveApplication.strApplyFromTime;
                model.ApprovalFlow.strApplyToTime = model.LeaveApplication.intDurationID > 0 ? model.strHalfDayToTime: model.LeaveApplication.strApplyToTime;

                model.ApprovalFlow.strHalfDayFor = model.LeaveApplication.strHalfDayFor;
                model.ApprovalFlow.intDurationID = model.LeaveApplication.intDurationID;

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.ApprovalFlow.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.ApprovalFlow.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.ApprovalFlow.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }
                else
                {
                    model.ApprovalFlow.fltDuration = model.LeaveApplication.fltDuration;
                    model.ApprovalFlow.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration;
                    model.ApprovalFlow.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration;
                }

                model.ApprovalFlow.intSourceNodeID = model.ApprovalFlow.intNodeID;
                model.ApprovalFlow.strSourceAuthorID = model.ApprovalFlow.strAuthorID;
                ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();

                /* Block For Central Approval System
                try
                {
                    intNextNodeID = objApprovalPathBLL.ApprovalPathDetailsGet(-1, model.intNodeID, -1, LoginInfo.Current.strCompanyID)[0].intNodeID;
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
                    if (objApprovalPathBLL.ApprovalPathDetailsGet(-1, intNextNodeID, -1, LoginInfo.Current.strCompanyID).Count <= 0)
                    {
                        model.LeaveApplication.IsForApproval = true;
                    }
                }
                catch (Exception ex)
                {
                    model.LeaveApplication.IsForApproval = false;
                }
                */
                
                i = objBll.Add(model.ApprovalFlow, ref strmsg);
                /* Block for Central Approval System
                if (intNextNodeID == 0)
                {
                    i = model.ApprovalFlow.intAppFlowID;
                }
                try
                {
                    if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                    {
                        model.LeaveApplication.strSupervisorID = model.ApprovalFlow.strDestAuthorID;
                        model.LeaveApplication.intAppStatusID = model.ApprovalFlow.intAppStatusID;
                        model.LeaveApplication.strCompanyID = model.ApprovalFlow.strCompanyID;
                        if (model.LeaveApplication.intDurationID > 0)
                        {
                            model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                            model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                        }

                        Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName);
                    }
                */
                    /*RH#01*/
                /* Block for Central Approval System
                    if (intNextNodeID == 0)
                    {
                        if (!String.IsNullOrEmpty(AppConstant.EmailNotificationToAdminFinalApproval))
                        {
                            Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName, AppConstant.EmailNotificationToAdminFinalApproval, "");
                        }
                    }
                 
                    /*End RH#01
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                */
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
        
        /// <summary>
        /// Delete
        /// </summary>
        public int Delete(int Id, ref string strmsg)
        {
            int i = -1;
            try
            {
                // TODO: Add insert logic here
                //  i = objBLL.Delete(Id);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public LeaveApplication GetLeaveApplication(long Id)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();
            try
            {
                 model.LeaveApplication = objBLL.LeaveApplicationGet(Id);
                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltSubmittedDuration = model.LeaveApplication.fltSubmittedDuration / LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithPayDuration = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithoutPayDuration = model.LeaveApplication.fltSubmittedWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                }
                return model.LeaveApplication;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApprovalFlow GetApprovalFlow(int intAppFlowID, int intApplicationID, int intNodeID, string strAuthorID, string strCompanyID)
        {
            ApprovalFlowBLL objBLLAF = new ApprovalFlowBLL();
            return objBLLAF.ApprovalFlowGet(intAppFlowID);
        }


        public void GetApprovalFlowComments(int intAppFlowID, long intApplicationID, int intAppStatusID)
        {
            ApprovalCommentsBLL objBLLAF = new ApprovalCommentsBLL();
            LstApprovalComments = objBLLAF.ApprovalCommentsGet(intAppFlowID, intApplicationID, intAppStatusID, LoginInfo.Current.strCompanyID);
        }

        public void GetLeaveApplicationAll()
        {
            //lstLeaveApplication = objBLL.LeaveApplicationGetAll(LoginInfo.Current.strCompanyID);
        }

        public List<LeaveApplication> GetLeaveApplicationPaging(LeaveApplication objLeaveApplication)
        {
            int Total = 0;
            lstLeaveApplication = objBLL.LeaveApplicationGet(objLeaveApplication.intApplicationID, objLeaveApplication.strEmpID,
                                                            objLeaveApplication.strEmpName, objLeaveApplication.intLeaveYearID,
                                                            objLeaveApplication.intLeaveTypeID, objLeaveApplication.strApplyFromDate,
                                                            objLeaveApplication.strApplyToDate, objLeaveApplication.strApplicationType,
                                                            objLeaveApplication.intAppStatusID, objLeaveApplication.strApprovalProcess,
                                                            objLeaveApplication.bitIsDiscard, LoginInfo.Current.strCompanyID,
                                                            objLeaveApplication.strDepartmentID, objLeaveApplication.strDesignationID,
                                                            objLeaveApplication.bitIsAdjustment, objLeaveApplication.ZoneId, strSortBy, strSortType, _startRowIndex, maximumRows, out Total);

            numTotalRows = Total;
            return LstLeaveApplication;
        }

        public List<LeaveLedger> GetLeaveStatus(LeaveApplication objLeaveApplication)
        {
            LstLeaveLedger = objBLL.LeaveBalanceIndividualGet(objLeaveApplication.intApplicationID, objLeaveApplication.intLeaveYearID, objLeaveApplication.intLeaveTypeID, objLeaveApplication.fltWithPayDuration, objLeaveApplication.strEmpID, LoginInfo.Current.strCompanyID, objLeaveApplication.strApplicationType);
            return LstLeaveLedger.OrderBy(c => c.strLeaveType).ToList();
        }

        public List<LeaveLedger> GetLeaveLedgerHistory(LeaveApplication objLeaveApplication)
        {
            LstLeaveLedger = objBLL.GetLeaveLedgerHistory(objLeaveApplication.intApplicationID, objLeaveApplication.intLeaveYearID, objLeaveApplication.intLeaveTypeID, objLeaveApplication.fltWithPayDuration, objLeaveApplication.strEmpID, LoginInfo.Current.strCompanyID);
            return LstLeaveLedger.OrderBy(c => c.strLeaveType).ToList();
        }

        public List<ValidationMessage> ValidateLeaveApplication(ApprovalFlowModels model)
        {
            List<ValidationMessage> messageList = new List<ValidationMessage>();
            try
            {
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;
                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                messageList = LeaveApplicationValidationBLL.ValidateLeaveApplication(model.LeaveApplication);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return messageList;
        }

        public LeaveYear GetLeaveYear(int Id)
        {
            LeaveYearBLL objLvYearBLL = new LeaveYearBLL();
            try
            {

                return objLvYearBLL.LeaveYearGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}