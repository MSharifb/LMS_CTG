<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveRuleModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveRule"));

        setTitle("Leave Rule");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        ChkEncahable();

        EnjoyAtaTime();
        //CheckLeaveType();
        if ($('#LeaveRule_intMaxDeductionDays').val()) {
            $("#IsAvail").attr('checked', true);
        }
        ChkDeduct();
        $("#Model_LeaveRule_bitIsCarryForward").click(function () {
            AddRemoveRequired();
        });

        $("#Model_LeaveRule_bitIsEnjoyAtaTime").click(function () {
            EnjoyAtaTime();
        });

        $("#IsAvail").click(function () {
            ChkDeduct();
        });

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        FormatTextBox();

        $("#LeaveRule_strAllowType").attr('disabled', 'disabled');

    });


    function save() {

        if (fnValidate() == true) {
            $('#btnSave').trigger('click');

        }
        return false;
    }

    //
    function ChkDeduct() {
        if ($("#IsAvail").attr('checked')) {
            $("#LeaveRule_intMaxDeductionDays").addClass('integer');
            $("#LeaveRule_intMaxDeductionDays").removeClass('integerNR');
            $("#LeaveRule_strDeductionAllowType").addClass('required');
            $("#LeaveRule_intDeductionLeaveTypeID").addClass('required');
            $('.trVisibility').css('visibility', 'visible');
        }
        else {
            $('.trVisibility').css('visibility', 'hidden');
            $("#LeaveRule_intMaxDeductionDays").removeClass('integer');
            $("#LeaveRule_intMaxDeductionDays").addClass('integerNR');
            $("#LeaveRule_strDeductionAllowType").removeClass('required');
            $("#LeaveRule_intDeductionLeaveTypeID").removeClass('required'); 
        }
    }

    function Delete() {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = '#divLeaveRuleDetails';
            var url = '/LMS/LeaveRule/Delete';
            var form = $('#frmLeaveRule');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }




    function ChkCalculationFrom() {
        strEligibleAfter1 = $('#LeaveRule_strEligibleAfter').val();
        strCalculationFrom1 = $('#LeaveRule_strCalculationFrom').val();

        if ((strEligibleAfter1 == 'Joining Date') && (strCalculationFrom1 == 'Confirmation Date')) {
            //alert("Calculation from cann't be Confermation Date");
            alert("When leave eligible from is joining date then Calculation from must be joining date.");
            $('#LeaveRule_strCalculationFrom').val("");
        }
        return false;
    }


    function ChkEncahable() {
        var url = "/LMS/LeaveRule/GetEncashment";
        var form = $("#frmLeaveRule");
        var serializedForm = form.serialize();

        var Id = $('#LeaveRule_intLeaveTypeID').val();

        if (Id > 0) {
            $.post(url, serializedForm, function (result) {

                $("#Model_LeaveRule_bitIsEncashable").attr('checked', result[0]);
                $("#LeaveRule_bitIsEncashable").val(result[0]);


                if (result[1] != "" && result[1] == "Calculated") {
                    $("#LeaveRule_fltEntitlement").val("");
                    $("#LeaveRule_fltEntitlement").attr('disabled', 'disabled');

                }
                else {
                    $("#LeaveRule_fltEntitlement").removeAttr('disabled');
                }

                // Added FOR BEPZA
                if (result[2] == true) {

                    $(".trRecreationVisibility").css('visibility', 'visible');

                    $("#LeaveRule_intNextEligibleAfterMonth").addClass('integer');
                    $("#LeaveRule_intNextEligibleAfterMonth").removeClass('integerNR');
                    $("#LeaveRule_intNextEligibleAfterMonth").removeAttr('disabled');

                    $("#LeaveRule_strNextEligibleFrom").addClass('required'); 
                }
                else {
                    $(".trRecreationVisibility").css('visibility', 'hidden');

                    $("#LeaveRule_intNextEligibleAfterMonth").val("");
                    $("#LeaveRule_intNextEligibleAfterMonth").addClass('integerNR');
                    $("#LeaveRule_intNextEligibleAfterMonth").removeClass('integer');
                    $("#LeaveRule_intNextEligibleAfterMonth").attr('disabled', 'disabled');

                    $("#LeaveRule_strNextEligibleFrom").removeClass('required');
                }

                //END

                if (result[0] == true) {

                    $(".trEarn").css('visibility', 'visible');

                    $("#lblMaxReqMark").css('visibility', 'visible');
                    $("#lblMinReqMark").css('visibility', 'visible');

                    $("#LeaveRule_intMaxEncahDays").addClass('integer');
                    $("#LeaveRule_intMaxEncahDays").removeClass('integerNR');
                    $("#LeaveRule_intMaxEncahDays").removeAttr('disabled');

                    $("#LeaveRule_intMinDaysInHand").addClass('integer');
                    $("#LeaveRule_intMinDaysInHand").removeClass('integerNR');
                    $("#LeaveRule_intMinDaysInHand").removeAttr('disabled');

                    $("#LeaveRule_intEarnLeaveUnitForDays").addClass('integer');
                    $("#LeaveRule_intEarnLeaveUnitForDays").removeClass('integerNR');
                }
                else {
                    $(".trEarn").css('visibility', 'hidden');
                  

                    $("#lblMaxReqMark").css('visibility', 'hidden');
                    $("#lblMinReqMark").css('visibility', 'hidden');

                    $("#LeaveRule_intEarnLeaveUnitForDays").addClass('integerNR');
                    $("#LeaveRule_intEarnLeaveUnitForDays").removeClass('integer');

                    $("#LeaveRule_intMaxEncahDays").val("");
                    $("#LeaveRule_intMaxEncahDays").addClass('integerNR');
                    $("#LeaveRule_intMaxEncahDays").removeClass('integer');
                    $("#LeaveRule_intMaxEncahDays").attr('disabled', 'disabled');

                    $("#LeaveRule_intMinDaysInHand").val("");
                    $("#LeaveRule_intMinDaysInHand").addClass('integerNR');
                    $("#LeaveRule_intMinDaysInHand").removeClass('integer');
                    $("#LeaveRule_intMinDaysInHand").attr('disabled', 'disabled');

                }
            }, "json");
        }
        else {
            $("#Model_LeaveRule_bitIsEncashable").attr('checked', false);
            $("#LeaveRule_bitIsEncashable").val(false);

            $("#lblMaxReqMark").css('visibility', 'hidden');
            $("#lblMinReqMark").css('visibility', 'hidden');

            $("#LeaveRule_fltEntitlement").val("");
            $("#LeaveRule_intMaxEncahDays").val("");
            $("#LeaveRule_intMinDaysInHand").val("");

            $("#LeaveRule_intMaxEncahDays").addClass('integerNR');
            $("#LeaveRule_intMaxEncahDays").removeClass('integer');
            $("#LeaveRule_intMaxEncahDays").attr('disabled', 'disabled');

            $("#LeaveRule_intMinDaysInHand").addClass('integerNR');
            $("#LeaveRule_intMinDaysInHand").removeClass('integer');
            $("#LeaveRule_intMinDaysInHand").attr('disabled', 'disabled');
            $("#LeaveRule_fltEntitlement").removeAttr('disabled');
        }

        $("#LeaveRule_strAllowType").attr('disabled', 'disabled');

        if (Id != '') {
            var url = "/LMS/LeaveRule/GetLeaveTypeStatus";
            $.getJSON(url, { Id: Id }, function (result) {
                $('#LeaveRule_hfstrAllowType').val(result.status);
                if (result.status == 'Y') {
                    $('#LeaveRule_strAllowType').children('option[value="Y"]').attr("selected", true);
                    $('#lblAllowName').text('Year');
                    $('#lblEntitlement').text('(Yearly)');

                    $('#LeaveRule_bitIsIncludeHoliday').attr('disabled', false);
                    $('#LeaveRule_bitIsIncludeWeekend').attr('disabled', false);
                    $('#Model_LeaveRule_bitIsCarryForward').attr('disabled', false);
                    
                }
                else {
                    $('#LeaveRule_strAllowType').children('option[value="S"]').attr("selected", true);
                    $('#lblAllowName').text('Service Life');
                    $('#lblEntitlement').text('(Service Life)');

                    $('#LeaveRule_bitIsIncludeHoliday').attr('disabled', 'disabled');
                    $('#LeaveRule_bitIsIncludeWeekend').attr('disabled', 'disabled');
                    $('#Model_LeaveRule_bitIsCarryForward').attr('disabled', 'disabled');
                }
            });
        }
        else {
            $('#LeaveRule_strAllowType').children('option[value="M"]').attr("selected", true);
            $('#lblAllowName').text('Month');
        }


        return false;

    }

    function AddRemoveRequired() {
        //var IsCarryFW = $('#LeaveRule_bitIsCarryForward').val();
        var IsCarryFW = $("#Model_LeaveRule_bitIsCarryForward").attr('checked');
        $('#LeaveRule_bitIsCarryForward').val(IsCarryFW);
        if (IsCarryFW == true) {

            $("#lblMaxCarryoverMark").css('visibility', 'visible');
            $("#lblObsoluteCarryMark").css('visibility', 'visible');

            $("#LeaveRule_intMaxCarryForwardDays").addClass('integer');
            $("#LeaveRule_intMaxCarryForwardDays").removeClass('integerNR');
            $("#LeaveRule_intMaxCarryForwardDays").removeAttr('disabled');

            $("#LeaveRule_strLeaveObsoluteMonth").addClass('required');
            $("#LeaveRule_strLeaveObsoluteMonth").removeAttr('disabled');


        }
        else {

            $("#lblMaxCarryoverMark").css('visibility', 'hidden');
            $("#lblObsoluteCarryMark").css('visibility', 'hidden');

            $("#LeaveRule_intMaxCarryForwardDays").val("");
            $("#LeaveRule_intMaxCarryForwardDays").addClass('integerNR');
            $("#LeaveRule_intMaxCarryForwardDays").removeClass('integer');
            $("#LeaveRule_intMaxCarryForwardDays").attr('disabled', 'disabled');

            $("#LeaveRule_strLeaveObsoluteMonth").val("");
            $("#LeaveRule_strLeaveObsoluteMonth").removeClass('required');
            $("#LeaveRule_strLeaveObsoluteMonth").attr('disabled', 'disabled');

        }

        return false;
    }



    function EnjoyAtaTime() {

        var IsEAT = $("#Model_LeaveRule_bitIsEnjoyAtaTime").attr('checked');
        $('#LeaveRule_bitIsEnjoyAtaTime').val(IsEAT);

        if (IsEAT == true) {
            //var Ent = $('#LeaveRule_fltEntitlement').val();
            var Ent = $('#LeaveRule_intMaxLeaveDaysInApplication').val();
            if (Ent == "" || Ent == 0) {
                //alert("Yearly Entitlement must be greater than zero.");
                alert("Max. No. of Leave Days to be Allowed at a Time must be greater than zero.");

                $("#Model_LeaveRule_bitIsEnjoyAtaTime").attr('checked', false);
                $('#LeaveRule_bitIsEnjoyAtaTime').val(false);
            }
            else {
                //$('#LeaveRule_intMaxLeaveDaysInApplication').val(0);
                //$('#LeaveRule_intMaxLeaveAppInMonth').val(0);
                $('#LeaveRule_intMaxLeaveDaysInMonth').val(0);

                //$('#LeaveRule_intMaxLeaveDaysInApplication').attr('disabled', 'disabled');
                //$('#LeaveRule_intMaxLeaveAppInMonth').attr('disabled', 'disabled');
                $('#LeaveRule_intMaxLeaveDaysInMonth').attr('disabled', 'disabled');

                $("#Model_LeaveRule_bitIsCarryForward").attr('checked', false);
                $('#LeaveRule_bitIsCarryForward').val(false);
                $('#Model_LeaveRule_bitIsCarryForward').attr('disabled', 'disabled');

                //$("#LeaveRule_strAllowType").val("M");
                //$("#LeaveRule_strAllowType").attr('disabled', 'disabled');

            }


        }
        else {
            $('#LeaveRule_intMaxLeaveDaysInApplication').removeAttr('disabled');
            $('#LeaveRule_intMaxLeaveAppInMonth').removeAttr('disabled');
            $('#LeaveRule_intMaxLeaveDaysInMonth').removeAttr('disabled');
            $('#Model_LeaveRule_bitIsCarryForward').removeAttr('disabled');
            $("#LeaveRule_strAllowType").val();
            $("#LeaveRule_strAllowType").removeAttr('disabled');

        }
        AddRemoveRequired();


        return false;
    }



    function CheckLeaveType() {
        var Id = $('#LeaveRule_intAdjustLeaveTypeID').val();

        if (Id > 0) {
            var Id2 = $('#LeaveRule_intLeaveTypeID').val();
            if (Id2 > 0) {
                if (Id == Id2) {
                    alert("Adjust leave type must be different.");
                    $('#LeaveRule_intAdjustLeaveTypeID').val("");
                    return false;
                }
            }
            else {
                alert("Please select leave type at first.");
                $('#LeaveRule_intAdjustLeaveTypeID').val("");
                return false;
            }

        }
        else {
            //$('#divEnjoy').css('visibility', 'hidden');
            $("#Model_LeaveRule_bitIsEnjoyAtaTime").attr('checked', false);
            $('#LeaveRule_bitIsEnjoyAtaTime').val(false);
        }

        return false;
    }


    function maxAllowedDays() {
        var yearlyEntitled = $("#LeaveRule_fltEntitlement").val();
        var maxDays = $("#LeaveRule_intMaxLeaveDaysInMonth").val();


        if (parseInt(yearlyEntitled) > 31) {

            if (parseInt(maxDays) > 31) {
                $("#LeaveRule_intMaxLeaveDaysInMonth").val('31');
            }
        }
        else {
            if (parseInt(yearlyEntitled) < parseInt(maxDays)) {
                $("#LeaveRule_intMaxLeaveDaysInMonth").val(yearlyEntitled);
            }
        }
    }

