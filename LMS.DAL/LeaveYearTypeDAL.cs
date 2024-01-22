using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class LeaveYearTypeDAL
    {
        public static List<LeaveYearType> GetItemList(int intLeaveYearTypeID, string LeaveYearType, string StartMonth, string EndMonth, string strCompanyID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intLeaveYearTypeID", intLeaveYearTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@LeaveYearType", LeaveYearType, DbType.String));
                cpList.Add(new CustomParameter("@StartMonth", StartMonth, DbType.String));
                cpList.Add(new CustomParameter("@EndMonth", EndMonth, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveYearTypeGet");

                List<LeaveYearType> results = new List<LeaveYearType>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveYearType obj = new LeaveYearType();
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


        public static int SaveItem(LeaveYearType obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intLeaveYearTypeId", obj.intLeaveYearTypeId , DbType.Int32));
            cpList.Add(new CustomParameter("@LeaveYearType", obj.LeaveYearTypeName, DbType.String));
            cpList.Add(new CustomParameter("@StartMonth", obj.StartMonth, DbType.String));
            cpList.Add(new CustomParameter("@EndMonth", obj.EndMonth, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));            
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveYearTypeSave");


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
