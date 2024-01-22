using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
using System.Transactions;
using LMS.DAL;
using LMSEntity;
using System.Data;
using TVL.DB;

namespace LMS.BLL
{
    public class LeaveApplicationBLL
    {
        public int Add(LeaveApplication obj, List<LeaveLedger> lst, out string strmsg)
        {
            strmsg = "";
            int i = 0;
            int intMasterId = 0;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {

                i = intMasterId = LeaveApplicationDAL.SaveItem(obj, "I", transection, con, out strmsg);

                if (lst != null)
                {
                    foreach (LeaveLedger obj1 in lst)
                    {
                        LeaveLedgerHistory objLLH = new LeaveLedgerHistory();
                        objLLH.intApplicationID = intMasterId;
                        objLLH.intLeaveTypeID = obj1.intLeaveTypeID;
                        objLLH.fltOB = obj1.fltOB;
                        objLLH.fltEntitlement = obj1.fltEntitlement;
                        objLLH.fltEncased = obj1.fltEncased;
                        objLLH.fltCB = obj1.fltCB;
                        objLLH.fltAvailed = obj1.fltAvailed;
                        objLLH.fltapplied = obj1.fltApplied;

                        objLLH.strCompanyID = obj.strCompanyID;
                        objLLH.strEUser = obj.strEUser;
                        objLLH.strIUser = obj.strIUser;

                        i = LeaveLedgerDAL.SaveItem(objLLH, "I", transection, con);
                    }
                }

                transection.Commit();

            }
            catch (Exception ex)
            {
                transection.Rollback();
                intMasterId = i;
                throw ex;
            }

            return intMasterId;
        }

        public int Update(LeaveApplication obj, List<LeaveLedger> lst, out string strmsg)
        {
            strmsg = "";
            int i = 0;
            int intMasterId = 0;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {
                i = LeaveApplicationDAL.SaveItem(obj, "U", transection, con, out strmsg);

                LeaveLedgerHistory objLLHForDel = new LeaveLedgerHistory();

                //Clear ledger first
                objLLHForDel.intApplicationID = Convert.ToInt32(obj.intApplicationID);
                i = LeaveLedgerDAL.SaveItem(objLLHForDel, "D", transection, con);

                if (lst != null)
                {
                    foreach (LeaveLedger obj1 in lst)
                    {

                        LeaveLedgerHistory objLLH = new LeaveLedgerHistory();
                        objLLH.intApplicationID = Convert.ToInt32(obj.intApplicationID);
                        objLLH.intLeaveTypeID = obj1.intLeaveTypeID;
                        objLLH.fltOB = obj1.fltOB;
                        objLLH.fltEntitlement = obj1.fltEntitlement;
                        objLLH.fltEncased = obj1.fltEncased;
                        objLLH.fltCB = obj1.fltCB;
                        objLLH.fltAvailed = obj1.fltAvailed;
                        objLLH.fltapplied = obj1.fltApplied;

                        objLLH.strCompanyID = obj.strCompanyID;
                        objLLH.strEUser = obj.strEUser;
                        objLLH.strIUser = obj.strIUser;

                        i = LeaveLedgerDAL.SaveItem(objLLH, "I", transection, con);
                    }
                }

                transection.Commit();
                intMasterId = i;
            }
            catch (Exception ex)
            {
                transection.Rollback();
                intMasterId = i;
                throw ex;
            }

            return intMasterId;
        }

        public int Cancel(LeaveApplication objLeaveApplication, out string strmsg)
        {
            strmsg = "";
            int i = 0;
            try
            {
                i = LeaveApplicationDAL.SaveItem(objLeaveApplication, "C", null, null, out strmsg);
            }
            catch (Exception ex)
            {
                i = -1;
            }

            return i;
        }

        public int Approve(LeaveApplication objLeaveApplication, out string strmsg)
        {
            strmsg = "";
            int i = 0;
            try
            {
                i = LeaveApplicationDAL.SaveItem(objLeaveApplication, "A", null, null, out strmsg);
            }
            catch (Exception ex)
            {
                i = -1;
            }

            return i;
        }

        public int AlternateRecommend(LeaveApplication objLeaveApplication, out string strmsg)
        {
            strmsg = "";
            int i = 0;
            try
            {

                i = LeaveApplicationDAL.SaveItem(objLeaveApplication, "REC", null, null, out strmsg);
            }
            catch (Exception ex)
            {
                i = -1;
            }

            return i;
        }
        
