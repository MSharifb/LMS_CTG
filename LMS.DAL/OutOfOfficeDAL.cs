using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using TVL.DB;
using System.Data;

namespace LMS.DAL
{
    public class OutOfOfficeDAL
    {
        public static int Save(OutOfOffice obj,IDbTransaction transaction, IDbConnection con,string company,string userID, string strMode)
        {
            int i = -1;
           
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@ID", obj.ID, DbType.Int64));
            cpList.Add(new CustomParameter("@STREMPID", obj.STREMPID, DbType.String));
            cpList.Add(new CustomParameter("@PURPOSE", obj.PURPOSE, DbType.String));
            cpList.Add(new CustomParameter("@OTHERPURPOSE", obj.OTHERPURPOSE, DbType.String));
            cpList.Add(new CustomParameter("@REPONSIBLEPERSONID", obj.RESPONSIBLEPERSONID, DbType.String));
            cpList.Add(new CustomParameter("@CONTACTPHONE", obj.CONTACTPHONE, DbType.String));
            cpList.Add(new CustomParameter("@GETOUTDATE", obj.GETOUTDATE, DbType.Date));
            cpList.Add(new CustomParameter("@GETOUTTIME", obj.GETOUTTIME, DbType.String));
            cpList.Add(new CustomParameter("@EXPGETINDATE", obj.EXPGETINDATE, DbType.Date));
            cpList.Add(new CustomParameter("@EXPGETINTIME", obj.EXPGETINTIME, DbType.String));
            cpList.Add(new CustomParameter("@GETINDATE", obj.GETINDATE, DbType.Date));
            cpList.Add(new CustomParameter("@GETINTIME", obj.GETINTIME, DbType.String));
            cpList.Add(new CustomParameter("@COMMENTS", obj.COMMENTS, DbType.String));
            cpList.Add(new CustomParameter("@STRCOMPANY", company, DbType.String));
            cpList.Add(new CustomParameter("@STRIUSER", userID, DbType.String));
            cpList.Add(new CustomParameter("@STREUSER", userID, DbType.String));
            cpList.Add(new CustomParameter("@ISGETIN", obj.ISGETIN, DbType.String));
            cpList.Add(new CustomParameter("@STATUS", obj.STATUS,DbType.String));
            cpList.Add(new CustomParameter("@PERMITTEDBY", obj.PERMITTEDBY, DbType.String));
            cpList.Add(new CustomParameter("@VERIFIEDBY", obj.VERIFIEDBY, DbType.String));
            cpList.Add(new CustomParameter("@APPROVEDBY", obj.APPROVEDBY, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE",strMode, DbType.String));
            
           

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                
                i = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes,transaction,con, "LMS_USPOUTOFOFFICESAVE");
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static List<OutOfOffice> Get(OutOfOffice searchObj, DateTime fromDate, DateTime toDate, int startIndex, int rowNumber, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


                cpList.Add(new CustomParameter("@ID", searchObj.ID, DbType.Int64));
                cpList.Add(new CustomParameter("@STREMPID", searchObj.STREMPID, DbType.String));

                cpList.Add(new CustomParameter("@FROMDATE", fromDate, DbType.Date));
                cpList.Add(new CustomParameter("@TODATE", toDate, DbType.Date));
                cpList.Add(new CustomParameter("@PURPOSE", searchObj.PURPOSE, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));
                

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_USPOUTOFOFFICEGET");
                numTotalRows =  (int)paramval;
                List<OutOfOffice> results = new List<OutOfOffice>();
                foreach (DataRow dr in dt.Rows)
                {
                    OutOfOffice obj = new OutOfOffice();

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

        public static List<OutOfOffice> GetForPermissionNVerify(string strAuthorID,string strEmpID, string fromDate,int StartIndex,int RowNumber, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@AUTHORID", strAuthorID, DbType.String));
                cpList.Add(new CustomParameter("@STREMPID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@FROMDATE", fromDate, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", StartIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", RowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPGETOOADATAFORPERMISSION");
                numTotalRows = (int)paramval;

                List<OutOfOffice> results = new List<OutOfOffice>();
                foreach (DataRow dr in dt.Rows)
                {
                    OutOfOffice obj = new OutOfOffice();

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


        public static List<OutOfOffice> GetApprovalDetaisData(OutOfOffice searchObj,string strAuthorID, DateTime fromDate, DateTime toDate, int startIndex, int rowNumber, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


                cpList.Add(new CustomParameter("@ID", searchObj.ID, DbType.Int64));
                cpList.Add(new CustomParameter("@STREMPID", searchObj.STREMPID, DbType.String));
                cpList.Add(new CustomParameter("@AUTHORID",strAuthorID, DbType.String));
                cpList.Add(new CustomParameter("@FROMDATE", fromDate, DbType.Date));
                cpList.Add(new CustomParameter("@TODATE", toDate, DbType.Date));
                cpList.Add(new CustomParameter("@PURPOSE", searchObj.PURPOSE, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_USPAPPROVALPROCESSDETAILSGET");
                numTotalRows = (int)paramval;
                List<OutOfOffice> results = new List<OutOfOffice>();
                foreach (DataRow dr in dt.Rows)
                {
                    OutOfOffice obj = new OutOfOffice();

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

        public static List<OutOfOffice> GetReport(OutOfOffice searchObj, string fromDate, string toDate, int startIndex, int rowNumber, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


                cpList.Add(new CustomParameter("@ID", searchObj.ID, DbType.Int64));
                cpList.Add(new CustomParameter("@STREMPID", searchObj.STREMPID, DbType.String));

                cpList.Add(new CustomParameter("@FROMDATE", fromDate, DbType.String));
                cpList.Add(new CustomParameter("@TODATE", toDate, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_USP_REPORT_OUTOFOFFICEGET");
                numTotalRows = (int)paramval;
                List<OutOfOffice> results = new List<OutOfOffice>();
                foreach (DataRow dr in dt.Rows)
                {
                    OutOfOffice obj = new OutOfOffice();

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
        

        public static int SaveOutOfOfficeLocation(OutOfOfficeLocaton obj,IDbTransaction transaction,IDbConnection con, string strMode)
        {
           
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@RECORDID", obj.RECORDID, DbType.Int64));
            cpList.Add(new CustomParameter("@OUTOFOFFICEID", obj.OUTOFOFFICEID, DbType.Int64));
            cpList.Add(new CustomParameter("@FROMLOCATION", obj.FROMLOCATION, DbType.String));
            cpList.Add(new CustomParameter("@TOLOCATION", obj.TOLOCATION, DbType.String));
            cpList.Add(new CustomParameter("@MODE", obj.MODE, DbType.String));
            cpList.Add(new CustomParameter("@AMOUNT", obj.AMOUNT, DbType.Decimal));
            cpList.Add(new CustomParameter("@PERMITTEDAMOUNT", obj.PERMITTEDAMOUNT, DbType.Decimal));
            cpList.Add(new CustomParameter("@PURPOSE", obj.PURPOSE, DbType.String));
            cpList.Add(new CustomParameter("@bIsRoundTrip", obj.bIsRoundTrip, DbType.Boolean));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.Date));
            

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction,con, "LMS_USPOUTOFOFFICELOCATIONSAVE");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<OutOfOfficeLocaton> GetOutOfOfficeLocation(OutOfOfficeLocaton searchObj, DateTime fromDate, DateTime toDate, int startIndex, int rowNumber)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


                cpList.Add(new CustomParameter("@RECORDID", searchObj.RECORDID, DbType.Int64));
                cpList.Add(new CustomParameter("@OUTOFOFFICEID", searchObj.OUTOFOFFICEID, DbType.Int64));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_USPOUTOFOFFICELOCATIONGET");

                List<OutOfOfficeLocaton> results = new List<OutOfOfficeLocaton>();
                foreach (DataRow dr in dt.Rows)
                {
                    OutOfOfficeLocaton obj = new OutOfOfficeLocaton();

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


        public static List<SearchLocation> GetSearchLocation(string searchObj)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


                cpList.Add(new CustomParameter("@SEARCHLOCATION", searchObj, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", 0, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", 10, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPLOCATIONSEARCHGET");

                List<SearchLocation> results = new List<SearchLocation>();
                foreach (DataRow dr in dt.Rows)
                {
                    SearchLocation obj = new SearchLocation();

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
