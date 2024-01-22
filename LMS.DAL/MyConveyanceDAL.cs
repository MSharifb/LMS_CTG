using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using TVL.DB;
using System.Data;

namespace LMS.DAL
{
    public class MyConveyanceDAL
    {
        public static List<MyConveyanceMaster> GetMasterList(int outofOfficeID, string strEmpID, string dtDate, int startIndex, int rowNumber, out int numTotalRows)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@OUTOFOFFICEID", outofOfficeID, DbType.Int32));
                cpList.Add(new CustomParameter("@STREMPID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@STRDATE", dtDate, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPMYCONVEYANCEMASTERGET");
                numTotalRows = (int)paramval;
                List<MyConveyanceMaster> results = new List<MyConveyanceMaster>();
                foreach (DataRow dr in dt.Rows)
                {
                    MyConveyanceMaster obj = new MyConveyanceMaster();
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


        public static List<MyConveyanceMaster> GetMasterList(string strAuthorID, int recordID, int outofOfficeID, string strEmpID, string dtDate, string isApproved, int startIndex, int rowNumber, out int numTotalRows)
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
                List<MyConveyanceMaster> results = new List<MyConveyanceMaster>();
                foreach (DataRow dr in dt.Rows)
                {
                    MyConveyanceMaster obj = new MyConveyanceMaster();
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

        public static List<MyConveyanceDetails> GetConveyanceDetails(Int64 recordID, Int64 strConveyanceID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@OUTOFOFFICEID", strConveyanceID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPMYCONVEYANCEDETAILGET");

                List<MyConveyanceDetails> results = new List<MyConveyanceDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    MyConveyanceDetails obj = new MyConveyanceDetails();
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

      
        public static List<ConveyanceApproverDetails> GetConveyanceApproverDetails(Int64 CONVEYANCEID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@OUTOFOFFICEID", CONVEYANCEID, DbType.Int64));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPMYCONVEYANCEDETAILGET");

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
