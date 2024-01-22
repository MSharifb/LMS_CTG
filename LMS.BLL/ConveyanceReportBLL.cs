using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class ConveyanceReportBLL
    {
        public static List<ConveyanceReport> GetReportData(string strEmpID, string strDepartmentID, string strFromDate, string strToDate, int startIndex, int rowNumber, out int numTotalRows)
        {
            return ConveyanceReportDAL.GetReportData(strEmpID, strDepartmentID, strFromDate, strToDate, startIndex, rowNumber, out numTotalRows);
        }
    }
}
