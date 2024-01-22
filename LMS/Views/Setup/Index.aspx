<%@ Page Title="Setup" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("#LeaveYear").click();
           
        });
    
    </script>
    <div id="divDataList">
    </div>
</asp:Content>
