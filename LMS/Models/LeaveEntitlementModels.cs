using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS.BLL;
using LMS.DAL;
using LMSEntity;
using MvcContrib.Pagination;

namespace LMS.Web.Models
{
    public class LeaveEntitlementModels
    {
        public string _Message;
        private SelectList _LeaveYear;
        private LeaveEntitlement _LeaveEntitlement;
        LeaveYearModels objLeaveYear = new LeaveYearModels();

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public LeaveEntitlement LeaveEntitlement
        {
            get
            {
                if (_LeaveEntitlement == null)
                {
                    _LeaveEntitlement = new LeaveEntitlement();
                }
                return _LeaveEntitlement;
            }
            set { _LeaveEntitlement = value; }
        }

        public void EntitlementProcess(LeaveEntitlementModels model, out string strmessage)
        {
            strmessage = "";
            LeaveEntitlementBLL objBll = new LeaveEntitlementBLL();
            try
            {
                var leaveYear = objLeaveYear.GetLeaveYear(model.LeaveEntitlement.intLeaveYearID);

                //if (model.LeaveEntitlement.intLeaveYearID == LoginInfo.Current.intLeaveYearID)
                if (leaveYear != null && leaveYear.bitIsActiveYear == true)
                {
                    model.LeaveEntitlement.strIUser = LoginInfo.Current.LoginName;
                    model.LeaveEntitlement.strCompanyID = LoginInfo.Current.strCompanyID;

                    objBll.Process(model.LeaveEntitlement, out strmessage);
                }
                else
                {
                    strmessage = "Please select active leave year.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EntitlementRollback(LeaveEntitlementModels model, out string strmessage)
        {
            strmessage = "";
            LeaveEntitlementBLL objBll = new LeaveEntitlementBLL();
            try
            {
                var leaveYear = objLeaveYear.GetLeaveYear(model.LeaveEntitlement.intLeaveYearID);

                //if (model.LeaveEntitlement.intLeaveYearID == LoginInfo.Current.intLeaveYearID)
                if (leaveYear != null && leaveYear.bitIsActiveYear == true)
                {
                    model.LeaveEntitlement.strIUser = LoginInfo.Current.LoginName;
                    model.LeaveEntitlement.strCompanyID = LoginInfo.Current.strCompanyID;

                    objBll.Rollback(model.LeaveEntitlement, out strmessage);
                }
                else
                {
                    strmessage = "Please select active leave year.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SelectList LeaveYear
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<LeaveYear> lstLeaveYear = new List<LeaveYear>();

                lstLeaveYear = Common.fetchLeaveYearActive();

                foreach (LeaveYear lt in lstLeaveYear)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = lt.intLeaveYearID.ToString();
                    item.Text = lt.strYearTitle;
                    itemList.Add(item);
                }
                this._LeaveYear = new SelectList(itemList, "Value", "Text", LoginInfo.Current.intLeaveYearID);

                return _LeaveYear;
            }
            set { _LeaveYear = value; }
        }
    }
}
