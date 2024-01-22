using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class EmployeeDAL
    {

        public static List<Employee> GetItemList(string strEmpInitial, string strEmpID, string strEmpName, string ActiveStatus, string strDepartmentID, string strDesignationID, string strDesignation, string strGender, string strReligionID, string strCompanyID, string strSearchType, string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpInitial", strEmpInitial, DbType.String));
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
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspEmployeeGet");
                numTotalRows =(int)paramval;
                List<Employee> results = new List<Employee>();
                foreach (DataRow dr in dt.Rows)
                {
                    Employee obj = new Employee();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }
                return results.OrderBy(x => x.strEmpInitial).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int SaveItem(Employee obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@strEmpName", obj.strEmpName, DbType.String));
            cpList.Add(new CustomParameter("@dtJoiningDate", obj.dtJoiningDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtConfirmationDate", obj.dtConfirmationDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtInactiveDate", obj.dtInactiveDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.String));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strGender", obj.strGender, DbType.String));
            cpList.Add(new CustomParameter("@strReligion", obj.strReligion, DbType.String));
            cpList.Add(new CustomParameter("@strLocationID", obj.strLocationID, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strEmail", obj.strEmail, DbType.String));
            cpList.Add(new CustomParameter("@strSupervisorID", obj.strSupervisorID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspEmployeeSave");
            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }
    }
}
