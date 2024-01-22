using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class BreakTimeSetupDAL
    {

        public static List<ATT_tblSetBreakTime> GetItemList(int intBreakSetID)
        {

            try 
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intBreakSetID", intBreakSetID, DbType.String));
               
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "ATT_uspBreakTimeSetupGet");

                List<ATT_tblSetBreakTime> results = new List<ATT_tblSetBreakTime>();
                foreach (DataRow dr in dt.Rows)
                {
                    ATT_tblSetBreakTime obj = new ATT_tblSetBreakTime();

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

        public static int SaveItem(ATT_tblSetBreakTime obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intBreakSetID", obj.intBreakSetID, DbType.Int32));	
            cpList.Add(new CustomParameter("@intBreakID", obj.intBreakID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtStartTime", obj.strStartTime , DbType.String));
            cpList.Add(new CustomParameter("@dtEndTime", obj.strEndTime , DbType.String));
            cpList.Add(new CustomParameter("@intShiftID", obj.intShiftID, DbType.Int32));		

            cpList.Add(new CustomParameter("@strIUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));			
           
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspBreakTimeSetupSave");
            }
            catch (Exception ex)
            {
                throw ex;               
            }
        }
    }
}
