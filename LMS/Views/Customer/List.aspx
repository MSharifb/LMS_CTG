<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.ViewModels.Shared.Grid<Customer, CustomerSearchForm>>" %>

<%@ Import Namespace="LMS.Web" %>
<%@ Import Namespace="LMS.Web.Models" %>
<%@ Import Namespace="LMS.Web.Controllers" %>
<%@ Import Namespace="LMS.Web.ViewModels.Shared" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~") %>Scripts/jquery-autocomplete/jquery.autocomplete.js"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~") %>Scripts/jquery.blockUI.js"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~") %>Scripts/grid.js"></script>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~")%>Scripts/jquery-autocomplete/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~")%>Content/css/grid.css" />
    <script type="text/javascript">
		function resolveUrl(relativeUrl) {
			var webAppRoot="<%= ResolveUrl("~") %>";
			var absoluteUrl;
			
			if (relativeUrl=="~")
				absoluteUrl=webAppRoot;
			else
				absoluteUrl=relativeUrl.replace("~/", webAppRoot);
			
			return absoluteUrl;
		}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Customers</h2>
    <%= Html.ActionLink("Add New", "Add") %>
    <% using (Html.BeginForm("List", "Customer", FormMethod.Post))
       { %>
    <div id="search-form">
        <%= Html.Hidden("SearchForm.IsAdvanced") %>
        <table>
            <tr>
                <td align="right">
                    <a href="#" id="advanced-link" <%= Model.SearchForm.IsAdvanced ? "class='hidden'" : ""%>>
                        advanced >></a> <a href="#" id="keyword-link" <%= Model.SearchForm.IsAdvanced ? "" : "class='hidden'"%>>
                            keyword >></a>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" id="advanced-search" <%= Model.SearchForm.IsAdvanced ? "" : "class='hidden'"%>>
                        <tr>
                            <td>
                                First Name<br />
                                <%= Html.TextBox("SearchForm.FirstName", null, new { @style = "width: 100px", @maxlength = "20" })%>
                            </td>
                            <td>
                                Last Name<br />
                                <%= Html.TextBox("SearchForm.LastName", null, new { @style = "width: 100px", @maxlength = "20" })%>
                            </td>
                            <td>
                                Phone<br />
                                <%= Html.TextBox("SearchForm.Phone", null, new { @style = "width: 100px", @maxlength = "20" })%>
                            </td>
                            <td>
                                Email<br />
                                <%= Html.TextBox("SearchForm.Email", null, new { @style = "width: 150px", @maxlength = "40" })%>
                            </td>
                            <td>
                                Date of Last order<br />
                                <table id="date-of-last-order-range">
                                    <tr>
                                        <td>
                                            <%= Html.TextBox("SearchForm.FromDateOfLastOrder", StringFormatter.FormatInputDate(Model.SearchForm.ToDateOfLastOrder), new { @style = "width: 100px", @maxlength = "15", @class = "with-date-picker" })%>
                                            <%= Html.ValidationMessage("SearchForm.FromDateOfLastOrder", "Invalid date.")%>
                                        </td>
                                        <td>
                                            to
                                        </td>
                                        <td>
                                            <%= Html.TextBox("SearchForm.ToDateOfLastOrder", StringFormatter.FormatInputDate(Model.SearchForm.FromDateOfLastOrder), new { @style = "width: 100px", @maxlength = "15", @class = "with-date-picker" })%>
                                            <%= Html.ValidationMessage("SearchForm.ToDateOfLastOrder", "Invalid date.")%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="right">
                                <input type="submit" id="advanced-search-submit" value="Search" />
                            </td>
                        </tr>
                    </table>
                    <table id="keyword-search" <%= Model.SearchForm.IsAdvanced ? "class='hidden'" : ""%>>
                        <tr>
                            <td>
                                <%= Html.TextBox("SearchForm.Keyword", null, new { @style = "width: 100px" , @maxlength ="20"}) %>
                            </td>
                            <td>
                                <input type="submit" id="keyword-search-submit" value="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- search-form -->
    <div style="clear: both">
    </div>
    <div id="grid">
        <% Html.RenderPartial("_Grid", Model); %>
    </div>
    <!-- grid -->
    <% } %>
</asp:Content>
