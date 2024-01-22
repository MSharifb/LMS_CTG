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
using System.Data.Odbc;


namespace LMS.Web.Controllers
{
    [NoCache]
    public class AttendanceReportController : Controller
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
        public ActionResult AttendanceReport(int? page)
        {
            AttendanceReportModels model = new AttendanceReportModels();
            try
            {
                model.LstAttendanceReportPaging = null;
            }
            catch (Exception ex)
            { model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString()); }

            return View(model);
        }



        ////POST: /Reports/AttendanceReport
        //[HttpPost]
        //[NoCache]
        //public ActionResult AttendanceReport(int? page, AttendanceReportModels model)
        //{
        //    int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
        //    try
        //    {
        //        model.strSortBy = "strEmpID";
        //        model.strSortType = LMS.Util.DataShortBy.ASC;
        //        model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
        //        model.maximumRows = AppConstant.PageSize20;
        //        model.LstAttendanceReport = model.GetAttendanceReport(model);
        //        model.LstAttendanceReportPaging = model.LstAttendanceReport.ToPagedList(currentPageIndex, AppConstant.PageSize20);
        //    }
        //    catch (Exception ex)
        //    { }
        //    return PartialView(LMS.Util.PartialViewName.rptAbsentEmployeeList, model);
        //}


        //POST: /Reports/ShowReport        
        [HttpPost]
        [NoCache]
        public ActionResult ShowReport(AttendanceReportModels model, int? page)
        {
            string strVP = "";
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                // odbc connection test ----------------
                ////OdbcConnection DbConnection = new OdbcConnection("DSN=AttendenceData");
                //OdbcConnection DbConnection = new System.Data.Odbc.OdbcConnection("DSN=Attendance;PWD=fdmsamho;");
                //string pass = "fdmsamho";
                //DbConnection.Open();
                //OdbcCommand DbCommand = DbConnection.CreateCommand();
                //DbCommand.CommandText = "SELECT * FROM tuser";
                //OdbcDataReader DbReader = DbCommand.ExecuteReader();
               
                //---- end -------------------------------
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                model.maximumRows = AppConstant.PageSize20;
                model.strSortBy = "strEmpID";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.LstAttendanceReport = model.GetAttendanceReport(model);
                model.LstAttendanceReportPaging = model.LstAttendanceReport.ToPagedList(currentPageIndex, AppConstant.PageSize20);
                //model.StrFromDate = Common.FormatDate(model.StrFromDate, "dd-MM-yyyy", "dd-MMM-yyyy");
                //model.StrToDate = Common.FormatDate(model.StrToDate, "dd-MM-yyyy", "dd-MMM-yyyy");
                ModelState.Clear();
                switch (model.strReportType)
                { 
                    case "EAS" :
                        strVP = LMS.Util.PartialViewName.rptEmployeeAttendanceStatus;
                        break;
                    case "LPE":
                        strVP = LMS.Util.PartialViewName.rptPresentEmployeeList;
                        break;
                    case "LAE":
                        strVP = LMS.Util.PartialViewName.rptAbsentEmployeeList;
                        break;
                    case "LOE":
                        strVP = LMS.Util.PartialViewName.rptOSDEmployeeList;
                        break;
                    case "LEOL":
                        strVP = LMS.Util.PartialViewName.rptEmployeeOnLeaveList;
                        break;
                    case "LEAE":
                        strVP = LMS.Util.PartialViewName.rptEarlyArrivalEmployeeList;
                        break;
                    case "LLAE":
                        strVP = LMS.Util.PartialViewName.rptLateArrivalEmployeeList;
                        break;
                    case "LEDE":
                        strVP = LMS.Util.PartialViewName.rptEarlyDepartureEmployeeList;
                        break;
                    case "LLDE":
                        strVP = LMS.Util.PartialViewName.rptLateDepartureEmployeeList;
                        break;
                    case "ESS":
                        strVP = LMS.Util.PartialViewName.rptEmployeeStatusSummary;
                        break;
                    case "EJC":
                        strVP = LMS.Util.PartialViewName.rptEmployeeJobCard;
                        break;
                    case "EWC":
                        strVP = LMS.Util.PartialViewName.rptEmployeeWorkingCalendar;
                        break;
                    case "OOC":
                        strVP = LMS.Util.PartialViewName.rptOutOfOfficeCompare;
                        break;

                    default:
                        break;
                }
                

            }
            catch (Exception ex)
            { }
            return PartialView(strVP, model);
        }


        //GET: /Reports/CheckHasData
        [NoCache]
        public JsonResult CheckHasData(AttendanceReportModels model, int? page)
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

                }
            }
            catch (Exception ex)
            { }
            return Json(IntDataCount);
        }


    }
}
