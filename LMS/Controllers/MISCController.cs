using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web.Models;
using LMS.Util;
using System.Globalization;
using MvcContrib.Pagination;
using MvcPaging;
using System.IO;
using LMS.Web.Helpers;

namespace LMS.Web.Controllers
{
    //public class Message
    //{ 
    //    public string strMessage;
    //}

    public class MISCController : Controller
    {
        //
        // GET: /MISC/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        [NoCache]

        public ActionResult MISCLIST(int? page)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            MISCModels model = new MISCModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            MISCMaster searchObj = new MISCMaster();
            model.MISCMaster.strMISCDATE = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.MISCMaster.strMISCToDATE = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            searchObj.MISCDATE =DateTime.Parse(model.MISCMaster.strMISCDATE == null? "":model.MISCMaster.strMISCDATE);
            searchObj.MISCToDATE = DateTime.Parse(model.MISCMaster.strMISCToDATE);

            
            model.LstMISCMaster = model.GetData(searchObj,model.startRowIndex,model.maximumRows);
           
            model.LstMISCMasterPaging = model.LstMISCMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            return PartialView("MISCLIST", model);
        }

        public ActionResult MISCLIST(int? page ,MISCModels model)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;


            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            MISCMaster searchObj = new MISCMaster();
            searchObj.MISCDATE = DateTime.Parse(model.MISCMaster.strMISCDATE);
            searchObj.MISCToDATE = DateTime.Parse(model.MISCMaster.strMISCToDATE);
            model.MISCMaster = searchObj;

