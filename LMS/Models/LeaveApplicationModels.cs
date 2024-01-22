using System;
using MvcPaging;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using MvcContrib.Pagination;
using System.Collections.Generic;

using LMS.BLL;
using LMSEntity;

/*
Revision History (RH):
		SL	    : 01
		Author	: AMN
		Date	: 2015-May-12
        SCR     : MFS_IWM_LMS_SCR.doc (SCR#65)
		Desc	: Email send to defined approval path person on submit, approve, reject and cancel
		---------
*/

namespace LMS.Web.Models
{
    public class LeaveApplicationModels
    {
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        private int _intNodeID;
        private int _intSearchLeaveYearId;
        private int _IntSearchApplicationStatusId;
        private int _intSearchLeaveTypeId;

        private bool _Isapplicable;
        private bool _IsSearch;
        private bool _IsDateWiseSearch;
        private bool _IsCanBulkApprove;
        private bool _IsBulkApprove;
        private bool _IsSendEmailToAuthority;

        private string strEmpSearch;
        private string strYearStartDate;
        private string strYearEndDate;

        private string _strSearchAuthorID;
        private string _strSearchID;
        private string _strSearchInitial;
        //private string _strApprovedByInitial;
        private string _strSearchName;
        
        private string _strSearchApplyTo;
        private string _strSearchLeaveYear;
        private string _strSearchDesignationId;
        private string _strSearchDepartmentId;
        private string _strSearchApplyFrom;
        private string _strHalfDayFromTime;
        private string _strHalfDayToTime;
        private string _strAuthorDesignation;
        private string _strAuthorDepartment;
        private string _strResDesignation;
        private string _strResDepartment;
        private string _strOffAuthorDesignation;
        private string _strOffAuthorDepartment;


        private IPagedList<LeaveApplication> _LstLeaveApplicationPaging;
        private IList<LeaveApplication> lstLeaveApplication;
        private IPagination<LeaveApplication> lstLeaveApplication1;
        private LeaveApplication _LeaveApplication;
        private LeaveApplication _RefLeaveApplication;
        private LeaveYear _ActiveLeaveYear;
        private string _Message;

        private List<SelectListItem> _Approver;
        private List<ApprovalComments> _LstApprovalComments;

        private SelectList _OnLineLeaveType;
        private SelectList _OffLineLeaveType;
        private SelectList _LeaveType;
        private SelectList _LeaveYear;
        private SelectList _ApplicationStatus;
        private SelectList _Department;
        private SelectList _Designation;
        private SelectList _HalfDayFor;
        private SelectList _WorkingTime;
        private SelectList _CountryList;

        LeaveApplicationBLL objBLL = new LeaveApplicationBLL();


        //Extend 2015-02-05
        public bool IsEmployeePhoto { get; set; }
        //public EmployeePhoto EmployeePhoto { get; set; }


        public bool IsSendEmailToAuthority
        {
            get { return _IsSendEmailToAuthority; }
            set { _IsSendEmailToAuthority = value; }
        }

        public bool IsBulkApprove
        {
            get { return _IsBulkApprove; }
            set { _IsBulkApprove = value; }
        }

        public bool IsCanBulkApprove
        {
            get { return _IsCanBulkApprove; }
            set { _IsCanBulkApprove = value; }
        }

        public bool IsDateWiseSearch
        {
            get { return _IsDateWiseSearch; }
            set { _IsDateWiseSearch = value; }
        }

        public bool IsSearch
        {
            get { return _IsSearch; }
            set { _IsSearch = value; }
        }

        public bool Isapplicable
        {
            get { return _Isapplicable; }
            set { _Isapplicable = value; }
        }
        
        public string StrEmpSearch
        {
            get { return strEmpSearch; }
            set { strEmpSearch = value; }
        }
       
        public string StrYearStartDate
        {
            get { return strYearStartDate; }
            set { strYearStartDate = value; }
        }

        public List<ApprovalFlow> ApprovalFlowList { set; get; }
       
        public string StrYearEndDate
        {
            get { return strYearEndDate; }
            set { strYearEndDate = value; }
        }

        
        public string StrSearchLeaveYear
        {
            get { return _strSearchLeaveYear; }
            set { _strSearchLeaveYear = value; }
        }

        
        public string StrSearchDesignationId
        {
            get { return _strSearchDesignationId; }
            set { _strSearchDesignationId = value; }
        }

        
        public string StrSearchDepartmentId
        {
            get { return _strSearchDepartmentId; }
            set { _strSearchDepartmentId = value; }
        }

        
        public int IntSearchApplicationStatusId
        {
            get { return _IntSearchApplicationStatusId; }
            set { _IntSearchApplicationStatusId = value; }
        }

       
        public int intSearchLeaveYearId
        {
            get { return _intSearchLeaveYearId; }
            set { _intSearchLeaveYearId = value; }
        }

       
        public int intSearchLeaveTypeId
        {
            get { return _intSearchLeaveTypeId; }
            set { _intSearchLeaveTypeId = value; }
        }

        
        public string StrSearchApplyFrom
        {
            get { return _strSearchApplyFrom; }
            set { _strSearchApplyFrom = value; }
        }

        
        public string StrSearchApplyTo
        {
            get { return _strSearchApplyTo; }
            set { _strSearchApplyTo = value; }
        }


        public List<LeaveLedger> _lstLeaveLedger;
        
        public LeaveYear ActiveLeaveYear
        {
            get { return _ActiveLeaveYear; }
            set { _ActiveLeaveYear = value; }
        }

        public string strSearchAuthorID
        {
            get { return _strSearchAuthorID; }
            set { _strSearchAuthorID = value; }
        }

        public string strSearchID
        {
            get { return _strSearchID; }
            set { _strSearchID = value; }
        }

        public string strSearchInitial
        {
            get { return _strSearchInitial; }
            set { _strSearchInitial = value; }
        }
        //public string strApprovedByInitial
        //{
        //    get { return _strApprovedByInitial; }
        //    set { _strApprovedByInitial = value; }
        //}


        public string strSearchName
        {
            get { return _strSearchName; }
            set { _strSearchName = value; }
        }

