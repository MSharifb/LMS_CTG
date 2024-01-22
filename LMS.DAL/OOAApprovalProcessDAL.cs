using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class OOAApprovalProcessDAL
    {
        public static int ApprovalProcess(Int64 OutOfOfficeID,string strType,string strEUser)
        {
            int i = -1;
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@OUTOFOFFICEID", OutOfOfficeID, DbType.Int64));
            cpList.Add(new CustomParameter("@STRTYPE", strType, DbType.String));
            cpList.Add(new CustomParameter("@STREUSER", strEUser, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();

                i = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval,null, "OOA_USPOUTOFOFFICEAPPROVALPROCESSSAVE");
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public static bool isReverify(string authorID,string strEmpID,int intFlowType)
        {
           
            bool isReverse = false;
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@STRAUTHORID", authorID, DbType.String));
            cpList.Add(new CustomParameter("@STREMPID", strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@INTFLOWTYPE", intFlowType, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "OOA_USPISREVERIFY");
                if (dt == null)
                {
                    isReverse = true;
                }
                else if (dt.Rows.Count < 1)
                {
                    isReverse = true;
                }

                return isReverse;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static List<Employee> GetApproverInfo(Int64 OutOfOfficeID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@OUTOFOFFICEID", OutOfOfficeID, DbType.Int64));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "OOA_USPGETAPPROVERINFO");

                List<Employee> results = new List<Employee>();
                foreach (DataRow dr in dt.Rows)
                {
                    Employee obj = new Employee();

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
