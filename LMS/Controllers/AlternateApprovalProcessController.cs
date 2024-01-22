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

namespace LMS.Web.Controllers
{
    [NoCache]
    public class AlternateApprovalProcessController : Controller
    {

        //GET: /AlternateApprovalProcess/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        

        //GET: /AlternateApprovalProcess/AlternateApprovalProcess
        [HttpGet]
        [NoCache]
        public ActionResult AlternateApprovalProcess(int? page)
        {
            LeaveApplicationModels model = SetPaging(page);
            ModelState.Clear();
            HttpContext.Response.Clear();
            return View(model);
        }


        //POST: /AlternateApprovalProcess/AlternateApprovalProcess
        [HttpPost]
        [NoCache]
        public ActionResult AlternateApprovalProcess(int? page, LeaveApplicationModels model)
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
            return PartialView(LMS.Util.PartialViewName.AlternateApprovalProcess, model);
        }

        [NoCache]
        public ActionResult GetLedger(LeaveApplicationModels model)
        {
            model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, false);
            return PartialView(LMS.Util.PartialViewName.AlternateApprovalProcessDetails, model);
        }
        

        //POST: /AlternateApprovalProcess/getPagedData
        [HttpPost]
        [NoCache]
        public ActionResult getPagedData(FormCollection collection)
        {
            string strPageIndex = collection.Get("txtPageNo");

            int pageNo = 1;
            int.TryParse(strPageIndex, out pageNo);
            if (pageNo < 1)
            {
                pageNo = 1;
            }

            LeaveApplicationModels model = new LeaveApplicationModels();

            model.GetLeaveApplicationAll();
            model.LstLeaveApplication1 = model.LstLeaveApplication.AsPagination(pageNo, AppConstant.PageSize);
            return PartialView(LMS.Util.PartialViewName.AlternateApprovalProcess, model);
        }


        //GET: /AlternateApprovalProcess/Details/5
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();
            model.Message = Messages.GetSuccessMessage("");

            try
            {
                GetDetails(model, Id);
            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        //POST: /AlternateApprovalProcess/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                i = model.SaveData(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = strmsg.ToString();
                }

                

                else
                {
                    model.GetLeaveApplicationAll();
                    model.LstLeaveApplication1 = model.LstLeaveApplication.AsPagination(1, AppConstant.PageSize);

                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.LeaveApplication = new LeaveApplication();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetSuccessMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        //POST: /AlternateApprovalProcess/AlternateDelete
        [HttpPost]
        [NoCache]
        public ActionResult AlternateDelete(LeaveApplicationModels model)
        {

            int i = 0;
            string strmsg = "";

            try
            {
                i = model.ALternateDelete(model.LeaveApplication.intApplicationID, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg);
                }
                else
                {
                    model = new LeaveApplicationModels();
                    model.Message = Util.Messages.GetSuccessMessage("Application Deleted Successfully.");

                    model.LeaveApplication = new LeaveApplication();
                    model.LeaveApplication.strApplyDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
                    model.LeaveApplication.strApplyFromDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
                    model.LeaveApplication.strApplyToDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);

                    model.LeaveApplication.strSubmittedApplyFromDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
                    model.LeaveApplication.strSubmittedApplyToDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);

                    model.LeaveApplication.strApplicationType = "";

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.AlternateApprovalProcessDetails, model);

        }


        //POST: /AlternateApprovalProcess/AlternateApprove
        [HttpPost]
        [NoCache]
        public ActionResult AlternateApprove(LeaveApplicationModels model)
        {
            int i = 0,intAppID = 0;
            string strmsg = "";
            try
            {
                intAppID = (int)model.LeaveApplication.intApplicationID;
                model.LeaveApplication.strRemarks = model.ApprovalFlow.strComments;

                i = model.AlternateApprove(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg);
                }
                else
                {                    
                    //model.LeaveApplication = new LeaveApplication();
                    model = new LeaveApplicationModels();
                    GetDetails(model, intAppID);
                    model.Message = Util.Messages.GetSuccessMessage("Application Approved Successfully.");                   
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.AlternateApprovalProcessDetails, model);
        }


        //POST: /AlternateApprovalProcess/AlternateRecommend
        [HttpPost]
        [NoCache]
        public ActionResult AlternateRecommend(LeaveApplicationModels model)
        {
            int i = 0,intAppID = 0;

            string strmsg = "";
            try
            {
                intAppID = (int)model.LeaveApplication.intApplicationID;                

                i = model.AlternateRecommend(model, out strmsg);               

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg);
                }
                else
                {                   
                    //model.LeaveApplication = new LeaveApplication();
                    model = new LeaveApplicationModels();
                    GetDetails(model, intAppID);
                    model.Message = Util.Messages.GetSuccessMessage("Application recommended successfully");
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.AlternateApprovalProcessDetails, model);

        }

        //POST: /AlternateApprovalProcess/AlternateReject
        [HttpPost]
        [NoCache]
        public ActionResult AlternateReject(LeaveApplicationModels model)
        {
            int i = 0, intAppID = 0;
            string strmsg = "";
            try
            {
                intAppID = (int)model.LeaveApplication.intApplicationID;
                model.LeaveApplication.strRemarks = model.ApprovalFlow.strComments;

                i = model.ALternateReject(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg);
                }
                else
                {
                    //model.LeaveApplication = new LeaveApplication();
                    model = new LeaveApplicationModels();
                    GetDetails(model, intAppID);
                    model.Message = Util.Messages.GetSuccessMessage("Application Rejected Successfully");
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.AlternateApprovalProcessDetails, model);
        }

        //POST: /AlternateApprovalProcess/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(FormCollection fc)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();

            int Id = int.Parse(fc.Get("intLeaveApplicationID").ToString());

            try
            {

                string strPageIndex = fc.Get("txtPageNo");

                int pageNo = 1;
                int.TryParse(strPageIndex, out pageNo);
                if (pageNo < 1)
                {
                    pageNo = 1;
                }

                model.GetLeaveApplicationAll();
                model.LstLeaveApplication1 = model.LstLeaveApplication.AsPagination(pageNo, AppConstant.PageSize);

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(LMS.Util.PartialViewName.AlternateApprovalProcess, model);
        }


        //GET: /AlternateApprovalProcess/AlternateApprovalProcessAdd
        [HttpGet]
        public ActionResult AlternateApprovalProcessAdd(string id)
        {

            LeaveApplicationModels model = new LeaveApplicationModels();

            model.LeaveApplication.dtApplyDate = DateTime.Today;
            model.LeaveApplication.strApplyFromDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.strApplyToDate = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.LeaveApplication.bitIsApprovalProcess = true;
            model.LeaveApplication.bitIsDiscard = false;
            model.LeaveApplication.strApplicationType = LMS.Util.LeaveDurationType.FullDay;
            model.LeaveApplication.IsFullDay = true;

            model.LeaveApplication.intAppStatusID = 6;  //Submit
            model.LeaveApplication.strEmpID = LoginInfo.Current.strEmpID;
            model.LeaveApplication.strEmpName = LoginInfo.Current.EmployeeName;

            model.LeaveApplication.intLeaveYearID = LoginInfo.Current.intLeaveYearID;
            model.LeaveApplication.strSupervisorID = "";

            model.intNodeID = LoginInfo.Current.intDestNodeID;

            model.LeaveApplication.intDestNodeID = LoginInfo.Current.intDestNodeID;


            try
            {


            }
            catch (Exception ex)
            {
                //ViewData["vdMsg"] = Messages.GetErroMessage(ex.Message);
            }

            return View(model);
        }

        
        //POST: /AlternateApprovalProcess/LeaveApplicationAdd
        [HttpPost]
        public ActionResult LeaveApplicationAdd(LeaveApplicationModels model)
        {
            int i = 0;
            string strmsg = "";
            try
            {
                i = model.SaveData(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg);
                }
                else
                {
                    model.GetLeaveApplicationAll();
                    model.LstLeaveApplication1 = model.LstLeaveApplication.AsPagination(1, AppConstant.PageSize);

                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.LeaveApplication = new LeaveApplication();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetSuccessMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /AlternateApprovalProcess/SaveLeaveApplication
        [HttpPost]
        public ActionResult SaveLeaveApplication(LeaveApplicationModels model, int Id)
        {
            int i = 0;
            string strmsg = "";
            try
            {

                model.LeaveApplication.intAppStatusID = Id;

                i = model.SaveData(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = strmsg.ToString();
                }
                else
                {
                    model.GetLeaveApplicationAll();
                    model.LstLeaveApplication1 = model.LstLeaveApplication.AsPagination(1, AppConstant.PageSize);

                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.LeaveApplication = new LeaveApplication();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetSuccessMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);

        }



        //POST: /AlternateApprovalProcess/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: /AlternateApprovalProcess/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //POST: /AlternateApprovalProcess/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private LeaveApplicationModels SetPaging(int? page)
        {
            LeaveApplicationModels model = new LeaveApplicationModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "dtApplyDate";
            model.strSortType = LMS.Util.DataShortBy.DESC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;

            model.LeaveApplication = new LeaveApplication();
            LeaveApplication objSearch = model.LeaveApplication;

            //model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            //model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID;
            //model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            //model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            //model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;

            model.StrSearchApplyFrom = DateTime.Parse(DateTime.Today.Month.ToString() + "-01-" + DateTime.Today.Year.ToString()).ToString(LMS.Util.DateTimeFormat.Date);
            model.StrSearchApplyTo = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);

            SetSearchParamiters(objSearch, model);

            model.LstLeaveApplicationPaging = model.LstLeaveApplication.ToPagedList(currentPageIndex, AppConstant.PageSize);

            return model;
        }

        private void SetSearchParamiters(LeaveApplication objSearch, LeaveApplicationModels model)
        {
            if (!string.IsNullOrEmpty(model.strSearchInitial))
            {
                objSearch.strEmpInitial = model.strSearchInitial.Trim();
            }
            else
            {
                objSearch.strEmpInitial = model.strSearchInitial;
            }

            if (!string.IsNullOrEmpty(model.strSearchName))
            {
                objSearch.strEmpName = model.strSearchName.Trim();
            }
            else
            {
                objSearch.strEmpName = model.strSearchName;
            }

            objSearch.strApplyFromDate = model.StrSearchApplyFrom == null ? model.StrYearStartDate : model.StrSearchApplyFrom;
            objSearch.strApplyToDate = model.StrSearchApplyTo == null ? model.StrYearEndDate : model.StrSearchApplyTo;
            objSearch.intLeaveYearID = model.intSearchLeaveYearId;
            objSearch.intLeaveTypeID = model.intSearchLeaveTypeId;
            objSearch.strDepartmentID = model.StrSearchDepartmentId;
            objSearch.strDesignationID = model.StrSearchDesignationId;
            objSearch.intAppStatusID = model.IntSearchApplicationStatusId;
            objSearch.bitIsForAlternateProcess = true;
            objSearch.strApprovalProcess = "";
            objSearch.strAuthorID = "";

            model.LstLeaveApplication = null;
            if (model.IsBulkApprove == false)
            {
                model.LstLeaveApplication = model.GetLeaveApplicationPaging(objSearch, false);
            }
            else
            {
                model.LstLeaveApplication = model.GetLeaveApplicationPaging(objSearch, true, model.IsSearch, true);
            }
        }
        private void GetDetails(LeaveApplicationModels model, int Id)
        {
            model.ApprovalFlowList = model.GetApprovalFlowList(Id);
            model.LeaveApplication = model.GetLeaveApplication(Id);
            InitializedModel(model);
            LeaveLedger objLvLedger = new LeaveLedger();
            objLvLedger = model.GetLeaveStatus(model.LeaveApplication, false).Where(c => c.intLeaveTypeID == model.LeaveApplication.intLeaveTypeID).SingleOrDefault();
            //if (objLvLedger != null)
            //{
            //    model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - objLvLedger.fltSubmitted;
            //}
            double fltDays = 0;
            double fltSubVal = 0;
            if (objLvLedger != null)
            {
                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                {
                    fltDays = model.LeaveApplication.fltWithPayDuration / LoginInfo.Current.fltOfficeTime;
                    if (model.LeaveApplication.strSubmittedApplicationType != LMS.Util.LeaveDurationType.Hourly)
                    { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration; }
                    else
                    { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime; }
                }
                else
                {
                    fltDays = model.LeaveApplication.fltWithPayDuration;
                    if (model.LeaveApplication.strSubmittedApplicationType == LMS.Util.LeaveDurationType.Hourly)
                    { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration / LoginInfo.Current.fltOfficeTime; }
                    else
                    { fltSubVal = model.LeaveApplication.fltSubmittedWithPayDuration; }
                }
                model.LeaveApplication.fltNetBalance = objLvLedger.fltCB - ((objLvLedger.fltSubmitted - fltSubVal) + fltDays);
                model.LeaveApplication.fltNetBalance = Math.Round(model.LeaveApplication.fltNetBalance, 2);
            }
        }

        private void InitializedModel(LeaveApplicationModels model)
        {
            if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
            {
                model.LeaveApplication.IsFullDayHalfDay = false;
                model.LeaveApplication.IsHourly = true;
                model.LeaveApplication.IsFullDay = false;
            }
            else if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
            {
                model.LeaveApplication.IsFullDayHalfDay = true;
                model.LeaveApplication.IsHourly = false;
                model.LeaveApplication.IsFullDay = false;
            }
            else
            {
                model.LeaveApplication.IsFullDayHalfDay = false;
                model.LeaveApplication.IsHourly = false;
                model.LeaveApplication.IsFullDay = true;
            }


            //--add and block by shaiful 02-June-2011---------------------------------------
            ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
            try
            {
                model.ApprovalFlow = model.GetApprovalFlow(model.ApprovalFlowList.OrderByDescending(c => c.intAppFlowID).First().intAppFlowID);
                model.intNodeID = objApprovalPathBLL.ApprovalPathDetailsGet(-1, model.ApprovalFlow.intNodeID, -1, LoginInfo.Current.strCompanyID)[0].intNodeID;
            }
            catch (Exception ex)
            { model.intNodeID = 0; }            
            //--------------------------------------------------------------------------
            
            model.LstLeaveLedger = model.GetLeaveStatus(model.LeaveApplication, true);
            model.GetApprovalFlowComments(model.LeaveApplication.intApplicationID);

            //model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            //model.intSearchLeaveYearId = LoginInfo.Current.intLeaveYearID;
            //model.StrSearchLeaveYear = model.ActiveLeaveYear.strYearTitle;
            //model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;
            //model.StrYearEndDate = model.ActiveLeaveYear.strEndDate;
        }
               
        public JsonResult CalcutateDuration(LeaveApplicationModels model)
        {
            double fltDuration = 0;
            try
            {
                BLL.LeaveApplicationBLL objBLL = new BLL.LeaveApplicationBLL();
                DateTime dtFromDateTime = model.LeaveApplication.dtApplyFromDate;
                DateTime dtToDateTime = model.LeaveApplication.dtApplyToDate;
                if (model.LeaveApplication.strApplicationType != LMS.Util.LeaveDurationType.FullDay)
                {
                    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                    dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
                    dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;

                    char[] sepAr = { '.', ' ' };

                    if (!string.IsNullOrEmpty(model.LeaveApplication.strApplyFromTime))
                    {
                        string[] time = model.LeaveApplication.strApplyFromTime.Split(sepAr);

                        try
                        {
                            string strDt = model.LeaveApplication.dtApplyFromDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                            dtFromDateTime = Convert.ToDateTime(strDt, dtfi);
                        }
                        catch (Exception ex)
                        { }
                    }
                    if (!string.IsNullOrEmpty(model.LeaveApplication.strApplyToTime))
                    {
                        string[] time = model.LeaveApplication.strApplyToTime.Split(sepAr);
                        try
                        {
                            string strDt = model.LeaveApplication.dtApplyToDate.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                            dtToDateTime = Convert.ToDateTime(strDt, dtfi);
                        }
                        catch (Exception ex)
                        { }
                    }
                }


                fltDuration = objBLL.GetDuration(model.LeaveApplication.strEmpID, model.LeaveApplication.intLeaveYearID, model.LeaveApplication.intLeaveTypeID, model.LeaveApplication.strApplicationType, model.LeaveApplication.dtApplyFromDate, model.LeaveApplication.dtApplyToDate, LoginInfo.Current.strCompanyID);

                if (model.LeaveApplication.strApplicationType == LMS.Util.LeaveDurationType.Hourly)
                {
                    if (fltDuration > 0)
                    {

                        TimeSpan ts1 = dtToDateTime.Subtract(dtFromDateTime);
                        fltDuration = ts1.TotalHours;
                    }
                }

                model.LeaveApplication.fltDuration = fltDuration;
                model.LeaveApplication.fltWithPayDuration = fltDuration;
                model.LeaveApplication.fltWithoutPayDuration = 0;

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return Json(fltDuration);
        }

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
                            GetDetails(model, (int)model.LstLeaveApplication[i].intApplicationID);
                            model.LeaveApplication.strRemarks = "Your application has been approved.";
                            model.AlternateApprove(model, out strMSG,model.IsSendEmailToAuthority);

                            if (strMSG.ToString().Length > 0)
                            { break; }
                        }
                    }
                    if (strMSG.ToString().Length <= 0)
                    {intActionStatus = 1;}
                }
                else
                {intActionStatus = 2;}
            }
            catch (Exception ex)
            {}
            return Json(intActionStatus);
        }

    
    
    }
}