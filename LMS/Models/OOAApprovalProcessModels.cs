using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSEntity;
using LMS.BLL;
using MvcPaging;
using System.Web.Mvc;
using MvcContrib.Pagination;

namespace LMS.Web.Models
{
    public class OOAApprovalProcessModels
    {
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        private IPagedList<OutOfOffice> _LstOutOfOfficePaging;
        List<OutOfOffice> lstOutOfOffice;

        public int startRowIndex
        {
            get { return _startRowIndex; }
            set { _startRowIndex = value; }
        }

        public int maximumRows
        {
            get { return _maximumRows; }
            set { _maximumRows = value; }
        }

        public int numTotalRows
        {
            get { return _numTotalRows; }
            set { _numTotalRows = value; }
        }

        public IPagedList<OutOfOffice> LstOutOfOfficePaging
        {
            get { return _LstOutOfOfficePaging; }
            set { _LstOutOfOfficePaging = value; }
        }
        public List<OutOfOffice> LstOutOfOffice
        {
            get
            {
                if (lstOutOfOffice == null)
                    lstOutOfOffice = new List<OutOfOffice>();

                return lstOutOfOffice;
            }
            set { lstOutOfOffice = value; }
        }

        IList<OutOfOfficeLocaton> lstOutOfOfficeLocation;
        public IList<OutOfOfficeLocaton> LstOutOfOfficeLocation
        {
            get { return lstOutOfOfficeLocation; }
            set { lstOutOfOfficeLocation = value; }
        }

        public static IList<OutOfOfficeLocaton> GetOutOfOfficeLocation(OutOfOfficeLocaton searchObj)
        {
            IList<OutOfOfficeLocaton> lst = new List<OutOfOfficeLocaton>();
            lst = OutOfOfficeBLL.GetOutOfOfficeLocation(searchObj, DateTime.MinValue, DateTime.MaxValue, 1, 1);
            return lst;
        }

        public  List<OutOfOffice> getDataForPermissionNVerify(string strAuthorID,string strEmpID,string fromDate,int StartIndex,int RowNum)
        {
            int P;
            LstOutOfOffice = OutOfOfficeBLL.GetListForPermissionNVerification(strAuthorID,strEmpID, fromDate,StartIndex, RowNum, out P);
            numTotalRows = P;
            return LstOutOfOffice;
        }

        public static OutOfOfficeModels GetDetailsData(OutOfOffice searchObj,string strAuthorID,  DateTime fromDate, DateTime toDate, int startIndex, int rowNumber)
        {
            OutOfOfficeModels model = new OutOfOfficeModels();
            try
            {
               
                List<OutOfOffice> lst = new List<OutOfOffice>();
                int totalRows;
                lst = OutOfOfficeBLL.GetApprovalDetaisData(searchObj,strAuthorID, fromDate, toDate, 0, 100, out totalRows);
                OutOfOfficeLocaton Obj = new OutOfOfficeLocaton();
                Obj.OUTOFOFFICEID = lst[0].ID;
                model.LstOutOfOfficeLocation = GetOutOfOfficeLocation(Obj);
                model.LstOOALocationComments = GetOOALocationComments(searchObj);
                model.LstOutOfOffice = lst;
                model.OutOfOffice = lst[0];
                model.AuthorTypeID = model.GetAuthorTypeID(LoginInfo.Current.strEmpID, model.OutOfOffice.STREMPID);
                model.ProcessAuthor();
                
            }
            catch (Exception ex)
            {
             
            }
            return model;
            
        }

        private static List<OOALocationWiseComments> GetOOALocationComments(OutOfOffice obj)
        {
            OOALocationWiseComments searchObj = new OOALocationWiseComments();
            searchObj.OUTOFOFFICEID = obj.ID;

            return OOALocationWiseCommentsBLL.Get(searchObj);
        }

        public static int Permission(Int64 OutOfOfficeID, string StrEUser)
        {
            return OOAApprovalProcessBLL.Permission(OutOfOfficeID, StrEUser);
        }

        public static int Verify(Int64 OutOfOfficeID, string StrEUser)
        {
            return OOAApprovalProcessBLL.Verify(OutOfOfficeID, StrEUser);
        }

        public static int Approve(Int64 OutOfOfficeID, string StrEUser)
        {
            return OOAApprovalProcessBLL.Approve(OutOfOfficeID, StrEUser);
        }

        public static int Recommend(Int64 OutOfOfficeID, string StrEUser)
        {
            return OOAApprovalProcessBLL.Recommend(OutOfOfficeID, StrEUser);
        }

        public static int Reverify(Int64 OutOfOfficeID, string StrEUser)
        {
            return OOAApprovalProcessBLL.Reverify(OutOfOfficeID, StrEUser);
        }

      
    }
}