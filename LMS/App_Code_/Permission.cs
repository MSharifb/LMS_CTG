using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.UserMgtService;

namespace LMS.Web
{
    public class Permission
    {
        public static Menu objMenu;

        public enum MenuNamesId
        {
            Setup = 1,
            LeaveYear,
            OfficeTime,
            LeaveType,
            LeaveYearMapping,
            LeaveRule,
            AssignLeaveRule,
            WeekendAndHoliday,
            WeekendAndHolidayRule,
            AssignWeekendAndHolidayRule,
            ApprovalPath,
            LeaveOpening,
            LeaveEntitlement,
            LeaveEncasment,
            OnlineLeaveApplication,
            OnlineLeaveAdjustment,
            ApproveApplicationList,
            SubmitedApplicationList,
            RejectedApplicationList,
            SearchApplication,
            Administration,
            ApplyOfflineApplication,
            ApprovalAuthorityAttendence,
            SupervisorSetup,
            AlternateApprovalProcess,
            LeaveInfo,
            LeaveStatus,
            EmployeeWiseApprovalPath,
            OfflineLeaveApplication,
            OfflineLeaveAdjustment,
            SystemInitialization,
            Initialization,
            Synchronize,
            AttRule,
            Shift,
            CommonConfig,
            AttendanceStatus,
            BreakType,
            BreakTimeSetup,
            CardInfo,
            WeekendHolidayAsWorkingday,
            WeekendHolidayWorkingHour,
            CardAssign,
            ShiftAssignment,
            ManualIO,
            ImportAttendRawData,
            SetOverTime,
            Dashboard,
            LeaveYearType,
            ApprovalGroup
        }

        public enum MenuOperation
        {
            Add = 1,
            Edit,
            Delete,
            Cancel,
            Print
        }

        public enum RightNamesId
        {
            SetApprover = 1,
            SetOthersOOA = 2,
            SetOfflineMisc = 3
        }

        public static bool IsMenuOperationPermited(MenuNamesId id, MenuOperation opt)
        {
            objMenu = UserMgtAgent.GetMenuByMenuNameAndLoginId(LoginInfo.Current.LoginName, id.ToString());

            if (opt == MenuOperation.Add)
            {
                return objMenu.IsAddAssigned;
            }
            else if (opt == MenuOperation.Edit)
            {
                return objMenu.IsEditAssigned;
            }
            else if (opt == MenuOperation.Delete)
            {
                return objMenu.IsDeleteAssigned;
            }
            else if (opt == MenuOperation.Cancel)
            {
                return objMenu.IsCancelAssigned;
            }
            else if (opt == MenuOperation.Print)
            {
                return objMenu.IsPrintAssigned;
            }
            else
            {
                return false;
            }

        }

        public static bool IsRightPermited(string loginId, RightNamesId id)
        {
            Right right = UserMgtAgent.GetRightByLoginIdAndRightName(loginId.Trim(), id.ToString());
            return right.IsAssignedRight;
        }
    }
}