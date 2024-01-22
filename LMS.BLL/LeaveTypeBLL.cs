using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveTypeBLL
    {

        public int Add(LeaveType objLeaveType, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objLeaveType, ref strmsg) == true)
            {
                if (objLeaveType.bitIsEarnLeave == false)
                {
                    objLeaveType.strEntitlementType = null;
                    objLeaveType.intEarnLeaveUnitForDays = 0;
                }
                else
                {
                    if (objLeaveType.strEntitlementType == null || objLeaveType.strEntitlementType == "Fixed")
                    {
                        objLeaveType.intEarnLeaveUnitForDays = 0;
                    }
                }

                intResult = LeaveTypeDAL.SaveItem(objLeaveType,  "I");
            }
            return intResult;

        }

        public int Edit(LeaveType objLeaveType, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objLeaveType, ref strmsg) == true)
            {
                if (objLeaveType.bitIsEarnLeave == false)
                {
                    objLeaveType.strEntitlementType = null;
                    objLeaveType.intEarnLeaveUnitForDays = 0;
                }
                else
                {
                    if (objLeaveType.strEntitlementType == null || objLeaveType.strEntitlementType == "Fixed")
                    {
                        objLeaveType.intEarnLeaveUnitForDays = 0;
                    }
                }

                intResult = LeaveTypeDAL.SaveItem(objLeaveType,  "U");
            }
            return intResult;
        }

        public int AddDeductLeaveType(LeaveTypeDeduct objLeaveTypeDeduct, ref string strmsg)
        {
            int intResult = 0;
            intResult = LeaveTypeDAL.SaveLeaveTypeDeductItem(objLeaveTypeDeduct, "I");
           
            return intResult;

        }

        public int EditDeductLeaveType(LeaveTypeDeduct objLeaveTypeDeduct, ref string strmsg)
        {
            int intResult = 0;
            intResult = LeaveTypeDAL.SaveLeaveTypeDeductItem(objLeaveTypeDeduct, "U");
            return intResult;
        }


        public int Delete(int Id)
        {
            LeaveType obj = new LeaveType();
            obj.intLeaveTypeID = Id;

            return LeaveTypeDAL.SaveItem(obj, "D");
        }

        public LeaveType LeaveTypeGet(int Id)
        {
            return LeaveTypeDAL.GetItemList(Id, "", "", "").Single();
        }
        public List<LeaveType> LeaveTypeGetAll(string @strCompanyID)
        {
            return LeaveTypeDAL.GetItemList(-1, "", "", @strCompanyID);
        }


        public List<LeaveType> LeaveTypeGet(int intLeaveTypeID, string strLeaveType, string strLeaveShortName, string strCompanyID)
        {
            return LeaveTypeDAL.GetItemList(intLeaveTypeID, strLeaveType, strCompanyID, strCompanyID);
        }

        /// <summary>
        /// Return All Deducted Leave Type
        /// </summary>
        /// <param name="intLeaveTypeID"> Leave Type ID</param>
        /// <returns></returns>
        public List<LeaveTypeDeduct> DeductedLeaveGet(int intLeaveTypeID)
        {
            return LeaveTypeDAL.GetDeductedLeaveItemList(intLeaveTypeID,0);
        }
        /// <summary>
        /// Modified By Shamim for MPA
        /// </summary>
        /// <param name="objLeaveType"></param>
        /// <param name="strMSG"></param>
        /// <returns></returns>
        private bool CheckValidation(LeaveType objLeaveType, ref string strMSG)
        {
            bool isvalid = true;

            //if ((objLeaveType.bitIsEarnLeave == true) && (objLeaveType.IsFixed == false) && (objLeaveType.intEarnLeaveUnitForDays == 0))
            //{
            //    strMSG = "Per Unit in Days must be greater than zero.";
            //    isvalid = false;
            //}

            return isvalid;


        }

        /// <summary>
        /// Delete Deducted Leave Type
        /// </summary>
        /// <param name="Id">intLeaveTypeDeductID</param>
        /// <returns></returns>
        public int DeleteDedectedLeaveType(int Id)
        {
            LeaveTypeDeduct obj = new LeaveTypeDeduct();
            obj.intLeaveTypeDeductID = Id;
            return LeaveTypeDAL.SaveLeaveTypeDeductItem(obj, "D");
        }
    }
}
