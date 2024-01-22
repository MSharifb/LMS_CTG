<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="dvConveyanceReport">
        <% Html.RenderPartial("ConveyanceReportDetails"); %>
    </div>
    
</asp:Content>
