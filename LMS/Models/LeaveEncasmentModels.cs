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
    public class LeaveEncasmentModels
    {
        private string _Message;
        private string _strSearchID;
        private string _strSearchName;

        private SelectList _LeaveYear;
        private SelectList _PaymentYear;
        private SelectList _PaymentMonth;
        private SelectList _LeaveType;
        private SelectList _Designation;
        private SelectList _Department;
        private SelectList _Location;

        private LeaveYear _ActiveLeaveYear;
        private LeaveEncasment _LeaveEncasment;
        private List<LeaveType> _LeaveTypeList;
        private IList<LeaveEncasment> _LstLeaveEncasment;
        private IPagedList<LeaveEncasment> _LstLeaveEncasmentPaging;

        private LeaveEncasmentBLL objBLL = new LeaveEncasmentBLL();
        private LeaveLedgerBLL objLvLedgerBLL = new LeaveLedgerBLL();
        private LeaveRuleBLL objLvRuleBLL = new LeaveRuleBLL();

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public string strSearchID
        {
            get { return _strSearchID; }
            set { _strSearchID = value; }
        }

        public string strSearchName
        {
            get { return _strSearchName; }
            set { _strSearchName = value; }
        }

        private string strYearStartDate;
        public string StrYearStartDate
        {
            get { return strYearStartDate; }
            set { strYearStartDate = value; }
        }

        public LeaveYear ActiveLeaveYear
        {
            get { return _ActiveLeaveYear; }
            set { _ActiveLeaveYear = value; }
        }

        public LeaveEncasment LeaveEncasment
        {
            get
            {
                if (_LeaveEncasment == null)
                {
                    _LeaveEncasment = new LeaveEncasment();
                }
                return _LeaveEncasment;
            }
            set { _LeaveEncasment = value; }
        }



        public List<LeaveType> LeaveTypeList
        {
            get { return _LeaveTypeList; }
            set { _LeaveTypeList = value; }
        }

        public IList<LeaveEncasment> LstLeaveEncasment
        {
            get
            {
                if (_LstLeaveEncasment == null)
                {
                    _LstLeaveEncasment = new List<LeaveEncasment>();
                }
                return _LstLeaveEncasment;
            }
            set { _LstLeaveEncasment = value; }
        }

        public IPagedList<LeaveEncasment> LstLeaveEncasmentPaging
        {
            get { return _LstLeaveEncasmentPaging; }
            set { _LstLeaveEncasmentPaging = value; }
        }

        public int SaveData(LeaveEncasmentModels model, ref string strmsg)
        {
            int i = 0;
            try
            {
                model.LeaveEncasment.strEUser = LoginInfo.Current.LoginName;
                model.LeaveEncasment.strCompanyID = LoginInfo.Current.strCompanyID;

                //if (model.LeaveEncasment.intLeaveEncaseID > 0) Changed By Md. Rokanuzzaman Sikder on 14th Jan 2012
                if (model.LeaveEncasment.intLeaveEncaseMasterID > 0)
                {
                    i = objBLL.Edit(model.LeaveEncasment, ref strmsg);
                }
                else
                {
                    model.LeaveEncasment.strIUser = LoginInfo.Current.LoginName;

                    i = objBLL.Add(model.LeaveEncasment, ref strmsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public int Delete(LeaveEncasmentModels model)
        {
            int i = 0;
            try
            {
                model.LeaveEncasment.strCompanyID = LoginInfo.Current.strCompanyID;
                i = objBLL.Delete(model.LeaveEncasment);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public LeaveEncasment GetLeaveEncasment(int Id)
        {
            LeaveEncasmentModels model = new LeaveEncasmentModels();

            try
            {
                return model.LeaveEncasment = objBLL.LeaveEncasmentGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetLeaveEncasmentAll()
        {
            LstLeaveEncasment = objBLL.LeaveEncasmentGetAll(LoginInfo.Current.strCompanyID.ToString(), LoginInfo.Current.intLeaveYearID);
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
                this._LeaveYear = new SelectList(itemList, "Value", "Text");

                return _LeaveYear;
            }
            set { _LeaveYear = value; }
        }

        public SelectList EnachLeaveYear
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<LeaveYear> lstLeaveYear = new List<LeaveYear>();

                lstLeaveYear = Common.fetchLeaveYear().OrderByDescending(c=> c.strYearTitle).ToList();

                foreach (LeaveYear lt in lstLeaveYear)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.strYearTitle;
                    item.Text = lt.strYearTitle;
                    itemList.Add(item);
                }
                this._LeaveYear = new SelectList(itemList, "Value", "Text");

                return _LeaveYear;
            }
            set { _LeaveYear = value; }
        }


        public SelectList PaymentYear
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                for (int i = 2000; i <= 2050; i++)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = i.ToString();
                    item.Text = i.ToString();
                    itemList.Add(item);
                }

                this._PaymentYear = new SelectList(itemList, "Value", "Text");

                return _PaymentYear;
            }
            set { _PaymentYear = value; }
        }


        public SelectList PaymentMonth
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                for (int i = 1; i <= 12; i++)
                {
                    string monthname = "";
                    SelectListItem item = new SelectListItem();

                    monthname = Common.GetMonthName(i, false);
                    item.Value = monthname;
                    item.Text = monthname;
                    itemList.Add(item);
                }

                this._PaymentMonth = new SelectList(itemList, "Value", "Text");

                return _PaymentMonth;
            }
            set { _PaymentMonth = value; }
        }


        public SelectList LeaveType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<LeaveType> lstLeaveType = new List<LeaveType>();

                lstLeaveType = Common.fetchLeaveType().Where(c => c.bitIsEncashable == true).ToList();

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

        public double GetLeaveBalance(int yearId, int leavetypeId, string strEmpId)
        {
            double balance = 0;
            LeaveLedger objLvLedger = new LeaveLedger();
            try
            {
                objLvLedger = objLvLedgerBLL.LeaveLedgerGet(yearId, leavetypeId, strEmpId, LoginInfo.Current.strCompanyID.ToString());

                if (objLvLedger != null)
                {
                    balance = objLvLedger.fltCB;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return balance;
        }

        public double GetLeaveEncahed(int Id, int yearId, int leavetypeId, string strEmpId)
        {
            double encashed = 0;

            List<LeaveEncasment> LstEncash = new List<LeaveEncasment>();
            try
            {
                
                //LstEncash = objBLL.LeaveEncasmentGet(yearId, leavetypeId, strEmpId, LoginInfo.Current.strCompanyID.ToString());
                LstEncash = objBLL.LeaveEncasedGet(yearId, leavetypeId, strEmpId, LoginInfo.Current.strCompanyID.ToString());

                if (LstEncash != null)
                {
                    encashed = LstEncash.Where(c => c.intLeaveEncaseID != Id).Sum(c => c.fltEncaseDuration);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return encashed;
        }

        public LeaveRule GetMaxMinEncashableLeave(string strEmpId, int leavetypeId)
        {
            return objLvRuleBLL.GetEmployeeWiseRule(strEmpId, leavetypeId, LoginInfo.Current.strCompanyID);
        }

        public List<LeaveType> GetLeaveTypeList(string intLocationID, string intDepartment, string intDesignationID)
        {
            return objBLL.GetLeaveTypeList(intLocationID, intDepartment, intDesignationID);
        }
    }
}
