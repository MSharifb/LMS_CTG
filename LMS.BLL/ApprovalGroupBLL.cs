using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ApprovalGroupBLL
    {

        public int Add(ApprovalGroup objApprovalGroup, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objApprovalGroup, ref strmsg) == true)
            {
                intResult = ApprovalGroupDAL.SaveItem(objApprovalGroup, "I");
            }
            return intResult;

        }

        public int Edit(ApprovalGroup objApprovalGroup, ref string strmsg)
        {
            int intResult = 0;

            if (CheckValidation(objApprovalGroup, ref strmsg) == true)
            {
                intResult = ApprovalGroupDAL.SaveItem(objApprovalGroup, "U");
            }
            return intResult;
        }

        public int Delete(int Id)
        {
            ApprovalGroup obj = new ApprovalGroup();
            obj.intApprovalGroupId = Id;

            return ApprovalGroupDAL.SaveItem(obj, "D");
        }

        public ApprovalGroup ApprovalGroupGet(int Id)
        {
            return ApprovalGroupDAL.GetItemList(Id, "",  "").Single();
        }
        public List<ApprovalGroup> ApprovalGroupGetAll(string @strCompanyID)
        {
            return ApprovalGroupDAL.GetItemList(-1, "", @strCompanyID);
        }

        public List<ApprovalGroup> ApprovalGroupGet(int intApprovalGroupId, string ApprovalGroupName, string strCompanyID)
        {
            return ApprovalGroupDAL.GetItemList(intApprovalGroupId, ApprovalGroupName, strCompanyID);
        }

        private bool CheckValidation(ApprovalGroup objLeaveYearType, ref string strMSG)
        {
            bool isvalid = true;
            return isvalid;
        }

    }
}


