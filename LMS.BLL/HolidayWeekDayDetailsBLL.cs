using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class HolidayWeekDayDetailsBLL
    {

        public int Add(HolidayWeekDayDetails objHolidayWeekDayDetails, ref string strmsg)
        {
            return HolidayWeekDayDetailsDAL.SaveItem(objHolidayWeekDayDetails, "I",null,null);
        }

        public int Edit(HolidayWeekDayDetails objHolidayWeekDayDetails, ref string strmsg)
        {
            return HolidayWeekDayDetailsDAL.SaveItem(objHolidayWeekDayDetails, "U", null, null);
        }

        public int Delete(int Id)
        {
            HolidayWeekDayDetails obj = new HolidayWeekDayDetails();
            obj.intHolidayWeekendDetailsID = Id;

            return HolidayWeekDayDetailsDAL.SaveItem(obj, "D", null, null);
        }

        public HolidayWeekDayDetails HolidayWeekDayDetailsGet(int Id)
        {
            return HolidayWeekDayDetailsDAL.GetItemList(Id, 0, "", "").Single();
        }

        public List<HolidayWeekDayDetails> HolidayWeekDayDetailsGetByMasterId(int masterID)
        {
            return HolidayWeekDayDetailsDAL.GetItemList(0, masterID, "", "");
        }

        public List<HolidayWeekDayDetails> HolidayWeekDayDetailsGet(int intLvyearId, string strType, string strCompanyId)
        {
            return HolidayWeekDayDetailsDAL.GetItemList(0, intLvyearId, strType, strCompanyId);
        }

        public List<HolidayWeekDayDetails> HolidayWeekDayDetailsGetAll()
        {
            return HolidayWeekDayDetailsDAL.GetItemList(0, 0, "", "");
        }

        public List<HolidayWeekDayDetails> HolidayWeekDayDetailsGetAll(string strCompanyId)
        {
            return HolidayWeekDayDetailsDAL.GetItemList(0, 0, "", strCompanyId);
        }

        public List<HolidayWeekDayDetails> HolidayWeekDayDetailsGetAll(int intLvyearId, string strCompanyId)
        {
            return HolidayWeekDayDetailsDAL.GetItemList(0, intLvyearId, "", strCompanyId);
        }

       

    }
}
