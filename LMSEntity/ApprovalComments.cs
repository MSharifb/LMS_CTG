using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class ApprovalComments : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intAppFlowID { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtOperationDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intAppStatusID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strComments { get; set; }

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

        public ApprovalComments() { }
    }
}