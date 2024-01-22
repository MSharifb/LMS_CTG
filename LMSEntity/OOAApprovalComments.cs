using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class OOAApprovalComments:EntityBase
    {

        [DataMember, DataColumn(true)]
        public Int64 RECORDID { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 INTOUTOFOFFICEID { get; set; }

        [DataMember, DataColumn(true)]
        public int INTFLOWPATHID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRAPPROVERID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRAPPROVERNAME { get; set; }

        [DataMember, DataColumn(true)]
        public int INTAPPROVERTYPEID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRCOMMENTS { get; set; }

        [DataMember, DataColumn(true)]
        public string STRCOMPANYID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRIUSERID { get; set; }

        [DataMember, DataColumn(true)]
        public string STREUSERID { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime DTIDATE { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime DTEDATE { get; set; }


    }
}
