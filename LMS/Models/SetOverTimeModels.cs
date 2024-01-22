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
    public class SetOverTimeModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;         

        private SetOverTime _SetOverTime;
        private IPagedList<SetOverTime> _LstSetOverTimePaging;
        private IList<SetOverTime> lstSetOverTime;

        SetOverTimeBLL objBLL = new SetOverTimeBLL();

        #region dropdown lists
        
        private SelectList _Designation;
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

        private SelectList _Department;
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

        private SelectList _Location;
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
                    item.Value = lt.strLocationID.ToString();
                    item.Text = lt.strLocation;
                    itemList.Add(item);
                }
                this._Location = new SelectList(itemList, "Value", "Text");
                return _Location;
            }
            set { _Location = value; }
        }
              

        #endregion

        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        private int _EntryType;
        public int EntryType { 
            get { return _EntryType;} 
            set { _EntryType = value;} 
        }
        public SetOverTime SetOverTime
        {
            get
            {
                if (this._SetOverTime == null)
                {
                    this._SetOverTime = new SetOverTime();
                }
                return _SetOverTime;
            }
            set { _SetOverTime = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<SetOverTime> LstSetOverTime
        {
            get { return lstSetOverTime; }
            set { lstSetOverTime = value; }
        }
        public IPagedList<SetOverTime> LstSetOverTimePaging
        {
            get { return _LstSetOverTimePaging; }
            set { _LstSetOverTimePaging = value; }
        }               

        public int SaveData(SetOverTimeModels model)
        {
            int returnid = 0;

            try
            {
                if (model.SetOverTime.EntryType == 1)
                {
                    model.SetOverTime.strEmpID = null;
                    model.SetOverTime.strEmpName = null;
                }
                else if (model.SetOverTime.EntryType == 2)
                {
                    model.SetOverTime.strCompanyID = model.SetOverTime.strDesignationID = null;
                    model.SetOverTime.strLocationID = model.SetOverTime.strDepartmentID = null;
                    model.SetOverTime.intCategoryCode = 0;
                }


                model.SetOverTime.bitFromJoiningDate = !model.SetOverTime.bitFromConfirmationDate;
                
                model.SetOverTime.strEUser = LoginInfo.Current.LoginName;
                if (model.SetOverTime.intRowID > 0)
                {

                    returnid = objBLL.Edit(model.SetOverTime);
                }
                else
                {
                    model.SetOverTime.strIUser = LoginInfo.Current.LoginName;

                    returnid = objBLL.Add(model.SetOverTime);

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

        public SetOverTime Get(int Id)
        {
            SetOverTimeModels model = new SetOverTimeModels();

            try
            {

                return model.SetOverTime = objBLL.GetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get(int intRowID, string strEmpID, string strCompanyID, string strLocationID,
            string strDesignationID, string strDepartmentID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo)
        {
            lstSetOverTime = objBLL.Get(intRowID, strEmpID, strCompanyID, strLocationID, strDesignationID, strDepartmentID, intCategoryCode,
                Common.FormatDate(dtPeriodFrom, "dd-MM-yyyy", "yyyy-MM-dd"), Common.FormatDate(dtPeriodTo, "dd-MM-yyyy", "yyyy-MM-dd"));
        }

        public List<SetOverTime> GetSetOverTimePaging(SetOverTime obj)
        {
            return objBLL.Get(obj.intRowID, obj.strEmpID, obj.strCompanyID, obj.strLocationID, obj.strDesignationID, obj.strDepartmentID, obj.intCategoryCode, 
                Common.FormatDate(obj.strPeriodFrom, "dd-MM-yyyy", "yyyy-MM-dd"), Common.FormatDate(obj.strPeriodTo, "dd-MM-yyyy", "yyyy-MM-dd"));
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