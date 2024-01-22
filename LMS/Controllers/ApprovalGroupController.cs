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
    public class ApprovalGroupController : Controller
    {
        //GET: / LeaveYearType/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: / ApprovalGroup/ApprovalGroup
        [HttpGet]
        [NoCache]
        public ActionResult ApprovalGroup(int? page)
        {
            ApprovalGroupModels model = new ApprovalGroupModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetApprovalGroupAll();
                model.LstApprovalGroupPaging = model.LstApprovalGroup.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: / ApprovalGroup/ApprovalGroup
        [HttpPost]
        [NoCache]
        public ActionResult ApprovalGroup(int? page, ApprovalGroupModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetApprovalGroupAll();
                model.LstApprovalGroupPaging = model.LstApprovalGroup.ToPagedList(currentPageIndex, AppConstant.PageSize10);
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


        //GET: / ApprovalGroup/Details/
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            ApprovalGroupModels model = new ApprovalGroupModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            try
            {
                model.ApprovalGroup = model.GetApprovalGroup(Id);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: / ApprovalGroup/Details 
        [HttpPost]
        [NoCache]
        public ActionResult Details(ApprovalGroupModels model)
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
        public ActionResult Delete(ApprovalGroupModels model, FormCollection fc)
        {

            try
            {
                int id = model.Delete(model.ApprovalGroup.intApprovalGroupId);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.ApprovalGroup = new ApprovalGroup();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveYearTypeDetails, model);
        }


        //GET: / ApprovalGroup/ApprovalGroupAdd 
        [HttpGet]
        [NoCache]
        public ActionResult ApprovalGroupAdd(string id)
        {
            ApprovalGroupModels model = new ApprovalGroupModels();
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


        //POST: / ApprovalGroup/ApprovalGroupAdd 
        [HttpPost]
        [NoCache]
        public ActionResult ApprovalGroupAdd(ApprovalGroupModels model)
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
                        model.ApprovalGroup = new ApprovalGroup();
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


        //POST: / ApprovalGroup/SaveApprovalGroup
        [HttpPost]
        [NoCache]
        public ActionResult SaveApprovalGroup(ApprovalGroupModels model)
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
                        model.ApprovalGroup = new ApprovalGroup();
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


        //POST: / ApprovalGroup/Create
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


        //GET: / ApprovalGroup/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }


        //POST: / ApprovalGroup/Edit/5
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

