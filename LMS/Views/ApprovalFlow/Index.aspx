<%@ Page Title="Leave Approval" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divDataList">
        <%if (Model.ShowPreview)
          { %>
        <% Html.RenderPartial("ApprovalFlowDetailsPreview"); %>
        <%}
          else
          { %>
        <% Html.RenderPartial("ApprovalFlowDetails"); %>
        <%} %>
    </div>
</asp:Content>
