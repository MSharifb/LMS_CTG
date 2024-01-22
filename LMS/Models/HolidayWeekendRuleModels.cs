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
    public class HolidayWeekendRuleModels
    {
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        bool _IsDelete;
        bool _bitIsSelectAll;

        public bool bitIsSelectAll
        {
            get
            {
                if (_bitIsSelectAll == null)
                {
                    _bitIsSelectAll = false;
                }
                return _bitIsSelectAll;
            }
            set { _bitIsSelectAll = value; }
        }

        public bool IsDelete
        {
            get
            {
                if (_IsDelete == null)
                {
                    _IsDelete = false;
                }
                return _IsDelete;
            }
            set { _IsDelete = value; }
        }

        public string _Message;
        private int _intLeaveYearID;
        private int _intHolidayWeekendID;
        private List<SelectListItem> _LeaveYearList;
        private List<SelectListItem> _HolidayWeekDayList;
        private HolidayWeekDayRule _HolidayWeekDayRule;
        private IList<HolidayWeekDayRule> _LstHolidayWeekDayRule;
        private IPagedList<HolidayWeekDayRule> _LstHolidayWeekDayRulePaging;
        private IList<WeekendConfigure> lstWeekendConfig;
        private WeekendConfigure _WeekendConfig;
        HolidayWeekDayRuleBLL objBLL = new HolidayWeekDayRuleBLL();
        HolidayWeekDayRuleChildBLL objHWRuleChildBLL = new HolidayWeekDayRuleChildBLL();

        public WeekendConfigure WeekendConfig
        {
            get
            {
                if (this._WeekendConfig == null)
                {
                    this._WeekendConfig = new WeekendConfigure();
                }
                return _WeekendConfig;
            }
            set { _WeekendConfig = value; }
        }
        
        public IList<WeekendConfigure> LstWeekendConfig
        {
            get { return lstWeekendConfig; }
            set { lstWeekendConfig = value; }
        }

        public HolidayWeekDayRule HolidayWeekDayRule
        {
            get
            {
                if (this._HolidayWeekDayRule == null)
                {
                    this._HolidayWeekDayRule = new HolidayWeekDayRule();
                }
                return _HolidayWeekDayRule;
            }
            set { _HolidayWeekDayRule = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public int IntLeaveYearID
        {
            get { return _intLeaveYearID; }
            set { _intLeaveYearID = value; }
        }
        public int IntHolidayWeekendID
        {
            get { return _intHolidayWeekendID; }
            set { _intHolidayWeekendID = value; }
        }

        public IList<HolidayWeekDayRule> LstHolidayWeekDayRule
        {
            get { return _LstHolidayWeekDayRule; }
            set { _LstHolidayWeekDayRule = value; }
        }

        public IPagedList<HolidayWeekDayRule> LstHolidayWeekDayRulePaging
        {
            get { return _LstHolidayWeekDayRulePaging; }
            set { _LstHolidayWeekDayRulePaging = value; }
        }

        public List<SelectListItem> LeaveYearList
        {
            get
            {
                int intHWYearId = 0;
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<HolidayWeekDay> lstHolidayYear = new List<HolidayWeekDay>();
                BLL.HolidayWeekDayBLL objHolidayWeekDayBLL = new HolidayWeekDayBLL();

                lstHolidayYear = objHolidayWeekDayBLL.HolidayWeekDayGetAll(LoginInfo.Current.strCompanyID).OrderBy(c => c.intLeaveYearID).ToList();

                if (lstHolidayYear.Count > 0)
                {
                    foreach (HolidayWeekDay lt in lstHolidayYear)
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

        public List<HolidayWeekDayRuleChild> GetHolidayWeekDayRuleChild(int ruleId, int yearId)
        {
            try
            {
                return objHWRuleChildBLL.HolidayWeekDayRuleChildGet(ruleId, yearId, LoginInfo.Current.strCompanyID).OrderBy(c => c.dtDateFrom).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveData(HolidayWeekendRuleModels model)
        {

            try
            {
                model.HolidayWeekDayRule.strEUser = LoginInfo.Current.LoginName;

                if (model.HolidayWeekDayRule.intHolidayRuleID > 0)
                {
                    model.HolidayWeekDayRule.strCompanyID = LoginInfo.Current.strCompanyID;
                    return objBLL.Edit(model.HolidayWeekDayRule);
                }
                else
                {
                    model.HolidayWeekDayRule.strIUser = LoginInfo.Current.LoginName;
                    model.HolidayWeekDayRule.strCompanyID = LoginInfo.Current.strCompanyID;

                    return objBLL.Add(model.HolidayWeekDayRule);

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Delete(int Id)
        {

            try
            {
                return objBLL.Delete(Id);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public HolidayWeekDayRule GetHolidayWeekDayRule(int ruleId)
        {
            HolidayWeekendRuleModels model = new HolidayWeekendRuleModels();
            try
            {
                model.HolidayWeekDayRule = objBLL.HolidayWeekDayRuleGet(ruleId);
                if (model.HolidayWeekDayRule.intLeaveYearID > 0)
                {
                    model.HolidayWeekDayRule.HolidayWeekDayRuleChild = objHWRuleChildBLL.HolidayWeekDayRuleChildGet(ruleId, model.HolidayWeekDayRule.intLeaveYearID, LoginInfo.Current.strCompanyID).OrderBy(c => c.dtDateFrom).ToList();
                }
                return model.HolidayWeekDayRule;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetHolidayWeekDayRuleAll()
        {
            LstHolidayWeekDayRule = objBLL.HolidayWeekDayRuleGetAll(LoginInfo.Current.strCompanyID);
        }

        public List<HolidayWeekDayRule> GetHolidayWeekDayRulePaging(HolidayWeekDayRule objHolidayWeekDayRule)
        {
            return objBLL.HolidayWeekDayRuleGetAll(LoginInfo.Current.strCompanyID);
        }

    }
}
