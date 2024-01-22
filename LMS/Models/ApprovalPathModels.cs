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
    public class ApprovalPathModels
    {

        private bool _IsModeEdit;
        private bool _blnTextBlank;
        private string _strSearchName;

        private SelectList _ApprovalGroup;
        private SelectList _AuthorType;
        private SelectList _ParentNode;
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private ApprovalAuthor _ApprovalAuthor;
        private ApprovalPathMaster _ApprovalPathMaster;
        private ApprovalPathDetails _ApprovalPathDetails;

        private IList<ApprovalPathMaster> lstApprovalPathMaster;
        private List<ApprovalPathDetails> lstApprovalPathDetails;
        private IPagedList<ApprovalPathMaster> lstApprovalPathMaster1;
        private List<ApprovalAuthor> lstApprovalAuthor;
        private List<ApprovalGroup> lstApprovalGroup;

        private ApprovalPathBLL objBLL = new ApprovalPathBLL();
        private ApprovalAuthorBLL objAppAoutBLL = new ApprovalAuthorBLL();

        public bool IsModeEdit
        {
            get { return _IsModeEdit; }
            set { _IsModeEdit = value; }
        }
        public bool BlnTextBlank
        {
            get { return _blnTextBlank; }
            set { _blnTextBlank = value; }
        }
        public string strSearchName
        {
            get { return _strSearchName; }
            set { _strSearchName = value; }
        }
        public string _Message;
        public int _DeleteNodeId;

        public SelectList _Node;
        public SelectList _AuthType;
        public ApprovalAuthor ApprovalAuthor
        {
            get
            {
                if (this._ApprovalAuthor == null)
                {
                    this._ApprovalAuthor = new ApprovalAuthor();
                }
                return _ApprovalAuthor;
            }
            set { _ApprovalAuthor = value; }
        }

        public ApprovalPathMaster ApprovalPathMaster
        {
            get
            {
                if (this._ApprovalPathMaster == null)
                {
                    this._ApprovalPathMaster = new ApprovalPathMaster();
                }
                return _ApprovalPathMaster;
            }
            set { _ApprovalPathMaster = value; }
        }
        public ApprovalPathDetails ApprovalPathDetails
        {
            get
            {
                if (this._ApprovalPathDetails == null)
                {
                    this._ApprovalPathDetails = new ApprovalPathDetails();
                }
                return _ApprovalPathDetails;
            }
            set { _ApprovalPathDetails = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public int DeleteNodeId
        {
            get { return _DeleteNodeId; }
            set { _DeleteNodeId = value; }
        }

        public SelectList Node
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<AuthorType> lstAuthorType = new List<AuthorType>();
                List<ApprovalPathDetails> lst = objBLL.ApprovalPathDetailsGet(-1, -1, ApprovalPathMaster.intPathID, LoginInfo.Current.strCompanyID);

                foreach (ApprovalPathDetails lt in lst)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intNodeID.ToString();
                    item.Text = lt.strNodeName;
                    itemList.Add(item);
                }
                this._Node = new SelectList(itemList, "Value", "Text");
                return _Node;
            }
            set { _Node = value; }
        }

        public SelectList AuthorType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<AuthorType> lstAuthorType = new List<AuthorType>();
                lstAuthorType = Common.fetchAuthorType();

                foreach (AuthorType lt in lstAuthorType)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intAuthorTypeID.ToString();
                    item.Text = lt.strAuthorType;
                    itemList.Add(item);
                }
                this._AuthorType = new SelectList(itemList, "Value", "Text");
                return _AuthorType;
            }
            set { _AuthorType = value; }
        }

        public SelectList AuthType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                SelectListItem item = new SelectListItem();
                item.Value = "Main";
                item.Text = "Main";
                itemList.Add(item);
                item = new SelectListItem();
                item.Value = "Alternate";
                item.Text = "Alternate";
                itemList.Add(item);

                this._AuthType = new SelectList(itemList, "Value", "Text");
                return _AuthType;
            }
            set { _AuthorType = value; }
        }

        public SelectList ParentNode
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                if (LstApprovalPathDetails != null)

                    foreach (ApprovalPathDetails lt in LstApprovalPathDetails)
                    {
                        SelectListItem item = new SelectListItem();
                        item.Text = lt.strNodeName == null ? "" : lt.strNodeName.ToString();
                        item.Value = (lt.intNodeID).ToString();
                        itemList.Add(item);
                    }
                else
                {

                }
                this._ParentNode = new SelectList(itemList, "Value", "Text");
                return _ParentNode;
            }
            set { _ParentNode = value; }
        }

        public IList<ApprovalPathMaster> LstApprovalPathMaster
        {
            get { return lstApprovalPathMaster; }
            set { lstApprovalPathMaster = value; }
        }
        public List<ApprovalPathDetails> LstApprovalPathDetails
        {
            get { return lstApprovalPathDetails; }
            set { lstApprovalPathDetails = value; }
        }
        public List<ApprovalAuthor> LstApprovalAuthor
        {
            get { return lstApprovalAuthor; }
            set { lstApprovalAuthor = value; }
        }

        public IPagedList<ApprovalPathMaster> LstApprovalPathMaster1
        {
            get { return lstApprovalPathMaster1; }
            set { lstApprovalPathMaster1 = value; }
        }


        /// <summary>
        /// Save
        /// </summary>
        public int SaveData(ApprovalPathModels model)
        {
            int i = 0;
            ApprovalPathBLL objBll = new ApprovalPathBLL();
            try
            {

                model.ApprovalPathMaster.strEUser = LoginInfo.Current.LoginName;
                if (model.ApprovalPathMaster.intPathID > 0)
                {
                    i = objBll.Edit(model.ApprovalPathMaster);
                }
                else
                {
                    model.ApprovalPathMaster.strIUser = LoginInfo.Current.LoginName;
                    model.ApprovalPathMaster.strCompanyID = LoginInfo.Current.strCompanyID;

                    i = objBll.Add(model.ApprovalPathMaster, LstApprovalPathDetails);

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public int AddNode(ApprovalPathModels model)
        {
            int i = 0;
            ApprovalPathBLL objBll = new ApprovalPathBLL();
            try
            {
                // TODO: Add insert logic here
                model.ApprovalPathMaster.strEUser = LoginInfo.Current.LoginName;
                model.ApprovalPathMaster.strIUser = LoginInfo.Current.LoginName;
                model.ApprovalPathMaster.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.ApprovalPathMaster.intPathID <= 0)
                {
                    ApprovalPathDetails = objBll.SavePath(model.ApprovalPathMaster, model.ApprovalPathDetails);
                    i = model.ApprovalPathMaster.intPathID = ApprovalPathDetails.intPathID;
                }
                else
                {
                    model.ApprovalPathDetails.intPathID = model.ApprovalPathMaster.intPathID;
                    model.ApprovalPathDetails.strCompanyID = LoginInfo.Current.strCompanyID;
                    model.ApprovalPathDetails.strEUser = LoginInfo.Current.LoginName;
                    model.ApprovalPathDetails.strIUser = LoginInfo.Current.LoginName;
                    i = objBll.SavePath(model.ApprovalPathDetails, true);

                    model.ApprovalPathDetails.intNodeID = i;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public int DeleteNode(int NodeId)
        {
            int i = 0;
            ApprovalPathBLL objBll = new ApprovalPathBLL();
            try
            {
                i = objBll.DeleteNode(NodeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public int deleteAuthor(int NodeId)
        {
            int i = 0;
            ApprovalAuthorBLL objBll = new ApprovalAuthorBLL();
            try
            {
                i = objBll.Delete(NodeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public int CheckPathIdWiseAuthorExists(ApprovalPathModels model)
        {
            return objAppAoutBLL.GetPathIdWiseAuthorExists(model.ApprovalPathMaster.intPathID, model.ApprovalAuthor.strAuthorID, LoginInfo.Current.strCompanyID);
        }

        public List<ApprovalAuthor> GetApproverAuth(int NodeId)
        {
            List<ApprovalAuthor> lst = new List<ApprovalAuthor>();
            ApprovalAuthorBLL objBll = new ApprovalAuthorBLL();
            if (NodeId > 0)
                lst = objBll.ApprovalAuthorGet(-1, NodeId, "", "", LoginInfo.Current.strCompanyID);
            return lst;

        }

        public int SetApprover(ApprovalPathModels model)
        {
            int i = 0;
            ApprovalAuthor objAut = new ApprovalAuthor();
            ApprovalAuthorBLL objBll = new ApprovalAuthorBLL();
            try
            {
                objAut.intPathID = model.ApprovalPathMaster.intPathID;
                objAut.intNodeID = model.ApprovalAuthor.intNodeID;

                if (model.LstApprovalAuthor != null)
                {
                    for (int j = 0; j < model.LstApprovalAuthor.Count; j++)
                    {

                        model.LstApprovalAuthor[j].strCompanyID = LoginInfo.Current.strCompanyID;
                        model.LstApprovalAuthor[j].strIUser = LoginInfo.Current.LoginName;
                        model.LstApprovalAuthor[j].strEUser = LoginInfo.Current.LoginName;
                    }
                }
                i = objBll.SetApprover(model.LstApprovalAuthor, objAut);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return i;
        }

        /// <summary>
        /// Save
        /// </summary>
        public int Delete(int Id)
        {
            int i = 0;
            try
            {
                // TODO: Add insert logic here
                i = objBLL.Delete(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public void GetApprovalPathDetails(int Id)
        {
            try
            {
                ApprovalPathDetails = objBLL.ApprovalPathDetailsGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetApprovverAuthorDetails(int Id)
        {

            ApprovalAuthorBLL objApprovalAuthorBLL = new ApprovalAuthorBLL();
            try
            {
                ApprovalAuthor = objApprovalAuthorBLL.ApprovalAuthorGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetApprover(int Id)
        {
            ApprovalPathModels model = new ApprovalPathModels();
            ApprovalAuthorBLL objApprovalAuthorBLL = new ApprovalAuthorBLL();

            try
            {
                model.LstApprovalAuthor = objApprovalAuthorBLL.ApprovalAuthorGet(-1, Id, "", "", LoginInfo.Current.strCompanyID);
                LstApprovalPathDetails = objBLL.ApprovalPathDetailsGet(-1, -1, Id, LoginInfo.Current.strCompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApprovalPathMaster GetApprovalPathMaster(int Id)
        {
            ApprovalPathModels model = new ApprovalPathModels();
            try
            {
                return objBLL.ApprovalPathMasterGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetApprovalPath(int Id)
        {
            try
            {
                ApprovalPathMaster = objBLL.ApprovalPathMasterGet(Id);
                LstApprovalPathDetails = objBLL.ApprovalPathDetailsGet(-1, -1, Id, LoginInfo.Current.strCompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetApprovalPathMasterAll(string pathname)
        {
            LstApprovalPathMaster = objBLL.ApprovalPathMasterGet(-1, pathname, LoginInfo.Current.strCompanyID);
        }

        public List<ApprovalPathMaster> GetApprovalPathMasterPaging(ApprovalPathMaster objApprovalPathMaster)
        {
            return objBLL.ApprovalPathMasterGet(objApprovalPathMaster.intPathID, objApprovalPathMaster.strPathName, LoginInfo.Current.strCompanyID);
        }

        public List<ApprovalGroup> LstApprovalGroup
        {
            get
            {
                if (lstApprovalGroup == null)
                {
                    lstApprovalGroup = new List<ApprovalGroup>();
                }
                return lstApprovalGroup;
            }
            set { lstApprovalGroup = value; }
        }

        public SelectList AprovalGroupType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                LstApprovalGroup = Common.fetchApprovalGroup();

                foreach (ApprovalGroup lt in lstApprovalGroup)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intApprovalGroupId.ToString();
                    item.Text = lt.ApprovalGroupName;
                    itemList.Add(item);
                }
                this._ApprovalGroup = new SelectList(itemList, "Value", "Text");
                return _ApprovalGroup;
            }
            set { _ApprovalGroup = value; }
        }

        private List<ApprovalProcess> lstApprovalProcess;
        public List<ApprovalProcess> LstApprovalProcess
        {
            get
            {
                if (lstApprovalProcess == null)
                {
                    lstApprovalProcess = new List<ApprovalProcess>();
                }
                return lstApprovalProcess;
            }
            set { lstApprovalProcess = value; }
        }

        private SelectList _ApprovalProcessList;
        public SelectList ApprovalProcessList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                LstApprovalProcess = Common.fetchApprovalProcess();

                foreach (ApprovalProcess lt in lstApprovalProcess)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.intApprovalProcessId.ToString();
                    item.Text = lt.strApprovalProcessName;
                    itemList.Add(item);
                }
                this._ApprovalProcessList = new SelectList(itemList, "Value", "Text");
                return _ApprovalProcessList;
            }

            set { _ApprovalProcessList = value; }
        }
    }
}