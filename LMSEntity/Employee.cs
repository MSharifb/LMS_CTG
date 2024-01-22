using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class Employee : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime dtJoiningDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strJoiningDate
        {
            get
            {
                return dtJoiningDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtJoiningDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.DateTime dtConfirmationDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strConfirmationDate
        {
            get
            {
                return dtConfirmationDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtConfirmationDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.DateTime dtInactiveDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strInactiveDate
        {
            get
            {
                return dtInactiveDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtInactiveDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.String strDesignationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDepartmentID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strGender { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strReligion { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLocationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEmail { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strSupervisorID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; } 

        [DataMember, DataColumn(true)]
        public System.String strAssignID { get; set; }

        [DataMember, DataColumn(false)]
        public System.String ActiveStatus { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strSearchType { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strReligionID { get; set; }

        // Added For BEPZA
        [DataMember, DataColumn(true)]
        public int ZoneId { get; set; }

        [DataMember, DataColumn(true)]
        public int EmployeeId { get; set; }


        public Employee() {
        
        strDesignationID=string.Empty;
        strDesignation=string.Empty;
        strDepartmentID=string.Empty;
        strDepartment=string.Empty;         
        strLocation=string.Empty;
        strEmpName = string.Empty;             
        }
    }
}