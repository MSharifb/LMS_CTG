using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class CardAssign:EntityBase
    {        

        [DataMember, DataColumn(true)]
        public int intCardAssignID { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtEffectiveDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strEffectiveDate
        {
            get
            {
                return dtEffectiveDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtEffectiveDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }      

        [DataMember, DataColumn(true)]
        public string strAssignID { get; set; }

        [DataMember, DataColumn(true)]
        public string strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public string strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }

        [DataMember, DataColumn(true)]
        public int intCardID { get; set; }

        [DataMember, DataColumn(true)]
        public string strCardID { get; set; }
        
        [DataMember, DataColumn(true)]
        public string strIUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }

        [DataMember, DataColumn(true)]
        public string strEUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtEDate { get; set; }

        [DataMember, DataColumn(true)]
        public bool btConsiderEarlyArrive { get; set; }

        public CardAssign() { }

    }
}
