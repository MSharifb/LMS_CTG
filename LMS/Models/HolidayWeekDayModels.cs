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
    public class HolidayWeekDayModels
    {
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;
        private SelectList _LeaveYear;
        private SelectList _SearchLeaveYear;
        private SelectList _SearchMonth;
        private SelectList _TypeList;
        private string _intLeaveYearId;

        private HolidayWeekDay _HolidayWeekDay;
        private List<HolidayWeekDayDetails> _lstHolidayWeekendDetails;
        private IList<HolidayWeekDay> _LstHolidayWeekDay;
        private IPagedList<HolidayWeekDay> _LstHolidayWeekDayPaging;

        private HolidayWeekDayBLL objBLL = new HolidayWeekDayBLL();



        public string IntLeaveYearId
        {
            get { return _intLeaveYearId; }
            set { _intLeaveYearId = value; }
        }

        private string strYearStartDate;
        public string StrYearStartDate
        {
            get { return strYearStartDate; }
            set { strYearStartDate = value; }
        }

        private string strYearEndDate;
        public string StrYearEndDate
        {
            get { return strYearEndDate; }
            set { strYearEndDate = value; }
        }

        private string _strSearchType;
        public string strSearchType
        {
            get { return _strSearchType; }
            set { _strSearchType = value; }
        }

        private int _intSearchYearId;
        public int intSearchYearId
        {
            get { return _intSearchYearId; }
            set { _intSearchYearId = value; }
        }

        private int _intSearchMonthId;
        public int intSearchMonthId
        {
            get { return _intSearchMonthId; }
            set { _intSearchMonthId = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public HolidayWeekDay HolidayWeekDay
        {
            get
            {
                if (_HolidayWeekDay == null)
                {
                    _HolidayWeekDay = new HolidayWeekDay();
                }
                return _HolidayWeekDay;
            }
            set { _HolidayWeekDay = value; }
        }

        public List<HolidayWeekDayDetails> LstHolidayWeekendDetails
        {
            get { return _lstHolidayWeekendDetails; }
            set { _lstHolidayWeekendDetails = value; }
        }

        public IList<HolidayWeekDay> LstHolidayWeekDay
        {
            get
            {
                if (_LstHolidayWeekDay == null)
                {
                    _LstHolidayWeekDay = new List<HolidayWeekDay>();
                }
                return _LstHolidayWeekDay;
            }
            set { _LstHolidayWeekDay = value; }
        }
        public IPagedList<HolidayWeekDay> LstHolidayWeekDayPaging
        {
            get { return _LstHolidayWeekDayPaging; }
            set { _LstHolidayWeekDayPaging = value; }
        }

        private bool _isAutomatic;
        public bool IsAutmatic
        {
            set { _isAutomatic = value; }
            get { return _isAutomatic; }
        }
        private WeekendConfigure _WeekendConfig;
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
        private IList<WeekendConfigure> lstWeekendConfig;
        public IList<WeekendConfigure> LstWeekendConfig
        {
            get
            {
                //lstWeekendConfig = new List<WeekendConfigure>();
                //foreach (var day in Enum.GetNames(typeof(Common.DayEnum)))
                //{
                //    var weekConf = new WeekendConfigure { WeekDay = day };

                //    lstWeekendConfig.Add(weekConf);
                //}

                return lstWeekendConfig;
            }
            set { lstWeekendConfig = value; }
        }

        private SelectList _WorkingTime;
        public SelectList WorkingTime
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<OfficeTimeDetails> lstWrkTime = new List<OfficeTimeDetails>();

                lstWrkTime = Common.fetchWorkingTime();

                foreach (OfficeTimeDetails wt in lstWrkTime)
                {

                    SelectListItem item = new SelectListItem();
                    item.Value = wt.intDurationID.ToString();
                    item.Text = wt.strDurationName;

                    itemList.Add(item);
                }
                this._WorkingTime = new SelectList(itemList, "Value", "Text");
                return _WorkingTime;
            }
            set { _WorkingTime = value; }
        }


        public int  Save(HolidayWeekDayModels model, ref string strmsg)
        {
            HolidayWeekDayBLL objBll = new HolidayWeekDayBLL();

            model.HolidayWeekDay.strEUser = LoginInfo.Current.LoginName;
            model.HolidayWeekDay.strCompanyID = LoginInfo.Current.strCompanyID;

            model = GetAutomaticWeekendList(model);
            model = GetDetailsDataList(model);
           return  objBll.Save(model.HolidayWeekDay, model.LstHolidayWeekendDetails, model.LstHolidayWeekDay.ToList(), ref strmsg);
        }


        public int SaveData(HolidayWeekDayModels model, ref string strmsg)
        {
            int returnid = 0;
            HolidayWeekDayBLL objBll = new HolidayWeekDayBLL();
            try
            {
                model.HolidayWeekDay.strEUser = LoginInfo.Current.LoginName;
                model.HolidayWeekDay.strCompanyID = LoginInfo.Current.strCompanyID;

                //if (model.HolidayWeekDay.intHolidayWeekendID > 0)
                //{
                //    returnid = objBll.Edit(model.HolidayWeekDay, ref strmsg);
                //}
                //else
                //{
                //    model.HolidayWeekDay.strIUser = LoginInfo.Current.LoginName;

                //    returnid = objBll.Add(model.HolidayWeekDay, ref strmsg);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnid;
        }

        public int Delete(HolidayWeekDay obj)
        {
            int returnid = 0;
            try
            {
                returnid = objBLL.Delete(obj);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return returnid;
        }

        public HolidayWeekDayModels GetHolidayWeekDay(int Id)
        {
            HolidayWeekDayModels model = new HolidayWeekDayModels();

            try
            {

                model.HolidayWeekDay = objBLL.HolidayWeekDayGet(Id);
                HolidayWeekDayDetailsBLL detailsObj = new HolidayWeekDayDetailsBLL();
                model.LstHolidayWeekendDetails = detailsObj.HolidayWeekDayDetailsGetByMasterId(Id);

               
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetHolidayWeekDayAll(int intleaveyearId, int intmonthId, string strtype)
        {

            if (intleaveyearId <= 0)
            { intleaveyearId = LoginInfo.Current.intLeaveYearID; }

            if (intmonthId != null && intmonthId > 0)
            {
                LstHolidayWeekDay = objBLL.HolidayWeekDayGet(intleaveyearId, strtype, LoginInfo.Current.strCompanyID.ToString()).Where(c => c.dtDateFrom.Month == intmonthId).OrderBy(c => c.dtDateFrom).ToList();
            }
            else
            {
                LstHolidayWeekDay = objBLL.HolidayWeekDayGet(intleaveyearId, strtype, LoginInfo.Current.strCompanyID.ToString()).OrderBy(c => c.dtDateFrom).ToList();
            }
        }

        public HolidayWeekDayModels GetAutomaticWeekendList(HolidayWeekDayModels model)
        {
            int retVal = -1;

            try
            {

                model.LstHolidayWeekDay = new List<HolidayWeekDay>();

                DateTime startDate = DateTime.Parse(model.HolidayWeekDay.strDateFrom, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                DateTime endDate = DateTime.Parse(model.HolidayWeekDay.strDateTo, new CultureInfo("fr-Fr", true), DateTimeStyles.None);

                bool isAlternateInsert = false;


                if (model.LstWeekendConfig.Where(c => c.IsAlternate == true && c.IsWeekend_FirstDayOfYear == true).ToList().Count > 0)
                {
                    isAlternateInsert = true;
                }


                for (DateTime dt = startDate; dt <= endDate; )
                {
                    HolidayWeekDay obj = new LMSEntity.HolidayWeekDay();

                    string day = dt.ToString("dddd");

                    if (model.LstWeekendConfig.Where(c => c.WeekDay == dt.ToString("dddd") && c.IsWeekend == true && c.IsAlternate == false).ToList().Count > 0)
                    {
                        obj.dtDateFrom = dt;
                        obj.dtDateTo = dt;
                        obj.intDuration = 1;
                        obj.IsHoliday = model.HolidayWeekDay.IsHoliday;
                        obj.dtEDate = model.HolidayWeekDay.dtEDate;
                        obj.dtIDate = model.HolidayWeekDay.dtIDate;
                        obj.intLeaveYearID = model.HolidayWeekDay.intLeaveYearID;
                        obj.strIUser = model.HolidayWeekDay.strIUser;
                        obj.strType = model.HolidayWeekDay.strType;
                        obj.strYearTitle = model.HolidayWeekDay.strYearTitle;
                        obj.strHolidayTitle = model.HolidayWeekDay.strHolidayTitle;
                        obj.strCompanyID = model.HolidayWeekDay.strCompanyID;
                        model.LstHolidayWeekDay.Add(obj);                        
                        //retVal = SaveData(model, ref refMsg);
                    }

                    else if (model.LstWeekendConfig.Where(c => c.WeekDay == dt.ToString("dddd") && c.IsWeekend == true && c.IsAlternate == true).ToList().Count > 0)
                    {
                        if (isAlternateInsert == true)
                        {
                            //model.HolidayWeekDay.dtDateFrom = dt;
                            //model.HolidayWeekDay.dtDateTo = dt;
                            //model.HolidayWeekDay.intDuration = 1;
                            obj.dtDateFrom = dt;
                            obj.dtDateTo = dt;
                            obj.intDuration = 1;
                            obj.IsHoliday = model.HolidayWeekDay.IsHoliday;
                            obj.dtEDate = model.HolidayWeekDay.dtEDate;
                            obj.dtIDate = model.HolidayWeekDay.dtIDate;
                            obj.intLeaveYearID = model.HolidayWeekDay.intLeaveYearID;
                            obj.strIUser = model.HolidayWeekDay.strIUser;
                            obj.strType = model.HolidayWeekDay.strType;
                            obj.strYearTitle = model.HolidayWeekDay.strYearTitle;
                            obj.strHolidayTitle = model.HolidayWeekDay.strHolidayTitle;
                            obj.strCompanyID = model.HolidayWeekDay.strCompanyID;
                            model.LstHolidayWeekDay.Add(obj);                        

                            isAlternateInsert = false;

                        }
                        else
                        {
                            isAlternateInsert = (isAlternateInsert == true ? false : true);
                        }

                    }

                    dt = dt.AddDays(1);


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        public int SaveMasterData(HolidayWeekDayModels model, ref string strmsg)
        {
            int returnid = 0;
            HolidayWeekDayBLL objBll = new HolidayWeekDayBLL();
            try
            {
                model.HolidayWeekDay.strEUser = LoginInfo.Current.LoginName;
                model.HolidayWeekDay.strCompanyID = LoginInfo.Current.strCompanyID;

                //if (model.HolidayWeekDay.intHolidayWeekendMasterID > 0)
                //{
                //    returnid = objBll.EditMaster(model.HolidayWeekDay, ref strmsg);
                //}
                //else
                //{
                //    model.HolidayWeekDay.strIUser = LoginInfo.Current.LoginName;

                //    returnid = objBll.AddMaster(model.HolidayWeekDay, ref strmsg);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnid;
        }

        public HolidayWeekDayModels GetDetailsDataList(HolidayWeekDayModels model)
        {
            int returnid = 0;
            HolidayWeekDayDetailsBLL objBll = new HolidayWeekDayDetailsBLL();
            try
            {
                model.HolidayWeekDay.strEUser = LoginInfo.Current.LoginName;
                model.HolidayWeekDay.strCompanyID = LoginInfo.Current.strCompanyID;
                model.LstHolidayWeekendDetails = new List<HolidayWeekDayDetails>();

                DateTime startDate = DateTime.Parse(model.HolidayWeekDay.strDateFrom, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                DateTime endDate = DateTime.Parse(model.HolidayWeekDay.strDateTo, new CultureInfo("fr-Fr", true), DateTimeStyles.None);


                foreach (WeekendConfigure item in model.LstWeekendConfig)
                {
                    if (item.IsWeekend == true)
                    {
                        HolidayWeekDayDetails obj = new HolidayWeekDayDetails();
                        obj.intHolidayWeekendDetailsID = -1;
                        
                        //obj.intHolidayWeekendMasterID = intWeekendHolidayMasterID;
                        obj.isAlternateDay = item.IsAlternate;
                        obj.isFromFirstWeekend = item.IsWeekend_FirstDayOfYear;
                        obj.strDay = item.WeekDay;
                        obj.intDuration = 1;
                        model.LstHolidayWeekendDetails.Add(obj);
                        //if (model.HolidayWeekDay.intHolidayWeekendID > 0)
                        //{
                        //    returnid = objBll.Edit(obj, ref strmsg);
                        //}
                        //else
                        //{
                        //    model.HolidayWeekDay.strIUser = LoginInfo.Current.LoginName;

                        //    returnid = objBll.Add(obj, ref strmsg);
                        //}
                    }
                }
                //if (model.HolidayWeekDay.intHolidayWeekendID > 0)
                //{
                //    returnid = objBll.Edit(model.HolidayWeekDay, ref strmsg);
                //}
                //else
                //{
                //    model.HolidayWeekDay.strIUser = LoginInfo.Current.LoginName;

                //    returnid = objBll.Add(model.HolidayWeekDay, ref strmsg);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
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

        public SelectList SearchLeaveYear
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<LeaveYear> lstSrcLeaveYear = new List<LeaveYear>();

                lstSrcLeaveYear = Common.fetchLeaveYear();

                foreach (LeaveYear lt in lstSrcLeaveYear)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.intLeaveYearID.ToString();
                    item.Text = lt.strYearTitle;
                    itemList.Add(item);
                }
                this._SearchLeaveYear = new SelectList(itemList, "Value", "Text", LoginInfo.Current.intLeaveYearID);

                return _SearchLeaveYear;
            }
            set { _SearchLeaveYear = value; }
        }

        public SelectList SearchMonth
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem list = new SelectListItem();
                DateTime month = Convert.ToDateTime("1/1/2000");
                for (int i = 0; i < 12; i++)
                {

                    DateTime NextMont = month.AddMonths(i);
                    list = new SelectListItem();
                    list.Text = NextMont.ToString("MMMM");
                    list.Value = NextMont.Month.ToString();
                    itemList.Add(list);
                }
                this._SearchMonth = new SelectList(itemList, "Value", "Text");
                return _SearchMonth;
            }
            set { _SearchMonth = value; }
        }


        public SelectList TypeList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                SelectListItem item = new SelectListItem();
                item.Value = "Holiday";
                item.Text = "Holiday";
                itemList.Add(item);
                item = new SelectListItem();

                item.Value = "Weekend";
                item.Text = "Weekend";
                itemList.Add(item);

                this._TypeList = new SelectList(itemList, "Value", "Text");
                return _TypeList;
            }
            set { _TypeList = value; }
        }

        public LeaveYear GetLeaveYearInfo(int Id)
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
    }
}
