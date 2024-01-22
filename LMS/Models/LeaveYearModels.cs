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
    public class LeaveYearModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;
        private bool _IsExists;
        private SelectList _LeaveYearType;
        private LeaveYear _LeaveYear;
        private IList<LeaveYear> lstLeaveYear;
        private List<LeaveYearType> lstLeaveYearType;
        private LeaveYearBLL objBLL = new LeaveYearBLL();
        private IPagedList<LeaveYear> _LstLeaveYearPaging;

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

        public LeaveYear LeaveYear
        {
            get
            {
                if (this._LeaveYear == null)
                {
                    this._LeaveYear = new LeaveYear();
                }
                return _LeaveYear;
            }
            set { _LeaveYear = value; }
        }

        private int _intSearchLeaveYearTypeId;
        public int intSearchLeaveYearTypeId
        {
            get { return _intSearchLeaveYearTypeId; }
            set { _intSearchLeaveYearTypeId = value; }
        }

        public IList<LeaveYear> LstLeaveYear
        {
            get { return lstLeaveYear; }
            set { lstLeaveYear = value; }
        }

        public List<LeaveYearType> LstLeaveYearType
        {
            get
            {
                if (lstLeaveYearType == null)
                {
                    lstLeaveYearType = new List<LeaveYearType>();
                }
                return lstLeaveYearType;
            }
            set { lstLeaveYearType = value; }
        }

        //private List<LeaveYear> _LstLeaveYear;
        //public List<LeaveYear> LstLeaveYear
        //{
        //    get
        //    {
        //        if (_LstLeaveYear == null)
        //        {
        //            _LstLeaveYear = new List<LeaveYear>();
        //        }
        //        return _LstLeaveYear;
        //    }
        //    set { _LstLeaveYear = value; }
        //}

        public IPagedList<LeaveYear> LstLeaveYearPaging
        {
            get { return _LstLeaveYearPaging; }
            set { _LstLeaveYearPaging = value; }
        }

        public int SaveData(LeaveYearModels model)
        {

            int i = -1;

            try
            {
                
                model.LeaveYear.strEUser = LoginInfo.Current.LoginName;
                model.LeaveYear.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveYear.intLeaveYearID > 0)
                {

                    i = objBLL.Edit(model.LeaveYear);
                }
                else
                {

                    model.LeaveYear.strIUser = LoginInfo.Current.LoginName;

                    i = objBLL.Add(model.LeaveYear);
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

        public LeaveYear GetLeaveYear(int Id)
        {

            LeaveYearModels model = new LeaveYearModels();

            try
            {
                return model.LeaveYear = objBLL.LeaveYearGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetLeaveYearAll(int intSearchLeaveYearTypeId)
        {
            lstLeaveYear = objBLL.LeaveYearGetAll(intSearchLeaveYearTypeId,LoginInfo.Current.strCompanyID);
        }

        public List<LeaveYear> GetLeaveYearPaging(LeaveYear objLeaveYear)
        {
            return objBLL.LeaveYearGet(objLeaveYear.intLeaveYearID, objLeaveYear.strYearTitle, LoginInfo.Current.strCompanyID);
        }


        public SelectList LeaveYearType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                LstLeaveYearType = Common.fetchLeaveYearType();

                foreach (LeaveYearType lt in lstLeaveYearType)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intLeaveYearTypeId.ToString();
                    item.Text = lt.LeaveYearTypeName;
                    itemList.Add(item);
                }
                this._LeaveYearType = new SelectList(itemList, "Value", "Text");
                return _LeaveYearType;
            }
            set { _LeaveYearType = value; }
        }

    }
}