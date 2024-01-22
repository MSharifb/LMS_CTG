using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
using System.Transactions;
using System.Data;
using TVL.DB;

namespace LMS.BLL
{
    public class ApprovalAuthorBLL
    {
        public int SetApprover(List<ApprovalAuthor> lst, ApprovalAuthor objAut)
        {
            int i = 0;            
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {               
                i = ApprovalAuthorDAL.SaveItem(objAut, "D", transection, con);

                if (lst != null)
                {
                    foreach (ApprovalAuthor obj in lst)
                    {
                        i = ApprovalAuthorDAL.SaveItem(obj, "I", transection, con);
                    }
                }


            }
            catch (Exception ex)
            {
                transection.Rollback();
            }

            finally
            {
                if (i < 0)
                {
                    transection.Rollback();
                }
                else
                {
                    transection.Commit();
                }
            }

            return i;
        }
        

       
        public int Edit(ApprovalAuthor objApprovalAuthor)
        {
            return 0;
        }
        public int Delete(int Id)
        {
            ApprovalAuthor obj = new ApprovalAuthor();
            obj.intAuthorityID = Id;
            
            return ApprovalAuthorDAL.SaveItem(obj, "D",null,null);
        }

        public int GetPathIdWiseAuthorExists(int pathId, string strAuthorId, string strCompanyId)
        {
            return ApprovalAuthorDAL.GetApprovalPathWiseAuthority(pathId, strAuthorId, strCompanyId);        
        }

        public ApprovalAuthor ApprovalAuthorGet(int Id)
        {
            return ApprovalAuthorDAL.GetItemList(Id,-1, "","","").Single();
        }

        public List<ApprovalAuthor> ApprovalAuthorGet(int intAuthorityID, int intNodeID, string strAuthorID, string strAuthorType, string strCompanyID)
        {
            return ApprovalAuthorDAL.GetItemList(intAuthorityID, intNodeID, strAuthorID, strAuthorType, strCompanyID);
        }

        public List<ApprovalAuthor> GetApprovalAuthorSteps(string strAuthorID, string strAuthorType, string strCompanyID, int intAuthorTypeID)
        {
            return ApprovalAuthorDAL.GetApprovalAuthorSteps(strAuthorID, strAuthorType, strCompanyID, intAuthorTypeID);
        }
        
        public string GetApprovalAuthorsEmail(long intApplicationID, string strCompanyID,string strCutoffEmail)
        {
            return ApprovalAuthorDAL.GetApprovalAuthorsEmail(intApplicationID, strCompanyID, strCutoffEmail);
        }

        /// <summary>
        /// Added for Central Approval System.
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public List<ApproverInfo> GetApproverInfoByEmpId(string empId)
        {
            return ApprovalAuthorDAL.GetApproverInfo(empId);
        }

    }
}
