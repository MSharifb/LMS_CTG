<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.AttRuleModels>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveRuleDetails">
        <% Html.RenderPartial("AttRuleDetails"); %>
    </div>

</asp:Content>
