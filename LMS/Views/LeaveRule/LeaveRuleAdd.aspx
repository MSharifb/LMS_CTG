<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveRuleModels>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveRuleDetails">
        <% Html.RenderPartial("LeaveRuleDetails"); %>
    </div>

</asp:Content>
