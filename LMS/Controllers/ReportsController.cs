using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Collections;
using System.Globalization;
using MvcContrib.Pagination;
using LMSEntity;
using LMS.Web;
using LMS.Web.Models;
using LMS.Util;
using MvcPaging;
using System.IO;
using System.Web.UI;


namespace LMS.Web.Controllers
{
    [NoCache]
    public class ReportsController : Controller
    {
        // GET: /Reports/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /Reports/Reports
        [HttpGet]
        [NoCache]
        public ActionResult Reports()
        {
            ReportsModels model = new ReportsModels();
            try
            {               
                model.IsApplyDate = true;                
            }
            catch (Exception ex)
            { }
            
            return PartialView(LMS.Util.PartialViewName.Reports, model);
        }

        
        //GET: /Reports/MyLeaveStatus
        [HttpGet]
        [NoCache]
        public ActionResult MyLeaveStatus()
        {
            ReportsModels model = new ReportsModels();
            try
            {
                model.StrEmpId = LoginInfo.Current.strEmpID;
                model.StrEmpName = LoginInfo.Current.EmployeeName;
                model.ReportId = LMS.Util.ReportId.LeaveStatus;
                model.IsFromMyLeaveMenu = true;
                model.IsIndividual = true;
                model.bitIsExcel = false;
            }
            catch (Exception ex)
            { }
            return PartialView(LMS.Util.PartialViewName.MyLeaveStatus, model);
        }

        //POST: /Reports/RptLeaveStatus 
        [HttpPost]
        [NoCache]
        //[System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult RptLeaveStatus(int? page, ReportsModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.strSortBy = "strEmpID,strLeaveType";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                model.maximumRows = AppConstant.PageSize20;
                model.LstRptLeaveStatus = model.GetLeaveStatus(model);
                model.LstRptLeaveStatusPaging = model.LstRptLeaveStatus.ToPagedList(currentPageIndex, AppConstant.PageSize20);
            }
            catch (Exception ex)
            { }
            return PartialView(LMS.Util.PartialViewName.rptLeaveStatus, model);
        }

        
        //POST: /Reports/RptLeaveEnjoyed 
        [HttpPost]
        [NoCache]
        public ActionResult RptLeaveEnjoyed(int? page, ReportsModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.strSortBy = "strEmpID,strLeaveType,dtApplyFromDate";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                model.maximumRows = AppConstant.PageSize20;
                model.LstRptLeaveEnjoyed = model.GetLeaveEnjoyed(model);
                model.LstRptLeaveEnjoyedPaging = model.LstRptLeaveEnjoyed.ToPagedList(currentPageIndex, AppConstant.PageSize20);
            }
            catch (Exception ex)
            { }
            return PartialView(LMS.Util.PartialViewName.rptLeaveEnjoyed, model);
        }

        //POST: /Reports/RptLeaveEncasment 
        [HttpPost]
        [NoCache]
        public ActionResult RptLeaveEncasment(int? page, ReportsModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.strSortBy = "strEmpID,strLeaveType";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                model.maximumRows = AppConstant.PageSize20;
                model.LstRptLeaveEncasment = model.GetLeaveEncasment(model);
                model.LstRptLeaveEncasmentPaging = model.LstRptLeaveEncasment.ToPagedList(currentPageIndex, AppConstant.PageSize20);
            }
            catch (Exception ex)
            { }
            return PartialView(LMS.Util.PartialViewName.rptLeaveEncasment, model);
        }

        
        //POST: /Reports/ShowReport        
        [HttpPost]
        [NoCache]
        public ActionResult ShowReport(ReportsModels model, int? page)
        {
            string strVP = "";
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                ModelState.Clear();

                if (model.ReportId == LMS.Util.ReportId.LeaveEncashment)
                {
                    model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                    model.maximumRows = AppConstant.PageSize20;
                    model.strSortBy = "strEmpID,strLeaveType";
                    model.strSortType = LMS.Util.DataShortBy.ASC;
                    model.LstRptLeaveEncasment = model.GetLeaveEncasment(model);
                    model.LstRptLeaveEncasmentPaging = model.LstRptLeaveEncasment.ToPagedList(currentPageIndex, AppConstant.PageSize20);
                    strVP = LMS.Util.PartialViewName.rptLeaveEncasment;
                }
                else if (model.ReportId == LMS.Util.ReportId.LeaveStatus)
                {
                    model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                    model.maximumRows = AppConstant.PageSize20;
                    model.strSortBy = "strEmpID,strLeaveType";
                    model.strSortType = LMS.Util.DataShortBy.ASC;
                    model.LstRptLeaveStatus = model.GetLeaveStatus(model);
                    model.LstRptLeaveStatusPaging = model.LstRptLeaveStatus.ToPagedList(currentPageIndex, AppConstant.PageSize20);
                    strVP = LMS.Util.PartialViewName.rptLeaveStatus;
                }
                else if (model.ReportId == LMS.Util.ReportId.LeaveAvailed)
                {
                    model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                    model.maximumRows = AppConstant.PageSize20;
                    model.strSortBy = "strEmpID,strLeaveType,dtApplyFromDate";
                    model.strSortType = LMS.Util.DataShortBy.ASC;
                    model.LstRptLeaveEnjoyed = model.GetLeaveEnjoyed(model);
                    model.LstRptLeaveEnjoyedPaging = model.LstRptLeaveEnjoyed.ToPagedList(currentPageIndex, AppConstant.PageSize20);
                    strVP = LMS.Util.PartialViewName.rptLeaveEnjoyed;
                }
            }
            catch (Exception ex)
            { }
            return PartialView(strVP, model);
        }

        
        //GET: /Reports/CheckHasData
        [NoCache]
        public JsonResult CheckHasData(ReportsModels model, int? page)
        {
            int IntDataCount = 0;
            try
            {
                if (model.StrFromDate != null && model.StrFromDate != "" && model.StrToDate != null && model.StrToDate != "")
                {
                    if (DateTime.Parse(model.StrFromDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.StrToDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None))
                    {
                        IntDataCount = -1;
                        //To date must be greater than From date.
                        return Json(IntDataCount);
                    }

                    if (DateTime.Parse(model.StrYearStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.StrFromDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) || DateTime.Parse(model.StrFromDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.StrYearEndDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None))
                    {
                        IntDataCount = -2;
                        //From date must be within selected leave year.
                        return Json(IntDataCount);
                    }

                    if (DateTime.Parse(model.StrYearStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.StrToDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) || DateTime.Parse(model.StrToDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.StrYearEndDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None))
                    {
                        IntDataCount = -3;
                        //To date must be within selected leave year.
                        return Json(IntDataCount);
                    }
                }
            }
            catch (Exception ex)
            { }
            return Json(IntDataCount);
        }

        //GET: /Reports/GetLeaveYearInfo
        [NoCache]
        public JsonResult GetLeaveYearInfo(ReportsModels model)
        {
            ArrayList list = new ArrayList();
            LeaveYear objLvYear = new LeaveYear();
            try
            {
                if (model.IntLeaveYearId > 0)
                {
                    objLvYear = model.GetLeaveYear(model.IntLeaveYearId);
                }

                list.Add(objLvYear.strStartDate);
                list.Add(objLvYear.strEndDate);
            }
            catch (Exception ex)
            {}            
            return Json(list);
        }


      

        


    }
}
