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
    public class ShiftAssignmentController : Controller
    {
        //GET: /ShiftAssignment/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /ShiftAssignment/ShiftAssignment
        [HttpGet]
        [NoCache]
        public ActionResult ShiftAssignment(int? page)
        {
            ShiftAssignmentModels model = new ShiftAssignmentModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {

                model.Get(-1,"",0,"");
                model.LstShiftAssignmentPaging = model.LstShiftAssignment.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /ShiftAssignment/ShiftAssignment
        [HttpPost]
        [NoCache]
        public ActionResult ShiftAssignment(int? page, ShiftAssignmentModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.Get(-1, model.ShiftAssignment.strEmpName == null ? "" : model.ShiftAssignment.strEmpID, model.ShiftAssignment.intShiftID, "");
                model.LstShiftAssignmentPaging = model.LstShiftAssignment.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ShiftAssignment, model);
        }


        //GET: /ShiftAssignment/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            ShiftAssignmentModels model = new ShiftAssignmentModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.ShiftAssignment = model.Get(Id);

                if (model.ShiftAssignment.strEmpID == "")
                    model.IsIndividual = false;
                else model.IsIndividual = true;
                getEmployeeInformation(model);  
                
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            return View(model);
        }


        //POST: /ShiftAssignment/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(ShiftAssignmentModels model)
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


        //POST: /ShiftAssignment/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(ShiftAssignmentModels model)
        {
            try
            {
                int id = model.Delete(model.ShiftAssignment.intShiftAssignmentID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.ShiftAssignment = new ShiftAssignment();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.ShiftAssignmentDetails, model);
        }


        //GET: /ShiftAssignment/ShiftAssignmentAdd
        [HttpGet]
        [NoCache]
        public ActionResult ShiftAssignmentAdd(string id)
        {
            ShiftAssignmentModels model = new ShiftAssignmentModels();
            model.ShiftAssignment.dtEffectiveDate = DateTime.Now.Date;
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


        //POST: /ShiftAssignment/ShiftAssignmentAdd
        [HttpPost]
        [NoCache]
        public ActionResult ShiftAssignmentAdd(ShiftAssignmentModels model)
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
                        model.ShiftAssignment = new ShiftAssignment();
                        model.ShiftAssignment.dtEffectiveDate = DateTime.Now.Date;
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


        //POST: /ShiftAssignment/SaveShiftAssignment
        [HttpPost]
        [NoCache]
        public ActionResult SaveShiftAssignment(ShiftAssignmentModels model)
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
                        model.ShiftAssignment = new ShiftAssignment();
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
        private bool CheckValidation(ShiftAssignmentModels model, bool IsEdit)
        {
            bool isvalid = true;
            ShiftAssignmentModels tmpModel = new ShiftAssignmentModels();

            tmpModel.Get(-1,"",-1, model.ShiftAssignment.dtEffectiveDate.ToString("yyyy-MM-dd")); // check uniq assignID

            if (tmpModel.LstShiftAssignment.Count > 0)
            {
                if (!string.IsNullOrEmpty(model.ShiftAssignment.strEmpID)) // check uniq employee ID
                {
                    tmpModel.LstShiftAssignment = tmpModel.LstShiftAssignment.Where(q => q.strEmpID == model.ShiftAssignment.strEmpID).ToList();
                    if (tmpModel.LstShiftAssignment.Count > 0)
                    {
                        if (IsEdit && tmpModel.LstShiftAssignment[0].intShiftAssignmentID == model.ShiftAssignment.intShiftAssignmentID)
                            return true;

                        model.Message = Util.Messages.GetErroMessage("This Employee already exists");
                        return false;
                    }
                }
                else // check duplicate designation
                {

                    string _strDesignationID = model.ShiftAssignment.strDesignationID == null ? "" : model.ShiftAssignment.strDesignationID;
                    string _strDepartmentID = model.ShiftAssignment.strDepartmentID == null ? "" : model.ShiftAssignment.strDepartmentID;
                    string _strCompanyID = model.ShiftAssignment.strCompanyID == null ? "" : model.ShiftAssignment.strCompanyID;
                    string _strLocationID = model.ShiftAssignment.strLocationID == null ? "" : model.ShiftAssignment.strLocationID;

                    tmpModel.LstShiftAssignment = tmpModel.LstShiftAssignment.Where(
                         q => q.strDesignationID == _strDesignationID
                         && q.strDepartmentID == _strDepartmentID
                         && q.strCompanyID == _strCompanyID
                         && q.strLocationID == _strLocationID
                         && q.intReligionID == model.ShiftAssignment.intReligionID
                         && q.intCategoryCode == model.ShiftAssignment.intCategoryCode).ToList();
                    if (tmpModel.LstShiftAssignment.Count > 0)
                    {
                        if (IsEdit && tmpModel.LstShiftAssignment[0].intShiftAssignmentID == model.ShiftAssignment.intShiftAssignmentID)
                            return true;

                        model.Message = Util.Messages.GetErroMessage("This data already exists");
                        return false;
                    }
                }
            }

            return isvalid;
        }

        [HttpGet]
        public JsonResult getEmployeeInformation(ShiftAssignmentModels model)
        {
            Employee objEmp = new Employee();
            if (!string.IsNullOrEmpty(model.ShiftAssignment.strEmpID))
            {
                objEmp = model.GetEmployeeInfo(model.ShiftAssignment.strEmpID);

                model.ShiftAssignment.strEmpName = objEmp.strEmpName;
                model.ShiftAssignment.strDepartment = objEmp.strDepartment;
                model.ShiftAssignment.strDesignation = objEmp.strDesignation;
                model.ShiftAssignment.strLocation = model.Location.Where(q => q.Value == objEmp.strLocationID).First().Text;
                model.ShiftAssignment.strCompany = model.Company.Where(q => q.Value == objEmp.strCompanyID).First().Text;

            }

            return Json(model.ShiftAssignment, JsonRequestBehavior.AllowGet);
        }
            
    }
}
