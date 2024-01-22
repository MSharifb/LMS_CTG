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
    public class OOAApprovalPathModels
    {

        private bool _IsModeEdit;
        private bool _blnTextBlank;
        private string _strSearchName;

        private SelectList _AuthorType;
        private SelectList _ParentNode;
        string _strSortBy;
        string _strSortType;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;

        private OOAApprovalAuthor _OOAApprovalAuthor;
        private OOAApprovalPathMaster _OOAApprovalPathMaster;
        private OOAApprovalPathDetails _OOAApprovalPathDetails;

        private IList<OOAApprovalPathMaster> lstOOAApprovalPathMaster;
        private List<OOAApprovalPathDetails> lstOOAApprovalPathDetails;
        private IPagedList<OOAApprovalPathMaster> lstOOAApprovalPathMaster1;
        private List<OOAApprovalAuthor> lstOOAApprovalAuthor;
        private List<SelectListItem> lstBillType;

        private OOAApprovalPathBLL objBLL = new OOAApprovalPathBLL();
        private OOAApprovalAuthorBLL objAppAoutBLL = new OOAApprovalAuthorBLL();

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
        public OOAApprovalAuthor OOAApprovalAuthor
        {
            get
            {
                if (this._OOAApprovalAuthor == null)
                {
                    this._OOAApprovalAuthor = new OOAApprovalAuthor();
                }
                return _OOAApprovalAuthor;
            }
            set { _OOAApprovalAuthor = value; }
        }

        public List<SelectListItem> GetBillType
        {
            get {
                List<BillType> lstData = BillTypeDAL.GetBillType();
                List<SelectListItem> lst = new List<SelectListItem>();
                foreach (BillType item in lstData)
                {
                    lst.Add(new SelectListItem { Text=item.TYPENAME,Value=item.TYPEID.ToString()});
                }

                return lst;
            }
            set { lstBillType = value; }
        }

        public OOAApprovalPathMaster OOAApprovalPathMaster
        {
            get
            {
                if (this._OOAApprovalPathMaster == null)
                {
                    this._OOAApprovalPathMaster = new OOAApprovalPathMaster();
                }
                return _OOAApprovalPathMaster;
            }
            set { _OOAApprovalPathMaster = value; }
        }
        public OOAApprovalPathDetails OOAApprovalPathDetails
        {
            get
            {
                if (this._OOAApprovalPathDetails == null)
                {
                    this._OOAApprovalPathDetails = new OOAApprovalPathDetails();
                }
                return _OOAApprovalPathDetails;
            }
            set { _OOAApprovalPathDetails = value; }
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
                List<OOAApprovalPathDetails> lst = objBLL.OOAApprovalPathDetailsGet(-1, -1, OOAApprovalPathMaster.intPathID, LoginInfo.Current.strCompanyID);

                foreach (OOAApprovalPathDetails lt in lst)
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

        public SelectList OOAAuthorType
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<AuthorType> lstAuthorType = new List<AuthorType>();
                lstAuthorType = Common.fetchOOAAuthorType();

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
                if (LstOOAApprovalPathDetails != null)

                    foreach (OOAApprovalPathDetails lt in LstOOAApprovalPathDetails)
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

        public IList<OOAApprovalPathMaster> LstOOAApprovalPathMaster
        {
            get { return lstOOAApprovalPathMaster; }
            set { lstOOAApprovalPathMaster = value; }
        }
        public List<OOAApprovalPathDetails> LstOOAApprovalPathDetails
        {
            get { return lstOOAApprovalPathDetails; }
            set { lstOOAApprovalPathDetails = value; }
        }
        public List<OOAApprovalAuthor> LstOOAApprovalAuthor
        {
            get { return lstOOAApprovalAuthor; }
            set { lstOOAApprovalAuthor = value; }
        }

        public IPagedList<OOAApprovalPathMaster> LstOOAApprovalPathMaster1
        {
            get { return lstOOAApprovalPathMaster1; }
            set { lstOOAApprovalPathMaster1 = value; }
        }


        /// <summary>
        /// Save
        /// </summary>
        /// 

        public int SaveData(OOAApprovalPathModels model)
        {
            int i = 0;
            OOAApprovalPathBLL objBll = new OOAApprovalPathBLL();
            try
            {

                model.OOAApprovalPathMaster.strEUser = LoginInfo.Current.LoginName;
                if (model.OOAApprovalPathMaster.intPathID > 0)
                {
                    i = objBll.Edit(model.OOAApprovalPathMaster);
                    foreach (OOAApprovalPathDetails item in model.LstOOAApprovalPathDetails)
                    {
                       i = OOAApprovalPathDetailsDAL.SaveItem(item, "U");
                        
                    }
                    
                }
                else
                {
                    model.OOAApprovalPathMaster.strIUser = LoginInfo.Current.LoginName;
                    model.OOAApprovalPathMaster.strCompanyID = LoginInfo.Current.strCompanyID;

                    i = objBll.Add(model.OOAApprovalPathMaster, LstOOAApprovalPathDetails);

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }
        public int AddNode(OOAApprovalPathModels model)
        {
            int i = 0;
            OOAApprovalPathBLL objBll = new OOAApprovalPathBLL();
            try
            {
                // TODO: Add insert logic here
                model.OOAApprovalPathMaster.strEUser = LoginInfo.Current.LoginName;
                model.OOAApprovalPathMaster.strIUser = LoginInfo.Current.LoginName;
                model.OOAApprovalPathMaster.strCompanyID = LoginInfo.Current.strCompanyID;

                if (model.OOAApprovalPathMaster.intPathID <= 0)
                {
                    OOAApprovalPathDetails = objBll.SavePath(model.OOAApprovalPathMaster, model.OOAApprovalPathDetails);
                    i = model.OOAApprovalPathMaster.intPathID = OOAApprovalPathDetails.intPathID;
                }
                else
                {
                    model.OOAApprovalPathDetails.intPathID = model.OOAApprovalPathMaster.intPathID;
                    model.OOAApprovalPathDetails.strCompanyID = LoginInfo.Current.strCompanyID;
                    model.OOAApprovalPathDetails.strEUser = LoginInfo.Current.LoginName;
                    model.OOAApprovalPathDetails.strIUser = LoginInfo.Current.LoginName;
                    i = objBll.SavePath(model.OOAApprovalPathDetails, true);

                    model.OOAApprovalPathDetails.intNodeID = i;
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
            OOAApprovalPathBLL objBll = new OOAApprovalPathBLL();
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
            OOAApprovalAuthorBLL objBll = new OOAApprovalAuthorBLL();
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
        public int CheckPathIdWiseAuthorExists(OOAApprovalPathModels model)
        {
            return objAppAoutBLL.GetPathIdWiseAuthorExists(model.OOAApprovalPathMaster.intPathID, model.OOAApprovalAuthor.strAuthorID, LoginInfo.Current.strCompanyID);
        }

        public List<OOAApprovalAuthor> GetOOAApproverAuth(int NodeId)
        {
            List<OOAApprovalAuthor> lst = new List<OOAApprovalAuthor>();
            OOAApprovalAuthorBLL objBll = new OOAApprovalAuthorBLL();
            if (NodeId > 0)
                lst = objBll.OOAApprovalAuthorGet(-1, NodeId, "", "", LoginInfo.Current.strCompanyID);
            return lst;

        }

        public int SetApprover(OOAApprovalPathModels model)
        {
            int i = 0;
            OOAApprovalAuthor objAut = new OOAApprovalAuthor();
            OOAApprovalAuthorBLL objBll = new OOAApprovalAuthorBLL();
            try
            {
                objAut.intPathID = model.OOAApprovalPathMaster.intPathID;
                objAut.intNodeID = model.OOAApprovalAuthor.intNodeID;

                if (model.LstOOAApprovalAuthor != null)
                {
                    for (int j = 0; j < model.LstOOAApprovalAuthor.Count; j++)
                    {

                        model.LstOOAApprovalAuthor[j].strCompanyID = LoginInfo.Current.strCompanyID;
                        model.LstOOAApprovalAuthor[j].strIUser = LoginInfo.Current.LoginName;
                        model.LstOOAApprovalAuthor[j].strEUser = LoginInfo.Current.LoginName;
                    }
                }
                i = objBll.SetApprover(model.LstOOAApprovalAuthor, objAut);

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
        /// 

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

        public void GetOOAApprovalPathDetails(int Id)
        {
            try
            {
                OOAApprovalPathDetails = objBLL.OOAApprovalPathDetailsGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetOOAApprovverAuthorDetails(int Id)
        {

            OOAApprovalAuthorBLL objOOAApprovalAuthorBLL = new OOAApprovalAuthorBLL();
            try
            {
                OOAApprovalAuthor = objOOAApprovalAuthorBLL.OOAApprovalAuthorGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetOOAApprover(int Id)
        {
            OOAApprovalPathModels model = new OOAApprovalPathModels();
            OOAApprovalAuthorBLL objOOAApprovalAuthorBLL = new OOAApprovalAuthorBLL();

            try
            {
                model.LstOOAApprovalAuthor = objOOAApprovalAuthorBLL.OOAApprovalAuthorGet(-1, Id, "", "", LoginInfo.Current.strCompanyID);
                LstOOAApprovalPathDetails = objBLL.OOAApprovalPathDetailsGet(-1, -1, Id, LoginInfo.Current.strCompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OOAApprovalPathMaster GetOOAApprovalPathMaster(int Id)
        {
            OOAApprovalPathModels model = new OOAApprovalPathModels();
            try
            {
                return objBLL.OOAApprovalPathMasterGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetOOAApprovalPath(int Id)
        {
            try
            {
                OOAApprovalPathMaster = objBLL.OOAApprovalPathMasterGet(Id);
                LstOOAApprovalPathDetails = objBLL.OOAApprovalPathDetailsGet(-1, -1, Id, LoginInfo.Current.strCompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetOOAApprovalPathMasterAll(string pathname)
        {
            LstOOAApprovalPathMaster = objBLL.OOAApprovalPathMasterGet(-1, pathname, LoginInfo.Current.strCompanyID);
        }

        public List<OOAApprovalPathMaster> GetOOAApprovalPathMasterPaging(OOAApprovalPathMaster objOOAApprovalPathMaster)
        {
            return objBLL.OOAApprovalPathMasterGet(objOOAApprovalPathMaster.intPathID, objOOAApprovalPathMaster.strPathName, LoginInfo.Current.strCompanyID);
        }
    }
}