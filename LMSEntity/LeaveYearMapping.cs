using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LeaveYearMapping : EntityBase
    {
       public LeaveYearMapping() 
       { 
       }

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearMapID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearId { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearTypeId { get; set; }

        [DataMember, DataColumn(true)]
        public System.Boolean bitIsActiveYear { get; set; }

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
        public string strYearTitle { get; set; }

        [DataMember, DataColumn(true)]
        public string strLeaveType { get; set; }

        [DataMember, DataColumn(true)]
        public string LeaveYearTypeName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strStartDate { get; set; }
        
        [DataMember, DataColumn(true)]
        public System.String strEndDate { get; set; }
        
        

    }
}
