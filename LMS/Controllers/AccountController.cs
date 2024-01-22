using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using LMS.UserMgtService;
using LMS.Web.Models;
using System.Globalization;
using LMS.Util;
using System.Configuration;

namespace LMS.Web.Controllers
{
    [NoCache]
    [HandleError]
    public class AccountController : Controller
    {

        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        #region Logon/logout

        //--------------------------------------------------------------------------------------------
        /* This method will closed when IWM login is applicable */
        /* Development */
        #region Development
        [NoCache]
        public ActionResult LogOn()
        {
            LogOnModel model = new LogOnModel();

            LookupRememberMe(model);
            return View(model);
        }

        ////this action will be closed once completion of LMS development
        ////Development   

        [NoCache]
        public ActionResult LogOff()
        {
            Session.Abandon();
            Session.Clear();
            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuth.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion
        /* End Development */
        //--------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------
        /* The following LogOn method is created to log in directly to LMS system from MPA, with the user and passward that is used in IWM login. */
        /* Deployment */
        #region Deployment
        //[NoCache]
        //public ActionResult LogOn(string uid, string pwd, Int32 ZoneId)
        //{
        //    LogOnModel model = new LogOnModel();
        //    model.UserName = uid;
        //    model.Password = pwd;
        //    model.ZoneId = ZoneId;

        //    LookupRememberMe(model);
        //    return AuthenticateUser(model);
        //}


        //This section will open when integrate with MPA dashboard
        //The following LogOff method is created to log out directly from LMS system to IWM log in page.
        //Deployment

        //[NoCache]
        //public ActionResult LogOff()
        //{
        //    string url = "";
        //    string lmsHostServerName = ConfigurationManager.AppSettings["HostServerName"].ToString();
        //    string MainProjectName = ConfigurationManager.AppSettings["MainProjectName"].ToString();

        //    Session.Abandon();
        //    Session.Clear();
        //    HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
        //    FormsAuth.SignOut();

        //    url = "http://" + lmsHostServerName + "/" + MainProjectName + "/Account/LogOff";
        //    return Redirect(url);
        //}
        #endregion
        /* End Deployment */
        //--------------------------------------------------------------------------------------------

        #endregion

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (model.IsFromCompany)
            {
                return AuthenticateUser(model);
            }
            else
            {
                if (ValidateLogOn(model.UserName, model.Password))
                {
                    ManageRememberMe(model);

                    return AuthenticateUser(model);
                }
            }
            return View(model);
        }


        private void LookupRememberMe(LogOnModel model)
        {
             HttpCookie cookieRememberMe = Request.Cookies["RememberMe"];
             if (cookieRememberMe != null && !string.IsNullOrEmpty(cookieRememberMe.Value))
             {
                 if (cookieRememberMe.Value.ToLower() == "true")
                 {
                     model.RememberMe = true;

                     HttpCookie cookieUserId = Request.Cookies["userId"];
                     HttpCookie cookiePass = Request.Cookies["Pass"];

                     if (cookieUserId != null && !string.IsNullOrEmpty(cookieUserId.Value))
                     {
                         model.UserName = cookieUserId.Value;
                     }

                     if (cookiePass != null && !string.IsNullOrEmpty(cookiePass.Value))
                     {
                         //model.Password.Attributes.Add("value", cookiePass.Value);
                         model.Password = cookiePass.Value;
                     }
                 }
             }
        }

