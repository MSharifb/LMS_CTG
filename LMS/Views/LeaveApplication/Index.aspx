<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LeaveApplicationModels>" %>

<%@ Import Namespace="LMS.Web" %>
<%@ Import Namespace="LMS.Web.Models" %>
<%@ Import Namespace="LMS.Web.Controllers" %>
<%@ Import Namespace="LMS.Web.ViewModels.Shared" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        $(document).ready(function () {

            setTitle("Leave Type");

            $("#OnlineLeaveApplication").click();

        });
    
    </script>
    <div id="divDataList">
    </div>
</asp:Content>
