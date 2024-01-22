using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS.BLL;
using LMS.DAL;
using LMSEntity;
using MvcContrib.Pagination;
using MvcPaging;

namespace LMS.Web.Models
{
    public class EmployeeWiseApprovalPathModels
    {
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private string _Message;
        private string _strSearchAuthorInitial;
        private string _strSearchAuthorName;
        private string _strSearchInitial;
        private string _strSearchName;
        private int _intSearchPathID;
        private int _intSearchNodeID;
        private string _strSearchDesignationId;
        private string _strSearchDepartmentId;
        private string _strSearchLocationId;

        private SelectList _ApprovalPath;
        private SelectList _InitalNode;
        private SelectList _Designation;
        private SelectList _Department;
        private SelectList _Location;

        private EmployeeWiseApprovalPath _EmployeeWiseApprovalPath;
        private IList<EmployeeWiseApprovalPath> _LstEmployeeWiseApprovalPath;
        private IPagedList<EmployeeWiseApprovalPath> _LstEmployeeWiseApprovalPathPaging;
        private EmployeeWiseApprovalPathBLL objBLL = new EmployeeWiseApprovalPathBLL();

        public int intPathID;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public int PageNumber { set; get; }


        public string strSearchAuthorInitial
        {
            get { return _strSearchAuthorInitial; }
            set { _strSearchAuthorInitial = value; }
        }

        public string strSearchAuthorName
        {
            get { return _strSearchAuthorName; }
            set { _strSearchAuthorName = value; }
        }

        public string strSearchInitial
        {
            get { return _strSearchInitial; }
            set { _strSearchInitial = value; }
        }

        public string strSearchName
        {
            get { return _strSearchName; }
            set { _strSearchName = value; }
        }

        public string strSearchDesignationId
        {
            get { return _strSearchDesignationId; }
            set { _strSearchDesignationId = value; }
        }

        public string strSearchDepartmentId
        {
            get { return _strSearchDepartmentId; }
            set { _strSearchDepartmentId = value; }
        }

        public string strSearchLocationId
        {
            get { return _strSearchLocationId; }
            set { _strSearchLocationId = value; }
        }

        public int intSearchPathID
        {
            get { return _intSearchPathID; }
            set { _intSearchPathID = value; }
        }

        public int intSearchNodeID
        {
            get { return _intSearchNodeID; }
            set { _intSearchNodeID = value; }
        }

        public string strSortBy
        {
            get { return _strSortBy; }
            set { _strSortBy = value; }
        }
        public string strSortType
        {
            get { return _strSortType; }
            set { _strSortType = value; }
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

        public EmployeeWiseApprovalPath EmployeeWiseApprovalPath
        {
            get
            {
                if (_EmployeeWiseApprovalPath == null)
                {
                    _EmployeeWiseApprovalPath = new EmployeeWiseApprovalPath();
                }
                return _EmployeeWiseApprovalPath;
            }
            set { _EmployeeWiseApprovalPath = value; }
        }

        public IList<EmployeeWiseApprovalPath> LstEmployeeWiseApprovalPath
        {
            get { return _LstEmployeeWiseApprovalPath; }
            set { _LstEmployeeWiseApprovalPath = value; }
        }

        public IPagedList<EmployeeWiseApprovalPath> LstEmployeeWiseApprovalPathPaging
        {
            get { return _LstEmployeeWiseApprovalPathPaging; }
            set { _LstEmployeeWiseApprovalPathPaging = value; }
        }

        public int SaveData(EmployeeWiseApprovalPathModels model)
        {
            int i = 0;
            EmployeeWiseApprovalPathBLL objBll = new EmployeeWiseApprovalPathBLL();
            EmployeeWiseApprovalPath objEmpAppPath = new EmployeeWiseApprovalPath();
            try
            {
                model.EmployeeWiseApprovalPath.strEUser = LoginInfo.Current.LoginName;
                model.EmployeeWiseApprovalPath.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.EmployeeWiseApprovalPath.intEmpPathID > 0)
                {
                    i = objBll.Edit(model.EmployeeWiseApprovalPath);
                }
                else
                {
                    model.EmployeeWiseApprovalPath.strIUser = LoginInfo.Current.LoginName;

                    i = objBll.Add(model.EmployeeWiseApprovalPath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public int Delete(int Id)
        {
            int i = 0;
            try
            {
                i = objBLL.Delete(Id);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public SelectList ApprovalPath
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<ApprovalPathMaster> lstApprovalPath = new List<ApprovalPathMaster>();

                lstApprovalPath = Common.fetchApprovalPath();

                foreach (ApprovalPathMaster lt in lstApprovalPath)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.intPathID.ToString();
                    item.Text = lt.strPathName;
                    itemList.Add(item);
                }
                this._ApprovalPath = new SelectList(itemList, "Value", "Text");

                return _ApprovalPath;
            }
            set { _ApprovalPath = value; }
        }

        public SelectList InitalNode
        {
            get
            {
                ApprovalPathBLL objBLL = new ApprovalPathBLL();
                List<SelectListItem> itemList = new List<SelectListItem>();

                if (intSearchPathID > 0)
                {
                    List<ApprovalPathDetails> lstInitialNode = new List<ApprovalPathDetails>();

                    lstInitialNode = objBLL.ApprovalPathDetailsGet(-1, -1, intSearchPathID, LoginInfo.Current.strCompanyID);

                    foreach (ApprovalPathDetails lt in lstInitialNode)
                    {
                        SelectListItem item = new SelectListItem();

                        item.Value = lt.intNodeID.ToString();
                        item.Text = lt.strNodeName;
                        itemList.Add(item);
                    }

                }

                this._InitalNode = new SelectList(itemList, "Value", "Text");


                return _InitalNode;
            }
            set { _InitalNode = value; }
        }

        public SelectList Designation
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Designation> lstDesignation = new List<Designation>();

                lstDesignation = Common.fetchDesignation().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();

                foreach (Designation lt in lstDesignation)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strDesignationID;
                    item.Text = lt.strDesignation;
                    itemList.Add(item);
                }
                this._Designation = new SelectList(itemList, "Value", "Text");
                return _Designation;
            }
            set { _Designation = value; }
        }

