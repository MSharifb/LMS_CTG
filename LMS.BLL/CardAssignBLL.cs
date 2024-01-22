using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class CardAssignBLL
    {
        public int Add(CardAssign objCardAssign)
        {            

            return CardAssignDAL.Save(objCardAssign, "I");
        }


        public int Edit(CardAssign objCardAssign)
        {
            return CardAssignDAL.Save(objCardAssign, "U");

        }

        public int Delete(int Id)
        {
            CardAssign obj = new CardAssign();
            obj.intCardAssignID = Id;

            return CardAssignDAL.Save(obj, "D");
        }

        public CardAssign GetByID(int Id)
        {
            return CardAssignDAL.Get(Id, "","","","").Single();
        }

        public List<CardAssign> GetAll()
        {
            return CardAssignDAL.Get(-1, "", "", "", "");
        }

        public List<CardAssign> Get(int intCardAssignID, string strAssignID, string strEmpID, string strCardID, string dtEffectiveDate)
        {
            return CardAssignDAL.Get(intCardAssignID, strAssignID, strEmpID, strCardID, dtEffectiveDate);
        }
        
    }
}