        public string strSortBy
        {
            get { return _strSortBy; }
            set { _strSortBy = value; }
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
       
        public string strHalfDayFromTime
        {
            get { return _strHalfDayFromTime; }
            set { _strHalfDayFromTime = value; }
        }
        
        public string strHalfDayToTime
        {
            get { return _strHalfDayToTime; }
            set { _strHalfDayToTime = value; }
        }

        public string strAuthorDesignation
        {
            get { return _strAuthorDesignation; }
            set { _strAuthorDesignation = value; }
        }

        public string strAuthorDepartment
        {
            get { return _strAuthorDepartment; }
            set { _strAuthorDepartment = value; }
        }

        public string strResDesignation
        {
            get { return _strResDesignation; }
            set { _strResDesignation = value; }
        }

        public string strResDepartment
        {
            get { return _strResDepartment; }
            set { _strResDepartment = value; }
        }
        
        

        public string strOffAuthorDesignation
        {
            get { return _strOffAuthorDesignation; }
            set { _strOffAuthorDesignation = value; }
        }

        public string strOffAuthorDepartment
        {
            get { return _strOffAuthorDepartment; }
            set { _strOffAuthorDepartment = value; }
        }

        public int intNodeID
        {
            get { return _intNodeID; }
            set { _intNodeID = value; }
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

        public int _intNextNodeID;
        
        public int intNextNodeID
        {
            get { return _intNextNodeID; }
            set { _intNextNodeID = value; }
        }

        private bool _isValidId = true;
        public bool IsValidId
        {
            get { return _isValidId; }
            set { _isValidId = value; }
        }

        public ApprovalFlow _ApprovalFlow;
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
        public LeaveApplication RefLeaveApplication
        {
            get
            {
                if (this._RefLeaveApplication == null)
                {
                    this._RefLeaveApplication = new LeaveApplication();
                }
                return _RefLeaveApplication;
            }
            set { _RefLeaveApplication = value; }
        }


        public List<ValidationMessage> MessageList { set; get; }

        public List<LeaveLedger> LstLeaveLedger
        {
            get { return _lstLeaveLedger; }
            set { _lstLeaveLedger = value; }
        }

        public List<SelectListItem> Approver
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<ApprovalAuthor> lstApprovalAuthor = new List<ApprovalAuthor>();

                ApprovalAuthorBLL objApprovalAuthorBLL = new ApprovalAuthorBLL();

                /*
                lstApprovalAuthor = objApprovalAuthorBLL.ApprovalAuthorGet(-1, intNodeID, "", "", LoginInfo.Current.strCompanyID).OrderBy(x => x.strEmpInitial).ToList();

                lstApprovalAuthor.OrderBy(c => c.strAuthorType);
                
                foreach (ApprovalAuthor lt in lstApprovalAuthor)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strAuthorID.ToString();
                    item.Text = lt.strEmpInitial+" - "+lt.strEmpName;
                    itemList.Add(item);
                }
                 * */
                // Added For Central Approval System.
                if (string.IsNullOrEmpty(this.EmpID))
                    this.EmpID = LoginInfo.Current.strEmpID;

                var ApproverInfoList = objApprovalAuthorBLL.GetApproverInfoByEmpId(this.EmpID); 

                foreach (ApproverInfo lt in ApproverInfoList)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.Id.ToString();
                    item.Text = lt.FullName;
                    itemList.Add(item);
                }
                // End

                _Approver = itemList;
                return _Approver;
            }
            set { _Approver = value; }
        }

        public List<ApprovalComments> LstApprovalComments
        {
            get { return _LstApprovalComments; }
            set { _LstApprovalComments = value; }
        }


        public IPagedList<LeaveApplication> LstLeaveApplicationPaging
        {
            get { return _LstLeaveApplicationPaging; }
            set { _LstLeaveApplicationPaging = value; }
        }

        public IList<LeaveApplication> LstLeaveApplication
        {
            get { return lstLeaveApplication; }
            set { lstLeaveApplication = value; }
        }

        public IPagination<LeaveApplication> LstLeaveApplication1
        {
            get { return lstLeaveApplication1; }
            set { lstLeaveApplication1 = value; }
        }


