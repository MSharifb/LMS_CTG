using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using LMSEntity;
using System.Data;

namespace LMS.DAL
{
    public class MiscellaneousReportDAL
    {
        public static List<MiscellaneousReport> GetReportData(string strEmpID, string strDepartmentID, string strFromDate, string strToDate, int startIndex, int rowNumber, out int numTotalRows)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@STREMPID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@STRDEPARTMENTID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@STRFROMDATE", strFromDate, DbType.String));
                cpList.Add(new CustomParameter("@STRTODATE", strToDate, DbType.String));
                cpList.Add(new CustomParameter("@STARTROWINDEX", startIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@MAXIMUMROWS", rowNumber, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPMISCELLANEOUSREPORTGET");
                numTotalRows = (int)paramval;
                List<MiscellaneousReport> results = new List<MiscellaneousReport>();
                foreach (DataRow dr in dt.Rows)
                {
                    MiscellaneousReport obj = new MiscellaneousReport();
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
