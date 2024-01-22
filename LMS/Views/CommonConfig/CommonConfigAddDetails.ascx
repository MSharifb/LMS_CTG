<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CommonConfigModels>" %>

<script type="text/javascript">
    $(document).ready(function () {
        $("#tabs").tabs();
        getContentTab(1);
    });

    function getContentTab(index) {
        var url = '<%= Url.Content("~/CommonConfig/getAjaxTab") %>/' + index;
        var targetDiv = "#tabs-1";
        $.get(url, null, function (result) {
            $(targetDiv).html(result);
        });
    }
</script>

<%--<div id="tabs" style="height:500px;">
	    <ul>
		    <li><a href="#tabs-1" onclick="getContentTab(1);">Table</a></li>
		    <li><a href="#tabs-1" onclick="getContentTab(2);">Column</a></li>
	    </ul>
	   <div id="tabs-1">
            
	   </div>
    </div>   --%>

