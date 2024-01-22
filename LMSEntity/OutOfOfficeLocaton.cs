using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
   public class OutOfOfficeLocaton:EntityBase
    {
        
        [DataMember, DataColumn(true)]
        public Int64 RECORDID { get; set; }
       
        [DataMember, DataColumn(true)]
        public Int64 OUTOFOFFICEID { get; set; }

        [DataMember, DataColumn(true)]
        public string FROMLOCATION { get; set; }

        [DataMember, DataColumn(true)]
        public string TOLOCATION { get; set; }

        [DataMember, DataColumn(true)]
        public string MODE { get; set; }

        [DataMember, DataColumn(true)]
        public decimal AMOUNT { get; set; }

        [DataMember, DataColumn(true)]
        public decimal PERMITTEDAMOUNT { get; set; }

        [DataMember, DataColumn(true)]
        public string PURPOSE { get; set; }

        [DataMember, DataColumn(true)]
        public bool bIsRoundTrip { get; set; }

        public List<OOALocationWiseComments> LocationWiseCommentsList { get; set; }
    }
}
