<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(document).ready(function () {

        alert('run');

    });
</script>

    <div id="divOutOfOfficeList">
        <% Html.RenderPartial("OutOfOfficeDetails"); %>
   </div>

</asp:Content>
