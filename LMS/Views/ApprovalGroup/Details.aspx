<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.ApprovalGroupModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveTypeDetails">
        <% Html.RenderPartial("ApprovalGroupDetails"); %>
    </div>
</asp:Content>
