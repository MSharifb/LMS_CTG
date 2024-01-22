<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.CommonConfigModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div id="divCommonConfigAddDetails">
        <% Html.RenderPartial("CommonConfigAddDetails"); %>
    </div>

</asp:Content>


