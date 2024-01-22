using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LeaveEntitlementBLL
    {
        public void Process(LeaveEntitlement objLeaveEntitlement, out string strmessage)
        {
            LeaveEntitlementDAL.EntitlementProcess(objLeaveEntitlement, out strmessage);
        }
        public void Rollback(LeaveEntitlement objLeaveEntitlement, out string strmessage)
        {
            LeaveEntitlementDAL.EntitlementRollback(objLeaveEntitlement, out strmessage);
        }
    }
}
