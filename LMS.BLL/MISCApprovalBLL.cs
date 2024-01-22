using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class MISCApprovalBLL
    {
        public static List<MISCApproval> GetData(string strAuthorID,string strEmpID, Int64 miscMasterID, Int64 intAppFlowID, string miscDate, int StartIndex, int RowNumber, out int numTotalRows)
        {
            return MiscApprovalDAL.GetData(strAuthorID, strEmpID,miscMasterID, intAppFlowID, miscDate, StartIndex, RowNumber, out numTotalRows);
        }

        public static string GetApprovalStatus(int MiscMasterID)
        {
            return MiscApprovalDAL.GetApprovalStatus(MiscMasterID);
        }

        public static int Recommend(Int64 MiscMasterID, string strEUser)
        {
            return MiscApprovalDAL.ApprovalProcess(MiscMasterID, "R", strEUser);
        }

        public static int Reverify(Int64 MiscMasterID, string strEUser)
        {
            return MiscApprovalDAL.ApprovalProcess(MiscMasterID, "RV", strEUser);
        }

        public static int Approve(Int64 MiscMasterID, string strEUser)
        {
            return MiscApprovalDAL.ApprovalProcess(MiscMasterID, "A", strEUser);
        }

    
    }
}
