using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class BillType:EntityBase
    {
        [DataMember, DataColumn(true)]
        public int TYPEID { get; set; }

        [DataMember, DataColumn(true)]
        public string TYPENAME { get; set; }

        [DataMember, DataColumn(true)]
        public string DESCRIPTION { get; set; }
    }
}
