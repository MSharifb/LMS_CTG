using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class LoginDetailsBLL
    {
        public LoginDetails GetLoginDetailsByEmpAndCompany(string strEmpID, string strCompanyID)
        {
            LoginDetails details = new LoginDetails();
            details = GetLoginDetailsByEmpAndCompany(strEmpID, strCompanyID, 0);

            //logList = LoginDetailsDAL.GetItemList(strEmpID, strCompanyID);
            //if (logList.Count > 0)
            //{
            //    return logList[0];
            //}
            return details;
        }

        public LoginDetails GetLoginDetailsByEmpAndCompany(string strEmpID, string strCompanyID, int loggedInZoneId)
        {
            List<LoginDetails> logList = new List<LoginDetails>();
            logList = LoginDetailsDAL.GetItemList(strEmpID, strCompanyID, loggedInZoneId);
            if (logList.Count > 0)
            {
                return logList[0];
            }
            return null;
        }
    }
}
