<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.MISCModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   
   <img width="680" class="example1" height="430"  alt=""  src="<%= Url.Content("~/MISCAttachedFiles/"+ Model.FileName)%>" />
</asp:Content>
