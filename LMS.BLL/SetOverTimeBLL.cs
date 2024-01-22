using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class SetOverTimeBLL
    {
        public int Add(SetOverTime objSetOverTime)
        {            

            return SetOverTimeDAL.Save(objSetOverTime, "I");
        }


        public int Edit(SetOverTime objSetOverTime)
        {
            return SetOverTimeDAL.Save(objSetOverTime, "U");

        }

        public int Delete(int Id)
        {
            SetOverTime obj = new SetOverTime();
            obj.intRowID = Id;

            return SetOverTimeDAL.Save(obj, "D");
        }

        public SetOverTime GetByID(int Id)
        {
            return SetOverTimeDAL.Get(Id,"","","","","",-1,"","").Single();
        }

        public List<SetOverTime> GetAll()
        {
            return SetOverTimeDAL.Get(-1,"","","","","",-1,"","");
        }

        public List<SetOverTime> Get(int intRowID, string strEmpID, string strCompanyID, string strLocationID,
            string strDesignationID, string strDepartmentID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo)
        {
            return SetOverTimeDAL.Get(intRowID,strEmpID,strCompanyID,strLocationID,strDesignationID,strDepartmentID,intCategoryCode,dtPeriodFrom,dtPeriodTo);
        }
        
    }
}
