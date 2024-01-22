<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LMS.Web" %>
<%@ Import Namespace="LMS.Web.Models" %>
<%@ Import Namespace="LMS.Web.Controllers" %>
<%@ Import Namespace="LMS.Web.ViewModels.Shared" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     
        <div id="divLeaveTypeDetails">
            <% Html.RenderPartial("OutOfOfficeDetails"); %>
        </div>
    

</asp:Content>

