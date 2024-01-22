using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class OOAAuthorTypeBLL
    {

        public int Add(AuthorType objAuthorType)
        {
            return OOAAuthorTypeDAL.SaveItem(objAuthorType, "I");
        }

        public int Edit(AuthorType objAuthorType)
        {
            return OOAAuthorTypeDAL.SaveItem(objAuthorType, "U");
        }

        public int Delete(string Id)
        {
            AuthorType obj = new AuthorType();
            return OOAAuthorTypeDAL.SaveItem(obj, "D");
        }

        public AuthorType AuthorTypeGet(int Id)
        {
            return OOAAuthorTypeDAL.GetItemList(Id, "", "").Single();
        }
        
        public List<AuthorType> AuthorTypeGet(int intAuthorTypeID, string strAuthorType, string strCompanyID)
        {
            return OOAAuthorTypeDAL.GetItemList(intAuthorTypeID, strAuthorType, strCompanyID);
        }

        public static int GetAuthorTypeID(string strAuthorID, string strEMPID)
        {
            return OOAAuthorTypeDAL.GetAuthorTypeID(strAuthorID, strEMPID);
        }

        public static List<OOAApprovalPathDetails> GetAuthorEmployeeWise(string strAuthorID, string strEMPID)
        {
            return OOAAuthorTypeDAL.GetAuthorEmployeeWise(strAuthorID, strEMPID);
        }
    }
}
