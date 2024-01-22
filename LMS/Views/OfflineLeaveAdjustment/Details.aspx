<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveApplicationModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divLeaveAdjustmentDetails">
        <%if (Model.LeaveApplication.intAppFlowID > 0)
          { %>
            <% Html.RenderPartial("OfflineLeaveAdjustmentDetailsPreview"); %>
          <% }
          else
          {%>
            <% Html.RenderPartial("OfflineLeaveAdjustmentDetails"); %>
        <%} %> 
    </div>

</asp:Content>
