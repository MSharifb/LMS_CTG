using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using LMSEntity;
using System.Data;

namespace LMS.DAL
{
    public class SkillTypeNameDAL
    {
        public static List<SkillTypeName> GetItemList(int SkillTypeNameID, string SkillTypeName, int startRow, int maxRows, out int P)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@SKILLTYPENAMEID", SkillTypeNameID, DbType.Int32));
                cpList.Add(new CustomParameter("@SKILLTYPENAME", SkillTypeName, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startRow, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", maxRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OSM_USPSKILLTYPENAMEGET");
                P = (int)paramval;

                List<SkillTypeName> results = new List<SkillTypeName>();
                foreach (DataRow dr in dt.Rows)
                {
                    SkillTypeName obj = new SkillTypeName();
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


        public static int SaveItem(SkillTypeName obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@SKILLTYPENAMEID", obj.SKILLTYPENAMEID, DbType.Int32));
            cpList.Add(new CustomParameter("@SKILLTYPENAME", obj.SKILLTYPENAME, DbType.String));
            cpList.Add(new CustomParameter("@REMARKS", obj.REMARKS, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OSM_USPSKILLTYPENAMESAVE");
            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }

        }
    }
}
