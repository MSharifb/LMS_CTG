<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.HolidayWeekendRuleAssignModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divHolidayWeekendRuleAssignDetails">
        <% Html.RenderPartial("HolidayWeekendRuleAssignDetails"); %>
    </div>
</asp:Content>
