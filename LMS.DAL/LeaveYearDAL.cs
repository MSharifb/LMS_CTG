using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class LeaveYearDAL
    {
        public static List<LeaveYear> GetItemList(int intLeaveYearID, int intLeaveYearTypeId, string strYearTitle, int bitIsActiveYear, string strCompanyID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveYearTypeId", intLeaveYearTypeId, DbType.Int32));
                cpList.Add(new CustomParameter("@strYearTitle", strYearTitle, DbType.String));
                cpList.Add(new CustomParameter("@bitIsActiveYear", bitIsActiveYear, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveYearGet");

                List<LeaveYear> results = new List<LeaveYear>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveYear obj = new LeaveYear();
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


        public static int SaveItem(LeaveYear obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearTypeId", obj.intLeaveYearTypeId, DbType.Int32));
            cpList.Add(new CustomParameter("@strYearTitle", obj.strYearTitle, DbType.String));
            cpList.Add(new CustomParameter("@dtStartDate", obj.dtStartDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtEndDate", obj.dtEndDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@bitIsActiveYear", obj.bitIsActiveYear, DbType.Boolean));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveYearSave");


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
