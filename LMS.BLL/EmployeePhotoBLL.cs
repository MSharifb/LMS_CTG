using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class EmployeePhotoBLL
    {
        public EmployeePhoto EmployeePhotoGet(string EmpId)
        {
            int p = 0;
            List<EmployeePhoto> empList = EmployeePhotoDAL.GetItemList(EmpId, "strEmpID", "asc", 1, 1, out p);

            if (empList.Count > 0)
            {
                return empList.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }      
    }
}
