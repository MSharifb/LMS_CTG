using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using LMSEntity;
using System.Data;

namespace LMS.DAL
{
    public class MembershipTypeNameDAL
    {

        public static List<MembershipTypeName> GetItemList(int MembershipTypeNameID, string MembershipTypeName, int startRow, int maxRows, out int P)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@MEMBERSHIPTYPENAMEID", MembershipTypeNameID, DbType.Int32));
                cpList.Add(new CustomParameter("@MEMBERSHIPTYPENAME", MembershipTypeName, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startRow, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", maxRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OSM_USPMEMBERSHIPTYPENAMEGET");
                P = (int)paramval;

                List<MembershipTypeName> results = new List<MembershipTypeName>();
                foreach (DataRow dr in dt.Rows)
                {
                    MembershipTypeName obj = new MembershipTypeName();
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


        public static int SaveItem(MembershipTypeName obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@MEMBERSHIPTYPENAMEID", obj.MEMBERSHIPTYPENAMEID, DbType.Int32));
            cpList.Add(new CustomParameter("@MEMBERSHIPTYPENAME", obj.MEMBERSHIPTYPENAME, DbType.String));
            cpList.Add(new CustomParameter("@REMARKS", obj.REMARKS, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OSM_USPMEMBERSHIPTYPENAMESAVE");
            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }

        }
    }
}
