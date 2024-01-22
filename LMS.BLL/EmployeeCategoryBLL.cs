using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class EmployeeCategoryBLL
    {
        public EmployeeCategory EmployeeCategoryGet(Int32 intCode)
        {
            return EmployeeCategoryDAL.GetItemList(intCode).Single();
        }

        public List<EmployeeCategory> EmployeeCategoryGetAll()
        {
            return EmployeeCategoryDAL.GetItemList(0);
        }
        /// <summary>
        /// Added For MPA
        /// </summary>
        /// <returns></returns>
        public List<EmployeeType> EmployeeTypeGetAll()
        {
            return EmployeeCategoryDAL.GetEmployeeTypeList(0);
        }
        /// <summary>
        /// Added For MPA
        /// </summary>
        /// <returns></returns>
        public List<Country> CountryGetAll()
        {
            return EmployeeCategoryDAL.GetCountryList(0);
        }

        /// <summary>
        /// Added For BEPZA
        /// </summary>
        /// <returns></returns>
        public List<JobGrade> JobGradeGetAll()
        {
            return EmployeeCategoryDAL.GetJobGradeAll(0);
        }

    }
}
