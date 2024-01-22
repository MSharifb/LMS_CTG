using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class ShiftAssignment:EntityBase
    {        

        [DataMember, DataColumn(true)]
        public int intShiftAssignmentID { get; set; }

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
        public string strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public string strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }

        [DataMember, DataColumn(true)]
        public int intShiftID { get; set; }

        [DataMember, DataColumn(true)]
        public string strDesignationID { get; set; }

        [DataMember, DataColumn(true)]
        public string strDepartmentID { get; set; }

        [DataMember, DataColumn(true)]
        public string strCompanyID { get; set; }

        [DataMember, DataColumn(true)]
        public int intReligionID { get; set; }

        [DataMember, DataColumn(true)]
        public string strLocationID { get; set; }

        [DataMember, DataColumn(true)]
        public int intCategoryCode { get; set; }


        [DataMember, DataColumn(true)]
        public string strShiftName { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtJoiningDate { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strJoiningDate
        {
            get
            {
                if (dtJoiningDate.ToString().Contains("1/1/0001")) return "";
                else
                    return dtJoiningDate.ToString("dd-MM-yyyy");
            }
            set
            {
               //dtJoiningDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }      

        [DataMember, DataColumn(true)]
        public string strIUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }

        [DataMember, DataColumn(true)]
        public string strEUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtEDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCompany { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCategory { get; set; }

        public ShiftAssignment() { }

    }
}
