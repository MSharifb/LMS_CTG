using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class ATT_AtdStatusSetupDAL
    {

        public static List<ATT_tblAtdStatusSetup> GetItemList(int intRowID)
        {

            try 
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intRowID", intRowID, DbType.String));
               
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "ATT_uspAtdStatusSetupGet");
               
                List<ATT_tblAtdStatusSetup> results = new List<ATT_tblAtdStatusSetup>();
                foreach (DataRow dr in dt.Rows)
                {
                    ATT_tblAtdStatusSetup obj = new ATT_tblAtdStatusSetup();

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

        public static int SaveItem(ATT_tblAtdStatusSetup obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intRowID", obj.intRowID, DbType.Int32));
            cpList.Add(new CustomParameter("@strPresent", obj.strPresent, DbType.String));
            cpList.Add(new CustomParameter("@strEarlyArrival", obj.strEarlyArrival, DbType.String));
            cpList.Add(new CustomParameter("@strEarlyDeparture", obj.strEarlyDeparture, DbType.String));
            cpList.Add(new CustomParameter("@strLateArrival", obj.strLateArrival, DbType.String));
            cpList.Add(new CustomParameter("@strLateDeparture", obj.strLateDeparture, DbType.String));
            cpList.Add(new CustomParameter("@strAbsent", obj.strAbsent, DbType.String));
            cpList.Add(new CustomParameter("@strOSD", obj.strOSD, DbType.String));
            cpList.Add(new CustomParameter("@strLeave", obj.strLeave, DbType.String));
            cpList.Add(new CustomParameter("@strWeekend", obj.strWeekend, DbType.String));
            cpList.Add(new CustomParameter("@strHoliday", obj.strHoliday, DbType.String));

            cpList.Add(new CustomParameter("@strIUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));			
           
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAtdStatusSetupSave");
            }
            catch (Exception ex)
            {
                throw ex;               
            }
        }
    }
}
