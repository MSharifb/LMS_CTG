<%@ Page Title="Administration" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#AlternateApprovalProcess").click();
        });
    
    </script>
    <div id="divDataList">
    </div>
</asp:Content>
