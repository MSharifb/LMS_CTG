using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class EmployeeWiseOOAApprovalPathDAL
    {
        public static List<EmployeeWiseOOAApprovalPath> GetItemList(int intFlowType,int Id, int intPathID, string strForAll, string strCompanyID,
                                                                string strEmpID, string strEmpName, int intNodeID, string strDepartmentID,
                                                                string strDesignationID, string strLocationID, string strAuthorID, string strAuthorName,
                                                                string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {
            List<EmployeeWiseOOAApprovalPath> results = new List<EmployeeWiseOOAApprovalPath>();
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intFLowType", intFlowType, DbType.Int32));
                cpList.Add(new CustomParameter("@intEmpPathID", Id, DbType.Int32));
                cpList.Add(new CustomParameter("@intPathID", intPathID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strForAll", strForAll, DbType.String));

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strEmpName", strEmpName, DbType.String));
                cpList.Add(new CustomParameter("@intNodeID", intNodeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strLocationID", strLocationID, DbType.String));
                cpList.Add(new CustomParameter("@strAuthorID", strAuthorID, DbType.String));
                cpList.Add(new CustomParameter("@strAuthorName", strAuthorName, DbType.String));

                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspEmployeeWiseOOAApprovalPathGet");

                numTotalRows = (int)paramval;
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeWiseOOAApprovalPath obj = new EmployeeWiseOOAApprovalPath();

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

        public static int SaveItem(EmployeeWiseOOAApprovalPath obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intEmpPathID", obj.intEmpPathID, DbType.Int32));
            cpList.Add(new CustomParameter("@intFlowType", obj.intFlowType, DbType.Int32));
            cpList.Add(new CustomParameter("@intPathID", obj.intPathID, DbType.Int32));
            cpList.Add(new CustomParameter("@intNodeID", obj.intNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strApplyType", obj.strApplyType, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.String));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strLocationID", obj.strLocationID, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspEmployeeWiseOOAApprovalPathSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

        public static DataTable GetFlowTypewise(int flowType)
        { 
          
             try
              {
                    CustomParameterList cpList = new CustomParameterList();

                    cpList.Add(new CustomParameter("@INTFLOWTYPE",  flowType, DbType.Int32));
                
                    object paramval = null;
                    DBHelper db = new DBHelper();
                    DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPFLOWTYPEWISEGET");
                            
                    return dt;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
        }
    }
}
