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
    public class WeekendHolidayAsWorkingdayController : Controller
    {
        //GET: /WeekendHolidayAsWorkingday/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        //GET: /WeekendHolidayAsWorkingday/WeekendHolidayAsWorkingday
        [HttpGet]
        [NoCache]
        public ActionResult WeekendHolidayAsWorkingday(int? page)
        {
            WeekendHolidayAsWorkingdayModels model = new  WeekendHolidayAsWorkingdayModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.WeekendHolidayAsWorkingdayGetAll();
                InitializeModel(model);
                model.LstWeekendHolidayAsWorkingdayPaged = model.LstWeekendHolidayAsWorkingday.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /WeekendHolidayAsWorkingday/WeekendHolidayAsWorkingday
        [HttpPost]
        [NoCache]
        public ActionResult WeekendHolidayAsWorkingday(int? page, WeekendHolidayAsWorkingdayModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.LstWeekendHolidayAsWorkingday = model.GetWeekendHolidayAsWorkingdayPaging(model);
                model.LstWeekendHolidayAsWorkingdayPaged = model.LstWeekendHolidayAsWorkingday.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.WeekendHolidayAsWorkingday, model);
        }


        //POST: /WeekendHolidayAsWorkingday/OptionWisePageRefresh 
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(WeekendHolidayAsWorkingdayModels model)
        {
            try
            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //GET: /WeekendHolidayAsWorkingday/Details/
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            WeekendHolidayAsWorkingdayModels model = new WeekendHolidayAsWorkingdayModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            try
            {
                model.WeekendHolidayAsWorkingday = model.WeekendHolidayAsWorkingdayGetByID(Id);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /WeekendHolidayAsWorkingday/Details 
        [HttpPost]
        [NoCache]
        public ActionResult Details(WeekendHolidayAsWorkingdayModels model)
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

            return View(model);
        }


        //POST: /WeekendHolidayAsWorkingday/Delete 
        [HttpPost]
        [NoCache]
        public ActionResult Delete(WeekendHolidayAsWorkingdayModels model, FormCollection fc)
        {

            try
            {
                int id = model.Delete(model.WeekendHolidayAsWorkingday.intWeekendWorkingday);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.WeekendHolidayAsWorkingday = new WeekendHolidayAsWorkingday();
                    InitializeModel(model);
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.WeekendHolidayAsWorkingdayDetails, model);
        }


        //GET: /WeekendHolidayAsWorkingday/WeekendHolidayAsWorkingdayAdd 
        [HttpGet]
        [NoCache]
        public ActionResult WeekendHolidayAsWorkingdayAdd(Int32? id)
        {
            WeekendHolidayAsWorkingdayModels model = new WeekendHolidayAsWorkingdayModels();
            try
            {
                InitializeModel(model);

                model.Message = Util.Messages.GetSuccessMessage("");
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /WeekendHolidayAsWorkingday/WeekendHolidayAsWorkingdayAdd 
        [HttpPost]
        [NoCache]
        public ActionResult WeekendHolidayAsWorkingdayAdd(WeekendHolidayAsWorkingdayModels model)
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
                        model.WeekendHolidayAsWorkingday = new WeekendHolidayAsWorkingday();
                        InitializeModel(model);
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


        //POST: /WeekendHolidayAsWorkingday/SaveWeekendHolidayAsWorkingday
        [HttpPost]
        [NoCache]
        public ActionResult SaveWeekendHolidayAsWorkingday(WeekendHolidayAsWorkingdayModels model)
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
                        model.WeekendHolidayAsWorkingday = new WeekendHolidayAsWorkingday();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.WeekendHolidayAsWorkingdayDetails, model);
        }


        //POST: /WeekendHolidayAsWorkingday/Create
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


        //GET: /WeekendHolidayAsWorkingday/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }


        //POST: /WeekendHolidayAsWorkingday/Edit/5
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

        private void InitializeModel(WeekendHolidayAsWorkingdayModels model)
        {
            model.WeekendHolidayAsWorkingday.strEffectiveDateFrom = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);
            model.WeekendHolidayAsWorkingday.strEffectiveDateTo = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);
            model.WeekendHolidayAsWorkingday.strDeclarationDate = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);
        }

    }
}
