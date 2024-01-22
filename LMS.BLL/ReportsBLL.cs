using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
using System.Data;

namespace LMS.BLL
{
    public class ReportsBLL
    {
        public List<rptLeaveEncasment> RptLeaveEncasmentGetData(int intleaveyearId, bool IsActiveLeaveYear, bool IsServiceLifeType, string strcompanyId, string strempId, int empStatus,
                                                                string strdepartmentId, string strdesignationId, string strgender,
                                                                string strlocationId, int intCategoryId, int intLeaveTypeID, int ZoneId,
                                                                string strSortBy, string strSortType, int startRowIndex,
                                                                int maximumRows, out int numTotalRows)
        {
            return ReportsDAL.GetLeaveEncasmentItemList(intleaveyearId, IsActiveLeaveYear, IsServiceLifeType, strcompanyId, strempId, empStatus,
                                                        strdepartmentId, strdesignationId, strgender,
                                                        strlocationId, intCategoryId, intLeaveTypeID, ZoneId,
                                                        strSortBy, strSortType, startRowIndex, maximumRows,
                                                        out numTotalRows);
        }


        public List<rptLeaveStatus> RptSubordinateLeaveStatusItemList(int intleaveyearId, string strcompanyId, string strauthorId,
                                                          string strSortBy, string strSortType,
                                                          int startRowIndex, int maximumRows, out int numTotalRows)
        {
            return ReportsDAL.GetSubordinateLeaveStatusItemList(intleaveyearId, strcompanyId, strauthorId,
                                                     strSortBy, strSortType, startRowIndex, maximumRows, out  numTotalRows);
        }



        public List<rptLeaveStatus> RptLeaveStatusGetData(int intleaveyearId, bool IsActiveLeaveYear, bool IsServiceLifeType, string strcompanyId, string strempId, int empStatus,
                                                          string strdepartmentId, string strdesignationId, string strgender,
                                                          string strlocationId, int intCategoryId, int intLeaveTypeID, int ZoneId,
                                                          string strSortBy, string strSortType, int startRowIndex,
                                                          int maximumRows, out int numTotalRows)
        {
            return ReportsDAL.GetLeaveStatusItemList(intleaveyearId, IsActiveLeaveYear, IsServiceLifeType, strcompanyId, strempId, empStatus,
                                                     strdepartmentId, strdesignationId, strgender,
                                                     strlocationId, intCategoryId, intLeaveTypeID, ZoneId,
                                                     strSortBy, strSortType, startRowIndex, maximumRows,
                                                     out  numTotalRows);
        }



        public List<rptLeaveEnjoyed> RptLeaveEnjoyedGetData(int intleaveyearId, bool IsServiceLifeType, string strcompanyId, string strempId, int empStatus,
                                                            string strdepartmentId, string strdesignationId, string strgender,
                                                            string strlocationId, string strfromDate, string strtoDate,
                                                            int intCategoryId, int intLeaveTypeID, bool bitIsWithoutPay,
                                                            bool bitIsApplyDate, int ZoneId, string strSortBy, string strSortType,
                                                            int startRowIndex, int maximumRows, out int numTotalRows)
        {
            return ReportsDAL.GetLeaveEnjoyedItemList(intleaveyearId, IsServiceLifeType, strcompanyId, strempId, empStatus,
                                                      strdepartmentId, strdesignationId, strgender,
                                                      strlocationId, strfromDate, strtoDate,
                                                      intCategoryId, intLeaveTypeID, bitIsWithoutPay,
                                                      bitIsApplyDate, ZoneId, strSortBy,
                                                      strSortType, startRowIndex, maximumRows,
                                                      out  numTotalRows);
        }

        public DataSet RptLeaveRegisterGetData(string strempId)
        {
            return ReportsDAL.GetLeaveRegister(strempId);
        }

        public DataSet RptYearlyLeaveStatusGetData(string strempId,int empStatus, string strdepartmentId, string strdesignationId, string strgender, int intCategoryId, int ZoneId)
        {
            return ReportsDAL.GetYearlyLeaveStatus(strempId,empStatus,strdepartmentId,strdesignationId,strgender,intCategoryId, ZoneId);
        }

        public DataSet RptLeaveApplicationInfo(int applicationId)
        {
            return ReportsDAL.GetLeaveApplicationInfo(applicationId);
        }
        public List<rptCompanyInformation> GetCompanyInformations()
        {
            return ReportsDAL.GetCompanyInformations();
        }

        public List<rptCompanyInformation> GetZoneInformations( int loggedZoneId)
        {
            return ReportsDAL.GetZoneInformations(loggedZoneId);
        }

        public DataSet RptRecreationLeaveGetData(int intleaveYearId, bool isActiveLeaveYear)
        {
            return ReportsDAL.GetRecreationLeave(intleaveYearId, isActiveLeaveYear);
        }

        public DataSet RptRecreationLeaveOfficeOrderGetData(int IntLeaveYearId, bool IsActiveLeaveYear, string StrFromDate, string StrToDate, bool IsApplyDate, int LoggedZoneId)
        {
            return ReportsDAL.GetRecreationLeaveOfficeOrder(IntLeaveYearId, IsActiveLeaveYear, StrFromDate, StrToDate, IsApplyDate, LoggedZoneId);
        }
    }
}
