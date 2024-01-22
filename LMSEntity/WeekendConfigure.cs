using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LMSEntity
{
    public class WeekendConfigure
    {
        [DataMember, DataColumn(true)]
        public string WeekDay { get; set; }

        [DataMember, DataColumn(true)]
        public bool IsWeekend { get; set; }

        [DataMember, DataColumn(true)]
        public int intLeaveYearID { get; set; }

        [DataMember, DataColumn(true)]
        public int intDurationID { get; set; }

        [DataMember, DataColumn(true)]
        public bool IsAlternate { get; set; }

        [DataMember, DataColumn(true)]
        public bool IsWeekend_FirstDayOfYear { get; set; }
    }
}
