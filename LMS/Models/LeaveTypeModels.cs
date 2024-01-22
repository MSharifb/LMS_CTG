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
    public class LeaveTypeModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        private SelectList _LeaveYearType;
        private SelectList _ApprovalGroup;
        IList<LeaveType> lstLeaveType;
        IPagedList<LeaveType> lstLeaveType1;
        private List<LeaveYearType> lstLeaveYearType;
        private List<ApprovalGroup> lstApprovalGroup;
        LeaveType _LeaveType;
        SelectList _CalculationType;
        SelectList _EarnLeaveType;
        SelectList _LeaveTypeList;
        List<LeaveTypeDeduct> _LeaveTypeDeduct;

        public string _Message;

        public List<LeaveTypeDeduct> LeaveTypeDeduct
        {
            get
            {
                if (this._LeaveTypeDeduct == null)
                {
                    this._LeaveTypeDeduct = new List<LeaveTypeDeduct>();
                }
                return _LeaveTypeDeduct;
            }
            set { _LeaveTypeDeduct = value; }
        }

        public LeaveType LeaveType
        {
            get
            {
                if (this._LeaveType == null)
                {
                    this._LeaveType = new LeaveType();
                }
                return _LeaveType;
            }
            set { _LeaveType = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<LeaveType> LstLeaveType
        {
            get { return lstLeaveType; }
            set { lstLeaveType = value; }
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

        public List<ApprovalGroup> LstApprovalGroup
        {
            get
            {
                if (lstApprovalGroup == null)
                {
                    lstApprovalGroup = new List<ApprovalGroup>();
                }
                return lstApprovalGroup;
            }
            set { lstApprovalGroup = value; }
        }

        public IPagedList<LeaveType> LstLeaveType1
        {
            get { return lstLeaveType1; }
            set { lstLeaveType1 = value; }
        }

        LeaveTypeBLL objBLL = new LeaveTypeBLL();

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
                item.Value = "Calendar Days";
                item.Text = "Calendar Days";
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

        public int SaveData(LeaveTypeModels model, ref string strmsg)
        {

            int i = 0;
            int d = 0;

            LeaveTypeBLL objBll = new LeaveTypeBLL();
            try
            {
                model.LeaveType.strEUser = LoginInfo.Current.LoginName;
                if (model.LeaveType.intLeaveTypeID > 0)
                {
                    i = objBll.Edit(model.LeaveType, ref strmsg);
                    if(model.LeaveType.intLeaveTypeAddID > 0)
                        if(model.LeaveTypeDeduct.Where(e=> e.intDeductLeaveTypeID == model.LeaveType.intLeaveTypeAddID).ToList().Count == 0)
                            model.LeaveTypeDeduct.Add(new LeaveTypeDeduct { intLeaveTypeDeductID = 0, intDeductLeaveTypeID = model.LeaveType.intLeaveTypeAddID, intLeaveTypeID = model.LeaveType.intLeaveTypeID });
                }
                else
                {
                    model.LeaveType.strIUser = LoginInfo.Current.LoginName;
                    model.LeaveType.strCompanyID = LoginInfo.Current.strCompanyID;

                    i = objBll.Add(model.LeaveType, ref strmsg);
                    if (model.LeaveType.intLeaveTypeAddID > 0)
                        model.LeaveTypeDeduct.Add(new LeaveTypeDeduct { intLeaveTypeDeductID = 0, intDeductLeaveTypeID = model.LeaveType.intLeaveTypeAddID, intLeaveTypeID = i });
                }

                var leaveTypelst = Common.fetchLeaveType(); 

                foreach (var item in model.LeaveTypeDeduct)
                {
                    
                    var exists = leaveTypelst.Where(e => e.intLeaveTypeID == item.intDeductLeaveTypeID).FirstOrDefault();
                    if (item.intLeaveTypeDeductID > 0)
                    {
                        item.strIUser = LoginInfo.Current.LoginName;
                        d = objBll.EditDeductLeaveType(item, ref strmsg);
                    }
                    else
                    {
                        item.strEUser = LoginInfo.Current.LoginName;
                        d = objBll.AddDeductLeaveType(item, ref strmsg);
                        item.intLeaveTypeDeductID = d;
                    }
                    
                    item.strLeaveType = exists.strLeaveType;
                }

                model.LeaveType.intLeaveTypeID = i;
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

        public LeaveType GetLeaveType(int Id)
        {


            LeaveTypeModels model = new LeaveTypeModels();

            try
            {
                return model.LeaveType = objBLL.LeaveTypeGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<LeaveTypeDeduct> GetDeductedLeaveType(int Id)
        {


            LeaveTypeModels model = new LeaveTypeModels();

            try
            {
                return model.LeaveTypeDeduct = objBLL.DeductedLeaveGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetLeaveTypeAll()
        {
            lstLeaveType = objBLL.LeaveTypeGetAll(LoginInfo.Current.strCompanyID).OrderBy(x=>x.isServiceLifeType).ThenBy(c=> c.strLeaveType).ToList();
        }

        public List<LeaveType> GetLeaveTypePaging(LeaveType objLeaveType)
        {
            return objBLL.LeaveTypeGet(objLeaveType.intLeaveTypeID, objLeaveType.strLeaveType, objLeaveType.strLeaveShortName, LoginInfo.Current.strCompanyID);
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

        public SelectList LeaveTypeList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<LeaveType> list = Common.fetchLeaveType();

                foreach (LeaveType lt in list)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intLeaveTypeID.ToString();
                    item.Text = lt.strLeaveType;
                    itemList.Add(item);
                }
                this._LeaveTypeList = new SelectList(itemList, "Value", "Text");
                return _LeaveTypeList;
            }
            set { _LeaveTypeList = value; }

        }

        public SelectList AprovalGroupType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                LstApprovalGroup = Common.fetchApprovalGroup();

                foreach (ApprovalGroup lt in lstApprovalGroup)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intApprovalGroupId.ToString();
                    item.Text = lt.ApprovalGroupName;
                    itemList.Add(item);
                }
                this._ApprovalGroup = new SelectList(itemList, "Value", "Text");
                return _ApprovalGroup;
            }
            set { _ApprovalGroup = value; }
        }

        public SelectList EarnLeaveType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem item = new SelectListItem();
                item.Value = "1";
                item.Text = "Full Average";
                itemList.Add(item);

                item = new SelectListItem();

                item.Value = "2";
                item.Text = "Half Average";
                itemList.Add(item);

                this._EarnLeaveType = new SelectList(itemList, "Value", "Text");
                return _EarnLeaveType;
            }
            set { _EarnLeaveType = value; }
        }


        public int DeleteDedectedLeaveType(int Id)
        {

            int i = 0;

            try
            {

                i = objBLL.DeleteDedectedLeaveType(Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return i;
        }
    }
}