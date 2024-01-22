using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LMS.BLL;
using LMSEntity;
using MvcPaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
using LMS.DAL;

namespace LMS.Web.Models
{
    public class ManualIOModels
    {

        public string strAttendDateFrom
        { get; set; }

        public string strAttendDateTo
        { get; set; }

 
        public string strEmpID
        { get; set; }

        IList<ManualIO> lstManualIO;
        IPagedList<ManualIO> lstManualIOPaged;
        ManualIO _ManualIO;
        SelectList _CalculationType;
        public string _Message;


        private SelectList _Shift;
        private SelectList _BreakType;

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


        public ManualIO ManualIO
        {
            get
            {
                if (this._ManualIO == null)
                {
                    this._ManualIO = new ManualIO();
                }
                return _ManualIO;
            }
            set { _ManualIO = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public IList<ManualIO> LstManualIO
        {
            get { return lstManualIO; }
            set { lstManualIO = value; }
        }

        public IPagedList<ManualIO> LstManualIOPaged
        {
            get { return lstManualIOPaged; }
            set { lstManualIOPaged = value; }
        }

        ManualIOBLL objBLL = new ManualIOBLL();

        /// <summary>
        /// Save
        /// </summary>
        /// 
        public int SaveData(ManualIOModels model, ref string strmsg)
        {

            int i = 0;

            try
            {
                model.ManualIO.strEUser = LoginInfo.Current.LoginName;
                if (model.ManualIO.intRowID > 0)
                {

                    i = objBLL.Edit(model.ManualIO, ref strmsg);
                }
                else
                {
                    model.ManualIO.strIUser = LoginInfo.Current.LoginName;

                    i = objBLL.Add(model.ManualIO, ref strmsg);
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

        public ManualIO ManualIOGetByID(int Id)
        {
            ManualIOModels model = new ManualIOModels();

            try
            {
                return model.ManualIO = objBLL.ManualIOGetByID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetManualIOAll()
        {
            LstManualIO = objBLL.ManualIOGetAll();
        }

        public List<ManualIO> GetManualIOPaging(ManualIOModels model)
        {
            return objBLL.ManualIOGetSrc(0, model.strEmpID, model.ManualIO.strEmpName,  model.strAttendDateFrom, model.strAttendDateTo, model.ManualIO.intShiftID );
        }


        public Employee GetEmployeeInfo(string Id)
        {
           
            try
            {
                return EmployeeGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private Employee EmployeeGet(string Id)
        {
            int p = 0;
            List<Employee> empList = GetItemList(Id, "", "", "", "", "", "", "", "", "", "strEmpID", "asc", 1, 1, out p);
            if (empList.Count > 0)
            {
                return empList.Single();
            }
            else
            {
                return null;
            }
        }

        private static List<Employee> GetItemList(string strEmpID, string strEmpName, string ActiveStatus, string strDepartmentID, string strDesignationID, string strDesignation, string strGender, string strReligionID, string strCompanyID, string strSearchType, string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strEmpName", strEmpName, DbType.String));
                cpList.Add(new CustomParameter("@ActiveStatus", ActiveStatus, DbType.String));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignation", strDesignation, DbType.String));
                cpList.Add(new CustomParameter("@strReligionID", strReligionID, DbType.String));
                cpList.Add(new CustomParameter("@strGender", strGender, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strSearchType", strSearchType, DbType.String));
                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.String));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.String));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "ATT_uspEmployeeGet");
                numTotalRows = (int)paramval;
                List<Employee> results = new List<Employee>();
                foreach (DataRow dr in dt.Rows)
                {
                    Employee obj = new Employee();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



    }
}