using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Util;
using LMSEntity;
using LMS.Web.Models;
using MvcPaging;
using MvcContrib.Pagination;

namespace LMS.Web.Controllers
{
    public class OutOfOfficeReportController : Controller
    {
        //
        // GET: /OutOfOfficeReport/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [NoCache]
        public ActionResult Report(int? page)
        {

            OutOfOfficeModels model = new OutOfOfficeModels();
            OutOfOffice searchObj = new OutOfOffice();
            model.ToDate = model.ToDate;
            model.FromDate = model.FromDate;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            model.LstOutOfOffice = model.GetReportData(searchObj, "", "", model.startRowIndex, model.maximumRows);
            model.LstOutOfOfficePaging = model.LstOutOfOffice.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return View(model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult DetailsReports(int? page, string strID, string Name, string FromDate, string ToDate)
        {

            OutOfOfficeModels model = new OutOfOfficeModels();
            OutOfOffice searchObj = new OutOfOffice();
            if (Name != "")
            {
                searchObj.STREMPID = strID;
                model.StrEmpID = strID;
            }

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;

            model.LstOutOfOffice = model.GetReportData(searchObj, FromDate, ToDate, model.startRowIndex, model.maximumRows);


            model.LstOutOfOfficePaging = model.LstOutOfOffice.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            model.EmployeeName = Name;
            model.FromDate = FromDate;
            model.ToDate = ToDate;
            return PartialView("Report", model);
        }

        public ActionResult OutOfOfficeReportsPaging(int? page, OutOfOfficeModels model)
        {
            OutOfOffice searchObj = new OutOfOffice();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;

            model.LstOutOfOffice = model.GetReportData(searchObj, model.FromDate, model.ToDate, model.startRowIndex, model.maximumRows);


            model.LstOutOfOfficePaging = model.LstOutOfOffice.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            model.EmployeeName = model.EmployeeName;
            model.FromDate = model.FromDate;
            model.ToDate = model.ToDate;
            return PartialView("Report", model);
        }


        [NoCache]
        public ActionResult OutOfOfficeDetailsData(string Id)
        {
            OutOfOffice searchObj = new OutOfOffice();
            OutOfOfficeModels model = new OutOfOfficeModels();
            searchObj.ID = long.Parse(Id);
            searchObj.STREMPID = "";
            searchObj.PURPOSE = "";
            model = model.GetReportDetailsData(searchObj, "", "", 1, 1000);
            model.IsNew = false;
            model.OutOfOffice.IsNew = false;

            if (model.OutOfOffice.ISGETIN == false)
            {
                model.OutOfOffice.STRGETINDATE = DateTime.Now.ToString("dd/MM/yyyy");
                model.OutOfOffice.GETINDATE = DateTime.Now;
                model.OutOfOffice.GETINTIME = DateTime.Now.ToShortTimeString();
            }
            else
            {
                model.OutOfOffice.STRGETINDATE = model.OutOfOffice.GETINDATE.ToString("dd/MM/yyyy");
            }

            InitializeModel(model, true);

            return View("OutOfOfficeAdd", model);
        }


        private void InitializeModel(OutOfOfficeModels model, bool isPostBack)
        {

            if (!isPostBack)
            {

            }
            Employee objEmp = new Employee();

            if (!string.IsNullOrEmpty(model.OutOfOffice.STREMPID))
            {
                objEmp = model.GetEmployeeInfo(model.OutOfOffice.STREMPID);

                model.OutOfOffice.Strdepartment = objEmp.strDepartment;
                model.OutOfOffice.Strdesignaiton = objEmp.strDesignation;
                model.EmployeeName = objEmp.strEmpName;
            }

        }

    }
}
