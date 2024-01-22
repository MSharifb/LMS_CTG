using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class Zone : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.String strZoneID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strZone { get; set; }
    }
}
