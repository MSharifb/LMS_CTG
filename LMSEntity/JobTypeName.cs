using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class JobTypeName:EntityBase
    {
        [DataMember, DataColumn(true)]
        public int JOBTYPENAMEID { get; set; }

        [DataMember, DataColumn(true)]
        public string JOBTYPENAME { get; set; }

        [DataMember, DataColumn(true)]
        public string REMARKS { get; set; }
    }
}
