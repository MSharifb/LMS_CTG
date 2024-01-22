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
    public class ApprovalPathBLL
    {
        private List<ApprovalPathMaster> _ApprovalPathMasterList;
        public List<ApprovalPathMaster> ApprovalPathMasterList
        {
            get
            {
                if (_ApprovalPathMasterList == null)
                {
                    _ApprovalPathMasterList = new List<ApprovalPathMaster>();
                }
                return _ApprovalPathMasterList;
            }
            set { _ApprovalPathMasterList = value; }
        }

        public ApprovalPathDetails SavePath(ApprovalPathMaster objApprovalPathMaster, ApprovalPathDetails obj)
        {
            int i = 0;
            int intMasterId = 0;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {
                i = intMasterId = ApprovalPathMasterDAL.SaveItem(objApprovalPathMaster, "I", transection, con);

                obj.intPathID = intMasterId;

                if (i > 0)
                {
                    obj.strCompanyID = objApprovalPathMaster.strCompanyID;
                    obj.strEUser = objApprovalPathMaster.strEUser;
                    obj.strIUser = objApprovalPathMaster.strIUser;

                    i = ApprovalPathDetailsDAL.SaveItem(obj, "I", transection, con);
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


        public int SavePath(ApprovalPathDetails obj, bool IsInsert)
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
                    i = ApprovalPathDetailsDAL.SaveItem(obj, "I");
                }

                else
                {
                    i = ApprovalPathDetailsDAL.SaveItem(obj, "D");

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

        public int Add(ApprovalPathMaster objApprovalPathMaster, List<ApprovalPathDetails> lst)
        {
            int i = -1;
            int intMasterId = -1;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {
                i = intMasterId = ApprovalPathMasterDAL.SaveItem(objApprovalPathMaster, "I", transection, con);

                foreach (ApprovalPathDetails obj in lst)
                {

                    obj.intPathID = intMasterId;

                    obj.strCompanyID = objApprovalPathMaster.strCompanyID;
                    obj.strEUser = objApprovalPathMaster.strEUser;
                    obj.strIUser = objApprovalPathMaster.strIUser;
                    i = ApprovalPathDetailsDAL.SaveItem(obj, "I", transection, con);

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

        public int Edit(ApprovalPathMaster objApprovalPathMaster)
        {
            return ApprovalPathMasterDAL.SaveItem(objApprovalPathMaster, "U", null, null);
        }
        public int Delete(int Id)
        {
            ApprovalPathMaster obj = new ApprovalPathMaster();
            obj.intPathID = Id;

            return ApprovalPathMasterDAL.SaveItem(obj, "D", null, null);
        }
        public int DeleteNode(int Id)
        {
            ApprovalPathDetails obj = new ApprovalPathDetails();
            obj.intNodeID = Id;

            return ApprovalPathDetailsDAL.SaveItem(obj, "D");
        }
        public ApprovalPathMaster ApprovalPathMasterGet(int intPathID)
        {
            return ApprovalPathMasterDAL.GetItemList(intPathID, "", "").Single();
        }
        public List<ApprovalPathMaster> ApprovalPathMasterGet(int intPathID, string strPathName, string strCompanyID)
        {
            return ApprovalPathMasterDAL.GetItemList(intPathID, strPathName, strCompanyID);
        }

        public List<ApprovalPathDetails> ApprovalPathDetailsGet(int intNodeID, int intParentNodeID, int intPathID, string strCompanyID)
        {
            return ApprovalPathDetailsDAL.GetItemList(intNodeID, intParentNodeID, intPathID, strCompanyID);
        }

        public ApprovalPathDetails ApprovalPathDetailsGet(int intNodeID)
        {
            return ApprovalPathDetailsDAL.GetItemList(intNodeID, -1, -1, "").SingleOrDefault();
        }

        public List<ApprovalPathMaster> GetApprovalAuthorityPath(string strCompanyID)
        {
            return ApprovalPathMasterDAL.GetApprovalAuthorityPath(strCompanyID);
        }
        


    }
}
