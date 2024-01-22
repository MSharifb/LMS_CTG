<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LMS.Web.ViewModels.Shared.Grid<Customer, CustomerSearchForm>>" %>

<%@ Import Namespace="LMS.Web" %>
<%@ Import Namespace="LMS.Web.Models" %>
<%@ Import Namespace="LMS.Web.Controllers" %>
<%= Html.Hidden("Pager.CurrentPage")%>
<%= Html.Hidden("Sorter.SortField")%>
<%= Html.Hidden("Sorter.SortDirection")%>
<%= Html.Hidden("GridAction") %>
<% if (!ViewData.ModelState.IsValid)
   {
       //display nothing
   }
   else if (Model.IsEmpty)
   { %>
<div id="empty-message">
    <strong>There are no customers that match specified criteria.</strong>
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
                <%= Model.SortActionLink("ID", "ID") %>
            </th>
            <th>
                <%= Model.SortActionLink("First Name", "FirstName")%>
            </th>
            <th>
                <%= Model.SortActionLink("Last Name", "LastName")%>
            </th>
            <th>
                <%= Model.SortActionLink("Phone", "Phone")%>
            </th>
            <th>
                <%= Model.SortActionLink("Email", "Email")%>
            </th>
            <th>
                <%= Model.SortActionLink("Orders Placed", "OrdersPlaced")%>
            </th>
            <th>
                <%= Model.SortActionLink("Last order on", "DateOfLastOrder")%>
            </th>
        </tr>
        <% 
           //Can't use Model.Data directly.  Doesn't pick up generic type.
           IList<Customer> customers = (IList<Customer>)Model.Data;
           foreach (Customer item in customers)
           { %>
        <tr onclick="onRowClick(<%= item.ID %>)">
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID}) %>
                |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.ID })%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(item.FirstName) %>
            </td>
            <td>
                <%= Html.Encode(item.LastName) %>
            </td>
            <td>
                <%= Html.Encode(StringFormatter.FormatPhone(item.Phone)) %>
            </td>
            <td>
                <%= Html.Encode(item.Email) %>
            </td>
            <td>
                <%= Html.Encode(item.OrdersPlaced) %>
            </td>
            <td>
                <%= Html.Encode(StringFormatter.FormatDate(item.DateOfLastOrder)) %>
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