        public int AlternateReject(LeaveApplication objLeaveApplication, out string strmsg)
        {
            strmsg = "";
            int i = 0;
            try
            {

                i = LeaveApplicationDAL.SaveItem(objLeaveApplication, "R", null, null, out strmsg);
            }
            catch (Exception ex)
            {
                i = -1;
            }

            return i;
        }

        public int Edit(LeaveApplication objLeaveApplication, out string strmsg)
        {
            strmsg = "";
            int i = -1;
            try
            {

                i = LeaveApplicationDAL.SaveItem(objLeaveApplication, "U", null, null, out strmsg);
            }
            catch (Exception ex)

            { }

            return i;
        }

        public int Delete(Int64 Id, out string strmsg)
        {
            strmsg = "";
            LeaveApplication obj = new LeaveApplication();
            obj.intApplicationID = Id;

            int i = -1;

            try
            {

                LeaveApplicationDAL.SaveItem(obj, "D", null, null, out strmsg);
            }
            catch (Exception ex)

            { }

            return i;
        }

        public LeaveApplication LeaveApplicationGet(System.Int64 Id)
        {
            int p = 0;
            return LeaveApplicationDAL.GetItemList(Id, "", "", 0, 0, "", "", "", -1, "", false, "", "", "",
                                                   false,0, "intApplicationID", "asc", 1, 1, out p).SingleOrDefault();
        }


        public List<LeaveApplication> EmployeeLeaveApplicationGet(string strEmpID, int intLeaveYearID, string strCompanyID,
                                                                  int intLeaveTypeID, string strApplyDateFrom, string strApplyDateTo,
                                                                  int intAppStatusID, string strDepartmentID, string strDesignationID)
        {

            return LeaveApplicationDAL.GetEmployeeLeaveItemList(strEmpID, intLeaveYearID, strCompanyID,
                                                                intLeaveTypeID, strApplyDateFrom, strApplyDateTo,
                                                                intAppStatusID, strDepartmentID, strDesignationID);
        }

        public List<LeaveApplication> EmployeeLeaveApplicationGet(string strEmpID, int intLeaveYearID, string strCompanyID,
                                                                  int intAppStatusID, bool bitIsAdjustment)
        {

            return LeaveApplicationDAL.GetEmployeeLeaveItemList(strEmpID, intLeaveYearID, strCompanyID,
                                                                0, "", "", intAppStatusID, "", "", bitIsAdjustment);
        }

        public List<LeaveApplication> EmployeeApprovedLeaveApplicationGet(string strEmpID, int intLeaveYearID, 
                                                                          string strCompanyID, bool bitIsAdjustment)
        {
            return LeaveApplicationDAL.GetEmployeeApprovedLeaveApplicationItemList(strEmpID, intLeaveYearID, strCompanyID,
                                                                0, "", "", "", "", bitIsAdjustment);
        }



        public List<LeaveApplication> EmployeeLeaveApplicationGet(string strEmpID, int intLeaveYearID, string strCompanyID)
        {

            return LeaveApplicationDAL.GetEmployeeLeaveItemList(strEmpID, intLeaveYearID, strCompanyID,
                                                                0, "", "", 0, "", "");
        }



        public List<LeaveApplication> LeaveApplicationGet(System.Int64 intApplicationID, string strEmpInitial, string strEmpName, int intLeaveYearID,
                                                        int intLeaveTypeID, string strApplyDateFrom, string strApplyDateTo,
                                                        string strApplicationType, int intAppStatusID, string strApprovalProcess,
                                                        bool bitIsForAlternateProcess, string strCompanyID, string strDepartmentID,
                                                        string strDesignationID, bool bitIsAdjustment, Int32 ZoneId, string strSortBy, string strSortType,
                                                        int startRowIndex, int maximumRows, out int numTotalRows)
        {
            return LeaveApplicationDAL.GetItemList(intApplicationID, strEmpInitial, strEmpName, intLeaveYearID,
                                                    intLeaveTypeID, strApplyDateFrom, strApplyDateTo,
                                                    strApplicationType, intAppStatusID, strApprovalProcess,
                                                    bitIsForAlternateProcess, strCompanyID, strDepartmentID,
                                                    strDesignationID, bitIsAdjustment, ZoneId, strSortBy, strSortType,
                                                    startRowIndex, maximumRows, out numTotalRows);
        }

