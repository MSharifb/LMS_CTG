using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class AttendanceReport : EntityBase
    {
         
        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

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
        public string strAttendDateTime
        {
            get
            {
                return dtAttendDateTime.ToString("dd-MMM-yyyy");
            }
            set
            {
                dtAttendDateTime = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
            }
        }


        [DataMember, DataColumn(true)]
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
        public System.DateTime dtEffectiveDateFrom { get; set; }

        //[DataMember, DataColumn(false)]
        //public System.String strEffectiveDateFrom
        //{
        //    get
        //    {
        //        return dtEffectiveDateFrom.ToString("dd-MM-yyyy");
        //    }
        //    set
        //    {
        //        dtEffectiveDateFrom = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
        //    }
        //}

        [DataMember, DataColumn(true)]
        public System.DateTime dtEffectiveDateTo { get; set; }

        //[DataMember, DataColumn(false)]
        //public System.String strEffectiveDateTo
        //{
        //    get
        //    {
        //        return dtEffectiveDateTo.ToString("dd-MM-yyyy");
        //    }
        //    set
        //    {
        //        dtEffectiveDateTo = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
        //    }
        //}



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

        #region extra columns
        [DataMember, DataColumn(true)]
        public string strAttendanceStatus { get; set; }

        [DataMember, DataColumn(true)]
        public string strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCategory { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strCompany { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strShiftName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDayStatus { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtInTime { get; set; }

        //[DataMember, DataColumn(false)]
        //public string strInTime 
        //{
        //    get
        //    {
        //        if (dtInTime != null)
        //        {
        //            return dtInTime.ToString("hh:mm tt");
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //}  

        [DataMember, DataColumn(true)]
        public DateTime dtOutTime { get; set; }        

        [DataMember, DataColumn(true)]
        public DateTime dtFirstInTime { get; set; }        

        [DataMember, DataColumn(true)]
        public DateTime dtLastOutTime { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtMidIn { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtMidOut { get; set; }

        [DataMember, DataColumn(true)]
        public int intWorkedHoliday{ get; set; }

        [DataMember, DataColumn(true)]
        public int intOverTime { get; set; }    

        [DataMember, DataColumn(true)]
        public System.String intEarlyIN { get; set; }  

        [DataMember, DataColumn(true)]
        public System.String intEarlyOUT { get; set; }

        [DataMember, DataColumn(true)]
        public System.String intLateIN { get; set; }

        [DataMember, DataColumn(true)]
        public System.String intLateOUT { get; set; }

        [DataMember, DataColumn(true)]
        public System.String intAbsent { get; set; }

        [DataMember, DataColumn(true)]
        public System.String intLeave { get; set; }

        [DataMember, DataColumn(true)]
        public System.String intOSD { get; set; }

        [DataMember, DataColumn(true)]
        public System.String intWorkingDays { get; set; }
        #endregion

        public AttendanceReport() { }

    }
}
