using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class SetOverTime:EntityBase
    {        

        [DataMember, DataColumn(true)]
        public int intRowID { get; set; }

        [DataMember, DataColumn(true)]
        public string strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public string strDesignationID { get; set; }

        [DataMember, DataColumn(true)]
        public string strDepartmentID { get; set; }

        [DataMember, DataColumn(true)]
        public string strCompanyID { get; set; }

        [DataMember, DataColumn(true)]
        public string strLocationID { get; set; }

        [DataMember, DataColumn(true)]
        public int intCategoryCode { get; set; }

        [DataMember, DataColumn(true)]
        public int intCalOTAfterMinute { get; set; }

        [DataMember, DataColumn(true)]
        public bool bitConsiderEarlyArrival { get; set; }   

        [DataMember, DataColumn(true)]
        public DateTime dtPeriodFrom { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strPeriodFrom
        {
            get
            {
                if (dtPeriodFrom.ToString().Contains("1/1/0001")) return "";
                else return dtPeriodFrom.ToString("dd-MM-yyyy");
            }
            set
            {
                if(value != null)
                dtPeriodFrom = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public DateTime dtPeriodTo { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strPeriodTo
        {
            get
            {
                if (dtPeriodTo.ToString().Contains("1/1/0001")) return "";
                else return dtPeriodTo.ToString("dd-MM-yyyy");               
            }
            set
            {
                if(value != null)
                dtPeriodTo = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public int intCalMinDuration { get; set; }       

        [DataMember, DataColumn(true)]
        public decimal mnyMaxOTHour { get; set; }       

        [DataMember, DataColumn(true)]
        public decimal mnyMinOTHour { get; set; }

        [DataMember, DataColumn(true)]
        public decimal mnyOTCeilingAmount { get; set; }        

        [DataMember, DataColumn(true)]
        public bool bitFromJoiningDate { get; set; }

        [DataMember, DataColumn(true)]
        public bool bitFromConfirmationDate { get; set; }

        //-- extracolumns
        [DataMember, DataColumn(false)]
        public int EntryType { get; set; }

        [DataMember, DataColumn(true)]
        public string strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCategory { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCompany { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }

       //--------------------
        
        [DataMember, DataColumn(true)]
        public string strIUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }

        [DataMember, DataColumn(true)]
        public string strEUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtEDate { get; set; }

        public SetOverTime() { }

    }
}
