<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MenuModels>" %>
<%@ Import Namespace="Helpers" %>

<ul class="submenu" id="">
    <% if (Model.MenuList != null)
       {
           int count = 0;
           var myMenu = (from menu in Model.MenuList
                         where menu.ParentMenuName.ToLower() == "myleave" && menu.MenuName.ToLower() == "searchapplication"
                         select menu).SingleOrDefault();
           if (myMenu != null)
           {
               Model.MenuList.Remove(myMenu);
               Model.MenuList.Insert(0, myMenu);
           }

           foreach (var item in Model.MenuList)
           {
               if (item.IsAssignedMenu || item.ParentMenuId == 52)
               {
                   if (item.PageUrl.IndexOf("http:") < 0)
                   {
                       string subDomain = item.PageUrl.Split(Convert.ToChar("/"))[1].ToString();
                       string controller = item.PageUrl.Split(Convert.ToChar("/"))[2].ToString();
                       string action = item.PageUrl.Split(Convert.ToChar("/"))[3].ToString();

                       string menuName = "";
                                            
    %>
    <li>
    <%= Html.MenuItem(item.MenuCaption, action, subDomain + @"/" + controller, Context, item.MenuId, item.ParentMenuId, true, menuName)%>
    </li>
    <% 
                   }
                   else
                   { %>
    <li><a href="<%=item.PageUrl %>">
        <%=item.MenuCaption%></a></li>
    <%} %>
    <% }
           } %>
    <%} %>
</ul>
<script type="text/javascript">
    $(function () {
        var url = window.location.href;

        var strAr = url.split("/");
        var str = strAr[strAr.length - 2].toLowerCase();
        var str2 = strAr[strAr.length - 1].toLowerCase();
       
       
        if (str == 'myleave' || str2 == 'myleave') {
     
            $(".submenu li:first a").click();
        }
        
         if (str == 'outofoffice' || str2 == 'outofoffice') {
     
            $(".submenu li:first a").click();
        }
         <% if(Request["fromMail"]=="true") { %>
                    $(".submenu").hide();
                    $("#divDataList").width("95%");
                <%} %> 
    });
</script>
