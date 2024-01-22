using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class SetOverTimeDAL
    {

        public static List<SetOverTime> Get(int intRowID, string strEmpID, string strCompanyID, string strLocationID,
            string strDesignationID, string strDepartmentID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo)
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
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryCode, DbType.Int32));
                cpList.Add(new CustomParameter("@dtPeriodFrom", dtPeriodFrom, DbType.String));
                cpList.Add(new CustomParameter("@dtPeriodTo", dtPeriodTo, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspSetOverTimeGet");

                List<SetOverTime> results = new List<SetOverTime>();
                foreach (DataRow dr in dt.Rows)
                {

                    SetOverTime obj = new SetOverTime();

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

        public static int Save(SetOverTime obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intRowID", obj.intRowID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strLocationID", obj.strLocationID, DbType.String));
            cpList.Add(new CustomParameter("@intCategoryCode", obj.intCategoryCode, DbType.String));
            cpList.Add(new CustomParameter("@intCalOTAfterMinute", obj.intCalOTAfterMinute, DbType.Int32));
            cpList.Add(new CustomParameter("@bitConsiderEarlyArrival", obj.bitConsiderEarlyArrival, DbType.Boolean));           
            cpList.Add(new CustomParameter("@intCalMinDuration", obj.intCalMinDuration, DbType.Int32));
            cpList.Add(new CustomParameter("@mnyMaxOTHour", obj.mnyMaxOTHour, DbType.Decimal));
            cpList.Add(new CustomParameter("@mnyMinOTHour", obj.mnyMinOTHour, DbType.Decimal));
            cpList.Add(new CustomParameter("@mnyOTCeilingAmount", obj.mnyOTCeilingAmount, DbType.Decimal));
            cpList.Add(new CustomParameter("@bitFromJoiningDate", obj.bitFromJoiningDate, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitFromConfirmationDate", obj.bitFromConfirmationDate, DbType.Boolean));
            cpList.Add(new CustomParameter("@dtPeriodFrom", obj.dtPeriodFrom, DbType.String));
            cpList.Add(new CustomParameter("@dtPeriodTo", obj.dtPeriodTo, DbType.String));
            
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspSetOverTimeSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
