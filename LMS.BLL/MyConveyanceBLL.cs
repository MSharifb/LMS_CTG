using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class MyConveyanceBLL
    {
        public static List<MyConveyanceMaster> GetMasterList(int OutOfOfficeID, string strEmpID, string dtDate, int startIndex, int rowNumber, out int numTotalRows)
        {
            return MyConveyanceDAL.GetMasterList(OutOfOfficeID, strEmpID, dtDate, startIndex, rowNumber, out numTotalRows);
        }

        public static List<MyConveyanceDetails> GetConveyanceDetails(Int64 recordID, Int64 conveyanceID)
        {
            return MyConveyanceDAL.GetConveyanceDetails(recordID, conveyanceID);
        }

        public static List<MyConveyanceMaster> GetMasterList(string strAuthorID, int recordID, int OutOfOfficeID, string strEmpID, string dtDate, string isApproved, int startIndex, int rowNumber, out int numTotalRows)
        {
            return MyConveyanceDAL.GetMasterList(strAuthorID, recordID, OutOfOfficeID, strEmpID, dtDate, isApproved, startIndex, rowNumber, out numTotalRows);
        }
    }
}
