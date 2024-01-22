<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveYearMappingModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveYearMappingDetails">
        <% Html.RenderPartial("LeaveYearMappingDetails"); %>
    </div>
</asp:Content>
