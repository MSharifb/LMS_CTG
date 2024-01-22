using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.BLL;
using LMSEntity;
using System.Data;
using MvcPaging;
using System.Web.Mvc;
using MvcContrib.Pagination;
using LMS.DAL;

namespace LMS.Web.Models
{
    public class MISCModels
    {
        private MISCBLL objBLL = new MISCBLL();
        IList<MISCDetails> lstMISCDetails;
        private List<MISCDetails> lstMISCSearchDetails;
        IList<MISCMaster> lstMISCMaster;
        private IPagedList<MISCMaster> _LstMISCMasterPaging;
        private MISCMaster _MISCMasterSearch;
        private MISCDetails miscDetails;
        MISCMaster _MISCMaster;
        int _numTotalRows;
        private bool isNew;
        int _startRowIndex;
        int _maximumRows;
        public string _Message;
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }



        public MISCDetails MiscDetails
        {
            get { return miscDetails; }
            set { miscDetails = value; }
        }

        public List<MISCDetails> LstMISCSearchDetails
        {
            get { return lstMISCSearchDetails; }
            set { lstMISCSearchDetails = value; }
        }
        public IList<MISCDetails> LstMISCDetails
        {
            get {
                    if (lstMISCDetails == null)
                    lstMISCDetails = new List<MISCDetails>();
                    
                    return lstMISCDetails; 
                }
            set { lstMISCDetails = value; }
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

        public IPagedList<MISCMaster> LstMISCMasterPaging
        {
            get { return _LstMISCMasterPaging; }
            set { _LstMISCMasterPaging = value; }
        }

        public IList<MISCMaster> LstMISCMaster
        {
            get
            {
                if (lstMISCMaster == null)
                    lstMISCMaster = new List<MISCMaster>();

                return lstMISCMaster;
            }
            set { lstMISCMaster = value; }
        }

        public MISCMaster MISCMasterSearch
        {
            get
            {
                if (this._MISCMasterSearch == null)
                {
                    this._MISCMasterSearch = new MISCMaster();
                }
                return _MISCMasterSearch;
            }
            set { _MISCMasterSearch = value; }
        }

        public MISCMaster MISCMaster
        {
            get
            {
                if (this._MISCMaster == null)
                {
                    this._MISCMaster = new MISCMaster();
                }
                return _MISCMaster;
            }
            set { _MISCMaster = value; }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public static int Save(MISCModels obj, string userID)
        {
            int i = -1;

            i = MISCBLL.Save(userID, obj.MISCMaster, obj.LstMISCDetails, "I");

            return i;
        }

        public static int Update(MISCModels obj)
        {
            int i = -1;

            i = MISCBLL.Save(LMS.Web.LoginInfo.Current.strEmpID, obj.MISCMaster, obj.LstMISCDetails, "U");

            return i;
        }

        //public static int Delete(MISCDetails obj)
        //{
        //    return MISCBLL.Delete(obj);
        //}

        public IList<MISCMaster> GetData(MISCMaster searchObj, int startRowIndex, int maximumRows)
        {
            int P;
            LstMISCMaster = MISCBLL.GetData(searchObj, startRowIndex, maximumRows, out P);
            numTotalRows = P;
            return LstMISCMaster;
        }

        public void GetSearchedData(int Id)
        {
            try
            {
               // MISCMasterSearch = objBLL.GetSearchedData(Id);
                MISCMaster = objBLL.GetSearchedData(Id);
                
                //LstMISCSearchDetails = objBLL.DetailsGet(-1, Id);

                LstMISCDetails = objBLL.DetailsGet(-1, Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<MISCMaster> GetSearchData(MISCMaster searchObj, int startRowIndex, int maximumRows)
        {
            int P;
            LstMISCMaster = MISCBLL.GetSearchData(searchObj, startRowIndex, maximumRows, out P);
            numTotalRows = P;
            return LstMISCMaster;
        }


        public static IList<MISCDetails> GetDetails(MISCDetails searchObj)
        {
            IList<MISCDetails> lst = new List<MISCDetails>();
            lst = MISCBLL.GetDetails(searchObj, 1, 1);
            return lst;
        }

        public  MISCDetails GetDetailByID(string id)
        {
            IList<MISCDetails> lst = new List<MISCDetails>();
            MISCDetails searchObj = new MISCDetails();
            searchObj.MISCDETAISLID = long.Parse(id);
            lst = MISCBLL.GetDetails(searchObj, 1, 1);
            MiscDetails = lst[0];
            return MiscDetails;
        }

        public  IList<MISCDetails> GetDetails(int MISCMASTERID)
        {
            IList<MISCDetails> lst = new List<MISCDetails>();
            lst = MISCBLL.GetDetails(MISCMASTERID, 1, 1);
            return lst;
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