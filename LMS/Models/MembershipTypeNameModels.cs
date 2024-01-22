using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSEntity;
using LMS.BLL;
using MvcPaging;
using MvcContrib.Pagination;

namespace LMS.Web.Models
{
    public class MembershipTypeNameModels
    {
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string _message;
        private MembershipTypeName membershipTypeNameObj;
        private List<MembershipTypeName> lstMembershipTypeName;
        private IPagedList<MembershipTypeName> _lstMembershipTypeNamePaging;


        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public MembershipTypeName MembershipTypeNameObj
        {
            get { return membershipTypeNameObj; }
            set { membershipTypeNameObj = value; }
        }

        public IPagedList<MembershipTypeName> LstMembershipTypeNamePaging
        {
            get { return _lstMembershipTypeNamePaging; }
            set { _lstMembershipTypeNamePaging = value; }
        }

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

        public List<MembershipTypeName> LstMembershipTypeName
        {
            get { return lstMembershipTypeName; }
            set { lstMembershipTypeName = value; }
        }


        public List<MembershipTypeName> GetData(int MembershipTypeNameID, string membershipTypeName, int startRow, int maxRows, out int P)
        {
            int numTotal = 0;
            LstMembershipTypeName = MembershipTypeNameBLL.GetItemList(MembershipTypeNameID, membershipTypeName, startRow, maxRows, out numTotal);
            numTotalRows = numTotal;
            P = numTotal;
            return LstMembershipTypeName;
        }

        public MembershipTypeName GetDataByID(int MembershipTypeNameID)
        {
            int numTotal = 0;
            LstMembershipTypeName = MembershipTypeNameBLL.GetItemList(MembershipTypeNameID, "", 1, 10, out numTotal);
            numTotalRows = numTotal;

            return LstMembershipTypeName[0];
        }

        public int SaveData(MembershipTypeName obj)
        {
            return MembershipTypeNameBLL.SaveItem(obj);
        }

        public int UpdateData(MembershipTypeName obj)
        {
            return MembershipTypeNameBLL.UpdateItem(obj);
        }

        public int DeleteData(MembershipTypeName obj)
        {
            return MembershipTypeNameBLL.DeleteItem(obj);
        }
    }
}