using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;
namespace LMSEntity
{
    public class ApprovalPathMaster : EntityBase
    {
        public ApprovalPathMaster() { }

        [DataMember, DataColumn(true)]
        public System.Int32 intPathID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strPathName { get; set; }
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
        public System.Int32 intApprovalGroupId { get; set; }

        [DataMember, DataColumn(true)]
        public System.String ApprovalGroupName { get; set; }

        #region Newly Added 2016-04-20

        [DataMember, DataColumn(true)]
        public System.Int32 intApprovalProcessId { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strApprovalProcessName { get; set; }

        #endregion
    }
}

