using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class ShiftAssignmentDAL
    {
        public static List<ShiftAssignment> Get(int intShiftAssignmentID, string strEmpID, int intShiftID, string dtEffectiveDate)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intShiftAssignmentID", intShiftAssignmentID, DbType.Int32));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@intShiftID", intShiftID, DbType.Int32));
                cpList.Add(new CustomParameter("@dtEffectiveDate", dtEffectiveDate, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspShiftAssignmentGet");

                List<ShiftAssignment> results = new List<ShiftAssignment>();
                foreach (DataRow dr in dt.Rows)
                {

                    ShiftAssignment obj = new ShiftAssignment();

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

        public static int Save(ShiftAssignment obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intShiftAssignmentID", obj.intShiftAssignmentID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));

            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strLocationID", obj.strLocationID, DbType.String));
            cpList.Add(new CustomParameter("@intCategoryCode", obj.intCategoryCode, DbType.String));

            cpList.Add(new CustomParameter("@intShiftID", obj.intShiftID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtEffectiveDate", obj.dtEffectiveDate, DbType.Date));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspShiftAssignmentSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
