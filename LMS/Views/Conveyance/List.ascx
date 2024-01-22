<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ConveyanceModels>" %>
<script type="text/javascript">
      $(document).ready(function() {
            $("#tabs").tabs();
            getContentTab (1);
             $("#divConveyance").dialog({ autoOpen: false, modal: true, height: 500, width: 700, resizable: false, title: 'Conveyance Details', beforeclose: function (event, ui) { Closing(); } });
             $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        });

        function getContentTab(index) {
            var url='<%= Url.Content("~/Conveyance/getAjaxTab") %>/' + index;
            var targetDiv = "#tabs-1";
            $.get(url,null, function(result) {
                $(targetDiv).html(result);
            });
        }
</script>

<style  type="text/css">
#tabs .ui-widget-header {
    background-image: none;
    background:none; 
    border-top:0px;
    border-right:0px;
    border-left:0px;
    border-bottom: 1px solid #4297D7;
}


</style>




<div id="tabs" style="height:500px;">
	    <ul>
		    <li><a href="#tabs-1" onclick="getContentTab(1);">Due List</a></li>
		    <li><a href="#tabs-1" onclick="getContentTab(2);">Paid List</a></li>
	    </ul>
	    <div id="tabs-1">
            
	    </div>	       
    </div>   
