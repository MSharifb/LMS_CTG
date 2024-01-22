using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class HRPolicyTypeNameBLL
    {
        public static List<HRPolicyTypeName> GetItemList(int HRPolicyTypeNameID, string HRPolicyTypeName, int startRow, int maxRows, out int P)
        {
            return HRPolicyTypeNameDAL.GetItemList(HRPolicyTypeNameID, HRPolicyTypeName, startRow, maxRows, out P);
        }

        public static int SaveItem(HRPolicyTypeName obj)
        {
            return HRPolicyTypeNameDAL.SaveItem(obj, "I");
        }

        public static int UpdateItem(HRPolicyTypeName obj)
        {
            return HRPolicyTypeNameDAL.SaveItem(obj, "U");
        }

        public static int DeleteItem(HRPolicyTypeName obj)
        {
            return HRPolicyTypeNameDAL.SaveItem(obj, "D");
        }
    }
}
