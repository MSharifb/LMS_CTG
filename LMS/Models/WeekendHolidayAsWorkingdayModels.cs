using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LMS.BLL;
using LMSEntity;
using MvcPaging;

namespace LMS.Web.Models
{
    public class WeekendHolidayAsWorkingdayModels
    {
        private string _strEffectiveDateFrom;
        public string StrEffectiveDateFrom
        {
            get
            {
                return _strEffectiveDateFrom;
            }
            set { _strEffectiveDateFrom = value; }
        }

        private string _strEffectiveDateTo;
        public string StrEffectiveDateTo
        {
            get
            {
                return _strEffectiveDateTo;
            }
            set { _strEffectiveDateTo = value; }
        }

        private string _strDeclarationDate;
        public string StrDeclarationDate
        {
            get
            {
                return _strDeclarationDate;
            }
            set { _strDeclarationDate = value; }
        }

       
        public string _Message;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        WeekendHolidayAsWorkingday _WeekendHolidayAsWorkingday;
        public WeekendHolidayAsWorkingday WeekendHolidayAsWorkingday
        {
            get
            {
                if (_WeekendHolidayAsWorkingday == null)
                {
                    _WeekendHolidayAsWorkingday = new WeekendHolidayAsWorkingday();
                }
                return _WeekendHolidayAsWorkingday;
            }
            set { _WeekendHolidayAsWorkingday = value; }
        }

        IList<WeekendHolidayAsWorkingday> lstWeekendHolidayAsWorkingday;
        public IList<WeekendHolidayAsWorkingday> LstWeekendHolidayAsWorkingday
        {
            get { return lstWeekendHolidayAsWorkingday; }
            set { lstWeekendHolidayAsWorkingday = value; }
        }

        IPagedList<WeekendHolidayAsWorkingday> lstWeekendHolidayAsWorkingdayPaged;
        public IPagedList<WeekendHolidayAsWorkingday> LstWeekendHolidayAsWorkingdayPaged
        {
            get { return lstWeekendHolidayAsWorkingdayPaged; }
            set { lstWeekendHolidayAsWorkingdayPaged = value; }
        }

        WeekendHolidayAsWorkingdayBLL objBLL = new  WeekendHolidayAsWorkingdayBLL();

        /// <summary>
        /// Save
        /// </summary>

        public int SaveData(WeekendHolidayAsWorkingdayModels model, ref string strmsg)
        {

            int i = 0;

            try
            {
                model.WeekendHolidayAsWorkingday.strEUser = LoginInfo.Current.LoginName;
                if (model.WeekendHolidayAsWorkingday.intWeekendWorkingday > 0)
                {

                    i = objBLL.Edit(model.WeekendHolidayAsWorkingday, ref strmsg);
                }
                else
                {
                    model.WeekendHolidayAsWorkingday.strIUser = LoginInfo.Current.LoginName;

                    i = objBLL.Add(model.WeekendHolidayAsWorkingday, ref strmsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// 
        public int Delete(int Id)
        {
            int i = 0;
            try
            {

                i = objBLL.Delete(Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return i;
        }

        public WeekendHolidayAsWorkingday WeekendHolidayAsWorkingdayGetByID(int Id)
        {
            WeekendHolidayAsWorkingdayModels model = new WeekendHolidayAsWorkingdayModels();

            try
            {
                return model.WeekendHolidayAsWorkingday = objBLL.WeekendHolidayAsWorkingdayGetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WeekendHolidayAsWorkingdayGetAll()
        {
            LstWeekendHolidayAsWorkingday = objBLL.WeekendHolidayAsWorkingdayGetAll();
        }

        public List<WeekendHolidayAsWorkingday> GetWeekendHolidayAsWorkingdayPaging(WeekendHolidayAsWorkingdayModels model)
        {
            return  objBLL.WeekendHolidayAsWorkingdayGetSrc(model.StrEffectiveDateFrom, model._strEffectiveDateTo, model.StrDeclarationDate);
        }

    }
}