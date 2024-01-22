using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class MISCApproval:EntityBase
    {
        [DataMember, DataColumn(true)]
        public string EMPLOYEENAME { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 INTAPPFLOWID { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 INTAPPLICATIONID { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime MISCDATE { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime DTFIRSTARRIVALDATETIME { get; set; }

        [DataMember, DataColumn(true)]
        public int INTNODEID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRAUTHORID { get; set; }

        [DataMember, DataColumn(true)]
        public int INTAUTHORTYPEID { get; set; }

        [DataMember, DataColumn(true)]
        public int INTSOURCENODEID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRSOURCEAUTHORID { get; set; }

        [DataMember, DataColumn(true)]
        public bool BITISNEWARRIVAL { get; set; }

        [DataMember, DataColumn(true)]
        public string STRDEPARTMENT { get; set; }

        [DataMember, DataColumn(true)]
        public string STRDESIGNATION { get; set; }

        [DataMember, DataColumn(true)]
        public bool BITISREVIEW { get; set; }

        [DataMember, DataColumn(true)]
        public string STRCOMPANYID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRIUSER { get; set; }

        [DataMember, DataColumn(true)]
        public string STREUSER { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime DTIDATE { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime DTEDATE { get; set; }

        [DataMember, DataColumn(true)]
        public string APPROVALSTATUS { get; set; }
    }
}
