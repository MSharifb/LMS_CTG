<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.MISCModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
   <script type="text/javascript">

       $(document).ready(function () {
          // $("#my-zoom").tzoom();

           // using bind
           $('#my-zoom').bind('mousewheel', function (event, delta) {
               console.log(delta);
           });

           // using the event helper
           $('#my-zoom').mousewheel(function (event, delta) {
               console.log(delta);
           });


       });

   </script>
   
   <div id="my-zoom" style="width:300px; height:300px;">
        <img width="680" class="example1" height="430"  alt=""  src="<%= Url.Content("~/MISCAttachedFiles/"+ Model.FileName)%>" />
   </div>

</asp:Content>
