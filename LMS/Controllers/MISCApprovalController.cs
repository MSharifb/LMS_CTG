using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Web.Models;
using LMSEntity;
using MvcPaging;
using MvcContrib.Pagination;
using LMS.Util;

namespace LMS.Web.Controllers
{
    public class MISCApprovalController : Controller
    {
        //
        // GET: /MISCApproval/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApprovalList(int? page)
        {
           
            MISCApprovalPathModels model = new MISCApprovalPathModels();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            //model.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
            model.StrDate = DateTime.Now.ToString("dd-MM-yyyy");
         
            
            model.LstMISCApproval = model.GetData(LoginInfo.Current.strEmpID,model.StrEmpID,-1,-1, model.StrDate,model.startRowIndex,model.maximumRows);

            model.LstMISCApprovalPaging = model.LstMISCApproval.ToPagedList(currentPageIndex, AppConstant.PageSize);
          

            return PartialView(model);
                 
        }

        public ActionResult AttachmentView(string id)
        {
            MISCModels model = new MISCModels();
            model.MiscDetails = model.GetDetailByID(id);
            model.FileName = model.MiscDetails.ATTACHMENTPATH;
            return View(model);
        }

        public ActionResult ApprovalListPaging(int? page, MISCApprovalPathModels model)
        {

           
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            
            model.LstMISCApproval = model.GetData(LoginInfo.Current.strEmpID,model.StrEmpID, -1, -1, model.StrDate, model.startRowIndex, model.maximumRows);

            model.LstMISCApprovalPaging = model.LstMISCApproval.ToPagedList(currentPageIndex, AppConstant.PageSize);


            return PartialView("ApprovalList", model);

        }


        [HttpPost]
        [NoCache]

        public ActionResult ApprovalSearch(int? page, string StrEmpID, string strName, string StrDate)
        {

            MISCApprovalPathModels model = new MISCApprovalPathModels();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;


            model.LstMISCApproval = model.GetData(LoginInfo.Current.strEmpID, StrEmpID, -1, -1, StrDate, model.startRowIndex, model.maximumRows);

            model.LstMISCApprovalPaging = model.LstMISCApproval.ToPagedList(currentPageIndex, AppConstant.PageSize);

            model.EmployeeName = strName;
            return PartialView("ApprovalList",model);

        }


        public ActionResult Detail(string Id)
        {
            MISCApprovalPathModels model = new MISCApprovalPathModels();
            MISCMaster searchObj = new MISCMaster();
            searchObj.MISCMASTERID = Int64.Parse(Id);
            model.MISCMaster = model.GetMasterData(searchObj);
            model.LstMISCDetails = model.GetDetails(int.Parse(Id));
            model.ApprovalStatus = model.GetApprovalStatus(int.Parse(Id));
            
            return View(model);
        }

        public ActionResult Recommend(MISCApprovalPathModels model)
        {
            UpdateDetails(model);
            model.Recommend(model.MISCMaster.MISCMASTERID,LoginInfo.Current.strEmpID);
            model.Message = LMS.Util.Messages.GetSuccessMessage("Successfully recommended.");
            return View("Detail", model);
        }

        private static void UpdateDetails(MISCApprovalPathModels model)
        {
            MISCModels m = new MISCModels();
            m.LstMISCDetails = model.LstMISCDetails;
            MISCMaster master = new MISCMaster();
            master.MISCMASTERID = model.MISCMaster.MISCMASTERID;
            m.MISCMaster = master;
            model.Update(m);
        }


        public ActionResult Reverify(MISCApprovalPathModels model)
        {
            UpdateDetails(model);
            model.Reverify(model.MISCMaster.MISCMASTERID, LoginInfo.Current.strEmpID);
            model.Message = LMS.Util.Messages.GetSuccessMessage("Successfully submitted for reverification."); 
            return View("Detail",model);
        }

        public ActionResult Approve(MISCApprovalPathModels model)
        {
            UpdateDetails(model);
            model.Approve(model.MISCMaster.MISCMASTERID, LoginInfo.Current.strEmpID);
            model.Message = LMS.Util.Messages.GetSuccessMessage("Successfully approved.");
            return View("Detail", model);
        }
    }
}
