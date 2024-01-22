using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
using System.Data;
using TVL.DB;

namespace LMS.BLL
{
    public class MISCBLL
    {
        public static int Save(string userID, MISCMaster obj, IList<MISCDetails> lst, string strMode)
        {
            int i = -1;
            int intMasterId = -1;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            try
            {
                intMasterId = MISCDAL.Save(obj, transection, con, userID, strMode);


                if (lst != null)
                {
                    MISCDetails obj1 = new MISCDetails();
                    obj1.MISCMASTERID = intMasterId;
                    SaveDetails(obj1, transection, con, "DE");

                    foreach (MISCDetails item in lst)
                    {
                        item.MISCMASTERID = intMasterId;
                        i = SaveDetails(item, transection, con, "I");

                        if (i < 0)
                        {
                            transection.Rollback();
                            transection.Dispose();
                        }
                    }
                }

                transection.Commit();
            }
            catch (Exception ex)
            {
                transection.Rollback();
                throw ex;
            }


            return intMasterId;
        }

        public MISCMaster GetSearchedData(int intSearchID)
        {
            return MISCDAL.GetSearchedItemList(intSearchID).Single();
        }

        public static List<MISCMaster> GetData(MISCMaster searchObj,int startIndex, int rowNum, out int P)
        {
            return MISCDAL.Get(searchObj, startIndex, rowNum, out P);
        }

        public static List<MISCMaster> GetSearchData(MISCMaster searchObj, int startIndex, int rowNum, out int P)
        {
            return MISCDAL.GetSearchData(searchObj, startIndex, rowNum, out P);
        }

        public static int SaveDetails(MISCDetails obj, IDbTransaction transaction, IDbConnection con, string strMode)
        {
            return MISCDAL.SaveDetails(obj, transaction, con, strMode);
        }

        public static List<MISCDetails> GetDetails(MISCDetails searchObj, int startIndex, int rowNum)
        {
            return MISCDAL.GetDetails(searchObj, startIndex, rowNum);
        }

        public static List<MISCDetails> GetDetails(int MISCMASTERID, int startIndex, int rowNum)
        {
            return MISCDAL.GetDetails(MISCMASTERID, startIndex, rowNum);
        }
        
        //public MISCDetails DetailsGet(int intNodeID)
        //{
        //    return MISCDAL.GetDetailsData(intNodeID).SingleOrDefault();
        //}

        public List<MISCDetails> DetailsGet(int intNodeID, int intSearchID)
        {
            return MISCDAL.GetDetailsData(intNodeID, intSearchID);
        }

        public int Delete(int Id)
        {

            MISCDetails obj = new MISCDetails();
            obj.MISCDETAISLID = Id;

            return MISCDAL.SaveDetails(obj, "D");
        }

        public static int UpdateDetails(IList<MISCDetails> lst,Int64 masterID)
        {
            int i = -1;
           
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            try
            {         
                               

                    foreach (MISCDetails item in lst)
                    {
                        item.MISCMASTERID = masterID;
                        i = SaveDetails(item, transection, con, "U");

                        if (i < 0)
                        {
                            transection.Rollback();
                            transection.Dispose();
                        }
                    }
                

                transection.Commit();
            }
            catch (Exception ex)
            {
                transection.Rollback();
                throw ex;
            }


            return i;
        }
    }
}
