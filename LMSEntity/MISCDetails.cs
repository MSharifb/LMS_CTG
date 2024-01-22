using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class MISCDetails : EntityBase
    {
        [DataMember, DataColumn(true)]
        public Int64 MISCDETAISLID { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 MISCMASTERID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRPARTICULAR { get; set; }

        [DataMember, DataColumn(true)]
        public decimal AMOUNT { get; set; }

        [DataMember, DataColumn(true)]
        public decimal APPROVEDAMOUNT { get; set; }

        [DataMember, DataColumn(true)]
        public string STRPURPOSE { get; set; }

        [DataMember, DataColumn(true)]
        public string STRREMARKS { get; set; }

        [DataMember, DataColumn(true)]
        public string ATTACHMENTPATH { get; set; }

        [DataMember, DataColumn(true)]
        public string ATTACHEDFILENAME { get; set; }

        public List<MISCDetails> _LstMISCDetails;
        public List<MISCDetails> LstMISCDetails
        {
            get
            {
                if (_LstMISCDetails == null)
                {
                    _LstMISCDetails = new List<MISCDetails>();
                }
                return _LstMISCDetails;
            }
            set { _LstMISCDetails = value; }
        }


        public MISCDetails() { }
    }
}
