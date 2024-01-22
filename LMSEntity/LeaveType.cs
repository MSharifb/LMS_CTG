using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LeaveType : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLeaveType { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLeaveShortName { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intPriority { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsEarnLeave { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsEncashable { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEntitlementType { get; set; }
        [DataMember, DataColumn(false)]
        public System.Boolean IsFixed
        {
            get
            {
                if (this.strEntitlementType == "Fixed")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    this.strEntitlementType = "Fixed";
                else
                    this.strEntitlementType = "Calculated";
            }
        }

        [DataMember, DataColumn(false)]
        public System.String strIsServiceLifeType { get; set; }

         [DataMember, DataColumn(true)]
        public System.Boolean isServiceLifeType { get; set; }


        [DataMember, DataColumn(true)]
        public System.Int32 intEarnLeaveUnitForDays { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEarnLeaveCalculationType { get; set; }

        [DataMember, DataColumn(true)]
        public Int32 intEarnLeaveType { get; set; }

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
        public System.Int32 intLeaveYearTypeId { get; set; }

        [DataMember, DataColumn(true)]
        public System.String LeaveYearTypeName { get; set; } 
        
        [DataMember, DataColumn(true)]
        public System.String strYearTitle { get; set; } 

         [DataMember, DataColumn(true)]
         public System.Int32 intLeaveYearID { get; set; }

         [DataMember, DataColumn(true)]
         public System.Int32 intApprovalGroupId { get; set; }

         [DataMember, DataColumn(true)]
         public System.String ApprovalGroupName { get; set; } 
        
        // Added By Shamim For BEPZA
        [DataMember, DataColumn(true)]
        public bool bitIsRecreationLeave { get; set; }
        [DataMember, DataColumn(true)]
        public bool bitIsDeductLeaveCalculate { get; set; }

        public Int32 intLeaveTypeAddID { get; set; }

        // End

        public LeaveType() { }


        
    }

    public class LeaveTypeDeduct : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeDeductID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intDeductLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }

        //Others Fields
        [DataMember, DataColumn(true)]
        public System.String strLeaveType { get; set; }

        public LeaveTypeDeduct() { }
    }
}