using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class DataSynchronizerBAL
    {
        public int IntializeData(DataSynchronizer obj)
        {
            return DataSynchronizerDAL.SaveItem(obj, "I");
        }

        public int SynchronizeData(DataSynchronizer obj)
        {
            return DataSynchronizerDAL.SaveItem(obj, "S");
        }
    }
}
