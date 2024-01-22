using System.Collections.Generic;
using System.Linq;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ManualIOBLL 
    {

        public int Add(ManualIO objManualIO, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objManualIO, ref strmsg) == true)
            {
                intResult = ManualIODAL.SaveItem(objManualIO, "I");
            }
            return intResult;

        }

        public int Edit(ManualIO objManualIO, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objManualIO, ref strmsg) == true)
            {
                intResult = ManualIODAL.SaveItem(objManualIO, "U");
            }
            return intResult;
        }

        public int Delete(int Id)
        {
            ManualIO obj = new ManualIO();
            obj.intRowID = Id;

            return ManualIODAL.SaveItem(obj, "D");
        }

        public ManualIO ManualIOGetByID(int Id)
        {
            return ManualIODAL.GetItemList(Id, "","", "","", 0).Single();
        }
        public List<ManualIO> ManualIOGetAll()
        {
            return ManualIODAL.GetItemList(0, "","", "","", 0);
        }

        public List<ManualIO> ManualIOGetSrc(
            
            int intRowID,
            string strEmpID,
            string strEmpName,
            string strAttendDateFrom,
            string strAttendDateTo,
            int intShiftID)
           
        {
            return ManualIODAL.GetItemList(intRowID, strEmpID, strEmpName, strAttendDateFrom, strAttendDateTo, intShiftID);
        }

        private bool CheckValidation(ManualIO objManualIO, ref string strMSG)
        {
            bool isvalid = true;

            return isvalid;


        }

        public int AddForMobile(MobileIO objMobileIO, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objMobileIO, ref strmsg) == true)
            {
                intResult = ManualIODAL.SaveItemForMobile(objMobileIO, "I");
            }
            return intResult;

        }

    }
}
