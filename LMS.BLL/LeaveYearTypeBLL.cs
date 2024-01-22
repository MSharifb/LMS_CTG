using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveYearTypeBLL
    {

        public int Add(LeaveYearType objLeaveYearType, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objLeaveYearType, ref strmsg) == true)
            {              
                intResult = LeaveYearTypeDAL.SaveItem(objLeaveYearType, "I");
            }
            return intResult;

        }

        public int Edit(LeaveYearType objLeaveYearType, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objLeaveYearType, ref strmsg) == true)
            {               
                intResult = LeaveYearTypeDAL.SaveItem(objLeaveYearType, "U");
            }
            return intResult;
        }

        public int Delete(int Id)
        {
            LeaveYearType obj = new LeaveYearType();
            obj.intLeaveYearTypeId = Id;

            return LeaveYearTypeDAL.SaveItem(obj, "D");
        }

        public LeaveYearType LeaveYearTypeGet(int Id)
        {
            return LeaveYearTypeDAL.GetItemList(Id, "", "", "","").Single();
        }
        public List<LeaveYearType> LeaveYearTypeGetAll(string @strCompanyID)
        {
            return LeaveYearTypeDAL.GetItemList(-1, "", "","", @strCompanyID);
        }

        public List<LeaveYearType> LeaveYearTypeGet(int intLeaveYearTypeID, string LeaveYearType, string StartMonth,string EndMonth, string strCompanyID)
        {
            return LeaveYearTypeDAL.GetItemList(intLeaveYearTypeID,LeaveYearType,StartMonth,EndMonth, strCompanyID);
        }

        private bool CheckValidation(LeaveYearType objLeaveYearType, ref string strMSG)
        {
            bool isvalid = true;
            return isvalid;
        }

    }
}

