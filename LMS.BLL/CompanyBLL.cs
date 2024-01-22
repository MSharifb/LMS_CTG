using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class CompanyBLL
    {
        public int Add(Company objCompany)
        {
            return CompanyDAL.SaveItem(objCompany, "I");

        }
        public int Edit(Company objCompany)
        {
            return CompanyDAL.SaveItem(objCompany, "U");
        }
        public int Delete(string Id)
        {
            Company obj = new Company();
            obj.strCompanyID = Id;
            
            return CompanyDAL.SaveItem(obj, "D");
        }

        public Company CompanyGet(string Id)
        {
            return CompanyDAL.GetItemList(Id, "").Single();
        }
        public List<Company> CompanyGetAll()
        {
            return CompanyDAL.GetItemList("", "");
        }


        public List<Company> CompanyGet(string strCompanyID, string strCompany)
        {
            return CompanyDAL.GetItemList(strCompanyID, strCompany);
        }
    }
}
