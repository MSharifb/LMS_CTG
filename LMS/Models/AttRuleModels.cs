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
    public class AttRuleModels
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
        private SelectList _Category;
        public string _Message;

        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
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

        private ATT_tblRule _ATT_tblRule;
        private IPagedList<ATT_tblRule> _LstATT_tblRulePaging;
        private IList<ATT_tblRule> lstATT_tblRule;

        AttRuleBLL objBLL = new AttRuleBLL();

        private int _intSearchLeaveTypeId;
        public int intSearchLeaveTypeId
        {
            get { return _intSearchLeaveTypeId; }
            set { _intSearchLeaveTypeId = value; }
        }

       
        public ATT_tblRule ATT_tblRule
        {
            get
            {
                if (this._ATT_tblRule == null)
                {
                    this._ATT_tblRule = new ATT_tblRule();
                }
                return _ATT_tblRule;
            }
            set { _ATT_tblRule = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<ATT_tblRule> LstATT_tblRule
        {
            get { return lstATT_tblRule; }
            set { lstATT_tblRule = value; }
        }
        public IPagedList<ATT_tblRule> LstATT_tblRulePaging
        {
            get { return _LstATT_tblRulePaging; }
            set { _LstATT_tblRulePaging = value; }
        }               

        public int SaveData(AttRuleModels model)
        {
            int returnid = 0;

            try
            {
                model.ATT_tblRule.strEUser = LoginInfo.Current.LoginName;
                if (model.ATT_tblRule.intRuleID > 0)
                {

                    returnid = objBLL.Edit(model.ATT_tblRule);
                }
                else
                {
                    model.ATT_tblRule.strIUser = LoginInfo.Current.LoginName;
                    //model.ATT_tblRule.strCompanyID = LoginInfo.Current.strCompanyID;

                    returnid = objBLL.Add(model.ATT_tblRule);

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

        public ATT_tblRule Get(int Id)
        {
            AttRuleModels model = new AttRuleModels();

            try
            {

                return model.ATT_tblRule = objBLL.GetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get(int Id, string dtEffectiveDate)
        {
            lstATT_tblRule = objBLL.Get(Id, dtEffectiveDate);
        }

        public List<ATT_tblRule> GetATT_tblRulePaging(ATT_tblRule obj)
        {
            return objBLL.Get(obj.intRuleID, obj.dtEffectiveDate.ToString());
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
       
    }
}