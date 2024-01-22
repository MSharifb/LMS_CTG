using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class CompanyUniDAL
    {
        public static List<CompanyUnit> GetList(int unitID, int companyID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@UNITID", unitID, DbType.Int32));
                cpList.Add(new CustomParameter("@COMPANYID", companyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_USPCOMPANYUNITGET");

                List<CompanyUnit> results = new List<CompanyUnit>();
                foreach (DataRow dr in dt.Rows)
                {

                    CompanyUnit obj = new CompanyUnit();

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
