<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divApprovalFlowDetails">
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
