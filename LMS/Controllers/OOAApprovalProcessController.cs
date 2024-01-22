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
using LMS.UserMgtService;

namespace LMS.Web.Controllers
{
    public class OOAApprovalProcessController : Controller
    {
        //
        // GET: /OOAApprovalProcess/

        public ActionResult Index(int? page)
        {
            string strAuthorID = "";
            string Id = ""; 

            if (Request["strId"] != null)
                strAuthorID = Request["strId"].ToString();

            if (Request["id"] != null)
                Id = Request["id"].ToString();

            OutOfOfficeModels model = new OutOfOfficeModels();

            if (Request["fromMail"] != null && Request["fromMail"].ToString().ToLower() == "true")
            { SetLoginInfo(strAuthorID); }

            OutOfOffice searchObj = new OutOfOffice();
            
            OutOfOfficeLocaton obj = new OutOfOfficeLocaton();
            obj.OUTOFOFFICEID = long.Parse(Id);
            searchObj.ID = long.Parse(Id);
            model = OOAApprovalProcessModels.GetDetailsData(searchObj, LoginInfo.Current.strEmpID, DateTime.MinValue, DateTime.MaxValue, -1, -1);
            model.GetAuthorEmployeeWise(LMS.Web.LoginInfo.Current.strEmpID, model.OutOfOffice.STREMPID);
            InitializeModel(model, true);
            model.AuthorTypeID = model.GetAuthorTypeID(LoginInfo.Current.strEmpID, model.OutOfOffice.STREMPID);
            model.GetApprovalComments(model);

            return View("Details", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult PermissionNVarify(int? page)
        {
            OutOfOfficeModels model = SetPaging(page);
            ModelState.Clear();
            HttpContext.Response.Clear();
            return View(model);
        }


        [HttpPost]
        [NoCache]
        public ActionResult search(int? page, OutOfOfficeModels model)
        {

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
           
            OutOfOffice searchObj = new OutOfOffice();
            if (model.EmployeeName != "" && model.EmployeeName != null)
                searchObj.STREMPID = model.StrEmpID;
            else
                model.StrEmpID = "";

            searchObj.PURPOSE = model.Purpose;
            model.LstOutOfOffice = model.getDataForPermissionNVerify(LoginInfo.Current.strEmpID,model.StrEmpID, model.FromDate,model.startRowIndex,model.maximumRows);
            model.LstOutOfOfficePaging = model.LstOutOfOffice.ToPagedList(currentPageIndex, AppConstant.PageSize10);
       
            InitializeModel(model, true);

            ModelState.Clear();
            HttpContext.Response.Clear();
            model.Purpose = searchObj.PURPOSE;
            return View("PermissionNVarify", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult Details(string Id)
        {
            OutOfOffice searchObj = new OutOfOffice();
            OutOfOfficeModels model = new OutOfOfficeModels();
            OutOfOfficeLocaton obj = new OutOfOfficeLocaton();
            obj.OUTOFOFFICEID = long.Parse(Id);
            searchObj.ID = long.Parse(Id);
            model = OOAApprovalProcessModels.GetDetailsData(searchObj,LoginInfo.Current.strEmpID, DateTime.MinValue, DateTime.MaxValue, -1, -1);
            model.GetAuthorEmployeeWise(LMS.Web.LoginInfo.Current.strEmpID,model.OutOfOffice.STREMPID);
            InitializeModel(model, true);
            model.AuthorTypeID= model.GetAuthorTypeID(LoginInfo.Current.strEmpID, model.OutOfOffice.STREMPID);
            model.GetApprovalComments(model);

            return View( model);
        }

        public ActionResult OutOfOfficeDetails()
        {
            return View();
        }

        private OutOfOfficeModels SetPaging(int? page)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            OutOfOfficeModels model = new OutOfOfficeModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;
            model.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
            OutOfOffice searchObj = new OutOfOffice();
            model.LstOutOfOffice = model.getDataForPermissionNVerify(LoginInfo.Current.strEmpID,model.StrEmpID, model.FromDate,model.startRowIndex,model.maximumRows);
            model.LstOutOfOfficePaging = model.LstOutOfOffice.ToPagedList(currentPageIndex, AppConstant.PageSize10);

       
            return model;

        }

        private void InitializeModel(OutOfOfficeModels model, bool isPostBack)
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
            LMSEntity.Employee objEmp = new LMSEntity.Employee();

            if (!string.IsNullOrEmpty(model.OutOfOffice.STREMPID))
            {
                objEmp = model.GetEmployeeInfo(model.OutOfOffice.STREMPID);

                model.OutOfOffice.Strdepartment = objEmp.strDepartment;
                model.OutOfOffice.Strdesignaiton = objEmp.strDesignation;
                model.EmployeeName = objEmp.strEmpName;
            }

        }

        [HttpPost]
        [NoCache]
        public ActionResult AddNode(OutOfOfficeModels model)
        {
            model.IsNew = model.OutOfOffice.IsNew;
            model.LstOutOfOfficeLocation.Add(new OutOfOfficeLocaton());
            return PartialView("OutOfOfficeDetails", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult DeleteNode(OutOfOfficeModels model, string Id, FormCollection fc)
        {
            if (model.LstOutOfOfficeLocation.Count > 1)
                model.LstOutOfOfficeLocation.RemoveAt(int.Parse(Id));

            model.IsNew = model.OutOfOffice.IsNew;

            return PartialView("OutOfOfficeDetails", model);
        }


        [HttpGet]
        [NoCache]
        public JsonResult Permission(OutOfOfficeModels model)
        {
            model.OutOfOffice.PERMITTEDBY = LoginInfo.Current.strEmpID;        
            UpdateOutOfOffice(model);
            OOAApprovalProcessModels.Permission(model.OutOfOffice.ID, LoginInfo.Current.strEmpID);
            ViewData["vdMsg"] = "Permitted Successfully.";
            model.Message = "Permitted Successfully.";

            if (model.EditPermission)
                SaveApproverComments(model, "I");

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [NoCache]
        public JsonResult Verify(OutOfOfficeModels model)
        {
            int AuthorTypeID = 0;
            model.OutOfOffice.VERIFIEDBY = LoginInfo.Current.strEmpID;
            UpdateOutOfOffice(model);
          

            if (model.EditPermission)
                SaveApproverComments(model, "U");

            OOAApprovalProcessModels.Verify(model.OutOfOffice.ID, LoginInfo.Current.strEmpID);
            model.Message = "Verified Successfully.";


            AuthorTypeID = model.RecommendAUthorTypeGet(int.Parse(model.OutOfOffice.ID.ToString()));
            Common.SendEmailOutofOffice(model.OutOfOffice, 1,AuthorTypeID,false);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NoCache]
        public JsonResult Approve(OutOfOfficeModels model)
        {
            int AuthorTypeID = 0;
            model.OutOfOffice.APPROVEDBY = LoginInfo.Current.strEmpID;
            UpdateOutOfOffice(model);

            AuthorTypeID = model.RecommendAUthorTypeGet(int.Parse(model.OutOfOffice.ID.ToString()));

            if (model.EditPermission)
                SaveApproverComments(model, "I");
            
            OOAApprovalProcessModels.Approve(model.OutOfOffice.ID, LoginInfo.Current.strEmpID);
            model.Message = "Approved Successfully.";

            Common.SendEmailOutofOffice(model.OutOfOffice, 1, AuthorTypeID, true);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NoCache]
        public JsonResult Recommend(OutOfOfficeModels model)
        {
            int AuthorTypeID = 0;
            model.OutOfOffice.APPROVEDBY = LoginInfo.Current.strEmpID;
            UpdateOutOfOffice(model);
            
           
            if (model.EditPermission)
                SaveApproverComments(model, "I");

            OOAApprovalProcessModels.Recommend(model.OutOfOffice.ID, LoginInfo.Current.strEmpID);

            model.Message = "Recommend Successfully.";

            AuthorTypeID = model.RecommendAUthorTypeGet(int.Parse(model.OutOfOffice.ID.ToString()));

            Common.SendEmailOutofOffice(model.OutOfOffice, 1, AuthorTypeID,false);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NoCache]
        public JsonResult PermittedAndVerified(OutOfOfficeModels model)
        {
            int AuthorTypeID = 0;
            model.OutOfOffice.APPROVEDBY = LoginInfo.Current.strEmpID;
            model.OutOfOffice.VERIFIEDBY = LoginInfo.Current.strEmpID;

            UpdateOutOfOffice(model);

           
            
            if (model.EditPermission)            
                SaveApproverComments(model, "I");

            OOAApprovalProcessModels.Permission(model.OutOfOffice.ID, LoginInfo.Current.strEmpID);
            OOAApprovalProcessModels.Verify(model.OutOfOffice.ID, LoginInfo.Current.strEmpID);

            model.Message = "Approved and Verified Successfully.";


            AuthorTypeID = model.RecommendAUthorTypeGet(int.Parse(model.OutOfOffice.ID.ToString()));
            Common.SendEmailOutofOffice(model.OutOfOffice, 1, AuthorTypeID,false);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [NoCache]
        public JsonResult Reverify(OutOfOfficeModels model)
        {
          
            model.OutOfOffice.APPROVEDBY = LoginInfo.Current.strEmpID;
            model.OutOfOffice.VERIFIEDBY = LoginInfo.Current.strEmpID;

            UpdateOutOfOffice(model);

           

            if (model.EditPermission)
                SaveApproverComments(model, "I");

            OOAApprovalProcessModels.Reverify(model.OutOfOffice.ID, LoginInfo.Current.strEmpID);

            model.Message = "Successfully submitted for reverify.";

            Common.SendEmailOutofOffice(model.OutOfOffice, 1, 5, false);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        private void SaveOutOfOffice(OutOfOfficeModels model)
        {

            OutOfOfficeModels.Save(model, LoginInfo.Current.strCompanyID, LoginInfo.Current.strEmpID);
        }

        private void UpdateOutOfOffice(OutOfOfficeModels model)
        {
            if (model.EditPermission && model.OutOfOffice.ISGETIN == false)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                string getoutdate = model.OutOfOffice.STRGETOUTDATE + " " + model.OutOfOffice.GETOUTTIME;
                string expGetInDate = model.OutOfOffice.STREXPGETINDATE + " " + model.OutOfOffice.EXPGETINTIME;

                model.OutOfOffice.EXPGETINDATE = DateTime.Parse(expGetInDate);
                model.OutOfOffice.GETOUTDATE = DateTime.Parse(getoutdate);                
                
            }

            OutOfOfficeModels.Update(model, LoginInfo.Current.strCompanyID, LoginInfo.Current.strEmpID);

           
        }


        private void SaveApproverComments(OutOfOfficeModels model,string strMode)
        {
            OOAApprovalComments obj = new OOAApprovalComments();

            obj.INTAPPROVERTYPEID = model.AuthorTypeID;
            obj.INTFLOWPATHID = model.PathID;
            obj.INTOUTOFOFFICEID = model.OutOfOffice.ID;
            obj.STRAPPROVERID = LoginInfo.Current.strEmpID;
            obj.STRCOMMENTS = model.ApproverComment;
            obj.STRCOMPANYID = LoginInfo.Current.strCompanyID;
            obj.STREUSERID = LoginInfo.Current.strEmpID;
            obj.STREUSERID = LoginInfo.Current.strEmpID;

            if (strMode == "I")
                model.SaveApproverComment(obj);
            else
                model.UpdateApproverComment(obj);
        }
        
        
        [NoCache]
        private void SetLoginInfo(string strEmpId)
        {
            string strCompanyId = "";
            LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
            
            LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(strEmpId, strCompanyId, MyAppSession.LoggedInZoneId);

            if (loginDetails != null)
            {
                LoginInfo Current = new LoginInfo();
                try
                {
                    Current.strCompanyID = loginDetails.strCompanyID;
                    Current.strEmpID = loginDetails.strEmpID;

                    Current.EmployeeName = loginDetails.strEmpName;
                    Current.Archive = new System.Collections.Generic.List<string>();


                    User user = UserMgtAgent.GetUserByLoginId(loginDetails.strEmpID);
                    Current.UserId = user.UserId;// Convert.ToInt32(loginDetails.strEmpID);
                    Current.LoggedZoneId = user.ZoneId;
                    Current.ZoneName = loginDetails.LoggedInZoneName;

                    Current.strDesignationID = loginDetails.strDesignationID;
                    Current.strDesignation = loginDetails.strDesignation;
                    Current.strDepartmentID = loginDetails.strDepartmentID;
                    Current.fltOfficeTime = (float)loginDetails.fltDuration;
                    Current.intLeaveYearID = loginDetails.intLeaveYearID;
                    Current.EmailAddress = loginDetails.strEmail;
                    Current.strSupervisorId = loginDetails.strSupervisorID;

                    Current.intDestNodeID = loginDetails.intNodeID;
                    Current.strAllowHourlyLeave = loginDetails.strAllowHourlyLeave;
                    Current.StartOfficeTime = loginDetails.StartOfficeTime;
                    Current.EndOfficeTime = loginDetails.EndOfficeTime;

                }
                catch (Exception ex)
                {

                }
                Session["LoginInfo"] = Current;

            }
        }

    }
}
