using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class AuthorTypeBLL
    {

        public int Add(AuthorType objAuthorType)
        {
            return AuthorTypeDAL.SaveItem(objAuthorType, "I");
        }

        public int Edit(AuthorType objAuthorType)
        {
            return AuthorTypeDAL.SaveItem(objAuthorType, "U");
        }

        public int Delete(string Id)
        {
            AuthorType obj = new AuthorType();                
            return AuthorTypeDAL.SaveItem(obj, "D");
        }

        public AuthorType AuthorTypeGet(int Id)
        {
            return AuthorTypeDAL.GetItemList(Id, "","").Single();
        }
        
        public List<AuthorType> AuthorTypeGet(int intAuthorTypeID, string strAuthorType, string strCompanyID)
        {
            return AuthorTypeDAL.GetItemList(intAuthorTypeID, strAuthorType,strCompanyID);
        }

    }
}
