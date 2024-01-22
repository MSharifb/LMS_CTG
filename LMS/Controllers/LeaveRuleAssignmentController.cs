using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Web.Models;
using LMSEntity;
using LMS.Web;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Util;
using MvcPaging;
using System.Collections;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class LeaveRuleAssignmentController : Controller
    {
        //GET: /LeaveRuleAssignment/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /LeaveRuleAssignment/LeaveRuleAssignment
        [HttpGet]
        [NoCache]
        public ActionResult LeaveRuleAssignment(int? page)
        {
            LeaveRuleAssignmentModels model = new LeaveRuleAssignmentModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
               // model.strSortBy = "intRuleAssignID";
                model.strSortBy = "strLeaveType,strRuleName";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
                model.maximumRows = AppConstant.PageSize10;
                model.PageNumber = currentPageIndex + 1;


                model.LeaveRuleAssignment = new LeaveRuleAssignment();
                LeaveRuleAssignment objSearch = model.LeaveRuleAssignment;

                if (!string.IsNullOrEmpty(model.strSearchInitial))
                {
                    objSearch.strEmpInitial = model.strSearchInitial.Trim();
                }
                else
                {
                    objSearch.strEmpInitial = model.strSearchInitial;
                }

                if (!string.IsNullOrEmpty(model.strSearchName))
                {
                    objSearch.strEmpName = model.strSearchName.Trim();
                }
                else
                {
                    objSearch.strEmpName = model.strSearchName;
                }


                objSearch.intLeaveTypeID = model.intSearchLeaveTypeID;
                objSearch.intRuleID = model.intSearchRuleID;
                objSearch.strDepartmentID = model.strSearchDepartmentId;
                objSearch.strDesignationID = model.strSearchDesignationId;
                objSearch.strLocationID = model.strSearchLocationId;
                objSearch.strGender = model.strSearchGender;
                objSearch.intCategoryCode = model.intSearchCategoryId;


                model.GetLeaveRuleAssignmentAll(objSearch);
                model.LstLeaveRuleAssignmentPaging = model.LstLeaveRuleAssignment.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /LeaveRuleAssignment/LeaveRuleAssignment
        [HttpPost]
        [NoCache]
        public ActionResult LeaveRuleAssignment(int? page, LeaveRuleAssignmentModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                //model.strSortBy = "intRuleAssignID";
                model.strSortBy = "strLeaveType,strRuleName";
                model.strSortType = LMS.Util.DataShortBy.ASC;
                model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
                model.maximumRows = AppConstant.PageSize;
                model.PageNumber = currentPageIndex + 1;

                model.LeaveRuleAssignment = new LeaveRuleAssignment();
                LeaveRuleAssignment objSearch = model.LeaveRuleAssignment;

                if (!string.IsNullOrEmpty(model.strSearchInitial))
                {
                    objSearch.strEmpInitial = model.strSearchInitial.Trim();
                }
                else
                {
                    objSearch.strEmpInitial = model.strSearchInitial;
                }

                if (!string.IsNullOrEmpty(model.strSearchName))
                {
                    objSearch.strEmpName = model.strSearchName.Trim();
                }
                else
                {
                    objSearch.strEmpName = model.strSearchName;
                }

                objSearch.intLeaveTypeID = model.intSearchLeaveTypeID;
                objSearch.intRuleID = model.intSearchRuleID;
                objSearch.strDepartmentID = model.strSearchDepartmentId;
                objSearch.strDesignationID = model.strSearchDesignationId;
                objSearch.strLocationID = model.strSearchLocationId;
                objSearch.strGender = model.strSearchGender;
                objSearch.intCategoryCode = model.intSearchCategoryId;

                model.GetLeaveRuleAssignmentAll(objSearch);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.LeaveRuleAssignment, model);
        }


        //POST: /LeaveRuleAssignment/GetDropDown
        [HttpPost]
        [NoCache]
        public ActionResult GetDropDown(LeaveRuleAssignmentModels model)
        {
            try
            {
                model.intLeaveTypeID = model.LeaveRuleAssignment.intLeaveTypeID;
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(LMS.Util.PartialViewName.LeaveRuleAssignmentDetails, model);

        }


        //GET: /LeaveRuleAssignment/Details      
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveRuleAssignmentModels model = new LeaveRuleAssignmentModels();
            model.Message = Util.Messages.GetErroMessage("");
            try
            {
                model.LeaveRuleAssignment = model.GetLeaveRuleAssignment(Id);
                if (model.LeaveRuleAssignment != null)
                {
                    model.intLeaveTypeID = model.LeaveRuleAssignment.intLeaveTypeID; // this is done for filter the leave rule according to the leave type
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /LeaveRuleAssignment/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(LeaveRuleAssignmentModels model)
        {
            string strmessage = "";
            try
            {
                model.SaveData(model, out strmessage);

                if (strmessage.ToString() != "Successful")
                {
                    model.Message = Util.Messages.GetErroMessage(strmessage);
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());                   
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /LeaveRuleAssignment/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(FormCollection fc)
        {
            string strmessage = "";
            LeaveRuleAssignmentModels model = new LeaveRuleAssignmentModels();

            int Id = int.Parse(fc.Get("intRuleAssignID").ToString());

            try
            {

                model.Delete(Id, out strmessage);

                if (strmessage.ToString() != "Successful")
                {
                    model.Message = Util.Messages.GetErroMessage(strmessage);
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.LeaveRuleAssignment = new LeaveRuleAssignment();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.LeaveRuleAssignmentDetails, model);
        }

        
        //GET: /LeaveRuleAssignment/LeaveRuleAssignmentAdd
        [HttpGet]
        [NoCache]
        public ActionResult LeaveRuleAssignmentAdd(string id)
        {
            LeaveRuleAssignmentModels model = new LeaveRuleAssignmentModels();
            try
            {
                model.Message = Util.Messages.GetErroMessage("");
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /LeaveRuleAssignment/LeaveRuleAssignmentAdd
        [HttpPost]
        [NoCache]
        public ActionResult LeaveRuleAssignmentAdd(LeaveRuleAssignmentModels model)
        {
            string strmessage = "";
            try
            {

                model.SaveData(model, out strmessage);

                if (strmessage.ToString() != "Successful")
                {
                    model.Message = Util.Messages.GetErroMessage(strmessage);
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.LeaveRuleAssignment = new LeaveRuleAssignment();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        //POST: /LeaveRuleAssignment/Index
        [HttpPost]
        [NoCache]
        public ActionResult Index(LeaveRuleAssignmentModels model)
        {
            string strmessage = "";
            try
            {
                model.SaveData(model, out strmessage);

                if (strmessage.ToString() != "Successful")
                {
                    model.Message = Util.Messages.GetErroMessage(strmessage);
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.LeaveRuleAssignment = new LeaveRuleAssignment();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        //POST: /LeaveRuleAssignment/OptionWisePageRefresh
        [HttpPost]
        [NoCache]
        public ActionResult OptionWisePageRefresh(LeaveRuleAssignmentModels model)
        {
            try
            { }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(LMS.Util.PartialViewName.LeaveRuleAssignmentDetails, model);
        }


        [NoCache]
        public JsonResult GetRules(LeaveRuleAssignmentModels model)
        {
            ArrayList list = new ArrayList();
            try
            {
                if (model.intSearchLeaveTypeID > 0)
                {

                    var items = (from item in model.GetLeaveTypewiseRules(model.intSearchLeaveTypeID)
                                 select new { item.intRuleID, item.strRuleName }).ToList();

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return Json(list);
        }


        [NoCache]
        public JsonResult GetEntitlement(LeaveRuleAssignmentModels model)
        {
            double dblEntitlement = 0;
            try
            {
                if (model.LeaveRuleAssignment.intLeaveTypeID > 0)
                {
                    dblEntitlement = model.GetEntitlement(model.LeaveRuleAssignment.intRuleID);

                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return Json(dblEntitlement);
        }


        [NoCache]
        public JsonResult GetLeaveTypeStatus(int Id)
        {
            var status = string.Empty;

            try
            {
                if (Id > 0)
                {
                    var leaveType = Common.fetchLeaveType().Where(y => y.intLeaveTypeID == Id).FirstOrDefault();
                    if (leaveType != null)
                    {
                        if (leaveType.isServiceLifeType == true)
                        {
                            status = "S";
                        }
                        else
                        {
                            status = "Y";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
    
    
    }
}
