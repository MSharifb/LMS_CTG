using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class ApprovalCommentsBLL
    {
        public int Add(ApprovalComments objApprovalComments)
        {
            return ApprovalCommentsDAL.SaveItem(objApprovalComments, "I");
        }
        public int Edit(ApprovalComments objApprovalComments)
        {
            return ApprovalCommentsDAL.SaveItem(objApprovalComments, "U");
        }
        public int Delete(string Id)
        {
            ApprovalComments obj = new ApprovalComments();                 
            return ApprovalCommentsDAL.SaveItem(obj, "D");
        }

        public List<ApprovalComments> ApprovalCommentsGet(int intAppFlowID,long intApplicationID, int intAppStatusID, string strCompanyID)
        {
            return ApprovalCommentsDAL.GetItemList( intAppFlowID,intApplicationID,  intAppStatusID,  strCompanyID);
        }

    }
}