            model.LstMISCMaster = model.GetData(model.MISCMaster, model.startRowIndex, model.maximumRows);
            model.LstMISCMasterPaging = model.LstMISCMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            return PartialView("MISCLIST", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult MISC()
        {
            MISCModels model = new MISCModels();
            model.LstMISCDetails.Add(new MISCDetails());
            
            model.MISCMaster = new MISCMaster();
            model.MISCMaster.strMISCDATE = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
            model.MISCMaster.STREMPID = LoginInfo.Current.strEmpID;
            InitializeModel(model);
            return View(model);
            //return PartialView("MISCDetails", model);
        }

        private MISCModels InitializeModel(MISCModels model)
        {
            Employee objEmp = new Employee();
            if (!string.IsNullOrEmpty(model.MISCMaster.STREMPID))
            {
                objEmp = model.GetEmployeeInfo(model.MISCMaster.STREMPID);

                model.MISCMaster.Strdepartment = objEmp.strDepartment;
                model.MISCMaster.StrDesignation = objEmp.strDesignation;
                model.MISCMaster.EmpName = objEmp.strEmpName;
            }

            return model;
        }

        [HttpGet]
        [NoCache]
        public JsonResult getEmployeeInformation(MISCModels model)
        {
            Employee objEmp = new Employee();
            if (!string.IsNullOrEmpty(model.MISCMaster.STREMPID))
            {
                objEmp = model.GetEmployeeInfo(model.MISCMaster.STREMPID);

                model.MISCMaster.Strdepartment = objEmp.strDepartment;
                model.MISCMaster.StrDesignation = objEmp.strDesignation;



            }

            return Json(objEmp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NoCache]
        public ActionResult searchData(int? page, string StrEmpID, string strName, string StrDate, string strToDate)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            MISCModels model = new MISCModels();

            DateTime dt = StrDate != "" ? DateTime.Parse(StrDate) : DateTime.MinValue;

           // DateTime Todt = StrToDate != "" ? DateTime.Parse(StrToDate) : DateTime.MinValue;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            model.MISCMaster.MISCDATE = DateTime.Parse( StrDate);

            model.MISCMaster.MISCToDATE = DateTime.Parse(strToDate);

            model.MISCMaster.STREMPID =StrEmpID;
            model.MISCMaster.EmpName = strName;

            model.LstMISCMaster = model.GetSearchData(model.MISCMaster, model.startRowIndex, model.maximumRows);
            model.LstMISCMasterPaging = model.LstMISCMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            //InitializeModel(model);
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("MISCLIST", model);
        }


        [HttpPost]
        [NoCache]
        public ActionResult AddNode(MISCModels model)
        {
            model.IsNew = model.MISCMaster.IsNew;
            model.LstMISCDetails.Add(new MISCDetails());
            return PartialView("MISCDetails", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult DeleteNode(MISCModels model, string Id, FormCollection fc)
        {
            string savedFileName = "";
            if (model.LstMISCDetails.Count > 1)
            {
                IList<MISCDetails> lst = new List<MISCDetails>();
                MISCDetails obj = new MISCDetails();
                obj = model.LstMISCDetails[int.Parse(Id)];
                model.LstMISCDetails.Remove(obj);
                
                lst = model.LstMISCDetails;
                if (obj.ATTACHMENTPATH != null)
                {
                    savedFileName = Path.Combine(
                       AppDomain.CurrentDomain.BaseDirectory + "MISCAttachedFiles",
                       obj.ATTACHMENTPATH);
                    System.IO.File.Delete(savedFileName);
                }
                model.LstMISCDetails = null;
                model.LstMISCDetails = lst;

            }
            model.IsNew = model.MISCMaster.IsNew;
            ModelState.Clear();

            return View("MISCDetails", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult Index(MISCModels model)
        {
            //Save(model);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //[NoCache]
        //public  JsonResult Save(MISCModels model)
        //{
        //    int i = -1;
        //    Message msg = new Message();
            

        //    model.MISCMaster.STRCOMPANYID = LoginInfo.Current.strCompanyID;

        //    if (model.MISCMaster.MISCMASTERID > 0)
        //    {
        //        MISCModels.Update(model);
        //        msg.strMessage = "Information has been updated successfully";
        //    }
        //    else
        //    {
        //        i = MISCModels.Save(model, LoginInfo.Current.strEmpID);
        //        msg.strMessage = "Information has been saved successfully";
        //    }
        //    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
        //    model = new MISCModels();
                       
        //    msg.intID = i;
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        [NoCache]
        public JsonResult Save(MISCModels model, IEnumerable<HttpPostedFileBase> files)
        {
            int i = -1;
            Message msg = new Message();

           // IEnumerable<HttpPostedFileBase> files = Request.Files["files"] as IEnumerable<HttpPostedFileBase>;

            model.MISCMaster.STRCOMPANYID = LoginInfo.Current.strCompanyID;

            if (model.MISCMaster.MISCMASTERID > 0)
            {
                MISCModels.Update(model);
                msg.strMessage = "Information has been updated successfully";
            }
            else
            {
                i = MISCModels.Save(model, LoginInfo.Current.strEmpID);
                msg.strMessage = "Information has been saved successfully";
            }
            model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
            model = new MISCModels();

            msg.intID = i;
            return Json(msg, JsonRequestBehavior.AllowGet);
          //  return View("MISCDetails",model);
        }


        [HttpPost]
        public ActionResult AddFile(long? eventId, string description)
        {
            int id = 5;
            return Json(new { id });
        }

        public ActionResult Upload(string id)
        {
            MISCModels model = new MISCModels();
            model.LstMISCDetails.Add(new MISCDetails());
            model.startRowIndex = int.Parse(id);
            return View("MISCUpload", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            MISCModels model = new MISCModels();
            MISCMaster searchObj = new MISCMaster();
           
            try
            {
                model.GetSearchedData(Id);
                ModelState.Clear();

            }
            catch (Exception ex)
            {
               // model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
           
            return PartialView("MISC", model);

        }

        [HttpPost]
        [NoCache]
        public ActionResult uploadfiles(HttpPostedFileBase file,MISCModels model)
        {
            string savedFileName="";
            string fileName = "";

            if (file.FileName.ToString() != "")
            {
                 fileName = LoginInfo.Current.strEmpID.ToString() + "_" + model.MISCMaster.MISCDATE.ToString("dd-MM-yyyy").Replace('-','_');
                 savedFileName= Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory + "MISCAttachedFiles",
                   fileName+"_"+ Path.GetFileName(file.FileName));
                   file.SaveAs(savedFileName);

            }

            model.LstMISCDetails[0].ATTACHMENTPATH = fileName+"_"+Path.GetFileName(file.FileName);
            model.FileName = Path.GetFileName(file.FileName);
            return View("MISCUpload", model) ;
        }

        

        [HttpPost]
        public FileUploadJsonResult uploadfiles1(MISCModels model,FormCollection fc)
        {
            FileUploadJsonResult result = new FileUploadJsonResult();
            
            IEnumerable<HttpPostedFileBase> files = Request.Files["files"] as IEnumerable<HttpPostedFileBase>;

            string path = HttpContext.Request["qqfile"];
            object o = HttpContext.Request["fc"];
            MISCModels m = o as MISCModels;
            
            //foreach (string fl in HttpContext.Request.Params.AllKeys)
            //{
               
            //    //HttpPostedFileBase hpf = Request.Files[fl] as HttpPostedFileBase;
            //    //if (hpf.ContentLength == 0)
            //     //   continue;

            //    //do something with file
            //}


            //if (file.FileName.ToString() != "" && (Path.GetExtension(file.FileName.ToString()).ToLower() == ".mdb" ))
            //    {

            //    string savedFileName = Path.Combine(
            //       AppDomain.CurrentDomain.BaseDirectory,
            //       Path.GetFileName(file.FileName));
            //    file.SaveAs(savedFileName);
            //}

            Message msg = new Message();
            msg.strMessage = "sd";
            // return Json(msg, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        [NoCache]
        public ActionResult Refresh(MISCModels model)
        {
            //MISCModels model = new MISCModels();
            //model.LstMISCDetails.Add(new MISCDetails());

            

            model.LstMISCDetails = new List<MISCDetails>();
            model.LstMISCDetails.Add(new MISCDetails());
            //model.MISCMaster = new MISCMaster();
            model.MISCMaster.strMISCDATE = DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date);
           // return View(model);
           
             ModelState.Clear();
            return PartialView("MISCDetails", model);
        }
    }
}
