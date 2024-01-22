<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveTypeDetails">
        <% Html.RenderPartial("LeaveYearTypeDetails"); %>
    </div>
</asp:Content>
