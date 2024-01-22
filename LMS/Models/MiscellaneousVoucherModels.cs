using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSEntity;
using LMS.BLL;
using MvcContrib.Pagination;
using MvcPaging;
using System.Web.Mvc;

namespace LMS.Web.Models
{
    public class MiscellaneousVoucherModels
    {
        private string strEmpName;
        private string strEmpID;
        private string strDepartment;
        private string strDesignation;
        private string strDate;        
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string isPaid;
        string _message;
        private List<int> iDList;
        private List<MISCDetails> lstMISCDetails;
        private List<MiscellaneousVoucher> lstMiscellaneousVoucher;
        private MiscellaneousVoucher miscellaneousVoucher;
        private IPagedList<MiscellaneousVoucher> _LstMiscellaneousVoucherPaging;
        private List<ConveyanceApproverDetails> lstConveyanceApproverDetails;



        public List<int> IDList
        {
            get { return iDList; }
            set { iDList = value; }
        }


        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string StrEmpID
        {
            get { return strEmpID; }
            set { strEmpID = value; }
        }

        public string IsPaid
        {
            get { return isPaid; }
            set { isPaid = value; }
        }

        public string StrDate
        {
            get { return strDate; }
            set { strDate = value; }
        }

        public string StrEmpName
        {
            get { return strEmpName; }
            set { strEmpName = value; }
        }
        
        public string StrDesignation
        {
            get { return strDesignation; }
            set { strDesignation = value; }
        }
        
        public string StrDepartment
        {
            get { return strDepartment; }
            set { strDepartment = value; }
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

        public IPagedList<MiscellaneousVoucher> LstMiscellaneousVoucherPaging
        {
            get { return _LstMiscellaneousVoucherPaging; }
            set { _LstMiscellaneousVoucherPaging = value; }
        }

        public MiscellaneousVoucher MiscellaneousVoucher
        {
            get { return miscellaneousVoucher; }
            set { miscellaneousVoucher = value; }
        }


        public List<MISCDetails> LstMISCDetails
        {
            get { return lstMISCDetails; }
            set { lstMISCDetails = value; }
        }

        public List<MiscellaneousVoucher> LstMiscellaneousVoucher
        {
            get { return lstMiscellaneousVoucher; }
            set { lstMiscellaneousVoucher = value; }
        }

        public MiscellaneousVoucherModels GetMiscVoucherList(MiscellaneousVoucherModels model)
        {
            int numTotal;
            
            model.LstMiscellaneousVoucher = MiscellaneousVoucherBLL.GetData(model.MiscellaneousVoucher, model.startRowIndex, model.maximumRows, out numTotal);
            model.numTotalRows = numTotal;
            return model;
        }

        public MiscellaneousVoucherModels GetData(MISCDetails searchObj,int startIndex,int rowNum)
        {
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();
            model.LstMISCDetails = MISCBLL.GetDetails(searchObj, startIndex, rowNum);    
            return model;
        }

        public MiscellaneousVoucherModels GetDetailsData(Int64 Id)
        {
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();
            MiscellaneousVoucher voucherObj = new MiscellaneousVoucher();
            
            voucherObj.MISCID = Id;
            int numTotal;
            model.LstMiscellaneousVoucher = MiscellaneousVoucherBLL.GetData(voucherObj,1,10, out numTotal);

            if (model.LstMiscellaneousVoucher.Count > 0)
                model.MiscellaneousVoucher = model.LstMiscellaneousVoucher[0];
            else
                model.MiscellaneousVoucher = new MiscellaneousVoucher();

            MISCDetails searchObj = new MISCDetails();
            searchObj.MISCMASTERID = Id;
            model.LstMISCDetails = MISCBLL.GetDetails(searchObj, 1, 10);


            DesignationBLL bll = new DesignationBLL();
            Designation designation = bll.DesignationGet(LMS.Web.LoginInfo.Current.strDesignationID);
            model.StrDesignation = designation.strDesignation;

            
            return model;
        }


        public Employee GetEmployeeInfo(string Id)
        {
            EmployeeBLL objEmpBLL = new EmployeeBLL();

            try
            {
                return objEmpBLL.EmployeeGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Approve(MiscellaneousVoucherModels model)
        {
           return MiscellaneousVoucherBLL.Update(model.MiscellaneousVoucher);
        }

        public List<ConveyanceApproverDetails> GetConveyanceApproverDetails(Int64 voucherID)
        {
            return MiscellaneousVoucherBLL.GetConveyanceApproverDetails(voucherID);
        }


        public MiscellaneousVoucherModels GetVoucherDetails(int id)
        {
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();
            model = model.GetDetailsData(Int64.Parse(id.ToString()));
            model.LstConveyanceApproverDetails = model.GetConveyanceApproverDetails(Int64.Parse(id.ToString()));
            InitializeModel(model);
            return model;
        }

        public MiscellaneousVoucherModels GetVoucherDetailsList(List<int> ids)
        {
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();
            model.LstConveyanceApproverDetails = new List<ConveyanceApproverDetails>();
            foreach (int item in ids)
            {
                model = model.GetDetailsData(Int64.Parse(item.ToString()));
                
                model.LstConveyanceApproverDetails.AddRange(model.GetConveyanceApproverDetails(Int64.Parse(item.ToString())));
                InitializeModel(model);    
            }
            
            return model;
        }

        private void InitializeModel(MiscellaneousVoucherModels model)
        {


            Employee objEmp = new Employee();

            if (!string.IsNullOrEmpty(model.MiscellaneousVoucher.STREMPID))
            {
                objEmp = model.GetEmployeeInfo(model.MiscellaneousVoucher.STREMPID);

                model.MiscellaneousVoucher.STRDEPARTMENT = objEmp.strDepartment;
                model.MiscellaneousVoucher.STRDESIGNATION = objEmp.strDesignation;


            }

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