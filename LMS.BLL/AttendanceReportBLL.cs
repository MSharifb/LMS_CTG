using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class AttendanceReportBLL
    {
        public List<AttendanceReport> AttendanceReportGetData( string strReportType, string strEmpID, string strCompanyID, string strDepartmentID, string strDesignationID,
                                                                        string strLocationID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo,
                                                                        string strSortBy, string strSortType,
                                                                        int startRowIndex, int maximumRows, out int numTotalRows)
        {
            return AttendaceReportDAL.GetAttendanceReport( strReportType, strEmpID,strCompanyID,strDepartmentID,strDesignationID,strLocationID,intCategoryCode, dtPeriodFrom,dtPeriodTo,
                                                            strSortBy, strSortType, startRowIndex, maximumRows, out numTotalRows);
        }



        public List<AttendanceReportForMobile> GetMobileAttendanceReport(string strEmpID, string dtPeriodFrom, string dtPeriodTo, out int numTotalRows)
        {
            return AttendaceReportDAL.GetMobileAttendanceReport(strEmpID, dtPeriodFrom, dtPeriodTo, out numTotalRows);
        }
    }
}
