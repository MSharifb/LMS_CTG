using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class ATT_tblSetBreakTime : EntityBase
    {
                 
        [DataMember, DataColumn(true)]         	
        public Int32 intBreakSetID { get; set; }

        [DataMember, DataColumn(true)]
        public Int32 intBreakID { get; set; }

     
        [DataMember, DataColumn(true)]
        public System.DateTime dtStartTime { get; set; }

         [DataMember, DataColumn(false)]
        public System.String strStartTime
        {
            
             get
            {
                return dtStartTime.ToString("HH:mm tt");
            }
            set
            {

                if (value != null)
                {

                   dtStartTime = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    dtStartTime = DateTime.Now;
                }
            }




        }


        [DataMember, DataColumn(true)]
        public System.DateTime dtEndTime { get; set; }
        public System.String strEndTime
        {
            get
            {
                return dtEndTime.ToString("HH:mm tt");
            }
            set
            {
                dtEndTime = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }



        public System.DateTime dtpIntime { get; set; }
        public System.String strIntime
        {
            get
            {
                return dtpIntime.ToString("HH:mm tt");
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
                return dtpOuttime.ToString("HH:mm tt");
            }
            set
            {
                dtpOuttime = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }


        [DataMember, DataColumn(true)]
        public Int64 intShiftID { get; set; }

        [DataMember, DataColumn(true)]
        public String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public DateTime dtEDate { get; set; }


        [DataMember, DataColumn(true)]
        public String strBreakName { get; set; }
        [DataMember, DataColumn(true)]
        public String strShiftName { get; set; }
        


        public ATT_tblSetBreakTime() { }

    }
}
