using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class OOALocationWiseComments:EntityBase
    {
        [DataMember, DataColumn(true)]
        public int COMMENTID { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 OUTOFOFFICEID { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 LOCATIONID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRAUTHORID { get; set; }

        [DataMember, DataColumn(true)]
        public int INTAUTHORTYPEID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRCOMMENTS { get; set; }

        [DataMember, DataColumn(true)]
        public string STRIUSER { get; set; }

        [DataMember, DataColumn(true)]
        public string STREUSER { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime DTIDATE { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime DTEDATE { get; set; }
    }
}
