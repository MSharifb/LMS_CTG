using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Util
{
    public class Messages
    {
        public static string GetSuccessMessage(string msg)
        {
            string htmMsg = "<label id='lblMsg' style='font-size:10pt;font-weight:bold;color:Green;'>" + msg + "</label>";
            return htmMsg;
        }
        public static string GetSuccessMessage(string msg, string labelID)
        {
            string htmMsg = "<label id='" + labelID + "' style='font-size:10pt;font-weight:bold;color:Green;'>" + msg + "</label>";
            return htmMsg;
        }

        public static string GetErroMessage(string msg)
        {
            string htmMsg = "<label id='lblMsg' style='font-size:10pt;font-weight:bold;color:Red;'>" + msg + "</label>";
            return htmMsg;
        }

        public static string GetErroMessage(string msg, string labelID)
        {
            string htmMsg = "<label id='" + labelID + "' style='font-size:10pt;font-weight:bold;color:Red;'>" + msg + "</label>";
            return htmMsg;
        }


        public static string SavedSuccessfully
        {
            get
            {
                return "Information has been saved successfully.";
            }
        }

        public static string CouldnotSave
        {
            get
            {
                return "Information couldn't save.";
            }
        }

        public static string UpdateSuccessfully
        {
            get
            {
                return "Information has been updated successfully.";
            }
        }

        public static string CouldnotUpdate
        {
            get
            {
                return "Information couldn't update.";
            }
        }


        public static string DeleteSuccessfully
        {
            get
            {
                return "Information has been deleted successfully.";
            }
        }

        public static string CouldnotDelete
        {
            get
            {
                return "Information couldn't delete.";
            }
        }


        public static string UnableToLoad
        {
            get
            {
                return "Information is unable to load.";
            }
        }


        public static string AddSuccessfully
        {
            get
            {
                return "Information has been added successfully.";
            }
        }

        public static string UnableToAdd
        {
            get
            {
                return "Information is unable to add.";
            }
        }


        public static string RemoveSuccessfully
        {
            get
            {
                return "Information has been removed successfully.";
            }
        }

        public static string UnableToRemove
        {
            get
            {
                return "Information is unable to remove.";
            }
        }

        public static string DataExists
        {
            get
            {
                return "Information already exists.";
            }
        }

        public static string ExceptionOccurred
        {
            get
            {
                return "Exception has been occurred.";
            }
        }


        public enum ErroMessageCodeEnum
        {
            DuplicateDataUK = -2601,
            DuplicateDataPK = -2627,
            DuplicateDataCUSTOM = -50010,
            DuplicatePathAssign = -50011,
            WeekendDateOverLapping = -50012,
            ForeignKeyConstraint = -547
        }


        public static string DbErrorMessage(int messageCode)
        {
            string msg = "";

            switch (messageCode)
            {
                case -2601: //DuplicateDataUK
                    msg = "Information already exists.";
                    break;
                case -2627: //DuplicateDataPK
                    msg = "Information already exists.";
                    break;
                case -547:   //ForeignKeyConstraint
                    msg = "This record already used into another location.";
                    break;

                //--custom error code generate from database---------
                case -50010: //DuplicateDataCUSTOM
                    msg = "Information already exists.";
                    break;

                case -50011: //DuplicatePathAssign
                    msg = "Approval path already assigned.";
                    break;

                case -50012: //WeekendDateOverLapping
                    msg = "Date is overlapping.";
                    break;
            }

            return msg;
        }

    }

    public static class LeaveDurationType
    {
        public const string FullDay = "FullDay";
        public const string Hourly = "Hourly";
        public const string FullDayHalfDay = "FullDayHalfDay";
    }
    
    public static class DateTimeFormat
    {
        public const string Date = "dd-MM-yyyy";
        public const string DateTime = "dd-MM-yyyy hh:mm tt";
        public const string Time = "hh:mm tt";
        public const string DateSeparator = "-";
    }

   
   public static class PartialViewName
   {
       public const string LeaveYearType = "LeaveYearType";
       public const string LeaveYearTypeDetails = "LeaveYearTypeDetails";

       public const string LeaveYear = "LeaveYear";
       public const string LeaveYearDetails = "LeaveYearDetails";

       public const string OfficeTime = "OfficeTime";
       public const string OfficeTimeDetails = "OfficeTimeDetails";
       
       public const string SearchEmployee = "SearchEmployee";
       public const string EmployeeWiseApprovalPath = "EmployeeWiseApprovalPath";
       public const string EmployeeWiseApprovalPathDetails = "EmployeeWiseApprovalPathDetails";
       
       public const string HolidayWeekDay = "HolidayWeekDay";
       public const string HolidayWeekDayDetails = "HolidayWeekDayDetails";
       public const string HolidayWeekendRule = "HolidayWeekendRule";
       public const string HolidayWeekendRuleDetails = "HolidayWeekendRuleDetails";
       public const string HolidayWeekendRuleAssign = "HolidayWeekendRuleAssign";
       public const string HolidayWeekendRuleAssignDetails = "HolidayWeekendRuleAssignDetails";

       public const string ApprovalPathDetails = "ApprovalPathDetails";
       public const string SetApproverDetails = "SetApproverDetails";

       public const string LeaveType = "LeaveType";
       public const string LeaveTypeDetails = "LeaveTypeDetails";

       public const string LeaveYearMapping = "LeaveYearMapping";
       public const string LeaveYearMappingDetails = "LeaveYearMappingDetails";

       public const string LeaveRule = "LeaveRule";
       public const string LeaveRuleDetails = "LeaveRuleDetails";

       public const string LeaveLedger = "LeaveLedger";
       public const string LeaveRuleAssignment = "LeaveRuleAssignment";
       public const string LeaveRuleAssignmentDetails = "LeaveRuleAssignmentDetails";
       
       public const string LeaveApplication = "LeaveApplication";
       public const string LeaveApplicationAdd = "LeaveApplicationAdd";
       public const string LeaveApplicationDetails = "LeaveApplicationDetails";
       public const string LeaveApplicationDetailsPreview = "LeaveApplicationDetailsPreview";

       public const string OfflineLeaveApplication = "OfflineLeaveApplication";
       public const string OfflineLeaveApplicationDetails = "OfflineLeaveApplicationDetails";
       public const string OfflineLeaveApplicationDetailsPreview = "OfflineLeaveApplicationDetailsPreview";

       public const string OfflineLeaveAdjustment = "OfflineLeaveAdjustment";
       public const string OfflineLeaveAdjustmentAdd = "OfflineLeaveAdjustmentAdd";
       public const string OfflineLeaveAdjustmentDetails = "OfflineLeaveAdjustmentDetails";
       public const string OfflineLeaveAdjustmentDetailsPreview = "OfflineLeaveAdjustmentDetailsPreview";

       public const string LeaveEncasment = "LeaveEncasment";
       public const string LeaveEncasmentDetails = "LeaveEncasmentDetails";

       public const string LeaveEntitlementDetails = "LeaveEntitlementDetails";
       public const string LeaveOpeningDetails = "LeaveOpeningDetails";
       public const string CommonConfigDetails = "CommonConfigDetails";

       public const string ApprovalFlowDetails = "ApprovalFlowDetails";
       public const string ApprovalFlowDetailsPreview = "ApprovalFlowDetailsPreview";

       public const string AlternateApprovalProcess = "AlternateApprovalProcess";
       public const string AlternateApprovalProcessDetails = "AlternateApprovalProcessDetails";
       
       public const string LeaveAdjustment = "LeaveAdjustment"; 
       public const string LeaveAdjustmentAdd = "LeaveAdjustmentAdd";
       public const string LeaveAdjustmentDetails = "LeaveAdjustmentDetails";
       public const string LeaveAdjustmentDetailsPreview = "LeaveAdjustmentDetailsPreview";

       public const string SearchApplication = "SearchApplication";
       public const string Initialization = "Initialization";
       public const string AlreadyInitialized = "AlreadyInitialized";
       public const string Synchronize = "Synchronize";
       public const string Error = "Error";

       public const string Reports = "Reports";
       public const string ReportView = "ReportView";
       public const string rptAbsentEmployeeList = "rptAbsentEmployeeList";
       public const string rptEarlyArrivalEmployeeList = "rptEarlyArrivalEmployeeList";
       public const string rptEarlyDepartureEmployeeList = "rptEarlyDepartureEmployeeList";
       public const string rptEmployeeAttendanceStatus = "rptEmployeeAttendanceStatus";
       public const string rptEmployeeJobCard = "rptEmployeeJobCard";
       public const string rptEmployeeOnLeaveList = "rptEmployeeOnLeaveList";
       public const string rptEmployeeStatusSummary = "rptEmployeeStatusSummary";
       public const string rptEmployeeWorkingCalendar = "rptEmployeeWorkingCalendar";
       public const string rptLateArrivalEmployeeList = "rptLateArrivalEmployeeList";
       public const string rptLateDepartureEmployeeList = "rptLateDepartureEmployeeList";
       public const string rptOSDEmployeeList = "rptOSDEmployeeList";
       public const string rptPresentEmployeeList = "rptPresentEmployeeList";
       public const string rptOutOfOfficeCompare = "rptOutOfOfficeCompare"; 

       public const string MyLeaveStatus = "MyLeaveStatus";
       public const string RdlcMyLeaveStatus = "RdlcMyLeaveStatus"; 

       public const string rptLeaveStatus = "rptLeaveStatus";
       public const string rptLeaveEnjoyed = "rptLeaveEnjoyed";
       public const string rptLeaveEncasment = "rptLeaveEncasment";

       public const string EmployeeWiseOOAApprovalPathDetails = "EmployeeWiseOOAApprovalPathDetails";
       public const string EmployeeWiseOOAApprovalPath = "EmployeeWiseOOAApprovalPath";
       public const string OOAApprovalPathDetails = "OOAApprovalPathDetails";

       public const string AttRule = "AttRule";
       public const string AttRuleDetails = "AttRuleDetails";

       public const string Shift = "Shift";
       public const string ShiftDetails = "ShiftDetails";

       public const string ShiftAssignment = "ShiftAssignment";
       public const string ShiftAssignmentDetails = "ShiftAssignmentDetails";

       public const string BreakType = "BreakType";
       public const string BreakTypeDetails = "BreakTypeDetails";
       
       public const string BreakTimeSetup = "BreakTimeSetup";
       public const string BreakTimeSetupDetails = "BreakTimeSetupDetails";

       public const string CardInfo = "CardInfo";
       public const string CardInfoDetails = "CardInfoDetails";
       public const string CardInfoSearch = "CardInfoSearch";

       public const string CardAssign = "CardAssign";
       public const string CardAssignDetails = "CardAssignDetails";

       public const string WeekendHolidayAsWorkingday = "WeekendHolidayAsWorkingday";
       public const string WeekendHolidayAsWorkingdayDetails = "WeekendHolidayAsWorkingdayDetails";

       public const string WeekendHolidayWorkingHour = "WeekendHolidayWorkingHour";
       public const string WeekendHolidayWorkingHourDetails = "WeekendHolidayWorkingHourDetails";

       public const string ManualIO = "ManualIO";
       public const string ManualIODetails = "ManualIODetails";

       public const string SetOverTime = "SetOverTime";
       public const string SetOverTimeDetails = "SetOverTimeDetails";

       public const string Dashboard = "Dashboard";



   }
   
   public static class DataShortBy
   {      
       public const string ASC = "ASC";
       public const string DESC = "DESC";
   }

   public static class ReportId
   {
       public const string LeaveAvailed="Leave Availed";
       public const string LeaveEncashment = "Leave Encashment";
       public const string LeaveStatus = "Leave Status";
       public const string LeaveRegister = "Leave Register";
       public const string RecreationLeave = "Recreation Leave";
       public const string OfficeOrderForRecreationLeave = "Office Order For Recreation Leave";
       public const string YearlyLeaveStatus= "Yearly Leave Status";
   }

   public static class CommonConfigKey
   {
       public const string HRMDB = "HRM Database";
       public const string AllowAutoRecommendation = "Allow Auto Recommendation";
       public const string RecommendationLeadTime = "Recommendation Lead Time Hour";
       public const string AllowAutoApproval = "Allow Auto Approval";
       public const string ApprovalLeadTime = "Approval Lead Time Hour";
       public const string EmailNotificationToHR = "E-mail Notification to HR";
       public const string AllowHourlyLeave = "Allow Hourly Leave";
       public const string NumberOfHourlyLeaveInaDay = "Number of Hourly Leave in a Day";
       public const string WebApplicationUrl = "Web Application Url";
       public const string ApprovalFlowUrl = "Approval Flow Url";
       public const string OOAApprovalFlowUrl = "Approval Flow URL for OOA";
       public const string AllowApprovalEmailLink = "Allow Approval E-mail Link";
       public const string SMTPServer = "SMTP Server";
       public const string SMTPUserName = "SMTP User Name";
       public const string SMTPUserPassword = "SMTP User Password";
       public const string SMTPPort = "SMTP Port";
       public const string EmailNotificationToAdminFinalApproval = "E-mail Notification to Admin for final approval";
   }
}
