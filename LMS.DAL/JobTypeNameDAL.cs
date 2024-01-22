using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using LMSEntity;
using System.Data;


namespace LMS.DAL
{
    public class JobTypeNameDAL
    {
        public static List<JobTypeName> GetItemList(int JobTypeNameID, string jobTypeName, int startRow, int maxRows, out int P)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@JOBTYPENAMEID", JobTypeNameID, DbType.Int32));
                cpList.Add(new CustomParameter("@JOBTYPENAME", jobTypeName, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startRow, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", maxRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OSM_USPJOBTYPENAMEGET");
                P = (int)paramval;

                List<JobTypeName> results = new List<JobTypeName>();
                foreach (DataRow dr in dt.Rows)
                {
                    JobTypeName obj = new JobTypeName();
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


        public static int SaveItem(JobTypeName obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@JOBTYPENAMEID", obj.JOBTYPENAMEID, DbType.Int32));
            cpList.Add(new CustomParameter("@JOBTYPENAME", obj.JOBTYPENAME, DbType.String));
            cpList.Add(new CustomParameter("@REMARKS", obj.REMARKS, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OSM_USPJOBTYPENAMESAVE");
            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }

        }
    }
}
