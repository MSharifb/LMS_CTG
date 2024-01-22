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
using System.Collections;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class HolidayWeekDayController : Controller
    {
        //GET: /HolidayWeekDay/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        //GET: /HolidayWeekDay/
        [HttpGet]
        [NoCache]
        public ActionResult HolidayWeekDay(int? page)
        {
            HolidayWeekDayModels model = new HolidayWeekDayModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.GetHolidayWeekDayAll(model.intSearchYearId, model.intSearchMonthId, model.strSearchType);
                model.LstHolidayWeekDayPaging = model.LstHolidayWeekDay.ToPagedList(currentPageIndex, AppConstant.PageSize15);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        //POST: /HolidayWeekDay/HolidayWeekDay
        [HttpPost]
        [NoCache]
        public ActionResult HolidayWeekDay(int? page, HolidayWeekDayModels model)
        {

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetHolidayWeekDayAll(model.intSearchYearId, model.intSearchMonthId, model.strSearchType);
                model.LstHolidayWeekDayPaging = model.LstHolidayWeekDay.ToPagedList(currentPageIndex, AppConstant.PageSize15);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.HolidayWeekDay, model);
        }


        private bool CheckValidation(HolidayWeekDayModels model)
        {
            bool isvalid = true;

            if (model.HolidayWeekDay.dtDateFrom > model.HolidayWeekDay.dtDateTo)
            {
                model.Message = Util.Messages.GetErroMessage("From Date must be smaller than or equal to To Date.");
                return false;
            }

            if (model.HolidayWeekDay.intDuration <= 0)
            {
                model.Message = Util.Messages.GetErroMessage("Days must be greater than zero.");
                return false;
            }

            if (model.HolidayWeekDay.strDateFrom != null && model.HolidayWeekDay.strDateFrom != "" && model.HolidayWeekDay.strDateTo != null && model.HolidayWeekDay.strDateTo != "")
            {
                if (DateTime.Parse(model.StrYearStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.HolidayWeekDay.strDateFrom.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) || DateTime.Parse(model.HolidayWeekDay.strDateFrom.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.StrYearEndDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None))
                {
                    model.Message = Util.Messages.GetErroMessage("From date must be within selected leave year.");
                    return false;
                }

                if (DateTime.Parse(model.StrYearStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.HolidayWeekDay.strDateTo.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) || DateTime.Parse(model.HolidayWeekDay.strDateTo.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None) > DateTime.Parse(model.StrYearEndDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None))
                {
                    model.Message = Util.Messages.GetErroMessage("To date must be within selected leave year.");
                    return false;
                }

            }

            return isvalid;
        }

        //GET: /HolidayWeekDay/HolidayWeekDayAdd
        [HttpGet]
        [NoCache]
        public ActionResult HolidayWeekDayAdd(string id)
        {

            HolidayWeekDayModels model = new HolidayWeekDayModels();
            try
            {
                model.Message = Util.Messages.GetErroMessage("");
                model.HolidayWeekDay.dtDateFrom = DateTime.Today;
                model.HolidayWeekDay.dtDateTo = DateTime.Today;

                model.LstWeekendConfig = new List<WeekendConfigure>();
                foreach (var day in Enum.GetNames(typeof(Common.DayEnum)))
                {
                    var weekConf = new WeekendConfigure { WeekDay = day };

                    model.LstWeekendConfig.Add(weekConf);
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        //POST: /HolidayWeekDay/HolidayWeekDayAdd
        [HttpPost]
        [NoCache]
        public ActionResult HolidayWeekDayAdd(HolidayWeekDayModels model)
        {
            string strmsg = "";
            int id = -1;

            SaveData(model, ref strmsg, ref id);

            return View(model);

        }

        private void SaveData(HolidayWeekDayModels model, ref string strmsg, ref int id)
        {
            try
            {
                if (!model.HolidayWeekDay.isAutomatic)
                {
                    if (CheckValidation(model) == true)
                    {
                        id = model.Save(model, ref strmsg);
                    }
                }
                else
                    id = model.Save(model, ref strmsg);

                //if (model.HolidayWeekDay.isAutomatic == true)
                //{
                //    id = model.Save(model, ref strmsg);
                //    //model.SaveDetailsData(model, id, ref strmsg);
                //    //model.HolidayWeekDay.intHolidayWeekendMasterID = id;
                //    //id = model.SaveAutomaticWeekend(model, ref strmsg);


                //}
                //else
                //{
                //    if (CheckValidation(model) == true)
                //    {
                        
                //        id = model.SaveMasterData(model, ref strmsg);
                //        model.HolidayWeekDay.intHolidayWeekendMasterID = id;
                //        id = model.SaveData(model, ref strmsg);
                //    }
                //}

                if (id < 0)
                {
                    if (id == -547)
                        model.Message = Util.Messages.GetErroMessage("This record already used into Weekend & Holiday Rule.");
                    else
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.HolidayWeekDay = new HolidayWeekDay();
                    ModelState.Clear();
                    model.HolidayWeekDay.dtDateFrom = DateTime.Today;
                    model.HolidayWeekDay.dtDateTo = DateTime.Today;
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
        }

        //GET: /HolidayWeekDay/Details/5
        [HttpGet]
        [NoCache]
        public ActionResult Details(int id)
        {

            HolidayWeekDayModels model = new HolidayWeekDayModels();
            model.Message = Util.Messages.GetErroMessage("");
            try
            {
                //model.HolidayWeekDay = model.GetHolidayWeekDay(id);
                model = model.GetHolidayWeekDay(id);
                model.LstWeekendConfig = new List<WeekendConfigure>();
                foreach (var day in Enum.GetNames(typeof(Common.DayEnum)))
                {
                    var weekConf = new WeekendConfigure { WeekDay = day };

                    model.LstWeekendConfig.Add(weekConf);
                }

                if (model.LstHolidayWeekendDetails != null)
                {
                    foreach (HolidayWeekDayDetails item in model.LstHolidayWeekendDetails)
                    {
                        model.LstWeekendConfig.Where(c => c.WeekDay == item.strDay).ToList()[0].IsWeekend = true;
                        model.LstWeekendConfig.Where(c => c.WeekDay == item.strDay).ToList()[0].IsAlternate = item.isAlternateDay;
                        model.LstWeekendConfig.Where(c => c.WeekDay == item.strDay).ToList()[0].IsWeekend_FirstDayOfYear = item.isFromFirstWeekend;
                    }
                    //for (int i = 0; i < model.LstWeekendConfig.Count; i++)
                    //{
                    //    if (model.LstHolidayWeekendDetails.Exists(c => c.strDay == model.LstWeekendConfig[i].WeekDay))
                    //    {
                    //        model.LstWeekendConfig[i].IsWeekend = true;
                    //    }
                    //}
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /HolidayWeekDay/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(HolidayWeekDayModels model)
        {
            string strmsg = "";
            int id = -1;
            SaveData(model, ref strmsg, ref id);
            //try
            //{

            //    if (CheckValidation(model) == true)
            //    {

            //        int id = model.SaveData(model, ref strmsg);

            //        if (id < 0)
            //        {
            //            model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
            //        }
            //        else
            //        {
            //            model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            //}

            return View(model);

        }


        //POST: /HolidayWeekDay/SaveHolidayWeekDay
        [HttpPost]
        [NoCache]
        public ActionResult SaveHolidayWeekDay(HolidayWeekDayModels model)
        {
            string strmsg = "";
            try
            {

                if (CheckValidation(model) == true)
                {


                    int id = model.SaveData(model, ref strmsg);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.HolidayWeekDay = new HolidayWeekDay();
                        ModelState.Clear();
                        model.HolidayWeekDay.dtDateFrom = DateTime.Today;
                        model.HolidayWeekDay.dtDateTo = DateTime.Today;
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        //POST: /HolidayWeekDay/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(HolidayWeekDayModels model, FormCollection fc)
        {

            try
            {

                int id = model.Delete(model.HolidayWeekDay);

                if (id < 0)
                {
                    if (id == -547)
                        model.Message =  Util.Messages.GetErroMessage("This record already used into Weekend & Holiday Rule.");
                    else
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.HolidayWeekDay = new HolidayWeekDay();
                    ModelState.Clear();
                    model.HolidayWeekDay.dtDateFrom = DateTime.Today;
                    model.HolidayWeekDay.dtDateTo = DateTime.Today;
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.HolidayWeekDayDetails, model);
        }


        //POST: /HolidayWeekDay/Create
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


        //GET: /HolidayWeekDay/Edit
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }


        //POST: /HolidayWeekDay/Edit
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


        //GET: /HolidayWeekDay/CalculateDays        
        [NoCache]
        public JsonResult CalculateDays(FormCollection collection)
        {
            int differenceInDays = 0;
            try
            {
                if (collection.Get("strStartDate").ToString() != "" && collection.Get("strEndDate").ToString() != "")
                {
                    DateTime startdate = DateTime.Parse(collection.Get("strStartDate").ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                    DateTime enddate = DateTime.Parse(collection.Get("strEndDate").ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);

                    // Difference in days, hours, and minutes.
                    TimeSpan ts = enddate - startdate;
                    // Difference in days.
                    differenceInDays = ts.Days;
                    if (differenceInDays >= 0)
                    {
                        differenceInDays = differenceInDays + 1;
                    }
                    else
                    {
                        differenceInDays = 0;
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return Json(differenceInDays);
        }

        //GET: /HolidayWeekDay/GetLeaveYearInfo   
        [HttpPost]
        [NoCache]
        public JsonResult GetLeaveYearInfo(int id)
        {
            ArrayList list = new ArrayList();
            LeaveYear objLvYear = new LeaveYear();
            HolidayWeekDayModels model = new HolidayWeekDayModels();
            try
            {
                

                if (id> 0)
                {
                    objLvYear = model.GetLeaveYearInfo(id);

                }

                list.Add(objLvYear.strStartDate);
                list.Add(objLvYear.strEndDate);

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return Json(list);
        }
    }
}
