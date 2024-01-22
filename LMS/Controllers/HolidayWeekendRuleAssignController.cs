using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using MvcPaging;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class HolidayWeekendRuleAssignController : Controller
    {
        //GET: /HolidayWeekendRuleAssign/Index
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        //GET: /HolidayWeekendRuleAssign/HolidayWeekendRuleAssign
        [HttpGet]
        [NoCache]
        public ActionResult HolidayWeekendRuleAssign(int? page)
        {
            HolidayWeekendRuleAssignModels model = new HolidayWeekendRuleAssignModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.strSortBy = "intRuleAssignID";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
                model.maximumRows = AppConstant.PageSize;
                model.PageNumber = currentPageIndex + 1;
                model.HolidayWeekendRuleAssign = new HolidayWeekendRuleAssign();
                HolidayWeekendRuleAssign objSearch = model.HolidayWeekendRuleAssign;

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

                objSearch.intHolidayRuleID = model.intSearchRuleID;
                objSearch.strDepartmentID = model.strSearchDepartmentId;
                objSearch.strDesignationID = model.strSearchDesignationId;
                objSearch.strReligionID = model.strSearchReligionId;
                objSearch.intCategoryCode = model.intSearchCategoryId;

                model.GetHolidayWeekendRuleAssignAll(objSearch);
                model.LstHolidayWeekendRuleAssignPaging = model.LstHolidayWeekendRuleAssign.ToPagedList(currentPageIndex, AppConstant.PageSize);

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /HolidayWeekendRuleAssign/HolidayWeekendRuleAssign
        [HttpPost]
        [NoCache]
        public ActionResult HolidayWeekendRuleAssign(int? page, HolidayWeekendRuleAssignModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {

                model.strSortBy = "intRuleAssignID";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
                model.maximumRows = AppConstant.PageSize;
                model.PageNumber = currentPageIndex + 1;

                model.HolidayWeekendRuleAssign = new HolidayWeekendRuleAssign();
                HolidayWeekendRuleAssign objSearch = model.HolidayWeekendRuleAssign;

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

                objSearch.intHolidayRuleID = model.intSearchRuleID;
                objSearch.strDepartmentID = model.strSearchDepartmentId;
                objSearch.strDesignationID = model.strSearchDesignationId;
                objSearch.strReligionID = model.strSearchReligionId;
                objSearch.intCategoryCode = model.intSearchCategoryId;

                model.GetHolidayWeekendRuleAssignAll(objSearch);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.HolidayWeekendRuleAssign, model);
        }


        //GET: /HolidayWeekendRuleAssign/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {

            HolidayWeekendRuleAssignModels model = new HolidayWeekendRuleAssignModels();
            BLL.HolidayWeekendRuleAssignBLL objBLL = new LMS.BLL.HolidayWeekendRuleAssignBLL();

            try
            {
                model.HolidayWeekendRuleAssign = model.GetHolidayWeekendRuleAssign(Id);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(model);
        }


        //POST: /HolidayWeekendRuleAssign/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(HolidayWeekendRuleAssignModels model)
        {
            string strmessage = "";

            try
            {

                model.SaveData(model, out strmessage);

                if (strmessage.ToString() != "Successful")
                {
                    model.Message = Messages.GetErroMessage(strmessage);
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());

                    ModelState.Clear();
                }
                model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList = model.GetHolidayRuleDetails(model.HolidayWeekendRuleAssign.intHolidayRuleID);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotUpdate.ToString());
            }

            return View(model);
        }

        
        //POST: /HolidayWeekendRuleAssign/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(HolidayWeekendRuleAssignModels model)
        {
            string strmessage = "";

            int Id = model.HolidayWeekendRuleAssign.intRuleAssignID;

            try
            {
                model.Delete(Id, out strmessage);

                if (strmessage.ToString() != "Successful")
                {
                    model.Message = Messages.GetErroMessage(strmessage);
                    model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList = model.GetHolidayRuleDetails(model.HolidayWeekendRuleAssign.intHolidayRuleID);
                }
                else
                {
                    model.Message = Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.HolidayWeekendRuleAssign = new HolidayWeekendRuleAssign();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.HolidayWeekendRuleAssignDetails, model);
        }


        //GET: /HolidayWeekendRuleAssign/HolidayWeekendRuleAssignAdd
        [HttpGet]
        [NoCache]
        public ActionResult HolidayWeekendRuleAssignAdd(string id)
        {
            HolidayWeekendRuleAssignModels model = new HolidayWeekendRuleAssignModels();
            try
            {

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }
            return View(model);
        }

        
        //POST: /HolidayWeekendRuleAssign/HolidayWeekendRuleAssignAdd
        [HttpPost]
        [NoCache]
        public ActionResult HolidayWeekendRuleAssignAdd(HolidayWeekendRuleAssignModels model)
        {
            string strmessage = "";
            try
            {
                model.SaveData(model, out strmessage);

                if (strmessage.ToString() != "Successful")
                {
                    model.Message = Messages.GetErroMessage(strmessage.ToString());
                    model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList = model.GetHolidayRuleDetails(model.HolidayWeekendRuleAssign.intHolidayRuleID);
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.HolidayWeekendRuleAssign = new HolidayWeekendRuleAssign();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }
            return View(model);
        }


        //POST: /HolidayWeekendRuleAssign/Index
        [HttpPost]
        [NoCache]
        public ActionResult Index(HolidayWeekendRuleAssignModels model)
        {
            string strmessage = "";
            try
            {
                model.SaveData(model, out strmessage);
                if (strmessage.ToString() != "Successful")
                {
                    model.Message = Messages.GetErroMessage(strmessage.ToString());
                    return View(LMS.Util.PartialViewName.HolidayWeekendRuleAssignDetails, model);
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.HolidayWeekendRuleAssign = new HolidayWeekendRuleAssign();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }
            return View(model);
        }

        
        //POST: /HolidayWeekendRuleAssign/GetHolidayRuleListByYearId
        [HttpPost]
        [NoCache]
        public ActionResult GetHolidayRuleListByYearId(HolidayWeekendRuleAssignModels model)
        {
            try
            {
                model.IntLeaveYearID = model.HolidayWeekendRuleAssign.intLeaveYearID;
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(LMS.Util.PartialViewName.HolidayWeekendRuleAssignDetails, model);
        }


        // POST: /HolidayWeekendRuleAssign/GetHolidayRuleDetails
        [HttpPost]
        [NoCache]
        public ActionResult GetHolidayRuleDetails(HolidayWeekendRuleAssignModels model)
        {
            try
            {
                model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList = model.GetHolidayRuleDetails(model.HolidayWeekendRuleAssign.intHolidayRuleID);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(LMS.Util.PartialViewName.HolidayWeekendRuleAssignDetails, model);
        }


        // GET: /HolidayWeekendRuleAssign/OptionWisePageRefresh
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(HolidayWeekendRuleAssignModels model)
        {
            try
            {

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }
            return View(LMS.Util.PartialViewName.HolidayWeekendRuleAssignDetails, model);
        }


    }
}
