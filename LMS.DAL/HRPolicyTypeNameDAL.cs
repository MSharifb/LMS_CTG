using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using LMSEntity;
using System.Data;

namespace LMS.DAL
{
    public class HRPolicyTypeNameDAL
    {
        public static List<HRPolicyTypeName> GetItemList(int HRPolicyTypeNameID, string HRPolicyTypeName,int startRow, int maxRows, out int P)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@HRPOLICYTYPENAMEID", HRPolicyTypeNameID, DbType.Int32));
                cpList.Add(new CustomParameter("@HRPOLICYTYPENAME", HRPolicyTypeName, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startRow, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", maxRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OSM_USPHRPOLICYTYPENAMEGET");
                P = (int)paramval;

                List<HRPolicyTypeName> results = new List<HRPolicyTypeName>();
                foreach (DataRow dr in dt.Rows)
                {
                    HRPolicyTypeName obj = new HRPolicyTypeName();
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


        public static int SaveItem(HRPolicyTypeName obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@HRPOLICYTYPENAMEID", obj.HRPOLICYTYPENAMEID, DbType.Int32));
            cpList.Add(new CustomParameter("@HRPOLICYTYPENAME", obj.HRPOLICYTYPENAME, DbType.String));
            cpList.Add(new CustomParameter("@REMARKS", obj.REMARKS, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OSM_USPHRPOLICYTYPENAMESAVE");
            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }

        }
    }
}
