<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="user-dropdown dropdown pull-right">
    <a class="btn btn-link" href="#" onclick="javascript:toggle_fullscreen()"><i class="fa fa-expand">
    </i></a>
    <% if (Request.IsAuthenticated)
       { %>
    <button class="btn btn-link dropdown-toggle" type="button" id="user-menu" data-toggle="dropdown"
        aria-haspopup="true" aria-expanded="true">
        <% if (Page.User.Identity.Name.ToString().Contains(LMS.Web.AppConstant.SysInitializer))
           { %>
        <%= Html.Label(Page.User.Identity.Name) %>
        <% }
           else
           {%>
        <div class="pull-right">
            <%= Html.Encode(LMS.Web.LoginInfo.Current.EmployeeName + ", " + LMS.Web.LoginInfo.Current.strDesignation + ", " + LMS.Web.LoginInfo.Current.LoginName)%>
        </div>
        <div class="clear">
        </div>
        <div class="pull-right">
            <%= Html.Encode(LMS.Web.LoginInfo.Current.ZoneName)%>
            <span class="caret"></span>
        </div>
        <% }%>
    </button>
    <ul class="dropdown-menu dropdown-menu-right user-dropdown-menu" aria-labelledby="user-menu">
        <li>
            <%= Html.ActionLink("Dashboard", "DashboardHome","Account")%>
            <%-- <a href="/LMS/Setup">Dashboard</a>--%>
            <!-- @devs: fix this with dynamic URL -->
        </li>
        <li>
            <%= Html.ActionLink("Change Password", "ChangePassword", "Account") %></li>
        <li role="separator" class="divider"></li>
        <li>
            <%= Html.ActionLink("Log out", "LogOff", "Account", null, new { onclick = "return ClearCooke()" }) %>
        </li>
    </ul>
    <% }
       else
       { %>
    <span>&nbsp;</span>
    <% } %>
</div>
