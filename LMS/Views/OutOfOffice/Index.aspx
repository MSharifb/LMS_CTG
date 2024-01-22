<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LMS.Web" %>
<%@ Import Namespace="LMS.Web.Models" %>
<%@ Import Namespace="LMS.Web.Controllers" %>
<%@ Import Namespace="LMS.Web.ViewModels.Shared" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
      $(document).ready(function () {

          // $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 735, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
         

      });
</script>

 <div id="divDataList">
    
   
     <%-- <div id="divLeaveTypeDetails">
        <%--<% Html.RenderPartial("OutOfOfficeDetails"); %>--%>
    
</div>
</asp:Content>


