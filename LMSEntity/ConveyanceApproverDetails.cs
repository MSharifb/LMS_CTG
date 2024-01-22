using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace LMSEntity
{
    public class ConveyanceApproverDetails:EntityBase
    {
        [DataMember, DataColumn(true)]
        public string STREMPNAME { get; set; }

        [DataMember, DataColumn(true)]
        public string STRDESIGNATION { get; set; }

        [DataMember, DataColumn(true)]
        public string STRAUTHORTYPE { get; set; }

        [DataMember, DataColumn(true)]
        public int INTAUTHORTYPEID { get; set; } 

    }
}
