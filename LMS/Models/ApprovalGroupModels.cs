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
    public class ApprovalGroupModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;
        private bool _IsExists;

        private ApprovalGroup _ApprovalGroup;
        private IList<ApprovalGroup> lstApprovalGroup;
        private ApprovalGroupBLL objBLL = new ApprovalGroupBLL();
        private IPagedList<ApprovalGroup> _LstApprovalGroupPaging;
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

        public ApprovalGroup ApprovalGroup
        {
            get
            {
                if (this._ApprovalGroup == null)
                {
                    this._ApprovalGroup = new ApprovalGroup();
                }
                return _ApprovalGroup;
            }
            set { _ApprovalGroup = value; }
        }

        public IList<ApprovalGroup> LstApprovalGroup
        {
            get { return lstApprovalGroup; }
            set { lstApprovalGroup = value; }
        }

        public IPagedList<ApprovalGroup> LstApprovalGroupPaging
        {
            get { return _LstApprovalGroupPaging; }
            set { _LstApprovalGroupPaging = value; }
        }

        public int SaveData(ApprovalGroupModels model, ref string strmsg)
        {

            int i = -1;

            try
            {

                model.ApprovalGroup.strEUser = LoginInfo.Current.LoginName;
                model.ApprovalGroup.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.ApprovalGroup.intApprovalGroupId > 0)
                {

                    i = objBLL.Edit(model.ApprovalGroup, ref strmsg);
                }
                else
                {

                    model.ApprovalGroup.strIUser = LoginInfo.Current.LoginName;

                    i = objBLL.Add(model.ApprovalGroup, ref strmsg);
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

        public ApprovalGroup GetApprovalGroup(int Id)
        {

            ApprovalGroupModels model = new ApprovalGroupModels();

            try
            {
                return model.ApprovalGroup = objBLL.ApprovalGroupGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetApprovalGroupAll()
        {
            lstApprovalGroup = objBLL.ApprovalGroupGetAll(LoginInfo.Current.strCompanyID);
        }

        public List<ApprovalGroup> GetApprovalGroupPaging(ApprovalGroup objApprovalGroup)
        {
            return objBLL.ApprovalGroupGet(objApprovalGroup.intApprovalGroupId, objApprovalGroup.ApprovalGroupName, LoginInfo.Current.strCompanyID);
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