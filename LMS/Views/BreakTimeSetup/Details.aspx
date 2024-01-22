<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.BreakTimeSetupModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divBreakTimeSetupDetails">
        <% Html.RenderPartial("BreakTimeSetupDetails"); %>
    </div>
</asp:Content>
