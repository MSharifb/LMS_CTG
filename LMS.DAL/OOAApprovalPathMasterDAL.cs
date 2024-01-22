using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;


namespace LMS.DAL
{
    public class OOAApprovalPathMasterDAL
    {
        public static List<OOAApprovalPathMaster> GetItemList(int intPathID, string strPathName, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intPathID", intPathID, DbType.Int32));
                cpList.Add(new CustomParameter("@strPathName", strPathName, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAApprovalPathMasterGet");

                List<OOAApprovalPathMaster> results = new List<OOAApprovalPathMaster>();
                foreach (DataRow dr in dt.Rows)
                {
                    OOAApprovalPathMaster obj = new OOAApprovalPathMaster();

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

        public static List<OOAApprovalPathMaster> GetOOAApprovalAuthorityPath(string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAAuthorityPathGet");

                List<OOAApprovalPathMaster> results = new List<OOAApprovalPathMaster>();
                foreach (DataRow dr in dt.Rows)
                {
                    OOAApprovalPathMaster obj = new OOAApprovalPathMaster();

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



        public static int SaveItem(OOAApprovalPathMaster obj, string strMode, IDbTransaction transacrtion, IDbConnection con)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intPathID", obj.intPathID, DbType.Int32));
            cpList.Add(new CustomParameter("@intFlowType", obj.intFlowType, DbType.Int32));
            cpList.Add(new CustomParameter("@strPathName", obj.strPathName, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                if (transacrtion != null)
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transacrtion, con, "LMS_uspOOAApprovalPathMasterSave");
                }
                else
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAApprovalPathMasterSave");
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return -5000;
            }
        }
    }
}
