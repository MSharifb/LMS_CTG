using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class AttRuleBLL
    {
        public int Add(ATT_tblRule objAttRule)
        {            

            return AttRuleDAL.Save(objAttRule, "I");
        }


        public int Edit(ATT_tblRule objAttRule)
        {
            return AttRuleDAL.Save(objAttRule, "U");

        }

        public int Delete(int Id)
        {
            ATT_tblRule obj = new ATT_tblRule();
            obj.intRuleID = Id;

            return AttRuleDAL.Save(obj, "D");
        }

        public ATT_tblRule GetByID(int Id)
        {
            return AttRuleDAL.Get(Id, "").Single();
        }

        public List<ATT_tblRule> GetAll()
        {
            return AttRuleDAL.Get(-1, "");
        }

        public List<ATT_tblRule> Get(int intRuleID, string dtEffectiveDate)
        {
            return AttRuleDAL.Get(intRuleID, dtEffectiveDate);
        }

    }
}
