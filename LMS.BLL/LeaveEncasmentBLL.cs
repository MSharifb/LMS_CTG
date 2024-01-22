using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveEncasmentBLL
    {

        public int Add(LeaveEncasment objLeaveEncasment, ref string strmsg)
        {
            int intResult = 0;

            if (objLeaveEncasment.strIsIndividual == "Individual")
            {
                if (CheckValidation(objLeaveEncasment, ref strmsg) == true)
                {
                    intResult = LeaveEncasmentDAL.SaveItem(objLeaveEncasment, "I");
                }
            }
            else
                intResult = LeaveEncasmentDAL.SaveItem(objLeaveEncasment, "I");

            return intResult;
        }


        public int Edit(LeaveEncasment objLeaveEncasment, ref string strmsg)
        {
            int intResult = 0;

            if (objLeaveEncasment.strIsIndividual == "Individual")
            {
                if (CheckValidation(objLeaveEncasment, ref strmsg) == true)
                {
                    intResult = LeaveEncasmentDAL.SaveItem(objLeaveEncasment, "U");
                }
            }
            else
                intResult = LeaveEncasmentDAL.SaveItem(objLeaveEncasment, "U");

            //if (CheckValidation(objLeaveEncasment, ref strmsg) == true)
            //{
            //    intResult = LeaveEncasmentDAL.SaveItem(objLeaveEncasment, "U");
            //}
            return intResult;
        }


        public int Delete(LeaveEncasment objLeaveEncasment)
        {
            return LeaveEncasmentDAL.SaveItem(objLeaveEncasment, "D");
        }


        public LeaveEncasment LeaveEncasmentGet(int Id)
        {
            return LeaveEncasmentDAL.GetItemList(Id, 0, 0, "", "").Single();
        }

        public List<LeaveEncasment> LeaveEncasmentGet(int intLeaveYearId, int intLeaveTypeId, string strEmpID, string strCompanyId)
        {
            return LeaveEncasmentDAL.GetItemList(0, intLeaveYearId, intLeaveTypeId, strEmpID, strCompanyId);
        }

        public List<LeaveEncasment> LeaveEncasedGet(int intLeaveYearId, int intLeaveTypeId, string strEmpID, string strCompanyId)
        {
            return LeaveEncasmentDAL.GetEncashed(0, intLeaveYearId, intLeaveTypeId, strEmpID, strCompanyId);
        }

        public List<LeaveEncasment> LeaveEncasmentGetAll()
        {
            return LeaveEncasmentDAL.GetItemList(0, 0, 0, "", "");
        }

        public List<LeaveEncasment> LeaveEncasmentGetAll(string strCompanyId)
        {
            return LeaveEncasmentDAL.GetItemList(0, 0, 0, "", strCompanyId);
        }

        public List<LeaveEncasment> LeaveEncasmentGetAll(string strCompanyId, int intYearId)
        {
            return LeaveEncasmentDAL.GetItemList(0, intYearId, 0, "", strCompanyId);
        }


        private bool CheckValidation(LeaveEncasment objLeaveEncasment, ref string strMSG)
        {
            bool isvalid = true;


            if (objLeaveEncasment.fltBeforeBalance <= 0)
            {
                strMSG = "Leave balance must be greater than zero.";
                return false;
            }
            if (objLeaveEncasment.fltEncaseDuration <= 0)
            {
                strMSG = "Encash day must be greater than zero.";
                return false;
            }

            if (objLeaveEncasment.fltEncaseDuration > objLeaveEncasment.fltBeforeBalance)
            {
                strMSG = "Encash day never exceed the leave balance days.";
                return false;
            }

            if (objLeaveEncasment.fltMinDaysinhand > 0 && objLeaveEncasment.fltMinDaysinhand > objLeaveEncasment.fltBeforeBalance)
            {
                strMSG = "Leave balance day must be greater than minimum balance days.";
                return false;
            }

            if (objLeaveEncasment.fltMaxDaysEncashable > 0)
            {
                if (objLeaveEncasment.fltEncashed + objLeaveEncasment.fltEncaseDuration > objLeaveEncasment.fltMaxDaysEncashable)
                {
                    strMSG = "Encash day never exceed the maximum encash days.";
                    return false;
                }

            }

            return isvalid;

        }

        public List<LeaveType> GetLeaveTypeList(string intLocationID, string intDepartmentID, string intDesignationID)
        {
            return LeaveEncasmentDAL.GetLeaveTypeList(intLocationID, intDepartmentID, intDesignationID);
        }

    }
}
