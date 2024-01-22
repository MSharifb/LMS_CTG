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
    public class HRPolicyNameModels
    {
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string _message;        
        private HRPolicyTypeName hrPolicyTypeNameObj;
        private List<HRPolicyTypeName> lstHRPolicyTypeName;
        private IPagedList<HRPolicyTypeName> _lstHRPolicyTypeNamePaging;


        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public HRPolicyTypeName HrPolicyTypeNameObj
        {
            get { return hrPolicyTypeNameObj; }
            set { hrPolicyTypeNameObj = value; }
        }

        public IPagedList<HRPolicyTypeName> LstHRPolicyTypeNamePaging
        {
            get { return _lstHRPolicyTypeNamePaging; }
            set { _lstHRPolicyTypeNamePaging = value; }
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

        public List<HRPolicyTypeName> LstHRPolicyTypeName
        {
            get { return lstHRPolicyTypeName; }
            set { lstHRPolicyTypeName = value; }
        }


        public List<HRPolicyTypeName> GetData(int HRPolicyTypeNameID,string HRPolicyTypeName, int startRow,int maxRows,out int P)
        { 
            int numTotal=0;
            LstHRPolicyTypeName =  HRPolicyTypeNameBLL.GetItemList(HRPolicyTypeNameID, HRPolicyTypeName, startRow, maxRows, out numTotal);
            numTotalRows = numTotal;
            P = numTotal;
            return LstHRPolicyTypeName;
        }

        public HRPolicyTypeName GetDataByID(int HRPolicyTypeNameID)
        {
            int numTotal = 0;
            LstHRPolicyTypeName = HRPolicyTypeNameBLL.GetItemList(HRPolicyTypeNameID, "", 1, 10, out numTotal);
            numTotalRows = numTotal;

            return LstHRPolicyTypeName[0];
        }

        public int SaveData(HRPolicyTypeName obj)
        {
            return HRPolicyTypeNameBLL.SaveItem(obj);
        }

        public int UpdateData(HRPolicyTypeName obj)
        {
            return HRPolicyTypeNameBLL.UpdateItem(obj);
        }

        public int DeleteData(HRPolicyTypeName obj)
        {
            return HRPolicyTypeNameBLL.DeleteItem(obj);
        }


    }
}