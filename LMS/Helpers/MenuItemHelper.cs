using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Helpers
{
    /// <summary>
    /// This helper method renders a link within an HTML LI tag.
    /// A class="selected" attribute is added to the tag when
    /// the link being rendered corresponds to the current
    /// controller and action.
    /// 
    /// This helper method is used in the Site.Master View 
    /// Master Page to display the website menu.
    /// </summary>
    public static class MenuItemHelper
    {
        public static string MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName, System.Web.HttpContext context, int menuId, int parentMenuId, bool isChild, string menuName)
        {
            return MenuItem(helper, linkText, actionName, controllerName, null, null, context, menuId, parentMenuId, isChild, menuName);
        }

        public static string MenuItem(this HtmlHelper helper, string linkText, string actionName, object routeValues, System.Web.HttpContext context)
        {
            return MenuItem(helper, linkText, actionName, null, routeValues, null, context, 0, 0, false, "");
        }

        public static string MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, System.Web.HttpContext context, int menuId, int parentMenuId, bool isChild, string menuName)
        {
            string currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            string currentActionName = (string)helper.ViewContext.RouteData.Values["action"];
            string lnk = "";
            var builder = new TagBuilder("li");

            // Add selected class           
            if (IsSelectControler(context.Request.Url.ToString(), controllerName))
            {
                builder.AddCssClass("selected");

                if (!isChild)
                {
                    context.Session["pmenuId"] = menuId;
                }
            }
            else
            {
                if (!isChild)
                {
                    if (context.Session["pmenuName"] != null)
                    {
                        if (string.Compare(context.Session["pmenuName"].ToString(), controllerName) == 0)
                        {
                            //builder.AddCssClass("selected");
                            context.Session["firstUrl"] = controllerName + "/Index";
                            //context.Session["firstUrl"] = "LMS/"+ controllerName + "/Index";
                        }
                    }
                }
            }

            if (isChild)
            {
                builder.MergeAttribute("Id", "liId" + menuId.ToString());
                // Add link
                string script = "return PopulateData('/" + controllerName + "/" + actionName + "','liId" + menuId.ToString() + "','pliId" + parentMenuId.ToString() + "','cmenuname=" + menuName + "');return false;";

                string ctrl = controllerName;
                string[] cNames = controllerName.Split('/');
                if (cNames.Length > 0)
                {
                    ctrl = cNames[1].Trim();
                }

             //   builder.InnerHtml = helper.ActionLink(linkText, actionName, ctrl, routeValues, new { onclick = script, id = menuName }).ToString();
                lnk = helper.ActionLink(linkText, actionName, ctrl, routeValues, new { onclick = script, id = menuName }).ToString();
            }
            else
            {
                builder.MergeAttribute("Id", "pliId" + menuId.ToString());
                builder.InnerHtml = helper.ActionLink(linkText, actionName, controllerName, routeValues, new { id = "hId" + menuId.ToString() }).ToString();
                lnk = helper.ActionLink(linkText, actionName, controllerName, routeValues, new { id = "hId" + menuId.ToString() }).ToString();
            }

            // Render Tag Builder
           // return builder.ToString(TagRenderMode.Normal);

           // return helper.ActionLink(linkText, actionName, controllerName, routeValues, new { id = "hId" + menuId.ToString() }).ToString();
            return lnk;
        }

        public static bool IsSelectControler(string url, string controllerName)
        {
            string[] strContent = url.Split("/".ToCharArray());
            foreach (string strItem in strContent)
            {
                if (strItem.Trim() == controllerName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

    }
}
