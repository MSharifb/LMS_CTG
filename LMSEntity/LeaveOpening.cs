using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LeaveOpening : EntityBase
    {
        private Employee _Employee;
        public Employee Employee
        {
            get
            {
                if (_Employee == null)
                {
                    _Employee = new Employee();
                }
                return _Employee;
            }
            set { _Employee = value; }
        }


        private List<LeaveOpening> _LstLeaveOpening;
        public List<LeaveOpening> LstLeaveOpening
        {
            get
            {
                if (_LstLeaveOpening == null)
                {
                    _LstLeaveOpening = new List<LeaveOpening>();
                }
                return _LstLeaveOpening;
            }
            set { _LstLeaveOpening = value; }
        }


        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime dtBalanceDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strBalanceDate
        {
            get
            {
                return dtBalanceDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtBalanceDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.Double fltOB { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltAvailed { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltAvailedWOP { get; set; }
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
        public System.String strLeaveType { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLeaveYear { get; set; }

        [DataMember, DataColumn(true)]
        public System.Boolean isServiceLifeType { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strYearTitle { get; set; }

        public LeaveOpening() { }
    }
}