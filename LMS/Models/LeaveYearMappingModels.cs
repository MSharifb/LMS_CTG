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
    public class LeaveYearMappingModels
    {
        LeaveYearMappingBLL objBLL = new LeaveYearMappingBLL();

        IList<LeaveYearMapping> lstLeaveYearMapping;
        IPagedList<LeaveYearMapping> lstLeaveYearMappingPaging;
        LeaveYearMapping _leaveYearMapping;

        private SelectList _LeaveType;
        private SelectList _LeaveYear;

        public string _Message;
        private List<LeaveType> lstLeaveType;
        private List<LeaveYear> lstLeaveYear; 

        int _intLeaveTypeID;
        public int intLeaveTypeID
        {
            get { return _intLeaveTypeID; }
            set { _intLeaveTypeID = value; }
        }

        int _intLeaveYearId;
        public int intLeaveYearId
        {
            get { return _intLeaveYearId; }
            set { _intLeaveYearId = value; }
        }

        public IList<LeaveYearMapping> LstLeaveYearMapping
        {
            get { return lstLeaveYearMapping; }
            set { lstLeaveYearMapping = value; }
        }

        public IPagedList<LeaveYearMapping> LstLeaveYearMappingPaging
        {
            get { return lstLeaveYearMappingPaging; }
            set { lstLeaveYearMappingPaging = value; }
        }

        public LeaveYearMapping LeaveYearMapping
        {
            get
            {
                if (this._leaveYearMapping == null)
                {
                    this._leaveYearMapping = new LeaveYearMapping();
                }
                return _leaveYearMapping;
            }
            set { _leaveYearMapping = value; }
        }
        
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
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

        public List<LeaveYear> LstLeaveYear
        {
            get
            {
                if (lstLeaveYear == null)
                {
                    lstLeaveYear = new List<LeaveYear>();
                }
                return lstLeaveYear;
            }
            set { lstLeaveYear = value; }
        }


        #region Save Leave Year Mapping

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <param name="strmsg"></param>
        /// <returns></returns>
        public int SaveData(LeaveYearMappingModels model, ref string strmsg)
        {
            int i = 0;

            LeaveYearMappingBLL objBll = new LeaveYearMappingBLL();
            try
            {
                model.LeaveYearMapping.strIUser = LoginInfo.Current.LoginName;
                model.LeaveYearMapping.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.LeaveYearMapping.intLeaveYearMapID > 0)
                {
                    i = objBll.Edit(model.LeaveYearMapping, ref strmsg);
                }
                else
                {
                    i = objBll.Add(model.LeaveYearMapping, ref strmsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
        
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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

        public LeaveYearMapping GetLeaveYearMapping(int Id) 
        {
            LeaveYearMappingModels model = new LeaveYearMappingModels();
            try
            {
                return model.LeaveYearMapping = objBLL.LeaveYearMappingGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetLeaveYearMappingAll(LeaveYearMapping model) 
        {
            LstLeaveYearMapping = objBLL.LeaveYearMappingGetAll(model, LoginInfo.Current.strCompanyID).ToList();
        }

        public List<LeaveYearMapping> GetLeaveTypeMappingPaging(LeaveYearMapping objLeaveYearMapping)
        {
            return objBLL.LeaveYearMappingGet(objLeaveYearMapping.intLeaveYearMapID, objLeaveYearMapping.intLeaveYearId, objLeaveYearMapping.intLeaveTypeID, LoginInfo.Current.strCompanyID);
        }

        #endregion


        #region Utilities

        public SelectList LeaveTypeList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                LstLeaveType = Common.fetchLeaveType().Where(x=>x.intLeaveYearTypeId >0).ToList();

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

        public SelectList LeaveYears 
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                LstLeaveYear = Common.fetchLeaveYear().Where(x => x.intLeaveYearTypeId > 0).ToList();

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

        public SelectList LeaveYearList
        {
            get
            {
                return _LeaveYear;
            }
            set { _LeaveYear = value; }
        }

       

        #endregion
    }
}