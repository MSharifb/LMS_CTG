<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.CardAssignModels>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divCardAssignDetails">
        <% Html.RenderPartial("CardAssignDetails"); %>
    </div>

</asp:Content>
