using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class HolidayWeekDay : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intHolidayWeekendID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intHolidayWeekendMasterID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime dtDateFrom { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strDateFrom
        {
            get
            {
                return dtDateFrom.ToString("dd-MM-yyyy");
            }
            set
            {
                dtDateFrom = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.DateTime dtDateTo { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strDateTo
        {
            get
            {
                return dtDateTo.ToString("dd-MM-yyyy");
            }
            set
            {
                dtDateTo = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }

        [DataMember, DataColumn(true)]
        public System.Int32 intDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strType { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strHolidayTitle { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean isAutomatic { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsHoliday
        {
            get
            {
                if (this.strType == "Holiday")
                    return true;
                else
                    return false;
            }
            set
            {

                if (value == true)
                    strType = "Holiday";
                else
                    strType = "Weekend";
            }
        }

        [DataMember, DataColumn(true)]
        public System.String strYearTitle { get; set; }

        public HolidayWeekDay() { }
    }
}
