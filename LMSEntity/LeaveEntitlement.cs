using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LeaveEntitlement : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsIndividual { get; set; }

        public LeaveEntitlement() { }
    }
}
