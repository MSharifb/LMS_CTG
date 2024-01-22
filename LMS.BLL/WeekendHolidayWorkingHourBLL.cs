using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class WeekendHolidayWorkingHourBLL
    {
        public int Add(WeekendHolidayWorkingHour objWeekendHolidayWorkingHour)
        {            

            return WeekendHolidayWorkingHourDAL.Save(objWeekendHolidayWorkingHour, "I");
        }


        public int Edit(WeekendHolidayWorkingHour objWeekendHolidayWorkingHour)
        {
            return WeekendHolidayWorkingHourDAL.Save(objWeekendHolidayWorkingHour, "U");

        }

        public int Delete(int Id)
        {
            WeekendHolidayWorkingHour obj = new WeekendHolidayWorkingHour();
            obj.intRowID = Id;

            return WeekendHolidayWorkingHourDAL.Save(obj, "D");
        }

        public WeekendHolidayWorkingHour GetByID(int Id)
        {
            return WeekendHolidayWorkingHourDAL.Get(Id,"","","","","",-1,-1,"","","",-1).Single();
        }

        public List<WeekendHolidayWorkingHour> GetAll()
        {
            return WeekendHolidayWorkingHourDAL.Get(-1, "", "", "", "", "", -1, -1, "", "", "", -1);
        }

        public List<WeekendHolidayWorkingHour> Get(int intRowID, string strEmpID, string strCompanyID, string strLocationID,
            string strDesignationID, string strDepartmentID, int intReligionID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo,
            string strWHType, int intShiftID)
        {
            return WeekendHolidayWorkingHourDAL.Get(intRowID,strEmpID,strCompanyID,strLocationID,strDesignationID,strDepartmentID,intReligionID,intCategoryCode,dtPeriodFrom,dtPeriodTo,strWHType,intShiftID);
        }
        
    }
}
