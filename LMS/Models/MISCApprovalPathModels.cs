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
    public class MISCApprovalPathModels
    {
        private MISCMaster miscMaster;
        private List<MISCDetails> lstMISCDetails;
        private List<MISCApproval> lstMISCApproval;
        private IPagedList<MISCApproval> _lstMISCApprovalPaging;
        


        string _approvalStatus;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string employeeName;
        string strEmpID;
        string strDate;
        string message;

        public string ApprovalStatus
        {
            get { return _approvalStatus; }
            set { _approvalStatus = value; }

        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }


        public string StrDate
        {
            get { return strDate; }
            set { strDate = value; }
        }

        public string StrEmpID
        {
            get { return strEmpID; }
            set { strEmpID = value; }
        }

        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
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

        public MISCMaster MISCMaster
        {
            get { return miscMaster; }
            set { miscMaster = value; }
        }


        public List<MISCDetails> LstMISCDetails
        {
            get { return lstMISCDetails; }
            set { lstMISCDetails = value; }
        }

        public List<MISCApproval> LstMISCApproval
        {
            get { return lstMISCApproval; }
            set { lstMISCApproval = value; }
        }

        public IPagedList<MISCApproval> LstMISCApprovalPaging
        {
            get { return _lstMISCApprovalPaging; }
            set { _lstMISCApprovalPaging = value; }
        }

        public List<MISCApproval> GetData(string strAuthorID,string strEmpID, Int64 miscMasterID, Int64 intAppFlowID, string miscDate, int StartIndex, int RowNumber)
        {            
            int totalRows;
            LstMISCApproval = MISCApprovalBLL.GetData(strAuthorID, strEmpID,miscMasterID, intAppFlowID, miscDate, StartIndex, RowNumber, out totalRows);
            numTotalRows = totalRows;
            return LstMISCApproval;
        }
              

        public List<MISCDetails> GetDetails(int MISCMASTERID)
        {
            List<MISCDetails> lst = new List<MISCDetails>();
            MISCBLL bll = new MISCBLL();
            lst = bll.DetailsGet(-1, MISCMASTERID);
            
            return lst;
        }

        public MISCMaster GetMasterData(MISCMaster searchObj)
        {
            int P;
            MISCMaster retMiscObj = new LMSEntity.MISCMaster();
            List<MISCMaster> LstMISCMaster = new List<LMSEntity.MISCMaster>();
            LstMISCMaster = MISCBLL.GetData(searchObj, 1,10, out P);
            numTotalRows = P;

            if (LstMISCMaster != null)
                retMiscObj = LstMISCMaster[0];
            return retMiscObj;
        }

        public string GetApprovalStatus(int MiscMasterID)
        {
            return MISCApprovalBLL.GetApprovalStatus(MiscMasterID);
        }

        public int Recommend(Int64 MiscMasterID, string userID)
        {
          return  MISCApprovalBLL.Recommend(MiscMasterID, userID);
        }

        public int Reverify(Int64 MiscMasterID, string userID)
        {
            return MISCApprovalBLL.Reverify(MiscMasterID, userID);
        }

        public int Approve(Int64 MiscMasterID, string userID)
        {
            return MISCApprovalBLL.Approve(MiscMasterID, userID);
        }


        public  int Update(MISCModels obj)
        {
            int i = -1;

            i = MISCBLL.UpdateDetails(obj.LstMISCDetails,obj.MISCMaster.MISCMASTERID);
            
            return i;
        }

        public List<SelectListItem> GetCompanyUnit
        {
            get
            {
                List<CompanyUnit> lst = new List<CompanyUnit>();
                lst = CompanyUnitBLL.GetList(-1, int.Parse(LoginInfo.Current.strCompanyID));

                List<SelectListItem> itemList = new List<SelectListItem>();

                foreach (CompanyUnit item in lst)
                {
                    itemList.Add(new SelectListItem { Text = item.UNITNAME, Value = item.UNITID.ToString() });
                }

                return itemList;
            }
        }

    }
}