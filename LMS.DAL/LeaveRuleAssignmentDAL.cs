using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class LeaveRuleAssignmentDAL
    {



        public static List<LeaveRuleAssignment> GetItemList(int intRuleAssignID, int intRuleID, string strEmpName,
                                                            string strEmpInitial, string strCompanyID, int intLeaveTypeID,
                                                            string strDepartmentID, string strDesignationID,string strLocationID,
                                                            string strGender,int intCategoryID, string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {

           
            try
            {              

                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intRuleAssignID", intRuleAssignID, DbType.Int32));
                cpList.Add(new CustomParameter("@intRuleID", intRuleID, DbType.Int32));
                cpList.Add(new CustomParameter("@strEmpName", strEmpName, DbType.String));
                cpList.Add(new CustomParameter("@strEmpInitial", strEmpInitial, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                //cpList.Add(new CustomParameter("@strLocationID", strLocationID, DbType.String));
                cpList.Add(new CustomParameter("@strGender", strGender, DbType.String));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryID, DbType.String));                
                
                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));

                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));              
                
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveRuleAssignmentGet");

                numTotalRows = (int)paramval;

                List<LeaveRuleAssignment> results = new List<LeaveRuleAssignment>();
                foreach (DataRow dr in dt.Rows)
                {

                    LeaveRuleAssignment obj = new LeaveRuleAssignment();

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

        public static int SaveItem(LeaveRuleAssignment obj, string strMode,out string strmessage)
        {

            int intResult = 0;
            strmessage = "";
            object paramval = null;
            DBHelper db = new DBHelper();

            CustomParameterList cpList = new CustomParameterList();


            cpList.Add(new CustomParameter("@intRuleAssignID", obj.intRuleAssignID, DbType.Int32));
            cpList.Add(new CustomParameter("@intRuleID", obj.intRuleID, DbType.Int32));
            cpList.Add(new CustomParameter("@strApplyType", obj.strApplyType, DbType.String));
            cpList.Add(new CustomParameter("@strEmpInitial", obj.strEmpInitial, DbType.String));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.String));
            cpList.Add(new CustomParameter("@strGender", obj.strGender, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            cpList.Add(new CustomParameter("@intCategoryCode", obj.intCategoryCode, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@intEmployeeTypeID", obj.intEmployeeTypeID, DbType.Int32));
            
            //Added For BEPZA
            cpList.Add(new CustomParameter("@GradeFrom", obj.GradeFrom, DbType.Int32));
            cpList.Add(new CustomParameter("@GradeTo", obj.GradeTo, DbType.Int32));
                       
            try
            {
                if (strMode != "D")
                {
                    DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspCheckDuplicateRowLeaveAssign");
                    strmessage = db.ReturnMessage.ToString();

                    if (strmessage.ToString() == "Successful")
                    {
                       // cpList.Remove(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
                        cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
                        cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));

                        intResult = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveRuleAssignmentSave");
                        strmessage = db.ReturnMessage.ToString();
                    }
                    
                }
                else
                {
                    //cpList.Remove(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
                    cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
                    cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));

                    intResult = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveRuleAssignmentSave");
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
