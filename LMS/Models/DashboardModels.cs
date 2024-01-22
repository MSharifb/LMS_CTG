using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.BLL;
using LMSEntity;
using System.Data;
using MvcPaging;
using System.Web.Mvc;
using MvcContrib.Pagination;
using LMS.DAL;

namespace LMS.Web.Models
{
    public class DashboardModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;         

        private CardInfo _CardInfo;
        private IPagedList<CardInfo> _LstCardInfoPaging;
        private IList<CardInfo> lstCardInfo;
        private SelectList _CardStatus;

        CardInfoBLL objBLL = new CardInfoBLL();

        public int startRowIndex
        {
            get { return _startRowIndex; }
            set { _startRowIndex = value; }
        }

        public int maximumRows
        {
            get { return _maximumRows; }
            set { _maximumRows = value; }
        }

        public int numTotalRows
        {
            get { return _numTotalRows; }
            set { _numTotalRows = value; }
        }

       
        public CardInfo CardInfo
        {
            get
            {
                if (this._CardInfo == null)
                {
                    this._CardInfo = new CardInfo();
                }
                return _CardInfo;
            }
            set { _CardInfo = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<CardInfo> LstCardInfo
        {
            get { return lstCardInfo; }
            set { lstCardInfo = value; }
        }
        public IPagedList<CardInfo> LstCardInfoPaging
        {
            get { return _LstCardInfoPaging; }
            set { _LstCardInfoPaging = value; }
        }

        private int _intSearchStatus;
        public int intSearchStatus
        {
            get { return _intSearchStatus; }
            set { _intSearchStatus = value; }
        }
        public SelectList CardStatus
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                SelectListItem list = new SelectListItem();
                itemList.Add(new SelectListItem() { Text = "...All...", Value = "-1" });

                list.Text = "Available";
                list.Value = "0";
                itemList.Add(list);

                list = new SelectListItem();
                list.Text = "Assigned";
                list.Value = "1";
                itemList.Add(list);

                this._CardStatus = new SelectList(itemList, "Value", "Text");
                return _CardStatus;
            }
            set { _CardStatus = value; }
        }

        public int SaveData(CardInfoModels model)
        {
            int returnid = 0;

            try
            {
                model.CardInfo.strEUser = LoginInfo.Current.LoginName;
                if (model.CardInfo.intCardID > 0)
                {

                    returnid = objBLL.Edit(model.CardInfo);
                }
                else
                {
                    model.CardInfo.strIUser = LoginInfo.Current.LoginName;

                    returnid = objBLL.Add(model.CardInfo);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return returnid;

        }


        public int Delete(int Id)
        {

            int returnid = 0;

            try
            {

                returnid = objBLL.Delete(Id);
            }

            catch (Exception ex)
            {

                throw ex;
            }

            return returnid;
        }               

        public CardInfo Get(int Id)
        {
            CardInfoModels model = new CardInfoModels();

            try
            {

                return model.CardInfo = objBLL.GetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get(int Id, string strBreakName, int intStatus)
        {
            lstCardInfo = objBLL.Get(Id, strBreakName, intStatus);
        }

        public List<CardInfo> GetCardInfoPaging(CardInfo obj)
        {
            return objBLL.Get(obj.intCardID, obj.strCardID, obj.intStatus);
        }
       
    }
}