using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using LMS.DAL;

namespace LMS.BLL
{
    public class MiscellaneousVoucherBLL
    {
        public static int Save(MiscellaneousVoucher obj)
        {
            return MiscellaneousVoucherDAL.Save(obj, "I");
        }

        public static int Update(MiscellaneousVoucher obj)
        {
            return MiscellaneousVoucherDAL.Save(obj, "U");
        }

        public static int Delete(MiscellaneousVoucher obj)
        {
            return MiscellaneousVoucherDAL.Save(obj, "D");
        }

        public static List<MiscellaneousVoucher> GetData(MiscellaneousVoucher obj, int startIndex, int rowNumber, out int numTotalRows)
        {
            return MiscellaneousVoucherDAL.GetData(obj, startIndex, rowNumber, out numTotalRows);
        }


        public static List<ConveyanceApproverDetails> GetConveyanceApproverDetails(Int64 VOUCHERID)
        {
            return MiscellaneousVoucherDAL.GetApproverDetails(VOUCHERID);
        }

    }
}
