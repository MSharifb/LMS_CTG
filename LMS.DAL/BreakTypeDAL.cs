using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class BreakTypeDAL
    {
        public static List<BreakType> Get(int intBreakID, string strBreakName)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intBreakID", intBreakID, DbType.Int32));
                cpList.Add(new CustomParameter("@strBreakName", strBreakName, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspBreakTypeGet");

                List<BreakType> results = new List<BreakType>();
                foreach (DataRow dr in dt.Rows)
                {

                    BreakType obj = new BreakType();

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

        public static int Save(BreakType obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intBreakID", obj.intBreakID, DbType.Int32));
            cpList.Add(new CustomParameter("@strBreakName", obj.strBreakName, DbType.String));
            cpList.Add(new CustomParameter("@strDescription", obj.strDescription, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspBreakTypeSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
