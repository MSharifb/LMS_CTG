using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class LeaveRuleBLL
    {
        public int Add(LeaveRule objLeaveRule)
        {
            if (objLeaveRule.bitIsEncashable == false)
            {
                objLeaveRule.intMaxEncahDays = 0;
                objLeaveRule.intMinDaysInHand = 0;
            }

            if (objLeaveRule.bitIsCarryForward == false)
            {
                objLeaveRule.intMaxCarryForwardDays = 0;
                objLeaveRule.strLeaveObsoluteMonth = null;
            }

            return LeaveRuleDAL.SaveItem(objLeaveRule, "I");
        }


        public int Edit(LeaveRule objLeaveRule)
        {

            if (objLeaveRule.bitIsEncashable == false)
            {
                objLeaveRule.intMaxEncahDays = 0;
                objLeaveRule.intMinDaysInHand = 0;
            }

            if (objLeaveRule.bitIsCarryForward == false)
            {
                objLeaveRule.intMaxCarryForwardDays = 0;
                objLeaveRule.strLeaveObsoluteMonth = null;
            }

            return LeaveRuleDAL.SaveItem(objLeaveRule, "U");

        }

        public int Delete(int Id)
        {
            LeaveRule obj = new LeaveRule();
            obj.intRuleID = Id;

            return LeaveRuleDAL.SaveItem(obj, "D");
        }

        public LeaveRule LeaveRuleGet(int Id)
        {
            return LeaveRuleDAL.GetItemList(Id, "", -1, "").Single();
        }

        public LeaveRule GetEmployeeWiseRule(string strempId, int intleavetypeId, string strcompanyId)
        {
            return LeaveRuleDAL.GetEmployeeWiseRule(strempId, intleavetypeId, strcompanyId);
        }

        public List<LeaveRule> LeaveRuleGetAll(int livetypeId, string strCompanyID)
        {
            return LeaveRuleDAL.GetItemList(-1, "", livetypeId, strCompanyID);
        }

        public List<LeaveRule> LeaveRuleGet(int intRuleID, string strRuleName, int intLeaveTypeID, string strCompanyID)
        {
            return LeaveRuleDAL.GetItemList(intRuleID, strRuleName, intLeaveTypeID, strCompanyID);
        }

    }
}
