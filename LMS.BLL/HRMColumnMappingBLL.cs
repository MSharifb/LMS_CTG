using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class HRMColumnMappingBLL
    {
        public int Add(HRMColumnMapping objHRMColumnMapping, out string strmsg)
        {
            return HRMColumnMappingDAL.SaveItem(objHRMColumnMapping, "I", out strmsg);
        }

        public void Edit(List<HRMColumnMapping> objHRMColumnMappingList, out string strmsg)
        {
            strmsg = "";
            try
            {
                HRMColumnMappingDAL.SaveItemList(objHRMColumnMappingList, "U", out strmsg);
            }
            catch (Exception ex)
            {

            }
        }

        public HRMColumnMapping HRMColumnMappingGetByColumnID(Int32 intColumnID)
        {
            return HRMColumnMappingDAL.GetItemList(intColumnID, 0).Single();
        }

        public List<HRMColumnMapping> HRMColumnMappingGetByTableID(Int32 intTableID)
        {
            return HRMColumnMappingDAL.GetItemList(0, intTableID);
        }

        public List<HRMColumnMapping> HRMColumnMappingGetAll()
        {
            return HRMColumnMappingDAL.GetItemList(0,0);
        }
    }
}