        public List<LeaveApplication> RequestedLeaveApplicationGet(System.Int64 intAppFlowID, string strEmpInitial, string strEmpName,
                                                                    int intLeaveYearID, int intLeaveTypeID, string strApplyDateFrom,
                                                                    string strApplyDateTo, string strApplicationType, int intAppStatusID,
                                                                    bool bitIsDiscard, string strCompanyID, string strDepartmentID,
                                                                    string strDesignationID, string strAuthorID, string strAppDirectionType,
                                                                    string strSortBy, string strSortType, int startRowIndex,
                                                                    int maximumRows, out int numTotalRows)
        {
            return LeaveApplicationDAL.GetRequestedLeaveItemList(intAppFlowID, strEmpInitial, strEmpName, intLeaveYearID, intLeaveTypeID,
                                                                strApplyDateFrom, strApplyDateTo, strApplicationType, intAppStatusID,
                                                                bitIsDiscard, strCompanyID, strDepartmentID, strDesignationID,
                                                                strAuthorID, strAppDirectionType, strSortBy, strSortType,
                                                                startRowIndex, maximumRows, out numTotalRows);
        }


        public List<LeaveApplication> RequestedLeaveGetForBulkApprove(string strAuthorID, string strEmpInitial, string strEmpName,
                                                                    int intLeaveYearID, int intLeaveTypeID, string strApplyDateFrom,
                                                                    string strApplyDateTo, string strApplicationType, int intAppStatusID,
                                                                    bool bitIsDiscard, string strCompanyID, string strDepartmentID,
                                                                    string strDesignationID, string strAppDirectionType,
                                                                    string strSortBy, string strSortType, int startRowIndex,
                                                                    int maximumRows, out int numTotalRows)
        {
            return LeaveApplicationDAL.GetRequestedLeaveForBulkApprove(strAuthorID, strEmpInitial, strEmpName, intLeaveYearID, intLeaveTypeID,
                                                                strApplyDateFrom, strApplyDateTo, strApplicationType, intAppStatusID,
                                                                bitIsDiscard, strCompanyID, strDepartmentID, strDesignationID,
                                                                strAppDirectionType, strSortBy, strSortType,
                                                                startRowIndex, maximumRows, out numTotalRows);
        }


        public List<LeaveLedger> LeaveBalanceIndividualGet(System.Int64 intApplicationID, int intLeaveYearID, int intLeaveTypeID,
                                                            double fltWithPayDuration, string strEmpID,
                                                            string strCompanyID, string strApplicationType)
        {
            return LeaveLedgerDAL.GetLeaveBalanceIndividual(intApplicationID, intLeaveYearID, intLeaveTypeID,
                                                            fltWithPayDuration, strEmpID,
                                                            strCompanyID, strApplicationType);
        }




        public List<LeaveLedger> GetLeaveLedgerHistory(System.Int64 intApplicationID, int intLeaveYearID,
                                                        int intLeaveTypeID, double fltWithPayDuration,
                                                        string strEmpID, string strCompanyID)
        {
            return LeaveLedgerDAL.GetLeaveLedgerHistory(intApplicationID, intLeaveYearID, intLeaveTypeID,
                                                        fltWithPayDuration, strEmpID, strCompanyID);
        }




        public double GetDuration(System.String strEmpID, int intLeaveYearID, int intLeaveTypeID,
                                    string strApplicationType, DateTime dtApplyFromDate,
                                    DateTime dtApplyToDate, string strCompanyID)
        {
            return LeaveApplicationDAL.GetDuration(strEmpID, intLeaveYearID, intLeaveTypeID,
                                                    strApplicationType, dtApplyFromDate,
                                                    dtApplyToDate, strCompanyID);
        }

        public int GetNodeID(System.String strEmpID,int intLeaveTypeID)
        {
            return LeaveApplicationDAL.GetNodeID(strEmpID, intLeaveTypeID);
        }


        private bool CheckValidation(LeaveApplication objLeaveApplication, ref string strMSG)
        {
            bool isvalid = true;
            return isvalid;
        }

    }
}
