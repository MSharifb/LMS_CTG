using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class DepartmentBLL
    {

        public int Add(Department objDepartment)
        {
            return DepartmentDAL.SaveItem(objDepartment, "I");
        }
        public int Edit(Department objDepartment)
        {

            return DepartmentDAL.SaveItem(objDepartment, "U");
        }
        public int Delete(string Id)
        {
            Department obj = new Department();
            obj.strDepartmentID = Id;

            return DepartmentDAL.SaveItem(obj, "D");
        }

        public Department DepartmentGet(string Id)
        {
            return DepartmentDAL.GetItemList(Id, "", "").Single();
        }
        public List<Department> DepartmentGetAll()
        {
            return DepartmentDAL.GetItemList("", "", "");
        }

        public List<Zone> ZoneGetAll()
        {
            return DepartmentDAL.GetZoneList();
        }

        public List<Department> DepartmentGet(string strDepartmentID, string strDepartment, string strCompanyID)
        {
            return DepartmentDAL.GetItemList(strDepartmentID, strDepartment, strCompanyID);
        }
    }
}
