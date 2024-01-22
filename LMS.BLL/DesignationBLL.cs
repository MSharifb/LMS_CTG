using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class DesignationBLL
    {

        public int Add(Designation objDesignation)
        {
            return DesignationDAL.SaveItem(objDesignation, "I");

        }
        public int Edit(Designation objDesignation)
        {
            return DesignationDAL.SaveItem(objDesignation, "U");
        }
        public int Delete(string Id)
        {
            Designation obj = new Designation();
            obj.strDesignationID = Id;          
            return DesignationDAL.SaveItem(obj, "D");
        }

        public Designation DesignationGet(string Id)
        {
            return DesignationDAL.GetItemList(Id, "","").Single();
        }
        public List<Designation> DesignationGetAll()
        {
            return DesignationDAL.GetItemList("", "","");
        }

        public List<Designation> DesignationGet(string strDesignationID, string strDesignation, string strCompanyID)
        {
            return DesignationDAL.GetItemList(strDesignationID, strDesignation,strCompanyID);
        }
    }
}
