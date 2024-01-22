using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;
namespace LMSEntity
{
    public class ApprovalPathDetails : EntityBase
    {

        [DataMember, DataColumn(true)]
        public System.Int32 intNodeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intPathID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strNodeName { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intAuthorTypeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intParentNodeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strRootPath { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strAuthorType
        { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strParentNode
        {
            get;
            set;
        }

        public ApprovalPathDetails() { }

    }
}

