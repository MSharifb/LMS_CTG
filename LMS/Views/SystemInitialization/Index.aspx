<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.DataSynchronizationModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">


    $(document).ready(function () {
        
        $("#Initialization").click();

    });

   
</script>

<div id="divDataList">
  
  </div>
</asp:Content>


