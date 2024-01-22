using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;


namespace LMSEntity
{
    public class HRMColumnMapping : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 ColumnID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 TableID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String ColumnName { get; set; }
        [DataMember, DataColumn(true)]
        public System.String SourceColumnName { get; set; }

        private List<HRMColumnMapping> _LstHRMColumnMapping;
        public List<HRMColumnMapping> LstHRMColumnMapping
        {
            get
            {
                if (_LstHRMColumnMapping == null)
                {
                    _LstHRMColumnMapping = new List<HRMColumnMapping>();
                }
                return _LstHRMColumnMapping;
            }
            set { _LstHRMColumnMapping = value; }
        }


        public HRMColumnMapping() { }
    }
}
