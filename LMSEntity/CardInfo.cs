using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class CardInfo:EntityBase
    {
        [DataMember, DataColumn(true)]
        public int intCardID { get; set; }

        [DataMember, DataColumn(true)]
        public string strCardID { get; set; }

        [DataMember, DataColumn(true)]
        public int intStatus { get; set; }

        [DataMember, DataColumn(true)]
        public string strIUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }

        [DataMember, DataColumn(true)]
        public string strEUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtEDate { get; set; }

        public CardInfo() { }
    }
}
