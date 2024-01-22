using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Helpers
{
    public static class ImageActionLinkHelper
    {

        public static string ImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string actionName, object routeValues, AjaxOptions ajaxOptions)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", altText);
            string link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions).ToString();
            return link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }

    }
}
