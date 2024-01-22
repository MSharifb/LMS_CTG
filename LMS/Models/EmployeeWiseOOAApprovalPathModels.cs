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
using System.Data;

namespace LMS.Web.Models
{
    public class EmployeeWiseOOAApprovalPathModels
    {
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private string _Message;
        private string _strSearchAuthorID;
        private string _strSearchAuthorName;
        private string _strSearchID;
        private string _strSearchName;
        private int _intSearchPathID;
        private int _intSearchNodeID;
        private int _intFlowTypeID;
        private string _strSearchDesignationId;
        private string _strSearchDepartmentId;
        private string _strSearchLocationId;

        private SelectList _OOAApprovalPath;
        private SelectList _InitalNode;
        private SelectList _Designation;
        private SelectList _Department;
        private SelectList _Location;

        private EmployeeWiseOOAApprovalPath _EmployeeWiseOOAApprovalPath;
        private IList<EmployeeWiseOOAApprovalPath> _LstEmployeeWiseOOAApprovalPath;
        private IPagedList<EmployeeWiseOOAApprovalPath> _LstEmployeeWiseOOAApprovalPathPaging;
        private EmployeeWiseOOAApprovalPathBLL objBLL = new EmployeeWiseOOAApprovalPathBLL();

        public int intPathID;


        public int IntFlowTypeID
        {
            get { return _intFlowTypeID; }
            set { _intFlowTypeID = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public int PageNumber { set; get; }


        public string strSearchAuthorID
        {
            get { return _strSearchAuthorID; }
            set { _strSearchAuthorID = value; }
        }

        public string strSearchAuthorName
        {
            get { return _strSearchAuthorName; }
            set { _strSearchAuthorName = value; }
        }

        public string strSearchID
        {
            get { return _strSearchID; }
            set { _strSearchID = value; }
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

        public List<SelectListItem> GetBillType
        {
            get
            {
                List<BillType> lstData = BillTypeDAL.GetBillType();
                List<SelectListItem> lst = new List<SelectListItem>();
                foreach (BillType item in lstData)
                {
                    lst.Add(new SelectListItem { Text = item.TYPENAME, Value = item.TYPEID.ToString() });
                }

                return lst;
            }           
        }

        public List<SelectListItem> OOAFLOWLIST
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                if ((EmployeeWiseOOAApprovalPath.intFlowType != null) && (EmployeeWiseOOAApprovalPath.intFlowType != 0))
                {

                    EmployeeWiseOOAApprovalPathBLL empBll = new EmployeeWiseOOAApprovalPathBLL();
                    DataTable dt = empBll.GetFlowTypeWise(EmployeeWiseOOAApprovalPath.intFlowType);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = dt.Rows[i][0].ToString();
                        item.Text = dt.Rows[i][1].ToString();
                        itemList.Add(item);
                    }
                }
                return itemList;
            }
        }

        public EmployeeWiseOOAApprovalPath EmployeeWiseOOAApprovalPath
        {
            get
            {
                if (_EmployeeWiseOOAApprovalPath == null)
                {
                    _EmployeeWiseOOAApprovalPath = new EmployeeWiseOOAApprovalPath();
                }
                return _EmployeeWiseOOAApprovalPath;
            }
            set { _EmployeeWiseOOAApprovalPath = value; }
        }

        public IList<EmployeeWiseOOAApprovalPath> LstEmployeeWiseOOAApprovalPath
        {
            get { return _LstEmployeeWiseOOAApprovalPath; }
            set { _LstEmployeeWiseOOAApprovalPath = value; }
        }

        public IPagedList<EmployeeWiseOOAApprovalPath> LstEmployeeWiseOOAApprovalPathPaging
        {
            get { return _LstEmployeeWiseOOAApprovalPathPaging; }
            set { _LstEmployeeWiseOOAApprovalPathPaging = value; }
        }

        public int SaveData(EmployeeWiseOOAApprovalPathModels model)
        {
            int i = 0;
            EmployeeWiseOOAApprovalPathBLL objBll = new EmployeeWiseOOAApprovalPathBLL();
            EmployeeWiseOOAApprovalPath objEmpAppPath = new EmployeeWiseOOAApprovalPath();
            try
            {
                model.EmployeeWiseOOAApprovalPath.strEUser = LoginInfo.Current.LoginName;
                model.EmployeeWiseOOAApprovalPath.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.EmployeeWiseOOAApprovalPath.intEmpPathID > 0)
                {
                    i = objBll.Edit(model.EmployeeWiseOOAApprovalPath);
                }
                else
                {
                    model.EmployeeWiseOOAApprovalPath.strIUser = LoginInfo.Current.LoginName;

                    i = objBll.Add(model.EmployeeWiseOOAApprovalPath);
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

        public SelectList OOAApprovalPath
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<OOAApprovalPathMaster> lstOOAApprovalPath = new List<OOAApprovalPathMaster>();

                lstOOAApprovalPath = Common.fetchOOAApprovalPath();

                foreach (OOAApprovalPathMaster lt in lstOOAApprovalPath)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.intPathID.ToString();
                    item.Text = lt.strPathName;
                    itemList.Add(item);
                }
                this._OOAApprovalPath = new SelectList(itemList, "Value", "Text");

                return _OOAApprovalPath;
            }
            set { _OOAApprovalPath = value; }
        }

