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
    public class OfficeTimeModels
    {

        int _rowID;
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private bool _blnTextBlank;
        private string _Message;
        private SelectList _LeaveYear;
        private OfficeTime _OfficeTime;
        private OfficeTimeDetails _OfficeTimeDetails;

        private IList<OfficeTime> lstOfficeTime;
        private List<OfficeTimeDetails> lstOfficeTimeDetails;
        private OfficeTimeBLL objBLL = new OfficeTimeBLL();

        IPagedList<OfficeTime> _LstOfficeTimePaging;

        public int rowID
        {
            get { return _rowID; }
            set { _rowID = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public bool BlnTextBlank
        {
            get { return _blnTextBlank; }
            set { _blnTextBlank = value; }
        }


        public OfficeTime OfficeTime
        {
            get
            {
                if (_OfficeTime == null)
                {
                    _OfficeTime = new OfficeTime();
                }
                return _OfficeTime;
            }
            set { _OfficeTime = value; }
        }

        public OfficeTimeDetails OfficeTimeDetails
        {
            get
            {
                if (_OfficeTimeDetails == null)
                {
                    _OfficeTimeDetails = new OfficeTimeDetails();
                }
                return _OfficeTimeDetails;
            }
            set { _OfficeTimeDetails = value; }
        }
        
        
        public IList<OfficeTime> LstOfficeTime
        {
            get { return lstOfficeTime; }
            set { lstOfficeTime = value; }
        }

        public List<OfficeTimeDetails> LstOfficeTimeDetails
        {
            get { return lstOfficeTimeDetails; }
            set { lstOfficeTimeDetails = value; }
        }

        public IPagedList<OfficeTime> LstOfficeTimePaging
        {
            get { return _LstOfficeTimePaging; }
            set { _LstOfficeTimePaging = value; }
        }





        public SelectList LeaveYear
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<LeaveYear> lstLeaveYear = new List<LeaveYear>();

                //lstLeaveYear = Common.fetchLeaveYear();

                lstLeaveYear = Common.fetchLeaveYearForOfficeHour();

                foreach (LeaveYear lt in lstLeaveYear)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.intLeaveYearID.ToString();
                    item.Text = lt.strYearTitle;
                    itemList.Add(item);
                }
                this._LeaveYear = new SelectList(itemList, "Value", "Text", this.OfficeTime.intLeaveYearID);

                return _LeaveYear;
            }
            set { _LeaveYear = value; }
        }


        public int SaveData(OfficeTimeModels model, ref string strmsg)
        {
            int i = 0;
            string strMode = "";
            OfficeTimeBLL objBll = new OfficeTimeBLL();
            try
            {
                model.OfficeTime.strEUser = LoginInfo.Current.LoginName;

                if (model.rowID > 0)
                {
                    strMode = "U";
                    //i = objBll.Edit(model.OfficeTime, ref strmsg);
                }
                else
                {
                    strMode = "I";
                    model.OfficeTime.strIUser = LoginInfo.Current.LoginName;
                    model.OfficeTime.strCompanyID = LoginInfo.Current.strCompanyID;

                    //i = objBll.Add(model.OfficeTime, ref strmsg);
                }
                i = objBll.SaveOfficeTime(model.OfficeTime,model.LstOfficeTimeDetails,strMode, ref strmsg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public int Delete(int yearId)
        {
            int i = 0;
            try
            {
                i = objBLL.Delete(LoginInfo.Current.strCompanyID, yearId);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        //public OfficeTime GetOfficeTime(string yearId)
        //{
        //    OfficeTimeModels model = new OfficeTimeModels();

        //    try
        //    {

        //        return model.OfficeTime = objBLL.OfficeTimeGet(LoginInfo.Current.strCompanyID, int.Parse(yearId));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        
        public void GetOfficeTime(string yearId)
        {
            OfficeTimeModels model = new OfficeTimeModels();

            try
            {
                OfficeTime = objBLL.OfficeTimeGet(LoginInfo.Current.strCompanyID, int.Parse(yearId));
                LstOfficeTimeDetails = objBLL.OfficeTimeDetailsGet(int.Parse(yearId));                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
        public void GetOfficeTimeAll()
        {
            lstOfficeTime = objBLL.OfficeTimeGetAll(LoginInfo.Current.strCompanyID);
        }

    }
}
