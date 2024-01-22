using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class HRMColumnMappingDAL
    {
        public static List<HRMColumnMapping> GetItemList(Int32 intColumnID, Int32 intTableID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intColumnID", intColumnID, DbType.Int32));
                cpList.Add(new CustomParameter("@intTableID", intTableID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHRMColumnMappingGet");

                List<HRMColumnMapping> results = new List<HRMColumnMapping>();
                foreach (DataRow dr in dt.Rows)
                {
                    HRMColumnMapping obj = new HRMColumnMapping();
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

        public static int SaveItem(HRMColumnMapping obj, string strMode, out string strmsg)
        {
            strmsg = "";
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intColumnID", obj.ColumnID, DbType.Int32));
            cpList.Add(new CustomParameter("@intTableID", obj.TableID, DbType.Int32));
            cpList.Add(new CustomParameter("@strColumnName", obj.ColumnName, DbType.String));
            cpList.Add(new CustomParameter("@strSourceColumnName", obj.SourceColumnName, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHRMColumnMappingSave");
            }
            catch (Exception ex)
            {
                strmsg = ex.Message;
                return -5000;
            }
        }


        public static void SaveItemList(List<HRMColumnMapping> objList, string strMode, out string strmsg)
        {
            strmsg = "";
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            foreach (HRMColumnMapping obj in objList)
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intColumnID", obj.ColumnID, DbType.Int32));
                cpList.Add(new CustomParameter("@intTableID", obj.TableID, DbType.Int32));
                cpList.Add(new CustomParameter("@strColumnName", obj.ColumnName, DbType.String));
                cpList.Add(new CustomParameter("@strSourceColumnName", obj.SourceColumnName, DbType.String));
                cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

                try
                {
                    DBHelper db = new DBHelper();
                    db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transection, con, "LMS_uspHRMColumnMappingSave");

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
