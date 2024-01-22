<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.BreakTypeModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmBreakType"));

        setTitle("Attendence Rule");

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
            var targetDiv = '#divBreakTypeDetails';
            var url = '/LMS/BreakType/Delete';
            var form = $('#frmBreakType');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }
    



   
    
</script>
<form id="frmBreakType" method="post" action="">
<div id="divBreakType">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.BreakType.intBreakID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 25%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Break Name
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                
                <%=Html.TextBoxFor(m => m.BreakType.strBreakName, new { @class = "textRegular required",@style = "width:275px;", maxlength = 100 })%>             
            </td>
        </tr>
        <tr>
            <td>
                Description
            </td>
            <td>
                
                <%=Html.TextAreaFor(m => m.BreakType.strDescription, new { @class = "textRegular", @style = "width:275px;", maxlength = 250, onkeyup = "return ismaxlengthPop(this)" })%>             
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
    <%if (Model.BreakType.intBreakID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.BreakType, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.BreakType, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.BreakType.intBreakID > 0)
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