        public SelectList InitalNode
        {
            get
            {
                OOAApprovalPathBLL objBLL = new OOAApprovalPathBLL();
                List<SelectListItem> itemList = new List<SelectListItem>();

                if (intSearchPathID > 0)
                {
                    List<OOAApprovalPathDetails> lstInitialNode = new List<OOAApprovalPathDetails>();

                    lstInitialNode = objBLL.OOAApprovalPathDetailsGet(-1, -1, intSearchPathID, LoginInfo.Current.strCompanyID);

                    foreach (OOAApprovalPathDetails lt in lstInitialNode)
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


        public EmployeeWiseOOAApprovalPath GetEmployeeWiseOOAApprovalPath(EmployeeWiseOOAApprovalPathModels model)
        {
            int total = 0;
            try
            {
                EmployeeWiseOOAApprovalPath objEmpAppPath = new EmployeeWiseOOAApprovalPath();

                model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath = objBLL.EmployeeWiseOOAApprovalPathGet(model.EmployeeWiseOOAApprovalPath.intFlowType, model.EmployeeWiseOOAApprovalPath.intEmpPathID, model.EmployeeWiseOOAApprovalPath.intPathID, LoginInfo.Current.strCompanyID.ToString(), "", "", -1, "", "", "", "", "", 1, 20, out total).OrderBy(c => c.intParentNodeID).ToList();

                objEmpAppPath = model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath.Where(c => c.intIsSelect == 1).SingleOrDefault();

                if (objEmpAppPath != null)
                {
                    model.EmployeeWiseOOAApprovalPath.intEmpPathID = objEmpAppPath.intEmpPathID;
                    model.EmployeeWiseOOAApprovalPath.strEmpID = objEmpAppPath.strEmpID;
                    model.EmployeeWiseOOAApprovalPath.strEmpName = objEmpAppPath.strEmpName;
                    model.EmployeeWiseOOAApprovalPath.intNodeID = objEmpAppPath.intNodeID;
                    model.EmployeeWiseOOAApprovalPath.strDepartmentID = objEmpAppPath.strDepartmentID;
                    model.EmployeeWiseOOAApprovalPath.strDesignationID = objEmpAppPath.strDesignationID;
                    model.EmployeeWiseOOAApprovalPath.strLocationID = objEmpAppPath.strLocationID;
                    model.EmployeeWiseOOAApprovalPath.strApplyType = objEmpAppPath.strApplyType;
                    model.EmployeeWiseOOAApprovalPath.strCompanyID = objEmpAppPath.strCompanyID;
                }
                else
                {
                    if (model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath != null)
                    {
                        if (model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath.Count > 0)
                        {
                            model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[0].intIsSelect = 1;
                            model.EmployeeWiseOOAApprovalPath.intNodeID = model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[0].intNodeID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model.EmployeeWiseOOAApprovalPath;
        }

        public void GetEmployeeWiseOOAApprovalPathAll(EmployeeWiseOOAApprovalPath objEmpOOAApprovalPath)
        {
            int Total = 0;
            LstEmployeeWiseOOAApprovalPath = objBLL.EmployeeWiseOOAApprovalPathGetAll(LoginInfo.Current.strCompanyID.ToString(), objEmpOOAApprovalPath.strEmpID,
                                                                                objEmpOOAApprovalPath.strEmpName, objEmpOOAApprovalPath.intPathID,objEmpOOAApprovalPath.intFlowType, objEmpOOAApprovalPath.intNodeID,
                                                                                objEmpOOAApprovalPath.strDepartmentID, objEmpOOAApprovalPath.strDesignationID, objEmpOOAApprovalPath.strLocationID,
                                                                                objEmpOOAApprovalPath.strAuthorID, objEmpOOAApprovalPath.strAuthorName, strSortBy, strSortType, startRowIndex, maximumRows, out Total);
            numTotalRows = Total;
        }

        public List<OOAApprovalPathDetails> GetPathwiseNodes(int intpathId)
        {
            OOAApprovalPathBLL objBLL = new OOAApprovalPathBLL();
            return objBLL.OOAApprovalPathDetailsGet(-1, -1, intpathId, LoginInfo.Current.strCompanyID);
        }


    }
}