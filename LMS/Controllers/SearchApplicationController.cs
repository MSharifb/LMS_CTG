using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using MvcPaging;
using System.Collections;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class SearchApplicationController : Controller
    {
        //GET: /SearchApplication/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }
       
        
        //GET: /SearchApplication/SearchApplication
        [HttpGet]
        [NoCache]
        public ActionResult SearchApplication(int? page)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "dtApplyDate";
            model.strSortType = LMS.Util.DataShortBy.DESC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            model.LeaveApplication = new LeaveApplication();

            //model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            //model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID;
            //model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            //model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            //model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.StrSearchApplyFrom = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.StrSearchApplyTo = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.strSearchAuthorID = LoginInfo.Current.strEmpID;
            model.IsBulkApprove = false;

            List<ApprovalAuthor> objLstAuthor = new List<ApprovalAuthor>();
            objLstAuthor = model.GetApprovalAuthorStepsByAuthorID(LoginInfo.Current.strEmpID, 1);
          /*  if (objLstAuthor.Count > 0) Block For Central Approval System
            {
               
            }
            else
            {
                model.IsCanBulkApprove = false;
            }*/

            model.IsCanBulkApprove = true;

            LeaveApplication objSearch = model.LeaveApplication;
            SetSearchParamiters(objSearch, model);

            model.LstLeaveApplicationPaging = model.LstLeaveApplication.ToPagedList(currentPageIndex, AppConstant.PageSize);
            return View(model);
        }

        
        //POST: /SearchApplication/SearchApplication
        [HttpPost]
        [NoCache]
        public ActionResult SearchApplication(int? page, LeaveApplicationModels model)
        {
            int intPageSize = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "dtApplyDate";
            model.strSortType = LMS.Util.DataShortBy.DESC;

            if (model.IsBulkApprove == false)
            {
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
                model.maximumRows = AppConstant.PageSize;
                intPageSize = AppConstant.PageSize;
            }
            else
            {
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
                model.maximumRows = AppConstant.PageSize10;
                intPageSize = AppConstant.PageSize10;
            }
            
            model.LeaveApplication = new LeaveApplication();
            LeaveApplication objSearch = model.LeaveApplication;
            SetSearchParamiters(objSearch, model);

            model.LstLeaveApplicationPaging = model.LstLeaveApplication.ToPagedList(currentPageIndex, intPageSize);
            
            ModelState.Clear();
            return PartialView(LMS.Util.PartialViewName.SearchApplication, model);
        }

       
        //GET: /SearchApplication/GetLeaveYearInfo
        [NoCache]
        public JsonResult GetLeaveYearInfo(LeaveApplicationModels model)
        {
            ArrayList list = new ArrayList();
            LeaveYear objLvYear = new LeaveYear();
            try
            {
                if (model.intSearchLeaveYearId > 0)
                {
                    objLvYear = model.GetLeaveYear(model.intSearchLeaveYearId);
                }

                list.Add(objLvYear.strStartDate);
                list.Add(objLvYear.strEndDate);
                list.Add(objLvYear.strYearTitle);

            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }
        
        
        //GET: /SearchApplication/SaveApprovalFlow
        [NoCache]
        public JsonResult SaveApprovalFlow(LeaveApplicationModels model)
        {
            int intActionStatus = 0;
            string strMSG = "";
            try
            {
                if (model.LstLeaveApplication.Count > 0)
                {
                    for (int i = 0; i < model.LstLeaveApplication.Count; i++)
                    {
                        if (model.LstLeaveApplication[i].IsChecked == true)
                        {
                            ApprovalFlowModels modelAF = new ApprovalFlowModels();
                            modelAF.ApprovalFlow = modelAF.GetApprovalFlow(model.LstLeaveApplication[i].intAppFlowID, -1, -1, LoginInfo.Current.strEmpID, LoginInfo.Current.strCompanyID);

                            modelAF.LeaveApplication = modelAF.GetLeaveApplication(modelAF.ApprovalFlow.intApplicationID);

                            modelAF.ApprovalFlow.intAppStatusID = 1;
                            modelAF.ApprovalFlow.strComments = "Your application has been approved.";
                            modelAF.intNodeID = modelAF.ApprovalFlow.intNodeID;

                            ///* updated by mamun, on 10 May, 2001 */
                            ///* Since the application is pulled from db, hence the value of the duration is in hour.
                            // * So, it requires to convert into day*/
                            //if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                            //{
                            //    modelAF.LeaveApplication.fltDuration = modelAF.LeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
                            //    modelAF.LeaveApplication.fltWithPayDuration = modelAF.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                            //    modelAF.LeaveApplication.fltWithoutPayDuration = modelAF.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                            //}
                            //if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                            //{
                            //    modelAF.LeaveApplication.fltSubmittedDuration = modelAF.LeaveApplication.fltSubmittedDuration / LoginInfo.Current.fltOfficeTime;
                            //    modelAF.LeaveApplication.fltSubmittedWithPayDuration = modelAF.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime;
                            //    modelAF.LeaveApplication.fltSubmittedWithoutPayDuration = modelAF.LeaveApplication.fltSubmittedWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
                            //}
                            ///* end of update*/

                            modelAF.SaveData(modelAF, ref strMSG);

                            if (strMSG.ToString().Length > 0)
                            { break; }
                        }
                    }
                    if (strMSG.ToString().Length <= 0)
                    { intActionStatus = 1; }
                }
                else
                { intActionStatus = 2;}
            }
            catch (Exception ex)
            { }

            return Json(intActionStatus);
        }

        
        [NoCache]
        private void SetSearchParamiters(LeaveApplication objSearch, LeaveApplicationModels model)
        {
            if (!string.IsNullOrEmpty(model.strSearchInitial))
            { objSearch.strEmpInitial = model.strSearchInitial.Trim(); }
            else
            { objSearch.strEmpInitial = model.strSearchInitial; }

            if (!string.IsNullOrEmpty(model.strSearchName))
                {objSearch.strEmpName = model.strSearchName.Trim();}
            else
                {objSearch.strEmpName = model.strSearchName;}

            objSearch.strApplyFromDate = model.StrSearchApplyFrom == null ? model.StrYearStartDate : model.StrSearchApplyFrom;
            objSearch.strApplyToDate = model.StrSearchApplyTo == null ? model.StrYearEndDate : model.StrSearchApplyTo;
            //objSearch.intLeaveYearID = model.intSearchLeaveYearId;
            objSearch.intLeaveTypeID = model.intSearchLeaveTypeId;
            objSearch.strDepartmentID = model.StrSearchDepartmentId;
            objSearch.strDesignationID = model.StrSearchDesignationId;
            objSearch.intAppStatusID = model.IntSearchApplicationStatusId;
            objSearch.strAuthorID = LoginInfo.Current.strEmpID;

            model.LstLeaveApplication = null;
            model.LstLeaveApplication = model.GetLeaveApplicationPaging(objSearch, true, model.IsSearch, model.IsBulkApprove);
        }

        
        [NoCache]
        private void PopulateModel(ApprovalFlowModels model)
        {
            model.ApprovalFlow = model.GetApprovalFlow(model.ApprovalFlow.intAppFlowID, -1, -1, LoginInfo.Current.strEmpID, LoginInfo.Current.strCompanyID);

            model.LeaveApplication = model.GetLeaveApplication(model.ApprovalFlow.intApplicationID);

            //if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
            //{
            //    model.LeaveApplication.fltDuration = model.LeaveApplication.fltDuration / LoginInfo.Current.fltOfficeTime;
            //    model.LeaveApplication.fltWithPayDuration = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
            //    model.LeaveApplication.fltWithoutPayDuration = model.LeaveApplication.fltWithoutPayDuration / LoginInfo.Current.fltOfficeTime;
            //}

            model.LstLeaveLedger = model.GetLeaveLedgerHistory(model.LeaveApplication);

            model.intNodeID = model.ApprovalFlow.intNodeID;

            model.GetApprovalFlowComments(-1, model.LeaveApplication.intApplicationID, -1);

        }
    
    }
}
