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
using LMS.Web.ViewModels.Shared;
using MvcPaging;

namespace LMS.Web.Models
{
    public class EmployeeModels
    {
        private string _strSearchInitial;
        private string _strSearchName;
        private string _strSearchStatus;

        private string _strSortBy;
        private string _strSortType;
        private int _startRowIndex;
        private int _maximumRows;
        private int _numTotalRows;

        private Employee _Employee;
        private SelectList _Department;
        private SelectList _Status;
        private SelectList _Zone;
        private string currentFocusID;

       
        public string strSearchDepartmentId { get; set; }

        public string strSearchZoneId { get; set; }


        public string strSearchStatus
        {
            get
            {
                if (this._strSearchStatus == null)
                {
                    this._strSearchStatus = "Active";
                }
                return _strSearchStatus;
            }
            set { _strSearchStatus = value; }

        }

        public string CurrentFocusID
        {
            get { return currentFocusID; }
            set { currentFocusID = value; }
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
        public Employee Employee
        {
            get
            {
                if (this._Employee == null)
                {
                    this._Employee = new Employee();
                }
                return _Employee;
            }
            set { _Employee = value; }
        }

        public IQueryable<Employee> GetQueryable()
        {
            return this.Employees.AsQueryable();
        }
        private IList<Employee> _Employees;

        public string strSearchInitial
        {
            get { return _strSearchInitial; }
            set { _strSearchInitial = value; }
        }

        public string strSearchName
        {
            get { return _strSearchName; }
            set { _strSearchName = value; }
        }

        private IPagedList<Employee> _LstEmployeesPaging;
        public IPagedList<Employee> LstEmployeesPaging
        {
            get { return _LstEmployeesPaging; }
            set { _LstEmployeesPaging = value; }
        }

        public IList<Employee> Employees
        {
            get
            {
                if (_Employees == null)
                {
                    _Employees = new List<Employee>();
                }
                return _Employees;
            }
            set { _Employees = value; }
        }

        public IList<Employee> GetEmployeeData(Employee objEmployee)
        {

            BLL.EmployeeBLL objBLL = new EmployeeBLL();
            int numTotalRows1 = 0;

            Employees = objBLL.EmployeeGet(objEmployee.strEmpInitial, objEmployee.strEmpID, objEmployee.strEmpName, objEmployee.ActiveStatus, objEmployee.strDepartmentID, objEmployee.strDesignationID, objEmployee.strDesignation, objEmployee.strGender, objEmployee.strReligionID, objEmployee.ZoneId.ToString(), objEmployee.strSearchType, strSortBy, strSortType, startRowIndex, maximumRows, out numTotalRows1);

            numTotalRows = numTotalRows1;

            return Employees;
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

        public SelectList Zone
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<Zone> lstZone = new List<Zone>();
                lstZone = Common.fetchZone().ToList();

                foreach (Zone lt in lstZone)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strZoneID;
                    item.Text = lt.strZone;
                    itemList.Add(item);
                }
                this._Zone = new SelectList(itemList, "Value", "Text");
                return _Zone;
            }
            set { _Zone = value; }
        }

        public SelectList Status
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                SelectListItem item = new SelectListItem();
                item.Value = "Active";
                item.Text = "Active";
                itemList.Add(item);

                item = new SelectListItem();
                item.Value = "Inactive";
                item.Text = "Inactive";
                itemList.Add(item);

                item = new SelectListItem();
                item.Value = "All";
                item.Text = "All";
                itemList.Add(item);

                this._Status = new SelectList(itemList, "Value", "Text");
                return _Status;
            }
            set { _Status = value; }
        }
    }
}
