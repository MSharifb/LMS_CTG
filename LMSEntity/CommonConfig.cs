using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class CommonConfig : EntityBase
    {
        
        [DataMember, DataColumn(true)]
        public System.Int32 ConfigID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String ConfigKey { get; set; }
        [DataMember, DataColumn(true)]
        public System.String ConfigValue { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strControlType { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDataType { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsMandatory { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intParentID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIfParentVal { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsChildMandatory { get; set; }        
        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCaption { get; set; }

        private List<CommonConfig> _LstCommonConfig;
        public List<CommonConfig> LstCommonConfig
        {
            get
            {
                if (_LstCommonConfig == null)
                {
                    _LstCommonConfig = new List<CommonConfig>();
                }
                return _LstCommonConfig;
            }
            set { _LstCommonConfig = value; }
        }


        public CommonConfig() { }

    }
}
