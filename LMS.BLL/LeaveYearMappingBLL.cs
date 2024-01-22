using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveYearMappingBLL
    {

        public int Add(LeaveYearMapping objLeaveType, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objLeaveType, ref strmsg) == true)
            {
                intResult = LeaveYearMappingDAL.SaveItem(objLeaveType, "I");
            }
            return intResult;

        }

        public int Edit(LeaveYearMapping objLeaveType, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objLeaveType, ref strmsg) == true)
            {
                intResult = LeaveYearMappingDAL.SaveItem(objLeaveType, "U");
            }
            return intResult;
        }

        public int Delete(int Id)
        {
            LeaveYearMapping obj = new LeaveYearMapping();
            obj.intLeaveYearMapID = Id;

            return LeaveYearMappingDAL.SaveItem(obj, "D");
        }

        public LeaveYearMapping LeaveYearMappingGet(int Id)
        {
            return LeaveYearMappingDAL.GetItemList(Id, 0, 0,true, "").FirstOrDefault();
        }

        public List<LeaveYearMapping> LeaveYearMappingGetAll(LeaveYearMapping model, string @strCompanyID)
        {
            return LeaveYearMappingDAL.GetItemList(0, model.intLeaveYearId, model.intLeaveTypeID, model.bitIsActiveYear, @strCompanyID);
        }


        public List<LeaveYearMapping> LeaveYearMappingGet(int intLeaveYearMapID, int intLeaveYearId, int intLeaveTypeID, string strCompanyID)
        {
            return LeaveYearMappingDAL.GetItemList(intLeaveYearMapID, intLeaveYearId, intLeaveTypeID,true, strCompanyID);
        }


        private bool CheckValidation(LeaveYearMapping objLeaveType, ref string strMSG)
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