        private void ManageRememberMe(LogOnModel model)
        {
            /* Set cookies for user id and password*/

            if (model.RememberMe == true)
            {
                int CookieExpireDay = 1;

                try
                {                    
                    CookieExpireDay = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CookieExpireDay"].ToString());
                }
                catch (Exception ex)
                {
                    
                }

                HttpCookie cookieUserId = new HttpCookie("userId", model.UserName);
                cookieUserId.Expires = DateTime.Now.AddDays(CookieExpireDay);
                HttpCookie cookiePass = new HttpCookie("Pass", model.Password);
                cookiePass.Expires = DateTime.Now.AddDays(CookieExpireDay);
                HttpCookie cookieRememberMe = new HttpCookie("RememberMe", "true");
                cookieRememberMe.Expires = DateTime.Now.AddDays(CookieExpireDay);

                
                HttpContext.Response.Cookies.Add(cookieUserId);
                HttpContext.Response.Cookies.Add(cookiePass);
                HttpContext.Response.Cookies.Add(cookieRememberMe);
            }
            else
            {
                /* Make the cookies obsolete */
                HttpCookie cookieUserId = new HttpCookie("userId");
                cookieUserId.Expires = DateTime.Now.AddDays(-1);
                HttpCookie cookiePass = new HttpCookie("Pass");
                cookiePass.Expires = DateTime.Now.AddDays(-1);
                HttpCookie cookieRememberMe = new HttpCookie("RememberMe");
                cookieRememberMe.Expires = DateTime.Now.AddDays(-1);

               // HttpContext context = HttpContext.Current;
                HttpContext.Response.Cookies.Add(cookieUserId);
                HttpContext.Response.Cookies.Add(cookiePass);
                HttpContext.Response.Cookies.Add(cookieRememberMe);
            }
        }
        private ActionResult AuthenticateUser(LogOnModel model)
        {

            //Get User Information
            User user = UserMgtAgent.GetUserByLoginId(model.UserName); // ok

            FormsAuth.SignIn(model.UserName, model.RememberMe);

            if (model.UserName.ContainsCaseInsensitive(AppConstant.SysInitializer))
            {
                return RedirectToAction("Index", "SystemInitialization");
            }
            else
            {
                LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();
                string strCompanyId = "";
                int loggedInZoneId = 0;
                if (model.ZoneId > 0)
                    loggedInZoneId = model.ZoneId;
                else
                    loggedInZoneId = user.ZoneId;

                MyAppSession.LoggedInZoneId = loggedInZoneId;

                LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(user.EmpId, strCompanyId, loggedInZoneId);

                if (loginDetails != null || model.UserName.ContainsCaseInsensitive(AppConstant.SysInitializer))
                {
                    LoginInfo Current = new LoginInfo();

                    try
                    {
                        Current.strCompanyID = loginDetails.strCompanyID;
                        Current.LoginName = user.LoginId;
                        Current.strPassword = model.Password ;

                        Current.strEmpID = loginDetails.strEmpID;

                        Current.EmployeeName = loginDetails.strEmpName;
                        Current.Archive = new System.Collections.Generic.List<string>();
                        Current.UserId = user.UserId;
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

                        // Added For BEPZA
                        Current.LoggedZoneId = loggedInZoneId;
                        Current.ZoneName = loginDetails.LoggedInZoneName;
                    }
                    catch (Exception ex)
                    {

                    }
                    Session["LoginInfo"] = Current;

                }
                else
                {
                    FormsAuth.SignOut();
                    return Redirect(model.ReturnUrl);
                }



                if (user.ChangePasswordAtFirstLogin == true)
                {
                    Session["IsFirstLogin"] = "Yes";
                    LoginInfo.Current.ShowMenus = false;
                    return RedirectToAction("ChangePassword", "Account");
                }
                else
                {
                    Session["IsFirstLogin"] = "No";
                    if (!String.IsNullOrEmpty(model.ReturnUrl) && !String.IsNullOrEmpty(model.ReturnUrl.Replace("Account", "")) && model.ReturnUrl.IndexOf("default") < 0 && model.ReturnUrl.IndexOf("Default") < 0 && model.ReturnUrl.IndexOf("Home") < 0)
                    {
                        if (string.IsNullOrEmpty(model.ReturnUrl) || model.ReturnUrl.IndexOf("/LMS") >= 0 || model.ReturnUrl.IndexOf("/lms") >= 0)
                        {
                            return RedirectToAction("Index", "MyLeave");
                        }
                        else
                        {
                            return Redirect(model.ReturnUrl);
                        }
                    }
                    else
                    {
                        //return RedirectToAction("Index", "MyLeave");
                        return RedirectToAction("Index", "Setup");
                    }
                }
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult LogOnWithCompany()
        {
            return RedirectToAction("Index", "Setup");
        }

       

        //The following DashboardHome method will redirect to the eHRmanager Dashboard.
        [NoCache]
        public ActionResult DashboardHome()
        {
            string userName = "";
            string userPass = "";

          
            userName = LMS.Web.LoginInfo.Current.LoginName.ToString();
           
            userPass = LMS.Web.LoginInfo.Current.strPassword.ToString();
          

            string url = "";
            string lmsHostServerName = ConfigurationManager.AppSettings["HostServerName"].ToString();
            string MainProjectName = ConfigurationManager.AppSettings["MainProjectName"].ToString();
            
            //string userName = model.UserName;
            //string userPass = model.Password;
            if ((userName != "" && userName != null) && (userPass != "" || userPass != null))
            {
                url = "http://" + lmsHostServerName + "/" + MainProjectName + "/Account/LogOnDashboard?uid=" + userName + "&pwd=" + Uri.EscapeDataString(userPass);
                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "MyLeave");
            }

        }

        public ActionResult Register()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (ValidateRegistration(userName, email, password, confirmPassword))
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(userName, password, email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuth.SignIn(userName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            ChangePasswordModel model = new ChangePasswordModel();
            model.OldPassword = "";
            return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!ValidateChangePassword(model.OldPassword, model.NewPassword, model.ConfirmPassword))
            {
                return View(model);
            }

            try
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("Model.OldPassword", "The current password is incorrect or the new password is invalid.");
                    return View(model);
                }
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                return View(model);
            }
        }

