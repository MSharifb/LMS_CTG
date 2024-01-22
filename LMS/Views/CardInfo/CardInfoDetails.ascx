<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CardInfoModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmCardInfo"));

        setTitle("Card Info");

        $("#btnSave").hide();

        $("#btnDelete").hide(); 

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        FormatTextBox();


    });


    function save() {
        if (fnValidateDateTime() == false) {
            alert("Invalid Date.");
            return false;
        }
        if (fnValidate() == true) {
            $('#btnSave').trigger('click');

        }
        return false;
    }



    function Delete() {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = '#divCardInfoDetails';
            var url = '/LMS/CardInfo/Delete';
            var form = $('#frmCardInfo');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }
    



   
    
</script>
<form id="frmCardInfo" method="post" action="">
<div id="divCardInfo">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.CardInfo.intCardID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 25%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Card ID
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                
                <%=Html.TextBoxFor(m => m.CardInfo.strCardID, new { @class = "textRegular required", maxlength = 50 })%>             
            </td>
        </tr>        
         
    </table>
</div>
<div class="divSpacer">
</div>
<%--<div class="divSpacer">
</div>
<div class="divSpacer">
</div>--%>
<div class="divButton">
    <%if (Model.CardInfo.intCardID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CardInfo, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return save();"></a>
    <%} %>
    <%}
      else
      {%>
    <a href="#" class="btnSave" onclick="return save();"></a>
    <%} %>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CardInfo, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.CardInfo.intCardID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <%} %>
    <%} %>
     <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
