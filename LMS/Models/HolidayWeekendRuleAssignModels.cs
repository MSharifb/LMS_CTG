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
    public class HolidayWeekendRuleAssignModels
    {
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private string _Message;
        private int _IntLeaveYearID;
        //private string _strSearchID;
        private string _strSearchInitial;
        private string _strSearchName;
        private int _intSearchRuleID;
        private string _strSearchDesignationId;
        private string _strSearchDepartmentId;
        private string _strSearchReligionId;
        private int _intSearchCategoryId;
        public int PageNumber { set; get; }

        private List<SelectListItem> _LeaveYearList;
        private List<SelectListItem> _HolidayRuleList;
        private IList<HolidayWeekendRuleAssign> lstHolidayWeekendRuleAssign;
        private IPagedList<HolidayWeekendRuleAssign> _LstHolidayWeekendRuleAssignPaging;

        private SelectList _Designation;
        private SelectList _Department;
        private SelectList _ReligionList;
        private SelectList _Category;

        private HolidayWeekendRuleAssign _HolidayWeekendRuleAssign;

        HolidayWeekDayRuleChildBLL objHWRuleChildBLL = new HolidayWeekDayRuleChildBLL();
        private HolidayWeekendRuleAssignBLL objBLL = new HolidayWeekendRuleAssignBLL();


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

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public int IntLeaveYearID
        {
            get { return _IntLeaveYearID; }
            set { _IntLeaveYearID = value; }
        }

        //public string strSearchID
        //{
        //    get { return _strSearchID; }
        //    set { _strSearchID = value; }
        //}

        public string strSearchName
        {
            get { return _strSearchName; }
            set { _strSearchName = value; }
        }

        public string strSearchInitial
        {
            get { return _strSearchInitial; }
            set { _strSearchInitial = value; }
        }

        public string strSearchDesignationId
        {
            get { return _strSearchDesignationId; }
            set { _strSearchDesignationId = value; }
        }

        public string strSearchDepartmentId
        {
            get { return _strSearchDepartmentId; }
            set { _strSearchDepartmentId = value; }
        }

        public string strSearchReligionId
        {
            get { return _strSearchReligionId; }
            set { _strSearchReligionId = value; }
        }

        public int intSearchRuleID
        {
            get { return _intSearchRuleID; }
            set { _intSearchRuleID = value; }
        }

        public int intSearchCategoryId
        {
            get { return _intSearchCategoryId; }
            set { _intSearchCategoryId = value; }
        }

        public HolidayWeekendRuleAssign HolidayWeekendRuleAssign
        {
            get
            {
                if (_HolidayWeekendRuleAssign == null)
                {
                    _HolidayWeekendRuleAssign = new HolidayWeekendRuleAssign();
                }
                return _HolidayWeekendRuleAssign;
            }
            set { _HolidayWeekendRuleAssign = value; }
        }

        public IList<HolidayWeekendRuleAssign> LstHolidayWeekendRuleAssign
        {
            get { return lstHolidayWeekendRuleAssign; }
            set { lstHolidayWeekendRuleAssign = value; }
        }

        public IPagedList<HolidayWeekendRuleAssign> LstHolidayWeekendRuleAssignPaging
        {
            get { return _LstHolidayWeekendRuleAssignPaging; }
            set { _LstHolidayWeekendRuleAssignPaging = value; }
        }

        public List<SelectListItem> LeaveYearList
        {
            get
            {
                int intHWYearId = 0;
                BLL.HolidayWeekDayRuleBLL objHolidayWeekDayRuleBLL = new HolidayWeekDayRuleBLL();
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<HolidayWeekDayRule> lstHolidayRuleYear = new List<HolidayWeekDayRule>();

                lstHolidayRuleYear = objHolidayWeekDayRuleBLL.HolidayWeekDayRuleGetAll(LoginInfo.Current.strCompanyID).OrderBy(c => c.intLeaveYearID).ToList();

                if (lstHolidayRuleYear.Count > 0)
                {
                    foreach (HolidayWeekDayRule lt in lstHolidayRuleYear)
                    {
                        SelectListItem item = new SelectListItem();
                        if (intHWYearId != lt.intLeaveYearID)
                        {
                            intHWYearId = lt.intLeaveYearID;

                            item.Value = lt.intLeaveYearID.ToString();
                            item.Text = lt.strYearTitle.ToString();
                            itemList.Add(item);
                        }
                    }

                    return itemList;
                }
                return new List<SelectListItem>();
            }
            set { _LeaveYearList = value; }
        }
        public List<SelectListItem> HolidayRuleList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<HolidayWeekDayRule> lstHolidayRule = new List<HolidayWeekDayRule>();
                BLL.HolidayWeekDayRuleBLL objHolidayWeekDayRuleBLL = new HolidayWeekDayRuleBLL();

                lstHolidayRule = objHolidayWeekDayRuleBLL.HolidayWeekDayRuleGet(this.HolidayWeekendRuleAssign.intLeaveYearID, 0, LoginInfo.Current.strCompanyID).OrderBy(c => c.strHolidayRule).ToList();

                if (lstHolidayRule.Count > 0)
                {
                    foreach (HolidayWeekDayRule lt in lstHolidayRule)
                    {
                        SelectListItem item = new SelectListItem();

                        item.Value = lt.intHolidayRuleID.ToString();
                        item.Text = lt.strHolidayRule;
                        itemList.Add(item);
                    }

                    return itemList;
                }
                return new List<SelectListItem>();
            }
            set { _HolidayRuleList = value; }
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

                //lstDepartment = Common.fetchDepartment().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();
                lstDepartment = Common.fetchDepartment().Where(c => c.ZoneInfoId == Convert.ToInt32(LoginInfo.Current.strCompanyID)).ToList();

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
        public SelectList ReligionList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Religion> lstReligion = new List<Religion>();

                lstReligion = Common.fetchReligion();

                foreach (Religion lt in lstReligion)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.strReligionID.ToString();
                    item.Text = lt.strReligion;
                    itemList.Add(item);
                }
                this._ReligionList = new SelectList(itemList, "Value", "Text");

                return _ReligionList;
            }
            set { _ReligionList = value; }
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

        public HolidayWeekendRuleAssign GetHolidayWeekendRuleAssign(int Id)
        {
            HolidayWeekendRuleAssignModels model = new HolidayWeekendRuleAssignModels();

            try
            {
                model.HolidayWeekendRuleAssign = objBLL.HolidayWeekendRuleAssignGet(Id);
                if (model.HolidayWeekendRuleAssign.intHolidayRuleID > 0)
                {
                    model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList = objHWRuleChildBLL.GetHolidayWeekDayByRuleId(model.HolidayWeekendRuleAssign.intHolidayRuleID).OrderBy(c => c.dtDateFrom).ToList();
                }
                return model.HolidayWeekendRuleAssign;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HolidayWeekDayRuleChild> GetHolidayRuleDetails(int ruleId)
        {
            try
            {
                return objHWRuleChildBLL.GetHolidayWeekDayByRuleId(ruleId).OrderBy(c => c.dtDateFrom).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetHolidayWeekendRuleAssignAll(HolidayWeekendRuleAssign objRuleAssign)
        {
            int Total = 0;
            LstHolidayWeekendRuleAssign = objBLL.HolidayWeekendRuleAssignGet(objRuleAssign.intHolidayRuleID, objRuleAssign.strEmpName, objRuleAssign.strEmpInitial, -1, objRuleAssign.strDepartmentID, objRuleAssign.strDesignationID,
                                                                             objRuleAssign.strReligionID, LoginInfo.Current.strCompanyID, objRuleAssign.intCategoryCode, strSortBy, strSortType, startRowIndex, maximumRows, out Total);
            numTotalRows = Total;

        }

        public int SaveData(HolidayWeekendRuleAssignModels model, out string strmessage)
        {
            int i = 0;
            HolidayWeekendRuleAssignBLL objBll = new HolidayWeekendRuleAssignBLL();
            try
            {
                model.HolidayWeekendRuleAssign.strEUser = LoginInfo.Current.LoginName;
                model.HolidayWeekendRuleAssign.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.HolidayWeekendRuleAssign.intRuleAssignID > 0)
                {
                    i = objBll.Edit(model.HolidayWeekendRuleAssign, out strmessage);
                }
                else
                {
                    model.HolidayWeekendRuleAssign.strIUser = LoginInfo.Current.LoginName;
                    i = objBll.Add(model.HolidayWeekendRuleAssign, out strmessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public int Delete(int Id, out string strmessage)
        {
            int i = 0;
            try
            {
                i = objBLL.Delete(Id, out strmessage);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
    }
}