        public ActionResult ChangePasswordSuccess()
        {

            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Validation Methods

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }

            if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            ModelState.Clear();

            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a Login Id.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a Password.");
            }
            if (ModelState.IsValid)
            {
                if (!MembershipService.ValidateUser(userName, password))
                {
                    ModelState.AddModelError("_FORM", "The login Id or password provided is incorrect.");
                }
                else
                {
                    if (userName.ContainsCaseInsensitive(AppConstant.SysInitializer))
                    {
                    }
                    else
                    {
                        List<Menu> lstMenu = UserMgtAgent.GetMenus(userName, "LMS", "LMS");
                        if (lstMenu != null)
                        {
                            if (lstMenu.Where(c => c.IsAssignedMenu == true).ToList().Count <= 0)
                            {

                                ModelState.AddModelError("_FORM", "You have no access in this system.");
                            }
                        }
                    }
                }
            }
            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a Login Id.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (password == null || password.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        // The FormsAuthentication type is sealed and contains static members, so it is difficult to
        // unit test code that calls its members. The interface and helper class below demonstrate
        // how to create an abstract wrapper around such a type in order to make the AccountController
        // code unit testable.

        public interface IFormsAuthentication
        {
            void SignIn(string userName, bool createPersistentCookie);
            void SignOut();
        }

        public class FormsAuthenticationService : IFormsAuthentication
        {
            public void SignIn(string userName, bool createPersistentCookie)
            {
                FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
            }
            public void SignOut()
            {
                FormsAuthentication.SignOut();
            }
        }

        public interface IMembershipService
        {
            int MinPasswordLength { get; }

            bool ValidateUser(string userName, string password);
            MembershipCreateStatus CreateUser(string userName, string password, string email);
            bool ChangePassword(string userName, string oldPassword, string newPassword);
        }

        public class AccountMembershipService : IMembershipService
        {
            private CustomMembershipProvider _provider;

            public AccountMembershipService()
                : this(null)
            {
            }

            public AccountMembershipService(CustomMembershipProvider provider)
            {
                _provider = provider ?? new CustomMembershipProvider();
            }

            public int MinPasswordLength
            {
                get
                {
                    return _provider.MinRequiredPasswordLength;
                }
            }

            public bool ValidateUser(string userName, string password)
            {
                return _provider.ValidateUser(userName, password);
            }

            public MembershipCreateStatus CreateUser(string userName, string password, string email)
            {
                MembershipCreateStatus status;
                _provider.CreateUser(userName, password, email, null, null, true, null, out status);
                return status;
            }

            public bool ChangePassword(string userName, string oldPassword, string newPassword)
            {
                return new CustomMembershipProvider().ChangePassword(userName, oldPassword, newPassword);
            }
        }
    }
}
