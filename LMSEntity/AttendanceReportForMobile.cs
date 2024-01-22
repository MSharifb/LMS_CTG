using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
   public class AttendanceReportForMobile : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

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
        public DateTime dtFirstInTime { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime dtLastOutTime { get; set; }

        [DataMember, DataColumn(true)]
        public System.String InReason { get; set; }

        [DataMember, DataColumn(true)]
        public System.String OutReason { get; set; }

        [DataMember, DataColumn(true)]
        public System.String InLocation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String OutLocation { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double InLongitude { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double OutLongitude { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double InLatitude { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double OutLatitude { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strAttendDate { get; set; }
        //{
        //    get
        //    {
        //        //return dtFirstInTime != null ? dtFirstInTime.ToString("dd-MMM-yyyy") : "";
        //        return this.strAttendDate;
        //    }
        //    set
        //    {
        //        this.strAttendDate = value;
        //    }



        //}

        [DataMember, DataColumn(false)]
        public System.String strInTime { get; set; }
        //{
        //    get
        //    {
        //        return dtFirstInTime != null ? dtFirstInTime.ToString("hh:mm tt") : "";
        //    }
        //}

        [DataMember, DataColumn(false)]
        public System.String strOutTime { get; set; }
        //{
        //    get
        //    {
        //        return dtLastOutTime != null ? dtLastOutTime.ToString("hh:mm tt") : "";
        //    }
        //}

    }
}
