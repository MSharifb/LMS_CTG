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
    public class BreakTypeController : Controller
    {
        //GET: /BreakType/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /BreakType/BreakType
        [HttpGet]
        [NoCache]
        public ActionResult BreakType(int? page)
        {
            BreakTypeModels model = new BreakTypeModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.Get(-1,"");
                model.LstBreakTypePaging = model.LstBreakType.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /BreakType/BreakType
        [HttpPost]
        [NoCache]
        public ActionResult BreakType(int? page, BreakTypeModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.Get(-1,"");
                model.LstBreakTypePaging = model.LstBreakType.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.BreakType, model);
        }


        //GET: /BreakType/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            BreakTypeModels model = new BreakTypeModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.BreakType = model.Get(Id);
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /BreakType/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(BreakTypeModels model)
        {
            try
            {
                if (CheckValidation(model, true) == true)
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


        //POST: /BreakType/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(BreakTypeModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.BreakType.intBreakID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.BreakType = new BreakType();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.BreakTypeDetails, model);
        }


        //GET: /BreakType/BreakTypeAdd
        [HttpGet]
        [NoCache]
        public ActionResult BreakTypeAdd(string id)
        {
            BreakTypeModels model = new BreakTypeModels();
            
            try
            {
                model.Message = Util.Messages.GetErroMessage("");
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /BreakType/BreakTypeAdd
        [HttpPost]
        [NoCache]
        public ActionResult BreakTypeAdd(BreakTypeModels model)
        {
            try
            {
                if (CheckValidation(model, false) == true)
                {
                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.BreakType = new BreakType();
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


        //POST: /BreakType/SaveBreakType
        [HttpPost]
        [NoCache]
        public ActionResult SaveBreakType(BreakTypeModels model)
        {
            try
            {
                if (CheckValidation(model, false) == true)
                {
                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.BreakType = new BreakType();
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


        //POST: /BreakType/OptionWisePageRefresh
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(BreakTypeModels model)
        {
            try

            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        [NoCache]
        private bool CheckValidation(BreakTypeModels model, bool IsEdit)
        {
            bool isvalid = true;
            BreakTypeModels tmpModel = new BreakTypeModels();
            tmpModel.Get(-1, model.BreakType.strBreakName.Trim());
            
            if (tmpModel.LstBreakType.Count > 0)
            {
                if (IsEdit)
                {
                    if ((tmpModel.LstBreakType.Count == 1) && (tmpModel.LstBreakType[0].intBreakID == model.BreakType.intBreakID))
                        return true;                  
                }
                model.Message = Util.Messages.GetErroMessage("Record already exists for this Break Type.");
                return false;
            }
            
            return isvalid;
        }

    
    
    }
}
