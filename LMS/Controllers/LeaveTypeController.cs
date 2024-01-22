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
    public class LeaveTypeController : Controller
    {
        //GET: /LeaveType/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /LeaveType/LeaveType
        [HttpGet]
        [NoCache]
        public ActionResult LeaveType(int? page)
        {
            LeaveTypeModels model = new LeaveTypeModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetLeaveTypeAll();
                model.LstLeaveType1 = model.LstLeaveType.ToPagedList(currentPageIndex, AppConstant.PageSize10);
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
        public ActionResult LeaveType(int? page, LeaveTypeModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetLeaveTypeAll();
                model.LstLeaveType1 = model.LstLeaveType.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveType, model);
        }

        
        //POST: /LeaveType/OptionWisePageRefresh 
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(LeaveTypeModels model)
        {
            try
            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        
        //GET: /LeaveType/Details/
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveTypeModels model = new LeaveTypeModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            try
            {
                model.LeaveType = model.GetLeaveType(Id);
                model.LeaveTypeDeduct = model.GetDeductedLeaveType(Id);

                if (model.LeaveType.isServiceLifeType == true)
                {
                    model.LeaveType.strIsServiceLifeType = "ServicePeriod";
                }
                else
                {
                    model.LeaveType.strIsServiceLifeType = "Yearly";
                }
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
        public ActionResult Details(LeaveTypeModels model)
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
                        model.LeaveType.intLeaveTypeAddID = 0; // Added For BEPZA
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


        //POST: /LeaveType/Delete 
        [HttpPost]
        [NoCache]
        public ActionResult Delete(LeaveTypeModels model, FormCollection fc)
        {

            try
            {
                int id = model.Delete(model.LeaveType.intLeaveTypeID);
                var list = model.GetDeductedLeaveType(model.LeaveType.intLeaveTypeID);
                foreach (var item in list)
                    model.DeleteDedectedLeaveType(item.intLeaveTypeDeductID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {

                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.LeaveType = new LeaveType();
                    model.LeaveTypeDeduct = new List<LeaveTypeDeduct>();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveTypeDetails, model);
        }
        
        [HttpPost]
        [NoCache]
        public ActionResult DeleteDeductedLeave(LeaveTypeModels model, Int32 Id)
        {
            try
            {
                int id = model.DeleteDedectedLeaveType(Id);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.LeaveTypeDeduct = model.GetDeductedLeaveType(model.LeaveType.intLeaveTypeID);
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveTypeDetails, model);
        }

        //GET: /LeaveType/LeaveTypeAdd 
        [HttpGet]
        [NoCache]
        public ActionResult LeaveTypeAdd(string id)
        {
            LeaveTypeModels model = new LeaveTypeModels();
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
        public ActionResult LeaveTypeAdd(LeaveTypeModels model)
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

                        model.LeaveType.intLeaveTypeAddID = 0; // Added For BEPZA
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                       // model.LeaveType = new LeaveType(); // Updated For BEPZA
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


        //POST: /LeaveType/SaveLeaveType
        [HttpPost]
        [NoCache]
        public ActionResult SaveLeaveType(LeaveTypeModels model)
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
                        model.LeaveType = new LeaveType();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveTypeDetails, model);
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
    
    
    
    }
}
