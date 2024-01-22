<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.EmployeeWiseOOAApprovalPathModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divEmployeeWiseApprovalPathDetails">
        <% Html.RenderPartial("EmployeeWiseApprovalPathDetails"); %>
    </div>
</asp:Content>
