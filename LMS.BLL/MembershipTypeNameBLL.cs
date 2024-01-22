using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LMSEntity;
using LMS.DAL;


namespace LMS.BLL
{
    public class MembershipTypeNameBLL
    {
        public static List<MembershipTypeName> GetItemList(int MembershipTypeNameID, string MembershipTypeName, int startRow, int maxRows, out int P)
        {
            return MembershipTypeNameDAL.GetItemList(MembershipTypeNameID, MembershipTypeName, startRow, maxRows, out P);
        }

        public static int SaveItem(MembershipTypeName obj)
        {
            return MembershipTypeNameDAL.SaveItem(obj, "I");
        }

        public static int UpdateItem(MembershipTypeName obj)
        {
            return MembershipTypeNameDAL.SaveItem(obj, "U");
        }

        public static int DeleteItem(MembershipTypeName obj)
        {
            return MembershipTypeNameDAL.SaveItem(obj, "D");
        }
    }
}
