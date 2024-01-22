using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSEntity;
using LMS.BLL;
using MvcContrib.Pagination;
using MvcPaging;


namespace LMS.Web.Models
{
    public class MyConveyanceModels
    {
        private List<MyConveyanceMaster> lstConveyanceMaster;
        private List<MyConveyanceDetails> lstConveyanceDetails;
        private IPagedList<MyConveyanceMaster> _LstConveyanceMasterPaging;
        private List<ConveyanceApproverDetails> lstConveyanceApproverDetails;


        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string _isPaid;
        string strDesignation;
        private MyConveyanceDetails conveyanceObj;

        public string StrDesignation
        {
            get { return strDesignation; }
            set { strDesignation = value; }
        }

        public string IsPaid
        {
            get { return _isPaid; }
            set { _isPaid = value; }
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

        public List<ConveyanceApproverDetails> LstConveyanceApproverDetails
        {
            get { return lstConveyanceApproverDetails; }
            set { lstConveyanceApproverDetails = value; }
        }

        public MyConveyanceDetails ConveyanceObj
        {
            get { return conveyanceObj; }
            set { conveyanceObj = value; }
        }
        public List<MyConveyanceMaster> LstConveyanceMaster
        {
            get { return lstConveyanceMaster; }
            set { lstConveyanceMaster = value; }
        }

        public List<MyConveyanceDetails> LstConveyanceDetails
        {
            get { return lstConveyanceDetails; }
            set { lstConveyanceDetails = value; }
        }


        public IPagedList<MyConveyanceMaster> LstConveyanceMasterPaging
        {
            get { return _LstConveyanceMasterPaging; }
            set { _LstConveyanceMasterPaging = value; }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public MyConveyanceModels GetMasterList(int OutOfOfficeID, string strEmpID, string dtDate, int startIndex, int rowNumber)
        {
            int p;
            MyConveyanceModels model = new MyConveyanceModels();
            model.LstConveyanceMaster = MyConveyanceBLL.GetMasterList(OutOfOfficeID, strEmpID, dtDate,startIndex, rowNumber, out p);
            model.ConveyanceObj = new MyConveyanceDetails();
            model.numTotalRows = p;
            model.startRowIndex = startIndex;
            model.maximumRows = rowNumber;
            return model;
        }

        public MyConveyanceModels GetMasterList(string strAuthorID, int recordID, int OutOfOfficeID, string strEmpID, string dtDate, string isApproved, int startIndex, int rowNumber)
        {
            int p;
            MyConveyanceModels model = new MyConveyanceModels();
            model.LstConveyanceMaster = MyConveyanceBLL.GetMasterList(strAuthorID, recordID, OutOfOfficeID, strEmpID, dtDate, isApproved, startIndex, rowNumber, out p);
            model.ConveyanceObj = new MyConveyanceDetails();
            model.numTotalRows = p;
            return model;
        }


        public static MyConveyanceModels GetConveyanceDetails(Int64 recordID, Int64 conveyanceID)
        {
            MyConveyanceModels model = new MyConveyanceModels();
            model.LstConveyanceDetails = MyConveyanceBLL.GetConveyanceDetails(recordID, conveyanceID);
            DesignationBLL bll = new DesignationBLL();
            Designation designation = bll.DesignationGet(LMS.Web.LoginInfo.Current.strDesignationID);
            model.StrDesignation = designation.strDesignation;
            return model;
        }

        public static int ApproveConveyance(int CONVEYANCEID, string voucherNumber, string ApprovedBy, string strMode)
        {
            return ConveyanceBLL.ApproveConveyance(CONVEYANCEID, voucherNumber, ApprovedBy, strMode);
        }

        public List<ConveyanceApproverDetails> GetConveyanceApproverDetails(Int64 CONVEYANCEID)
        {
            return ConveyanceBLL.GetConveyanceApproverDetails(@CONVEYANCEID);
        }

    }
}