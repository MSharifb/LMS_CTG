using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class EmployeeCategoryDAL
    {
        public static List<EmployeeCategory> GetItemList(Int32 intCategoryCode)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryCode, DbType.Int32));
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspEmployeeCategoryGet");

                List<EmployeeCategory> results = new List<EmployeeCategory>();
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeCategory obj = new EmployeeCategory();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        /// <summary>
        /// Added For MPA
        /// </summary>
        /// <param name="intEmpTypeCode"></param>
        /// <returns></returns>
        public static List<EmployeeType> GetEmployeeTypeList(Int32 intEmpTypeCode)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intTypeID", intEmpTypeCode, DbType.Int32));
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspEmployeeTypeGet");

                List<EmployeeType> results = new List<EmployeeType>();
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeType obj = new EmployeeType();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// Added For MPA
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public static List<Country> GetCountryList(int countryId)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intCountryID", countryId, DbType.Int32));
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspCountryGet");

                List<Country> results = new List<Country>();
                foreach (DataRow dr in dt.Rows)
                {
                    Country obj = new Country();
                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        /// <summary>
        /// Added For BEPZA
        /// </summary>
        /// <param name="jobGradeId"></param>
        /// <returns></returns>
        public static List<JobGrade> GetJobGradeAll(int jobGradeId)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intGradeId", jobGradeId, DbType.Int32));
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspJobGradeGet");

                List<JobGrade> results = new List<JobGrade>();
                foreach (DataRow dr in dt.Rows)
                {
                    JobGrade obj = new JobGrade();
                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        
    }
}
