using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveLedgerBLL
    {
        public LeaveLedger LeaveLedgerGet(int yearId, int intLeaveTypeId,string strEmpId, string strCompanyId)
        {
            return LeaveLedgerDAL.GetItemList(yearId, intLeaveTypeId,strEmpId, strCompanyId).SingleOrDefault();
        }

        public List<LeaveLedger> LeaveLedgerGet(string strEmpId, string strCompanyId)
        {
            return LeaveLedgerDAL.GetItemList(0, 0, strEmpId, strCompanyId);
        }
        public List<LeaveLedger> LeaveLedgerGet(int yearId, string strEmpId, string strCompanyId)
        {
            return LeaveLedgerDAL.GetItemList(yearId, -1, strEmpId, strCompanyId);
        }


        public List<LeaveLedger> LeaveLedgerGet(int yearId, int intLeaveTypeId, string strCompanyId)
        {
            return LeaveLedgerDAL.GetItemList(yearId, intLeaveTypeId,"", strCompanyId);
        }

        public List<LeaveLedger> LeaveLedgerGetAll()
        {
            return LeaveLedgerDAL.GetItemList(0,0,"","");
        }
        public List<LeaveLedger> LeaveLedgerGetAll(string strCompanyId)
        {
            return LeaveLedgerDAL.GetItemList(0,0,"", strCompanyId);
        }


        public List<LeaveLedger> LeaveBalanceIndividualGet(System.Int64 intApplicationID, int intLeaveYearID, int intLeaveTypeID, double fltWithPayDuration, string strEmpID, string strCompanyID, string strApplicationType)
        {
            return LeaveLedgerDAL.GetLeaveBalanceIndividual(intApplicationID, intLeaveYearID, intLeaveTypeID, fltWithPayDuration, strEmpID, strCompanyID, strApplicationType);
        }
    }
}
