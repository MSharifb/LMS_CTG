using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
    public class rptCompanyInformation
    {
        public rptCompanyInformation()
        {
        }

        [DataMember, DataColumn(true)]
        public System.Int32 Id { get; set; }

        [DataMember, DataColumn(true)]
        public System.String CompanyName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String Address { get; set; }

        [DataMember, DataColumn(true)]
        public System.String City { get; set; }

        [DataMember, DataColumn(true)]
        public System.String Country { get; set; }

        [DataMember, DataColumn(true)]
        public System.String Phone1 { get; set; }

        [DataMember, DataColumn(true)]
        public System.String Phone2 { get; set; }

        [DataMember, DataColumn(true)]
        public System.String FaxNo { get; set; }

        [DataMember, DataColumn(true)]
        public System.String EmailAddress { get; set; }

        [DataMember, DataColumn(true)]
        public System.String WebURL { get; set; }

        [DataMember, DataColumn(true)]
        public byte[] CompanyLogo { get; set; }

        [DataMember, DataColumn(true)]
        public System.String IUser { get; set; }

        [DataMember, DataColumn(true)]
        public System.String EUser { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime IDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime EDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 ApplicationId { get; set; }

        #region Zone
        [DataMember, DataColumn(true)]
        public System.Int32 ZoneId { get; set; }

        [DataMember, DataColumn(true)]
        public System.String ZoneName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String ZoneAddress { get; set; }

        [DataMember, DataColumn(true)]
        public System.String ZoneCode { get; set; }

        [DataMember, DataColumn(true)]
        public System.Boolean IsHeadOffice { get; set; } 
        #endregion

    }
}
