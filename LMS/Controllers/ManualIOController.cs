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
    public class ManualIOController : Controller
    {
        //GET: /ManualIO/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /ManualIO/ManualIO
        [HttpGet]
        [NoCache]
        public ActionResult ManualIO(int? page)
        {
            ManualIOModels model = new ManualIOModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetManualIOAll();
                InitializeModel(model);
                model.LstManualIOPaged = model.LstManualIO.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /ManualIO/ManualIO
        [HttpPost]
        [NoCache]
        public ActionResult ManualIO(int? page, ManualIOModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.LstManualIO = model.GetManualIOPaging(model);
                model.LstManualIOPaged = model.LstManualIO.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ManualIO, model);
        }


        //POST: /ManualIO/OptionWisePageRefresh 
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(ManualIOModels model)
        {
            try
            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //Edit page get
        //GET: /ManualIO/Details/
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            ManualIOModels model = new ManualIOModels();
            model.Message = Util.Messages.GetSuccessMessage("");

            try
            {
                model.ManualIO = model.ManualIOGetByID(Id);

                model.ManualIO.strAttendDate = model.ManualIO.dtAttendDateTime.ToString("dd-MM-yyyy");

                model.ManualIO.strAttenTime = model.ManualIO.dtAttendDateTime.ToString("HH:mm tt");

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        //Add and Edit page Save
        //POST: /ManualIO/Details 
        [HttpPost]
        [NoCache]
        public ActionResult Details(ManualIOModels model)
        {
            string strmsg = "";
            try
            {
                string msg = Util.Messages.SavedSuccessfully.ToString();
                if (model.ManualIO.intRowID > 0)
                    msg = Util.Messages.UpdateSuccessfully.ToString();

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
                        if (model.ManualIO.intRowID < 1)
                        {
                            model = new ManualIOModels();
                            InitializeModel(model);
                        }
                        model.Message = Util.Messages.GetSuccessMessage(msg);
                        model.ManualIO.IsSingleEmp = true;
                        ModelState.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ManualIODetails, model);
            //return View(model);
        }


        //POST: /ManualIO/Delete 
        [HttpPost]
        [NoCache]
        public ActionResult Delete(ManualIOModels model, FormCollection fc)
        {

            try
            {
                int id = model.Delete(model.ManualIO.intRowID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.ManualIO = new ManualIO();
                    InitializeModel(model);
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ManualIODetails, model);
        }


        //Add page Get
        //GET: /ManualIO/ManualIOAdd 
        [HttpGet]
        [NoCache]
        public ActionResult ManualIOAdd(Int32? id)
        {
            ManualIOModels model = new ManualIOModels();
            try
            {
                InitializeModel(model);
                model.ManualIO.IsSingleEmp = true;
                model.Message = Util.Messages.GetSuccessMessage("");
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /ManualIO/ManualIOAdd 
        /* [HttpPost]
         [NoCache]
         public ActionResult ManualIOAdd(ManualIOModels model)
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
                         model.ManualIO = new  ManualIO();
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
         */


        /*
        //POST: /ManualIO/SaveManualIO
        [HttpPost]
        [NoCache]
        public ActionResult SaveManualIO(ManualIOModels model)
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
                        model.ManualIO = new ManualIO();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ManualIODetails, model);
        }
        */

        /*
        //POST: /ManualIO/Create
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


        //GET: /ManualIO/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }

       
        //POST: /ManualIO/Edit/5
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
        */

        private void InitializeModel(ManualIOModels model)
        {

            model.ManualIO.strAttendDate = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Date);

            model.ManualIO.strAttenTime = DateTime.Now.ToString(LMS.Util.DateTimeFormat.Time);

        }



        //GET: /ManualIO/GetShiftInOutTime
        [NoCache]
        public JsonResult GetShiftInOutTime(ManualIOModels model)
        {
            ShiftModels objShiftModels = new ShiftModels();
            ArrayList list = new ArrayList();

            try
            {

                Shift objShift = objShiftModels.ShiftGetByID(Convert.ToInt16(model.ManualIO.intShiftID));

                list.Add(objShift.strIntime);

                list.Add(objShift.strOuttime);
                list.Add(objShift.strHalfTime);

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }

            return Json(list);
        }


        [NoCache]
        public JsonResult getEmployeeInformation(ManualIOModels model)
        {
            ArrayList list = new ArrayList();
            Employee objEmp = new Employee();
            try
            {
                if (!string.IsNullOrEmpty(model.ManualIO.strEmpID))
                {
                    objEmp = model.GetEmployeeInfo(model.ManualIO.strEmpID);

                    //model.ManualIO.strCardID = objEmp.ca;
                    //model.ManualIO.strDesignation = objEmp.strDesignation;

                    list.Add(objEmp.strDesignation);
                    list.Add(objEmp.strAssignID);


                }
            }

            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }

            return Json(list);


        }



    }
}
