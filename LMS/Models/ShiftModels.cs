using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LMS.BLL;
using LMSEntity;
using MvcPaging;

namespace LMS.Web.Models
{
    public class ShiftModels
    {
        private string _strShiftName;
        public string strShiftName
        {
            get { return _strShiftName; }
            set { _strShiftName = value; }
        }
        private bool _IsRoaster;
        public bool IsRoaster
        {
            get { return _IsRoaster; }
            set { _IsRoaster = value; }
        }
        private string _strPeriodFrom;
        public string StrPeriodFrom
        {
            get
            {
                //if (_strPeriodFrom == "" || _strPeriodFrom == null)
                //{
                //    _strPeriodFrom = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);
                //}

                return _strPeriodFrom;
            }
            set { _strPeriodFrom = value; }
        }
        private string _strPeriodTo;

        public string StrPeriodTo
        {
            get
            {

                //if (_strPeriodTo == "" || _strPeriodTo == null)
                //{
                //    _strPeriodTo = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);
                //}

                return _strPeriodTo;
            }
            set { _strPeriodTo = value; }
        }

        IList<Shift> lstShift;
        IPagedList<Shift> lstShiftPaged;
        Shift _Shift;
        SelectList _CalculationType;
        public string _Message;

        public Shift Shift
        {
            get
            {
                if (this._Shift == null)
                {
                    this._Shift = new Shift();
                }
                return _Shift;
            }
            set { _Shift = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<Shift> LstShift
        {
            get { return lstShift; }
            set { lstShift = value; }
        }

        public IPagedList<Shift> LstShiftPaged
        {
            get { return lstShiftPaged; }
            set { lstShiftPaged = value; }
        }

        ShiftBLL objBLL = new ShiftBLL();

        /// <summary>
        /// Save
        /// </summary>
        /// 
        public SelectList CalculationType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem item = new SelectListItem();
                item.Value = "Calender Days";
                item.Text = "Calender Days";
                itemList.Add(item);

                item = new SelectListItem();

                item.Value = "Employee Attendance";
                item.Text = "Employee Attendance";
                itemList.Add(item);

                this._CalculationType = new SelectList(itemList, "Value", "Text");
                return _CalculationType;
            }
            set { _CalculationType = value; }
        }

        public int SaveData(ShiftModels model, ref string strmsg)
        {

            int i = 0;

            try
            {
                model.Shift.strEUser = LoginInfo.Current.LoginName;
                if (model.Shift.intShiftID > 0)
                {

                    i = objBLL.Edit(model.Shift, ref strmsg);
                }
                else
                {
                    model.Shift.strIUser = LoginInfo.Current.LoginName;

                    i = objBLL.Add(model.Shift, ref strmsg);
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

        public Shift ShiftGetByID(int Id)
        {
            ShiftModels model = new ShiftModels();

            try
            {
                return model.Shift = objBLL.ShiftGetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetShiftAll()
        {
            LstShift = objBLL.ShiftGetAll();
        }

        public List<Shift> GetShiftPaging(ShiftModels model)
        {
            return objBLL.ShiftGetSrc(model.strShiftName, model.IsRoaster, model.StrPeriodFrom, model.StrPeriodTo);
        }

    }
}