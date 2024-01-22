<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.HRPolicyNameModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="dvHRPolicyNameDetails">
        <% Html.RenderPartial("HRPolicyNameDetails"); %>
    </div>

</asp:Content>
