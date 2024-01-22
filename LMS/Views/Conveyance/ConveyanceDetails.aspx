<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.ConveyanceModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divApprovalPathDetails">
        <% Html.RenderPartial("Details"); %>
   </div>
</asp:Content>
