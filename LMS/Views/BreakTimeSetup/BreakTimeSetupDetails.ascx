<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.BreakTimeSetupModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmBreakTimeSetupDetails"));

        setTitle("Break Assignment to Shift");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy' });
        $("#BreakTimeSetup_strStartTime").timepicker({ ampm: true });
        $("#BreakTimeSetup_strEndTime").timepicker({ ampm: true });
        OptionWisePageRefresh();

        FormatTextBox();

    });


    function OptionWisePageRefresh() {
        var ckBox = document.getElementById("Model_BreakTimeSetup_bitIsEarnLeave");
        var IsEarnLeave = $(ckBox).attr('checked');

        $('#BreakTimeSetup_bitIsEarnLeave').val(IsEarnLeave);

        if (IsEarnLeave == true) {
            $('#tblEarnLeave').css('visibility', 'visible');

            var IsFixed = $('#Model_BreakTimeSetup_IsFixed').attr('checked');

            $('#BreakTimeSetup_IsFixed').val(IsFixed);

            if (IsFixed == true) {

                $('#trPerUnitDays').css('visibility', 'hidden');

                $('#BreakTimeSetup_intEarnLeaveUnitForDays').val("");
                $('#BreakTimeSetup_intEarnLeaveUnitForDays').removeClass('integer');
                $('#BreakTimeSetup_intEarnLeaveUnitForDays').addClass('integerNR');
                $('#BreakTimeSetup_intEarnLeaveUnitForDays').attr('disabled', 'disabled');

                $('#BreakTimeSetup_strEarnLeaveCalculationType').val("");
                $('#BreakTimeSetup_strEarnLeaveCalculationType').removeClass('required');
                $('#BreakTimeSetup_strEarnLeaveCalculationType').attr('disabled', 'disabled');

                $('#lblPerUnitDays').css('visibility', 'hidden');
                $('#lblCalculationType').css('visibility', 'hidden');
            }
            else {
                $('#trPerUnitDays').css('visibility', 'visible');
                $('#BreakTimeSetup_intEarnLeaveUnitForDays').addClass('integer');
                $('#BreakTimeSetup_intEarnLeaveUnitForDays').removeAttr('disabled');

                $('#BreakTimeSetup_strEarnLeaveCalculationType').addClass('required');
                $('#BreakTimeSetup_strEarnLeaveCalculationType').removeAttr('disabled');

                $('#lblPerUnitDays').css('visibility', 'visible');
                $('#lblCalculationType').css('visibility', 'visible');

            }
        }
        else {
            $('#BreakTimeSetup_IsFixed').val(false);
            $('#tblEarnLeave').css('visibility', 'hidden');
            $('#trPerUnitDays').css('visibility', 'hidden');
            $('#lblPerUnitDays').css('visibility', 'hidden');
            $('#lblCalculationType').css('visibility', 'hidden');
            $('#BreakTimeSetup_strEarnLeaveCalculationType').val("");
            $('#BreakTimeSetup_strEarnLeaveCalculationType').removeClass('required');
            $('#BreakTimeSetup_intEarnLeaveUnitForDays').val("");
            $('#BreakTimeSetup_intEarnLeaveUnitForDays').removeClass('integer');
            $('#BreakTimeSetup_intEarnLeaveUnitForDays').addClass('integerNR');

        }
        return false;
    }

    function save() {

        if (fnValidate() == true) {

            $('#btnSave').trigger('click');

        }
        return false;
    }

    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divBreakTimeSetupDetails';
            var url = '/LMS/BreakTimeSetup/Delete';
            var form = $('#frmBreakTimeSetupDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }



    function SetShiftInOutTime() {

        var url = "/LMS/BreakTimeSetup/GetShiftInOutTime";
        var form = $("#frmBreakTimeSetupDetails");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {

            //alert(result);
            //alert(result[0]);
            //alert(result[1]);

            $("#BreakTimeSetup_strIntime").val(result[0]);
            $("#BreakTimeSetup_strOuttime").val(result[1]);

        }, "json");


    }




    
</script>
<form id="frmBreakTimeSetupDetails" method="post" action="">
<div id="divBreakTimeSetup">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.BreakTimeSetup.intBreakSetID , Model.BreakTimeSetup.intBreakSetID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 30%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Shift Name<label class="labelRequired">*</label>
            </td>
            <td style="width: 30%;" class="contenttabletd">
                <%= Html.DropDownListFor(m => m.BreakTimeSetup.intShiftID, Model.Shift, "...Select One..", new { @class = "selectBoxRegular required",@style = "width:175px", @onchange = "return SetShiftInOutTime();" })%>
            </td>
        </tr>
        <tr>
            <td>
                Break Name<label class="labelRequired">*</label>
            </td>
            <td style="width: 30%;" class="contenttabletd">
                <%= Html.DropDownListFor(m => m.BreakTimeSetup.intBreakID, Model.BreakType, "...Select One..", new { @class = "selectBoxRegular required", @style = "width:175px" })%>
            </td>
        </tr>
        <tr>
            <td>
                Shift In:<label class="labelRequired"></label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.BreakTimeSetup.strIntime, new { @class = "textRegularDate", @readonly = "true" })%>
            </td>
            <td>
                Shift Out:<label class="labelRequired"></label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.BreakTimeSetup.strOuttime, new { @class = "textRegularDate", @readonly = "true" })%>
            </td>
        </tr>
        <tr>
            <td>
                Start Time:<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m=> m.BreakTimeSetup.strStartTime, new { @class = "textRegularDate" })%>
            </td>
            <td>
                End Time:<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m=> m.BreakTimeSetup.strEndTime, new { @class = "textRegularDate" })%>
            </td>
        </tr>
    </table>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.BreakTimeSetup.intBreakSetID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.BreakTimeSetup, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.BreakTimeSetup, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.BreakTimeSetup.intBreakSetID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <input id="btnDelete" style="visibility: hidden;" name="btnDelete" type="submit"
        value="Delete" visible="false" />
    <%} %>
    <%} %>
      <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
