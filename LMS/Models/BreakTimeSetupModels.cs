using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LMS.BLL;
using LMSEntity;
using MvcPaging;

namespace LMS.Web.Models
{
    public class BreakTimeSetupModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        IList<ATT_tblSetBreakTime> lstSetBreakTime;

        IPagedList<ATT_tblSetBreakTime> _lstSetBreakTimePaged;

        ATT_tblSetBreakTime _BreakTimeSetup;

        SelectList _CalculationType;

        private SelectList _Shift;
        private SelectList _BreakType;

        public string _Message;

        public ATT_tblSetBreakTime BreakTimeSetup
        {
            get
            {
                if (this._BreakTimeSetup == null)
                {
                    this._BreakTimeSetup = new ATT_tblSetBreakTime();
                }
                return _BreakTimeSetup;
            }
            set { _BreakTimeSetup = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<ATT_tblSetBreakTime> LstBreakTimeSetup
        {
            get { return lstSetBreakTime; }
            set { lstSetBreakTime = value; }
        }

        public IPagedList<ATT_tblSetBreakTime> LstSetBreakTimePaged
        {
            get { return _lstSetBreakTimePaged; }
            set { _lstSetBreakTimePaged = value; }
        }

        BreakTimeSetupBLL objBLL = new BreakTimeSetupBLL();

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


        public SelectList Shift
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<Shift> lstShift = new List<Shift>();
                ShiftBLL objShiftBLL = new ShiftBLL();

                lstShift = objShiftBLL.ShiftGetAll();

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


        public SelectList BreakType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<BreakType> lstBreakType = new List<BreakType>();
                BreakTypeBLL objBreakTypeBLL = new BreakTypeBLL();

                lstBreakType = objBreakTypeBLL.GetAll();

                foreach (BreakType lt in lstBreakType)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intBreakID.ToString();
                    item.Text = lt.strBreakName;
                    itemList.Add(item);
                }
                this._BreakType = new SelectList(itemList, "Value", "Text");
                return _BreakType;
            }
            set { _BreakType = value; }
        }


        public int SaveData(BreakTimeSetupModels model, ref string strmsg)
        {

            int i = 0;

            try
            {
                model.BreakTimeSetup.strEUser = LoginInfo.Current.LoginName;
                if (model.BreakTimeSetup.intBreakSetID > 0)
                {

                    i = objBLL.Edit(model.BreakTimeSetup, ref strmsg);
                }
                else
                {
                    model.BreakTimeSetup.strIUser = LoginInfo.Current.LoginName;

                    i = objBLL.Add(model.BreakTimeSetup, ref strmsg);
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

        public ATT_tblSetBreakTime BreakTimeSetupGetByID(int Id)
        {
            BreakTimeSetupModels model = new BreakTimeSetupModels();

            try
            {
                return model.BreakTimeSetup = objBLL.BreakTimeSetupGetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetBreakTimeSetupAll()
        {
            LstBreakTimeSetup = objBLL.BreakTimeSetupGetAll();
        }

        public List<ATT_tblSetBreakTime> GetShiftPaging(ATT_tblSetBreakTime objSetBreakTime)
        {
            return objBLL.BreakTimeSetupGetSrc(objSetBreakTime.intBreakSetID);
        }

    }
}