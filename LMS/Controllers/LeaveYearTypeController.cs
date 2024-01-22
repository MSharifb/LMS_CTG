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
    public class LeaveYearTypeController : Controller
    {
        //GET: / LeaveYearType/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: / LeaveYearType/LeaveYearType
        [HttpGet]
        [NoCache]
        public ActionResult LeaveYearType(int? page)
        {
            LeaveYearTypeModels model = new LeaveYearTypeModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetLeaveYearTypeAll();
                model.LstLeaveYearTypePaging = model.LstLeaveYearType.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: / LeaveYearType/LeaveYearType
        [HttpPost]
        [NoCache]
        public ActionResult LeaveYearType(int? page, LeaveYearTypeModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetLeaveYearTypeAll();
                model.LstLeaveYearTypePaging = model.LstLeaveYearType.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveYearType, model);
        }


        ////POST: / LeaveYearType/OptionWisePageRefresh 
        //[HttpPost]
        //[NoCache]
        //public ActionResult OptionWisePageRefresh(LeaveTypeModels model)
        //{
        //    try
        //    { }

        //    catch (Exception ex)
        //    {
        //        model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
        //    }

        //    return View(model);
        //}


        //GET: / LeaveYearType/Details/
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveYearTypeModels model = new LeaveYearTypeModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            try
            {
                model.LeaveYearType = model.GetLeaveYearType(Id);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: / LeaveYearType/Details 
        [HttpPost]
        [NoCache]
        public ActionResult Details(LeaveYearTypeModels model)
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


        //POST: / LeaveYearType/Delete 
        [HttpPost]
        [NoCache]
        public ActionResult Delete(LeaveYearTypeModels model, FormCollection fc)
        {

            try
            {
                int id = model.Delete(model.LeaveYearType.intLeaveYearTypeId);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.LeaveYearType = new LeaveYearType();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveYearTypeDetails, model);
        }


        //GET: / LeaveYearType/LeaveYearTypeAdd 
        [HttpGet]
        [NoCache]
        public ActionResult LeaveYearTypeAdd(string id)
        {
            LeaveYearTypeModels model = new LeaveYearTypeModels();
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


        //POST: / LeaveYearType/LeaveYearTypeAdd 
        [HttpPost]
        [NoCache]
        public ActionResult LeaveYearTypeAdd(LeaveYearTypeModels model)
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
                        model.LeaveYearType = new LeaveYearType();
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


        //POST: / LeaveYearType/SaveLeaveYearType
        [HttpPost]
        [NoCache]
        public ActionResult SaveLeaveYearType(LeaveYearTypeModels model)
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
                        model.LeaveYearType = new LeaveYearType();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveYearTypeDetails, model);
        }


        //POST: / LeaveYearType/Create
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


        //GET: / LeaveYearType/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }


        //POST: / LeaveYearType/Edit/5
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


        [NoCache]
        public JsonResult GetMonthAndName(string startMonth)
        {
            //double dblEntitlement = 0;
            var endMonth=string.Empty;
            var sMonth = string.Empty;
            var yearType = string.Empty;

            try
            {
                if (startMonth!=string.Empty)
                {
                    DateTime dblEntitlement = Convert.ToDateTime(DateTime.Now.Year+"-" + startMonth + "-01");
                   var  dblEntitlement1 = dblEntitlement.AddYears(1).AddDays(-1);
                    endMonth = dblEntitlement1.ToString("MMMM");
                    sMonth = dblEntitlement.ToString("MMMM");
                    yearType = sMonth + "-" + endMonth;
                }
            }
            catch (Exception ex)
            {
                //model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return Json(new { startMonth = endMonth, yearType = yearType }, JsonRequestBehavior.AllowGet);
        }
    
    
    }
}
