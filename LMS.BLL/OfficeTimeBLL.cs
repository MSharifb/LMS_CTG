using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
using System.Transactions;
using System.Data;
using TVL.DB;

namespace LMS.BLL
{
    public class OfficeTimeBLL
    {
        public int Add(OfficeTime objOfficeTime, ref string strmsg)
        {
            int intResult = 0;
            try
            {
                if (CheckValidation(objOfficeTime, ref strmsg) == true)
                {
                    intResult = OfficeTimeDAL.SaveItem(objOfficeTime, "I");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return intResult;
        }


        public int Edit(OfficeTime objOfficeTime, ref string strmsg)
        {
            int intResult = 0;
            try
            {
                if (CheckValidation(objOfficeTime, ref strmsg) == true)
                {
                    intResult = OfficeTimeDAL.SaveItem(objOfficeTime, "U");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return intResult;

        }


        public int Delete(string compId, int intYearId)
        {
            OfficeTime obj = new OfficeTime();
            obj.strCompanyID = compId;
            obj.intLeaveYearID = intYearId;

            return OfficeTimeDAL.SaveItem(obj, "D");
        }

        public int SaveOfficeTime(OfficeTime objOfficeTime, List<OfficeTimeDetails> lstOffT,string strMode, ref string strmsg)
        {
            int intResult = 0;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {
                if (strMode == "I")
                {
                    if (CheckValidation(objOfficeTime, ref strmsg) == true)
                    {
                        intResult = OfficeTimeDAL.SaveItem(objOfficeTime, "I", transection, con);
                    }
                
                }
                else if (strMode == "U")
                {
                    if (CheckValidation(objOfficeTime, ref strmsg) == true)
                    {
                        intResult = OfficeTimeDAL.SaveItem(objOfficeTime, "U", transection, con);
                    }                
                }

                if (lstOffT != null && strmsg.ToString().Length == 0)
                   intResult = OfficeTimeDetailsDAL.SaveItem(lstOffT.FirstOrDefault(), "D", transection, con);
                {
                    foreach (OfficeTimeDetails obj in lstOffT)
                    {
                        intResult = OfficeTimeDetailsDAL.SaveItem(obj, "I", transection, con);
                    }
                }

            }
            catch (Exception ex)
            {
                transection.Rollback();
                strmsg = ex.Message;
            }

            finally
            {
                if (intResult < 0)
                {
                    transection.Rollback();
                }
                else
                {
                    transection.Commit();
                }
            }

            return intResult;
        }
        
        
        
        
        
        
        public OfficeTime OfficeTimeGet(string compId, int intYearId)
        {
            return OfficeTimeDAL.GetItemList(compId, intYearId).SingleOrDefault();
        }

        public List<OfficeTime> OfficeTimeGetAll(string compId)
        {
            return OfficeTimeDAL.GetItemList(compId, 0);
        }

        public List<OfficeTime> OfficeTimeGetAll()
        {
            return OfficeTimeDAL.GetItemList("", 0);
        }


        public List<OfficeTimeDetails> OfficeTimeDetailsGet(int intYearId)
        {
            return OfficeTimeDetailsDAL.GetItemList(intYearId);
        }


        private bool CheckValidation(OfficeTime objOfficeTime, ref string strMSG)
        {
            bool isvalid = true;

            if (objOfficeTime.fltDuration == 0)
            {
                strMSG = "Total Working hour must be greater than zero.";
                isvalid = false;
            }
            if (objOfficeTime.fltDuration > 24)
            {
                strMSG = "Total Working hour must be within 24 hours.";
                isvalid = false;
            }
            return isvalid;
        }
    }
}
