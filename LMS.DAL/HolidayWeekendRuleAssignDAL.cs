using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class HolidayWeekendRuleAssignDAL
    {
        public static List<HolidayWeekendRuleAssign> GetItemList(int Id, int intRuleID, string strEmpName, string strEmpInitial,
                                                                 int intYearId, string strDepartmentID, string strDesignationID,
                                                                 string strReligionID, string strCompanyID, int intCategoryID, string strSortBy,
                                                                 string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intRuleAssignID", Id, DbType.Int32));
                cpList.Add(new CustomParameter("@intHolidayRuleID", intRuleID, DbType.Int32));
                cpList.Add(new CustomParameter("@strEmpName", strEmpName, DbType.String));
                cpList.Add(new CustomParameter("@strEmpInitial", strEmpInitial, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveYearID", intYearId, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strReligionID", strReligionID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryID, DbType.String));

                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));

                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekendRuleAssignGet");
                numTotalRows = (int)paramval;

                List<HolidayWeekendRuleAssign> results = new List<HolidayWeekendRuleAssign>();
                foreach (DataRow dr in dt.Rows)
                {
                    HolidayWeekendRuleAssign obj = new HolidayWeekendRuleAssign();

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

        public static int SaveItem(HolidayWeekendRuleAssign obj, string strMode, out string strmessage)
        {
            int intResult = -1;
            strmessage = "";
            object paramval = null;

            DBHelper db = new DBHelper();
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            cpList.Add(new CustomParameter("@intRuleAssignID", obj.intRuleAssignID, DbType.Int32));
            cpList.Add(new CustomParameter("@intHolidayRuleID", obj.intHolidayRuleID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.String));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strReligionID", obj.strReligionID, DbType.String));
            //cpList.Add(new CustomParameter("@strLocationID", obj.strLocationID, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@intCategoryCode", obj.intCategoryCode, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@strApplyTo", obj.strApplyTo, DbType.String));

            try
            {
                if (strMode != "D")
                {
                    DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspCheckDuplicateRowWeekendAssign");
                    strmessage = db.ReturnMessage.ToString();
                    if (strmessage == "Successful")
                    {
                        cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
                        cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));

                        intResult = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekendRuleAssignSave");
                        strmessage = db.ReturnMessage.ToString();
                    }

                }
                else
                {

                    cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
                    cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));

                    intResult = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekendRuleAssignSave");
                    strmessage = db.ReturnMessage.ToString();
                }
            }
            catch (Exception ex)
            {
                strmessage = ex.Message;
            }
            return intResult;
        }

    }
}
