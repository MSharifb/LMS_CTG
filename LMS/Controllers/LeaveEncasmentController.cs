using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using LMSEntity;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using MvcPaging;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class LeaveEncasmentController : Controller
    {
        //GET: /LeaveEncasment/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        
        //GET: /LeaveEncasment/LeaveEncasment
        [HttpGet]
        [NoCache]
        public ActionResult LeaveEncasment(int? page)
        {
            LeaveEncasmentModels model = new LeaveEncasmentModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetLeaveEncasmentAll();
                model.LstLeaveEncasmentPaging = model.LstLeaveEncasment.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /LeaveEncasment/LeaveEncasment
        [HttpPost]
        [NoCache]
        public ActionResult LeaveEncasment(int? page, LeaveEncasmentModels model)
        {

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.GetLeaveEncasmentAll();
                model.LstLeaveEncasmentPaging = model.LstLeaveEncasment.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.LeaveEncasment, model);
        }

        
        //GET: /LeaveEncasment/LeaveEncasmentAdd
        [HttpGet]
        [NoCache]
        public ActionResult LeaveEncasmentAdd(string id)
        {
            LeaveEncasmentModels model = new LeaveEncasmentModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            try
            {
                InitializeModel(model);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        
        //POST: /LeaveEncasment/LeaveEncasmentAdd
        [HttpPost]
        [NoCache]
        public ActionResult LeaveEncasmentAdd(LeaveEncasmentModels model)
        {
            string strmsg = "";
            try
            {
                string strPaymentDate = "01-" + Common.GetMonthNo(model.LeaveEncasment.strPaymentMonth).ToString() + "-" + model.LeaveEncasment.intPaymentYear.ToString();
                DateTime dtPaymentdate = DateTime.Parse(strPaymentDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                DateTime dtStartdate = DateTime.Parse(model.StrYearStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                if (dtPaymentdate >= dtStartdate)
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
                            model.LeaveEncasment = new LeaveEncasment();
                            InitializeModel(model);

                            ModelState.Clear();
                        }
                    }
                }
                else
                {
                    model.Message = Util.Messages.GetErroMessage("Payment year and month must be equal or greater than start date of current leave year.");
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        
        //GET: /LeaveEncasment/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int id)
        {
            LeaveEncasmentModels model = new LeaveEncasmentModels();
            model.Message = Util.Messages.GetSuccessMessage("");

            try
            {
                model.LeaveEncasment = model.GetLeaveEncasment(id);
                model.ActiveLeaveYear = model.GetLeaveYear(model.LeaveEncasment.intLeaveYearID);
                model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }

        
        //POST: /LeaveEncasment/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(LeaveEncasmentModels model)
        {
            string strmsg = "";
            try
            {
                string strPaymentDate = "01-" + Common.GetMonthNo(model.LeaveEncasment.strPaymentMonth).ToString() + "-" + model.LeaveEncasment.intPaymentYear.ToString();
                DateTime dtPaymentdate = DateTime.Parse(strPaymentDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                DateTime dtStartdate = DateTime.Parse(model.StrYearStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                if (dtPaymentdate >= dtStartdate)
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
                            InitializeModel(model);
                            ModelState.Clear();
                        }
                    }
                }
                else
                {
                    model.Message = Util.Messages.GetErroMessage("Payment year and month must be equal or greater than start date of current leave year.");
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        
        //POST: /LeaveEncasment/SaveLeaveEncasment
        [HttpPost]
        [NoCache]
        public ActionResult SaveLeaveEncasment(LeaveEncasmentModels model)
        {
            string strmsg = "";
            try
            {
                string strPaymentDate = "01-" + Common.GetMonthNo(model.LeaveEncasment.strPaymentMonth).ToString() + "-" + model.LeaveEncasment.intPaymentYear.ToString();
                DateTime dtPaymentdate = DateTime.Parse(strPaymentDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                DateTime dtStartdate = DateTime.Parse(model.StrYearStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                if (dtPaymentdate >= dtStartdate)
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
                            model.LeaveEncasment = new LeaveEncasment();
                            InitializeModel(model);
                            ModelState.Clear();
                        }
                    }
                }
                else
                {
                    model.Message = Util.Messages.GetErroMessage("Payment year and month must be equal or greater than start date of current leave year.");
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);

        }

        
        //POST: /LeaveEncasment/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(LeaveEncasmentModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.LeaveEncasment = new LeaveEncasment();
                    InitializeModel(model);
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveEncasmentDetails, model);
        }

        
        //POST: /LeaveEncasment/Create
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

        //GET: /LeaveEncasment/Edit
        public ActionResult Edit(int id)
        {
            return View();
        }

        
        //POST: /LeaveEncasment/Edit
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

        
        //GET: /LeaveEncasment/OptionWisePageRefresh
        [HttpGet]
        [NoCache]
        public ActionResult OptionWisePageRefresh(LeaveEncasmentModels model)
        {
            try
            {

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }
            return View(model);
        }

        
        //GET: /LeaveEncasment/GetLeaveBalance   
        [NoCache]
        public JsonResult GetLeaveBalance(LeaveEncasmentModels model)
        {
            double leaveCL = 0;
            double currECash = 0;
            double leaveECashed = 0;

            ArrayList list = new ArrayList();
            LeaveRule objLvRule = new LeaveRule();
            try
            {
                if (model.LeaveEncasment.intLeaveYearID > 0 && model.LeaveEncasment.intLeaveTypeID > 0 && model.LeaveEncasment.strEmpID != "")
                {
                    leaveCL = model.GetLeaveBalance(model.LeaveEncasment.intLeaveYearID, model.LeaveEncasment.intLeaveTypeID, model.LeaveEncasment.strEmpID) / LoginInfo.Current.fltOfficeTime;
                    objLvRule = model.GetMaxMinEncashableLeave(model.LeaveEncasment.strEmpID, model.LeaveEncasment.intLeaveTypeID);
                    leaveECashed = model.GetLeaveEncahed(model.LeaveEncasment.intLeaveEncaseID, model.LeaveEncasment.intLeaveYearID, model.LeaveEncasment.intLeaveTypeID, model.LeaveEncasment.strEmpID);
                }

                if (model.LeaveEncasment.intLeaveEncaseID > 0)
                {
                    model.LeaveEncasment = model.GetLeaveEncasment(model.LeaveEncasment.intLeaveEncaseID);
                    currECash = model.LeaveEncasment.fltEncaseDuration;
                }

                list.Add(leaveCL + currECash);
                list.Add(objLvRule.intMaxEncahDays);
                list.Add(objLvRule.intMinDaysInHand);
                list.Add(leaveECashed);

            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }


        
        
        [NoCache]
        private void InitializeModel(LeaveEncasmentModels model)
        {
            model.ActiveLeaveYear = model.GetLeaveYear(LoginInfo.Current.intLeaveYearID);
            model.StrYearStartDate = model.ActiveLeaveYear.strStartDate;

            model.LeaveEncasment.intLeaveYearID = model.ActiveLeaveYear.intLeaveYearID;
            model.LeaveEncasment.strYearTitle = model.ActiveLeaveYear.strYearTitle;
        }

        [HttpGet]
        [NoCache]
        public ActionResult GetLeaveTypeList(LeaveEncasmentModels model)
        {
            List<LeaveType> lst = model.GetLeaveTypeList(model.LeaveEncasment.strBranchID, model.LeaveEncasment.strDepartmentID, model.LeaveEncasment.strDesignationID);
            ModelState.Clear();

            List<SelectListItem> itemList = new List<SelectListItem>();

            foreach (LeaveType item in lst)
            {
                SelectListItem selectitem = new SelectListItem();
                selectitem.Value = item.intLeaveTypeID.ToString();
                selectitem.Text = item.strLeaveType;
                itemList.Add(selectitem);
            }

            return Json(new SelectList(itemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }
    }
}
