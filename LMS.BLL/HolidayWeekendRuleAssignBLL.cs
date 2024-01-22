using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class HolidayWeekendRuleAssignBLL
    {
        public int Add(HolidayWeekendRuleAssign objHolidayWeekendRuleAssign, out string strmessage)
        {
            return HolidayWeekendRuleAssignDAL.SaveItem(objHolidayWeekendRuleAssign, "I", out strmessage);
        }
        public int Edit(HolidayWeekendRuleAssign objHolidayWeekendRuleAssign, out string strmessage)
        {
            return HolidayWeekendRuleAssignDAL.SaveItem(objHolidayWeekendRuleAssign, "U", out strmessage);
        }
        public int Delete(int Id, out string strmessage)
        {
            HolidayWeekendRuleAssign obj = new HolidayWeekendRuleAssign();
            obj.intRuleAssignID = Id;

            return HolidayWeekendRuleAssignDAL.SaveItem(obj, "D", out strmessage);
        }

        public HolidayWeekendRuleAssign HolidayWeekendRuleAssignGet(int Id)
        {
            int total = 0;
            return HolidayWeekendRuleAssignDAL.GetItemList(Id, -1, "", "", -1, "", "", "", "", -1, "intRuleAssignID", "ASC", 1, 1, out total).Single();
        }
        public List<HolidayWeekendRuleAssign> HolidayWeekendRuleAssignGet(int intRuleId, string strEmpName, string strEmpInitial, int intYearId, string strDepartmentId,
                                                                          string strDesignationId, string strReligionId, string strCompanyId, int intCategoryID,
                                                                          string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {

            return HolidayWeekendRuleAssignDAL.GetItemList(-1, intRuleId, strEmpName, strEmpInitial, intYearId, strDepartmentId, strDesignationId,
                                                           strReligionId, strCompanyId, intCategoryID, strSortBy, strSortType, startRowIndex, maximumRows, out numTotalRows);
        }

        public List<HolidayWeekendRuleAssign> HolidayWeekendRuleAssignGetAll()
        {
            int total = 0;
            return HolidayWeekendRuleAssignDAL.GetItemList(-1, -1, "", "", -1, "", "", "", "", -1, "intRuleAssignID", "ASC", 1, 10000, out total);
        }
        public List<HolidayWeekendRuleAssign> HolidayWeekendRuleAssignGetAll(string strCompanyId)
        {
            int total = 0;
            return HolidayWeekendRuleAssignDAL.GetItemList(-1, -1, "", "", -1, "", "", "", strCompanyId, -1, "intRuleAssignID", "ASC", 1, 10000, out total);
        }
    }
}
