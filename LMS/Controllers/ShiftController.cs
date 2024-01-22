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
    public class ShiftController : Controller
    {
        //GET: /Shift/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /Shift/Shift
        [HttpGet]
        [NoCache]
        public ActionResult Shift(int? page)
        {
            ShiftModels model = new ShiftModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetShiftAll();
               // InitializeModel(model);
                model.LstShiftPaged = model.LstShift.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /Shift/Shift
        [HttpPost]
        [NoCache]
        public ActionResult Shift(int? page, ShiftModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.LstShift = model.GetShiftPaging(model);
                model.LstShiftPaged = model.LstShift.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.Shift, model);
        }


        //POST: /Shift/OptionWisePageRefresh 
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(ShiftModels model)
        {
            try
            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //GET: /Shift/Details/
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            ShiftModels model = new ShiftModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            try
            {
                model.Shift = model.ShiftGetByID(Id);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /Shift/Details 
        [HttpPost]
        [NoCache]
        public ActionResult Details(ShiftModels model)
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


        //POST: /Shift/Delete 
        [HttpPost]
        [NoCache]
        public ActionResult Delete(ShiftModels model, FormCollection fc)
        {

            try
            {
                int id = model.Delete(model.Shift.intShiftID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.Shift = new Shift();
                    InitializeModel(model);
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ShiftDetails, model);
        }


        //GET: /Shift/ShiftAdd 
        [HttpGet]
        [NoCache]
        public ActionResult ShiftAdd(Int32? id)
        {
            ShiftModels model = new ShiftModels();
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


        //POST: /Shift/ShiftAdd 
        [HttpPost]
        [NoCache]
        public ActionResult ShiftAdd(ShiftModels model)
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
                        model.Shift = new Shift();
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


        //POST: /Shift/SaveShift
        [HttpPost]
        [NoCache]
        public ActionResult SaveShift(ShiftModels model)
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
                        model.Shift = new Shift();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ShiftDetails, model);
        }


        //POST: /Shift/Create
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


        //GET: /Shift/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }


        //POST: /Shift/Edit/5
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

        private void InitializeModel(ShiftModels model)
        {
            model.Shift.strPeriodFrom = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);
            model.Shift.strPeriodTo = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);
            model.Shift.strEffectiveDate = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);

            model.Shift.strIntime = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Time);
            model.Shift.strOuttime = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Time);
            model.Shift.strHalfTime = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Time);
        }

    }
}
