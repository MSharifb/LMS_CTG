<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveTypeModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveTypeDetails">
        <% Html.RenderPartial("LeaveTypeDetails"); %>
    </div>
</asp:Content>
