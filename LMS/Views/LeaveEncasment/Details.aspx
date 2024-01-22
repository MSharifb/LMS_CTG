<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveEncasmentModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divLeaveEncasmentDetails">
        <% Html.RenderPartial("LeaveEncasmentDetails"); %>
    </div>

</asp:Content>
