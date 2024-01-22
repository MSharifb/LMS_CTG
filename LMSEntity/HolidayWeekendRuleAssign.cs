using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class HolidayWeekendRuleAssign : EntityBase
    {
        private List<HolidayWeekDayRuleChild> _HolidayWeekDayRuleList;

        public List<HolidayWeekDayRuleChild> HolidayWeekDayRuleList
        {
            get { return _HolidayWeekDayRuleList; }
            set { _HolidayWeekDayRuleList = value; }
        }

        [DataMember, DataColumn(true)]
        public System.Int32 intRuleAssignID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intHolidayRuleID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strApplyTo { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartmentID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDesignationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strReligionID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intCategoryCode { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strHolidayRule { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strYearTitle { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strReligion { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEmpCategory { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsIndividual
        {
            get
            {
                if (this.strApplyTo == "Individual")
                    return true;
                else
                    return false;
            }
            set
            {

                if (value == true)
                    this.strApplyTo = "Individual";
                else
                    this.strApplyTo = "All";
            }
        }
        [DataMember, DataColumn(true)]
        public System.String strLocationID { get; set; }

        public HolidayWeekendRuleAssign() { }
    }
}