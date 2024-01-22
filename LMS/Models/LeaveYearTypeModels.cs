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
    public class LeaveYearTypeModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;
        private bool _IsExists;

        private LeaveYearType _LeaveYearType;
        private IList<LeaveYearType> lstLeaveYearType;
        private LeaveYearTypeBLL objBLL = new LeaveYearTypeBLL();
        private IPagedList<LeaveYearType> _LstLeaveYearTypePaging;
        private SelectList _monthList;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public bool IsExists
        {
            get { return _IsExists; }
            set { _IsExists = value; }
        }

        public LeaveYearType LeaveYearType
        {
            get
            {
                if (this._LeaveYearType == null)
                {
                    this._LeaveYearType = new LeaveYearType();
                }
                return _LeaveYearType;
            }
            set { _LeaveYearType = value; }
        }

        public IList<LeaveYearType> LstLeaveYearType
        {
            get { return lstLeaveYearType; }
            set { lstLeaveYearType = value; }
        }

        public IPagedList<LeaveYearType> LstLeaveYearTypePaging
        {
            get { return _LstLeaveYearTypePaging; }
            set { _LstLeaveYearTypePaging = value; }
        }

        public int SaveData(LeaveYearTypeModels model, ref string strmsg)
        {

            int i = -1;

            try
            {

                model.LeaveYearType.strEUser = LoginInfo.Current.LoginName;
                model.LeaveYearType.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveYearType.intLeaveYearTypeId > 0)
                {

                    i = objBLL.Edit(model.LeaveYearType,ref strmsg);
                }
                else
                {

                    model.LeaveYearType.strIUser = LoginInfo.Current.LoginName;

                    i = objBLL.Add(model.LeaveYearType, ref strmsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Delete(int Id)
        {

            int i = -1;

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

        public LeaveYearType GetLeaveYearType(int Id)
        {

            LeaveYearTypeModels model = new LeaveYearTypeModels();

            try
            {
                return model.LeaveYearType = objBLL.LeaveYearTypeGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetLeaveYearTypeAll()
        {
            lstLeaveYearType = objBLL.LeaveYearTypeGetAll(LoginInfo.Current.strCompanyID);
        }

        public List<LeaveYearType> GetLeaveYearPaging(LeaveYearType objLeaveYear)
        {
            return objBLL.LeaveYearTypeGet(objLeaveYear.intLeaveYearTypeId, objLeaveYear.LeaveYearTypeName, objLeaveYear.StartMonth ,objLeaveYear.EndMonth, LoginInfo.Current.strCompanyID);
        }

        public SelectList MonthList
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
                    list.Value = NextMont.ToString("MMMM");
                    //list.Value = NextMont.Month.ToString();
                    itemList.Add(list);
                }

                this._monthList = new SelectList(itemList, "Value", "Text");
                return _monthList;
            }
            set { _monthList = value; }
        }

    }
}