using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class SearchLocation:EntityBase
    {
        [DataMember, DataColumn(true)]
        public string LOCATION { get; set; }
    }
}
