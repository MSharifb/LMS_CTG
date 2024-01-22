using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web.Models;
using MvcPaging;
using MvcContrib.Pagination;
using LMS.Util;
using LMS.BLL;

namespace LMS.Web.Controllers
{
    public class MiscellaneousVoucherController : Controller
    {
        //
        // GET: /MiscellaneousVoucher/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult getAjaxTab(int? page, int id)
        {
            string viewName = string.Empty;
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            viewName = "VoucherList";

            MiscellaneousVoucher searchObj = new MiscellaneousVoucher();
            searchObj.STRAUTHORID = LoginInfo.Current.strEmpID;
            model.StrDate = "";
            switch (id)
            {
                case 1:

                    searchObj.ISAPPROVED = "0";
                    model.IsPaid = "0";
                    break;
                case 2:
                    searchObj.ISAPPROVED = "1";
                    model.IsPaid = "1";
                    
                    break;

                default:
                    searchObj.ISAPPROVED = "0";
                    model.IsPaid = "0";
                    break;
            }

            searchObj.STRAUTHORID = LoginInfo.Current.strEmpID;
           
            model.MiscellaneousVoucher = searchObj;
            model = model.GetMiscVoucherList(model);

            model.LstMiscellaneousVoucherPaging = model.LstMiscellaneousVoucher.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            ModelState.Clear();

            return PartialView(viewName, model);
        }

        public ActionResult List(int? page)
        {
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();
          
            return PartialView("List",model);
        }


        public ActionResult AttachmentView(string id)
        {
            MISCModels model = new MISCModels();
            model.MiscDetails = model.GetDetailByID(id);
            model.FileName = model.MiscDetails.ATTACHMENTPATH;
            return View(model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult searchData(int? page, string IsPaid, string StrEmpID, string strName, string StrDate)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();

            DateTime dt = StrDate != "" ? DateTime.Parse(StrDate) : DateTime.MinValue;

             MiscellaneousVoucher searchObj = new MiscellaneousVoucher();

             searchObj.MISCDATE = dt;
            searchObj.STREMPID =strName ==""? "" : StrEmpID;
            searchObj.STRAUTHORID = LoginInfo.Current.strEmpID;
            searchObj.ISAPPROVED = IsPaid;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            model.IsPaid = IsPaid;
            model.MiscellaneousVoucher = searchObj;
            model = model.GetMiscVoucherList(model);
            model.LstMiscellaneousVoucherPaging = model.LstMiscellaneousVoucher.ToPagedList(currentPageIndex, AppConstant.PageSize10);

           
            model.StrEmpID = StrEmpID;
            model.StrDate = StrDate;
            model.StrEmpName = strName;
            model.IsPaid = IsPaid;
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("VoucherList", model);
        }

        public ActionResult Details(int id)
        {
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();
            model = model.GetDetailsData(Int64.Parse(id.ToString()));
            model.LstConveyanceApproverDetails = model.GetConveyanceApproverDetails(Int64.Parse(id.ToString()));
            InitializeModel(model);
            return PartialView(model);

        }

       

        private void InitializeModel(MiscellaneousVoucherModels model)
        {

            
            Employee objEmp = new Employee();

            if (!string.IsNullOrEmpty(model.MiscellaneousVoucher.STREMPID))
            {
                objEmp = model.GetEmployeeInfo(model.MiscellaneousVoucher.STREMPID);

                model.MiscellaneousVoucher.STRDEPARTMENT = objEmp.strDepartment;
                model.MiscellaneousVoucher.STRDESIGNATION = objEmp.strDesignation;
              
               
            }

        }


        public JsonResult Approve(MiscellaneousVoucherModels model)
        {
            

            model.MiscellaneousVoucher.ISAPPROVED = "1";
            model.MiscellaneousVoucher.APPROVEDBY = LoginInfo.Current.strEmpID;
            model.MiscellaneousVoucher.APPROVEDDATE = DateTime.Now;
            model.Approve(model);
            
            model.Message = LMS.Util.Messages.GetSuccessMessage("Paid Successfully.");

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDetailsList(string id)
        {
            MiscellaneousVoucherModels model = new MiscellaneousVoucherModels();
            string[] ids = id.Split(',');

            List<int> idList = new List<int>();

            foreach (string item in ids)
            {
                if (item.Length > 0)
                {
                    idList.Add(int.Parse(item));
                }
            }

            model.IDList = idList;
            return View("DetailsList", model);
        }
    }
}
