<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.MiscellaneousVoucherModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <% Html.RenderPartial("VoucherDetail"); %>

</asp:Content>
