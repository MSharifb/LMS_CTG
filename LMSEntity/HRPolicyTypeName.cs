using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class HRPolicyTypeName:EntityBase
    {
        [DataMember, DataColumn(true)]
        public int HRPOLICYTYPENAMEID { get; set; }

        [DataMember, DataColumn(true)]
        public string HRPOLICYTYPENAME { get; set; }

        [DataMember, DataColumn(true)]
        public string REMARKS { get; set; }
    }
}
