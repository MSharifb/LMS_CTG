using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LeaveRule : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intRuleID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strRuleName { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltEntitlement { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intEligibleAfterMonth { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intCalculateAfterMonth { get; set; }        
        [DataMember, DataColumn(true)]
        public System.String strCalculationFrom { get; set; }       
        [DataMember, DataColumn(true)]
        public System.String strEligibleAfter { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsEncashable { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intMaxEncahDays { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intMinDaysInHand { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsCarryForward { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intMaxCarryForwardDays { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLeaveObsoluteMonth { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsIncludeHoliday { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsIncludeWeekend { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsIncludeWHForWOP { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intMaxLeaveDaysInApplication { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intMaxLeaveAppInMonth { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intMaxLeaveDaysInMonth { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLeaveType { get; set; }

        //-----------------------------------------------------
        [DataMember, DataColumn(true)]
        public System.Int32 intAdjustLeaveTypeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Boolean bitIsEnjoyAtaTime { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strAllowType { get; set; }

        [DataMember, DataColumn(true)]
        public System.String hfstrAllowType { get; set; }
        /// <summary>
        /// Added For MPA
        /// </summary>
        [DataMember, DataColumn(true)]
        public Int32 intMaxValidReasonDays { get; set; }
        [DataMember, DataColumn(true)]
        public Int32 intDeductionLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public string strDeductionAllowType { get; set; }
        [DataMember, DataColumn(true)]
        public Int32 intMaxDeductionDays { get; set; }
        [DataMember, DataColumn(true)]
        public Int32 intEarnLeaveUnitForDays { get; set; }
        // END MPA
        /// <summary>
        /// Added For BEPZA
        /// </summary>
        [DataMember, DataColumn(true)]
        public Int32 intNextEligibleAfterMonth { get; set; }
        [DataMember, DataColumn(true)]
        public string strNextEligibleFrom { get; set; }

        // END BEPZA

        public LeaveRule() { }
    }
}