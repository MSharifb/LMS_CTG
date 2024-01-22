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
    public class OOAApprovalAuthorBLL
    {
        public int SetApprover(List<OOAApprovalAuthor> lst, OOAApprovalAuthor objAut)
        {
            int i = 0;            
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {               
                i = OOAApprovalAuthorDAL.SaveItem(objAut, "D", transection, con);

                if (lst != null)
                {
                    foreach (OOAApprovalAuthor obj in lst)
                    {
                        i = OOAApprovalAuthorDAL.SaveItem(obj, "I", transection, con);
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
        

       
        public int Edit(OOAApprovalAuthor objOOAApprovalAuthor)
        {
            return 0;
        }
        public int Delete(int Id)
        {
            OOAApprovalAuthor obj = new OOAApprovalAuthor();
            obj.intAuthorityID = Id;
            
            return OOAApprovalAuthorDAL.SaveItem(obj, "D",null,null);
        }

        public int GetPathIdWiseAuthorExists(int pathId, string strAuthorId, string strCompanyId)
        {
            return OOAApprovalAuthorDAL.GetOOAApprovalPathWiseAuthority(pathId, strAuthorId, strCompanyId);        
        }

        public OOAApprovalAuthor OOAApprovalAuthorGet(int Id)
        {
            return OOAApprovalAuthorDAL.GetItemList(Id,-1, "","","").Single();
        }

        public List<OOAApprovalAuthor> OOAApprovalAuthorGet(int intAuthorityID, int intNodeID, string strAuthorID, string strAuthorType, string strCompanyID)
        {
            return OOAApprovalAuthorDAL.GetItemList(intAuthorityID, intNodeID, strAuthorID, strAuthorType, strCompanyID);
        }

        public List<OOAApprovalAuthor> GetOOAApprovalAuthorSteps(string strAuthorID, string strAuthorType, string strCompanyID, int intAuthorTypeID)
        {
            return OOAApprovalAuthorDAL.GetOOAApprovalAuthorSteps(strAuthorID, strAuthorType, strCompanyID, intAuthorTypeID);
        }
        
        public string GetOOAApprovalAuthorsEmail(long intApplicationID, string strCompanyID,string strCutoffEmail)
        {
            return OOAApprovalAuthorDAL.GetOOAApprovalAuthorsEmail(intApplicationID, strCompanyID, strCutoffEmail);
        }

    }
}
