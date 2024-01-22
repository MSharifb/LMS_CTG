using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class CompanyDAL
    {

        public static List<Company> GetItemList(string strCompanyID, string strCompany)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strCompany", strCompany, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspCompanyGet");

                List<Company> results = new List<Company>();
                foreach (DataRow dr in dt.Rows)
                {
                    Company obj = new Company();
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
        
        public static int SaveItem(Company obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCompany", obj.strCompany, DbType.String));
            cpList.Add(new CustomParameter("@strAddress", obj.strAddress, DbType.DateTime));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspCompanySave");
            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }

        }

    }
}
