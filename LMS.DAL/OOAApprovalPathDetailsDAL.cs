using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class OOAApprovalPathDetailsDAL
    {
        public static List<OOAApprovalPathDetails> GetItemList(int intNodeID, int intParentNodeID, int intPathID, string strCompanyID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intNodeID", intNodeID, DbType.Int32));
                cpList.Add(new CustomParameter("@intParentNodeID", intParentNodeID, DbType.Int32));
                cpList.Add(new CustomParameter("@intPathID", intPathID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAApprovalPathDetailsGet");

                List<OOAApprovalPathDetails> results = new List<OOAApprovalPathDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    OOAApprovalPathDetails obj = new OOAApprovalPathDetails();

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

        public static int SaveItem(OOAApprovalPathDetails obj, string strMode, IDbTransaction transacrtion, IDbConnection con)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intNodeID", obj.intNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@intPathID", obj.intPathID, DbType.Int32));
            cpList.Add(new CustomParameter("@strNodeName", obj.strNodeName, DbType.String));
            cpList.Add(new CustomParameter("@intAuthorTypeID", obj.intAuthorTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@intParentNodeID", obj.intParentNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strRootPath", obj.strRootPath, DbType.Int32));
            cpList.Add(new CustomParameter("@isEdit", obj.isEdit,DbType.Boolean));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transacrtion, con, "LMS_uspOOAApprovalPathDetailsSave");

            }
            catch (Exception ex)
            {

                throw ex;
                return -5000;
            }
        }
        public static int SaveItem(OOAApprovalPathDetails obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intNodeID", obj.intNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@intPathID", obj.intPathID, DbType.Int32));
            cpList.Add(new CustomParameter("@strNodeName", obj.strNodeName, DbType.String));
            cpList.Add(new CustomParameter("@intAuthorTypeID", obj.intAuthorTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@intParentNodeID", obj.intParentNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strRootPath", obj.strRootPath, DbType.Int32));
            cpList.Add(new CustomParameter("@isEdit", obj.isEdit, DbType.Boolean));

            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();                                                                                        
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAApprovalPathDetailsSave");

            }
            catch (Exception ex)
            {

                throw ex;
                return -5000;
            }
        }
    }
}
