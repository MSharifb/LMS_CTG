using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class Department : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.String strDepartmentID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }
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
        // Added For BEPZA
        [DataMember, DataColumn(true)]
        public System.Int32 ZoneInfoId { get; set; }

        public Department() { }
    }
}
