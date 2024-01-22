using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ImportAttendRawDataBLL
    {
        public int Add(string strPath, string strDBUser, string strPass, string appUser)
        {
            return ImportAttendRawDataDAL.SaveItem(strPath, strDBUser, strPass, 1, 10000, appUser);
        }
    
    }
}
