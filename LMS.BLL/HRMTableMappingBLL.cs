using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class HRMTableMappingBLL
    {
        public int Add(HRMTableMapping objHRMTableMapping, out string strmsg)
        {
            return HRMTableMappingDAL.SaveItem(objHRMTableMapping, "I", out strmsg);
        }

        public void Edit(List<HRMTableMapping> objHRMTableMappingList, out string strmsg)
        {
            strmsg = "";
            try
            {
                HRMTableMappingDAL.SaveItemList(objHRMTableMappingList, "U", out strmsg);
            }
            catch (Exception ex)
            {

            }
        }

        public HRMTableMapping HRMTableMappingGet(Int32 intTableID)
        {
            return HRMTableMappingDAL.GetItemList(intTableID).Single();
        }

        public List<HRMTableMapping> HRMTableMappingGetAll()
        {
            return HRMTableMappingDAL.GetItemList(0);
        }
    }
}
