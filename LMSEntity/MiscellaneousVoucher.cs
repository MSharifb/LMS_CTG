using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class MiscellaneousVoucher:EntityBase
    {
       
        private string sTRDEPARTMENT;
        public string STRDEPARTMENT
        {
            get { return sTRDEPARTMENT; }
            set { sTRDEPARTMENT = value; }
        }

        private string sTRDESIGNATION;
        public string STRDESIGNATION
        {
            get { return sTRDESIGNATION; }
            set { sTRDESIGNATION = value; }
        }

        [DataMember, DataColumn(true)]
        public string UNITNAME { get; set; }

        [DataMember, DataColumn(true)]
        public int UNITID { get; set; }

        [DataMember, DataColumn(true)]
        public string STREMPNAME { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 RECORDID { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 MISCID { get; set; }

        [DataMember, DataColumn(true)]
        public string VOUCHERNUMBER { get; set; }

        [DataMember, DataColumn(true)]
        public string STRAUTHORID { get; set; }

        [DataMember, DataColumn(true)]
        public string STREMPID { get; set; }

        [DataMember,DataColumn(true)]
        public DateTime MISCDATE { get; set; }
                       
        [DataMember, DataColumn(true)]
        public string ISAPPROVED { get; set; }

        [DataMember, DataColumn(true)]
        public string APPROVEDBY { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime APPROVEDDATE { get; set; } 

    }
}
