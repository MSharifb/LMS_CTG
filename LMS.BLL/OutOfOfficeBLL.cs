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
    public class OutOfOfficeBLL
    {
        public static int Save(OutOfOffice obj,IList<OutOfOfficeLocaton> lst,string authorID,int intAuthorTypeID,string company,string userID, string strMode)
        {
            int i = -1;
            int intMasterId = -1;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            try
            {
                intMasterId = OutOfOfficeDAL.Save(obj,transection,con,company,userID, strMode);


                if (lst != null)
                {
                    OOALocationWiseComments comObj = new OOALocationWiseComments();
                    OutOfOfficeLocaton obj1 = new OutOfOfficeLocaton();
                    obj1.OUTOFOFFICEID = intMasterId;
                    comObj.OUTOFOFFICEID = Int64.Parse(intMasterId.ToString());
                    SaveOutOfOfficeLocation(obj1,transection,con, "AD");
                    DeleteOOALocationComments(comObj,transection,con);
                    
                    foreach (OutOfOfficeLocaton item in lst)
                    {
                        item.OUTOFOFFICEID = intMasterId;
                        i = SaveOutOfOfficeLocation(item,transection,con, "I");
                        i = SaveOOALocationComments(item.LocationWiseCommentsList,transection,con, Int64.Parse(intMasterId.ToString()), Int64.Parse(i.ToString()),authorID,intAuthorTypeID);

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

        public static List<OutOfOffice> GetData(OutOfOffice searchObj,DateTime fromDate,DateTime toDate,int startIndex,int rowNum,out int P)
        {
            return OutOfOfficeDAL.Get(searchObj, fromDate, toDate, startIndex, rowNum,out P);
        }

        public static List<OutOfOffice> GetApprovalDetaisData(OutOfOffice searchObj,string strAuthorID, DateTime fromDate, DateTime toDate, int startIndex, int rowNum, out int P)
        {
            return OutOfOfficeDAL.GetApprovalDetaisData(searchObj, strAuthorID,fromDate, toDate, startIndex, rowNum, out P);
        }


        public static List<OutOfOffice> GetReport(OutOfOffice searchObj, string fromDate, string toDate, int startIndex, int rowNum, out int numTotalRows)
        {
            return OutOfOfficeDAL.GetReport(searchObj, fromDate, toDate, startIndex, rowNum,out numTotalRows);
        }
        public static int SaveOutOfOfficeLocation(OutOfOfficeLocaton obj,IDbTransaction transaction,IDbConnection con, string strMode)
        {
            return OutOfOfficeDAL.SaveOutOfOfficeLocation(obj,transaction,con, strMode);
        }

        public static List<OutOfOfficeLocaton> GetOutOfOfficeLocation(OutOfOfficeLocaton searchObj, DateTime fromDate, DateTime toDate, int startIndex, int rowNum)
        {
            return OutOfOfficeDAL.GetOutOfOfficeLocation(searchObj, fromDate, toDate, startIndex, rowNum);
        }

        public static List<OutOfOffice> GetListForPermissionNVerification(string strAuthorID,string strEmpID,string fromDate,int StartIndex,int RowNumber,out int numTotalRows)
        {
            return OutOfOfficeDAL.GetForPermissionNVerify(strAuthorID,strEmpID, fromDate, StartIndex, RowNumber, out numTotalRows);
        }

        private static int SaveOOALocationComments(List<OOALocationWiseComments> lst,IDbTransaction transaction, IDbConnection con, Int64 OutOfOfficeID,Int64 LocationID,string strAuthorID,int intAuthorTypeID)
        {
            int i = 0;
            if (lst == null) return 1;
            foreach (OOALocationWiseComments item in lst)
            {
                
                if (item.OUTOFOFFICEID <1)
                item.OUTOFOFFICEID = OutOfOfficeID;

                
                if (item.LOCATIONID < 1)
                item.LOCATIONID = LocationID;

              
                if (item.INTAUTHORTYPEID < 1)
                item.INTAUTHORTYPEID = intAuthorTypeID;

                if (item.STRAUTHORID != null)
                    if (item.STRAUTHORID.Length < 1)
                        item.STRAUTHORID = strAuthorID;
                    else
                    { }
                else
                {
                    item.STRAUTHORID = strAuthorID;
                }

                if (item.STRCOMMENTS != null)
                if (item.STRCOMMENTS.Length > 0)
                    i = OOALocationWiseCommentsDAL.SaveData(item,transaction,con,"I");
            }
            return i;
        }

        private static int DeleteOOALocationComments(OOALocationWiseComments obj, IDbTransaction transaction, IDbConnection con)
        {
            OOALocationWiseCommentsDAL.SaveData(obj, transaction, con, "D");
            return 0;
        }


        public static List<SearchLocation> GetSearchLocation(string searchObj)
        {
            return OutOfOfficeDAL.GetSearchLocation(searchObj);
        }
    }
}
