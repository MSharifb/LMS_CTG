using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveRuleAssignmentBLL
    {
        public int Add(LeaveRuleAssignment objLeaveRuleAssignment, out string strmessage)
        {
            return LeaveRuleAssignmentDAL.SaveItem(objLeaveRuleAssignment, "I", out strmessage);

        }
        public int Edit(LeaveRuleAssignment objLeaveRuleAssignment, out string strmessage)
        {

            return LeaveRuleAssignmentDAL.SaveItem(objLeaveRuleAssignment, "U", out strmessage);
        }
        public int Delete(int Id, out string strmessage)
        {
            LeaveRuleAssignment obj = new LeaveRuleAssignment();
            obj.intRuleAssignID = Id;

            return LeaveRuleAssignmentDAL.SaveItem(obj, "D", out strmessage);
        }

        public LeaveRuleAssignment LeaveRuleAssignmentGet(int Id)
        {
            int total = 0;
            return LeaveRuleAssignmentDAL.GetItemList(Id, -1, "", "", "", -1, "", "", "", "", -1, "intRuleAssignID", "asc", 1, 1, out total).Single();
        }
        public List<LeaveRuleAssignment> LeaveRuleAssignmentGetAll(string strCompanyID)
        {
            int total = 0;
            return LeaveRuleAssignmentDAL.GetItemList(0, -1, "", "", strCompanyID, -1, "", "", "", "", -1, "intRuleAssignID", "asc", 1, 100000, out total);
        }


        public List<LeaveRuleAssignment> LeaveRuleAssignmentGet(int intRuleID, string strEmpName, string strEmpInitial, string strCompanyID,
                                                                int intLeaveTypeID, string strDepartmentID, string strDesignationID, 
                                                                string strGender, int intCategoryID, string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {

            return LeaveRuleAssignmentDAL.GetItemList(0, intRuleID, strEmpName, strEmpInitial, strCompanyID, intLeaveTypeID, strDepartmentID,
                                                      strDesignationID, "", strGender, intCategoryID, strSortBy, strSortType, startRowIndex, maximumRows, out numTotalRows);
        }

    }
}
