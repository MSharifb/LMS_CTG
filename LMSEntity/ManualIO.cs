using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class ManualIO : EntityBase
    {

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }


        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intRowID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intCardID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCardID { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime dtAttendDateTime {

            get ;                        
            set ;
        }


        [DataMember, DataColumn(false)]
        public System.DateTime dtAttendDate { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strAttendDate
        {
            get
            {
                return dtAttendDate.ToString("dd-MM-yyyy");
            }
            set
            {
                dtAttendDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }



        [DataMember, DataColumn(true)]
        public System.Int32 intPort { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDeviceID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEntryType { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 RandomValue { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intShiftID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strReason { get; set; }

        [DataMember, DataColumn(true)]
        public String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public DateTime dtEDate { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsSingleEmp { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsShift { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strInTime { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strOutTime { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strHalfTime { get; set; }

        [DataMember, DataColumn(false)]
        public System.DateTime dtAttenTime { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strAttenTime
        {

            get
            {
                return dtAttenTime.ToString("HH:mm tt");
            }
            set
            {

                if (value != null)
                {

                    dtAttenTime = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    dtAttenTime = DateTime.Now;
                }
            }




        }

        public ManualIO() { }

    }
}
