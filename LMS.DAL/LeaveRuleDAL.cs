using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class LeaveRuleDAL
    {
        public static List<LeaveRule> GetItemList(int intRuleID, string strRuleName, int intLeaveTypeID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intRuleID", intRuleID, DbType.Int32));
                cpList.Add(new CustomParameter("@strRuleName", strRuleName, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveRuleGet");

                List<LeaveRule> results = new List<LeaveRule>();
                foreach (DataRow dr in dt.Rows)
                {

                    LeaveRule obj = new LeaveRule();

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

        public static LeaveRule GetEmployeeWiseRule(string strempId, int intleavetypeId, string strcompanyId)
        {

            LeaveRule obj = new LeaveRule();
            try
            {

                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strEmpID", strempId, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intleavetypeId, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strcompanyId, DbType.String));
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspGetEmployeeLeaveTypeWiseRule");

                if (dt.Rows.Count > 0)
                {
                    MapperBase.GetInstance().MapItem(obj, dt.Rows[0]); ;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return obj;
        }

        public static int SaveItem(LeaveRule obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intRuleID", obj.intRuleID, DbType.Int32));
            cpList.Add(new CustomParameter("@strRuleName", obj.strRuleName, DbType.String));
            cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@fltEntitlement", obj.fltEntitlement, DbType.Double));
            cpList.Add(new CustomParameter("@strCalculationFrom", obj.strCalculationFrom, DbType.String));
            cpList.Add(new CustomParameter("@strEligibleAfter", obj.strEligibleAfter, DbType.String));
            cpList.Add(new CustomParameter("@bitIsEncashable", obj.bitIsEncashable, DbType.Boolean));

            cpList.Add(new CustomParameter("@intMaxEncahDays", obj.intMaxEncahDays, DbType.Int32));
            cpList.Add(new CustomParameter("@intMinDaysInHand", obj.intMinDaysInHand, DbType.Int32));
            cpList.Add(new CustomParameter("@bitIsCarryForward", obj.bitIsCarryForward, DbType.Boolean));
            cpList.Add(new CustomParameter("@intMaxCarryForwardDays", obj.intMaxCarryForwardDays, DbType.Int32));
            cpList.Add(new CustomParameter("@strLeaveObsoluteMonth", obj.strLeaveObsoluteMonth, DbType.String));
            cpList.Add(new CustomParameter("@bitIsIncludeHoliday", obj.bitIsIncludeHoliday, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitIsIncludeWeekend", obj.bitIsIncludeWeekend, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitIsIncludeWHForWOP", obj.bitIsIncludeWHForWOP, DbType.Boolean));
            cpList.Add(new CustomParameter("@intMaxLeaveDaysInApplication", obj.intMaxLeaveDaysInApplication, DbType.Int32));
            cpList.Add(new CustomParameter("@intMaxLeaveAppInMonth", obj.intMaxLeaveAppInMonth, DbType.Int32));
            cpList.Add(new CustomParameter("@intMaxLeaveDaysInMonth", obj.@intMaxLeaveDaysInMonth, DbType.Int32));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            //----------------------------------------------------------------------------------------------
            cpList.Add(new CustomParameter("@intAdjustLeaveTypeID", obj.intAdjustLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@bitIsEnjoyAtaTime", obj.bitIsEnjoyAtaTime, DbType.Boolean));
            cpList.Add(new CustomParameter("@strAllowType", obj.strAllowType, DbType.String));
            cpList.Add(new CustomParameter("@intEligibleAfterMonth", obj.intEligibleAfterMonth, DbType.Int32));
            cpList.Add(new CustomParameter("@intCalculateAfterMonth", obj.intCalculateAfterMonth, DbType.Int32));

            //-------------------------- Added for MPA-------------------------------------------------------------
            cpList.Add(new CustomParameter("@intEarnLeaveUnitForDays", obj.intEarnLeaveUnitForDays, DbType.Int32));
            cpList.Add(new CustomParameter("@intMaxValidReasonDays", obj.intMaxValidReasonDays, DbType.Int32));
            cpList.Add(new CustomParameter("@intDeductionLeaveTypeID", obj.intDeductionLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@intMaxDeductionDays", obj.intMaxDeductionDays, DbType.Int32));
            cpList.Add(new CustomParameter("@strDeductionAllowType", obj.strDeductionAllowType, DbType.String));
            
            // END MPA

            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            // ------------------- Added FOR BEPZA----------------------------------------------
            cpList.Add(new CustomParameter("@intNextEligibleAfterMonth", obj.intNextEligibleAfterMonth, DbType.Int32));
            cpList.Add(new CustomParameter("@strNextEligibleFrom", obj.strNextEligibleFrom, DbType.String));
            // END

            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveRuleSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
