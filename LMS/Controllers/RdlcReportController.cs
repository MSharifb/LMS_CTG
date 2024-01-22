using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Web.Models.ReportModel;

namespace LMS.Web.Controllers
{
    public class RdlcReportController : Controller
    {
        private readonly ReportViewerViewModel _model;

        public RdlcReportController()
        {
            _model = new ReportViewerViewModel();
        }

        public ActionResult MyLeaveStatus()
        {
            _model.ReportPath = Url.Content("~/Reports/viewers/MyLeaveStatus.aspx");
            return View("ReportViewer", _model);
        }

        public ActionResult LeaveReports()
        {
            _model.ReportPath = Url.Content("~/Reports/viewers/ReportLeaveInfo.aspx");
            return View("ReportViewer", _model);
        }
        public ActionResult PrintLeaveApplication(string applicationId)
        {
            try
            {
                var reportUrl = "~/Reports/viewers/PrintLeaveApplication.aspx";
                reportUrl += "?param1=" + applicationId;
                _model.ReportPath = Url.Content(reportUrl);
                return Redirect(_model.ReportPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
