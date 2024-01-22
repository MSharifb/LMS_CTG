using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class OOAApprovalFlowBLL
    {
        public int Add(OOAApprovalFlow objOOAApprovalFlow, ref string strmsg)
        {
            return OOAApprovalFlowDAL.SaveItem(objOOAApprovalFlow, "I");
        }
        
        public int Edit(OOAApprovalFlow objOOAApprovalFlow)
        {
            return OOAApprovalFlowDAL.SaveItem(objOOAApprovalFlow, "U");
        }
        
        public int Delete(string Id)
        {
            OOAApprovalFlow obj = new OOAApprovalFlow();            
           
            return OOAApprovalFlowDAL.SaveItem(obj, "D");
        }

        public int AlternateApproval(string strAlternateSupervisorID, OOAApprovalFlow objOOAApprovalFlow, out string strmsg)
        {
            return OOAApprovalFlowDAL.AlternateOOAApproval(strAlternateSupervisorID, objOOAApprovalFlow, out strmsg);
        }


        public OOAApprovalFlow OOAApprovalFlowGet(int Id)
        {
            return OOAApprovalFlowDAL.GetItemList(Id,-1,-1,"","").SingleOrDefault();
        }
        
        public List<OOAApprovalFlow> OOAApprovalFlowGet(int intAppFlowID,long intApplicationID,int intNodeID, string strAuthorID, string strCompanyID)
        {
            return OOAApprovalFlowDAL.GetItemList( intAppFlowID, intApplicationID, intNodeID,  strAuthorID,  strCompanyID);
        }

        public int RecommendAUthorTypeGet(int outOfOfficeID)
        {
            return OOAApprovalFlowDAL.RecommendAUthorTypeGet(outOfOfficeID);
        }
    }
}
