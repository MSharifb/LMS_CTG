<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.MISCModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divMISCDetails">
        <% Html.RenderPartial("MISCDetails"); %>
   </div>

</asp:Content>
