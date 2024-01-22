using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class SkillTypeName:EntityBase
    {
        [DataMember, DataColumn(true)]
        public Int32 SKILLTYPENAMEID { get; set; }

        [DataMember, DataColumn(true)]
        public string SKILLTYPENAME { get; set; }

        [DataMember, DataColumn(true)]
        public string REMARKS { get; set; }
    }
}
