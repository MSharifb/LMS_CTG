using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace LMSEntity
{
    public class ConveyanceMaster:EntityBase
    {
      
        [DataMember, DataColumn(true)]
        public Int64 RECORDID { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 OUTOFOFFICEID { get; set; }

        [DataMember, DataColumn(true)]
        public string STREMPID { get; set; }

        [DataMember, DataColumn(true)]
        public string EMPNAME { get; set; }

        [DataMember, DataColumn(true)]
        public string OUTOFOFFICETIME { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataMember, DataColumn(true)]
        public DateTime OUTOFOFFICEDATE { get; set; }

        [DataMember, DataColumn(true)]
        public bool ISAPPROVED { get; set; }

        [DataMember, DataColumn(true)]
        public string APPROVEDBY { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataMember, DataColumn(true)]
        public DateTime APPROVEDDATE { get; set; }
    }
}
