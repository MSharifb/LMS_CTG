using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class ApprovalFlow : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 intAppFlowID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int64 intApplicationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtFirstArrivalDateTime { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intNodeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strAuthorID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strAuthorInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intSourceNodeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strSourceAuthorID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsNewArrival { get; set; }

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
        public string strAuthorName { set; get; }

        //Extra
        [DataMember, DataColumn(false)]
        public System.Int32 intDestNodeID { get; set; }
        [DataMember, DataColumn(false)]
        public System.Int32 intAppStatusID { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strDestAuthorID { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strDestAuthorInitial { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strComments { get; set; }

        /*updated by mamun on 15 feb 2011*/
        [DataMember, DataColumn(true)]
        public System.String strDesignationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDepartmentID { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strApplicationType { get; set; }
        [DataMember, DataColumn(false)]
        public System.DateTime dtApplyFromDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.DateTime dtApplyToDate { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strApplyFromTime { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strApplyToTime { get; set; }
        [DataMember, DataColumn(false)]
        public System.Double fltDuration { get; set; }
        [DataMember, DataColumn(false)]
        public System.Double fltWithPayDuration { get; set; }
        [DataMember, DataColumn(false)]
        public System.Double fltWithoutPayDuration { get; set; }
        [DataMember, DataColumn(false)]
        public System.Int32 intDurationID { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strHalfDayFor { get; set; }
        /* end of update*/

        public ApprovalFlow() { }
    }
}