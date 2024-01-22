using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using LMS.DAL;
using System.Data;

namespace LMS.BLL
{
    public class OOAApprovalCommentsBLL
    {
        public static int Save(OOAApprovalComments obj)
        {
            return OOAApprovalCommentsDAL.SaveData(obj, "I");
        }

        public static int Update(OOAApprovalComments obj)
        {
            return OOAApprovalCommentsDAL.SaveData(obj, "U");
        }

        public static int Delete(OOAApprovalComments obj)
        {
            return OOAApprovalCommentsDAL.SaveData(obj, "D");
        }

        public static List<OOAApprovalComments> Get(OOAApprovalComments obj)
        {
            return OOAApprovalCommentsDAL.Get(obj);
        }
    }
}
