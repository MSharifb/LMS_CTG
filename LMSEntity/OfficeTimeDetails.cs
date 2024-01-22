using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class OfficeTimeDetails : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intDurationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDurationName { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strStartTime { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEndTime { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltDuration { get; set; }


        public OfficeTimeDetails() { }
    }
}
