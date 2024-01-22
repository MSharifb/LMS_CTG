<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ShiftModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

//            $("#datepicker").datepicker({
//                changeMonth: true,
//                changeYear: false
//            }).click(function () { $('#Shift_strPeriodFrom').datepicker('show'); });


//                $("#datepicker1").datepicker({
//                    changeMonth: true,
//                    changeYear: false
//                }).click(function () { $('#Shift_strPeriodTo').datepicker('show'); });

//                $("#datepicker2").datepicker({
//                    changeMonth: true,
//                    changeYear: false
//                }).click(function () { $('#Shift_strEffectiveDate').datepicker('show'); });



        preventSubmitOnEnter($("#frmShiftDetails"));

        setTitle("Shift Information");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#Shift_strIntime").timepicker({ ampm: true });
        $("#Shift_strOuttime").timepicker({ ampm: true });
        $("#Shift_strHalfTime").timepicker({ ampm: true });
        OptionWisePageRefresh();

        FormatTextBox();

    });





    function OptionWisePageRefresh() {
        var ckBox = document.getElementById("Model_Shift_bitIsEarnLeave");
        var IsEarnLeave = $(ckBox).attr('checked');

        $('#Shift_bitIsEarnLeave').val(IsEarnLeave);

        if (IsEarnLeave == true) {
            $('#tblEarnLeave').css('visibility', 'visible');

            var IsFixed = $('#Model_Shift_IsFixed').attr('checked');

            $('#Shift_IsFixed').val(IsFixed);

            if (IsFixed == true) {

                $('#trPerUnitDays').css('visibility', 'hidden');

                $('#Shift_intEarnLeaveUnitForDays').val("");
                $('#Shift_intEarnLeaveUnitForDays').removeClass('integer');
                $('#Shift_intEarnLeaveUnitForDays').addClass('integerNR');
                $('#Shift_intEarnLeaveUnitForDays').attr('disabled', 'disabled');

                $('#Shift_strEarnLeaveCalculationType').val("");
                $('#Shift_strEarnLeaveCalculationType').removeClass('required');
                $('#Shift_strEarnLeaveCalculationType').attr('disabled', 'disabled');

                $('#lblPerUnitDays').css('visibility', 'hidden');
                $('#lblCalculationType').css('visibility', 'hidden');
            }
            else {
                $('#trPerUnitDays').css('visibility', 'visible');
                $('#Shift_intEarnLeaveUnitForDays').addClass('integer');
                $('#Shift_intEarnLeaveUnitForDays').removeAttr('disabled');

                $('#Shift_strEarnLeaveCalculationType').addClass('required');
                $('#Shift_strEarnLeaveCalculationType').removeAttr('disabled');

                $('#lblPerUnitDays').css('visibility', 'visible');
                $('#lblCalculationType').css('visibility', 'visible');

            }
        }
        else {
            $('#Shift_IsFixed').val(false);
            $('#tblEarnLeave').css('visibility', 'hidden');
            $('#trPerUnitDays').css('visibility', 'hidden');
            $('#lblPerUnitDays').css('visibility', 'hidden');
            $('#lblCalculationType').css('visibility', 'hidden');
            $('#Shift_strEarnLeaveCalculationType').val("");
            $('#Shift_strEarnLeaveCalculationType').removeClass('required');
            $('#Shift_intEarnLeaveUnitForDays').val("");
            $('#Shift_intEarnLeaveUnitForDays').removeClass('integer');
            $('#Shift_intEarnLeaveUnitForDays').addClass('integerNR');

        }
        return false;
    }

    function save() {

        if (fnValidate() == true && checkDateValidation() == true) {

            $('#btnSave').trigger('click');

        }
        return false;
    }

    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divShiftDetails';
            var url = '/LMS/Shift/Delete';
            var form = $('#frmShiftDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }

    function checkDateValidation() {

        var pdtAPFrom = $('#Shift_strPeriodFrom').val();
        var pdtAPTo = $('#Shift_strPeriodTo').val();
        var dtIntime = $('#Shift_strIntime').val();
        var dtOuttime = $('#Shift_strOuttime').val();


        if (pdtAPFrom != '' && pdtAPTo != '') {
            if (pdtAPFrom > pdtAPTo) {
                alert("From Date must be smaller than or equal to 'To Date'.");
                return false;
            }
        }


        //        if (CheckTime(dtIntime, dtOuttime)) {
        //            alert("In Time must be smaller than or equal to 'Out Time'.");
        //            return false;
        //        }


        return true;
    }
    
</script>
<form id="frmShiftDetails" method="post" action="">
<div id="divShift">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m=>m.Shift.intShiftID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%; margin-left: 5px;">
        <colgroup>
            <col style="width: 22%" />
            <col style="width: 79%" />
        </colgroup>
        <tr>
            <td>
                Shift Name<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.Shift.strShiftName, new  {@class="textRegular required", maxlength=50 })%>
            </td>
        </tr>
        <tr>
            <td>
                Description<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextAreaFor(m => m.Shift.strDescription, new { @class = "textRegular required", @style = "width:443px;", maxlength=50 })%>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <%=Html.CheckBoxFor(m => m.Shift.bitIsRoaster)%>Is Roastering?
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 0px;">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; margin-left: -2px;">
                    <colgroup>
                        <col style="width: 22%" />
                        <col style="width: 27%" />
                        <col style="width: 20%" />
                        <col style="width: 31%" />
                    </colgroup>
                    <tr>
                        <td>
                            From Date<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.Shift.strPeriodFrom, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                            <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
                        </td>
                        <td>
                            To Date<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.Shift.strPeriodTo, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                            <%--<img alt="" id="datepicker1" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>      
                        </td>
                    </tr>
                    <tr>
                        <td>
                            In Time<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.Shift.strIntime, new { @class = "textRegularDate" })%>
                        </td>
                        <td>
                            Out Time<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.Shift.strOuttime, new { @class = "textRegularDate" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            In Grace Time<label class="labelRequired">*</label>
                        </td>
                        <td style="white-space: nowrap;">
                            <%=Html.TextBoxFor(m => m.Shift.intGraceInMin, new { @class = "textRegularDate required", maxlength = 3 })%>&nbsp;Minute(s)
                        </td>
                        <td>
                            Out Grace Time<label class="labelRequired">*</label>
                        </td>
                        <td style="white-space: nowrap;">
                            <%=Html.TextBoxFor(m => m.Shift.intGraceOutTimeMin, new { @class = "textRegularDate required", maxlength = 3 })%>&nbsp;Minute(s)
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Absent Time<label class="labelRequired">*</label>
                        </td>
                        <td style="white-space: nowrap;">
                            <%=Html.TextBoxFor(m => m.Shift.intAbsentMin, new { @class = "textRegularDate required", maxlength = 3 })%>&nbsp;Minute(s)
                        </td>
                        <td>
                            Half Time<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.Shift.strHalfTime, new { @class = "textRegularDate" })%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Effective Date<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.Shift.strEffectiveDate, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                <%--<img alt="" id="datepicker2" style="height:16px;"  src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  onclick="return datepicker_onclick()" />--%>
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
    <%if (Model.Shift.intShiftID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.Shift, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.Shift, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.Shift.intShiftID > 0)
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
