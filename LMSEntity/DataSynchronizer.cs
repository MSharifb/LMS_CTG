using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class DataSynchronizer : EntityBase
    {
        public bool bitCompany { set; get; }
        public bool bitDepartment { set; get; }
        public bool bitLocation { set; get; }
        public bool bitDesignation { set; get; }
        public bool bitReligion { set; get; }
        public bool bitEmployeeCategory { set; get; }
        public bool bitEmployee { set; get; }
    }
}
