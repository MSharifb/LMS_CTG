<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveApplicationModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divLeaveApplicationDetails">
        <% Html.RenderPartial("LeaveApplicationDetails"); %>    
    </div>

</asp:Content>
