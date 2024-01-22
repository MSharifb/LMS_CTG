<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.WeekendHolidayAsWorkingdayModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divWeekendHolidayAsWorkingdayDetails">
        <% Html.RenderPartial("WeekendHolidayAsWorkingdayDetails"); %>
    </div>
</asp:Content>
