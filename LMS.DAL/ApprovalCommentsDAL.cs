using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class ApprovalCommentsDAL
    {
        public static List<ApprovalComments> GetItemList(int intAppFlowID, long intApplicationID, int intAppStatusID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intAppFlowID", intAppFlowID, DbType.Int32));
                cpList.Add(new CustomParameter("@intApplicationID", intApplicationID, DbType.Int32));
                cpList.Add(new CustomParameter("@intAppStatusID", intAppStatusID, DbType.Int32));

                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspApprovalCommentsGet");

                List<ApprovalComments> results = new List<ApprovalComments>();
                foreach (DataRow dr in dt.Rows)
                {
                    ApprovalComments obj = new ApprovalComments();

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


        public static int SaveItem(ApprovalComments obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspApprovalComments");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }
    }
}
