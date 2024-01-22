using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class OfficeTimeDetailsDAL
    {
        public static List<OfficeTimeDetails> GetItemList(int intLeaveYearID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOfficeTimeDetailsGet");

                List<OfficeTimeDetails> results = new List<OfficeTimeDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    OfficeTimeDetails obj = new OfficeTimeDetails();

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

        public static int SaveItem(OfficeTimeDetails obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intDurationID", obj.intDurationID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@strDurationName", obj.strDurationName, DbType.String));
            cpList.Add(new CustomParameter("@strStartTime", obj.strStartTime, DbType.String));
            cpList.Add(new CustomParameter("@strEndTime", obj.strEndTime, DbType.String));
            cpList.Add(new CustomParameter("@fltDuration", obj.fltDuration, DbType.Decimal));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOfficeTimeDetailsSave");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static int SaveItem(OfficeTimeDetails obj, string strMode, IDbTransaction transaction, IDbConnection conn)
        {
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intDurationID", obj.intDurationID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@strDurationName", obj.strDurationName, DbType.String));
            cpList.Add(new CustomParameter("@strStartTime", obj.strStartTime, DbType.String));
            cpList.Add(new CustomParameter("@strEndTime", obj.strEndTime, DbType.String));
            cpList.Add(new CustomParameter("@fltDuration", obj.fltDuration, DbType.Decimal));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction, conn, "LMS_uspOfficeTimeDetailsSave");

            }
            catch (Exception ex)
            {

                throw ex;

                return -5000;
            }
        }




    }
}
