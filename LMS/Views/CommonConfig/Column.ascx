<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CommonConfigModels>" %>

<script type="text/javascript">

    function savedata() {
        if (fnValidate() == true) {
            var form = $("#frmColumnMapping");
            var serializedForm = form.serialize();
            var url = '/LMS/CommonConfig/SaveHRMColumnMapping';
            $.post(url, serializedForm, function (result) { $("#tabs-1").html(result); }, "html");
        }
        return false;
    }
   
    function GetColumns()
    {
        var targetDiv = "#tabs-1";
        var url = "/LMS/CommonConfig/GetColumns";
        var form = $("#frmColumnMapping");
        var serializedForm = form.serialize();       

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        return false;
    }

 </script>

   <form id="frmColumnMapping" method="post" action="">

   
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                LMS Table Name<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.IntTableID, Model.LMSTables, "...Select One...", new { @class = "selectBoxRegular required", @onchange = "return GetColumns();" })%>
            </td>
        </tr>        
    </table>
</div>

<div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
        <div id="divLedger">
        <% Html.RenderPartial("ColumnDetail"); %>
    </div>
    

    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>

   <div class="divButton">
        <a id="btnImgSave" href="#" class="btnSave" onclick="return savedata();"></a>
        <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
   </div>

   <div id="divMsgStd" class="divMsg">
        <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
    </div>

   </form>