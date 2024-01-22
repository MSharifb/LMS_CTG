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
using LMS.BLL;
using MvcPaging;
using System.Text.RegularExpressions;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class LeaveYearController : Controller
    {
        //GET: /LeaveYear/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        
        //GET: /LeaveYear/LeaveYear
        [HttpGet]
        [NoCache]
        public ActionResult LeaveYear(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            LeaveYearModels model = new LeaveYearModels();

            try
            {
                model.GetLeaveYearAll(model.intSearchLeaveYearTypeId);
                model.LstLeaveYearPaging = model.LstLeaveYear.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveYear, model);
        }


        //POST: /LeaveYear/LeaveYear
        [HttpPost]
        [NoCache]
        public ActionResult LeaveYear(int? page, LeaveYearModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetLeaveYearAll(model.intSearchLeaveYearTypeId);
                model.LstLeaveYearPaging = model.LstLeaveYear.ToPagedList(currentPageIndex, AppConstant.PageSize10);

                /* intLeaveYearIDTemp 's value is set in the Add and Detail action*/

                //if ((LoginInfo.Current.intLeaveYearIDTemp > 0 && LoginInfo.Current.intLeaveYearID == 0)
                // || (LoginInfo.Current.intLeaveYearIDTemp == 0 && LoginInfo.Current.intLeaveYearID > 0))
                //{
                //    string js = "<script type='text/Javascript'> $('#leaveYearId').val(" + LoginInfo.Current.intLeaveYearID.ToString() + "); $('#liId2 a').click();</script>";
                //    return JavaScript(js);
                //}
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveYear, model);
        }


        //GET: /LeaveYear/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            LeaveYearModels model = new LeaveYearModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            try
            {
                model.GetLeaveYearAll(model.intSearchLeaveYearTypeId);
                model.LeaveYear = model.GetLeaveYear(Id);

                model.LeaveYear.startDateDay = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("dd");
                model.LeaveYear.startDateMonth = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("MMMM");
                model.LeaveYear.startDateYear = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("yyyy");

                if (model.LstLeaveYear.Count > 1)
                {
                    model.IsExists = true;
                }
                else
                {
                    model.IsExists = false;
                }

                /* storing the value for temporary and it will be used in the  LeaveYear(int? page, LeaveYearModels model) action*/
                LoginInfo.Current.intLeaveYearIDTemp = LoginInfo.Current.intLeaveYearID;
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /LeaveYear/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(LeaveYearModels model)
        {
            try
            {
                model.LeaveYear.dtStartDate = Convert.ToDateTime(model.LeaveYear.startDateYear + "-" + model.LeaveYear.startDateMonth + "-" + model.LeaveYear.startDateDay);

                int id = model.SaveData(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());

                    if (model.LeaveYear.bitIsActiveYear)
                    {
                        LoginInfo.Current.intLeaveYearID = model.LeaveYear.intLeaveYearID;

                        OfficeTimeBLL objBLL = new OfficeTimeBLL();
                        OfficeTime objOFT = new OfficeTime();
                        objOFT = objBLL.OfficeTimeGet(LoginInfo.Current.strCompanyID, model.LeaveYear.intLeaveYearID);
                        if (objOFT != null)
                        {
                            LoginInfo.Current.fltOfficeTime = (float)objOFT.fltDuration;
                        }
                        else
                        {
                            model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString() + " Please input Office Hour for the year " + model.LeaveYear.strYearTitle.ToString());
                        }
                    }
                    else
                    {
                        /*If model is updated by active leave year to inactive leave year, then we need to fetch the current active leave year*/
                        /*If this model had in active mode, in this case intLeaveYearId is 0*/
                        LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
                        LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(LoginInfo.Current.strEmpID, LoginInfo.Current.strCompanyID);
                        LoginInfo.Current.intLeaveYearID = loginDetails.intLeaveYearID;
                    }

                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /LeaveYear/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(FormCollection fc)
        {
            LeaveYearModels model = new LeaveYearModels();
            int yearId = int.Parse(fc.Get("intLeaveYearID").ToString());
            try
            {
                int id = model.Delete(yearId);
               
                if (id < 0)
                {
                    model.LeaveYear = model.GetLeaveYear(yearId);
                    model.LeaveYear.startDateDay = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("dd");
                    model.LeaveYear.startDateMonth = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("MMMM");
                    model.LeaveYear.startDateYear = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("yyyy");

                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model = GetModel();

                    if (LoginInfo.Current.intLeaveYearID == yearId)
                    {
                        LoginInfo.Current.intLeaveYearID = 0;
                    }

                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.LeaveYear = model.GetLeaveYear(yearId);
                model.LeaveYear.startDateDay = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("dd");
                model.LeaveYear.startDateMonth = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("MMMM");
                model.LeaveYear.startDateYear = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("yyyy");

                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.LeaveYearDetails, model);
        }

        
        //GET: /LeaveYear/LeaveYearAdd
        [HttpGet]
        [NoCache]
        public ActionResult LeaveYearAdd(string id)
        {
            LeaveYearModels model = new LeaveYearModels();
            try
            {
                //model = GetModel();
                model.Message = Util.Messages.GetSuccessMessage("");
                /* storing the value for temporary and it will be used in the  LeaveYear(int? page, LeaveYearModels model) action*/
                LoginInfo.Current.intLeaveYearIDTemp = LoginInfo.Current.intLeaveYearID;
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /LeaveYear/Delete
        [HttpPost]
        [NoCache]
        public ActionResult LeaveYearAdd(LeaveYearModels model)
        {
            try
            {
                if (!ValidateModel(model))
                {
                    return View(model);
                }
                model.LeaveYear.dtStartDate = Convert.ToDateTime(model.LeaveYear.startDateYear + "-" + model.LeaveYear.startDateMonth + "-" + model.LeaveYear.startDateDay);
                int id = model.SaveData(model);
                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {

                    if (id > 0 && model.LeaveYear.bitIsActiveYear)
                    {
                        LoginInfo.Current.intLeaveYearID = id;

                        OfficeTimeBLL objBLL = new OfficeTimeBLL();
                        OfficeTime objOFT = new OfficeTime();
                        objOFT = objBLL.OfficeTimeGet(LoginInfo.Current.strCompanyID, id);
                        if (objOFT != null)
                        {
                            LoginInfo.Current.fltOfficeTime = (float)objOFT.fltDuration;
                        }
                        else
                        {
                            model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString() + " Please input Office Hour for the year " + model.LeaveYear.strYearTitle.ToString());
                        }
                    }

                    model = GetModel();
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }

        private bool ValidateModel(LeaveYearModels model)
        {
            Regex r1 = new Regex("^[0-9]{4}$");
            Regex r2 = new Regex("^[0-9]{4}-[0-9]{4}$");

            if (!r1.IsMatch(model.LeaveYear.strYearTitle) && !r2.IsMatch(model.LeaveYear.strYearTitle))
            {
                model.Message = Util.Messages.GetErroMessage("Please provide valid leave year name.");
                return false;
            }

            return true;
        }
        
        [NoCache]
        public JsonResult CreateEndDate(string startDateDay, string startDateMonth, string startDateYear)
        {
            string EndDate = string.Empty;
            string leaveYear = string.Empty;
            try
            {
                if (startDateYear != "" && startDateYear.Length==4)
                {
                    DateTime startdate = Convert.ToDateTime(startDateYear + "-" + startDateMonth + "-" + startDateDay);

                    //DateTime startdate = DateTime.Parse(collection.Get("strStartDate").ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                    var dt = startdate.AddMonths(12).AddDays(-1);
                    EndDate = dt.ToString(LMS.Util.DateTimeFormat.Date);

                    if (startdate.Year == dt.Year)
                    {
                        leaveYear = Convert.ToString(startdate.Year);
                    }
                    else
                    {
                        leaveYear = Convert.ToString(startdate.Year) + "-" + Convert.ToString(dt.Year);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["vdMsg"] = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return Json(new { EndDate = EndDate, leaveYear = leaveYear }, JsonRequestBehavior.AllowGet);
            //return Json(EndDate);
        }


        [NoCache]
        public JsonResult CheckValidData(LeaveYearModels model)
        {
            string strMSG = "";
            try
            {
                if (model.LeaveYear.strYearTitle.ToString() != "")
                {
                    model.GetLeaveYearAll(model.intSearchLeaveYearTypeId);
                    LeaveYear objLY = new LeaveYear();
                    objLY = model.LstLeaveYear.Where(c => c.strYearTitle == model.LeaveYear.strYearTitle && c.intLeaveYearID != model.LeaveYear.intLeaveYearID).SingleOrDefault();
                    if (objLY!=null)
                    {
                        strMSG = "Leave year title already exists.";
                    }                    
                }
            }
            catch (Exception ex)
            {
                ViewData["vdMsg"] = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return Json(strMSG);
        }

        [NoCache]
        private LeaveYearModels GetModel()
        {
            LeaveYearModels model = new LeaveYearModels();
            DateTime startdate = DateTime.Today;
            try
            {
                model.GetLeaveYearAll(model.intSearchLeaveYearTypeId);
                if (model.LstLeaveYear.Count > 0)
                {
                    startdate = model.LstLeaveYear.Max(c => c.dtEndDate);
                    startdate = startdate.AddDays(1);
                }

                model.LeaveYear.dtStartDate = startdate;

                model.LeaveYear.startDateDay = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("dd");
                model.LeaveYear.startDateMonth = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("MMMM");
                model.LeaveYear.startDateYear = Convert.ToDateTime(model.LeaveYear.dtStartDate).ToString("yyyy");

                startdate = startdate.AddMonths(12);
                model.LeaveYear.dtEndDate = startdate.AddDays(-1);

                if (model.LstLeaveYear.Count > 0)
                {
                    model.IsExists = true;
                }
                else
                {
                    model.IsExists = false;
                }
            }
            catch (Exception ex)
            {
            }
            return model;
        }


        [NoCache]
        public JsonResult GetMonthDateName(int intLeaveYearTypeId)
        {
            //double dblEntitlement = 0;
            var startDateDay = string.Empty;
            var startDateMonth = string.Empty;
            var yearType = string.Empty;

            LeaveYearTypeModels model = new LeaveYearTypeModels();
            

            try
            {
                if (intLeaveYearTypeId != 0)
                {
                    var LeaveYear = model.GetLeaveYearType(intLeaveYearTypeId);
                    if (LeaveYear != null)
                    {
                        startDateDay = "01";
                        startDateMonth = LeaveYear.StartMonth;
                    }

                    //DateTime dblEntitlement = Convert.ToDateTime(DateTime.Now.Year + "-" + startMonth + "-01");
                    //var dblEntitlement1 = dblEntitlement.AddYears(1).AddDays(-1);
                    //endMonth = dblEntitlement1.ToString("MMMM");
                    //sMonth = dblEntitlement.ToString("MMMM");
                    //yearType = sMonth + "-" + endMonth;
                }
            }
            catch (Exception ex)
            {
                //model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return Json(new { startDateDay = startDateDay, startDateMonth = startDateMonth }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Added For Mongla
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="dtFromDateTime"></param>
        /// <param name="dtToDateTime"></param>
        private void CreateDateTime(string strStartTime, string strEndTime, out DateTime dtFromDateTime, out DateTime dtToDateTime)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
            dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;

            char[] sepAr = { ':', ' ' };
            dtFromDateTime = DateTime.Today;
            dtToDateTime = DateTime.Today;

            try
            {
                if (!string.IsNullOrEmpty(strStartTime))
                {
                    string[] time = strStartTime.Split(sepAr);
                    try
                    {
                        string strDt = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                        dtFromDateTime = Convert.ToDateTime(strDt, dtfi);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (!string.IsNullOrEmpty(strEndTime))
                {
                    string[] time = strEndTime.Split(sepAr);
                    try
                    {
                        string strDt = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date) + " " + time[0] + ":" + time[1] + " " + time[2];
                        dtToDateTime = Convert.ToDateTime(strDt, dtfi);
                    }
                    catch (Exception ex)
                    {
                    }
                }

            }
            catch (Exception ex)
            { }
        }

    }
}
