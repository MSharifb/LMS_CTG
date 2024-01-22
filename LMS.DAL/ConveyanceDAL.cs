using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using TVL.DB;
using System.Data;

namespace LMS.DAL
{
    public class ConveyanceDAL
    {
        public static List<ConveyanceMaster> GetMasterList(string strAuthorID, int recordID, int outofOfficeID, string strEmpID, string dtDate, string isApproved, int startIndex, int rowNumber, out int numTotalRows)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@RECORDID", recordID, DbType.Int32));
                cpList.Add(new CustomParameter("@OUTOFOFFICEID", outofOfficeID, DbType.Int32));
                cpList.Add(new CustomParameter("@STRAUTHORID", strAuthorID, DbType.String));
                cpList.Add(new CustomParameter("@STREMPID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@STRDATE", dtDate, DbType.String));
                cpList.Add(new CustomParameter("@ISAPPROVED", isApproved, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPCONVEYANCEMASTERGET");
                numTotalRows = (int)paramval;
                List<ConveyanceMaster> results = new List<ConveyanceMaster>();
                foreach (DataRow dr in dt.Rows)
                {
                    ConveyanceMaster obj = new ConveyanceMaster();
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

        public static List<ConveyanceDetails> GetConveyanceDetails(Int64 recordID, Int64 strConveyanceID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@RECORDID", recordID, DbType.Int32));
                cpList.Add(new CustomParameter("@CONVEYANCEID", strConveyanceID, DbType.String));
                
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPCONVEYANCEDETAILSGET");

                List<ConveyanceDetails> results = new List<ConveyanceDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    ConveyanceDetails obj = new ConveyanceDetails();
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

        public static int ApproveConveyance(int CONVEYANCEID,string voucherNumber, string ApprovedBy, string strMode)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@CONVEYANCEID", CONVEYANCEID, DbType.Int32));
                cpList.Add(new CustomParameter("@VOUCHERNUMBER", voucherNumber, DbType.String));
                cpList.Add(new CustomParameter("@APPROVEDBY", ApprovedBy, DbType.String));
                cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPCONVEYANCEUPDATE");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static List<ConveyanceApproverDetails> GetConveyanceApproverDetails(Int64 CONVEYANCEID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@CONVEYANCEID", CONVEYANCEID, DbType.Int64));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPGETCONVEYANCEAPPROVERINFO");

                List<ConveyanceApproverDetails> results = new List<ConveyanceApproverDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    ConveyanceApproverDetails obj = new ConveyanceApproverDetails();
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
