using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class BreakTypeBLL
    {
        public int Add(BreakType objBreakType)
        {            

            return BreakTypeDAL.Save(objBreakType, "I");
        }


        public int Edit(BreakType objBreakType)
        {
            return BreakTypeDAL.Save(objBreakType, "U");

        }

        public int Delete(int Id)
        {
            BreakType obj = new BreakType();
            obj.intBreakID = Id;

            return BreakTypeDAL.Save(obj, "D");
        }

        public BreakType GetByID(int Id)
        {
            return BreakTypeDAL.Get(Id, "").Single();
        }

        public List<BreakType> GetAll()
        {
            return BreakTypeDAL.Get(-1, "");
        }

        public List<BreakType> Get(int intBreakID, string strBreakName)
        {
            return BreakTypeDAL.Get(intBreakID, strBreakName);
        }

    }
}
