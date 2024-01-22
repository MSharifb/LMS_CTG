using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;
namespace LMSEntity
{
    public class ApprovalAuthor : EntityBase
    {

        [DataMember, DataColumn(true)]
        public System.Int32 intAuthorityID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intPathID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intNodeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strAuthorID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strAuthorType { get; set; }

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
        public System.String strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        public ApprovalAuthor() { }
    }

    /// <summary>
    /// Add for Central Approval System 
    /// </summary>
    public class ApproverInfo : EntityBase
    {
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }
        [DataMember, DataColumn(true)]
        public string FullName { get; set; }
        [DataMember, DataColumn(true)]
        public int InitialStepId { get; set; }

        // Added For Next Approver
        [DataMember, DataColumn(true)]
        public int RequiredActionId { get; set; }
        [DataMember, DataColumn(true)]
        public int NextStepId { get; set; }

        public string IUser { get; set; }
        public System.DateTime IDate { get; set; }
        public string EUser { get; set; }
        public Nullable<System.DateTime> EDate { get; set; }

    }

    /// <summary>
    /// Add for Save Applicaiton Information for Central Approval System.
    /// </summary>
    public class APV_ApplicationInformation 
    {
        public Int32 Id { get; set; }

        public Int32? ModuleId { get; set; }
        public Int32 ApprovalProcessId { get; set; }

        public Int32 ApplicationId { get; set; }
        public bool? IsOnlineApplication { get; set; }
        public Int32 ApprovalStepId { get; set; }
        public Int32 ApproverId { get; set; }
        public Int32? ApprovalStatusId { get; set; }
        public string ApproverComments { get; set; }

        public string IUser { get; set; }
        public System.DateTime IDate { get; set; }
        public string EUser { get; set; }
        public Nullable<System.DateTime> EDate { get; set; }
    }

}

