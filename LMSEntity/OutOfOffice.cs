using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class OutOfOffice:EntityBase
    {


        private string _strdesignaiton;
        public string Strdesignaiton
        {
            get { return _strdesignaiton; }
            set { _strdesignaiton = value; }
        }

        public string _strdepartment;
        public string Strdepartment
        {
            get { return _strdepartment; }
            set { _strdepartment = value; }
        }

        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }
                
        

        [DataMember, DataColumn(true)]
        public Int64 ID { get; set; }

        [DataMember, DataColumn(true)]
        public string STREMPID { get; set; }

        [DataMember, DataColumn(true)]
        public string EMPNAME { get; set; }

        [DataMember, DataColumn(true)]
        public string PURPOSE { get; set; }

        [DataMember, DataColumn(true)]
        public string OTHERPURPOSE { get; set; }
        
        [DataMember, DataColumn(true)]
        public string RESPONSIBLEPERSONID { get;set; }

        [DataMember, DataColumn(true)]
        public string RESPONSIBLEPERSON { get; set; }

        [DataMember, DataColumn(true)]
        public string CONTACTPHONE { get; set; }

        [DataMember, DataColumn(true)]
        public string VISITLOCATION { get; set; }
             
        [DataMember, DataColumn(true)]
        public DateTime GETOUTDATE { get; set; }

        [DataMember, DataColumn(true)]
        public string GETOUTTIME { get; set; }

        [DataMember, DataColumn(true)]
        public string STRGETOUTDATE { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime EXPGETINDATE { get; set; }

        [DataMember, DataColumn(true)]
        public string  STREXPGETINDATE { get; set; }

        [DataMember, DataColumn(true)]
        public string EXPGETINTIME { get; set; }
        
        [DataMember, DataColumn(true)]
        public DateTime GETINDATE { get; set; }

        [DataMember, DataColumn(true)]
        public string STRGETINDATE { get; set; }

        [DataMember, DataColumn(true)]
        public string GETINTIME { get; set; }

        [DataMember, DataColumn(true)]
        public string COMMENTS { get; set; }

        [DataMember, DataColumn(true)]
        public string STATUS { get; set; }

        [DataMember, DataColumn(true)]
        public bool ISGETIN { get; set; }

        [DataMember, DataColumn(true)]
        public bool ISPERMITTED { get; set; }

        [DataMember, DataColumn(true)]
        public bool ISVARIFIED { get; set; }

        [DataMember, DataColumn(true)]
        public bool ISAPPROVED { get; set; }

        [DataMember, DataColumn(true)]
        public string PERMITTEDBY { get; set; }

        [DataMember, DataColumn(true)]
        public string VERIFIEDBY { get; set; }

        [DataMember, DataColumn(true)]
        public string APPROVEDBY { get; set; }

        [DataMember, DataColumn(true)]
        public int APPROVALSTATUS { get; set; }

        [DataMember, DataColumn(true)]
        public int BITISNEWARRIVAL { get; set; }

        [DataMember, DataColumn(true)]
        public int BITISREVIEW { get; set; }  


    }
}
