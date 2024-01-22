<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.EmployeeModels>" %>
<%@ Import Namespace="MvcPaging" %>


<div id="divSearchCategory">
    <% Html.RenderPartial("Search"); %>    
</div>

<div id="dvSearch">
    <% Html.RenderPartial("SearchEmployee"); %>
</div>

