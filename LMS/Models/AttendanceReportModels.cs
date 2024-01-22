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

namespace LMS.Web.Models
{
    public class AttendanceReportModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private string _Message;
        private string _ReportId;
        private int intDataCount;
        public int IntDataCount
        {
            get { return intDataCount; }
            set { intDataCount = value; }
        }       

        
        private string strEmpId;
        public string StrEmpId
        {
            get { return strEmpId; }
            set { strEmpId = value; }
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

        private string strCompanyID;
        public string StrCompanyID
        {
            get { return strCompanyID; }
            set { strCompanyID = value; }
        }      

        private string strDesignationId;
        public string StrDesignationId
        {
            get { return strDesignationId; }
            set { strDesignationId = value; }
        }      

        private string strLocationId;
        public string StrLocationId
        {
            get { return strLocationId; }
            set { strLocationId = value; }
        }

        private int _intCategoryCode;
        public int intCategoryCode
        {
            get { return _intCategoryCode; }
            set { _intCategoryCode = value; }
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

        
        public string DisplayFromDate
        {
            get { return Common.FormatDate(StrFromDate, "dd-MM-yyyy", "dd-MMM-yyyy"); }
        }
        
        public string DisplayToDate
        {
            get { return Common.FormatDate(StrToDate, "dd-MM-yyyy", "dd-MMM-yyyy"); }
        }

        private bool isIndividual;
        public bool IsIndividual
        {
            get { return isIndividual; }
            set { isIndividual = value; }
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
        
        private SelectList _Department;
        private SelectList _Designation;
        private SelectList _Gender;
        private SelectList _Location;
        private SelectList _ReportList;
        private SelectList _Category;

        private AttendanceReport _AttendanceReport;
        private IList<AttendanceReport> _LstAttendanceReport;
        private IPagedList<AttendanceReport> _LstAttendanceReportPaging;


        private AttendanceReportBLL objBLL = new AttendanceReportBLL();

        public AttendanceReport AttendanceReport
        {
            get
            {
                if (this._AttendanceReport == null)
                {
                    this._AttendanceReport = new AttendanceReport();
                }
                return _AttendanceReport;
            }
            set { _AttendanceReport = value; }
        }

        public IList<AttendanceReport> LstAttendanceReport
        {
            get
            {
                if (_LstAttendanceReport == null)
                {
                    _LstAttendanceReport = new List<AttendanceReport>();
                }
                return _LstAttendanceReport;
            }
            set { _LstAttendanceReport = value; }
        }

        public IPagedList<AttendanceReport> LstAttendanceReportPaging
        {
            get { return _LstAttendanceReportPaging; }
            set { _LstAttendanceReportPaging = value; }
        }
               
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public string strReportType
        {
            get { return _ReportId; }
            set { _ReportId = value; }
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

                lstDepartment = Common.fetchDepartment().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();

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

        private SelectList _Company;
        public SelectList Company
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Company> lstCompany = new List<Company>();

                lstCompany = Common.fetchCompany().ToList();

                foreach (Company lt in lstCompany)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strCompanyID;
                    item.Text = lt.strCompany;
                    itemList.Add(item);
                }
                this._Company = new SelectList(itemList, "Value", "Text");
                return _Company;
            }
            set { _Company = value; }
        }

        private SelectList _EmployeeCategory;
        public SelectList EmployeeCategory
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<EmployeeCategory> lstEmployeeCategory = new List<EmployeeCategory>();

                lstEmployeeCategory = Common.fetchEmployeeCategory().ToList();

                foreach (EmployeeCategory lt in lstEmployeeCategory)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intCategoryCode.ToString();
                    item.Text = lt.strCategory;
                    itemList.Add(item);
                }
                this._EmployeeCategory = new SelectList(itemList, "Value", "Text");
                return _EmployeeCategory;
            }
            set { _EmployeeCategory = value; }
        }

        private SelectList _Shift;
        public SelectList Shift
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Shift> lstShift = new List<Shift>();

                lstShift = Common.fetchShift().ToList();

                foreach (Shift lt in lstShift)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intShiftID.ToString();
                    item.Text = lt.strShiftName;
                    itemList.Add(item);
                }
                this._Shift = new SelectList(itemList, "Value", "Text");
                return _Shift;
            }
            set { _Shift = value; }
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
                List<SelectListItem> itemList = new List<SelectListItem>();

                itemList.Add(new SelectListItem() { Text = "Employee Attendance Status", Value = "EAS" });
                itemList.Add(new SelectListItem() { Text = "List of Present Employee", Value = "LPE" });
                itemList.Add(new SelectListItem() { Text = "List of Absent Employee", Value = "LAE" });
                itemList.Add(new SelectListItem() { Text = "List of OOD Employee", Value = "LOE" });
                itemList.Add(new SelectListItem() { Text = "List of Employee on Leave", Value = "LEOL" });
                itemList.Add(new SelectListItem() { Text = "List of Early Arrival Employee", Value = "LEAE" });
                itemList.Add(new SelectListItem() { Text = "List of Late Arrival Employee", Value = "LLAE" });
                itemList.Add(new SelectListItem() { Text = "List of Early Departure Employee", Value = "LEDE" });
                itemList.Add(new SelectListItem() { Text = "List of Late Departure Employee", Value = "LLDE" });
                itemList.Add(new SelectListItem() { Text = "Employee Status Summary", Value = "ESS" });
                itemList.Add(new SelectListItem() { Text = "Employee Job Card", Value = "EJC" });
                itemList.Add(new SelectListItem() { Text = "Employee Working Calender", Value = "EWC" });
                itemList.Add(new SelectListItem() { Text = "Out of Office Compare", Value = "OOC" });
               

                this._ReportList = new SelectList(itemList, "Value", "Text");
                return _ReportList;
            }
            set { _ReportList = value; }
        }
       
        
        public IList<AttendanceReport> GetAttendanceReport(AttendanceReportModels model)
        {
            int Total = 0;

            model.LstAttendanceReport = objBLL.AttendanceReportGetData(model.strReportType, model.StrEmpId, StrCompanyID,
                                                                         model.StrDepartmentId, model.StrDesignationId,
                                                                         model.StrLocationId, model.intCategoryCode,
                                                                         Common.FormatDate(model.strFromDate, "dd-MM-yyyy", "yyyy-MM-dd"),
                                                                         Common.FormatDate(model.strToDate, "dd-MM-yyyy", "yyyy-MM-dd"),
                                                                         model.strSortBy, model.strSortType, model.startRowIndex,
                                                                         model.maximumRows, out Total);
            model.numTotalRows = Total;            
            return model.LstAttendanceReport;
        }

        public string GetHourFromMinute(string minute)
        {
            string strHour = "";
            string strMin = "";
            if (!string.IsNullOrEmpty(minute) && minute.Trim() != "0")
            {
                strHour = (Convert.ToInt32(minute) / 60).ToString("00") + ":" ;
                strMin = (Convert.ToInt32(minute) % 60).ToString("00") ;
                return strHour + strMin;
            }
            return strHour;
        }

        
    }
}