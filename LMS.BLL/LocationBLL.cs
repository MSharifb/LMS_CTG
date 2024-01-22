using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LocationBLL
    {
        public int Add(Location objLocation)
        {
            return LocationDAL.SaveItem(objLocation, "I");
        }
        public int Edit(Location objLocation)
        {
            return LocationDAL.SaveItem(objLocation, "U");
        }
        public int Delete(string Id)
        {
            Location obj = new Location();
            obj.strLocationID = Id;

            return LocationDAL.SaveItem(obj, "D");
        }

        public Location LocationGet(string Id)
        {
            return LocationDAL.GetItemList(Id, "", "").Single();
        }

        public List<Location> LocationGetAll()
        {
            return LocationDAL.GetItemList("", "", "");
        }

        public List<Location> LocationGet(string strLocationID, string strLocation, string strCompanyID)
        {
            return LocationDAL.GetItemList(strLocationID, strLocation, strCompanyID);
        }
    }
}
