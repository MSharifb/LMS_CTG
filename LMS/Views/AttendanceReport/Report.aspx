<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.AttendanceReportModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        $(document).ready(function () {

            setTitle("Attendance Reports");

            $("#AttendanceReport").click();

        });
    </script>
    <div id="divDataList">
    </div>
</asp:Content>


<%--<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divAttendanceReport">
        <% Html.RenderPartial("AttendanceReport"); %>
    </div>
</asp:Content>--%>
