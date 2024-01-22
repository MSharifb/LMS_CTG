using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;
namespace LMSEntity
{
    public class ApplicationStatusCaption : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intAppStatusID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strStatus { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDirectionType { get; set; }
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

        public ApplicationStatusCaption() { }
    }
}
