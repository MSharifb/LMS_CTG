using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class BreakType:EntityBase
    {
       

        [DataMember, DataColumn(true)]
        public int intBreakID { get; set; }

        [DataMember, DataColumn(true)]
        public String strBreakName { get; set; }         

        [DataMember, DataColumn(true)]
        public String strDescription { get; set; }                

        [DataMember, DataColumn(true)]
        public string strIUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }

        [DataMember, DataColumn(true)]
        public string strEUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtEDate { get; set; }

        public BreakType() { }

    }
}
