<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.ImportAttendRawDataModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divAttRuleDetails">
        <% Html.RenderPartial("ImportAttendRawDataDetails"); %>
    </div>
</asp:Content>
