using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.Pagination;
using LMSEntity;
using LMS.Web.Models;
using LMS.Util;
using MvcPaging;

namespace LMS.Web.Controllers
{
    public class MiscellaneousReportController : Controller
    {
        //
        // GET: /ConveyanceReport/

        public ActionResult Index()
        {
            MiscellaneousReportModels model = new MiscellaneousReportModels();
            model.StrToDate = DateTime.Now.ToString("dd-MM-yyyy");
            model.StrFromDate = DateTime.Now.ToString("dd-MM-yyyy");
            return PartialView("MiscellaneousReport", model);
        }


        //POST: /Reports/ShowReport        
        [HttpPost]
        [NoCache]
        public ActionResult ShowReport(MiscellaneousReportModels model, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                model.maximumRows = AppConstant.PageSize20;

                if (model.StrEmpName == "" || model.StrEmpName == null)
                    model.StrEmpID = "";

                model = model.GetReportData(model);
                model.LstMiscellaneousReportPaging = model.LstMiscellaneousReport.ToPagedList(model.startRowIndex, AppConstant.PageSize20);
                ModelState.Clear();

            }
            catch (Exception ex)
            {
            }

            return PartialView("MiscellaneousReportDetails", model);
        }

    }
}
