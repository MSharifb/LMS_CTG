using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;


namespace LMS.DAL
{
    public class AuthorTypeDAL
    {
        public static List<AuthorType> GetItemList(int intAuthorTypeID, string strAuthorType, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intAuthorTypeID", intAuthorTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strAuthorType", strAuthorType, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspAuthorTypeGet");

                List<AuthorType> results = new List<AuthorType>();
                foreach (DataRow dr in dt.Rows)
                {
                    AuthorType obj = new AuthorType();

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

        public static int SaveItem(AuthorType obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, null, null, "LMS_uspAuthorTypeSave");
            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
