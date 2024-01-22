using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class HolidayWeekDayDetails:EntityBase
    {
        [DataMember,DataColumn(true)]
        public int intHolidayWeekendDetailsID{get;set;}

        [DataMember,DataColumn(true)]
        public int intHolidayWeekendMasterID{get;set;}

        [DataMember,DataColumn(true)]
        public DateTime dtDateFrom{get;set;}

        [DataMember,DataColumn(true)]
        public DateTime dtDateTo{get;set;}

        [DataMember,DataColumn(true)]
        public string strDay{get;set;}

        [DataMember,DataColumn(true)]
        public int intDuration{get;set;}

        [DataMember,DataColumn(true)]
        public bool isAlternateDay{get;set;}

        [DataMember, DataColumn(true)]
        public bool isFromFirstWeekend { get; set; }

    }
}
