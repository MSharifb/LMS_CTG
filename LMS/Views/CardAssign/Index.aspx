<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.CardAssignModels>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        $(document).ready(function () {

            setTitle("Card Assign");

            $("#CardAssign").click();

        });
    </script>
    <div id="divDataList">
    </div>
</asp:Content>
