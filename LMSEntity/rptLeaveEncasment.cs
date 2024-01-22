using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class rptLeaveEncasment : EntityBase
    {

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveEncaseID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intPaymentYear { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strPaymentMonth { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltBeforeBalance { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltEncaseDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltAfterBalance { get; set; }
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
        public System.String strYearTitle { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLeaveType { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }

        [DataMember, DataColumn(true)]
        public System.String PaymentMonth { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strGender { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strReligion { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompany { get; set; }

        public rptLeaveEncasment() { }

    }
}
