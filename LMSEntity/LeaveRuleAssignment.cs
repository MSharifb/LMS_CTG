using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LeaveRuleAssignment : EntityBase
    {

        [DataMember, DataColumn(true)]
        public System.Int32 intRuleAssignID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intRuleID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strApplyType { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }
        
        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDepartmentID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strGender { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLocationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intCategoryCode { get; set; }
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

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strRuleName { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double fltEntitlement { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpCategory { get; set; }

        [DataMember, DataColumn(true)]
        public Int32 intEmployeeTypeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmployeeTypeName { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsIndividual
        {
            get
            {
                if (this.strApplyType == "Individual")
                    return true;
                else
                    return false;
            }
            set
            {

                if (value == true)
                    this.strApplyType = "Individual";
                else
                    this.strApplyType = "All";
            }
        }

        //Added For BEPZA
        [DataMember, DataColumn(true)]
        public System.Int32 GradeFrom { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 GradeTo { get; set; }
        // End

        public LeaveRuleAssignment() { }

    }
}
