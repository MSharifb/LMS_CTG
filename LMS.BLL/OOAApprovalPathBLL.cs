using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using LMS.DAL;
using LMSEntity;
using System.Data;
using TVL.DB;

namespace LMSEntity
{
    public class OOAApprovalPathBLL
    {
        private List<OOAApprovalPathMaster> _OOAApprovalPathMasterList;
        public List<OOAApprovalPathMaster> OOAApprovalPathMasterList
        {
            get
            {
                if (_OOAApprovalPathMasterList == null)
                {
                    _OOAApprovalPathMasterList = new List<OOAApprovalPathMaster>();
                }
                return _OOAApprovalPathMasterList;
            }
            set { _OOAApprovalPathMasterList = value; }
        }

        public OOAApprovalPathDetails SavePath(OOAApprovalPathMaster objOOAApprovalPathMaster, OOAApprovalPathDetails obj)
        {
            int i = 0;
            int intMasterId = 0;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {
                i = intMasterId = OOAApprovalPathMasterDAL.SaveItem(objOOAApprovalPathMaster, "I", transection, con);

                obj.intPathID = intMasterId;

                if (i > 0)
                {
                    obj.strCompanyID = objOOAApprovalPathMaster.strCompanyID;
                    obj.strEUser = objOOAApprovalPathMaster.strEUser;
                    obj.strIUser = objOOAApprovalPathMaster.strIUser;

                    i = OOAApprovalPathDetailsDAL.SaveItem(obj, "I", transection, con);
                    obj.intNodeID = i;
                }
            }
            catch (Exception ex)
            {
                transection.Rollback();
            }

            finally
            {
                if (i < 0)
                {
                    transection.Rollback();
                }
                else
                {
                    transection.Commit();
                }
            }

            return obj;
        }


        public int SavePath(OOAApprovalPathDetails obj, bool IsInsert)
        {
            int i = -1;
            int intMasterId = -1;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {
                if (IsInsert == true)
                {
                    i = OOAApprovalPathDetailsDAL.SaveItem(obj, "I");
                }

                else
                {
                    i = OOAApprovalPathDetailsDAL.SaveItem(obj, "D");

                }
            }
            catch (Exception ex)
            {

                transection.Rollback();
            }

            finally
            {
                if (i < 0)
                    transection.Rollback();

                transection.Commit();
            }

            return i;
        }

        public int Add(OOAApprovalPathMaster objOOAApprovalPathMaster, List<OOAApprovalPathDetails> lst)
        {
            int i = -1;
            int intMasterId = -1;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {
                i = intMasterId = OOAApprovalPathMasterDAL.SaveItem(objOOAApprovalPathMaster, "I", transection, con);

                foreach (OOAApprovalPathDetails obj in lst)
                {

                    obj.intPathID = intMasterId;

                    obj.strCompanyID = objOOAApprovalPathMaster.strCompanyID;
                    obj.strEUser = objOOAApprovalPathMaster.strEUser;
                    obj.strIUser = objOOAApprovalPathMaster.strIUser;
                    i = OOAApprovalPathDetailsDAL.SaveItem(obj, "I", transection, con);

                }

            }
            catch (Exception ex)
            {
                transection.Rollback();
            }

            finally
            {
                if (i < 0)
                    transection.Rollback();

                transection.Commit();
            }

            return i;
        }

        public int Edit(OOAApprovalPathMaster objOOAApprovalPathMaster)
        {
            return OOAApprovalPathMasterDAL.SaveItem(objOOAApprovalPathMaster, "U", null, null);
        }
        public int Delete(int Id)
        {
            OOAApprovalPathMaster obj = new OOAApprovalPathMaster();
            obj.intPathID = Id;

            return OOAApprovalPathMasterDAL.SaveItem(obj, "D", null, null);
        }
        public int DeleteNode(int Id)
        {
            OOAApprovalPathDetails obj = new OOAApprovalPathDetails();
            obj.intNodeID = Id;

            return OOAApprovalPathDetailsDAL.SaveItem(obj, "D");
        }
        public OOAApprovalPathMaster OOAApprovalPathMasterGet(int intPathID)
        {
            return OOAApprovalPathMasterDAL.GetItemList(intPathID, "", "").Single();
        }
        public List<OOAApprovalPathMaster> OOAApprovalPathMasterGet(int intPathID, string strPathName, string strCompanyID)
        {
            return OOAApprovalPathMasterDAL.GetItemList(intPathID, strPathName, strCompanyID);
        }

        public List<OOAApprovalPathDetails> OOAApprovalPathDetailsGet(int intNodeID, int intParentNodeID, int intPathID, string strCompanyID)
        {
            return OOAApprovalPathDetailsDAL.GetItemList(intNodeID, intParentNodeID, intPathID, strCompanyID);
        }

        public OOAApprovalPathDetails OOAApprovalPathDetailsGet(int intNodeID)
        {
            return OOAApprovalPathDetailsDAL.GetItemList(intNodeID, -1, -1, "").SingleOrDefault();
        }

        public List<OOAApprovalPathMaster> GetOOAApprovalAuthorityPath(string strCompanyID)
        {
            return OOAApprovalPathMasterDAL.GetOOAApprovalAuthorityPath(strCompanyID);
        }




       
    }
}
