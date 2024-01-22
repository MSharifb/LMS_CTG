using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.BLL;
using LMSEntity;
using System.Data;
using MvcPaging;
using System.Web.Mvc;
using MvcContrib.Pagination;
using LMS.DAL;

namespace LMS.Web.Models
{
    public class ShiftAssignmentModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private SelectList _Department;
        private SelectList _Designation;
        private SelectList _Gender;
        private SelectList _Location;
        private SelectList _ReportList;
        private SelectList _Company;
        private SelectList _EmployeeCategory;

        public string _Message;         

        private ShiftAssignment _ShiftAssignment;
        private IPagedList<ShiftAssignment> _LstShiftAssignmentPaging;
        private IList<ShiftAssignment> lstShiftAssignment;

        ShiftAssignmentBLL objBLL = new ShiftAssignmentBLL();

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
        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }
        
        //private string strEmpId;
        //public string StrEmpId
        //{
        //    get { return strEmpId; }
        //    set { strEmpId = value; }
        //}
        
        //private string strEmpName;
        //public string StrEmpName
        //{
        //    get { return strEmpName; }
        //    set { strEmpName = value; }
        //}
        
        //private string strDepartmentId;
        //public string StrDepartmentId
        //{
        //    get { return strDepartmentId; }
        //    set { strDepartmentId = value; }
        //}

        //private string strCompanyID;
        //public string StrCompanyID
        //{
        //    get { return strCompanyID; }
        //    set { strCompanyID = value; }
        //}

        //private string strDesignationId;
        //public string StrDesignationId
        //{
        //    get { return strDesignationId; }
        //    set { strDesignationId = value; }
        //}

        //private string strLocationId;
        //public string StrLocationId
        //{
        //    get { return strLocationId; }
        //    set { strLocationId = value; }
        //}

        //private int _intCategoryCode;
        //public int intCategoryCode
        //{
        //    get { return _intCategoryCode; }
        //    set { _intCategoryCode = value; }
        //}
       
        private bool isIndividual;
        public bool IsIndividual
        {
            get { return isIndividual; }
            set { isIndividual = value; }
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
                this._EmployeeCategory = new SelectList(itemList, "Value", "Text");
                return _EmployeeCategory;
            }
            set { _EmployeeCategory = value; }
        }
                
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



        public ShiftAssignment ShiftAssignment
        {
            get
            {
                if (this._ShiftAssignment == null)
                {
                    this._ShiftAssignment = new ShiftAssignment();
                }
                return _ShiftAssignment;
            }
            set { _ShiftAssignment = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<ShiftAssignment> LstShiftAssignment
        {
            get { return lstShiftAssignment; }
            set { lstShiftAssignment = value; }
        }
        public IPagedList<ShiftAssignment> LstShiftAssignmentPaging
        {
            get { return _LstShiftAssignmentPaging; }
            set { _LstShiftAssignmentPaging = value; }
        }               

        public int SaveData(ShiftAssignmentModels model)
        {
            int returnid = 0;

            try
            {
                model.ShiftAssignment.strEUser = LoginInfo.Current.LoginName;
                if (model.ShiftAssignment.intShiftAssignmentID > 0)
                {

                    returnid = objBLL.Edit(model.ShiftAssignment);
                }
                else
                {
                    model.ShiftAssignment.strIUser = LoginInfo.Current.LoginName;

                    returnid = objBLL.Add(model.ShiftAssignment);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return returnid;

        }


        public int Delete(int Id)
        {

            int returnid = 0;

            try
            {

                returnid = objBLL.Delete(Id);
            }

            catch (Exception ex)
            {

                throw ex;
            }

            return returnid;
        }               

        public ShiftAssignment Get(int Id)
        {
            ShiftAssignmentModels model = new ShiftAssignmentModels();

            try
            {

                return model.ShiftAssignment = objBLL.GetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get(int Id, string strEmpID, int intShiftID, string dtEffectiveDate)
        {
            lstShiftAssignment = objBLL.Get(Id, strEmpID,intShiftID,dtEffectiveDate);
        }

        public List<ShiftAssignment> GetShiftAssignmentPaging(ShiftAssignment obj)
        {
            return objBLL.Get(obj.intShiftAssignmentID, obj.strEmpID, obj.intShiftID ,obj.strEffectiveDate);
        }

        #region employee info
        
        string employeeName;
        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }
        string strEmpID;
        public string StrEmpID
        {
            get { return strEmpID; }
            set { strEmpID = value; }
        }
        private string strDesignation;
        public string StrDesignation
        {
            get { return strDesignation; }
            set { strDesignation = value; }
        }

        private string _strDepartment;
        public string StrDepartment
        {
            get { return _strDepartment; }
            set { _strDepartment = value; }
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

        #endregion

    }
}