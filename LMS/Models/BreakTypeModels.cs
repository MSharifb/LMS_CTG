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
    public class BreakTypeModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;         

        private BreakType _BreakType;
        private IPagedList<BreakType> _LstBreakTypePaging;
        private IList<BreakType> lstBreakType;

        BreakTypeBLL objBLL = new BreakTypeBLL();

        private int _intSearchLeaveTypeId;
        public int intSearchLeaveTypeId
        {
            get { return _intSearchLeaveTypeId; }
            set { _intSearchLeaveTypeId = value; }
        }

       
        public BreakType BreakType
        {
            get
            {
                if (this._BreakType == null)
                {
                    this._BreakType = new BreakType();
                }
                return _BreakType;
            }
            set { _BreakType = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<BreakType> LstBreakType
        {
            get { return lstBreakType; }
            set { lstBreakType = value; }
        }
        public IPagedList<BreakType> LstBreakTypePaging
        {
            get { return _LstBreakTypePaging; }
            set { _LstBreakTypePaging = value; }
        }               

        public int SaveData(BreakTypeModels model)
        {
            int returnid = 0;

            try
            {
                model.BreakType.strEUser = LoginInfo.Current.LoginName;
                if (model.BreakType.intBreakID > 0)
                {

                    returnid = objBLL.Edit(model.BreakType);
                }
                else
                {
                    model.BreakType.strIUser = LoginInfo.Current.LoginName;

                    returnid = objBLL.Add(model.BreakType);

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

        public BreakType Get(int Id)
        {
            BreakTypeModels model = new BreakTypeModels();

            try
            {

                return model.BreakType = objBLL.GetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get(int Id, string strBreakName)
        {
            lstBreakType = objBLL.Get(Id, strBreakName);
        }

        public List<BreakType> GetBreakTypePaging(BreakType obj)
        {
            return objBLL.Get(obj.intBreakID, obj.strBreakName);
        }
       
    }
}