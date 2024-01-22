using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Web.Security;

namespace LMS.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,  
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Default",                                              // Route name
            //    "{controller}/{action}/{id}",                           // URL with parameters
            //    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            //);

            routes.MapRoute(
        "Default", // Route name
        "{controller}/{action}/{id}", // URL with parameters
        new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
        new[] { AppConstant.ProjectName + ".Controllers" }

);

        }

        protected void Session_Start()
        {
            Session.Timeout = AppConstant.SessionTimeout;//4999;
        }
        protected void Session_End()
        {
            // RedirectToAction("Index", "Home");
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            TVL.DB.Utility.strDBConnectionString = AppConstant.ConnectionString;    // ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            TVL.DB.Utility.strDBProvider = AppConstant.Provider;                    //ConfigurationManager.ConnectionStrings["providerName"].ToString();
            TVL.DB.Utility.intDBCommandTimeout = AppConstant.CommandTimeout;        //ConfigurationManager.ConnectionStrings["CommandTimeout"].ToString();

        }


        protected void Application_AuthenticateRequest()
        {
            //string url = HttpContext.Current.Request.RawUrl;

            //if (!url.Contains("/Account/LogOn") && !url.Contains(".css") && !url.Contains(".jpg") && !url.Contains(".gif") && !url.Contains(".png") && !url.Contains(".js"))
            //{
            //    if (HttpContext.Current.User != null)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        Response.Redirect("/Setup/Index");
            //    }
            //}


            //HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            //if (cookie == null)
            //{
            //    return;
            //}
        }


    }
}