        public SelectList Department
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Department> lstDepartment = new List<Department>();

                lstDepartment = Common.fetchDepartment().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();

                foreach (Department lt in lstDepartment)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strDepartmentID;
                    item.Text = lt.strDepartment;
                    itemList.Add(item);
                }
                this._Department = new SelectList(itemList, "Value", "Text");
                return _Department;
            }
            set { _Department = value; }
        }

        public SelectList Location
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Location> lstLocation = new List<Location>();

                lstLocation = Common.fetchLocation().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();

                foreach (Location lt in lstLocation)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strLocationID;
                    item.Text = lt.strLocation;
                    itemList.Add(item);
                }
                this._Location = new SelectList(itemList, "Value", "Text");
                return _Location;
            }
            set { _Location = value; }
        }


        public EmployeeWiseApprovalPath GetEmployeeWiseApprovalPath(EmployeeWiseApprovalPathModels model)
        {
            int total = 0;
            try
            {
                EmployeeWiseApprovalPath objEmpAppPath = new EmployeeWiseApprovalPath();

                model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath = objBLL.EmployeeWiseApprovalPathGet(model.EmployeeWiseApprovalPath.intEmpPathID, model.EmployeeWiseApprovalPath.intPathID, LoginInfo.Current.strCompanyID.ToString(), "", "", -1, "", "", "", "", "", 1, 20, out total).OrderBy(c => c.intParentNodeID).ToList();

                objEmpAppPath = model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath.Where(c => c.intIsSelect == 1).SingleOrDefault();

                if (objEmpAppPath != null)
                {
                    model.EmployeeWiseApprovalPath.intEmpPathID = objEmpAppPath.intEmpPathID;
                    model.EmployeeWiseApprovalPath.strEmpID = objEmpAppPath.strEmpID;
                    model.EmployeeWiseApprovalPath.strEmpInitial = objEmpAppPath.strEmpInitial;
                    model.EmployeeWiseApprovalPath.strEmpName = objEmpAppPath.strEmpName;
                    model.EmployeeWiseApprovalPath.intNodeID = objEmpAppPath.intNodeID;
                    model.EmployeeWiseApprovalPath.strDepartmentID = objEmpAppPath.strDepartmentID;
                    model.EmployeeWiseApprovalPath.strDesignationID = objEmpAppPath.strDesignationID;
                    model.EmployeeWiseApprovalPath.strLocationID = objEmpAppPath.strLocationID;
                    model.EmployeeWiseApprovalPath.strApplyType = objEmpAppPath.strApplyType;
                    model.EmployeeWiseApprovalPath.strCompanyID = objEmpAppPath.strCompanyID;
                }
                else
                {
                    if (model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath != null)
                    {
                        if (model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath.Count > 0)
                        {
                            model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[0].intIsSelect = 1;
                            model.EmployeeWiseApprovalPath.intNodeID = model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[0].intNodeID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model.EmployeeWiseApprovalPath;
        }

        public void GetEmployeeWiseApprovalPathAll(EmployeeWiseApprovalPath objEmpApprovalPath)
        {
            int Total = 0;
            LstEmployeeWiseApprovalPath = objBLL.EmployeeWiseApprovalPathGetAll(LoginInfo.Current.strCompanyID.ToString(), objEmpApprovalPath.strEmpInitial,
                                                                                objEmpApprovalPath.strEmpName, objEmpApprovalPath.intPathID, objEmpApprovalPath.intNodeID,
                                                                                objEmpApprovalPath.strDepartmentID, objEmpApprovalPath.strDesignationID, objEmpApprovalPath.strLocationID,
                                                                                objEmpApprovalPath.strAuthorInitial, objEmpApprovalPath.strAuthorName, strSortBy, strSortType, startRowIndex, maximumRows, out Total);
            numTotalRows = Total;
        }

        public List<ApprovalPathDetails> GetPathwiseNodes(int intpathId)
        {
            ApprovalPathBLL objBLL = new ApprovalPathBLL();
            return objBLL.ApprovalPathDetailsGet(-1, -1, intpathId, LoginInfo.Current.strCompanyID);
        }
    }
}