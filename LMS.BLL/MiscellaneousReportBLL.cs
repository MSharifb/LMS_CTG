using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class MiscellaneousReportBLL
    {
        public static List<MiscellaneousReport> GetReportData(string strEmpID, string strDepartmentID, string strFromDate, string strToDate, int startIndex, int rowNumber, out int numTotalRows)
        {
            return MiscellaneousReportDAL.GetReportData(strEmpID, strDepartmentID, strFromDate, strToDate, startIndex, rowNumber, out numTotalRows);
        }
    }
}
