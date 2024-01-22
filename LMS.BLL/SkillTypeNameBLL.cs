using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class SkillTypeNameBLL
    {
        public static List<SkillTypeName> GetItemList(int SkillTypeNameID, string SkillTypeName, int startRow, int maxRows, out int P)
        {
            return SkillTypeNameDAL.GetItemList(SkillTypeNameID, SkillTypeName, startRow, maxRows, out P);
        }

        public static int SaveItem(SkillTypeName obj)
        {
            return SkillTypeNameDAL.SaveItem(obj, "I");
        }

        public static int UpdateItem(SkillTypeName obj)
        {
            return SkillTypeNameDAL.SaveItem(obj, "U");
        }

        public static int DeleteItem(SkillTypeName obj)
        {
            return SkillTypeNameDAL.SaveItem(obj, "D");
        }
    }
}
