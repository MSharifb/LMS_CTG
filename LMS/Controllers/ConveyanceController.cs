using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web.Models;
using LMS.Util;
using MvcPaging;
using MvcContrib.Pagination;

namespace LMS.Web.Controllers
{
    public class ConveyanceController : Controller
    {
        //
        // GET: /Conveyence/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [NoCache]
        public PartialViewResult PaidList(int? page)
        {
            ConveyanceModels model = new ConveyanceModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;

            model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, "", "", "0", model.startRowIndex, model.maximumRows);
            model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            return PartialView(model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult List()
        {

            return PartialView();
        }

        [HttpGet]
        [NoCache]
        public PartialViewResult getAjaxTab(int? page, int id)
        {
            string viewName = string.Empty;
            ConveyanceModels model = new ConveyanceModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            viewName = "PaidList";

            switch (id)
            {
                    case 1:
                   
                    model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, "", "", "0", model.startRowIndex, model.maximumRows);
                    model.IsPaid = "0";
	                    break;
	                case 2:
                        //viewName = "ViewList";
                        model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, "", "", "1", model.startRowIndex, model.maximumRows);
                        model.IsPaid = "1";
                    break;
	                
	                default:
	                   viewName = "PaidList";
                       model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, "", "", "0", model.startRowIndex, model.maximumRows);
                    break;
	            }
                model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                ModelState.Clear();
                
                return PartialView(viewName, model);
	        }

        [HttpGet]
        [NoCache]
        public PartialViewResult ViewList(int? page)
        {
            ConveyanceModels model = new ConveyanceModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, "", "", "1", model.startRowIndex, model.maximumRows);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ConveyanceDetails(string Id)
        {
            ConveyanceModels model = new ConveyanceModels();            
            model = ConveyanceModels.GetConveyanceDetails(Int64.Parse("-1"),Int64.Parse(Id));
            model.ConveyanceObj = model.LstConveyanceDetails[0];            
            model.LstConveyanceApproverDetails = model.GetConveyanceApproverDetails(model.ConveyanceObj.CONVEYANCEID);
            model = InitializeModel(model);            
            return View(model);
        }

        private ConveyanceModels InitializeModel(ConveyanceModels model)
        {
            Employee objEmp = new Employee();
            OutOfOfficeModels mModel = new OutOfOfficeModels();
            if (!string.IsNullOrEmpty(model.ConveyanceObj.STREMPID))
            {
                objEmp = mModel.GetEmployeeInfo(model.ConveyanceObj.STREMPID);

                model.ConveyanceObj.STRDEPARTMENT= objEmp.strDepartment;
                model.ConveyanceObj.STRDESIGNATION = objEmp.strDesignation;
                model.ConveyanceObj.STREMPNAME = objEmp.strEmpName;
            }
            return model;
        }

              

        [HttpGet]
        [NoCache]

        public JsonResult Approve(ConveyanceModels model)
        {           
            
            if (ConveyanceModels.ApproveConveyance(int.Parse(model.ConveyanceObj.CONVEYANCEID.ToString()), model.ConveyanceObj.VOUCHERNUMBER, LoginInfo.Current.strEmpID, "A") > 0)
            {
                model.Message = LMS.Util.Messages.GetSuccessMessage("Paid Successfully.");
            }
            else
                model.Message = LMS.Util.Messages.GetErroMessage("Paid failed.");

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [NoCache]
        public ActionResult search(int?page,string IsPaid,string StrEmpID,string strName, string StrDate)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            ConveyanceModels model = new ConveyanceModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            DateTime dt = StrDate != "" ? DateTime.Parse(StrDate) : DateTime.MinValue;
            model.IsPaid = IsPaid;
            model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, StrEmpID, StrDate, "1", model.startRowIndex, model.maximumRows);

            InitializeModel(model);
            model.ConveyanceObj.STREMPNAME = StrEmpID;
            model.ConveyanceObj.STRDATE = StrDate;
            model.ConveyanceObj.STREMPNAME = strName;
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("ViewList", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult searchData(int? page, string IsPaid, string StrEmpID, string strName, string StrDate)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            ConveyanceModels model = new ConveyanceModels();

            DateTime dt = StrDate != "" ? DateTime.Parse(StrDate) : DateTime.MinValue;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            model.IsPaid = IsPaid;
            model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, StrEmpID, StrDate, model.IsPaid, model.startRowIndex, model.maximumRows);
            model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            InitializeModel(model);
            model.ConveyanceObj.STREMPNAME = StrEmpID;
            model.ConveyanceObj.STRDATE = StrDate;
            model.ConveyanceObj.STREMPNAME = strName;
            model.IsPaid = IsPaid;
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("PaidList", model);
        }

        public ActionResult ConveyanceDuePaging(int? page, ConveyanceModels model)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            string isPaid = model.IsPaid;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, "", "", model.IsPaid, model.startRowIndex, model.maximumRows);
            model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            model.IsPaid = isPaid;
            InitializeModel(model);
            model.ConveyanceObj.STREMPNAME = model.ConveyanceObj.STREMPNAME;
            model.ConveyanceObj.OUTOFOFFICEDATE = model.ConveyanceObj.OUTOFOFFICEDATE;
            model.ConveyanceObj.STREMPNAME = model.ConveyanceObj.STREMPNAME;
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("PaidList", model);

            
        }

        public ActionResult ConveyanceViewPaging(int? page, ConveyanceModels model)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            model = model.GetMasterList(LoginInfo.Current.strEmpID, -1, -1, model.ConveyanceObj.STREMPID, "", "1", model.startRowIndex, model.maximumRows);
            model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            InitializeModel(model);
            model.ConveyanceObj.STREMPNAME = model.ConveyanceObj.STREMPNAME;
            model.ConveyanceObj.OUTOFOFFICEDATE = model.ConveyanceObj.OUTOFOFFICEDATE;
            model.ConveyanceObj.STREMPNAME = model.ConveyanceObj.STREMPNAME;
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("PaidList", model);            
        }
    }
}
