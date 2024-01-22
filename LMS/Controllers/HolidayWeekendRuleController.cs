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
    public class HolidayWeekendRuleController : Controller
    {
        //GET: /HolidayWeekendRule/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /HolidayWeekendRule/HolidayWeekendRule
        [HttpGet]
        [NoCache]
        public ActionResult HolidayWeekendRule(int? page)
        {
            HolidayWeekendRuleModels model = new HolidayWeekendRuleModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetHolidayWeekDayRuleAll();               
                model.LstHolidayWeekDayRulePaging = model.LstHolidayWeekDayRule.ToPagedList(currentPageIndex, AppConstant.PageSize10);
               
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /HolidayWeekendRule/HolidayWeekendRule
        [HttpPost]
        [NoCache]
        public ActionResult HolidayWeekendRule(int? page, HolidayWeekendRuleModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetHolidayWeekDayRuleAll();
                model.LstHolidayWeekDayRulePaging = model.LstHolidayWeekDayRule.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.HolidayWeekendRule, model);
        }

        
        //GET: /HolidayWeekendRule/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            HolidayWeekendRuleModels model = new HolidayWeekendRuleModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.HolidayWeekDayRule = model.GetHolidayWeekDayRule(Id);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /HolidayWeekendRule/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(HolidayWeekendRuleModels model)
        {
            try
            {
                if (CheckValidation(model) == true)
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


        //POST: /HolidayWeekendRule/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(HolidayWeekendRuleModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.HolidayWeekDayRule.intHolidayRuleID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.HolidayWeekDayRule = new HolidayWeekDayRule();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.HolidayWeekendRuleDetails, model);
        }


        //GET: /HolidayWeekendRule/HolidayWeekendRuleAdd
        [HttpGet]
        [NoCache]
        public ActionResult HolidayWeekendRuleAdd(string id)
        {
            HolidayWeekendRuleModels model = new HolidayWeekendRuleModels();
            try
            {
                model.Message = Util.Messages.GetErroMessage("");
                model = GetWeekendConfigure(model);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /HolidayWeekendRule/HolidayWeekendRuleAdd
        [HttpPost]
        [NoCache]
        public ActionResult HolidayWeekendRuleAdd(HolidayWeekendRuleModels model)
        {
            try
            {
                if (CheckValidation(model) == true)
                {
                    int id = model.SaveData(model);
                    

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.HolidayWeekDayRule = new HolidayWeekDayRule();
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


        // POST: /HolidayWeekendRule/Index
        [HttpPost]
        [NoCache]
        public ActionResult Index(HolidayWeekendRuleModels model)
        {
            try
            {
                if (CheckValidation(model) == true)
                {
                    int id = model.SaveData(model);
                    

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.HolidayWeekDayRule = new HolidayWeekDayRule();
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

        
        // POST: /HolidayWeekendRule/GetHolidayWeekDayDetails        
        [HttpPost]
        [NoCache]
        public ActionResult GetHolidayWeekDayRuleDetails(HolidayWeekendRuleModels model)
        {
            try
            {
                model.HolidayWeekDayRule.HolidayWeekDayRuleChild = model.GetHolidayWeekDayRuleChild(model.HolidayWeekDayRule.intHolidayRuleID, model.HolidayWeekDayRule.intLeaveYearID);
                model = GetWeekendConfigure(model);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(LMS.Util.PartialViewName.HolidayWeekendRuleDetails, model);
        }



        [NoCache]
        private bool CheckValidation(HolidayWeekendRuleModels model)
        {
            bool isvalid = true;

            List<HolidayWeekDayRuleChild> objLst = new List<HolidayWeekDayRuleChild>();
            List<HolidayWeekDayRuleChild> objFindLst = new List<HolidayWeekDayRuleChild>();
            model = GetWeekendConfigure(model);
            objLst = model.HolidayWeekDayRule.HolidayWeekDayRuleChild.Where(c => c.IsChecked == true).OrderBy(c => c.dtDateFrom).ToList();
            if (objLst.Count == 0)
            {
                model.Message = Util.Messages.GetErroMessage("Please select the required checkbox for Weekend or Holiday.");
                return false;
            }
            else
            {
                objFindLst = objLst.ToList();

                foreach (HolidayWeekDayRuleChild obj in objLst)
                {
                    List<HolidayWeekDayRuleChild> objList = new List<HolidayWeekDayRuleChild>();
                }
            }

            return isvalid;
        }

        private HolidayWeekendRuleModels GetWeekendConfigure(HolidayWeekendRuleModels model)
        {

             
            try
            {
                model.Message = Util.Messages.GetErroMessage("");

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

            return model;
        }
    }
}
