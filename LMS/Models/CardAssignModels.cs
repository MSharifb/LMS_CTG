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
    public class CardAssignModels
    {

        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        public string _Message;         

        private CardAssign _CardAssign;
        private IPagedList<CardAssign> _LstCardAssignPaging;
        private IList<CardAssign> lstCardAssign;

        CardAssignBLL objBLL = new CardAssignBLL();

        private string _strCardID;
        public string strCardID
        {
            get { return _strCardID; }
            set { _strCardID = value; }
        }

        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }
       
        public CardAssign CardAssign
        {
            get
            {
                if (this._CardAssign == null)
                {
                    this._CardAssign = new CardAssign();
                }
                return _CardAssign;
            }
            set { _CardAssign = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<CardAssign> LstCardAssign
        {
            get { return lstCardAssign; }
            set { lstCardAssign = value; }
        }
        public IPagedList<CardAssign> LstCardAssignPaging
        {
            get { return _LstCardAssignPaging; }
            set { _LstCardAssignPaging = value; }
        }               

        public int SaveData(CardAssignModels model)
        {
            int returnid = 0;

            try
            {
                model.CardAssign.strEUser = LoginInfo.Current.LoginName;
                if (model.CardAssign.intCardAssignID > 0)
                {

                    returnid = objBLL.Edit(model.CardAssign);
                }
                else
                {
                    model.CardAssign.strIUser = LoginInfo.Current.LoginName;

                    returnid = objBLL.Add(model.CardAssign);

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

        public CardAssign Get(int Id)
        {
            CardAssignModels model = new CardAssignModels();

            try
            {

                return model.CardAssign = objBLL.GetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get(int Id, string strAssignID, string strEmpID, string strCardID, string dtEffectiveDate)
        {
            lstCardAssign = objBLL.Get(Id, strAssignID, strEmpID,strCardID,dtEffectiveDate);
        }

        public List<CardAssign> GetCardAssignPaging(CardAssign obj)
        {
            return objBLL.Get(obj.intCardAssignID, obj.strAssignID, obj.strEmpID, "",obj.strEffectiveDate);
        }

        #region employee info
        
        string employeeName;
        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }
        string strEmpID;
        public string StrEmpID
        {
            get { return strEmpID; }
            set { strEmpID = value; }
        }
        private string strDesignation;
        public string StrDesignation
        {
            get { return strDesignation; }
            set { strDesignation = value; }
        }

        private string _strDepartment;
        public string StrDepartment
        {
            get { return _strDepartment; }
            set { _strDepartment = value; }
        }

        public Employee GetEmployeeInfo(string Id)
        {
            EmployeeBLL objEmpBLL = new EmployeeBLL();

            try
            {
                return objEmpBLL.EmployeeGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}