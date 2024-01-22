using System.Collections.Generic;
using System.Linq;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class WeekendHolidayAsWorkingdayBLL
    {

        public int Add(WeekendHolidayAsWorkingday objWeekendHolidayAsWorkingday, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objWeekendHolidayAsWorkingday, ref strmsg) == true)
            {
                intResult = WeekendHolidayAsWorkingdayDAL.SaveItem(objWeekendHolidayAsWorkingday, "I");
            }
            return intResult;

        }

        public int Edit(WeekendHolidayAsWorkingday objWeekendHolidayAsWorkingday, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objWeekendHolidayAsWorkingday, ref strmsg) == true)
            {
                intResult = WeekendHolidayAsWorkingdayDAL.SaveItem(objWeekendHolidayAsWorkingday, "U");
            }
            return intResult;
        }

        public int Delete(int Id)
        {
           WeekendHolidayAsWorkingday obj = new  WeekendHolidayAsWorkingday();
            obj.intWeekendWorkingday = Id;

            return WeekendHolidayAsWorkingdayDAL.SaveItem(obj, "D");
        }

        public WeekendHolidayAsWorkingday WeekendHolidayAsWorkingdayGetByID(int Id)
        {
            return WeekendHolidayAsWorkingdayDAL.GetItemList(Id, "",  "", "").Single();
        }
        public List<WeekendHolidayAsWorkingday> WeekendHolidayAsWorkingdayGetAll()
        {
            return WeekendHolidayAsWorkingdayDAL.GetItemList(0, "", "", "");
        }

        public List<WeekendHolidayAsWorkingday> WeekendHolidayAsWorkingdayGetSrc(string strEffectiveDateFrom, string strEffectiveDateTo, string strDeclarationDate)
        {
            return WeekendHolidayAsWorkingdayDAL.GetItemList(0, strEffectiveDateFrom, strEffectiveDateTo, strDeclarationDate);
        }

        private bool CheckValidation(WeekendHolidayAsWorkingday objWeekendHolidayAsWorkingday, ref string strMSG)
        {
            bool isvalid = true;

           
            return isvalid;


        }

    }
}
