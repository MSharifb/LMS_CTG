using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web.Models;
using LMS.Util;

namespace LMS.Web.Controllers
{
    public class OutOfOfficeController : Controller
    {
        //
        // GET: /OutOfOffice/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {

            //OutOfOffice searchObj = new OutOfOffice();
            //ViewData["searchData"] = OutOfOfficeModels.GetData(searchObj, DateTime.MinValue, DateTime.MaxValue);
            return View();
        }

        [HttpPost]
        [NoCache]
        public ActionResult Index(OutOfOfficeModels model)
        {
            SaveOutOfOffice(model);
            return RedirectToAction("Index");
        }
       
        [HttpGet]
        [NoCache]
        public ActionResult Default()
        {
            //OutOfOffice searchObj = new OutOfOffice();
            //ViewData["searchData"] = OutOfOfficeModels.GetData(searchObj, DateTime.MinValue, DateTime.MaxValue, 1, 1);
            return View();
        }


        [HttpGet]
        [NoCache]
        public ActionResult OutOfOfficeAdd()
        {
            OutOfOfficeModels model = new OutOfOfficeModels();
            InitializeModel(model);
            return View(model);
        }

       [HttpGet]
        public ActionResult OutOfOfficeDetails(string Id)
        {
            //OutOfOffice searchObj = new OutOfOffice();
            //OutOfOfficeModels model = new OutOfOfficeModels();
            //searchObj.ID = long.Parse(Id);
            //int totalRows;
            //model.OutOfOffice = OutOfOfficeModels.GetData(searchObj, DateTime.MinValue, DateTime.MaxValue, 1, 1, out totalRows).SingleOrDefault();
            return View("OutOfOfficeEntry");
        }

        public ActionResult OutOfOfficeEntry()
        {
            return View();
        }


        public ActionResult Reports()
        {
            return View();
        }
       

        #region Add, Delete, update

        private void InitializeModel(OutOfOfficeModels model)
        {
            // OutOfOffice obj = new OutOfOffice();
            // model.OutOfOffice = obj;
            model.OutOfOffice.STREMPID = LoginInfo.Current.strEmpID;
            model.OutOfOffice.EMPNAME = LoginInfo.Current.EmployeeName;
        }

        private void SaveOutOfOffice(OutOfOfficeModels model)
        {
           // obj.STREMPID = "00258";
           
            //obj.STREMPID = LoginInfo.Current.strEmpID;
            //obj.GETOUTDATE = System.DateTime.Now;
           // obj.GETOUTTIME = System.DateTime.Now.ToShortTimeString();
            OutOfOfficeModels.Save(model, LoginInfo.Current.strCompanyID , LoginInfo.Current.strEmpID);           
        }
        #endregion
    }
}
