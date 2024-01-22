using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class HolidayWeekDayRule : EntityBase
    {
        List<HolidayWeekDayRuleChild> _HolidayWeekDayRuleChild;
        public List<HolidayWeekDayRuleChild> HolidayWeekDayRuleChild
        {
            get
            {
                if (_HolidayWeekDayRuleChild == null)
                {
                    _HolidayWeekDayRuleChild = new List<HolidayWeekDayRuleChild>();
                }
                return _HolidayWeekDayRuleChild;
            }
            set { _HolidayWeekDayRuleChild = value; }
        }

        [DataMember, DataColumn(true)]
        public System.Int32 intHolidayRuleID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strHolidayRule { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }
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
        public System.String strCompany { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strYearTitle { get; set; }

        public HolidayWeekDayRule() { }
    }
}
