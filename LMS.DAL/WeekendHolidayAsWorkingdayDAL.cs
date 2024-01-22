using System;
using System.Collections.Generic;
using System.Data;
using LMSEntity;
using TVL.DB;
namespace LMS.DAL
{
    public class WeekendHolidayAsWorkingdayDAL
    {
        public static List<WeekendHolidayAsWorkingday> GetItemList(Int32 intWeekendWorkingday, string strEffectiveDateFrom, string strEffectiveDateTo, string strDeclarationDate)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intWeekendWorkingday", intWeekendWorkingday, DbType.Int32));                
                cpList.Add(new CustomParameter("@strEffectiveDateFrom", strEffectiveDateFrom, DbType.String));
                cpList.Add(new CustomParameter("@strEffectiveDateTo", strEffectiveDateTo, DbType.String));
                cpList.Add(new CustomParameter("@strDeclarationDate", strDeclarationDate, DbType.String));
                

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspWeekendHolidayAsWorkingdayGet");

                List<WeekendHolidayAsWorkingday> results = new List< WeekendHolidayAsWorkingday>();
                foreach (DataRow dr in dt.Rows)
                {

                    WeekendHolidayAsWorkingday obj = new WeekendHolidayAsWorkingday();

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

        public static int SaveItem(WeekendHolidayAsWorkingday obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intWeekendWorkingday", obj.intWeekendWorkingday, DbType.Int32));
            cpList.Add(new CustomParameter("@dtEffectiveDateFrom", obj.dtEffectiveDateFrom, DbType.String));
            cpList.Add(new CustomParameter("@dtEffectiveDateTo", obj.dtEffectiveDateTo, DbType.String));
            cpList.Add(new CustomParameter("@dtDeclarationDate", obj.dtDeclarationDate, DbType.String));
            cpList.Add(new CustomParameter("@strDescription", obj.strDescription, DbType.String));            
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspWeekendHolidayAsWorkingdaySave");
            }
            catch 
            {
                return -5000;
            }

        }

    }
}
