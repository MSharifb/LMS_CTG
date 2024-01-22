<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.JobTypeNameModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div id="dvJobTypeNameDetails">
        <% Html.RenderPartial("JobTypeNameDetails"); %>
    </div>

</asp:Content>
