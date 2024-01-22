using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LMSEntity
{
   public class MobileIO : ManualIO
    {
        [DataMember, DataColumn(true)]
        public System.Boolean bitFromMobile { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double Longitude { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double Latitude { get; set; }

        public MobileIO() : base()
        {
            this.bitFromMobile = true;
        }
    }
}
