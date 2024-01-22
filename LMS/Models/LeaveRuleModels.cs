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
    public class LeaveRuleModels
    {
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;
        private bool _IsEncashable1;

        private SelectList _LeaveType;
        private SelectList _AdjustLeaveType;
        private SelectList _Eligible;
        private SelectList _CalculationFrom;
        private SelectList _CarryForwardMonth;
        private SelectList _MonthYearService;
        private SelectList _NextEligibleList; // Added FOR BEPZA

        private LeaveRule _LeaveRule;
        private List<LeaveType> lstLeaveType;
        private IPagedList<LeaveRule> _LstLeaveRulePaging;
        private IList<LeaveRule> lstLeaveRule;

        LeaveRuleBLL objBLL = new LeaveRuleBLL();

        private int _intSearchLeaveTypeId;
        public int intSearchLeaveTypeId
        {
            get { return _intSearchLeaveTypeId; }
            set { _intSearchLeaveTypeId = value; }
        }

        public List<LeaveType> LstLeaveType
        {
            get
            {
                if (lstLeaveType == null)
                {
                    lstLeaveType = new List<LeaveType>();
                }
                return lstLeaveType;
            }
            set { lstLeaveType = value; }
        }

        public LeaveRule LeaveRule
        {
            get
            {
                if (this._LeaveRule == null)
                {
                    this._LeaveRule = new LeaveRule();
                }
                return _LeaveRule;
            }
            set { _LeaveRule = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<LeaveRule> LstLeaveRule
        {
            get { return lstLeaveRule; }
            set { lstLeaveRule = value; }
        }
        public IPagedList<LeaveRule> LstLeaveRulePaging
        {
            get { return _LstLeaveRulePaging; }
            set { _LstLeaveRulePaging = value; }
        }

        public SelectList CarryForwardMonth
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem list = new SelectListItem();
                list.Text = "N/A";
                list.Value = "N/A";
                itemList.Add(list);


                DateTime month = Convert.ToDateTime("1/1/2000");
                for (int i = 0; i < 12; i++)
                {

                    DateTime NextMont = month.AddMonths(i);
                    list = new SelectListItem();
                    list.Text = NextMont.ToString("MMMM");
                    list.Value = NextMont.Month.ToString();
                    itemList.Add(list);
                }

                this._CarryForwardMonth = new SelectList(itemList, "Value", "Text");
                return _CarryForwardMonth;
            }
            set { _CarryForwardMonth = value; }
        }

        public SelectList MonthYearService
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem list = new SelectListItem();
                list.Text = "Month";
                list.Value = "M";
                itemList.Add(list);

                list = new SelectListItem();
                list.Text = "Year";
                list.Value = "Y";
                itemList.Add(list);

                list = new SelectListItem();
                list.Text = "Service Life";
                list.Value = "S";
                itemList.Add(list);

                this._MonthYearService = new SelectList(itemList, "Value", "Text");
                return _MonthYearService;
            }
            set { _MonthYearService = value; }
        }

        // Added FOR BEPZA
        public SelectList NextEligibleList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem list = new SelectListItem();
                list.Text = "Leave Apply Date";
                list.Value = "Apply";
                itemList.Add(list);

                list = new SelectListItem();
                list.Text = "Leave Eligible Date";
                list.Value = "Eligible";
                itemList.Add(list);

                this._NextEligibleList = new SelectList(itemList, "Value", "Text");
                return _NextEligibleList;
            }
            set { _NextEligibleList = value; }
        }

        public SelectList LeaveType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                LstLeaveType = Common.fetchLeaveType();

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

        public SelectList AdjustLeaveType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                LstLeaveType = Common.fetchLeaveType();

                foreach (LeaveType lt in lstLeaveType)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intLeaveTypeID.ToString();
                    item.Text = lt.strLeaveType;
                    itemList.Add(item);
                }
                this._AdjustLeaveType = new SelectList(itemList, "Value", "Text");
                return _AdjustLeaveType;
            }
            set { _AdjustLeaveType = value; }
        }

        public SelectList Eligible
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem item = new SelectListItem();
                item.Value = "Confirmation Date";
                item.Text = "Confirmation Date";
                itemList.Add(item);

                item = new SelectListItem();
                item.Value = "Joining Date";
                item.Text = "Joining Date";

                itemList.Add(item);

                this._Eligible = new SelectList(itemList, "Value", "Text");
                return _Eligible;
            }
            set { _Eligible = value; }
        }
        public SelectList CalculationFrom
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem item = new SelectListItem();
                item.Value = "Confirmation Date";
                item.Text = "Confirmation Date";
                itemList.Add(item);

                item = new SelectListItem();
                item.Value = "Joining Date";
                item.Text = "Joining Date";
                itemList.Add(item);

                item = new SelectListItem();
                item.Value = "N/A";
                item.Text = "N/A";
                itemList.Add(item);


                this._CalculationFrom = new SelectList(itemList, "Value", "Text");
                return _CalculationFrom;
            }
            set { _CalculationFrom = value; }
        }

        public int SaveData(LeaveRuleModels model)
        {
            int returnid = 0;

            try
            {
                model.LeaveRule.strEUser = LoginInfo.Current.LoginName;
                if (model.LeaveRule.intRuleID > 0)
                {

                    returnid = objBLL.Edit(model.LeaveRule);
                }
                else
                {
                    model.LeaveRule.strIUser = LoginInfo.Current.LoginName;
                    model.LeaveRule.strCompanyID = LoginInfo.Current.strCompanyID;

                    returnid = objBLL.Add(model.LeaveRule);

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

        public LeaveType GetEncashment(int Id)
        {

            LeaveTypeBLL objBLL1 = new LeaveTypeBLL();

            try
            {

                return objBLL1.LeaveTypeGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeaveRule GetLeaveRule(int Id)
        {
            LeaveRuleModels model = new LeaveRuleModels();

            try
            {

                return model.LeaveRule = objBLL.LeaveRuleGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetLeaveRuleAll(int livetypeId)
        {
            lstLeaveRule = objBLL.LeaveRuleGetAll(livetypeId, LoginInfo.Current.strCompanyID).OrderBy(c=> c.strLeaveType).ThenBy(c=> c.strRuleName).ToList() ;
        }

        public List<LeaveRule> GetLeaveRulePaging(LeaveRule objLeaveRule)
        {
            return objBLL.LeaveRuleGet(objLeaveRule.intRuleID, objLeaveRule.strRuleName, objLeaveRule.intLeaveTypeID, LoginInfo.Current.strCompanyID);
        }
    }
}