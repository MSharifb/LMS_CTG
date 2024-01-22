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
    public class ConveyanceModels
    {
        private List<ConveyanceMaster> lstConveyanceMaster;
        private List<ConveyanceDetails> lstConveyanceDetails;
        private IPagedList<ConveyanceMaster> _LstConveyanceMasterPaging;
        private List<ConveyanceApproverDetails> lstConveyanceApproverDetails;

               
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string strDesignation;
        string _isPaid;
        private ConveyanceDetails conveyanceObj;


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

        public ConveyanceDetails ConveyanceObj
        {
            get { return conveyanceObj; }
            set { conveyanceObj = value; }
        }
        public List<ConveyanceMaster> LstConveyanceMaster
        {
            get { return lstConveyanceMaster; }
            set { lstConveyanceMaster = value; }
        }

        public List<ConveyanceDetails> LstConveyanceDetails
        {
            get { return lstConveyanceDetails; }
            set { lstConveyanceDetails = value; }
        }


        public IPagedList<ConveyanceMaster> LstConveyanceMasterPaging
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

        public  ConveyanceModels GetMasterList(string strAuthorID, int recordID, int OutOfOfficeID, string strEmpID, string dtDate, string isApproved,int startIndex,int rowNumber)
        {
            int p;
            ConveyanceModels model = new ConveyanceModels();
            model.LstConveyanceMaster = ConveyanceBLL.GetMasterList(strAuthorID, recordID, OutOfOfficeID, strEmpID, dtDate, isApproved, startIndex, rowNumber, out p);
            model.ConveyanceObj = new ConveyanceDetails();
            model.numTotalRows = p;
            return model;
        }

        public static ConveyanceModels GetConveyanceDetails(Int64 recordID, Int64 conveyanceID)
        {
            ConveyanceModels model = new ConveyanceModels();
            model.LstConveyanceDetails= ConveyanceBLL.GetConveyanceDetails(recordID, conveyanceID);
            DesignationBLL bll = new DesignationBLL();
            Designation designation = bll.DesignationGet(LMS.Web.LoginInfo.Current.strDesignationID);
            model.StrDesignation = designation.strDesignation;
            return model;
        }

        public static int ApproveConveyance(int CONVEYANCEID, string voucherNumber,string ApprovedBy, string strMode)
        {
            return ConveyanceBLL.ApproveConveyance(CONVEYANCEID,voucherNumber, ApprovedBy, strMode);
        }

        public List<ConveyanceApproverDetails> GetConveyanceApproverDetails(Int64 CONVEYANCEID)
        {
            return ConveyanceBLL.GetConveyanceApproverDetails(@CONVEYANCEID);
        }


    }
}