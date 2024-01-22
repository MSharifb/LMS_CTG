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
    public class DashboardController : Controller
    {
        //GET: /Dashboard/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /Dashboard/Dashboard
        [HttpGet]
        [NoCache]
        public ActionResult Dashboard(int? page)
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
                model.strSortBy = "strEmpID,strLeaveType";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.startRowIndex = 1;
                model.maximumRows = 10;
                model.IntLeaveYearId = 190;
                model.LstRptLeaveStatus = model.GetLeaveStatus(model);
            }
            catch (Exception ex)
            { }
            return View(model);
        }

        /*
        //POST: /Dashboard/Dashboard
        [HttpPost]
        [NoCache]
        public ActionResult Dashboard(int? page, DashboardModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.Get(-1,"", model.intSearchStatus);
                model.LstDashboardPaging = model.LstDashboard.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.Dashboard, model);
        }


        //GET: /Dashboard/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            DashboardModels model = new DashboardModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.Dashboard = model.Get(Id);
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /Dashboard/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(DashboardModels model)
        {
            try
            {
                if (CheckValidation(model, true) == true)
                {
                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());

                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);

        }

        //*/        
    
    }
}
