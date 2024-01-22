using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class ApprovalProcessDAL
    {
        public static List<ApprovalProcess> GetItemList(int intApprovalProcessId, int intModuleId, string strApprovalProcessName)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intApprovalProcessId", intApprovalProcessId, DbType.Int32));
                cpList.Add(new CustomParameter("@intModuleId", intModuleId, DbType.Int32));
                cpList.Add(new CustomParameter("@strApprovalProcessName", strApprovalProcessName, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspApprovalProcessGet");

                List<ApprovalProcess> results = new List<ApprovalProcess>();
                foreach (DataRow dr in dt.Rows)
                {
                    ApprovalProcess obj = new ApprovalProcess();
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

        public static int SaveItem(ApprovalProcess obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intApprovalProcessId", obj.intApprovalProcessId, DbType.Int32));
            cpList.Add(new CustomParameter("@intModuleId", obj.intModuleId, DbType.Int32));
            cpList.Add(new CustomParameter("@strApprovalProcessName", obj.strApprovalProcessName, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspApprovalProcessSave");
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}

