<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.MyConveyanceModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <% Html.RenderPartial("ConveyanceDetails"); %>

</asp:Content>
