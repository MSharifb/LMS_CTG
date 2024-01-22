using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class EmployeeCategory : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intCategoryCode { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCategory { get; set; }

        public EmployeeCategory() { }
    }

    public class EmployeeType : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intEmployeeTypeId { get; set; }
        [DataMember, DataColumn(true)]
        public System.String EmployeeTypeName { get; set; }

        public EmployeeType() { }
    }

    public class Country : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 Id { get; set; }
        [DataMember, DataColumn(true)]
        public System.String Name { get; set; }

        public Country() { }
    }


    public class JobGrade : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 Id { get; set; }
        [DataMember, DataColumn(true)]
        public System.String GradeName { get; set; }

        public JobGrade() { }
    }

}
