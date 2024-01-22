using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class CompanyUnit:EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 UNITID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 COMPANYID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String UNITNAME { get; set; }

        [DataMember, DataColumn(true)]
        public System.String REMARKS { get; set; }

    }
}
