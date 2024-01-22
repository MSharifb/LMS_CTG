using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class OOAApprovalProcessBLL
    {
        public static int Permission(Int64 OutOfOfficeID, string strEUser)
        {
            return OOAApprovalProcessDAL.ApprovalProcess(OutOfOfficeID, "P", strEUser);
        }

        public static int Verify(Int64 OutOfOfficeID, string strEUser)
        {
            return OOAApprovalProcessDAL.ApprovalProcess(OutOfOfficeID, "V", strEUser);
        }

        public static int Reverify(Int64 OutOfOfficeID, string strEUser)
        {
            return OOAApprovalProcessDAL.ApprovalProcess(OutOfOfficeID, "RV", strEUser);
        }

        public static int Approve(Int64 OutOfOfficeID, string strEUser)
        {
            return OOAApprovalProcessDAL.ApprovalProcess(OutOfOfficeID, "A", strEUser);
        }

        public static int Recommend(Int64 OutOfOfficeID, string strEUser)
        {
            return OOAApprovalProcessDAL.ApprovalProcess(OutOfOfficeID, "R", strEUser);
        }

        public static List<Employee> GetApproverInfo(Int64 OutOfOfficeID)
        {
            return OOAApprovalProcessDAL.GetApproverInfo(OutOfOfficeID);
        }

        public static bool isReverify(string authorID, string strEmpID, int intFlowType)
        {
          return  OOAApprovalProcessDAL.isReverify(authorID, strEmpID, intFlowType);
        }
    }
}
