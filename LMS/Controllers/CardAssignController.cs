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
    public class CardAssignController : Controller
    {
        //GET: /CardAssign/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /CardAssign/CardAssign
        [HttpGet]
        [NoCache]
        public ActionResult CardAssign(int? page)
        {
            CardAssignModels model = new CardAssignModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.Get(-1,"","","","");
                model.LstCardAssignPaging = model.LstCardAssign.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /CardAssign/CardAssign
        [HttpPost]
        [NoCache]
        public ActionResult CardAssign(int? page, CardAssignModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.Get(-1, model.CardAssign.strAssignID == null ? "": model.CardAssign.strAssignID,
                    model.EmployeeName == null ? "" : model.StrEmpID,
                    model.strCardID, "");
                model.LstCardAssignPaging = model.LstCardAssign.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.CardAssign, model);
        }


        //GET: /CardAssign/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            CardAssignModels model = new CardAssignModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.CardAssign = model.Get(Id);                       
                
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /CardAssign/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(CardAssignModels model)
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


        //POST: /CardAssign/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(CardAssignModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.CardAssign.intCardAssignID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.CardAssign = new CardAssign();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.CardAssignDetails, model);
        }


        //GET: /CardAssign/CardAssignAdd
        [HttpGet]
        [NoCache]
        public ActionResult CardAssignAdd(string id)
        {
            CardAssignModels model = new CardAssignModels();
            model.CardAssign.dtEffectiveDate = DateTime.Now.Date;
            model.IsNew = true;
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


        //POST: /CardAssign/CardAssignAdd
        [HttpPost]
        [NoCache]
        public ActionResult CardAssignAdd(CardAssignModels model)
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
                        model.CardAssign = new CardAssign();
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


        //POST: /CardAssign/SaveCardAssign
        [HttpPost]
        [NoCache]
        public ActionResult SaveCardAssign(CardAssignModels model)
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
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        model.CardAssign = new CardAssign();
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
               
        
        [NoCache]
        private bool CheckValidation(CardAssignModels model, bool IsEdit)
        {
            bool isvalid = true;
            CardAssignModels tmpModel = new CardAssignModels();

            tmpModel.Get(-1,model.CardAssign.strAssignID,"","",""); // check uniq assignID

            if (tmpModel.LstCardAssign.Count == 0)
                tmpModel.Get(-1, "", model.CardAssign.strEmpID, "", ""); // check uniq employeeid
           
            if (tmpModel.LstCardAssign.Count > 0)
            {
                if (IsEdit)
                {
                    if ((tmpModel.LstCardAssign.Count == 1) && (tmpModel.LstCardAssign[0].intCardAssignID == model.CardAssign.intCardAssignID))
                        return true;
                }
                model.Message = Util.Messages.GetErroMessage("Record already exists.");
                return false;
            }
            
            return isvalid;
        }

        [HttpGet]
        public JsonResult getEmployeeInformation(CardAssignModels model)
        {
            Employee objEmp = new Employee();
            if (!string.IsNullOrEmpty(model.CardAssign.strEmpID))
            {
                objEmp = model.GetEmployeeInfo(model.CardAssign.strEmpID);

                model.StrDepartment = objEmp.strDepartment;
                model.StrDesignation = objEmp.strDesignation;

            }

            return Json(objEmp, JsonRequestBehavior.AllowGet);
        }
            
    }
}
