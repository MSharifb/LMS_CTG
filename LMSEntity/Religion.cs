using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class Religion : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.String strReligionID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strReligion { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIUser{get ; set ;}
        [DataMember, DataColumn(true)]
        public System.String strEUser{get ; set ;}
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate{get ; set ;}
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate{get ; set ;}

        [DataMember, DataColumn(true)]
        public System.String strCompany { get; set; }

        public Religion() { }
    }
}
