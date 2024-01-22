using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class EmployeeBLL
    {
        public int Add(Employee objEmployee)
        {
            return EmployeeDAL.SaveItem(objEmployee, "I");
        }
        public int Edit(Employee objEmployee)
        {
            return EmployeeDAL.SaveItem(objEmployee, "U");
        }
        public int Delete(string empId)
        {
            Employee obj = new Employee();
            obj.strEmpID = empId;

            return EmployeeDAL.SaveItem(obj, "D");
        }

        public Employee EmployeeGet(string Id)
        {
            int p = 0;
            List<Employee> empList = EmployeeDAL.GetItemList("",Id, "", "", "", "", "", "", "", "", "", "strEmpID", "asc", 1, 1, out p);
            if (empList.Count > 0)
            {
                return empList.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public Employee EmployeeGetByEmployeeId(string Id)
        {
            int p = 0;
            List<Employee> empList = EmployeeDAL.GetItemList(Id, "", "", "", "", "", "", "", "", "", "", "strEmpID", "asc", 1, 1, out p);
            if (empList.Count > 0)
            {
                return empList.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public List<Employee> EmployeeGet(string strEmpInitial, string strEmpID, string strEmpName, string ActiveStatus, string strDepartmentID, string strDesignationID, string strDesignation, string strGender, string strReligionID, string strCompanyID, string strSearchType, string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {
            return EmployeeDAL.GetItemList(strEmpInitial, strEmpID, strEmpName, ActiveStatus, strDepartmentID, strDesignationID, strDesignation, strGender, strReligionID, strCompanyID, strSearchType, strSortBy, strSortType, startRowIndex, maximumRows, out  numTotalRows);
        }
    }
}
