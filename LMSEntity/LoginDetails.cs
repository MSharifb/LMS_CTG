using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LoginDetails : EntityBase
    {
        [DataMember, DataColumn(true)]
        public string strEmpID { set; get; }

        [DataMember, DataColumn(true)]
        public string strEmpInitial { get; set; } 

        [DataMember, DataColumn(true)]
        public string strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtJoiningDate { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtConfirmationDate { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtInactiveDate { get; set; }

        [DataMember, DataColumn(true)]
        public string strDesignationID { get; set; }

        [DataMember, DataColumn(true)]
        public string strDepartmentID { get; set; }

        [DataMember, DataColumn(true)]
        public string strCompanyID { get; set; }

        [DataMember, DataColumn(true)]
        public string strGender { get; set; }

        [DataMember, DataColumn(true)]
        public string strReligionID { get; set; }

        [DataMember, DataColumn(true)]
        public string strLocationID { get; set; }

        [DataMember, DataColumn(true)]
        public string strEmail { get; set; }

        [DataMember, DataColumn(true)]
        public string strSupervisorID { get; set; }

        [DataMember, DataColumn(true)]
        public string strIUser { get; set; }

        [DataMember, DataColumn(true)]
        public string strEUser { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtEDate { set; get; }

        [DataMember, DataColumn(true)]
        public string strDesignation { set; get; }

        [DataMember, DataColumn(true)]
        public string strDepartment { set; get; }

        [DataMember, DataColumn(true)]
        public int intNodeID { set; get; }

        [DataMember, DataColumn(true)]
        public int intLeaveYearID { set; get; }

        [DataMember, DataColumn(true)]
        public System.Double fltDuration { set; get; }

        [DataMember, DataColumn(true)]
        public string strAllowHourlyLeave { set; get; }

        [DataMember, DataColumn(true)]
        public DateTime StartOfficeTime { set; get; }

        [DataMember, DataColumn(true)]
        public DateTime EndOfficeTime { set; get; }
        
        [DataMember, DataColumn(false)]
        public int intCountLeaveYear { set; get; }

        [DataMember, DataColumn(true)]
        public string LoggedInZoneName { set; get; }
        
    }
}
