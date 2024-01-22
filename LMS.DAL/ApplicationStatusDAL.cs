using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class ApplicationStatusDAL
    {
        public static List<ApplicationStatusCaption> GetItemList(string strCompanyID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspApplicationStatusGet");

                List<ApplicationStatusCaption> results = new List<ApplicationStatusCaption>();
                foreach (DataRow dr in dt.Rows)
                {

                    ApplicationStatusCaption obj = new ApplicationStatusCaption();

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
