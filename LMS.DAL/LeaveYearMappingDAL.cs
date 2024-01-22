using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
   public class LeaveYearMappingDAL
    {

       public static List<LeaveYearMapping> GetItemList(int intLeaveYearMapID, int intLeaveYearId, int intLeaveTypeID, bool bitIsActiveYear, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intLeaveYearMapID", intLeaveYearMapID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveYearId", intLeaveYearId, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@bitIsActiveYear", bitIsActiveYear, DbType.Boolean));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveYearMapGet");

                List<LeaveYearMapping> results = new List<LeaveYearMapping>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveYearMapping obj = new LeaveYearMapping();
                    MapperBase.GetInstance().MapItem(obj, dr);
                    results.Add(obj);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

       public static int SaveItem(LeaveYearMapping obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intLeaveYearMapID", obj.intLeaveYearMapID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearId", obj.intLeaveYearId, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@bitIsActiveYear", obj.bitIsActiveYear, DbType.Boolean));

            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            //cpList.Add(new CustomParameter("@numErrorCode", 0, DbType.Int32));
            //cpList.Add(new CustomParameter("@strErrorMsg", "", DbType.String));
           
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveYearMapSave");
            }
            catch (Exception ex)
            {
                throw ex;
                return -5000;
            }

        }

    }
}
