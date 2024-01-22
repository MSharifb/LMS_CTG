using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ApprovalFlowBLL
    {
        public int Add(ApprovalFlow objApprovalFlow, ref string strmsg)
        {
            return ApprovalFlowDAL.SaveItem(objApprovalFlow, "I");
        }
        
        public int Edit(ApprovalFlow objApprovalFlow)
        {
            return ApprovalFlowDAL.SaveItem(objApprovalFlow, "U");
        }

        public int UpdateEmployeeStatus(string strEmpID)
        {
            return ApprovalFlowDAL.UpdateEmployeeStatus(strEmpID);
        }
        
        public int Delete(string Id)
        {
            ApprovalFlow obj = new ApprovalFlow();            
           
            return ApprovalFlowDAL.SaveItem(obj, "D");
        }

        public int AlternateApproval(string strAlternateSupervisorID, ApprovalFlow objApprovalFlow, out string strmsg)
        {
            return ApprovalFlowDAL.AlternateApproval(strAlternateSupervisorID, objApprovalFlow, out strmsg);
        }


        public ApprovalFlow ApprovalFlowGet(int Id)
        {
            return ApprovalFlowDAL.GetItemList(Id,-1,-1,"","").SingleOrDefault();
        }
        
        public List<ApprovalFlow> ApprovalFlowGet(int intAppFlowID,long intApplicationID,int intNodeID, string strAuthorID, string strCompanyID)
        {
            return ApprovalFlowDAL.GetItemList( intAppFlowID, intApplicationID, intNodeID,  strAuthorID,  strCompanyID);
        }

    }
}
