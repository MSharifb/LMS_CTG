using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class Shift : EntityBase
    {
        public Shift() { }

        [DataMember, DataColumn(true)]
        public System.Int32 intShiftID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strShiftName { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDescription { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsRoaster { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtpPeriodFrom { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strPeriodFrom
        {
            get
            {
                return dtpPeriodFrom.ToString("dd-MM-yyyy");
            }
            set
            {
                dtpPeriodFrom = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }
        [DataMember, DataColumn(true)]
        public System.DateTime dtpPeriodTo { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strPeriodTo
        {
            get
            {
                return dtpPeriodTo.ToString("dd-MM-yyyy");
            }
            set
            {
                dtpPeriodTo = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }
        [DataMember, DataColumn(true)]
        public System.DateTime dtpIntime { get; set; }
        public System.String strIntime
        {
            get
            {
                return dtpIntime.ToString("hh:mm tt");
            }
            set
            {
                dtpIntime = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }
        [DataMember, DataColumn(true)]
        public System.DateTime dtpOuttime { get; set; }
        public System.String strOuttime
        {
            get
            {
                return dtpOuttime.ToString("hh:mm tt");
            }
            set
            {
                dtpOuttime = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }
        [DataMember, DataColumn(true)]        
        public System.Int32 intGraceInMin { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intGraceOutTimeMin { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intAbsentMin { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtpHalfTime { get; set; }
        public System.String strHalfTime
        {
            get
            {
                return dtpHalfTime.ToString("hh:mm tt");
            }
            set
            {
                dtpHalfTime = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEffectiveDate { get; set; }
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
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }           
    }
}