using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace LMSEntity
{
    public class MyConveyanceDetails:EntityBase
    {
        public string STRDESIGNATION { get; set; }
        public string STRDEPARTMENT { get; set; }
        public string STREMPNAME { get; set; }

        public string STRDATE { get; set; } 

        [DataMember, DataColumn(true)]
        public string STREMPID { get; set; }
        
        [DataMember, DataColumn(true)]
        public Int64 RECORDID { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 CONVEYANCEID { get; set; }
               
        [DataMember, DataColumn(true)]
        public string FROMLOCATION { get; set; }

        [DataMember, DataColumn(true)]
        public string TOLOCATION { get; set; }

        [DataMember, DataColumn(true)]
        public string MODE { get; set; }

        [DataMember, DataColumn(true)]
        public decimal AMOUNT { get; set; }

        [DataMember, DataColumn(true)]
        public string PURPOSE { get; set; }

        [DataMember, DataColumn(true)]
        public bool ISAPPROVED { get; set; }

        [DataMember, DataColumn(true)]
        public string PERMITTEDBY { get; set; }

        [DataMember, DataColumn(true)]
        public string APPROVEDBY { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataMember, DataColumn(true)]
        public DateTime APPROVEDDATE { get; set; } 
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [DataMember, DataColumn(true)]
        public DateTime OUTOFOFFICEDATE { get; set; }

        [DataMember, DataColumn(true)]
        public string VOUCHERNUMBER { get; set; }

        [DataMember, DataColumn(true)]
        public string UNITNAME { get; set; }
                
    }
}
