using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using TVL.DB;
using System.Data;

namespace LMS.DAL
{
    public class MiscApprovalDAL
    {
        public static List<MISCApproval> GetData(string strAuthorID,string strEmpID, Int64 miscMasterID, Int64 intAppFlowID, string miscDate, int StartIndex, int RowNumber, out int numTotalRows)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@STRAUTHORID", strAuthorID, DbType.String));
                cpList.Add(new CustomParameter("@MISCMASTERID", miscMasterID, DbType.Int64));
                cpList.Add(new CustomParameter("@INTAPPFLOWID", intAppFlowID, DbType.Int64));
                cpList.Add(new CustomParameter("@STREMPID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@MISCDATE", miscDate, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", StartIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", RowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_USPAPPROVALFLOWGET");
                numTotalRows = (int)paramval;

                List<MISCApproval> results = new List<MISCApproval>();
                foreach (DataRow dr in dt.Rows)
                {
                    MISCApproval obj = new MISCApproval();

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


        public static string GetApprovalStatus(int  MiscMasterID)
        {
            try
            {
                string result = "";
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@MISCMASTERID", MiscMasterID, DbType.Int32));
                

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_USPMISCSTATUSGET");

                result = dt.Rows[0][0].ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int ApprovalProcess(Int64 MiscMasterID, string strType, string strEUser)
        {
            int i = -1;
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@MISCMASTERID", MiscMasterID, DbType.Int64));
            cpList.Add(new CustomParameter("@STRTYPE", strType, DbType.String));
            cpList.Add(new CustomParameter("@STREUSER", strEUser, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();

                i = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_USPMISCAPPROVALPROCESSSAVE");
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


      

    }
}
