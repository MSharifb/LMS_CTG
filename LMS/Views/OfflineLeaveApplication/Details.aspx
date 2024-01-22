<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveApplicationModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divOfflineLeaveApplicationDetails">
        <%if (Model.LeaveApplication.intAppFlowID > 0)
          { %>
            <% Html.RenderPartial("OfflineLeaveApplicationDetailsPreview"); %>
          <% }
          else
          {%>
            <% Html.RenderPartial("OfflineLeaveApplicationDetails"); %>
        <%} %>

    </div>
</asp:Content>
