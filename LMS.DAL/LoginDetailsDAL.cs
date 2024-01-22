using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class LoginDetailsDAL
    {
        public static List<LoginDetails> GetItemList(string strEmpID, string strCompanyID)
        {
            try
            {
                List<LoginDetails> results = new List<LoginDetails>();
                results = GetItemList(strEmpID, strCompanyID, 0);
                
                //CustomParameterList cpList = new CustomParameterList();
                //cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                //cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));

                //object paramval = null;
                //DBHelper db = new DBHelper();
                //DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLoginInfoGet");

                //List<LoginDetails> results = new List<LoginDetails>();
                //foreach (DataRow dr in dt.Rows)
                //{
                //    LoginDetails obj = new LoginDetails();

                //    MapperBase.GetInstance().MapItem(obj, dr); ;
                //    results.Add(obj);
                //}
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static List<LoginDetails> GetItemList(string strEmpID, string strCompanyID, int loggedInZone)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@intLoggedInZoneId", loggedInZone, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLoginInfoGetWithLoggedInZoneName");

                List<LoginDetails> results = new List<LoginDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    LoginDetails obj = new LoginDetails();

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
