using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class LeaveEntitlementDAL
    {
        public static void EntitlementProcess(LeaveEntitlement obj, out string strmessage)
        {
            strmessage = "";
            int intResult = 0;
            DBHelper db = new DBHelper();
            CustomParameterList cpList = new CustomParameterList();

            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID1", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));

            try
            {
                intResult = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transection, con, "LMS_uspLeaveEntitlement");
                strmessage = db.ReturnMessage.ToString();
                transection.Commit();
            }
            catch (Exception ex)
            {
                transection.Rollback();
                strmessage = ex.Message;
            }

        }

        public static void EntitlementRollback(LeaveEntitlement obj, out string strmessage)
        {
            strmessage = "";
            int intResult = 0;
            DBHelper db = new DBHelper();
            CustomParameterList cpList = new CustomParameterList();

            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));

            try
            {
                intResult = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transection, con, "LMS_uspLeaveEntitlementRollback");
                strmessage = db.ReturnMessage.ToString();
                transection.Commit();
            }
            catch (Exception ex)
            {
                transection.Rollback();
                strmessage = ex.Message;
            }

        }
    }
}
