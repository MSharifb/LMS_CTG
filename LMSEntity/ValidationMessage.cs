using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class ValidationMessage : EntityBase
    {
        [DataMember, DataColumn(true)]
        public Int32 code { set; get; }
        [DataMember, DataColumn(true)]
        public string msg { set; get; }
    }
}
