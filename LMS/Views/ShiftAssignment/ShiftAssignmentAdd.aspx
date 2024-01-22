<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.ShiftAssignmentModels>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divShiftAssignmentDetails">
        <% Html.RenderPartial("ShiftAssignmentDetails"); %>
    </div>

</asp:Content>
