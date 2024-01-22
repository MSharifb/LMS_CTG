using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LMSEntity;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using System.Web.UI.WebControls;
using LMS.Web.Helpers;

namespace LMS.Web.Controllers
{
    public class AttendanceStatusController : Controller
    {
        //
        // GET: /AttendanceStatus/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /AttendanceStatus/AttendanceStatus
        [HttpGet]
        [NoCache]
        public ActionResult AttendanceStatus(int? page)
        {
            AttendanceStatusModels model = new AttendanceStatusModels();
            try
            {
                model.AtdStatusSetup = model.GetAttendanceStatus(Convert.ToInt32(page));
            }
            catch (Exception ex) { }

            return PartialView("StatusDetails", model);
            //return View(model);
        }

        
   
        //POST: /AttendanceStatus/SaveAttendanceStatus
        [HttpPost]
        [NoCache]
        public ActionResult AttendanceStatus(AttendanceStatusModels model)
        {
            string strmsg = "";
            try
            {
                //SaveData
                model.SaveData(model);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg.ToString());
                }
                else
                {
                    model = GetModelWithData();
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }

            return PartialView("StatusDetails", model);
        }



        [NoCache]
        private AttendanceStatusModels GetModelWithData()
        {
            AttendanceStatusModels model = new AttendanceStatusModels();
            model.AtdStatusSetup = model.GetAttendanceStatus(0);
            return model;
        }


    }
}
