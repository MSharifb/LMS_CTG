using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class ConveyanceReport:EntityBase
    {
        [DataMember, DataColumn(true)]
        public string STREMPNAME { get; set; }

        [DataMember, DataColumn(true)]
        public string PURPOSE { get; set; }

        [DataMember, DataColumn(true)]
        public string STRDEPARTMENT { get; set; }

        [DataMember, DataColumn(true)]
        public string STRDESIGNATION { get; set; }

        [DataMember, DataColumn(true)]
        public decimal TOTALAMOUNT { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime CONVEYANCEDATE { get; set; }
    }
}
