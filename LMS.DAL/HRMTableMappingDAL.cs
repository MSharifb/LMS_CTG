using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class HRMTableMappingDAL
    {
        public static List<HRMTableMapping> GetItemList(Int32 intTableID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intTableID", intTableID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHRMTableMappingGet");

                List<HRMTableMapping> results = new List<HRMTableMapping>();
                foreach (DataRow dr in dt.Rows)
                {
                    HRMTableMapping obj = new HRMTableMapping();
                    MapperBase.GetInstance().MapItem(obj, dr); 
                    results.Add(obj);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int SaveItem(HRMTableMapping obj, string strMode, out string strmsg)
        {
            strmsg = "";
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intTableID", obj.TableID, DbType.Int32));
            cpList.Add(new CustomParameter("@strTableName", obj.TableName, DbType.String));
            cpList.Add(new CustomParameter("@strSourceTableName", obj.SourceTableName, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHRMTableMappingSave");
            }
            catch (Exception ex)
            {
                strmsg = ex.Message;
                return -5000;
            }
        }


        public static void SaveItemList(List<HRMTableMapping> objList, string strMode, out string strmsg)
        {
            strmsg = "";
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            foreach (HRMTableMapping obj in objList)
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intTableID", obj.TableID, DbType.Int32));
                cpList.Add(new CustomParameter("@strTableName", obj.TableName, DbType.String));
                cpList.Add(new CustomParameter("@strSourceTableName", obj.SourceTableName, DbType.String));
                cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

                try
                {
                    DBHelper db = new DBHelper();
                    db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transection, con, "LMS_uspHRMTableMappingSave");

                }
                catch (Exception ex)
                {
                    transection.Rollback();
                    strmsg = ex.Message;
                }
            }
            transection.Commit();
        }
    }
}
