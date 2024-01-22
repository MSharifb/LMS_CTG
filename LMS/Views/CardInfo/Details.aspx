<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.CardInfoModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divCardInfoDetails">
        <% Html.RenderPartial("CardInfoDetails"); %>
    </div>
</asp:Content>
