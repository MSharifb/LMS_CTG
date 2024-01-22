using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class DataSynchronizerDAL
    {
        public static int SaveItem(DataSynchronizer obj,string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@bitCompany", obj.bitCompany, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitDepartment", obj.bitDepartment, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitLocation", obj.bitLocation, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitDesignation", obj.bitDesignation, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitReligion", obj.bitReligion, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitEmployee", obj.bitEmployee, DbType.Boolean));
            cpList.Add(new CustomParameter("@bitEmployeeCategory", obj.bitEmployeeCategory, DbType.Boolean));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            DBHelper db = new DBHelper();
            try
            {
                object paramval = null;                
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspInitiate");

            }
            catch (Exception ex)
            {
                return -5000;
            }
        }
    }
}
