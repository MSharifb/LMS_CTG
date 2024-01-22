using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LMSEntity;
using System.Web.Mvc;
using System.Collections;

namespace LMS.Web.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISoapService" in both code and config file together.
    [ServiceContract]
    public interface ISoapService
    {
        [OperationContract]
        string DoLogin(string userName, string password);

        [OperationContract]
        IList<MLeave> GetAppliedApplicationList(string empId, int leaveYearID);

        [OperationContract]
        MLeave GetApplicationDetail(int intAppFlowID);

        [OperationContract]
        string SaveApprovalFlow(int intAppFlowID, int intAppStatusID, string LoginId, string EmpId, string AuthorComments);

        [OperationContract]
        List<LMS.Web.WebService.SoapService.LeaveTypeCls> GetOnLineLeaveType(string strEmpID, int intLeaveYearID);

        [OperationContract]
        LMS.Web.WebService.SoapService.CalcutateDurationCls CalcutateDuration(
            string strApplyFromDate, string strApplyToDate, string strApplyFromTime, string strApplyToTime,
            string strApplicationType, string strEmpID,
            int intLeaveYearID, int intLeaveTypeID, string strOfficeTime);

        [OperationContract]
         string OnlineSubmit(
            string LoginName,
            string strEmpID,
            int intLeaveTypeID,
            string strApplyDate,
            string strApplyFromDate,
            string strApplyToDate,
            string strApplyFromTime,
            string strApplyToTime,
            string strApplicationType,
            string strDuration,
            string strWithPayDuration,
            string strtWithoutPayDuration,
            string strPurpose,
            string strContactAddress,
            string strRemarks,
            string strSupervisorID
            );

        [OperationContract]
        List<LMS.Web.WebService.SoapService.ApproverCls> GetApproverList(string strEmpID);

        [OperationContract]
        IList<rptLeaveEnjoyed> GetAvailedLeave(int intLeaveYearId, string strApplyFromDate, string strApplyToDate);

        [OperationContract]
        IList<AttendanceReport> GetAttendanceReport(string empId, string strFromDate, string strToDate);

        [OperationContract]
        IList<Employee> GetEmployeeData();

        [OperationContract]
        string SaveMobileAttendance(string strUser, string strEmpID, string strAttDateTime, string strReason, string strLocation,
            string strLongitude, string strLatitude,string deviceID);

        [OperationContract]
        IList<AttendanceReportForMobile> GetMobileAttendanceReport(string empId, string strFromDate, string strToDate);
    }
}
