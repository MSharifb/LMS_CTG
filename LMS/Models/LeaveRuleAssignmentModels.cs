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
    public class LeaveRuleAssignmentModels
    {
        private SelectList _Location;
        private SelectList _Designation;
        private SelectList _Department;
        private SelectList _Gender;
        private SelectList _Category;
        private SelectList _LeaveType;
        private SelectList _LeaveRule;
        private SelectList _EmployeeType;
        private SelectList _JobGrade;

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;

        int _maximumRows;
        int _numTotalRows;

        private IList<LeaveRuleAssignment> _lstLeaveRuleAssignment;
        private LeaveRuleAssignment _LeaveRuleAssignment;
        private IPagedList<LeaveRuleAssignment> _LstLeaveRuleAssignmentPaging;

        public IPagedList<LeaveRuleAssignment> LstLeaveRuleAssignmentPaging
        {
            get { return _LstLeaveRuleAssignmentPaging; }
            set { _LstLeaveRuleAssignmentPaging = value; }
        }

        private string _Message;
        private string _strSearchInitial;

        
        private string _strSearchName;
        private int _intSearchRuleID;
        private int _intSearchCategoryId;
        private int _intSearchLeaveTypeID;
        private string _strSearchDesignationId;
        private string _strSearchDepartmentId;
        private string _strSearchLocationId;
        private string _strSearchGender;

        public int PageNumber { set; get; }

        int _intLeaveTypeID;
        public int intLeaveTypeID
        {
            get { return _intLeaveTypeID; }
            set { _intLeaveTypeID = value; }
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

        public LeaveRuleAssignment LeaveRuleAssignment
        {
            get
            {
                if (this._LeaveRuleAssignment == null)
                {
                    this._LeaveRuleAssignment = new LeaveRuleAssignment();
                }
                return _LeaveRuleAssignment;
            }
            set { _LeaveRuleAssignment = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

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

        public string strSearchLocationId
        {
            get { return _strSearchLocationId; }
            set { _strSearchLocationId = value; }
        }

        public int intSearchRuleID
        {
            get { return _intSearchRuleID; }
            set { _intSearchRuleID = value; }
        }

        public int intSearchLeaveTypeID
        {
            get { return _intSearchLeaveTypeID; }
            set { _intSearchLeaveTypeID = value; }
        }

        public string strSearchGender
        {
            get { return _strSearchGender; }
            set { _strSearchGender = value; }
        }

        public int intSearchCategoryId
        {
            get { return _intSearchCategoryId; }
            set { _intSearchCategoryId = value; }
        }

        public IList<LeaveRuleAssignment> LstLeaveRuleAssignment
        {
            get { return _lstLeaveRuleAssignment; }
            set { _lstLeaveRuleAssignment = value; }
        }

        LeaveRuleAssignmentBLL objBLL = new LeaveRuleAssignmentBLL();

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

        public SelectList EmployeeType { get {
            List<SelectListItem> itemList = new List<SelectListItem>();

            List<EmployeeType> lstCategory = new List<EmployeeType>();

            lstCategory = Common.fetchEmployeeType().ToList();

            foreach (EmployeeType lt in lstCategory)
            {
                SelectListItem item = new SelectListItem();
                item.Value = lt.intEmployeeTypeId.ToString();
                item.Text = lt.EmployeeTypeName;
                itemList.Add(item);
            }
            this._Category = new SelectList(itemList, "Value", "Text");
            return _Category;
        } set { _EmployeeType = value; } }

        public SelectList JobGrade
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<JobGrade> lstCategory = new List<JobGrade>();

                lstCategory = Common.fetchJobGrade().ToList();

                foreach (JobGrade lt in lstCategory)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.Id.ToString();
                    item.Text = lt.GradeName;
                    itemList.Add(item);
                }
                this._JobGrade = new SelectList(itemList, "Value", "Text");
                return _JobGrade;
            }
            set { _EmployeeType = value; }
        }

        public SelectList Department
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Department> lstDepartment = new List<Department>();

              //  lstDepartment = Common.fetchDepartment().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();
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


        public SelectList LeaveRule
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<LeaveRule> lstLeaveRule = new List<LeaveRule>();

                lstLeaveRule = GetLeaveTypewiseRules(this.intLeaveTypeID).OrderBy(c => c.strRuleName).ToList();

                foreach (LeaveRule lt in lstLeaveRule)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intRuleID.ToString();
                    item.Text = lt.strRuleName;
                    itemList.Add(item);
                }
                this._LeaveRule = new SelectList(itemList, "Value", "Text");
                return _LeaveRule;
            }
            set { _LeaveRule = value; }
        }

        public int SaveData(LeaveRuleAssignmentModels model, out string strmessage)
        {
            int i = -1;

            try
            {

                model.LeaveRuleAssignment.strEUser = LoginInfo.Current.LoginName;
                model.LeaveRuleAssignment.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveRuleAssignment.intRuleAssignID > 0)
                {
                    i = objBLL.Edit(model.LeaveRuleAssignment, out strmessage);
                }
                else
                {
                    model.LeaveRuleAssignment.strIUser = LoginInfo.Current.LoginName;
                    i = objBLL.Add(model.LeaveRuleAssignment, out strmessage);
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

            int i = -1;

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

        public void GetLeaveRuleAssignmentAll(LeaveRuleAssignment objRuleAssign)
        {

            int Total = 0;
            LstLeaveRuleAssignment = objBLL.LeaveRuleAssignmentGet(objRuleAssign.intRuleID, objRuleAssign.strEmpName, objRuleAssign.strEmpInitial, LoginInfo.Current.strCompanyID,
                                                                                objRuleAssign.intLeaveTypeID, objRuleAssign.strDepartmentID, objRuleAssign.strDesignationID,
                                                                                objRuleAssign.strGender, objRuleAssign.intCategoryCode, strSortBy, 
                                                                                strSortType, startRowIndex, maximumRows, out Total);
            numTotalRows = Total;
        }

        public double GetEntitlement(int intRuleID)
        {
            BLL.LeaveRuleBLL objBLLLeaveRuleBLL = new LeaveRuleBLL();
            return objBLLLeaveRuleBLL.LeaveRuleGet(intRuleID, "", -1, LoginInfo.Current.strCompanyID).Single().fltEntitlement;
        }

        public LeaveRuleAssignment GetLeaveRuleAssignment(int Id)
        {
            LeaveRuleAssignmentModels model = new LeaveRuleAssignmentModels();

            model.LeaveRuleAssignment = objBLL.LeaveRuleAssignmentGet(Id);

            return model.LeaveRuleAssignment;
        }

        public List<LeaveRuleAssignment> GetLeaveRuleAssignmentPaging(LeaveRuleAssignment objRuleAssign)
        {
            int Total = 0;
            return objBLL.LeaveRuleAssignmentGet(objRuleAssign.intRuleID, objRuleAssign.strEmpName, objRuleAssign.strEmpID, LoginInfo.Current.strCompanyID,
                                                                                objRuleAssign.intLeaveTypeID, objRuleAssign.strDepartmentID, objRuleAssign.strDesignationID,
                                                                                objRuleAssign.strGender, objRuleAssign.intCategoryCode, strSortBy, strSortType, startRowIndex, maximumRows, out Total);



        }

        public List<LeaveRule> GetLeaveTypewiseRules(int intleavetypeId)
        {
            LeaveRuleBLL objLeaveRuleBLL = new LeaveRuleBLL();
            return objLeaveRuleBLL.LeaveRuleGet(-1, "", intleavetypeId, LoginInfo.Current.strCompanyID);
        }

    }
}