</script>
<form id="frmLeaveRule" method="post" action="">
<div id="divLeaveRule">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.LeaveRule.intRuleID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 65%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Rule Name
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveRule.strRuleName, new { @class = "textRegular required", maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td>
                Leave Type
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveRule.intLeaveTypeID, Model.LeaveType, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return ChkEncahable();" })%>
            </td>
        </tr>
        <tr>
            <td>
                Leave Adjust With
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveRule.intAdjustLeaveTypeID, Model.AdjustLeaveType, "...Select One...", new { @class = "selectBoxRegular", onchange = "return CheckLeaveType();" })%>
            </td>
        </tr>
        <tr>
            <td>
                Entitlement <label id="lblEntitlement"></label>
            </td>
            <td>
                <%=Html.TextBox("LeaveRule.fltEntitlement", Model.LeaveRule.fltEntitlement == 0 ? "" : Model.LeaveRule.fltEntitlement.ToString("N2"), new { @class = "textRegularNumber doubleNR", @style = "width:50px; min-width:50px;", maxlength = 8 })%>
            </td>
        </tr>
        <tr class="trEarn" style="visibility: hidden" >
            <td>
                Per Unit In Days
            </td>
            <td>
                <%=Html.TextBox("LeaveRule.intEarnLeaveUnitForDays", Model.LeaveRule.intEarnLeaveUnitForDays == 0 ? "" : Model.LeaveRule.intEarnLeaveUnitForDays.ToString(), new { @class = "textRegularNumber", @style = "width:50px; min-width:50px;", maxlength = 8 })%>
            </td>
        </tr>
        
        <tr>
            <td>
                Eligible From<label class="labelRequired">*</label>
            </td>
            <td>
                <div style="float: left; text-align: left;">
                    <%= Html.DropDownListFor(m => m.LeaveRule.strEligibleAfter, Model.Eligible, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return ChkCalculationFrom();" })%>
                </div>
                <div style="float: left; text-align: left; padding-left: 2px;">
                    After month of
                    <%=Html.TextBox("LeaveRule.intEligibleAfterMonth", Model.LeaveRule.intEligibleAfterMonth == 0 ? "" : Model.LeaveRule.intEligibleAfterMonth.ToString(), new { @class = "textRegularNumber integerNR", @style = "width:50px; min-width:50px;", maxlength =2 })%>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Calculation From<label class="labelRequired">*</label>
            </td>
            <td>
                <div style="float: left; text-align: left;">
                    <%= Html.DropDownListFor(m => m.LeaveRule.strCalculationFrom, Model.CalculationFrom, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return ChkCalculationFrom();" })%>
                </div>
                <div style="float: left; text-align: left; padding-left: 2px;">
                    After month of
                    <%=Html.TextBox("LeaveRule.intCalculateAfterMonth", Model.LeaveRule.intCalculateAfterMonth == 0 ? "" : Model.LeaveRule.intCalculateAfterMonth.ToString(), new { @class = "textRegularNumber integerNR", @style = "width:50px; min-width:50px;", maxlength = 2 })%>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="contenttext" style="width: 100%;">
                    <tr>
                        <td>
                            <table class="contenttext">
                                <tr>
                                    <td class="contenttabletd">
                                        <%=Html.HiddenFor(m => m.LeaveRule.bitIsEncashable)%>
                                        <%=Html.CheckBox("Model_LeaveRule_bitIsEncashable", Model.LeaveRule.bitIsEncashable, new { @disabled = "disabled" })%>Encashable
                                        <%--<%=Html.CheckBoxFor(m => m.LeaveRule.bitIsEncashable)%>Encashable--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contenttabletd">
                                        Max. Days Encashable<label id="lblMaxReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td class="contenttabletd">
                                        <%=Html.TextBox("LeaveRule.intMaxEncahDays", Model.LeaveRule.intMaxEncahDays == 0 ? "" : Model.LeaveRule.intMaxEncahDays.ToString(), new { @class = "textRegularDuration integerNR", maxlength = 4 })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contenttabletd">
                                        Min. Days in Hand
                                        <label id="lblMinReqMark" style="visibility: hidden" class="labelRequired">
                                            *</label>
                                    </td>
                                    <td class="contenttabletd">
                                        <%=Html.TextBox("LeaveRule.intMinDaysInHand", Model.LeaveRule.intMinDaysInHand == 0 ? "" : Model.LeaveRule.intMinDaysInHand.ToString(), new { @class = "textRegularDuration integerNR", maxlength = 4 })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="contenttext">
                                <tr>
                                    <td class="contenttabletd">
                                        <%=Html.HiddenFor(m => m.LeaveRule.bitIsCarryForward)%>
                                        <%=Html.CheckBox("Model_LeaveRule_bitIsCarryForward", Model.LeaveRule.bitIsCarryForward, new { onchange = "return AddRemoveRequired();" })%>Carry
                                        Over
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contenttabletd">
                                        Max. Carry Over in Days<label id="lblMaxCarryoverMark" style="visibility: hidden"
                                            class="labelRequired">*</label>
                                    </td>
                                    <td class="contenttabletd">
                                        <%=Html.TextBox("LeaveRule.intMaxCarryForwardDays", Model.LeaveRule.intMaxCarryForwardDays == 0 ? "" : Model.LeaveRule.intMaxCarryForwardDays.ToString(), new { @class = "textRegularDuration integerNR", maxlength = 4 })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contenttabletd">
                                        Obsolete Carry Forward Month<label id="lblObsoluteCarryMark" style="visibility: hidden"
                                            class="labelRequired">*</label>
                                    </td>
                                    <td class="contenttabletd">
                                        <%= Html.DropDownListFor(m => m.LeaveRule.strLeaveObsoluteMonth, Model.CarryForwardMonth, "...Select One...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="width: 100%; float: left; text-align: left;">
                    <div style="float: left; text-align: left;">
                        <%=Html.CheckBoxFor(m => m.LeaveRule.bitIsIncludeHoliday)%>Exclude Holiday from
                        Leave Duration
                    </div>
                    <div style="float: right; text-align: right; padding-left: 12px;">
                        <%=Html.CheckBoxFor(m => m.LeaveRule.bitIsIncludeWeekend)%>Exclude Weekend from
                        Leave Duration
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="width: 100%; float: left; text-align: left;">
                    <div style="float: left; text-align: left;">
                        <%=Html.CheckBoxFor(m => m.LeaveRule.bitIsIncludeWHForWOP)%>Include Holiday & Weekend
                        for Without Pay Leave Duration
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Max. No. of Leave Days to be Allowed at a Time
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <div style="float: left; text-align: left;">
                    <%=Html.TextBox("LeaveRule.intMaxLeaveDaysInApplication", Model.LeaveRule.intMaxLeaveDaysInApplication == 0 ? "" : Model.LeaveRule.intMaxLeaveDaysInApplication.ToString(), new { @class = "textRegularNumber integer", @style = "width:75px; min-width:75px;", maxlength = 4 })%>
                </div>
                <%--<div id="divEnjoy" style="float: left; text-align: left; padding-left: 8px; visibility: hidden;">--%>
                <div id="divEnjoy" style="float: left; text-align: left; padding-left: 8px;">
                    <%=Html.HiddenFor(m => m.LeaveRule.bitIsEnjoyAtaTime)%>
                    <%=Html.CheckBox("Model_LeaveRule_bitIsEnjoyAtaTime", Model.LeaveRule.bitIsEnjoyAtaTime, new { onchange = "return EnjoyAtaTime();" })%>Avail
                    at a Time
                </div>
            </td>
        </tr>
         <tr>
            <td>
                Max. No. of Leave Days to be Allowed Against Valid Reason
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <div style="float: left; text-align: left;">
                    <%=Html.TextBox("LeaveRule.intMaxValidReasonDays", Model.LeaveRule.intMaxValidReasonDays == 0 ? "" : Model.LeaveRule.intMaxValidReasonDays.ToString(), new { @class = "textRegularNumber integer", @style = "width:75px; min-width:75px;", maxlength = 4 })%>
                </div>
              
            </td>
        </tr>

        <tr>
            <%= Html.HiddenFor(m => m.LeaveRule.hfstrAllowType)%>
            <td>
                Max. No. of Leave Application to be Allowed in a
                <%= Html.DropDownListFor(m => m.LeaveRule.strAllowType, Model.MonthYearService, new { @class = "selectBoxRegular", @style = "width:90px; font-size:12px;" })%><label
                    class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBox("LeaveRule.intMaxLeaveAppInMonth", Model.LeaveRule.intMaxLeaveAppInMonth == 0 ? "" : Model.LeaveRule.intMaxLeaveAppInMonth.ToString(), new { @class = "textRegularNumber integer", @style = "width:75px; min-width:75px;", maxlength = 4 })%>
            </td>
        </tr>
        <tr>
            <td>
                Max. No. of Leave Days to be Allowed in a
                <label id="lblAllowName">
                    Month</label><label class="labelRequired">*</label>
            </td>
            <td>
                <%--<%=Html.TextBox("LeaveRule.intMaxLeaveDaysInMonth", Model.LeaveRule.intMaxLeaveDaysInMonth == 0 ? "" : Model.LeaveRule.intMaxLeaveDaysInMonth.ToString(), new { @class = "textRegularNumber integer",maxlength=4 })%>--%>
                <%=Html.TextBox("LeaveRule.intMaxLeaveDaysInMonth", Model.LeaveRule.intMaxLeaveDaysInMonth.ToString(), new { @class = "textRegularNumber integer", @style = "width:75px; min-width:75px;", maxlength = 4 })%>
            </td>
        </tr>
         <tr>
            <td>
                If want to Avail More than Granted Leave ?
                
            </td>
            <td>
               <%= Html.CheckBox("IsAvail",false) %>
            </td>
        </tr>
        <tr class="trVisibility" style="visibility: hidden">
            <td>
               Leave Type for Deduction of More Availing
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveRule.intDeductionLeaveTypeID, Model.LeaveType, "...Select One...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr class="trVisibility" style="visibility: hidden">
           
            <td>
                Max. Leave Deduction to be Allowed In
                <%= Html.DropDownListFor(m => m.LeaveRule.strDeductionAllowType, Model.MonthYearService, "..Select One..", new { @class = "selectBoxRegular", @style = "width:90px; font-size:12px;" })%>
            </td>
            <td>
                <%=Html.TextBox("LeaveRule.intMaxDeductionDays", Model.LeaveRule.intMaxDeductionDays == 0 ? "" : Model.LeaveRule.intMaxDeductionDays.ToString(), new { @class = "textRegularNumber", @style = "width:75px; min-width:75px;", maxlength = 4 })%>
            </td>
        </tr>
        <tr class="trRecreationVisibility" style="visibility: hidden">
           
            <td>
                Next Eligible After Month Of <label class="labelRequired">*</label>
                <%=Html.TextBox("LeaveRule.intNextEligibleAfterMonth", Model.LeaveRule.intNextEligibleAfterMonth == 0 ? "" : Model.LeaveRule.intNextEligibleAfterMonth.ToString(), new { @class = "textRegularNumber", @style = "width:75px; min-width:75px;", maxlength = 4 })%>
            </td>
            <td>
                From <label class="labelRequired">*</label>
                <%= Html.DropDownListFor(m => m.LeaveRule.strNextEligibleFrom, Model.NextEligibleList, "..Select One..", new { @class = "selectBoxRegular", @style = "width:150px; font-size:12px;" })%>
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
    <%if (Model.LeaveRule.intRuleID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveRule, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveRule, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.LeaveRule.intRuleID > 0)
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
