using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcPaging
{
    public class Pager
    {
        private ViewContext viewContext;
        private readonly int pageSize;
        private readonly int currentPage;
        private readonly int totalItemCount;
        private readonly RouteValueDictionary linkWithoutPageValuesDictionary;

        public Pager(ViewContext viewContext, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary)
        {
            this.viewContext = viewContext;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.linkWithoutPageValuesDictionary = valuesDictionary;
        }

        public string RenderHtml(string formName, string url, string divName)
        {
            int pageCount = (int)Math.Ceiling(this.totalItemCount / (double)this.pageSize);
            int nrOfPagesToDisplay = 10;

            var sb = new StringBuilder();

            int start = 1;
            int end = pageCount;


            if (1 == this.currentPage || start == end)
            {
                sb.Append("<span class=\"pager-item disabled\">First</span>");
            }
            else
            {
                sb.Append(GeneratePageLink("First", 1, formName, url, divName));
            }


            // Previous
            if (this.currentPage > 1)
            {
                sb.Append(GeneratePageLink("Prev", this.currentPage - 1, formName, url, divName));
            }
            else
            {
                sb.Append("<span class=\"pager-item disabled\">Prev</span>");
            }


            if (pageCount > nrOfPagesToDisplay)
            {
                int middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                int below = (this.currentPage - middle);
                int above = (this.currentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append(GeneratePageLink("1", 1, formName, url, divName));
                sb.Append(GeneratePageLink("2", 2, formName, url, divName));
                sb.Append("...");
            }
            for (int i = start; i <= end; i++)
            {
                if (i == this.currentPage)
                {
                    sb.AppendFormat("<span class=\"pager-item current\">{0}</span>", i);
                }
                else
                {
                    sb.Append(GeneratePageLink(i.ToString(), i, formName, url, divName));
                }
            }
            if (end < (pageCount - 3))
            {
                sb.Append("...");
                sb.Append(GeneratePageLink((pageCount - 1).ToString(), pageCount - 1, formName, url, divName));
                sb.Append(GeneratePageLink(pageCount.ToString(), pageCount, formName, url, divName));
            }

            // Next
            if (this.currentPage < pageCount)
            {
                sb.Append(GeneratePageLink("Next", (this.currentPage + 1), formName, url, divName));
            }
            else
            {
                sb.Append("<span class=\"pager-item disabled\">Next</span>");
            }


            if (end == this.currentPage)
            {
                sb.Append("<span class=\"pager-item disabled\">Last</span>");
            }
            else
            {
                sb.Append(GeneratePageLink("Last", pageCount, formName, url, divName));
            }



            return sb.ToString();
        }

        private string GeneratePageLink(string linkText, int pageNumber, string formName, string url, string divName)
        {
            var pageLinkValueDictionary = new RouteValueDictionary(this.linkWithoutPageValuesDictionary);
            pageLinkValueDictionary.Add("page", pageNumber);

            var virtualPathData = RouteTable.Routes.GetVirtualPath(this.viewContext.RequestContext, pageLinkValueDictionary);

            if (virtualPathData != null)
            {
                if (url.Length > 0)
                {
                    url = url + "?page=" + pageNumber.ToString();
                    string linkFormat = "<a onclick=\"return GetAjaxCall('" + formName + "','" + url + "','" + divName + "');\" href='#'>" + linkText.ToString() + "</a>";

                    return linkFormat;
                }
                else
                {
                    string linkFormat = "<a href=\"{0}\">{1}</a>";
                    return String.Format(linkFormat, virtualPathData.VirtualPath, linkText);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
