using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class WeekendHolidayWorkingHourDAL
    {

        public static List<WeekendHolidayWorkingHour> Get(int intRowID, string strEmpID, string strCompanyID, string strLocationID,
            string strDesignationID, string strDepartmentID, int intReligionID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo,
            string strWHType, int intShiftID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intRowID", intRowID, DbType.Int32));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strLocationID", strLocationID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@intReligionID", intReligionID, DbType.Int32));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryCode, DbType.Int32));
                cpList.Add(new CustomParameter("@dtPeriodFrom", dtPeriodFrom, DbType.String));
                cpList.Add(new CustomParameter("@dtPeriodTo", dtPeriodTo, DbType.String));
                cpList.Add(new CustomParameter("@strWHType", strWHType, DbType.String));
                cpList.Add(new CustomParameter("@intShiftID", intShiftID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspWeekendHolidayWorkingHourGet");

                List<WeekendHolidayWorkingHour> results = new List<WeekendHolidayWorkingHour>();
                foreach (DataRow dr in dt.Rows)
                {

                    WeekendHolidayWorkingHour obj = new WeekendHolidayWorkingHour();

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

        public static int Save(WeekendHolidayWorkingHour obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intRowID", obj.intRowID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@intReligionID", obj.intReligionID, DbType.Int32));
            cpList.Add(new CustomParameter("@strLocationID", obj.strLocationID, DbType.String));
            cpList.Add(new CustomParameter("@intCategoryCode", obj.intCategoryCode, DbType.String));
            cpList.Add(new CustomParameter("@strWHType", obj.strWHType, DbType.String));
            cpList.Add(new CustomParameter("@intShiftID", obj.intShiftID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtPeriodFrom", obj.dtPeriodFrom, DbType.String));
            cpList.Add(new CustomParameter("@dtPeriodTo", obj.dtPeriodTo, DbType.String));
            cpList.Add(new CustomParameter("@dtInTime", obj.dtInTime, DbType.String));
            cpList.Add(new CustomParameter("@dtOutTime", obj.dtOutTime, DbType.String));
            cpList.Add(new CustomParameter("@intGraceInMin", obj.intGraceInMin, DbType.Int32));
            cpList.Add(new CustomParameter("@intGraceOutMin", obj.intGraceOutMin, DbType.Int32));
            cpList.Add(new CustomParameter("@intAbsentMin", obj.intAbsentMin, DbType.Int32));
            cpList.Add(new CustomParameter("@intMinWorkingHour", obj.intMinWorkingHour, DbType.Int32));
            cpList.Add(new CustomParameter("@dtHalfTime", obj.dtHalfTime, DbType.String));
            cpList.Add(new CustomParameter("@intBreakTime", obj.intBreakTime, DbType.Int32));
            cpList.Add(new CustomParameter("@dtAlternativeHoliday", obj.dtAlternativeHoliday, DbType.String));
            
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspWeekendHolidayWorkingHourSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
