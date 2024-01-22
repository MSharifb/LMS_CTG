<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.ViewModels.Shared.Grid<LMSEntity.Employee, EmployeeSearchForm>>" %>

<%@ Import Namespace="LMS.Web" %>
<%@ Import Namespace="LMS.Web.Models" %>
<%@ Import Namespace="LMS.Web.Controllers" %>
<%@ Import Namespace="LMS.Web.ViewModels.Shared" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <h2>
        Search Employee</h2>
    <% using (Html.BeginForm("List", "Employee", FormMethod.Post))
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
                <td colspan="2">
                    <table border="0" id="advanced-search" <%= Model.SearchForm.IsAdvanced ? "" : "class='hidden'"%>>
                        <tr>
                            <td>
                                ID<br />
                                <%= Html.TextBox("SearchForm.strEmpInitial", null, new { @style = "width: 100px", @maxlength = "50" })%>
                            </td>
                            <td>
                                Name<br />
                                <%= Html.TextBox("SearchForm.strEmpName", null, new { @style = "width: 100px", @maxlength = "50" })%>
                            </td>
                            <td>
                                Designation<br />
                                <%= Html.TextBox("SearchForm.strDesignation", null, new { @style = "width: 100px", @maxlength = "50" })%>
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