        public SelectList OnLineLeaveType
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
                this._OnLineLeaveType = new SelectList(itemList, "Value", "Text");
                return _OnLineLeaveType;
            }
            set { _OnLineLeaveType = value; }
        }


        public SelectList OffLineLeaveType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<LeaveType> lstLeaveType = new List<LeaveType>();

                lstLeaveType = Common.fetchEmpployeeLeaveTypeAll(LeaveApplication.strEmpID == null ? "-1" : LeaveApplication.strEmpID);

                foreach (LeaveType lt in lstLeaveType)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intLeaveTypeID.ToString();
                    item.Text = lt.strLeaveType;
                    itemList.Add(item);
                }
                this._OffLineLeaveType = new SelectList(itemList, "Value", "Text");
                return _OffLineLeaveType;
            }
            set { _OffLineLeaveType = value; }
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
                this._LeaveType = new SelectList(itemList, "Value", "Text",this.intSearchLeaveTypeId);
                return _LeaveType;
            }
            set { _LeaveType = value; }
        }

        public SelectList LeaveYear
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<LeaveYear> lstLeaveYear = new List<LeaveYear>();

                lstLeaveYear = Common.fetchLeaveYear();

                foreach (LeaveYear lt in lstLeaveYear)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.intLeaveYearID.ToString();
                    item.Text = lt.strYearTitle;
                    itemList.Add(item);
                }
                this._LeaveYear = new SelectList(itemList, "Value", "Text", LoginInfo.Current.intLeaveYearID);

                return _LeaveYear;
            }
            set { _LeaveYear = value; }
        }

        public SelectList Designation
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Designation> lstDesignation = new List<Designation>();

                lstDesignation = Common.fetchDesignation().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();

                foreach (Designation lt in lstDesignation)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strDesignationID;
                    item.Text = lt.strDesignation;
                    itemList.Add(item);
                }
                this._Designation = new SelectList(itemList, "Value", "Text",this.StrSearchDesignationId);
                return _Designation;
            }
            set { _Designation = value; }
        }

        public SelectList Department
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Department> lstDepartment = new List<Department>();

               // lstDepartment = Common.fetchDepartment().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();
                lstDepartment = Common.fetchDepartment().Where(c => c.ZoneInfoId == Convert.ToInt32( LoginInfo.Current.strCompanyID)).ToList();

                foreach (Department lt in lstDepartment)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strDepartmentID;
                    item.Text = lt.strDepartment;
                    itemList.Add(item);
                }
                this._Department = new SelectList(itemList, "Value", "Text",this.StrSearchDepartmentId);
                return _Department;
            }
            set { _Department = value; }
        }

        public SelectList ApplicationStatus
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<ApplicationStatusCaption> lstApplicationStatus = new List<ApplicationStatusCaption>();

                lstApplicationStatus = Common.fetchApplicationStatus();

                foreach (ApplicationStatusCaption lt in lstApplicationStatus)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intAppStatusID.ToString();
                    item.Text = lt.strStatus;
                    itemList.Add(item);
                }
                this._ApplicationStatus = new SelectList(itemList, "Value", "Text",this.IntSearchApplicationStatusId);
                return _ApplicationStatus;
            }
            set { _ApplicationStatus = value; }
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
        /// <summary>
        /// Added For MPA
        /// </summary>
        public SelectList CountryList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Country> lstCountry = new List<Country>();

                lstCountry = Common.fetchCountry();

                foreach (Country lt in lstCountry)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.Id.ToString();
                    item.Text = lt.Name;
                    itemList.Add(item);
                }
                this._CountryList = new SelectList(itemList, "Value", "Text");
                return _CountryList;
            }
            set { _CountryList = value; }
        }
        

        public List<ValidationMessage> ValidateLeaveApplication(LeaveApplicationModels model)
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

        public int OnlineSubmit(LeaveApplicationModels model, out string strmsg)
        {
            int i = -1;

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            try
            {
                /*RH#01*/
                model.LeaveApplication.strPLID = model.LeaveApplication.strSupervisorID;
                /*End RH#01*/
               // if(model.LeaveApplication.intLeaveTypeID)
                // Added FOR BEPZA
                /*
                LeaveTypeBLL leaveTypeObj = new LeaveTypeBLL();
                var objLvType = leaveTypeObj.LeaveTypeGet(model.LeaveApplication.intLeaveTypeID);
                if (objLvType.bitIsRecreationLeave)
                    model.LeaveApplication.bitIsApprovalProcess = false;
                else
                 Block Temporary */
                    model.LeaveApplication.bitIsApprovalProcess = true;
                // End

                model.LeaveApplication.intAppStatusID = 6;   //Submit              

                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                model.LeaveApplication.strDepartmentID = LoginInfo.Current.strDepartmentID;
                model.LeaveApplication.strDesignationID = LoginInfo.Current.strDesignationID;

                /*---[For FullDayHalfDay Leave]----------*/
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay && model.LeaveApplication.intDurationID >0)
                {
                    model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                    model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                }


                /*---[For not Hourly Leave]----------*/
                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                foreach (LeaveLedger obj in model.LstLeaveLedger)
                {
                    if (obj.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID)
                    {
                        obj.fltApplied = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    }
                }

                /*---[add leave application to db--*/
                i = objBll.Add(model.LeaveApplication, LstLeaveLedger, out strmsg);


                /*---[if leave application added successfully--*/
                /* Block For Central Approval System 
                if (i >= 0)
                {
                    ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                    ApprovalFlow appFlow = objApprovalFlowBLL.ApprovalFlowGet(-1, i, -1, "", LoginInfo.Current.strCompanyID).SingleOrDefault();
                    i = appFlow.intAppFlowID;

                    try
                    {
                        //---[check the submittion is for approval or recommander--
                        ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();                        
                        if (objApprovalPathBLL.ApprovalPathDetailsGet(-1, appFlow.intNodeID, -1, LoginInfo.Current.strCompanyID).Count <= 0)
                        {
                            model.LeaveApplication.IsForApproval = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        model.LeaveApplication.IsForApproval = false;
                    }
                    ENd Block For Central Approval System   */ 

                    // Block For Temporary For Testing Purpose
                    /*
                    try
                    {
                        if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                        { 
                            Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName); 
                        }
                    }
                    catch (Exception ex)
                    {
                        //RH#2015-12-21
                        throw ex;
                    }
                    */
                    // End Block
                //  } ENd If Block For Central Approval System
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int OnlineAdjustmentSubmit(LeaveApplicationModels model, out string strmsg)
        {
            int i = -1;

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            try
            {
                /*Added For MPA to Support PLID System that was used Iwm*/
                model.LeaveApplication.strPLID = model.LeaveApplication.strSupervisorID;
                /*End */

                model.LeaveApplication.bitIsApprovalProcess = true;
                model.LeaveApplication.intAppStatusID = 6;   //Submit

                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                model.LeaveApplication.strDepartmentID = LoginInfo.Current.strDepartmentID;
                model.LeaveApplication.strDesignationID = LoginInfo.Current.strDesignationID;


                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                /*---[For FullDayHalfDay Leave]----------*/
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay && model.LeaveApplication.intDurationID > 0)
                {
                    model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                    model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                }


                //---[add adjustment application to leave application table]
                i = objBll.Add(model.LeaveApplication, LstLeaveLedger, out strmsg);



                //---[if adjustment application added successfully]
                /* Block For Central System
                if (i >= 0)
                {

                    model.LeaveApplication.intApplicationID = i;

                    ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                    ApprovalFlow appFlow = objApprovalFlowBLL.ApprovalFlowGet(-1, i, -1, "", LoginInfo.Current.strCompanyID).SingleOrDefault();

                    i = appFlow.intAppFlowID;

                    try
                    {
                        ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();

                        //---[Check the submittion is for approval or recommander]
                        if (objApprovalPathBLL.ApprovalPathDetailsGet(-1, appFlow.intNodeID, -1, LoginInfo.Current.strCompanyID).Count <= 0)
                        {
                            model.LeaveApplication.IsForApproval = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        model.LeaveApplication.IsForApproval = false;                       
                    }

                    // Block for Temporary for Tesing Purpose
                    /* This Block Previously Block Approval System
                    try
                    {
                        if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                        {
                            Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                     * */
                    // End
             //   }
                // End Block For Central System

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int OnlineCancel(LeaveApplicationModels model, out string strmsg)
        {
            int i = -1;
            strmsg = "";

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            try
            {

                model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;

                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.intApplicationID > 0)
                {
                    model.LeaveApplication.bitIsApprovalProcess = false;
                    model.LeaveApplication.intAppStatusID = 2;
                    model.LeaveApplication.bitIsDiscard = true;

                    i = objBll.Cancel(model.LeaveApplication, out strmsg);  // Cancel Leave

                    if (i >= 0)
                    {
                        ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                        i = objApprovalFlowBLL.ApprovalFlowGet(-1, model.LeaveApplication.intApplicationID, -1, "", LoginInfo.Current.strCompanyID).Max(c => c.intAppFlowID);

                        // Block for Temporary for Tesing Purpose
                        /*
                        try
                        {
                            if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                            { 
                                Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName); 
                            }
                        }
                        catch (Exception ex)
                        {
                            //RH#2015-12-21
                            throw ex;
                        }
                         * */
                        // End
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int OnlineDelete(long Id, out string strmsg)
        {
            int i = -1;
            strmsg = "";

            try
            {
                i = objBLL.Delete(Id, out strmsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int OfflineApprove(LeaveApplicationModels model, out string strmsg)
        {
            int i = 0;

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            try
            {
                if (!string.IsNullOrEmpty(model.LeaveApplication.strSupervisorID))
                {
                    model.LeaveApplication.bitIsApprovalProcess = true;
                    model.LeaveApplication.intAppStatusID = 6;  //Submit      
                    model.LeaveApplication.strPLID = model.LeaveApplication.strSupervisorID; // Added For BEPZA
                }
                else
                {
                    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                    dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
                    dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;

                    char[] sepAr = { ':', ' ' };
                    string[] time = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Time).Split(sepAr);

                    string strAppvDt = model.LeaveApplication.dtOffLineAppvDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                    model.LeaveApplication.dtOffLineAppvDate = Convert.ToDateTime(strAppvDt, dtfi);

                    model.LeaveApplication.bitIsApprovalProcess = false;
                    model.LeaveApplication.intAppStatusID = 1;  //Approve
                    //model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;
                }

                model.LeaveApplication.bitIsOffLine = true;
                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }
                /*---[For FullDayHalfDay Leave]----------*/
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay && model.LeaveApplication.intDurationID > 0)
                {
                    model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                    model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                }
                
                //add leave application to db
                foreach (LeaveLedger obj1 in model.LstLeaveLedger)
                {
                    if (obj1.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID)
                    {
                        obj1.fltApplied = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    }
                }

                i = objBll.Add(model.LeaveApplication, model.LstLeaveLedger, out strmsg);

                LeaveTypeBLL objLeaveTypeBLL = new LeaveTypeBLL();
                var leavetype = (from tr in objLeaveTypeBLL.LeaveTypeGetAll(LoginInfo.Current.strCompanyID).OrderBy(x => x.isServiceLifeType).ThenBy(c => c.strLeaveType)
                                 where tr.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID && tr.isServiceLifeType == true
                                 select tr).LastOrDefault();
                if (leavetype != null)
                {
                    model.LeaveApplication.isServiceLifeType = true;
                }

                #region Update employee status when an employee has taken long term leave.

                if (i >= 0 && model.LeaveApplication.intAppStatusID == 1 && model.LeaveApplication.isServiceLifeType == true && model._LeaveApplication.dtApplyDateFrom<=System.DateTime.Now  )
                {
                    Common.UpdateEmployeeStatus(model.LeaveApplication.strEmpID);
                }

                #endregion


                /*---[if leave application added successfully--*/
               /* Block For Central Approval System.
                if (i >= 0 && !string.IsNullOrEmpty(model.LeaveApplication.strSupervisorID) && model.LeaveApplication.intAppStatusID == 6)
                {
                    ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                    ApprovalFlow appFlow = objApprovalFlowBLL.ApprovalFlowGet(-1, i, -1, "", LoginInfo.Current.strCompanyID).SingleOrDefault();
                    i = appFlow.intAppFlowID;

                    try
                    {
                        //---[check the submittion is for approval or recommander--
                        ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();                        
                        if (objApprovalPathBLL.ApprovalPathDetailsGet(-1, appFlow.intNodeID, -1, LoginInfo.Current.strCompanyID).Count <= 0)
                        {model.LeaveApplication.IsForApproval = true;}
                    }
                    catch (Exception ex)
                          {model.LeaveApplication.IsForApproval = false;}
                    // Block for Temporary for Tesing Purpose
                    /*
                    try
                    {
                        if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                        { 
                            Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName); 
                        }
                    }
                    catch (Exception ex)
                    {}
                     * */
                    // End
               // } ENd Block Central Approval System

            }
            catch (Exception ex)
            {
                i = -1;
                throw ex;
            }
            return i;
        }

        public int OfflineUpdate(LeaveApplicationModels model, out string strmsg)
        {
            int i = 0;
            strmsg = "";

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            try
            {
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
                dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;

                char[] sepAr = { ':', ' ' };
                string[] time = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Time).Split(sepAr);

                string strAppvDt = model.LeaveApplication.dtOffLineAppvDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                model.LeaveApplication.dtOffLineAppvDate = Convert.ToDateTime(strAppvDt, dtfi);
                
                
                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.bitIsOffLine = true;

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }
               
                /*---[For FullDayHalfDay Leave]----------*/
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay && model.LeaveApplication.intDurationID > 0)
                {
                    model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                    model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                }

                //add leave application to db
                foreach (LeaveLedger obj1 in model.LstLeaveLedger)
                {
                    if (obj1.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID)
                    {
                        obj1.fltApplied = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    }
                }

                i = objBll.Update(model.LeaveApplication, model.LstLeaveLedger, out strmsg);
            }

            catch (Exception ex)
            {
                i = -1;
                throw ex;
            }

            return i;

        }

        public int OfflineDelete(long Id, out string strmsg)
        {
            int i = 0;
            strmsg = "";

            try
            {

                i = objBLL.Delete(Id, out strmsg);

            }
            catch (Exception ex)
            {
                i = -1;
                throw ex;
            }
            return i;
        }

        public int OfflineCancel(LeaveApplicationModels model, out string strmsg)
        {
            int i = -1;
            strmsg = "";

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            try
            {

                model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;

                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.intApplicationID > 0)
                {
                    model.LeaveApplication.bitIsApprovalProcess = false;
                    model.LeaveApplication.intAppStatusID = 2;
                    model.LeaveApplication.bitIsDiscard = true;

                    i = objBll.Cancel(model.LeaveApplication, out strmsg);  // Cancel Leave

                    if (i >= 0)
                    {
                        ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                        i = objApprovalFlowBLL.ApprovalFlowGet(-1, model.LeaveApplication.intApplicationID, -1, "", LoginInfo.Current.strCompanyID).Max(c => c.intAppFlowID);

                        // Block for Temporary for Tesing Purpose
                        /*
                        try
                        {
                            if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                            { Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName); }
                        }
                        catch (Exception ex)
                        { }
                         * */
                        // End
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
        
        public int OfflineAdjustmentApprove(LeaveApplicationModels model, out string strmsg)
        {
            int i = -1;
            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            try
            {
                /*Added For MPA to Support PLID System that was used Iwm*/
                model.LeaveApplication.strPLID = model.LeaveApplication.strSupervisorID;
                /*End */

                if (!string.IsNullOrEmpty(model.LeaveApplication.strSupervisorID))
                {
                    model.LeaveApplication.bitIsApprovalProcess = true;
                    model.LeaveApplication.intAppStatusID = 6;  //Submit 
                }
                else
                {
                    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                    dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
                    dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;

                    char[] sepAr = { ':', ' ' };
                    string[] time = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Time).Split(sepAr);

                    string strAppvDt = model.LeaveApplication.dtOffLineAppvDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                    model.LeaveApplication.dtOffLineAppvDate = Convert.ToDateTime(strAppvDt, dtfi);
                    
                    model.LeaveApplication.bitIsApprovalProcess = false;
                    model.LeaveApplication.intAppStatusID = 1;  //Approve
                    //model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;
                }
                
                model.LeaveApplication.bitIsOffLine = true;
                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                /*---[For FullDayHalfDay Leave]----------*/
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay && model.LeaveApplication.intDurationID > 0)
                {
                    model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                    model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                }


                //---[add adjustment application to leave application table]
                i = objBll.Add(model.LeaveApplication, LstLeaveLedger, out strmsg);



                //---[if adjustment application added successfully]
                /* Block For Central Approval System
                 * 
                if (i >= 0 && !string.IsNullOrEmpty(model.LeaveApplication.strSupervisorID) && model.LeaveApplication.intAppStatusID == 6)
                {

                    model.LeaveApplication.intApplicationID = i;

                    ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                    ApprovalFlow appFlow = objApprovalFlowBLL.ApprovalFlowGet(-1, i, -1, "", LoginInfo.Current.strCompanyID).SingleOrDefault();

                    i = appFlow.intAppFlowID;

                    try
                    {
                        ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();

                        //---[Check the submittion is for approval or recommander]
                        if (objApprovalPathBLL.ApprovalPathDetailsGet(-1, appFlow.intNodeID, -1, LoginInfo.Current.strCompanyID).Count <= 0)
                        {
                            model.LeaveApplication.IsForApproval = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        model.LeaveApplication.IsForApproval = false;
                    }

                    // Block for Temporary for Tesing Purpose
                    /*
                    try
                    {
                        if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                        {
                            Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    */
                    // End
               // } // End Block for Central Approval System.
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int OfflineAdjustmentUpdate(LeaveApplicationModels model, out string strmsg)
        {
            int i = 0;
            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            try
            {
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
                dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;

                char[] sepAr = { ':', ' ' };
                string[] time = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Time).Split(sepAr);

                string strAppvDt = model.LeaveApplication.dtOffLineAppvDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                model.LeaveApplication.dtOffLineAppvDate = Convert.ToDateTime(strAppvDt, dtfi);
                
                model.LeaveApplication.bitIsOffLine = true;
                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
 
                model.LeaveApplication.bitIsApprovalProcess = false;
                model.LeaveApplication.intAppStatusID = 1;  //Approve
                model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                /*---[For FullDayHalfDay Leave]----------*/
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay && model.LeaveApplication.intDurationID > 0)
                {
                    model.LeaveApplication.strApplyFromTime = model.strHalfDayFromTime;
                    model.LeaveApplication.strApplyToTime = model.strHalfDayToTime;
                }


                //---[edit adjustment application to leave application table]
                i = objBll.Update(model.LeaveApplication, LstLeaveLedger, out strmsg);

            }
            catch (Exception ex)
            {
                i = -1;
                throw ex;
            }

            return i;
        }

        public int OfflineAdjustmentDelete(long Id, out string strmsg)
        {
            int i = 0;
            strmsg = "";

            try
            {
                i = objBLL.Delete(Id, out strmsg);
            }
            catch (Exception ex)
            {
                i = -1;
                throw ex;
            }
            return i;
        }

        public int AlternateApprove(LeaveApplicationModels model, out string strmsg, bool dontSendCCEmailToAuthority = false)
        {
            int i = -1;
            strmsg = "";

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            LeaveApplication objAppvLeaveInfo = new LeaveApplication();
            try
            {
                model.LeaveApplication.bitIsApprovalProcess = false;
                model.LeaveApplication.intAppStatusID = (int)Utils.LeaveAppStatus.Approve;  // Alternate Approve
                model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;              
                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                //-------------------------------------------              
                objAppvLeaveInfo = model.LeaveApplication.ShallowCopy();
                //------------------------------------------
                

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltSubmittedDuration = model.LeaveApplication.fltSubmittedDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithPayDuration = model.LeaveApplication.fltSubmittedWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithoutPayDuration = model.LeaveApplication.fltSubmittedWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.intApplicationID > 0)
                {
                    i = objBll.Approve(model.LeaveApplication, out strmsg);  //Alternate Approval Process

                    #region Update employee status when an employee has taken long term leave.

                    if (i >= 0 && model.LeaveApplication.intAppStatusID == 1 && model.LeaveApplication.isServiceLifeType == true && model._LeaveApplication.dtApplyDateFrom <= System.DateTime.Now)
                        {
                            Common.UpdateEmployeeStatus(model.LeaveApplication.strEmpID);
                        }

                    #endregion

                    if (i >= 0)
                    {
                        ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                        i = objApprovalFlowBLL.ApprovalFlowGet(-1, model.LeaveApplication.intApplicationID, -1, "", LoginInfo.Current.strCompanyID).Max(c => c.intAppFlowID);

                        // Block for Temporary for Tesing Purpose
                        /*
                        try
                        {
                            Common.SendMail(objAppvLeaveInfo, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName, dontSendCCEmailToAuthority);   // Send Mail to Applicant
                        }
                        catch (Exception ex)
                        {

                        }
                        */
                        // End
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        //public int AlternateRecommend(LeaveApplicationModels model, out string strmsg)
        //{
        //    int i = -1;
        //    strmsg = "";

        //    LeaveApplicationBLL objBll = new LeaveApplicationBLL();
        //    LeaveApplication objAppvLeaveInfo = new LeaveApplication();
        //    try
        //    {
        //        model.LeaveApplication.bitIsApprovalProcess = true;
        //        model.LeaveApplication.intAppStatusID = (int)Common.ApplicationStatus.Recommended;  // Alternate Recommend
        //        model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;
        //        model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
        //        model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
        //        model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

        //        //-------------------------------------------------------------------              
        //        objAppvLeaveInfo = model.LeaveApplication.ShallowCopy();
        //        //-------------------------------------------------------------------


        //        if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
        //        {
        //            model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
        //            model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
        //            model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
        //        }

        //        if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
        //        {
        //            model.LeaveApplication.fltSubmittedDuration = model.LeaveApplication.fltSubmittedDuration * LoginInfo.Current.fltOfficeTime;
        //            model.LeaveApplication.fltSubmittedWithPayDuration = model.LeaveApplication.fltSubmittedWithPayDuration * LoginInfo.Current.fltOfficeTime;
        //            model.LeaveApplication.fltSubmittedWithoutPayDuration = model.LeaveApplication.fltSubmittedWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
        //        }

        //        if (model.LeaveApplication.intApplicationID > 0)
        //        {

        //            i = objBll.AlternateRecommend(model.LeaveApplication, out strmsg);  //Alternate Recommend Process
        //            if (i >= 0)
        //            {

        //                ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
        //                i = objApprovalFlowBLL.ApprovalFlowGet(-1, model.LeaveApplication.intApplicationID, -1, "", LoginInfo.Current.strCompanyID).Max(c => c.intAppFlowID);

        //                try
        //                {
        //                    Common.SendMail(objAppvLeaveInfo, i);   // Send Mail to Next Author
        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return i;
        //}

        public int AlternateRecommend(LeaveApplicationModels model, out string strmsg)
        {
            int i = -1;
            ApprovalFlowBLL objBll = new ApprovalFlowBLL();
            ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();

            try
            {
                model.ApprovalFlow.intAppStatusID = (int)Utils.LeaveAppStatus.Recommend;
                model.ApprovalFlow.intApplicationID = model.LeaveApplication.intApplicationID;
                model.ApprovalFlow.strEUser = LoginInfo.Current.LoginName;
                model.ApprovalFlow.strIUser = LoginInfo.Current.LoginName;
                model.ApprovalFlow.strCompanyID = LoginInfo.Current.strCompanyID;

                model.ApprovalFlow.strDepartmentID = LoginInfo.Current.strDepartmentID;
                model.ApprovalFlow.strDesignationID = LoginInfo.Current.strDesignationID;

                model.ApprovalFlow.strApplicationType = model.LeaveApplication.strApplicationType;
                model.ApprovalFlow.dtApplyFromDate = model.LeaveApplication.dtApplyFromDate;
                model.ApprovalFlow.dtApplyToDate = model.LeaveApplication.dtApplyToDate;
                model.ApprovalFlow.strApplyFromTime = model.LeaveApplication.strApplyFromTime;
                model.ApprovalFlow.strApplyToTime = model.LeaveApplication.strApplyToTime;

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
                model.ApprovalFlow.strSourceAuthorID = LoginInfo.Current.strEmpID; //model.ApprovalFlow.strAuthorID;
                intNextNodeID = model.intNodeID;

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

                i = objBll.AlternateApproval(LoginInfo.Current.strEmpID, model.ApprovalFlow,out strmsg);
                
                if (i >= 0)
                {
                    if (intNextNodeID == 0)
                    {
                        i = model.ApprovalFlow.intAppFlowID;
                    }

                    // Block for Temporary for Tesing Purpose
                    /*
                    try
                    {
                        if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                        {
                            model.LeaveApplication.strSupervisorID = model.ApprovalFlow.strDestAuthorID;
                            model.LeaveApplication.intAppStatusID = model.ApprovalFlow.intAppStatusID;
                            model.LeaveApplication.strCompanyID = model.ApprovalFlow.strCompanyID;

                            Common.SendMail(model.LeaveApplication, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName);
                        }

                    }
                    catch (Exception ex)
                    { }
                    */
                    // End
                }
               
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }
        
        public int ALternateReject(LeaveApplicationModels model, out string strmsg)
        {
            int i = 0;
            strmsg = "";

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            LeaveApplication objRejLeaveInfo = new LeaveApplication();
            try
            {
                model.LeaveApplication.bitIsApprovalProcess = false;
                model.LeaveApplication.intAppStatusID = (int)Utils.LeaveAppStatus.Reject;  // Alternate Reject
                model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;
                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;
               
                //--------------------------------------------------------------------
                objRejLeaveInfo = model.LeaveApplication.ShallowCopy();
                //--------------------------------------------------------------------
                
                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltSubmittedDuration = model.LeaveApplication.fltSubmittedDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithPayDuration = model.LeaveApplication.fltSubmittedWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithoutPayDuration = model.LeaveApplication.fltSubmittedWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.intApplicationID > 0)
                {
                    i = objBll.AlternateReject(model.LeaveApplication, out strmsg);  //Alternate Approval Process

                    if (i >= 0)
                    {
                        ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                        
                        // Block for Temporary for Tesing Purpose
                        /*
                        try
                        {
                            if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                            {
                                Common.SendMail(objRejLeaveInfo, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName); // Send Mail to Applicant
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        */
                        // End
                    }
                }
            }
            catch (Exception ex)
            {
                i = -1;
                throw ex;
            }
            return i;
        }

        public int ALternateDelete(long Id, out string strmsg)
        {
            int i = 0;
            strmsg = "";

            try
            {
                i = objBLL.Delete(Id, out strmsg);
            }

            catch (Exception ex)
            {
                i = -1;
                throw ex;
            }

            return i;
        }

        public int SaveData(LeaveApplicationModels model, out string strmsg)
        {
            strmsg = "";
            int i = -1;

            LeaveApplicationBLL objBll = new LeaveApplicationBLL();
            LeaveApplication objLeaveInfo = new LeaveApplication();

            try
            {
                if (model.LeaveApplication.bitIsApprovalProcess == false)
                {
                    model.LeaveApplication.strSupervisorID = LoginInfo.Current.strEmpID;
                }

                model.LeaveApplication.strEUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strIUser = LoginInfo.Current.LoginName;
                model.LeaveApplication.strCompanyID = LoginInfo.Current.strCompanyID;

                //----------------------------------------------------------------------
                objLeaveInfo = model.LeaveApplication.ShallowCopy();
                //-----------------------------------------------------------------------

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltSubmittedDuration = model.LeaveApplication.fltSubmittedDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithPayDuration = model.LeaveApplication.fltSubmittedWithPayDuration * LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithoutPayDuration = model.LeaveApplication.fltSubmittedWithoutPayDuration * LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.intApplicationID > 0)
                {
                    if (model.LeaveApplication.intAppStatusID == 2)
                    {
                        i = objBll.Cancel(model.LeaveApplication, out strmsg);  // Cancel Leave
                    }
                    else if (model.LeaveApplication.intAppStatusID == 1 && model.LeaveApplication.bitIsApprovalProcess == true)
                    {
                        model.LeaveApplication.bitIsApprovalProcess = false;
                        i = objBll.Approve(model.LeaveApplication, out strmsg);  //Alternate Approval Process
                    }

                    if (i >= 0 && model.LeaveApplication.bitIsApprovalProcess == false && model.LeaveApplication.bitIsDiscard == true)
                    {

                        ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                        ApprovalFlow appFlow = objApprovalFlowBLL.ApprovalFlowGet(-1, model.LeaveApplication.intApplicationID, -1, "", LoginInfo.Current.strCompanyID).SingleOrDefault();
                        i = appFlow.intAppFlowID;
                        try
                        {
                            ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
                            //Check the submittion is for approval or recommander
                            if (objApprovalPathBLL.ApprovalPathDetailsGet(-1, appFlow.intNodeID, -1, LoginInfo.Current.strCompanyID).Count <= 0)
                            {
                                model.LeaveApplication.IsForApproval = true;

                            }
                        }
                        catch (Exception ex)
                        {
                            model.LeaveApplication.IsForApproval = false;                            
                        }

                        // Block for Temporary for Tesing Purpose
                        /*
                        try
                        {
                            if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                            {
                                Common.SendMail(objLeaveInfo, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        */
                        // End Block
                    }
                }
                else
                {

                    i = objBll.Add(model.LeaveApplication, LstLeaveLedger, out strmsg);

                    if (i > 0 && model.LeaveApplication.bitIsApprovalProcess == true)
                    {
                        ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
                        i = objApprovalFlowBLL.ApprovalFlowGet(-1, i, -1, "", LoginInfo.Current.strCompanyID).SingleOrDefault().intAppFlowID;

                        // Block for Temporary for Tesing Purpose
                        /*
                        try
                        {
                            if (LoginInfo.Current.EmailAddress.ToString().Length > 0)
                            {
                                Common.SendMail(objLeaveInfo, i, LoginInfo.Current.fltOfficeTime, LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        */
                        // End 
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Delete(int Id, out string strmsg)
        {
            strmsg = "";
            int i = -1;
            try
            {

                i = objBLL.Delete(Id, out strmsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
        
        public LeaveApplication GetLeaveApplication(int Id)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();

            try
            {
                model.LeaveApplication = objBLL.LeaveApplicationGet(Id);

                if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltSubmittedDuration = model.LeaveApplication.fltSubmittedDuration / LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithPayDuration = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltSubmittedWithoutPayDuration = model.LeaveApplication.fltSubmittedWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                }

                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;

                }

                return model.LeaveApplication;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ApprovalFlow> GetApprovalFlowList(int Id)
        {
            ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();
            return this.ApprovalFlowList = objApprovalFlowBLL.ApprovalFlowGet(-1, Id, -1, "", LoginInfo.Current.strCompanyID);
        }

        public void GetLeaveApplicationAll()
        {
            //lstLeaveApplication = objBLL.LeaveApplicationGetAll(LoginInfo.Current.strCompanyID);
        }

        public ApprovalFlow GetApprovalFlow(int intAppFlowID)
        {
            ApprovalFlowBLL objBLLAF = new ApprovalFlowBLL();
            return objBLLAF.ApprovalFlowGet(intAppFlowID);
        }

        public void GetApprovalFlowComments(long intApplicationID)
        {
            ApprovalCommentsBLL objBLLAF = new ApprovalCommentsBLL();
            LstApprovalComments = objBLLAF.ApprovalCommentsGet(-1, intApplicationID, -1, LoginInfo.Current.strCompanyID);
        }

        public List<LeaveApplication> GetEmployeeLeaveApplications(string strEmpID, int intLeaveYearID)
        {
            List<LeaveApplication> LstEmpLeaveApplications = new List<LeaveApplication>();

            LstEmpLeaveApplications = objBLL.EmployeeLeaveApplicationGet(strEmpID, intLeaveYearID, LoginInfo.Current.strCompanyID);

            return LstEmpLeaveApplications;
        }

        public List<LeaveApplication> GetEmployeeLeaveApplications(string strEmpID, int intLeaveYearID, bool bolIsAdjustment)
        {
            List<LeaveApplication> LstEmpLeaveApplications = new List<LeaveApplication>();

            LstEmpLeaveApplications = objBLL.EmployeeLeaveApplicationGet(strEmpID, intLeaveYearID, LoginInfo.Current.strCompanyID, 0, bolIsAdjustment);

            return LstEmpLeaveApplications;
        }

        public LeaveApplication GetEmployeeLastApprovedApplication(string strEmpID, int intLeaveYearID)
        {
            List<LeaveApplication> LstEmpApp = new List<LeaveApplication>();
            LeaveApplication EmpApvApp = new LeaveApplication();

            //LstEmpApp = objBLL.EmployeeLeaveApplicationGet(strEmpID, intLeaveYearID, LoginInfo.Current.strCompanyID, 1, false).OrderByDescending(c => c.dtApplyFromDate).ToList(); ;
            LstEmpApp = objBLL.EmployeeApprovedLeaveApplicationGet(strEmpID, intLeaveYearID, LoginInfo.Current.strCompanyID, false).OrderByDescending(c => c.ApproveDateTime).ToList(); ;
            
            if (LstEmpApp.Count > 0)
            {
                EmpApvApp = LstEmpApp[0];
                if (EmpApvApp.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    EmpApvApp.fltDuration = EmpApvApp.fltDuration / LoginInfo.Current.fltOfficeTime;
                    EmpApvApp.fltWithPayDuration = EmpApvApp.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    EmpApvApp.fltWithoutPayDuration = EmpApvApp.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                }
            }

            return EmpApvApp;
        }

        public IList<LeaveApplication> GetLeaveApplicationPaging(LeaveApplication objLeaveApplication, bool IsForRequestedLeave)
        {
            int Total = 0;
            if (IsForRequestedLeave == false)
            {
                lstLeaveApplication = objBLL.LeaveApplicationGet(objLeaveApplication.intApplicationID, objLeaveApplication.strEmpInitial,
                                                                objLeaveApplication.strEmpName, objLeaveApplication.intLeaveYearID,
                                                                objLeaveApplication.intLeaveTypeID, objLeaveApplication.strApplyFromDate,
                                                                objLeaveApplication.strApplyToDate, objLeaveApplication.strApplicationType,
                                                                objLeaveApplication.intAppStatusID, objLeaveApplication.strApprovalProcess,
                                                                objLeaveApplication.bitIsForAlternateProcess, LoginInfo.Current.strCompanyID,
                                                                objLeaveApplication.strDepartmentID, objLeaveApplication.strDesignationID,
                                                                objLeaveApplication.bitIsAdjustment, objLeaveApplication.ZoneId, strSortBy, strSortType, _startRowIndex, maximumRows, out Total);
            }
            else
            {
                //--block shaiful 17-Jan-2011
                //lstLeaveApplication = objBLL.RequestedLeaveApplicationGet(objLeaveApplication.intAppFlowID, objLeaveApplication.strEmpID, objLeaveApplication.strEmpName, objLeaveApplication.intLeaveYearID, objLeaveApplication.intLeaveTypeID, objLeaveApplication.strApplyFromDate, objLeaveApplication.strApplyToDate, objLeaveApplication.strApplicationType, objLeaveApplication.intAppStatusID, objLeaveApplication.bitIsDiscard, LoginInfo.Current.strCompanyID, objLeaveApplication.strDepartmentID, objLeaveApplication.strDesignationID, objLeaveApplication.strAuthorID, strSortBy, strSortType, _startRowIndex, maximumRows, out Total);           
            }
            numTotalRows = Total;

            return LstLeaveApplication;
        }

        //--add shaiful 30-Dec-2010
        public IList<LeaveApplication> GetLeaveApplicationPaging(LeaveApplication objLeaveApplication,
                                                                bool IsForRequestedLeave,
                                                                bool IsForLeaveSearch,
                                                                bool IsRequestedLeaveForBulkApprove)
        {

            int Total = 0;
            string strAppDirectionType = "";
            string strApplyFromDate = objLeaveApplication.strApplyFromDate;
            string strApplyToDate = objLeaveApplication.strApplyToDate;

            if (IsForLeaveSearch == false)
            {
                strApplyFromDate = "";
                strApplyToDate = "";
                strAppDirectionType = "Forward";
            }

            if (IsForRequestedLeave == false)
            {
                lstLeaveApplication = objBLL.LeaveApplicationGet(objLeaveApplication.intApplicationID, objLeaveApplication.strEmpInitial,
                                                                objLeaveApplication.strEmpName, objLeaveApplication.intLeaveYearID,
                                                                objLeaveApplication.intLeaveTypeID, strApplyFromDate,
                                                                strApplyToDate, objLeaveApplication.strApplicationType,
                                                                objLeaveApplication.intAppStatusID, objLeaveApplication.strApprovalProcess,
                                                                objLeaveApplication.bitIsDiscard, LoginInfo.Current.strCompanyID,
                                                                objLeaveApplication.strDepartmentID, objLeaveApplication.strDesignationID,
                                                                objLeaveApplication.bitIsAdjustment, objLeaveApplication.ZoneId, strSortBy, strSortType, _startRowIndex, maximumRows, out Total);
            }
            else
            {
                if (IsRequestedLeaveForBulkApprove == true)
                {

                    lstLeaveApplication = objBLL.RequestedLeaveGetForBulkApprove(objLeaveApplication.strAuthorID, objLeaveApplication.strEmpInitial,
                                                         objLeaveApplication.strEmpName, objLeaveApplication.intLeaveYearID,
                                                         objLeaveApplication.intLeaveTypeID, strApplyFromDate,
                                                         strApplyToDate, objLeaveApplication.strApplicationType,
                                                         objLeaveApplication.intAppStatusID, objLeaveApplication.bitIsDiscard,
                                                         LoginInfo.Current.strCompanyID, objLeaveApplication.strDepartmentID,
                                                         objLeaveApplication.strDesignationID, "Forward",
                                                         strSortBy, strSortType, _startRowIndex, maximumRows, out Total);

                    if (lstLeaveApplication.Count > 0)
                    {
                        for (int i = 0; i < lstLeaveApplication.Count; i++)
                        {
                            lstLeaveApplication[i].IsChecked = true;

                        }

                    }
                }
                else
                {
                    lstLeaveApplication = objBLL.RequestedLeaveApplicationGet(objLeaveApplication.intAppFlowID, objLeaveApplication.strEmpInitial,
                                                                            objLeaveApplication.strEmpName, objLeaveApplication.intLeaveYearID,
                                                                            objLeaveApplication.intLeaveTypeID, strApplyFromDate,
                                                                            strApplyToDate, objLeaveApplication.strApplicationType,
                                                                            objLeaveApplication.intAppStatusID, objLeaveApplication.bitIsDiscard,
                                                                            LoginInfo.Current.strCompanyID, objLeaveApplication.strDepartmentID,
                                                                            objLeaveApplication.strDesignationID, objLeaveApplication.strAuthorID, strAppDirectionType,
                                                                            strSortBy, strSortType, _startRowIndex, maximumRows, out Total);
                }

            }

            numTotalRows = Total;
            return LstLeaveApplication;
        }

        public List<LeaveLedger> GetLeaveStatus(LeaveApplication objLeaveApplication, bool IsForHistory)
        {
            if (IsForHistory == true)
            {
                objLeaveApplication.LstLeaveLedger = objBLL.GetLeaveLedgerHistory(objLeaveApplication.intApplicationID, objLeaveApplication.intLeaveYearID, objLeaveApplication.intLeaveTypeID, objLeaveApplication.fltWithPayDuration, objLeaveApplication.strEmpID, LoginInfo.Current.strCompanyID);

            }
            else
            {
                objLeaveApplication.LstLeaveLedger = objBLL.LeaveBalanceIndividualGet(objLeaveApplication.intApplicationID, objLeaveApplication.intLeaveYearID, objLeaveApplication.intLeaveTypeID, objLeaveApplication.fltWithPayDuration, objLeaveApplication.strEmpID, LoginInfo.Current.strCompanyID, objLeaveApplication.strApplicationType);
            }

            return objLeaveApplication.LstLeaveLedger.OrderBy(c => c.strLeaveType).ToList();
        }

        public List<ApprovalAuthor> GetApprovalAuthorStepsByAuthorID(string strAuthorID, int intAuthorTypeID)
        {
            ApprovalAuthorBLL objAppvAutBLL = new ApprovalAuthorBLL();
            List<ApprovalAuthor> objLstAuthor = new List<ApprovalAuthor>();
            objLstAuthor = objAppvAutBLL.GetApprovalAuthorSteps(strAuthorID, "", LoginInfo.Current.strCompanyID, intAuthorTypeID);
            return objLstAuthor;
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

        public LeaveYear GetLeaveYearByLeaveYearType(int LeaveYearTypeID,string companyID)
        {
            LeaveYearBLL objLvYearBLL = new LeaveYearBLL();

            try
            {
                return objLvYearBLL.LeaveYearGetActive(LeaveYearTypeID, companyID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Employee GetEmployeeInfo(string Id)
        {
            EmployeeBLL objEmpBLL = new EmployeeBLL();

            try
            {
                return objEmpBLL.EmployeeGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OfficeTimeDetails> GetWorkingTime()
        {
            OfficeTimeBLL objOffTimeBLL = new OfficeTimeBLL();
            return objOffTimeBLL.OfficeTimeDetailsGet(LoginInfo.Current.intLeaveYearID);
        }

        public bool IsLeaveTypeApplicable(LeaveApplicationModels model)
        {
            LeaveLedger ledg = new LeaveLedger();
            bool bolApp = false;

            if (model.LstLeaveLedger != null)
            {
                ledg = model.LstLeaveLedger.Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
                if (ledg != null)
                {
                    bolApp = true;
                }
            }
            return bolApp;
        }

        public SelectList GetEmployeeLeaveType(LeaveApplicationModels model)
        {
            this.LeaveApplication.strEmpID = model.LeaveApplication.strEmpID;
            return OffLineLeaveType;
        }

        public List<SelectListItem> GetApprovers(LeaveApplicationModels model)
        {
            this.intNodeID = model.intNodeID;
            this.EmpID = model.LeaveApplication.strEmpID; // Added For Central Approval System.
            return Approver;
        }

        public string EmpID { get; set; } // Added For Central Approval System.

        public Employee GetEmployeeInfoById(string employeeId)
        {
            EmployeeBLL objEmpBLL = new EmployeeBLL();

            try
            {
                return objEmpBLL.EmployeeGetByEmployeeId(employeeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}