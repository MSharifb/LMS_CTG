using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ATT_AtdStatusSetupBLL
    {
        public int Add(ATT_tblAtdStatusSetup objATT_tblAtdStatusSetup)
        {
            return ATT_AtdStatusSetupDAL.SaveItem(objATT_tblAtdStatusSetup, "I");
        }
        public int Edit(ATT_tblAtdStatusSetup objATT_tblAtdStatusSetup)
        {
            return ATT_AtdStatusSetupDAL.SaveItem(objATT_tblAtdStatusSetup, "U");
        }
        public int Delete(int intRowID)
        {
            ATT_tblAtdStatusSetup obj = new ATT_tblAtdStatusSetup();
            obj.intRowID = intRowID;

            return ATT_AtdStatusSetupDAL.SaveItem(obj, "D");
        }

        public ATT_tblAtdStatusSetup AteendanceStatusGet(Int32 Id)
        {
        
            List<ATT_tblAtdStatusSetup> empList = ATT_AtdStatusSetupDAL.GetItemList(Id);
            if (empList.Count > 0)
            {
                return empList.Single();
            }
            else
            {
                return null;
            }
        }
        public List<ATT_tblAtdStatusSetup> AteendanceStatusGet()
        {
            return ATT_AtdStatusSetupDAL.GetItemList(0);
        }
    }
}
