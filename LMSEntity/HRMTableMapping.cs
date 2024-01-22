using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class HRMTableMapping : EntityBase
    {
        [DataMember, DataColumn(true)]
        public System.Int32 TableID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String TableName { get; set; }
        [DataMember, DataColumn(true)]
        public System.String SourceTableName { get; set; }

        private List<HRMTableMapping> _LstHRMTableMapping;
        public List<HRMTableMapping> LstHRMTableMapping
        {
            get
            {
                if (_LstHRMTableMapping == null)
                {
                    _LstHRMTableMapping = new List<HRMTableMapping>();
                }
                return _LstHRMTableMapping;
            }
            set { _LstHRMTableMapping = value; }
        }


        public HRMTableMapping() { }
    }
}
