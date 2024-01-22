using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class rptLeaveEnjoyed : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int64 intApplicationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtApplyDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime dtApplyFromDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtApplyToDate { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strApplyFromDate
        {
            get
            {
                return dtApplyFromDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtApplyFromDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(false)]
        public System.String strApplyToDate
        {
            get
            {
                return dtApplyToDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtApplyToDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.String strApplyFromTime { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strApplyToTime { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strApplicationType { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltWithPayDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltWithoutPayDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strPurpose { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strRemarks { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strYearTitle { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLeaveType { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; } 

        
        [DataMember, DataColumn(true)]
        public System.DateTime dtJoiningDate { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strJoiningDate
        {
            get
            {
                return dtJoiningDate.ToString("dd-MMM-yyyy");
            }
            set
            {
                dtJoiningDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.DateTime dtConfirmationDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strConfirmationDate
        {
            get
            {
                return dtConfirmationDate.ToString("dd-MMM-yyyy");
            }
            set
            {
                dtConfirmationDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strGender { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strReligion { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompany { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltDurationDay { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double fltCB { get; set; }

        [DataMember, DataColumn(true)]
        public System.String ApproverName { get; set; }
        [DataMember, DataColumn(true)]
        public System.String ApproverDesignation { get; set; }
        [DataMember, DataColumn(true)]
        public System.String ApproverDepartment { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime ApproveDateTime { get; set; }

        [DataMember, DataColumn(true)]
        public System.Boolean bitIsAdjustment { get; set; }

        public rptLeaveEnjoyed() { }

    }
}
