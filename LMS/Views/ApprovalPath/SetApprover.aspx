<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.ApprovalPathModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divSetApproverDetails">
        <% Html.RenderPartial("SetApproverDetails"); %>
    </div>
</asp:Content>
