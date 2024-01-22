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
    public class CardInfoController : Controller
    {
        //GET: /CardInfo/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /CardInfo/CardInfo
        [HttpGet]
        [NoCache]
        public ActionResult CardInfo(int? page)
        {
            CardInfoModels model = new CardInfoModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.Get(-1,"",-1);
                model.LstCardInfoPaging = model.LstCardInfo.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /CardInfo/CardInfo
        [HttpPost]
        [NoCache]
        public ActionResult CardInfo(int? page, CardInfoModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.Get(-1,"", model.intSearchStatus);
                model.LstCardInfoPaging = model.LstCardInfo.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.CardInfo, model);
        }


        //GET: /CardInfo/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            CardInfoModels model = new CardInfoModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.CardInfo = model.Get(Id);
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /CardInfo/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(CardInfoModels model)
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


        //POST: /CardInfo/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(CardInfoModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.CardInfo.intCardID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.CardInfo = new CardInfo();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.CardInfoDetails, model);
        }


        //GET: /CardInfo/CardInfoAdd
        [HttpGet]
        [NoCache]
        public ActionResult CardInfoAdd(string id)
        {
            CardInfoModels model = new CardInfoModels();
            
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


        //POST: /CardInfo/CardInfoAdd
        [HttpPost]
        [NoCache]
        public ActionResult CardInfoAdd(CardInfoModels model)
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
                        model.CardInfo = new CardInfo();
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


        //POST: /CardInfo/SaveCardInfo
        [HttpPost]
        [NoCache]
        public ActionResult SaveCardInfo(CardInfoModels model)
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
                        model.CardInfo = new CardInfo();
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


        //POST: /CardInfo/OptionWisePageRefresh
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(CardInfoModels model)
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
        private bool CheckValidation(CardInfoModels model, bool IsEdit)
        {
            bool isvalid = true;
            CardInfoModels tmpModel = new CardInfoModels();
            tmpModel.Get(-1, model.CardInfo.strCardID.Trim(), model.CardInfo.intStatus);
            
            if (tmpModel.LstCardInfo.Count > 0)
            {
                if (IsEdit)
                {
                    if ((tmpModel.LstCardInfo.Count == 1) && (tmpModel.LstCardInfo[0].intCardID == model.CardInfo.intCardID))
                        return true;                  
                }
                model.Message = Util.Messages.GetErroMessage("Record already exists for this Card ID.");
                return false;
            }
            
            return isvalid;
        }

        [HttpGet]
        [NoCache]
        public ActionResult CardInfoSearch(int? page)
        {

            CardInfoModels model = new CardInfoModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
           
            model.startRowIndex = (currentPageIndex) * 10 + 1;
            model.maximumRows = 10;
            model.CardInfo = new CardInfo();

            CardInfo objSearch = model.CardInfo;
            
             objSearch.intStatus = 0;           


            model.LstCardInfo = model.GetCardInfoPaging(objSearch);
            model.LstCardInfoPaging = model.LstCardInfo.ToPagedList(currentPageIndex, AppConstant.PageSize);

            return PartialView(LMS.Util.PartialViewName.CardInfoSearch, model);
        }
    
    }
}
