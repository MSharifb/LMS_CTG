using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class MembershipTypeName:EntityBase
    {
        [DataMember, DataColumn(true)]
        public int MEMBERSHIPTYPENAMEID { get; set; }

        [DataMember, DataColumn(true)]
        public string MEMBERSHIPTYPENAME { get; set; }

        [DataMember, DataColumn(true)]
        public string REMARKS { get; set; }
    }
}
