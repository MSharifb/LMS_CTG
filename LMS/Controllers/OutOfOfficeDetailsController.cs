using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web.Models;
using LMS.Util;
using MvcPaging;
using MvcContrib.Pagination;

namespace LMS.Web.Controllers
{
    public class Message
    {
        public string strMessage;
        public string strGetInTime;
        public int intID { get; set; }
    }


    public class OutOfOfficeDetailsController : Controller
    {
        //
        // GET: /OutOfOfficeDetails/

        public ActionResult Index()
        {
            
            return View();
        }

     
        private OutOfOfficeModels SetPaging(int? page)
        {
            OutOfOfficeModels model = new OutOfOfficeModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
                       
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
          
            OutOfOffice searchObj = new OutOfOffice();
            model.LstOutOfOffice = model.GetData(searchObj, DateTime.MinValue, DateTime.MaxValue, model.startRowIndex, model.maximumRows);

            model.LstOutOfOfficePaging = model.LstOutOfOffice.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            return model;

        }

      
        [HttpPost]
        [NoCache]
        public ActionResult search(int? page, OutOfOfficeModels model)
        {
           
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
           
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            
            OutOfOffice searchObj = new OutOfOffice();
            if (model.EmployeeName !="" &&  model.EmployeeName !=null)
                searchObj.STREMPID = model.StrEmpID;
            
            searchObj.PURPOSE = model.Purpose;
            model.LstOutOfOffice = model.GetData(searchObj, DateTime.MinValue, DateTime.MaxValue, model.startRowIndex, model.maximumRows);
            model.LstOutOfOfficePaging = model.LstOutOfOffice.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            InitializeModel(model, true);   
            
            ModelState.Clear();
            HttpContext.Response.Clear();
            model.Purpose = searchObj.PURPOSE;
            return View("OutOfOfficeDetails", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult OutOfOfficeDetails(int? page)
        {
            OutOfOfficeModels model = SetPaging(page);
            ModelState.Clear();
            HttpContext.Response.Clear();
            return View(model);
        }

        // POST: /LeaveApplication/LeaveApplication
        [HttpPost]
        [NoCache]
        public ActionResult OutOfOfficeDetails(int? page, OutOfOfficeModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
                       
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;

            OutOfOffice objSearch = new OutOfOffice();

          
            model.LstOutOfOffice = model.GetData(objSearch, DateTime.MinValue, DateTime.MaxValue, model.startRowIndex, model.maximumRows);
            
            model.LstOutOfOfficePaging = model.LstOutOfOffice.ToPagedList(currentPageIndex, AppConstant.PageSize10);

                     
            HttpContext.Response.Clear();

            return PartialView("OutOfOfficeDetails", model);
        }

        // POST: /LeaveApplication/getPagedData
        [HttpPost]
        [NoCache]
        public ActionResult getPagedData(FormCollection collection)
        {
            string strPageIndex = collection.Get("txtPageNo");

            int pageNo = 1;
            int.TryParse(strPageIndex, out pageNo);
            if (pageNo < 1)
            {
                pageNo = 1;
            }

            OutOfOfficeModels model = new OutOfOfficeModels();
            OutOfOffice searchObj = new OutOfOffice();
          

            model.LstOutOfOffice = (IList<OutOfOffice>)model.GetData(searchObj, DateTime.MinValue, DateTime.MaxValue, 1, 1).AsPagination(pageNo, AppConstant.PageSize10); // model.ls.AsPagination(pageNo, AppConstant.PageSize);
           
            return PartialView("OutOfOfficeDetails", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult OutOfOfficeEntry()
        {
           
            return View();
        }

        public ActionResult OutOfOfficeAdd()
        {
            OutOfOfficeModels model = new OutOfOfficeModels();
            //EmployeeWiseOOAApprovalPathModels m = new EmployeeWiseOOAApprovalPathModels();
            //m.EmployeeWiseOOAApprovalPath.strEmpID = LoginInfo.Current.strEmpID;
            //EmployeeWiseOOAApprovalPath obj  =   m.GetEmployeeWiseOOAApprovalPath(m);

            //if (obj != null)
            //{
            //    if (obj.intFlowType > 0)
            //        model.HasFlow = true;
            //}
            
            IList<OutOfOfficeLocaton> lst = new List<OutOfOfficeLocaton>();
            OutOfOfficeLocaton OOALocation = new OutOfOfficeLocaton();
            OOALocation.FROMLOCATION = "Office";
            lst.Add(OOALocation);
            model.LstOutOfOfficeLocation = lst;
            InitializeModel(model,false);
            model.IsNew = true;
            model.OutOfOffice.IsNew = true;
            model.Message = "";
            return View(model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult AddNode(OutOfOfficeModels model)
        {
            model.IsNew = model.OutOfOffice.IsNew;
            model.LstOutOfOfficeLocation.Add(new OutOfOfficeLocaton());
            return PartialView("OutOfOfficeEntry", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult DeleteNode(OutOfOfficeModels model, string Id, FormCollection fc)
        {
            if (model.LstOutOfOfficeLocation.Count > 1)
            {
                IList<OutOfOfficeLocaton> lst = new List<OutOfOfficeLocaton>();
                OutOfOfficeLocaton obj = new OutOfOfficeLocaton();
                obj = model.LstOutOfOfficeLocation[int.Parse(Id)];
                model.LstOutOfOfficeLocation.Remove(obj);
                
                lst = model.LstOutOfOfficeLocation;

                model.LstOutOfOfficeLocation = null;                
                model.LstOutOfOfficeLocation = lst;

            }
            model.IsNew = model.OutOfOffice.IsNew;
            ModelState.Clear();
            return View("OutOfOfficeEntry", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult OutOfOfficeAdd(OutOfOfficeModels model)
        {

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string d = model.OutOfOffice.STRGETOUTDATE + " " + model.OutOfOffice.GETOUTTIME;

            model.OutOfOffice.EXPGETINDATE =  DateTime.Parse(d);
            model.OutOfOffice.GETOUTDATE =  DateTime.Parse(model.OutOfOffice.STRGETOUTDATE);
            SaveOutOfOffice(model);

            model.Message = Util.Messages.GetSuccessMessage("Application Inserted Successfully.");
            ModelState.Clear();

            return RedirectToAction("OutOfOfficeDetails");
          
        }

        [HttpGet]
        [NoCache]
        public JsonResult OutOfOfficeDraft(OutOfOfficeModels model)
        {
            Message msg = new Message();
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                string d = model.OutOfOffice.STRGETOUTDATE + " " + model.OutOfOffice.GETOUTTIME;

                model.OutOfOffice.EXPGETINDATE = DateTime.Parse(d);
                model.OutOfOffice.GETOUTDATE = DateTime.Parse(model.OutOfOffice.STRGETOUTDATE);

                if (model.OutOfOffice.RESPONSIBLEPERSON == "")
                    model.OutOfOffice.RESPONSIBLEPERSONID = "";

                model.OutOfOffice.STATUS = "GD";
                SaveOutOfOffice(model);

                msg.strMessage = "Drafted Successflly.";

            }
            catch (Exception ex)
            {
                msg.strMessage = ex.Message;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NoCache]
        public JsonResult OutOfOfficeGetOut(OutOfOfficeModels model)
        {
            Message msg = new Message();
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                string d = model.OutOfOffice.STRGETOUTDATE + " " + model.OutOfOffice.GETOUTTIME;

                model.OutOfOffice.EXPGETINDATE = DateTime.Parse(d);
                model.OutOfOffice.GETOUTDATE = DateTime.Parse(model.OutOfOffice.STRGETOUTDATE);

                if (model.OutOfOffice.RESPONSIBLEPERSON == "")
                    model.OutOfOffice.RESPONSIBLEPERSONID = "";

                if (model.OutOfOffice.STATUS == "GD")
                {
                    model.OutOfOffice.STATUS = "GO";
                    UpdateOutOfOffice(model);
                }
                else
                {
                    model.OutOfOffice.STATUS = "GO";
                    SaveOutOfOffice(model);
                }
                
                
                msg.strMessage = "Get Out Successflly.";
                                
            }
            catch (Exception ex)
            {
                msg.strMessage = ex.Message;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NoCache]
        public JsonResult OutOfOfficeGetIn(OutOfOfficeModels model)
        {

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            model.OutOfOffice.GETINDATE = System.DateTime.Now;
            model.OutOfOffice.GETINTIME = DateTime.Now.ToString("h:mm tt");
            model.OutOfOffice.ISGETIN = true;
            model.OutOfOffice.STATUS = "GI";
            UpdateOutOfOffice(model);
            model.Message = Util.Messages.GetSuccessMessage("Get In Successfully.");
          
            model.IsNew = false;
            model.OutOfOffice.IsNew = false;

            ModelState.Clear();
            HttpContext.Response.Clear();
            Message msg = new Message();
            msg.strMessage = "Get In Successflly.";
            msg.strGetInTime = DateTime.Now.ToString("dd-MM-yyyy") + " At " + DateTime.Now.ToString("h:mm tt");
          
            return Json(msg, JsonRequestBehavior.AllowGet);
            
        }

        
         [NoCache]
        public ActionResult OutOfOfficeDetailsData(string Id)
        {
            OutOfOffice searchObj = new OutOfOffice();
            OutOfOfficeModels model = new OutOfOfficeModels();
            searchObj.ID = long.Parse(Id);
            
            model = model.GetDetailsData(searchObj, DateTime.MinValue, DateTime.MaxValue, 1, 1000);
            model.IsNew = false;
            model.OutOfOffice.IsNew = false;

            if (model.OutOfOffice.ISGETIN == false)
            {
                model.OutOfOffice.STRGETINDATE = DateTime.Now.ToString("dd-MM-yyyy");
                model.OutOfOffice.GETINDATE = DateTime.Now;
                model.OutOfOffice.GETINTIME = DateTime.Now.ToShortTimeString();
            }
            else
            {
                model.OutOfOffice.STRGETINDATE = model.OutOfOffice.GETINDATE.ToString("dd-MM-yyyy");
            }
            
            InitializeModel(model, true);
            model.GetApprovalComments(model);
            model.AdvanceDate = model.OutOfOffice.STRGETOUTDATE + " @ " + model.OutOfOffice.GETOUTTIME;
            return View("OutOfOfficeAdd", model);
        }

       
        [HttpGet]
        [NoCache]
        public JsonResult getEmployeeInformation(OutOfOfficeModels model)
        {
            Employee objEmp = new Employee();
            if (!string.IsNullOrEmpty(model.OutOfOffice.STREMPID))
            {
                objEmp = model.GetEmployeeInfo(model.OutOfOffice.STREMPID);

                model.OutOfOffice.Strdepartment = objEmp.strDepartment;
                model.OutOfOffice.Strdesignaiton = objEmp.strDesignation;
                
            }

            return Json(objEmp,JsonRequestBehavior.AllowGet);
        }

        private void InitializeModel(OutOfOfficeModels model,bool isPostBack)
        {

            if (!isPostBack)
            {
                model.OutOfOffice.STREMPID = LoginInfo.Current.strEmpID;
                model.OutOfOffice.EMPNAME = LoginInfo.Current.EmployeeName;
                model.OutOfOffice.STREXPGETINDATE = DateTime.Now.ToString("dd-MM-yyyy");
                model.OutOfOffice.EXPGETINTIME = DateTime.Now.ToShortTimeString();
                model.OutOfOffice.STRGETOUTDATE = DateTime.Now.ToString("dd-MM-yyyy");
                model.OutOfOffice.GETOUTTIME = DateTime.Now.ToShortTimeString();
            }
            Employee objEmp = new Employee();

            if (!string.IsNullOrEmpty(model.OutOfOffice.STREMPID))
            {
                objEmp = model.GetEmployeeInfo(model.OutOfOffice.STREMPID);

                model.OutOfOffice.Strdepartment= objEmp.strDepartment;
                model.OutOfOffice.Strdesignaiton = objEmp.strDesignation;
                model.EmployeeName = objEmp.strEmpName;
            }
            
        }

        private void SaveOutOfOffice(OutOfOfficeModels model)
        {

            OutOfOfficeModels.Save(model, LoginInfo.Current.strCompanyID, LoginInfo.Current.strEmpID);
        }

        private void UpdateOutOfOffice(OutOfOfficeModels model)
        {

            OutOfOfficeModels.Update(model, LoginInfo.Current.strCompanyID, LoginInfo.Current.strEmpID);
        }


        public ActionResult Advance()
        {
            return View();
        }

        public ActionResult AdvanceGetOut()
        {           
            return View();
        }

         [HttpGet]
        public JsonResult CheckDate(OutOfOfficeModels models)
        {       
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string strDt1 = models.OutOfOffice.STREXPGETINDATE+" "+models.OutOfOffice.EXPGETINTIME;
            DateTime dt1 = DateTime.Parse(strDt1);

            string strDt2 = models.OutOfOffice.STRGETOUTDATE + " " + models.OutOfOffice.GETOUTTIME;
            DateTime dt2 = DateTime.Parse(strDt2);

            TimeSpan ts = dt2 - dt1;

            Message msg = new Message();
            msg.strMessage="Invalid";
            return Json(msg,JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
         public JsonResult GetEmployeeWiseApprovalPath(string strEmpID)
         {
             string jsonString = "";
             EmployeeWiseOOAApprovalPathModels model = new EmployeeWiseOOAApprovalPathModels();
             int currentPageIndex = 0;

             model.strSortBy = "strPathName,strNodeName";
             model.strSortType = LMS.Util.DataShortBy.ASC;
             model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
             model.maximumRows = AppConstant.PageSize;
             model.PageNumber = currentPageIndex + 1;

             model.EmployeeWiseOOAApprovalPath = new EmployeeWiseOOAApprovalPath();
             EmployeeWiseOOAApprovalPath objSearch = model.EmployeeWiseOOAApprovalPath;

             //---[Search by approval author ID and Name]------------------------
             if (!string.IsNullOrEmpty(model.strSearchAuthorID))
             {
                 objSearch.strAuthorID = model.strSearchAuthorID.Trim();
             }
             else
             {
                 objSearch.strAuthorID = model.strSearchAuthorID;
             }

             if (!string.IsNullOrEmpty(model.strSearchAuthorName))
             {
                 objSearch.strAuthorName = model.strSearchAuthorName.Trim();
             }
             else
             {
                 objSearch.strAuthorName = model.strSearchAuthorName;
             }


             //---[Search by applicant ID and Name]------------------------
             if (!string.IsNullOrEmpty(strEmpID))
             {
                 objSearch.strEmpID = strEmpID;
             }
             else
             {
                 objSearch.strEmpID = model.strSearchID;
             }
                        

             objSearch.intPathID = model.intSearchPathID;
             objSearch.intNodeID = model.intSearchNodeID;
             objSearch.strDepartmentID = model.strSearchDepartmentId;
             objSearch.strDesignationID = model.strSearchDesignationId;
             objSearch.strLocationID = model.strSearchLocationId;
             model.GetEmployeeWiseOOAApprovalPathAll(objSearch);
             Message msg = new Message();
             if (model.LstEmployeeWiseOOAApprovalPath.Count > 0)
                 msg.strMessage = "1";
             else
                 msg.strMessage = "0";
             return Json(msg, JsonRequestBehavior.AllowGet);
         }

        [HttpGet]
        public JsonResult GetSearchLocation(string q, int limit)
        {
            List<SearchLocation> lst = new List<SearchLocation>();
            OutOfOfficeModels model = new OutOfOfficeModels();

            lst = model.GetSearchLocaton(q);
            return Json(lst, JsonRequestBehavior.AllowGet);
            
        }
    }
}
