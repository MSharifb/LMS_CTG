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
    public class BreakTimeSetupController : Controller
    {
        //GET: /BreakTimeSetup/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /BreakTimeSetup/BreakTimeSetup
        [HttpGet]
        [NoCache]
        public ActionResult BreakTimeSetup(int? page)
        {
            BreakTimeSetupModels model = new  BreakTimeSetupModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetBreakTimeSetupAll();
                InitializeModel(model);
                model.LstSetBreakTimePaged = model.LstBreakTimeSetup.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /BreakTimeSetup/BreakTimeSetup
        [HttpPost]
        [NoCache]
        public ActionResult BreakTimeSetup(int? page, BreakTimeSetupModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetBreakTimeSetupAll();
                model.LstSetBreakTimePaged = model.LstBreakTimeSetup.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.BreakTimeSetup, model);
        }


        //POST: /BreakTimeSetup/OptionWisePageRefresh 
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(BreakTimeSetupModels model)
        {
            try
            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //GET: /BreakTimeSetup/Details/
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            BreakTimeSetupModels model = new  BreakTimeSetupModels();
            model.Message = Util.Messages.GetSuccessMessage("");

            Shift objShift = new Shift();
            ShiftModels objShiftModels = new ShiftModels();

            try
            {
                model.BreakTimeSetup = model.BreakTimeSetupGetByID(Id);
                objShift = objShiftModels.ShiftGetByID(Convert.ToInt16(model.BreakTimeSetup.intShiftID));

                model.BreakTimeSetup.strIntime = objShift.strIntime;
                model.BreakTimeSetup.strOuttime = objShift.strOuttime;

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /BreakTimeSetup/Details 
        [HttpPost]
        [NoCache]
        public ActionResult Details(BreakTimeSetupModels model)
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


        //POST: /BreakTimeSetup/Delete 
        [HttpPost]
        [NoCache]
        public ActionResult Delete(BreakTimeSetupModels model, FormCollection fc)
        {

            try
            {
                int id = model.Delete(model.BreakTimeSetup.intBreakSetID );

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.BreakTimeSetup = new ATT_tblSetBreakTime();
                    InitializeModel(model);
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.BreakTimeSetupDetails, model);
        }


        //GET: /BreakTimeSetup/BreakTimeSetupAdd 
        [HttpGet]
        [NoCache]
        public ActionResult BreakTimeSetupAdd(Int32? id)
        {
            BreakTimeSetupModels model = new  BreakTimeSetupModels();
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


        //POST: /BreakTimeSetup/BreakTimeSetupAdd 
        [HttpPost]
        [NoCache]
        public ActionResult BreakTimeSetupAdd(BreakTimeSetupModels model)
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
                        model.BreakTimeSetup = new  ATT_tblSetBreakTime();
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


        //POST: /BreakTimeSetup/SaveBreakTimeSetup
        [HttpPost]
        [NoCache]
        public ActionResult SaveBreakTimeSetup(BreakTimeSetupModels model)
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
                        model.BreakTimeSetup = new ATT_tblSetBreakTime();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.BreakTimeSetupDetails, model);
        }


        //POST: /BreakTimeSetup/Create
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


        //GET: /BreakTimeSetup/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }

       
        //POST: /BreakTimeSetup/Edit/5
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


        private void InitializeModel(BreakTimeSetupModels model)
        {
           
            //model.BreakTimeSetup.strStartTime = DateTime.Now.ToString();
            //model.BreakTimeSetup.strEndTime = DateTime.Now.ToString();
           
        }



        //GET: /BreakTimeSetup/GetShiftInOutTime
        [NoCache]
        public JsonResult GetShiftInOutTime(BreakTimeSetupModels model)
        {
            ShiftModels objShiftModels = new ShiftModels();
            ArrayList list = new ArrayList();

            try
            {

               Shift objShift =  objShiftModels.ShiftGetByID(Convert.ToInt16(model.BreakTimeSetup.intShiftID));

               list.Add(objShift.strIntime);

               list.Add(objShift.strOuttime);
               
            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }

            return Json(list);
        }
    


    
    }
}
