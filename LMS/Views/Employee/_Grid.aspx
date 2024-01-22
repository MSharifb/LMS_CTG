<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LMS.Web.ViewModels.Shared.Grid<Employee, EmployeeSearchForm>>" %>

<%@ Import Namespace="LMS.Web" %>
<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="LMS.Web.Models" %>
<%@ Import Namespace="LMS.Web.Controllers" %>
<%= Html.Hidden("Pager.CurrentPage")%>
<%= Html.Hidden("Sorter.SortField")%>
<%= Html.Hidden("Sorter.SortDirection")%>
<%= Html.Hidden("GridAction") %>
<script type="text/javascript">

    function setData(id, name) {
        window.parent.setData(id, name);
    }

</script>
<% if (!ViewData.ModelState.IsValid)
   {
       //display nothing
   }
   else if (Model.IsEmpty)
   { %>
<div id="empty-message">
    <strong>There are no Employees that match specified criteria.</strong>
</div>
<% }
   else
   { %>
<div id="grid-header">
    <div id="row-stats">
        <%= Model.Pager.RowStats %>
    </div>
    <div id="pager-nav">
        <% if (Model.Pager.IsNavVisible)
           { %>
        <table border="0">
            <tr>
                <td>
                    <% if (Model.Pager.IsFirstPage)
                       { %>
                    <span class="disabled">
                        <%= Html.Encode("<<") %></span>
                    <% }
                       else
                       { %>
                    <%= Model.PageNavActionLink("<<", Model.Pager.FirstPage) %>
                    <% } %>
                </td>
                <td>
                    <% if (Model.Pager.IsFirstPage)
                       { %>
                    <span class="disabled">
                        <%= Html.Encode("<") %></span>
                    <% }
                       else
                       { %>
                    <%= Model.PageNavActionLink("<", Model.Pager.PreviousPage) %>
                    <% } %>
                </td>
                <td>
                    Page
                    <%= Model.Pager.CurrentPage %>
                    of
                    <%= Model.Pager.TotalPages %>
                </td>
                <td>
                    <% if (Model.Pager.IsLastPage)
                       { %>
                    <span class="disabled">
                        <%= Html.Encode(">") %></span>
                    <% }
                       else
                       { %>
                    <%= Model.PageNavActionLink(">", Model.Pager.NextPage) %>
                    <% } %>
                </td>
                <td>
                    <% if (Model.Pager.IsLastPage)
                       { %>
                    <span class="disabled">
                        <%= Html.Encode(">>") %></span>
                    <% }
                       else
                       { %>
                    <%= Model.PageNavActionLink(">>", Model.Pager.LastPage) %>
                    <% } %>
                </td>
            </tr>
        </table>
        <% } %>
    </div>
    <div style="clear: both">
    </div>
</div>
<!-- grid-header -->
<div id="grid-data">
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%= Model.SortActionLink("Initial", "strEmpInitial")%>
            </th>
            <th>
                <%= Model.SortActionLink("Name", "strEmpName")%>
            </th>
            <th>
                <%= Model.SortActionLink("Designation", "strDesignation")%>
            </th>
        </tr>
        <% 
           //Can't use Model.Data directly.  Doesn't pick up generic type.
           IList<LMSEntity.Employee> Employees = (IList<LMSEntity.Employee>)Model.Data;
           foreach (Employee item in Employees)
           { %>
        <tr>
            <td>
                <a href='#' class="gridEdit" onclick='javascript:return setData("<%=item.strSearchInitial%>","<%=item.strEmpName%>");'>
                    Select</a>
            </td>
            <td>
                <%= Html.Encode(item.strEmpInitial)%>
            </td>
            <td>
                <%= Html.Encode(item.strEmpName) %>
            </td>
            <td>
                <%= Html.Encode(item.strDesignation)%>
            </td>
        </tr>
        <% } %>
    </table>
</div>
<!-- data -->
<div id="grid-footer">
    <noscript>
        <input type="submit" id="refresh-button" value="refresh" /></noscript>
    <%= Html.DropDownList("Pager.PageSize", Model.PageSizeSelectList())%>
    rows per page
</div>
<% } %>
