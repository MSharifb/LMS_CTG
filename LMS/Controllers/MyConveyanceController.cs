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
    public class MyConveyanceController : Controller
    {
        //
        // GET: /MyConveyance/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConveyanceList()
        {

            return View();
        }

        [HttpGet]
        [NoCache]
        public PartialViewResult getAjaxTab(int? page, int id)
        {
            string viewName = string.Empty;
            MyConveyanceModels model = new MyConveyanceModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            viewName = "DetailsList";

            switch (id)
            {
                case 1:
                    
                    model = model.GetMasterList(-1, LoginInfo.Current.strEmpID, "", model.startRowIndex, model.maximumRows);
                    model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                    model.IsPaid = "0";
                    break;
                case 2:                                       
                   
                    model = model.GetMasterList("", -1, -1, LoginInfo.Current.strEmpID, "", "1", model.startRowIndex, model.maximumRows);
                    model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                    model.IsPaid = "1";
                    break;

                default:
                    viewName = "DetailsList";
                    break;
            }
           
            return PartialView(viewName, model);
        }

        public ActionResult MyConveyancePaging(int? page, MyConveyanceModels model)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string viewName = string.Empty;
            string isPaid = model.IsPaid;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            switch (isPaid)
            {
                case "0":
                   
                    model = model.GetMasterList(-1, LoginInfo.Current.strEmpID, "", model.startRowIndex, model.maximumRows);
                    model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                    model.IsPaid = "0";
                    break;

                case "1":                                        
                    model = model.GetMasterList("", -1, -1, LoginInfo.Current.strEmpID, "", "1", model.startRowIndex, model.maximumRows);                  
                    model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                    model.IsPaid = "1";
                    break;

                default:
                    viewName = "DetailsList";
                    break;
            }
            

            model.IsPaid = isPaid;
            InitializeModel(model);
            model.ConveyanceObj.STREMPNAME = model.ConveyanceObj.STREMPNAME;
            model.ConveyanceObj.OUTOFOFFICEDATE = model.ConveyanceObj.OUTOFOFFICEDATE;
            model.ConveyanceObj.STREMPNAME = model.ConveyanceObj.STREMPNAME;
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("DetailsList", model);


        }


        public ActionResult Details(string Id)
        {
            MyConveyanceModels model = new MyConveyanceModels();
            model = MyConveyanceModels.GetConveyanceDetails(Int64.Parse("-1"), Int64.Parse(Id));
            model.ConveyanceObj = model.LstConveyanceDetails[0];
            model.LstConveyanceApproverDetails = model.GetConveyanceApproverDetails(model.ConveyanceObj.CONVEYANCEID);
            model = InitializeModel(model);
            return View(model);
           
        }

        private MyConveyanceModels InitializeModel(MyConveyanceModels model)
        {
            Employee objEmp = new Employee();
            OutOfOfficeModels mModel = new OutOfOfficeModels();
            if (!string.IsNullOrEmpty(model.ConveyanceObj.STREMPID))
            {
                objEmp = mModel.GetEmployeeInfo(model.ConveyanceObj.STREMPID);

                model.ConveyanceObj.STRDEPARTMENT = objEmp.strDepartment;
                model.ConveyanceObj.STRDESIGNATION = objEmp.strDesignation;
                model.ConveyanceObj.STREMPNAME = objEmp.strEmpName;
            }
            return model;
        }


        [HttpPost]
        [NoCache]
        public ActionResult searchData(int? page, string IsPaid, string StrDate)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            MyConveyanceModels model = new MyConveyanceModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            DateTime dt = StrDate != "" ? DateTime.Parse(StrDate) : DateTime.MinValue;
            model.IsPaid = IsPaid;
            switch (IsPaid)
            {
                case "0":
                   
                    model = model.GetMasterList(-1, LoginInfo.Current.strEmpID, StrDate, model.startRowIndex, model.maximumRows);
                    model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                    model.IsPaid = "0";
                    break;

                case "1":
                    model = model.GetMasterList("", -1, -1, LoginInfo.Current.strEmpID, StrDate, "1", model.startRowIndex, model.maximumRows);
                    model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                    model.IsPaid = "1";
                    break;

                default:                    
                    break;
            }
            InitializeModel(model);
            model.ConveyanceObj.STRDATE = StrDate;            
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("DetailsList", model);
        }

    }
}
