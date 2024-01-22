using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class CardInfoBLL
    {
        public int Add(CardInfo objCardInfo)
        {            

            return CardInfoDAL.Save(objCardInfo, "I");
        }


        public int Edit(CardInfo objCardInfo)
        {
            return CardInfoDAL.Save(objCardInfo, "U");

        }

        public int Delete(int Id)
        {
            CardInfo obj = new CardInfo();
            obj.intCardID = Id;

            return CardInfoDAL.Save(obj, "D");
        }

        public CardInfo GetByID(int Id)
        {
            return CardInfoDAL.Get(Id, "",-1).Single();
        }

        public List<CardInfo> GetAll()
        {
            return CardInfoDAL.Get(-1, "",-1);
        }

        public List<CardInfo> Get(int intCardID, string strCardID, int intStatus)
        {
            return CardInfoDAL.Get(intCardID, strCardID, intStatus);
        }

    }
}
