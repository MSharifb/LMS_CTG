using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class OOALocationWiseCommentsBLL
    {
        //public static int Save(OOALocationWiseComments obj)
        //{
        //   // return OOALocationWiseCommentsDAL.SaveData(obj, "I");
        //}

        //public static int Update(OOALocationWiseComments obj)
        //{
        //   // return OOALocationWiseCommentsDAL.SaveData(obj, "U");
        //}

        //public static int Delete(OOALocationWiseComments obj)
        //{
        //    return OOALocationWiseCommentsDAL.SaveData(obj, "D");
        //}

        public static List<OOALocationWiseComments> Get(OOALocationWiseComments obj)
        {
            return OOALocationWiseCommentsDAL.Get(obj);
        }
    }
}
