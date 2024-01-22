using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveApplicationValidationBLL
    {
        public static List<ValidationMessage> ValidateLeaveApplication(LeaveApplication obj)
        {
            string strMode = "";
            if (obj.intApplicationID > 0)
            {
                strMode = "U";
            }
            else
            {
                strMode = "I";
            }
            return LeaveApplicationValidationDAL.ValidateLeaveApplication(obj, strMode);
        }
    }
}
