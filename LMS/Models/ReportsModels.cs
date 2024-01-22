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
using MvcPaging;
using System.Data;

namespace LMS.Web.Models
{
    public class ReportsModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private string _Message;
        private string _ReportId;
        private int intDataCount;
        private int _empStatus;

        public int EmpStatus
        {
            get { return _empStatus; }
            set { _empStatus = value; }
        }
        public int IntDataCount
        {
            get { return intDataCount; }
            set { intDataCount = value; }
        }


        private int intCategoryId;
        public int IntCategoryId
        {
            get { return intCategoryId; }
            set { intCategoryId = value; }
        }


        private int intLeaveYearId;
        public int IntLeaveYearId
        {
            get { return intLeaveYearId; }
            set { intLeaveYearId = value; }
        }

        private int intLeaveTypeId;
        public int IntLeaveTypeId
        {
            get { return intLeaveTypeId; }
            set { intLeaveTypeId = value; }
        }

        private string strEmpId;
        public string StrEmpId
        {
            get { return strEmpId; }
            set { strEmpId = value; }
        }

        private bool _isActiveLeaveYear;

        public bool IsActiveLeaveYear
        {
            get { return _isActiveLeaveYear; }
            set { _isActiveLeaveYear = value; }
        }

        private string strEmpName;
        public string StrEmpName
        {
            get { return strEmpName; }
            set { strEmpName = value; }
        }
        public string strSortBy
        {
            get { return _strSortBy; }
            set { _strSortBy = value; }
        }
        public int startRowIndex
        {
            get { return _startRowIndex; }
            set { _startRowIndex = value; }
        }
        public string strSortType
        {
            get { return _strSortType; }
            set { _strSortType = value; }
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
        private string strDepartmentId;
        public string StrDepartmentId
        {
            get { return strDepartmentId; }
            set { strDepartmentId = value; }
        }

        private string strDesignationId;
        public string StrDesignationId
        {
            get { return strDesignationId; }
            set { strDesignationId = value; }
        }

        private string strGender;
        public string StrGender
        {
            get { return strGender; }
            set { strGender = value; }
        }

        private string strLocationId;
        public string StrLocationId
        {
            get { return strLocationId; }
            set { strLocationId = value; }
        }

        private string strFromDate;
        public string StrFromDate
        {
            get { return strFromDate; }
            set { strFromDate = value; }
        }

        private string strToDate;
        public string StrToDate
        {
            get { return strToDate; }
            set { strToDate = value; }
        }


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

        private bool isIndividual;
        public bool IsIndividual
        {
            get { return isIndividual; }
            set { isIndividual = value; }
        }

        private bool isServiceLifeType;
        public bool IsServiceLifeType
        {
            get { return isServiceLifeType; }
            set { isServiceLifeType = value; }
        }

        private bool _IsFromMyLeaveMenu;
        public bool IsFromMyLeaveMenu
        {
            get { return _IsFromMyLeaveMenu; }
            set { _IsFromMyLeaveMenu = value; }
        }

        private bool _IsWithoutPay;
        public bool IsWithoutPay
        {
            get { return _IsWithoutPay; }
            set { _IsWithoutPay = value; }
        }

        private bool _IsApplyDate;
        public bool IsApplyDate
        {
            get { return _IsApplyDate; }
            set { _IsApplyDate = value; }
        }

        private bool _bitIsExcel;
        public bool bitIsExcel
        {
            get
            {
                if (_bitIsExcel == null)
                {
                    _bitIsExcel = false;
                }
                return _bitIsExcel;
            }
            set { _bitIsExcel = value; }
        }

        private SelectList _LeaveYear;
        private SelectList _LeaveType;
        private SelectList _LeaveYearType;
        private SelectList _Department;
        private SelectList _Designation;
        private SelectList _Gender;
        private SelectList _Location;
        private SelectList _ReportList;
        private SelectList _Category;

        private rptLeaveEncasment _RptLeaveEncasment;
        private IList<rptLeaveEncasment> _LstRptLeaveEncasment;
        private IPagedList<rptLeaveEncasment> _LstRptLeaveEncasmentPaging;

        private rptLeaveStatus _RptLeaveStatus;
        private IList<rptLeaveStatus> _LstRptLeaveStatus;
        private IPagedList<rptLeaveStatus> _LstRptLeaveStatusPaging;

        private IList<rptCompanyInformation> _rptCompanyInformation;
        public IList<rptCompanyInformation> RptCompanyInformation
        {
            get
            {
                if (_rptCompanyInformation == null)
                {
                    _rptCompanyInformation = new List<rptCompanyInformation>();
                }
                return _rptCompanyInformation;
            }
            set { _rptCompanyInformation = value; }
        }

        private rptLeaveEnjoyed _RptLeaveEnjoyed;
        private IList<rptLeaveEnjoyed> _LstRptLeaveEnjoyed;
        private IPagedList<rptLeaveEnjoyed> _LstRptLeaveEnjoyedPaging;

        private ReportsBLL objBLL = new ReportsBLL();

        public rptLeaveEncasment RptLeaveEncasment
        {
            get
            {
                if (this._RptLeaveEncasment == null)
                {
                    this._RptLeaveEncasment = new rptLeaveEncasment();
                }
                return _RptLeaveEncasment;
            }
            set { _RptLeaveEncasment = value; }
        }

        public IList<rptLeaveEncasment> LstRptLeaveEncasment
        {
            get
            {
                if (_LstRptLeaveEncasment == null)
                {
                    _LstRptLeaveEncasment = new List<rptLeaveEncasment>();
                }
                return _LstRptLeaveEncasment;
            }
            set { _LstRptLeaveEncasment = value; }
        }

        public IPagedList<rptLeaveEncasment> LstRptLeaveEncasmentPaging
        {
            get { return _LstRptLeaveEncasmentPaging; }
            set { _LstRptLeaveEncasmentPaging = value; }
        }

        public rptLeaveStatus RptLeaveStatus
        {
            get
            {
                if (_RptLeaveStatus == null)
                {
                    _RptLeaveStatus = new rptLeaveStatus();
                }
                return _RptLeaveStatus;
            }
            set { _RptLeaveStatus = value; }
        }

        public IList<rptLeaveStatus> LstRptLeaveStatus
        {
            get
            {
                if (_LstRptLeaveStatus == null)
                {
                    _LstRptLeaveStatus = new List<rptLeaveStatus>();
                }
                return _LstRptLeaveStatus;
            }
            set { _LstRptLeaveStatus = value; }
        }

        public IPagedList<rptLeaveStatus> LstRptLeaveStatusPaging
        {
            get { return _LstRptLeaveStatusPaging; }
            set { _LstRptLeaveStatusPaging = value; }
        }

        public rptLeaveEnjoyed RptLeaveEnjoyed
        {
            get { return _RptLeaveEnjoyed; }
            set { _RptLeaveEnjoyed = value; }
        }

        public IList<rptLeaveEnjoyed> LstRptLeaveEnjoyed
        {
            get { return _LstRptLeaveEnjoyed; }
            set { _LstRptLeaveEnjoyed = value; }
        }

        public IPagedList<rptLeaveEnjoyed> LstRptLeaveEnjoyedPaging
        {
            get { return _LstRptLeaveEnjoyedPaging; }
            set { _LstRptLeaveEnjoyedPaging = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public string ReportId
        {
            get { return _ReportId; }
            set { _ReportId = value; }
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

        public SelectList LeaveYearType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<LeaveYearType> lstLeaveYear = new List<LeaveYearType>();

                lstLeaveYear = Common.fetchLeaveYearType();

                foreach (LeaveYearType lt in lstLeaveYear)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.intLeaveYearTypeId.ToString();
                    item.Text = lt.LeaveYearTypeName;
                    itemList.Add(item);
                }
                this._LeaveYearType = new SelectList(itemList, "Value", "Text", LoginInfo.Current.intLeaveYearID);

                return _LeaveYearType;
            }
            set { _LeaveYearType = value; }
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
                this._Designation = new SelectList(itemList, "Value", "Text");
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

                lstDepartment = Common.fetchDepartment().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID && c.ZoneInfoId == LoginInfo.Current.LoggedZoneId).ToList();

                foreach (Department lt in lstDepartment)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strDepartmentID;
                    item.Text = lt.strDepartment;
                    itemList.Add(item);
                }
                this._Department = new SelectList(itemList, "Value", "Text");
                return _Department;
            }
            set { _Department = value; }
        }

        public SelectList Category
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<EmployeeCategory> lstCategory = new List<EmployeeCategory>();

                lstCategory = Common.fetchEmployeeCategory().ToList();

                foreach (EmployeeCategory lt in lstCategory)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intCategoryCode.ToString();
                    item.Text = lt.strCategory;
                    itemList.Add(item);
                }
                this._Category = new SelectList(itemList, "Value", "Text");
                return _Category;
            }
            set { _Category = value; }
        }

        public SelectList Zones
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Zone> lstCategory = new List<Zone>();

                lstCategory = Common.fetchZone().ToList();

                foreach (Zone lt in lstCategory)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strZoneID.ToString();
                    item.Text = lt.strZone;
                    itemList.Add(item);
                }
                this._Category = new SelectList(itemList, "Value", "Text");
                return _Category;
            }
            set { _Category = value; }
        }

        public SelectList Gender
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                SelectListItem item = new SelectListItem();
                item.Value = "Male";
                item.Text = "Male";
                itemList.Add(item);
                item = new SelectListItem();

                item.Value = "Female";
                item.Text = "Female";
                itemList.Add(item);

                this._Gender = new SelectList(itemList, "Value", "Text");
                return _Gender;
            }
            set { _Gender = value; }
        }


        public SelectList Location
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Location> lstLocation = new List<Location>();

                lstLocation = Common.fetchLocation().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();

                foreach (Location lt in lstLocation)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strLocationID;
                    item.Text = lt.strLocation;
                    itemList.Add(item);
                }
                this._Location = new SelectList(itemList, "Value", "Text");
                return _Location;
            }
            set { _Location = value; }
        }


        public SelectList ReportList
        {
            get
            {
                string strReportName = "Leave Status,Leave Availed,Leave Encashment,Leave Register,Recreation Leave,Office Order For Recreation Leave,Yearly Leave Status";

                string[] parts = strReportName.Split(new char[] { ',' });
                List<SelectListItem> itemList = new List<SelectListItem>();

                for (int i = 0; i < parts.Count(); i++)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = parts[i].ToString();
                    item.Text = parts[i].ToString();
                    itemList.Add(item);
                }

                this._ReportList = new SelectList(itemList, "Value", "Text");
                return _ReportList;
            }
            set { _ReportList = value; }
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

        public SelectList LeaveTypeEncashable
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<LeaveType> lstLeaveType = new List<LeaveType>();

                lstLeaveType = Common.fetchLeaveType().Where(x=>x.bitIsEncashable==true).ToList();

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

        public SelectList EmployeeStatus
        {
            get
            {
                string[] status = { "Active", "Inactive" };

                List<SelectListItem> lstItem = new List<SelectListItem>();

                for (int i = 0; i < status.Length; i++)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = status[i];
                    item.Value = (i+1).ToString();
                    lstItem.Add(item);
                }

                return new SelectList(lstItem, "Value", "Text");
            }
           
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

        public IList<rptLeaveEncasment> GetLeaveEncasment(ReportsModels model)
        {
            int Total = 0;

            model.LstRptLeaveEncasment = objBLL.RptLeaveEncasmentGetData(model.IntLeaveYearId, model.IsActiveLeaveYear,model.IsServiceLifeType, LoginInfo.Current.strCompanyID, model.StrEmpId, (model.EmpStatus - 1),
                                                                         model.StrDepartmentId, model.StrDesignationId, model.StrGender,
                                                                         model.StrLocationId, model.IntCategoryId, model.IntLeaveTypeId, model.ZoneId,
                                                                         model.strSortBy, model.strSortType, model.startRowIndex,
                                                                         model.maximumRows, out Total);
            model.numTotalRows = Total;

            return model.LstRptLeaveEncasment;
        }

        public IList<rptLeaveStatus> GetLeaveStatus(ReportsModels model)
        {
            int Total = 0;
           
            //if (model.IsFromMyLeaveMenu == true && model.IsIndividual == false)
            //{
            //    model.LstRptLeaveStatus = objBLL.RptSubordinateLeaveStatusItemList(model.IntLeaveYearId, LoginInfo.Current.strCompanyID, model.StrEmpId,
            //                                                           model.strSortBy, model.strSortType, model.startRowIndex, model.maximumRows, out Total);
            //}
            //else
            //{
                model.LstRptLeaveStatus = objBLL.RptLeaveStatusGetData(model.IntLeaveYearId, model.IsActiveLeaveYear, model.IsServiceLifeType, LoginInfo.Current.strCompanyID, model.StrEmpId, (model.EmpStatus - 1),
                                                                       model.StrDepartmentId, model.StrDesignationId, model.StrGender,
                                                                       model.StrLocationId, model.IntCategoryId, model.IntLeaveTypeId, model.ZoneId,
                                                                       model.strSortBy, model.strSortType, model.startRowIndex,
                                                                       model.maximumRows, out Total);
            //}
            model.numTotalRows = Total;

            return model.LstRptLeaveStatus;

        }


        public IList<rptLeaveEnjoyed> GetLeaveEnjoyed(ReportsModels model)
        {
            int Total = 0;

            model.LstRptLeaveEnjoyed = objBLL.RptLeaveEnjoyedGetData(model.IntLeaveYearId, model.IsServiceLifeType, LoginInfo.Current.strCompanyID, model.StrEmpId, (model.EmpStatus - 1),
                                                                    model.StrDepartmentId, model.StrDesignationId, model.StrGender,
                                                                    model.StrLocationId, model.StrFromDate.ToString(), model.StrToDate.ToString(),
                                                                    model.IntCategoryId, model.IntLeaveTypeId, model.IsWithoutPay, model.IsApplyDate, model.ZoneId,
                                                                    model.strSortBy, model.strSortType, model.startRowIndex, model.maximumRows, out Total);
            model.numTotalRows = Total;

            return model.LstRptLeaveEnjoyed;
        }
        public DataSet GetLeaveRegister(ReportsModels model)
        {
            return objBLL.RptLeaveRegisterGetData(model.StrEmpId);
        }

        public DataSet GetYearlyLeaveStatus(ReportsModels model)
        {
            return objBLL.RptYearlyLeaveStatusGetData(model.StrEmpId, (model.EmpStatus - 1), model.StrDepartmentId, model.StrDesignationId, model.StrGender, model.IntCategoryId, model.ZoneId);
        }

        public DataSet GetLeaveApplicationInfo(int applicationId)
        {
            return objBLL.RptLeaveApplicationInfo(applicationId);
        }


        public IList<rptCompanyInformation> GetCompanyInformation()
        {
            ReportsModels model=new ReportsModels();
            return model.RptCompanyInformation= objBLL.GetCompanyInformations();
 
        }

        public IList<rptCompanyInformation> GetZoneInformation(int loggedZoneId)
        {
            ReportsModels model = new ReportsModels();
            return model.RptCompanyInformation = objBLL.GetZoneInformations(loggedZoneId);

        }


        public DataSet GetRecreationLeave(ReportsModels model)
        {
            return objBLL.RptRecreationLeaveGetData(model.IntLeaveYearId,model.IsActiveLeaveYear);
        }

        public DataSet GetRecreationLeaveOfficeOrder(ReportsModels obj, int LoggedZoneId)
        {
            return objBLL.RptRecreationLeaveOfficeOrderGetData(obj.IntLeaveYearId, obj.IsActiveLeaveYear, obj.StrFromDate, obj.StrToDate, obj.IsApplyDate, LoggedZoneId);
        }

        public int ZoneId { get; set; }
    }
}