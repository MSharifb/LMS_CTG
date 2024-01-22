<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.SetOverTimeModels>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divSetOverTimeDetails">
        <% Html.RenderPartial("SetOverTimeDetails"); %>
    </div>

</asp:Content>
