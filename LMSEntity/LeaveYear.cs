using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{

    public class LeaveYear : EntityBase, IComparable<LeaveYear>
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearTypeId { get; set; }

        public string startDateDay { get; set; }
        public string startDateMonth { get; set; }
        public string startDateYear { get; set; }

  
        [DataMember, DataColumn(true)]
        public System.String strYearTitle { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtStartDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strStartDate
        {
            get
            {
                return dtStartDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtStartDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEndDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strEndDate
        {
            get
            {
                return dtEndDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtEndDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsActiveYear { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }

        public LeaveYear() { }

        public int CompareTo(LeaveYear other)
        {
            return this.dtStartDate.CompareTo(other.dtStartDate);
        }

        [DataMember, DataColumn(true)]
        public string LeaveYearTypeName { get; set; }
    
    }
}

