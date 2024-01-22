using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LMSEntity;
using LMS.DAL;


namespace LMS.BLL
{
    public class JobTypeNameBLL
    {
        public static List<JobTypeName> GetItemList(int JobTypeNameID, string JobTypeName, int startRow, int maxRows, out int P)
        {
            return JobTypeNameDAL.GetItemList(JobTypeNameID, JobTypeName, startRow, maxRows, out P);
        }

        public static int SaveItem(JobTypeName obj)
        {
            return JobTypeNameDAL.SaveItem(obj, "I");
        }

        public static int UpdateItem(JobTypeName obj)
        {
            return JobTypeNameDAL.SaveItem(obj, "U");
        }

        public static int DeleteItem(JobTypeName obj)
        {
            return JobTypeNameDAL.SaveItem(obj, "D");
        }
    }
}
