using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class ATT_tblAtdStatusSetup : EntityBase
    {

        [DataMember, DataColumn(true)] 
        public Int64 intRowID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strPresent { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEarlyArrival { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEarlyDeparture { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLateArrival { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLateDeparture { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strAbsent { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strOSD { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLeave { get; set; }
        [DataMember, DataColumn(true)]

        public System.String strWeekend { get; set; }
        [DataMember, DataColumn(true)]

        public System.String strHoliday { get; set; }
        [DataMember, DataColumn(true)]


        public DateTime dtEffectiveDate { get; set; }
        [DataMember, DataColumn(true)]
        public String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public DateTime dtEDate { get; set; }



        public ATT_tblAtdStatusSetup() { }

    }
}
