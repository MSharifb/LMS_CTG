using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;
namespace LMSEntity
{

    public class AuthorType : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intAuthorTypeID { get; set; }
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

        public AuthorType() { }
    }
}

