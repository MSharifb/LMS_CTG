using System.Collections.Generic;
using System.Linq;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ShiftBLL
    {

        public int Add(Shift objShift, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objShift, ref strmsg) == true)
            {
                intResult = ShiftDAL.SaveItem(objShift, "I");
            }
            return intResult;

        }

        public int Edit(Shift objShift, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objShift, ref strmsg) == true)
            {
                intResult = ShiftDAL.SaveItem(objShift, "U");
            }
            return intResult;
        }

        public int Delete(int Id)
        {
            Shift obj = new Shift();
            obj.intShiftID = Id;

            return ShiftDAL.SaveItem(obj, "D");
        }

        public Shift ShiftGetByID(int Id)
        {
            return ShiftDAL.GetItemList(Id, "", false, "","").Single();
        }
        public List<Shift> ShiftGetAll()
        {
            return ShiftDAL.GetItemList(0, "", false, "","");
        }

        public List<Shift> ShiftGetSrc(string strShiftName, bool bitIsRoaster, string strPeriodFrom, string strPeriodTo)
        {
            return ShiftDAL.GetItemList(0,strShiftName, bitIsRoaster, strPeriodFrom, strPeriodTo);
        }

        private bool CheckValidation(Shift objShift, ref string strMSG)
        {
            bool isvalid = true;

            //if ((objLeaveType.bitIsEarnLeave == true) && (objLeaveType.IsFixed == false) && (objLeaveType.intEarnLeaveUnitForDays == 0))
            //{
            //    strMSG = "Per Unit in Days must be greater than zero.";
            //    isvalid = false;
            //}

            return isvalid;


        }

    }
}
