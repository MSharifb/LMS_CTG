using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class DepartmentDAL
    {

        public static List<Department> GetItemList(string strDepartmentID, string strDepartment, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strDepartmentID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strDepartment", strDepartment, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspDepartmentGet");

                List<Department> results = new List<Department>();
                foreach (DataRow dr in dt.Rows)
                {

                    Department obj = new Department();

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

        public static List<Zone> GetZoneList()
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspZoneGet");

                List<Zone> results = new List<Zone>();
                foreach (DataRow dr in dt.Rows)
                {

                    Zone obj = new Zone();

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
        

        public static int SaveItem(Department obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.Int32));
            cpList.Add(new CustomParameter("@strDepartment", obj.strDepartment, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.DateTime));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspDepartmentSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }

        }

    }
}
