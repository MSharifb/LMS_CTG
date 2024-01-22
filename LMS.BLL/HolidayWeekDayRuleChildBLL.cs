using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class HolidayWeekDayRuleChildBLL
    {

        public List<HolidayWeekDayRuleChild> HolidayWeekDayRuleChildGet(int ruleId, int intLvyearId, string strCompanyId)
        {
            return HolidayWeekDayRuleDAL.GetChildItemList(ruleId, intLvyearId, strCompanyId);
        }

        public List<HolidayWeekDayRuleChild> GetHolidayWeekDayByRuleId(int ruleId)
        {
            return HolidayWeekDayRuleDAL.GetChildItemListByRuleId(ruleId);
        }

        public List<HolidayWeekDayRuleChild> HolidayWeekDayRuleChildGetAll()
        {
            return HolidayWeekDayRuleDAL.GetChildItemList(0, 0, "");
        }

        public List<HolidayWeekDayRuleChild> HolidayWeekDayRuleGetAll(string strCompanyId)
        {
            return HolidayWeekDayRuleDAL.GetChildItemList(0, 0, strCompanyId);
        }
    }
}
