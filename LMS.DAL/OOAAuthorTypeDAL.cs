using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;


namespace LMS.DAL
{
    public class OOAAuthorTypeDAL
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
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_uspAuthorTypeGet");

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


        public static int GetAuthorTypeID(string strAuthorID, string strEMPID)
        {

            try
            {
                int intAuthorTypeID = -1;
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@STRAUTHORID", strAuthorID, DbType.Int32));
                cpList.Add(new CustomParameter("@STREMPID", strEMPID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_GETAUTHORTYPEID");
                               
                foreach (DataRow dr in dt.Rows)
                {
                    intAuthorTypeID = int.Parse(dr["INTAUTHORTYPEID"].ToString());
                }
                return intAuthorTypeID;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static List<OOAApprovalPathDetails> GetAuthorEmployeeWise(string strAuthorID, string strEMPID)
        {

            try
            {                
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@STRAUTHORID", strAuthorID, DbType.Int32));
                cpList.Add(new CustomParameter("@STREMPID", strEMPID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_GETAUTHORTYPEID");

                List<OOAApprovalPathDetails> result = new List<OOAApprovalPathDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    OOAApprovalPathDetails obj = new OOAApprovalPathDetails();
                    MapperBase.GetInstance().MapItem(obj,dr);
                    result.Add(obj);
                }

                return result;
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
