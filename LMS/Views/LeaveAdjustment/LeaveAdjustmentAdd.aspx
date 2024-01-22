<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveApplicationModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveAdjustmentDetails">
        <% Html.RenderPartial("LeaveAdjustmentDetails"); %>
    </div>
</asp:Content>
