using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class HolidayWeekDayRuleBLL
    {
        public int Add(HolidayWeekDayRule objHolidayWeekDayRule)
        {
            return HolidayWeekDayRuleDAL.SaveItemWithList(objHolidayWeekDayRule, "I");
        }
        public int Edit(HolidayWeekDayRule objHolidayWeekDayRule)
        {
            return HolidayWeekDayRuleDAL.SaveItemWithList(objHolidayWeekDayRule, "U");
        }

        public int Delete(int Id)
        {
            HolidayWeekDayRule obj = new HolidayWeekDayRule();
            obj.intHolidayRuleID = Id;

            return HolidayWeekDayRuleDAL.SaveItem(obj, "D");
        }


        public HolidayWeekDayRule HolidayWeekDayRuleGet(int Id)
        {
            return HolidayWeekDayRuleDAL.GetItemList(Id, 0, 0, "").Single();
        }

        public List<HolidayWeekDayRule> HolidayWeekDayRuleGet(int intLvyearId, int intHolidayId, string strCompanyId)
        {
            return HolidayWeekDayRuleDAL.GetItemList(0, intLvyearId, intHolidayId, strCompanyId);
        }

        public List<HolidayWeekDayRule> HolidayWeekDayRuleGetAll()
        {
            return HolidayWeekDayRuleDAL.GetItemList(0, 0, 0, "");
        }

        public List<HolidayWeekDayRule> HolidayWeekDayRuleGetAll(string strCompanyId)
        {
            return HolidayWeekDayRuleDAL.GetItemList(0, 0, 0, strCompanyId);
        }

    }
}
