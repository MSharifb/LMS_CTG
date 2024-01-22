using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveYearBLL
    {
        public int Add(LeaveYear objLeaveYear)
        {
            return LeaveYearDAL.SaveItem(objLeaveYear, "I");

        }
        public int Edit(LeaveYear objLeaveYear)
        {
            return LeaveYearDAL.SaveItem(objLeaveYear, "U");
        }
        public int Delete(int Id)
        {
            LeaveYear obj = new LeaveYear();
            obj.intLeaveYearID = Id;
            return LeaveYearDAL.SaveItem(obj, "D");
        }

        public LeaveYear LeaveYearGet(int Id)
        {
            //return LeaveYearDAL.GetItemList(Id,0, "", "").Single();
            return LeaveYearDAL.GetItemList(Id, 0, "",-1, "").FirstOrDefault();
        }
        public List<LeaveYear> LeaveYearGetAll(int intSearchLeaveYearTypeId,string strCompanyID)
        {
            return LeaveYearDAL.GetItemList(0, intSearchLeaveYearTypeId, "",-1, strCompanyID);
        }

        public List<LeaveYear> LeaveYearGet(int intLeaveYearID, string strYearTitle, string strCompanyID)
        {
            return LeaveYearDAL.GetItemList(intLeaveYearID,0, strYearTitle,-1, strCompanyID);
        }

        public List<LeaveYear> LeaveYearGetInactive(int intSearchLeaveYearTypeId, string strCompanyID)
        {
            return LeaveYearDAL.GetItemList(0, intSearchLeaveYearTypeId, "", 0, strCompanyID);
        }

        public List<LeaveYear> LeaveYearGetActive(int intSearchLeaveYearTypeId, string strCompanyID)
        {
            return LeaveYearDAL.GetItemList(0, intSearchLeaveYearTypeId, "", 1, strCompanyID);
        }

    }
}
