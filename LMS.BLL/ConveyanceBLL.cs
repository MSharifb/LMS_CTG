using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using LMS.DAL;
namespace LMS.BLL
{
    public class ConveyanceBLL
    {
        public static List<ConveyanceMaster> GetMasterList(string strAuthorID, int recordID, int OutOfOfficeID, string strEmpID, string dtDate, string isApproved, int startIndex, int rowNumber, out int numTotalRows)
        {
            return ConveyanceDAL.GetMasterList(strAuthorID,recordID, OutOfOfficeID,strEmpID,dtDate,isApproved,startIndex, rowNumber, out numTotalRows);           
        }

        public static List<ConveyanceDetails> GetConveyanceDetails(Int64 recordID, Int64 conveyanceID)
        {
            return ConveyanceDAL.GetConveyanceDetails(recordID,conveyanceID);
        }

        public static int ApproveConveyance(int CONVEYANCEID,string voucherNumber, string ApprovedBy, string StrMode)
        {
            return ConveyanceDAL.ApproveConveyance(CONVEYANCEID,voucherNumber, ApprovedBy, StrMode);
        }

        public static List<ConveyanceApproverDetails> GetConveyanceApproverDetails(Int64 CONVEYANCEID)
        {
            return ConveyanceDAL.GetConveyanceApproverDetails(CONVEYANCEID);
        }
    }
}
