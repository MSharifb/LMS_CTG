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
using System.Web.Script.Serialization;

namespace LMS.Web.Controllers
{
    public class LeaveYearMappingController : Controller
    {
        private SelectList _LeaveYear;
        private List<LeaveYear> lstLeaveYear;

        public List<LeaveYear> LstLeaveYear
        {
            get
            {
                if (lstLeaveYear == null)
                {
                    lstLeaveYear = new List<LeaveYear>();
                }
                return lstLeaveYear;
            }
            set { lstLeaveYear = value; }
        }

        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /LeaveType/LeaveType
        [HttpGet]
        [NoCache]
        public ActionResult LeaveYearMapping(int? page)
        {
            LeaveYearMappingModels model = new LeaveYearMappingModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.LeaveYearMapping.bitIsActiveYear = true;
                model.GetLeaveYearMappingAll(model.LeaveYearMapping);
                model.LstLeaveYearMappingPaging = model.LstLeaveYearMapping.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /LeaveType/LeaveType
        [HttpPost]
        [NoCache]
        public ActionResult LeaveYearMapping(int? page, LeaveYearMappingModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetLeaveYearMappingAll(model.LeaveYearMapping);
                model.LstLeaveYearMappingPaging = model.LstLeaveYearMapping.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveYearMapping, model);
        }

        //GET: /LeaveType/Details/
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveYearMappingModels model = new LeaveYearMappingModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            try
            {
                model.LeaveYearMapping = model.GetLeaveYearMapping(Id);
                GetDropDownEdit(model);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /LeaveType/Details 
        [HttpPost]
        [NoCache]
        public ActionResult Details(LeaveYearMappingModels model)
        {
            string strmsg = "";
            try
            {

                int id = model.SaveData(model, ref strmsg);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    if (strmsg.ToString().Length > 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(strmsg);
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

            GetDropDownEdit(model);

            return View(model);
        }


        //POST: /LeaveType/Delete 
        [HttpPost]
        [NoCache]
        public ActionResult Delete(LeaveYearMappingModels model, FormCollection fc)
        {

            try
            {
                int id = model.Delete(model.LeaveYearMapping.intLeaveYearMapID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.LeaveYearMapping = new LeaveYearMapping();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            List<SelectListItem> itemList = new List<SelectListItem>();
            model.LeaveYearList = new SelectList(itemList, "Value", "Text");

            return PartialView(LMS.Util.PartialViewName.LeaveYearMappingDetails, model);
        }


        //GET: /LeaveType/LeaveTypeAdd 
        [HttpGet]
        [NoCache]
        public ActionResult LeaveYearMappingAdd(string id)
        {
            LeaveYearMappingModels model = new LeaveYearMappingModels();
            List<SelectListItem> itemList = new List<SelectListItem>();
            model.LeaveYearList = new SelectList(itemList, "Value", "Text");

            try
            {
                model.Message = Util.Messages.GetSuccessMessage("");
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /LeaveType/LeaveTypeAdd 
        [HttpPost]
        [NoCache]
        public ActionResult LeaveYearMappingAdd(LeaveYearMappingModels model)
        {
            string strmsg = "";
            try
            {
                int id = model.SaveData(model, ref strmsg);
                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    if (strmsg.ToString().Length > 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(strmsg);
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.LeaveYearMapping = new LeaveYearMapping();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            List<SelectListItem> itemList = new List<SelectListItem>();
            model.LeaveYearList = new SelectList(itemList, "Value", "Text");

            return View(model);
        }


        //POST: /LeaveType/SaveLeaveType
        [HttpPost]
        [NoCache]
        public ActionResult SaveLeaveYearMapping(LeaveYearMappingModels model)
        {
            string strmsg = "";
            try
            {
                int id = model.SaveData(model, ref strmsg);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    if (strmsg.ToString().Length > 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(strmsg);
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.LeaveYearMapping = new LeaveYearMapping();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveYearMappingDetails, model);
        }


        //POST: /LeaveType/Create
        [HttpPost]
        [NoCache]
        public ActionResult Create(FormCollection collection)
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


        //GET: /LeaveType/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }


        //POST: /LeaveType/Edit/5
        [HttpPost]
        [NoCache]
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


        //POST: /LeaveType/OptionWisePageRefresh 
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(LeaveYearMappingModels model)
        {
            try
            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        public JsonResult GetDropDown(int Id)
        {
            JsonResult result = new JsonResult();
            var listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "...Select One...", Value = "" });
            var leaveType = Common.fetchLeaveType().Where(x => x.intLeaveTypeID == Id).FirstOrDefault();
            
            if (leaveType != null)
            {
                var leaveYear = Common.fetchLeaveYear().Where(y => y.intLeaveYearTypeId == leaveType.intLeaveYearTypeId).ToList();

                foreach (var c in leaveYear)
                {
                    var listItem = new SelectListItem { Text = c.strYearTitle, Value = c.intLeaveYearID.ToString() };
                    listItems.Add(listItem);
                }
            }
            result.Data = listItems.ToList();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        [NoCache]
        public JsonResult GetStartEndDate(int intLeaveYearId)
        {
            var startDate = string.Empty;
            var endDate = string.Empty;

            try
            {
                if (intLeaveYearId != 0)
                {
                    var leaveYear = Common.fetchLeaveYear().Where(y => y.intLeaveYearID == intLeaveYearId).FirstOrDefault();
                    if (leaveYear != null)
                    {
                        startDate = leaveYear.strStartDate;
                        endDate = leaveYear.strEndDate;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { startDate = startDate, endDate = endDate }, JsonRequestBehavior.AllowGet);
        }


        public LeaveYearMappingModels GetDropDownEdit(LeaveYearMappingModels model)
        {
            var listItems = new List<SelectListItem>();
            var leaveType = Common.fetchLeaveType().Where(x => x.intLeaveTypeID == model.LeaveYearMapping.intLeaveTypeID).FirstOrDefault();

            if (leaveType != null)
            {
                var leaveYearList = Common.fetchLeaveYear().Where(y => y.intLeaveYearTypeId == leaveType.intLeaveYearTypeId).ToList();

                foreach (var c in leaveYearList)
                {
                    var listItem = new SelectListItem { Text = c.strYearTitle, Value = c.intLeaveYearID.ToString() };
                    listItems.Add(listItem);
                }
                model.LeaveYearList = new SelectList(listItems, "Value", "Text");
            }

            var leaveYear = Common.fetchLeaveYear().Where(y => y.intLeaveYearID == model.LeaveYearMapping.intLeaveYearId).FirstOrDefault();

            model.LeaveYearMapping.strStartDate =leaveYear.dtStartDate.ToString("dd-MM-yyyy");
            model.LeaveYearMapping.strEndDate = leaveYear.dtEndDate.ToString("dd-MM-yyyy");

            return model;
        }

    }
}
