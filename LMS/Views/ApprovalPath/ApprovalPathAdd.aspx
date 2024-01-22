<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.ApprovalPathModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divApprovalPathDetails">
        <% Html.RenderPartial("ApprovalPathDetails"); %>
    </div>
</asp:Content>
