using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LeaveLedger : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltOB { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltEntitlement { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltAvailed { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltEncased { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltCB { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLeaveType { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double fltApplied { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double fltSubmitted { get; set; }

        public LeaveLedger() { }
    }
}