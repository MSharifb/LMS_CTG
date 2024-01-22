<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.DataSynchronizationModel>" %>

<%= Html.ValidationSummary("Process Failed.") %>
<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmInitialized"));
    });
    
    function ProcessData() {

        document.getElementById('imgLoader').style.visibility = 'visible';
        targetDiv = "#" + 'divDataList';       
        var url = '/LMS/SystemInitialization/Initialize';
        var form = $("#" + 'frmInitialized');
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        return false;
        
    }
</script>
<form id="frmInitialized" method="post" action="">
<div>
    <span class="textmediumbold">Please click on the following button to initialize the system</span>
    
</div>
<div style="height:15px;"></div>
     <div>
             <a href="#" class="btnProcess" onclick="return ProcessData();"></a>          
     </div>
        <img id="imgLoader" src="../../Content/ajax-loader.gif" style="visibility:hidden;"/>
     <div>
        <%=Model.Message %>
     </div>
 
    </form> 
