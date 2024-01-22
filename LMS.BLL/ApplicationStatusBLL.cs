using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ApplicationStatusBLL
    {
        public List<ApplicationStatusCaption> ApplicationStatusGet(string strCompanyID)
        {
            return ApplicationStatusDAL.GetItemList(strCompanyID);
        }
    }
}
