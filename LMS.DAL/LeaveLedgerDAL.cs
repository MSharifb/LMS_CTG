using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class LeaveLedgerDAL
    {
        public static List<LeaveLedger> GetItemList(int intLeaveYearID, int intLeaveTypeID, string strEmpID, string strCompanyID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveLedgerGet");

                List<LeaveLedger> results = new List<LeaveLedger>();
                foreach (DataRow dr in dt.Rows)
                {

                    LeaveLedger obj = new LeaveLedger();

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

        public static List<LeaveLedger> GetLeaveBalanceIndividual(System.Int64 intApplicationID, int intLeaveYearID, int intLeaveTypeID, double fltWithPayDuration, string strEmpID, string strCompanyID, string strApplicationType)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intApplicationID", intApplicationID, DbType.Int64));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@fltWithPayDuration", fltWithPayDuration, DbType.Double));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strApplicationType", strApplicationType, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspLeaveBalanceIndividualGet");

                List<LeaveLedger> results = new List<LeaveLedger>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveLedger obj = new LeaveLedger();

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

        public static List<LeaveLedger> GetLeaveLedgerHistory(System.Int64 intApplicationID, int intLeaveYearID, int intLeaveTypeID, double fltWithPayDuration, string strEmpID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intApplicationID", intApplicationID, DbType.Int64));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspLeaveLedgerHistoryGet");

                List<LeaveLedger> results = new List<LeaveLedger>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveLedger obj = new LeaveLedger();

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


        public static int SaveItem(LeaveLedgerHistory obj, string strMode, IDbTransaction transacrtion, IDbConnection con)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intApplicationID", obj.@intApplicationID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@fltOB", obj.fltOB, DbType.Double));
            cpList.Add(new CustomParameter("@fltEntitlement", obj.fltEntitlement, DbType.Double));
            cpList.Add(new CustomParameter("@fltAvailed", obj.fltAvailed, DbType.Double));
            cpList.Add(new CustomParameter("@fltEncased", obj.fltEncased, DbType.Double));
            cpList.Add(new CustomParameter("@fltapplied", obj.fltapplied, DbType.Double));

            cpList.Add(new CustomParameter("@fltCB", obj.fltCB, DbType.Double));

            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transacrtion, con, "LMS_uspLeaveLedgerHistorySave");

            }
            catch (Exception ex)
            {
                throw ex;
                return -5000;
            }
        }

    }
}
