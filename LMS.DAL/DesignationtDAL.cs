using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class DesignationDAL
    {

        public static List<Designation> GetItemList(string strDesignationID, string strDesignation, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignation", strDesignation, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspDesignationGet");

                List<Designation> results = new List<Designation>();
                foreach (DataRow dr in dt.Rows)
                {

                    Designation obj = new Designation();

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


        public static int SaveItem(Designation obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.Int32));
            cpList.Add(new CustomParameter("@strDesignation", obj.strDesignation, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.DateTime));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspDesignationSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }

        }

    }
}
