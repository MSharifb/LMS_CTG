using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ApprovalProcessBLL
    {
        public int Add(ApprovalProcess objApprovalProcess, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objApprovalProcess, ref strmsg) == true)
            {
                intResult = ApprovalProcessDAL.SaveItem(objApprovalProcess, "I");
            }
            return intResult;

        }

        public int Edit(ApprovalProcess objApprovalProcess, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objApprovalProcess, ref strmsg) == true)
            {
                intResult = ApprovalProcessDAL.SaveItem(objApprovalProcess, "U");
            }
            return intResult;
        }

        public int Delete(int Id)
        {
            ApprovalProcess obj = new ApprovalProcess();
            obj.intApprovalProcessId = Id;

            return ApprovalProcessDAL.SaveItem(obj, "D");
        }

        public ApprovalProcess ApprovalProcessGet(int Id)
        {
            return ApprovalProcessDAL.GetItemList(Id, 0,  "").Single();
        }
        public List<ApprovalProcess> ApprovalProcessGetAll()
        {
            return ApprovalProcessDAL.GetItemList(-1, 0, "");
        }

        public List<ApprovalProcess> ApprovalProcessGet(int intApprovalProcessId, int intModuleId, string strProcessName)
        {
            return ApprovalProcessDAL.GetItemList(intApprovalProcessId, intModuleId, strProcessName);
        }

        private bool CheckValidation(ApprovalProcess objLeaveYearType, ref string strMSG)
        {
            bool isvalid = true;
            return isvalid;
        }
    }
}


