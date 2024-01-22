using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using LMSEntity;
using System.Data;

namespace LMS.DAL
{
    public class MiscellaneousVoucherDAL
    {
        public static int Save(MiscellaneousVoucher obj , string strMode)
        {
          
            try
            {
                int i = 0;
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@RECORDID",obj.RECORDID, DbType.Int64));
                cpList.Add(new CustomParameter("@MISCID", obj.MISCID, DbType.Int64));
                cpList.Add(new CustomParameter("@VOUCHERNUMBER", obj.VOUCHERNUMBER, DbType.String));
                cpList.Add(new CustomParameter("@STRAUTHORID", obj.STRAUTHORID, DbType.String));
                cpList.Add(new CustomParameter("@STREMPID", obj.STREMPID, DbType.String));
                cpList.Add(new CustomParameter("@MISCDATE", obj.MISCDATE, DbType.Date));
                cpList.Add(new CustomParameter("@UNITID", obj.UNITID, DbType.Date));
                cpList.Add(new CustomParameter("@ISAPPROVED", obj.ISAPPROVED, DbType.String));
                cpList.Add(new CustomParameter("@APPROVEDBY", obj.APPROVEDBY, DbType.String));
                cpList.Add(new CustomParameter("@APPROVEDDATE", obj.APPROVEDDATE, DbType.Date));
                cpList.Add(new CustomParameter("@STRMODE", strMode,DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                i =  db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_MISCELLANEOUSVOUCHERSAVE");
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MiscellaneousVoucher> GetData(MiscellaneousVoucher obj, int startIndex, int rowNumber, out int numTotalRows)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@RECORDID", obj.RECORDID, DbType.Int64));
                cpList.Add(new CustomParameter("@MISCID", obj.MISCID, DbType.Int64));
                cpList.Add(new CustomParameter("@STRAUTHORID", obj.STRAUTHORID, DbType.String));
                cpList.Add(new CustomParameter("@STREMPID", obj.STREMPID, DbType.String));
                cpList.Add(new CustomParameter("@MISCDATE", (obj.MISCDATE == DateTime.MinValue?"":obj.MISCDATE.ToString("dd-MM-yyyy")), DbType.String));
                cpList.Add(new CustomParameter("@ISAPPROVED", obj.ISAPPROVED, DbType.String));
                cpList.Add(new CustomParameter("@STARTINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@ROWNUMBER", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));



                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_MISCELLANEOUSVOUCHERGET");
                numTotalRows = (int)paramval;
                List<MiscellaneousVoucher> results = new List<MiscellaneousVoucher>();
                foreach (DataRow dr in dt.Rows)
                {
                    MiscellaneousVoucher obj1 = new MiscellaneousVoucher();
                    MapperBase.GetInstance().MapItem(obj1, dr); ;
                    results.Add(obj1);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public static List<ConveyanceApproverDetails> GetApproverDetails(Int64 VOUCHERID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@VOUCHERID", VOUCHERID, DbType.Int64));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "MISC_USPGETAPPROVERINFO");

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
