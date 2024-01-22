using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LMS.DAL;
using TVL.DB;
using LMSEntity;

namespace LMS.BLL
{
    public class HolidayWeekDayBLL
    {
        public int Save(HolidayWeekDay objHolidayWeekDay, List<HolidayWeekDayDetails> lstHolidayDetails, List<HolidayWeekDay> lstHolidayWeekend, ref string strmsg)
        {
            int retVal = -1;
            int intHolidayWeekendMasterID = -1;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            try
            {
                /****************** Save Master Data *************************/

                if (objHolidayWeekDay.intHolidayWeekendMasterID > 0)
                {
                    retVal =  intHolidayWeekendMasterID = HolidayWeekDayDAL.SaveMasterItem(objHolidayWeekDay, "U", transection, con);
                }
                else
                {
                    retVal =  intHolidayWeekendMasterID = HolidayWeekDayDAL.SaveMasterItem(objHolidayWeekDay, "I", transection, con);
                }

                if (retVal < 0)
                {
                    transection.Rollback();
                    return retVal;
                }

                foreach (HolidayWeekDayDetails item in lstHolidayDetails)
                {
                    item.intHolidayWeekendMasterID = intHolidayWeekendMasterID;
                    
                    int i = HolidayWeekDayDetailsDAL.SaveItem(item, "I", transection, con);

                    if (i < 0)
                    {
                        transection.Rollback();
                        return i;
                    }
                }

                if (objHolidayWeekDay.isAutomatic)
                {
                    foreach (HolidayWeekDay item in lstHolidayWeekend)
                    {
                        item.intHolidayWeekendMasterID = intHolidayWeekendMasterID;
                        
                        int i = HolidayWeekDayDAL.SaveItem(item, "I", transection, con);
                        if (i < 0)
                        {
                            transection.Rollback();
                            return i;
                        }
                    }
                }
                else
                {
                    objHolidayWeekDay.intHolidayWeekendMasterID = intHolidayWeekendMasterID;
                    int i = HolidayWeekDayDAL.SaveItem(objHolidayWeekDay, "I", transection, con);
                    if (i < 0)
                    {
                        transection.Rollback();
                        return i;
                    }
                }

                transection.Commit();

            }
            catch (Exception ex)
            {
                transection.Rollback();
                return -547;
            }

            return retVal;
        }


        public int Delete(HolidayWeekDay objHolidayWeekDay)
        {
            int retVal = -1;
            int intHolidayWeekendMasterID = -1;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            try
            {
                /****************** Save Master Data *************************/

                if (objHolidayWeekDay.intHolidayWeekendMasterID > 0)
                {
                    retVal = intHolidayWeekendMasterID = HolidayWeekDayDAL.SaveMasterItem(objHolidayWeekDay, "D", transection, con);
                }

                if (retVal < 0)
                {
                    transection.Rollback();
                    return retVal;
                }

                transection.Commit();

            }
            catch (Exception ex)
            {
                transection.Rollback();
                return -547;
            }

            return retVal;
        }

        //public int AddMaster(HolidayWeekDay objHolidayWeekDay, ref string strmsg)
        //{
        //    return HolidayWeekDayDAL.SaveMasterItem(objHolidayWeekDay, "I");
        //}

        //public int EditMaster(HolidayWeekDay objHolidayWeekDay, ref string strmsg)
        //{
        //    return HolidayWeekDayDAL.SaveMasterItem(objHolidayWeekDay, "U");
        //}

        //public int DeleteMaster(int Id)
        //{
        //    HolidayWeekDay obj = new HolidayWeekDay();
        //    obj.intHolidayWeekendID = Id;

        //    return HolidayWeekDayDAL.SaveMasterItem(obj, "D");
        //}

        //public int Add(HolidayWeekDay objHolidayWeekDay, ref string strmsg)
        //{
        //    return HolidayWeekDayDAL.SaveItem(objHolidayWeekDay, "I");
        //}

        //public int Edit(HolidayWeekDay objHolidayWeekDay, ref string strmsg)
        //{
        //    return HolidayWeekDayDAL.SaveItem(objHolidayWeekDay, "U");
        //}

        //public int Delete(int Id)
        //{
        //    HolidayWeekDay obj = new HolidayWeekDay();
        //    obj.intHolidayWeekendID = Id;

        //    return HolidayWeekDayDAL.SaveItem(obj, "D");
        //}

        public HolidayWeekDay HolidayWeekDayGet(int Id)
        {
            //return HolidayWeekDayDAL.GetItemList(Id, 0, "", "").Single();
            return HolidayWeekDayDAL.GetItemList(Id, 0, "", "").FirstOrDefault();
        }

        public List<HolidayWeekDay> HolidayWeekDayGet(int intLvyearId, string strType, string strCompanyId)
        {
            return HolidayWeekDayDAL.GetItemList(0, intLvyearId, strType, strCompanyId);
        }

        public List<HolidayWeekDay> HolidayWeekDayGetAll()
        {
            return HolidayWeekDayDAL.GetItemList(0, 0, "", "");
        }

        public List<HolidayWeekDay> HolidayWeekDayGetAll(string strCompanyId)
        {
            return HolidayWeekDayDAL.GetItemList(0, 0, "", strCompanyId);
        }

        public List<HolidayWeekDay> HolidayWeekDayGetAll(int intLvyearId, string strCompanyId)
        {
            return HolidayWeekDayDAL.GetItemList(0, intLvyearId, "", strCompanyId);
        }



    }
}
