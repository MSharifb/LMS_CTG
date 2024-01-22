using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using LMS.BLL;
using System.Configuration;
using MvcPaging;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class OfficeTimeController : Controller
    {
        //GET: /OfficeTime/
        [NoCache]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /OfficeTime/OfficeTime
        [HttpGet]
        [NoCache]
        public ActionResult OfficeTime(int? page)
        {
            OfficeTimeModels model = new OfficeTimeModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetOfficeTimeAll();
                model.LstOfficeTimePaging = model.LstOfficeTime.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(model);
        }


        //POST: /OfficeTime/OfficeTime
        [HttpPost]
        [NoCache]
        public ActionResult OfficeTime(int? page, OfficeTimeModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.GetOfficeTimeAll();
                model.LstOfficeTimePaging = model.LstOfficeTime.ToPagedList(currentPageIndex, AppConstant.PageSize10);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.OfficeTime, model);
        }

        
        //GET: /OfficeTime/OfficeTimeAdd                  
        [HttpGet]
        [NoCache]
        public ActionResult OfficeTimeAdd(string id)
        {

            OfficeTimeModels model = new OfficeTimeModels();
            try
            {
                model.Message = Util.Messages.GetSuccessMessage("");
                model.rowID = 0;
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }



        //POST: /OfficeTime/AddNewTime
        [HttpPost]
        [NoCache]
        public ActionResult AddNewTime(OfficeTimeModels model)
        {                        
            DateTime dtFromDateTime,dtToDateTime; 
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            OfficeTimeDetails offTD = new OfficeTimeDetails();

            bool IsOverlap = false;
            model.BlnTextBlank = false;

            try
            {
                dtfi.ShortDatePattern = LMS.Util.DateTimeFormat.DateTime;
                dtfi.DateSeparator = LMS.Util.DateTimeFormat.DateSeparator;
                CreateDateTime(model.OfficeTimeDetails.strStartTime,model.OfficeTimeDetails.strEndTime, out dtFromDateTime, out dtToDateTime);

                if (model.LstOfficeTimeDetails == null)
                {        
                    model.LstOfficeTimeDetails = new List<OfficeTimeDetails>();
                }
                else
                {
                    offTD = model.LstOfficeTimeDetails.Where(c => c.intLeaveYearID == model.OfficeTime.intLeaveYearID && c.strDurationName == model.OfficeTimeDetails.strDurationName).SingleOrDefault();
                    if (offTD != null)
                    {
                        model.Message = Util.Messages.GetErroMessage("Duration name already exists in the selected leave year.");
                        return View(LMS.Util.PartialViewName.OfficeTimeDetails, model);
                    }

                    foreach (OfficeTimeDetails obj in model.LstOfficeTimeDetails)
                    {
                        IsOverlap = false;
                        DateTime dtSTime , dtETime ;
                        CreateDateTime(obj.strStartTime, obj.strEndTime, out dtSTime, out dtETime);                                                                                          
                        if ((dtSTime <= dtFromDateTime && dtETime >= dtFromDateTime) || (dtSTime <= dtToDateTime && dtETime >= dtToDateTime))
                        {
                            IsOverlap = true;
                            break;
                        }                                      
                    }
                    if (IsOverlap == true)
                    {
                        model.Message = Util.Messages.GetErroMessage("Working time could not overlap with other duration.");
                        return View(LMS.Util.PartialViewName.OfficeTimeDetails, model);
                    }
                }
               // model.OfficeTimeDetails.strDurationName =  model.OfficeTimeDetails.strDurationName.Replace("'","\'");

                if (model.LstOfficeTimeDetails != null)
                {
                    int maxId = (model.LstOfficeTimeDetails.Count == 0 ? 0 : model.LstOfficeTimeDetails.Max(c =>Math.Abs( c.intDurationID))) + 1;
                    model.OfficeTimeDetails.intDurationID = -maxId;
                }
                else
                {
                    model.OfficeTimeDetails.intDurationID = -1;
                }
                model.OfficeTimeDetails.intLeaveYearID = model.OfficeTime.intLeaveYearID;
                model.LstOfficeTimeDetails.Add(model.OfficeTimeDetails);
                model.BlnTextBlank = true;
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.OfficeTimeDetails, model);
        }


        //POST: /OfficeTime/DeleteTime
        [HttpPost]
        [NoCache]
        public ActionResult DeleteTime(OfficeTimeModels model, int Id)
        {
            OfficeTimeDetails offTD = new OfficeTimeDetails();
            try
            {
                offTD = model.LstOfficeTimeDetails.Where(c => c.intLeaveYearID == model.OfficeTime.intLeaveYearID && c.intDurationID == Id).SingleOrDefault();
                model.LstOfficeTimeDetails.Remove(offTD);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView(LMS.Util.PartialViewName.OfficeTimeDetails, model);

        }




        //POST: /OfficeTime/OfficeTimeAdd
        [HttpPost]
        [NoCache]
        public ActionResult OfficeTimeAdd(OfficeTimeModels model)
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
                        ModelState.Clear();
                        model = new OfficeTimeModels();
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        if (LoginInfo.Current.intLeaveYearID == model.OfficeTime.intLeaveYearID)
                        {
                            LoginInfo.Current.fltOfficeTime = (float)model.OfficeTime.fltDuration;
                        }
                        //model.OfficeTime = new OfficeTime();
                        
                       
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //GET: /OfficeTime/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(string id)
        {
            OfficeTimeModels model = new OfficeTimeModels();
            model.Message = Util.Messages.GetSuccessMessage("");
            model.rowID = 1;
            try
            {
                //model.OfficeTime = model.GetOfficeTime(id);
                model.GetOfficeTime(id);

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(model);
        }


        //POST: /OfficeTime/SaveOfficeTime
        [HttpPost]
        [NoCache]
        public ActionResult Details(OfficeTimeModels model)
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
                        if (LoginInfo.Current.intLeaveYearID == model.OfficeTime.intLeaveYearID)
                        {
                            LoginInfo.Current.fltOfficeTime = (float)model.OfficeTime.fltDuration;
                        }                      
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

        
        //POST: /OfficeTime/SaveOfficeTime
        [HttpPost]
        [NoCache]
        public ActionResult SaveOfficeTime(OfficeTimeModels model)
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
                        model.rowID = 0;
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                        if (LoginInfo.Current.intLeaveYearID == model.OfficeTime.intLeaveYearID)
                        {
                            LoginInfo.Current.fltOfficeTime = (float)model.OfficeTime.fltDuration;
                        }
                        model.OfficeTime = new OfficeTime();
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return View(LMS.Util.PartialViewName.OfficeTimeDetails, model);
        }

        
        //POST: /OfficeTime/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(OfficeTimeModels model)
        {
            int yearId = model.OfficeTime.intLeaveYearID;

            try
            {
                int id = model.Delete(yearId);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.rowID = 0;
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    if (LoginInfo.Current.intLeaveYearID == model.OfficeTime.intLeaveYearID)
                    {
                        LoginInfo.Current.fltOfficeTime = 0;
                    }
                    model.OfficeTime = new OfficeTime();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.OfficeTimeDetails, model);
        }

        
        //POST: /OfficeTime/Create
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

        //GET: /OfficeTime/Edit
        [HttpGet]
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //POST: /OfficeTime/Edit
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
        public JsonResult CalcutateDuration(OfficeTimeModels model)
        {
            double fltHrs = 0;
            DateTime dtFromDateTime,dtToDateTime;
            try
            {                              
                CreateDateTime(model.OfficeTimeDetails.strStartTime,model.OfficeTimeDetails.strEndTime, out dtFromDateTime, out dtToDateTime);
                TimeSpan ts1 = dtToDateTime.Subtract(dtFromDateTime);
                fltHrs = Math.Round(ts1.TotalHours,2);
                model.OfficeTimeDetails.fltDuration = fltHrs;
            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(ex.Message);
            }

            return Json(fltHrs);
        }

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
