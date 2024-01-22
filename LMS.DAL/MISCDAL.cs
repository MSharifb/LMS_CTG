using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using TVL.DB;
using System.Data;
namespace LMS.DAL
{
    public class MISCDAL
    {
        public static int Save(MISCMaster obj, IDbTransaction transaction, IDbConnection con, string userID, string strMode)
        {
            int i = -1;

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@MISCMASTERID", obj.MISCMASTERID, DbType.Int64));
            cpList.Add(new CustomParameter("@STREMPID", obj.STREMPID, DbType.String));
            cpList.Add(new CustomParameter("@STRCOMPANYID", obj.STRCOMPANYID, DbType.String));
            cpList.Add(new CustomParameter("@MISCDATE", obj.MISCDATE, DbType.Date));
            cpList.Add(new CustomParameter("@STRIUSERID", userID, DbType.String));
            cpList.Add(new CustomParameter("@STREUSERID", userID, DbType.String));
            cpList.Add(new CustomParameter("@UNITID", obj.UNITID, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.String));


            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();

                i = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction, con, "MISC_uspMISCMasterSave");
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<MISCMaster> Get(MISCMaster searchObj,int startIndex, int rowNumber, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


                cpList.Add(new CustomParameter("@MISCMASTERID", searchObj.MISCMASTERID, DbType.Int64));
                cpList.Add(new CustomParameter("@STREMPID", searchObj.STREMPID, DbType.String));
                cpList.Add(new CustomParameter("@MISCDATE", (searchObj.MISCDATE == DateTime.MinValue ? "" : searchObj.MISCDATE.ToString("dd-MM-yyyy")), DbType.String));
                cpList.Add(new CustomParameter("@MISCToDATE", (searchObj.MISCToDATE == DateTime.MinValue ? "" : searchObj.MISCToDATE.ToString("dd-MM-yyyy")), DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_uspMISCMasterGet");
                numTotalRows = (int)paramval;
                List<MISCMaster> results = new List<MISCMaster>();
                foreach (DataRow dr in dt.Rows)
                {
                    MISCMaster obj = new MISCMaster();

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

        public static List<MISCMaster> GetSearchedItemList(int intSearchID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@MISCMASTERID", intSearchID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_uspMISCSearchDetailsGet");

                List<MISCMaster> results = new List<MISCMaster>();
                foreach (DataRow dr in dt.Rows)
                {
                    MISCMaster obj = new MISCMaster();

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

        public static List<MISCMaster> GetSearchData(MISCMaster searchObj, int startIndex, int rowNumber, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


                cpList.Add(new CustomParameter("@MISCMASTERID", searchObj.MISCMASTERID, DbType.Int64));
                cpList.Add(new CustomParameter("@STREMPID", searchObj.STREMPID, DbType.String));
                cpList.Add(new CustomParameter("@MISCDATE", (searchObj.MISCDATE == DateTime.MinValue?"": searchObj.MISCDATE.ToString("dd-MM-yyyy")), DbType.String));
                cpList.Add(new CustomParameter("@MISCToDATE", (searchObj.MISCToDATE == DateTime.MinValue ? "" : searchObj.MISCToDATE.ToString("dd-MM-yyyy")), DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_uspMISCMasterGet");
                numTotalRows = (int)paramval;
                List<MISCMaster> results = new List<MISCMaster>();
                foreach (DataRow dr in dt.Rows)
                {
                    MISCMaster obj = new MISCMaster();

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
        public static int SaveDetails(MISCDetails obj, IDbTransaction transaction, IDbConnection con, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@MISCDETAISLID", obj.MISCDETAISLID, DbType.Int64));
            cpList.Add(new CustomParameter("@MISCMASTERID", obj.MISCMASTERID, DbType.Int64));
            cpList.Add(new CustomParameter("@STRPARTICULAR", obj.STRPARTICULAR, DbType.String));
            cpList.Add(new CustomParameter("@AMOUNT", obj.AMOUNT, DbType.Decimal));
            cpList.Add(new CustomParameter("@APPROVEDAMOUNT", obj.APPROVEDAMOUNT, DbType.Decimal));
            cpList.Add(new CustomParameter("@STRPURPOSE", obj.STRPURPOSE, DbType.String));
            cpList.Add(new CustomParameter("@STRREMARKS", obj.STRREMARKS, DbType.String));
            cpList.Add(new CustomParameter("@ATTACHMENTPATH", obj.ATTACHMENTPATH, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.Date));


            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction, con, "MISC_uspMISCDetailsSave");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static int SaveDetails(MISCDetails obj,  string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@MISCDETAISLID", obj.MISCDETAISLID, DbType.Int64));
            cpList.Add(new CustomParameter("@MISCMASTERID", obj.MISCMASTERID, DbType.Int64));
            cpList.Add(new CustomParameter("@STRPARTICULAR", obj.STRPARTICULAR, DbType.String));
            cpList.Add(new CustomParameter("@AMOUNT", obj.AMOUNT, DbType.Decimal));
            cpList.Add(new CustomParameter("@APPROVEDAMOUNT", obj.APPROVEDAMOUNT, DbType.Decimal));
            cpList.Add(new CustomParameter("@STRPURPOSE", obj.STRPURPOSE, DbType.String));
            cpList.Add(new CustomParameter("@STRREMARKS", obj.STRREMARKS, DbType.String));
            cpList.Add(new CustomParameter("@ATTACHMENTPATH", obj.ATTACHMENTPATH, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.Date));


            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes,ref paramval, null, "MISC_uspMISCDetailsSave");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<MISCDetails> GetDetails(MISCDetails searchObj,int startIndex, int rowNumber)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


                cpList.Add(new CustomParameter("@MISCDETAISLID", searchObj.MISCDETAISLID, DbType.Int64));
                cpList.Add(new CustomParameter("@MISCMASTERID", searchObj.MISCMASTERID, DbType.Int64));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_uspMISCDetailsGet");

                List<MISCDetails> results = new List<MISCDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    MISCDetails obj = new MISCDetails();

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
          public static List<MISCDetails> GetDetails(int MISCMASTERID,int startIndex, int rowNumber)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();


              
                cpList.Add(new CustomParameter("@MISCMASTERID", MISCMASTERID, DbType.Int64));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_uspMISCSearchDetailsGet");

                List<MISCDetails> results = new List<MISCDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    MISCDetails obj = new MISCDetails();

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

        
        public static List<MISCDetails> GetDetailsData(int NodeID, int intSearchID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@MISCDETAISLID", NodeID, DbType.Int64));
                cpList.Add(new CustomParameter("@MISCMASTERID", intSearchID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_uspMISCDetailsGet");

                List<MISCDetails> results = new List<MISCDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    MISCDetails obj = new MISCDetails();

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
