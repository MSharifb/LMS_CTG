using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class ReligionBLL
    {
        public Religion ReligionGet(string Id, string strCompanyId)
        {
            return ReligionDAL.GetItemList(Id, strCompanyId).Single();
        }

        public List<Religion> ReligionGetAll()
        {
            return ReligionDAL.GetItemList("", "");
        }
        public List<Religion> ReligionGetAll(string strCompanyId)
        {
            return ReligionDAL.GetItemList("", strCompanyId);
        }

    }
}
