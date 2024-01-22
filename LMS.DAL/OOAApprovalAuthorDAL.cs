using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class OOAApprovalAuthorDAL
    {
        public static List<OOAApprovalAuthor> GetItemList(int intAuthorityID, int intNodeID, string strAuthorID, string strAuthorType, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intAuthorityID", intAuthorityID, DbType.Int32));
                cpList.Add(new CustomParameter("@intNodeID", intNodeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strAuthorID", strAuthorID, DbType.String));
                cpList.Add(new CustomParameter("@strAuthorType", strAuthorType, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAApprovalAuthorGet");

                List<OOAApprovalAuthor> results = new List<OOAApprovalAuthor>();
                foreach (DataRow dr in dt.Rows)
                {
                    OOAApprovalAuthor obj = new OOAApprovalAuthor();

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


        public static List<OOAApprovalAuthor> GetOOAApprovalAuthorSteps(string strAuthorID, string strAuthorType, string strCompanyID, int @intAuthorTypeID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strAuthorID", strAuthorID, DbType.String));
                cpList.Add(new CustomParameter("@strAuthorType", strAuthorType, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@intAuthorTypeID", @intAuthorTypeID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAApprovalAuthorStepsGet");

                List<OOAApprovalAuthor> results = new List<OOAApprovalAuthor>();
                foreach (DataRow dr in dt.Rows)
                {
                    OOAApprovalAuthor obj = new OOAApprovalAuthor();

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

        public static int GetOOAApprovalPathWiseAuthority(int intpathId, string strAuthorId, string strCompanyID)
        {
            int results = 0;
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intPathID", intpathId, DbType.Int32));
                cpList.Add(new CustomParameter("@strAuthorID", strAuthorId, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspGetOOAApprovalPathWiseAuthority");

                results = Convert.ToInt32(dt.Rows[0][0]);


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return results;
        }


        public static string GetOOAApprovalAuthorsEmail(long intApplicationID, string strCompanyID, string strCutoffEmail)
        {
            string results = "";
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intApplicationID", intApplicationID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAAuthorsEmailGet");

                for (int index = 0; index < dt.Rows.Count; index++)
                {

                    string strEmail = dt.Rows[index]["strEmail"].ToString();
                    if (String.Compare(strEmail, strCutoffEmail, true) != 0)
                    {
                        results = results + strEmail + ",";
                    }

                }
                if (results.Length > 0)
                {
                    results = results.Substring(0, results.Length - 1);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return results;
        }

        public static int SaveItem(OOAApprovalAuthor obj, string strMode, IDbTransaction transaction, IDbConnection conn)
        {
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intAuthorityID", obj.intAuthorityID, DbType.Int32));
            cpList.Add(new CustomParameter("@intNodeID", obj.intNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strAuthorID", obj.strAuthorID, DbType.String));
            cpList.Add(new CustomParameter("@strAuthorType", obj.strAuthorType, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction, conn, "LMS_uspOOAApprovalAuthorSave");

            }
            catch (Exception ex)
            {

                throw ex;

                return -5000;
            }
        }

    }
}
