using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class rptLeaveStatus : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double fltOB { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltEntitlement { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltApplied { get; set; }        
        [DataMember, DataColumn(true)]
        public System.Double fltAvailed { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltAvailedWOP { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltEncased { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltCB { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strYearTitle { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLeaveType { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtJoiningDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strJoiningDate
        {
            get
            {
                return dtJoiningDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtJoiningDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
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

        public rptLeaveStatus() { }

    }
}
