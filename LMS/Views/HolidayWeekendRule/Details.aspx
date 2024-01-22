<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.HolidayWeekendRuleModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divHolidayWeekendRuleDetails">
        <% Html.RenderPartial("HolidayWeekendRuleDetails"); %>
    </div>
</asp:Content>
