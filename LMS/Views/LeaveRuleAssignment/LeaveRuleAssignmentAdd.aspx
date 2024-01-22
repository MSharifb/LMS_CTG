<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveRuleAssignmentModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveRuleAssignmentDetails">
        <% Html.RenderPartial("LeaveRuleAssignmentDetails"); %>
    </div>

</asp:Content>
