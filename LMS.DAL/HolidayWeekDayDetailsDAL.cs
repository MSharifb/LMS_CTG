using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class HolidayWeekDayDetailsDAL
    {
        public static List<HolidayWeekDayDetails> GetItemList(int Id, int intMasterID, string strType, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intHolidayWeekendDetailsID", Id, DbType.Int32));
                cpList.Add(new CustomParameter("@intHolidayWeekendMasterID", intMasterID, DbType.Int32));
                
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDayDetailsGet");

                List<HolidayWeekDayDetails> results = new List<HolidayWeekDayDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    HolidayWeekDayDetails obj = new HolidayWeekDayDetails();

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

        public static int SaveItem(HolidayWeekDayDetails obj, string strMode, IDbTransaction transaction, IDbConnection con)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intHolidayWeekendDetailsID", obj.intHolidayWeekendMasterID, DbType.Int32));
            cpList.Add(new CustomParameter("@intHolidayWeekendMasterID", obj.intHolidayWeekendMasterID, DbType.Int32));            
            cpList.Add(new CustomParameter("@strDay", obj.strDay, DbType.String));
            cpList.Add(new CustomParameter("@intDuration", obj.intDuration, DbType.Int32));
            cpList.Add(new CustomParameter("@isAlternateDay", obj.isAlternateDay, DbType.Boolean));
            cpList.Add(new CustomParameter("@isFromFirstWeekend", obj.isFromFirstWeekend, DbType.Boolean));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();

                if (transaction != null)
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction, con, "LMS_uspHolidayWeekendDetailsSave");
                }
                else
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekendDetailsSave");
                }

               // return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekendDetailsSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }
    }
}
