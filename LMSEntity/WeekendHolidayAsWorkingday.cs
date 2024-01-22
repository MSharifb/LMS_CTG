using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class WeekendHolidayAsWorkingday : EntityBase
    {
        public WeekendHolidayAsWorkingday() { }

        [DataMember, DataColumn(true)]
        public System.Int32 intWeekendWorkingday { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDescription { get; set; }       
        [DataMember, DataColumn(true)]
        public System.DateTime dtEffectiveDateFrom { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strEffectiveDateFrom
        {
            get
            {
                return dtEffectiveDateFrom.ToString("dd-MM-yyyy");
            }
            set
            {
                dtEffectiveDateFrom = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEffectiveDateTo { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strEffectiveDateTo
        {
            get
            {
                return dtEffectiveDateTo.ToString("dd-MM-yyyy");
            }
            set
            {
                dtEffectiveDateTo = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }
        [DataMember, DataColumn(true)]        
        public System.DateTime dtDeclarationDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strDeclarationDate
        {
            get
            {
                return dtDeclarationDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtDeclarationDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
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