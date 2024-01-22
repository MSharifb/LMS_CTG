<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MenuModels>" %>
<%@ Import Namespace="Helpers" %>
<% 
    if (Request["fromMail"] != null && Request["fromMail"].ToString().ToLower() == "true")
    {
        var item = (from obj in Model.MenuList
                    where obj.MenuName == "FromEmail"
                    select obj).SingleOrDefault();

        string subDomain = item.PageUrl.Split(Convert.ToChar("/"))[1].ToString();
        string controller = item.PageUrl.Split(Convert.ToChar("/"))[2].ToString();
        string action = item.PageUrl.Split(Convert.ToChar("/"))[3].ToString();

%>
    <li>
    <%= Html.MenuItem(item.MenuCaption, action, controller, Context, item.MenuId, item.ParentMenuId, false, item.MenuName)%>

    </li>
<%--             <%if (LMS.Web.LoginInfo.Current.ShowMenus)
              { %>
            <%= Html.Action("LeftMenu", "Menu")%>
            <%} %>--%>
<%
    }
    else
    {

        foreach (var item in Model.MenuList)
        {
            //if ((item.IsAssignedMenu || item.MenuId == 52) && item.ParentMenuId == -1)
            if ((item.IsAssignedMenu || item.MenuId == 52) && item.ParentMenuId == -1 && item.PageUrl.Contains('/'))
            {
                string subDomain = item.PageUrl.Split(Convert.ToChar("/"))[1].ToString();
                string controller = item.PageUrl.Split(Convert.ToChar("/"))[2].ToString();
                string action = item.PageUrl.Split(Convert.ToChar("/"))[3].ToString();
             
%>
        <li>
    <%= Html.MenuItem(item.MenuCaption, action, controller, Context, item.MenuId, item.ParentMenuId, false, item.MenuName)%>
        
        <%
         if (LMS.Web.LoginInfo.Current.ShowMenus)
              { %>
            <%= Html.Action("LeftMenu", "Menu", new { @pid = item.MenuId })%>
            <%} 
        %>
        </li>
<% }
           }
  
          

       }%>
