using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class CompanyUnitBLL
    {
        public static List<CompanyUnit> GetList(int unitID, int companyID)
        {
            return CompanyUniDAL.GetList(unitID, companyID);
        }
    }
}
