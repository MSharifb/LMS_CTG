using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS.BLL;
using LMS.DAL;
using LMSEntity;
using MvcContrib.Pagination;
using System.Net.Mail;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net;

namespace LMS.Web.Models
{
    public class OOAApprovalFlowModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        List<ApprovalComments> _LstOOAApprovalComments;
        string _strDestAuthorID;

        int _intNodeID;
        List<OutOfOffice> lstOutOfOffice;
        IPagination<OutOfOffice> lstOutOfOffice1;
        OutOfOffice _OutOfOffice;
        public string _Message;
        public SelectList _LeaveType;
        public List<SelectListItem> _Approver;
        public List<LeaveLedger> _lstLeaveLedger;
        public int _intNextNodeID;
        public OOAApprovalFlow _OOAApprovalFlow;
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
        public string strSortType
        {
            get { return _strSortType; }
            set { _strSortType = value; }
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

        public OutOfOffice OutOfOffice
        {
            get
            {
                if (this._OutOfOffice == null)
                {
                    this._OutOfOffice = new OutOfOffice();
                }
                return _OutOfOffice;
            }
            set { _OutOfOffice = value; }
        }
        public OOAApprovalFlow OOAApprovalFlow
        {
            get
            {
                if (this._OOAApprovalFlow == null)
                {
                    this._OOAApprovalFlow = new OOAApprovalFlow();
                }
                return _OOAApprovalFlow;
            }
            set { _OOAApprovalFlow = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public List<OutOfOffice> LstOutOfOffice
        {
            get { return lstOutOfOffice; }
            set { lstOutOfOffice = value; }
        }

        public List<LeaveLedger> LstLeaveLedger
        {
            get { return _lstLeaveLedger; }
            set { _lstLeaveLedger = value; }
        }


        public List<ApprovalComments> LstOOAApprovalComments
        {
            get { return _LstOOAApprovalComments; }
            set { _LstOOAApprovalComments = value; }
        }

        public IPagination<OutOfOffice> LstOutOfOffice1
        {
            get { return lstOutOfOffice1; }
            set { lstOutOfOffice1 = value; }
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
                List<OOAApprovalAuthor> lstOOAApprovalAuthor = new List<OOAApprovalAuthor>();
                OOAApprovalAuthorBLL objOOAApprovalAuthorBLL = new OOAApprovalAuthorBLL();
                OOAApprovalPathBLL objOOAApprovalPathBLL = new OOAApprovalPathBLL();
                try
                {
                    intNextNodeID = objOOAApprovalPathBLL.OOAApprovalPathDetailsGet(-1, intNodeID, -1, LoginInfo.Current.strCompanyID)[0].intNodeID;
                }
                catch (Exception ex)
                {

                }

                lstOOAApprovalAuthor = objOOAApprovalAuthorBLL.OOAApprovalAuthorGet(-1, intNextNodeID, "", "", LoginInfo.Current.strCompanyID);
                lstOOAApprovalAuthor.OrderBy(c => c.strAuthorType);

                foreach (OOAApprovalAuthor lt in lstOOAApprovalAuthor)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strAuthorID.ToString();
                    item.Text = lt.strEmpName;
                    itemList.Add(item);
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


        OutOfOfficeBLL objBLL = new OutOfOfficeBLL();


        /// <summary>
        /// Save
        /// </summary>
        /// 

      
        public int SaveData(OOAApprovalFlowModels model, ref string strmsg)
        {

            int i = -1;
        
            return i;
        }

       
        
        
        
        
        
        /// <summary>
        /// Delete
        /// </summary>
        /// 

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
           

            return null;
        }

        public ApprovalFlow GetApprovalFlow(int intAppFlowID, int intApplicationID, int intNodeID, string strAuthorID, string strCompanyID)
        {
            ApprovalFlowBLL objBLLAF = new ApprovalFlowBLL();
            return objBLLAF.ApprovalFlowGet(intAppFlowID);
        }


        public void GetApprovalFlowComments(int intAppFlowID, long intApplicationID, int intAppStatusID)
        {
            ApprovalCommentsBLL objBLLAF = new ApprovalCommentsBLL();
            LstOOAApprovalComments = objBLLAF.ApprovalCommentsGet(intAppFlowID, intApplicationID, intAppStatusID, LoginInfo.Current.strCompanyID);
        }

        public void GetLeaveApplicationAll()
        {
            //lstLeaveApplication = objBLL.LeaveApplicationGetAll(LoginInfo.Current.strCompanyID);
        }

        public List<LeaveApplication> GetLeaveApplicationPaging(LeaveApplication objLeaveApplication)
        {
           
            return null;
        }

        public List<LeaveLedger> GetLeaveStatus(LeaveApplication objLeaveApplication)
        {
             return null;
        }

        public List<LeaveLedger> GetLeaveLedgerHistory(LeaveApplication objLeaveApplication)
        {
            //LstLeaveLedger = objBLL.GetLeaveLedgerHistory(objLeaveApplication.intApplicationID, objLeaveApplication.intLeaveYearID, objLeaveApplication.intLeaveTypeID, objLeaveApplication.fltWithPayDuration, objLeaveApplication.strEmpID, LoginInfo.Current.strCompanyID);
            //return LstLeaveLedger.OrderBy(c => c.strLeaveType).ToList();
            return null;
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