using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class AttRuleDAL
    {
        public static List<ATT_tblRule> Get(int intRuleID, string dtEffectiveDate)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intRuleID", intRuleID, DbType.Int32));
                cpList.Add(new CustomParameter("@dtEffectiveDate", dtEffectiveDate, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttRuleGet");

                List<ATT_tblRule> results = new List<ATT_tblRule>();
                foreach (DataRow dr in dt.Rows)
                {

                    ATT_tblRule obj = new ATT_tblRule();

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

        public static int Save(ATT_tblRule obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intRuleID", obj.intRuleID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strLocationID", obj.strLocationID, DbType.String));
            cpList.Add(new CustomParameter("@intCategoryCode", obj.intCategoryCode, DbType.String));
            cpList.Add(new CustomParameter("@intShiftID", obj.intShiftID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtEffectiveDate", obj.dtEffectiveDate, DbType.Date));
            cpList.Add(new CustomParameter("@intWorkingHourRule", obj.intWorkingHourRule, DbType.Int32));
            cpList.Add(new CustomParameter("@intEarlyArrival", obj.intEarlyArrival, DbType.Int32));
            cpList.Add(new CustomParameter("@intLateArrival", obj.intLateArrival, DbType.Int32));
            cpList.Add(new CustomParameter("@intEarlyDeparture", obj.intEarlyDeparture, DbType.Int32));
            cpList.Add(new CustomParameter("@intLateDeparture", obj.intLateDeparture, DbType.Int32));
            cpList.Add(new CustomParameter("@intOverTimeRule", obj.intOverTimeRule, DbType.Int32));
            cpList.Add(new CustomParameter("@btConsiderEarlyArrive", obj.intWorkingHourRule, DbType.Int32));
            

            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttRuleSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
