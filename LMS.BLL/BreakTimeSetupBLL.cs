using System.Collections.Generic;
using System.Linq;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class BreakTimeSetupBLL
    {

        public int Add(ATT_tblSetBreakTime objSetBreakTime, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objSetBreakTime, ref strmsg) == true)
            {
                intResult = BreakTimeSetupDAL.SaveItem(objSetBreakTime, "I");
            }
            return intResult;

        }

        public int Edit(ATT_tblSetBreakTime objSetBreakTime, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objSetBreakTime, ref strmsg) == true)
            {
                intResult = BreakTimeSetupDAL.SaveItem(objSetBreakTime, "U");
            }
            return intResult;
        }

        public int Delete(int Id)
        {
            ATT_tblSetBreakTime objSetBreakTime = new ATT_tblSetBreakTime();
            objSetBreakTime.intBreakSetID = Id;

            return BreakTimeSetupDAL.SaveItem(objSetBreakTime, "D");
        }

        public ATT_tblSetBreakTime BreakTimeSetupGetByID(int Id)
        {
            return BreakTimeSetupDAL.GetItemList(Id).Single();
        }
        public List<ATT_tblSetBreakTime> BreakTimeSetupGetAll()
        {
            return BreakTimeSetupDAL.GetItemList(0);
        }

        public List<ATT_tblSetBreakTime> BreakTimeSetupGetSrc(int Id)
        {
            return BreakTimeSetupDAL.GetItemList(Id);
        }

        private bool CheckValidation(ATT_tblSetBreakTime objSetBreakTime, ref string strMSG)
        {
            bool isvalid = true;
                

            return isvalid;


        }

    }
}
