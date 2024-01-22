<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveTypeModels>" %>
<script type="text/javascript">
    
    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveTypeDetails"));

        setTitle("Leave Type");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        OptionWisePageRefresh();

        OptionWiseLeaveTypeRefresh();

        FormatTextBox();

    });

    function addLeaveType() {
        if (fnValidate() == true) {
            save();
        }

        return false;
    }

    function deleteDeductLeaveType(dedectedLeaveTypeId) {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divLeaveTypeDetails';
            var url = "/LMS/LeaveType/DeleteDeductedLeave/" + dedectedLeaveTypeId;
            var form = $('#frmLeaveTypeDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }

    function OptionWisePageRefresh() {
        var ckBox = document.getElementById("Model_LeaveType_bitIsEarnLeave");
        var IsEarnLeave = $(ckBox).attr('checked');

        $('#LeaveType_bitIsEarnLeave').val(IsEarnLeave);

        if (IsEarnLeave == true) {
            $('#tblEarnLeave').css('visibility', 'visible');

            var IsFixed = $('#Model_LeaveType_IsFixed').attr('checked');

            //alert('IsFixed' + IsFixed);

            $('#LeaveType_IsFixed').val(IsFixed);

            if (IsFixed == true) {

                $('#trPerUnitDays').css('visibility', 'hidden');

                $('#LeaveType_intEarnLeaveUnitForDays').val("");
                $('#LeaveType_intEarnLeaveUnitForDays').removeClass('integer'); 
                $('#LeaveType_intEarnLeaveUnitForDays').addClass('integerNR');
                $('#LeaveType_intEarnLeaveUnitForDays').attr('disabled', 'disabled');

                $('#LeaveType_strEarnLeaveCalculationType').val("");
                $('#LeaveType_strEarnLeaveCalculationType').removeClass('required');
                $('#LeaveType_intEarnLeaveType').removeClass('required'); 
                $('#LeaveType_strEarnLeaveCalculationType').attr('disabled', 'disabled');
                $('#LeaveType_intEarnLeaveType').attr('disabled', 'disabled');
                $('#lblPerUnitDays').css('visibility', 'hidden');
                $('#lblEarnLeaveType').css('visibility', 'hidden');
                $('#lblCalculationType').css('visibility', 'hidden');
            }
            else {
                $('#trPerUnitDays').css('visibility', 'visible');
                $('#LeaveType_intEarnLeaveUnitForDays').addClass('integer');
                $('#LeaveType_intEarnLeaveUnitForDays').removeAttr('disabled');

                $('#LeaveType_strEarnLeaveCalculationType').addClass('required');
                $('#LeaveType_strEarnLeaveCalculationType').removeAttr('disabled');

                $('#LeaveType_intEarnLeaveType').addClass('required');
                $('#LeaveType_intEarnLeaveType').removeAttr('disabled');

                $('#lblPerUnitDays').css('visibility', 'visible');
                $('#lblEarnLeaveType').css('visibility', 'visible');
                $('#lblCalculationType').css('visibility', 'visible');

            }
        }
        else {
            $('#LeaveType_IsFixed').val(false);
            $('#tblEarnLeave').css('visibility', 'hidden');
            $('#trPerUnitDays').css('visibility', 'hidden');
            $('#lblPerUnitDays').css('visibility', 'hidden');
            $('#lblEarnLeaveType').css('visibility', 'hidden'); 
            $('#lblCalculationType').css('visibility', 'hidden');
            $('#lblPerUnitDays').css('visibility', 'hidden');
            $('#LeaveType_strEarnLeaveCalculationType').val("");
            $('#LeaveType_strEarnLeaveCalculationType').removeClass('required');
            $('#LeaveType_intEarnLeaveType').removeClass('required');
            $('#LeaveType_intEarnLeaveUnitForDays').val("");
            $('#LeaveType_intEarnLeaveUnitForDays').removeClass('integer');
            $('#LeaveType_intEarnLeaveUnitForDays').addClass('integerNR');

        }
        return false;
    }

    function OptionWiseLeaveTypeRefresh() {

        var isServiceLifeType = $('#Model_LeaveType_isServiceLifeType').attr('checked');
        $('#LeaveType_isServiceLifeType').val(isServiceLifeType);

        //bitIsRecreationLeave

        if (isServiceLifeType) {

            $('#tblLeaveYearly').css('visibility', 'hidden');

            $('#LeaveType_intLeaveYearTypeId').val("");
            $('#LeaveType_intLeaveYearTypeId').removeClass('required');
            $('#LeaveType_intLeaveYearTypeId').attr('disabled', 'disabled');

            $('#LeaveType_bitIsEncashable').attr('checked', false);
            $('#LeaveType_bitIsEncashable').attr('disabled', 'disabled');
            $('#Model_LeaveType_bitIsEarnLeave').attr('checked', false);
            $('#Model_LeaveType_bitIsEarnLeave').attr('disabled', 'disabled');

            // Added For BEPZA
            $('#LeaveType_bitIsRecreationLeave').removeAttr('disabled');  

            OptionWisePageRefresh();

        } else {
            $('#LeaveType_isServiceLifeType').val(false);
            $('#tblLeaveYearly').css('visibility', 'visible');
            $('#LeaveType_intLeaveYearTypeId').addClass('required');
            $('#LeaveType_intLeaveYearTypeId').removeAttr('disabled');

            $('#LeaveType_bitIsEncashable').removeAttr('disabled');
            $('#Model_LeaveType_bitIsEarnLeave').removeAttr('disabled');

            // Added For BEPZA
            $('#LeaveType_bitIsRecreationLeave').attr('checked', false);
            $('#LeaveType_bitIsRecreationLeave').attr('disabled', 'disabled');

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

            var targetDiv = '#divLeaveTypeDetails';
            var url = '/LMS/LeaveType/Delete';
            var form = $('#frmLeaveTypeDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }

    function PreventHyphen(evt) {
        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;
        }

        else if (evt) keyCode = evt.which;
        else return true;
        if (keyCode == 45)
            return false;

        return true;
    } 

</script>
<form id="frmLeaveTypeDetails" method="post" action="">
<div id="divLeaveType">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m=>m.LeaveType.intLeaveTypeID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 55%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Leave Type<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveType.strLeaveType, new  {@class="textRegular required", maxlength=50 })%>
            </td>
        </tr>
        <tr>
            <td>
                Short Name<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveType.strLeaveShortName, new { @class = "textRegular required", maxlength = 50, onkeypress = "return PreventHyphen(event);" })%>
            </td>
        </tr>
         <tr>
            <td>
                Approval Group<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveType.intApprovalGroupId, Model.AprovalGroupType, "...Select One...", new { @class = "selectBoxRegular required" })%>
            </td>
        </tr>
    </table>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 40%" />
            <col style="width: 60%" />
        </colgroup>
        <%=Html.HiddenFor(m => m.LeaveType.isServiceLifeType)%>
        <tr>
            <td colspan="1">
                <%=Html.RadioButton("Model_LeaveType_isServiceLifeType", true, Model.LeaveType.isServiceLifeType, new { onClick = "OptionWiseLeaveTypeRefresh();" })%>Service
                Period &nbsp;
                <%=Html.RadioButton("Model_LeaveType_isServiceLifeType", false, !Model.LeaveType.isServiceLifeType, new { onClick = "OptionWiseLeaveTypeRefresh();" })%>Yearly
            </td>
            <td>
                <div id="tblLeaveYearly" style="width: 100%; visibility: hidden;">
                    Leave Year Type<label class="labelRequired">*</label>
                    <%= Html.DropDownListFor(m => m.LeaveType.intLeaveYearTypeId, Model.LeaveYearType, "...Select One...", new { @class = "selectBoxRegular" })%>
                </div>
            </td>
        </tr>
    </table>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 40%" />
            <col style="width: 60%" />
        </colgroup>
<%--         <tr>
            <td colspan="2">
                <%=Html.CheckBoxFor(m => m.LeaveType.bitIsDeductLeaveCalculate)%> Deduct Other Leave Duration to Calculate
            </td>
        </tr>--%>
        
         <tr>
            <td colspan="2">
                <%=Html.CheckBoxFor(m => m.LeaveType.bitIsRecreationLeave)%>Recreation Leave
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <%=Html.CheckBoxFor(m => m.LeaveType.bitIsEncashable)%>Encashable Leave
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%=Html.HiddenFor(m => m.LeaveType.bitIsEarnLeave)%>
                <%=Html.CheckBox("Model_LeaveType_bitIsEarnLeave", Model.LeaveType.bitIsEarnLeave, new { onClick = "OptionWisePageRefresh();" })%>Earn
                Leave
            </td>
        </tr>
    </table>
    <div>
    <fieldset>
        <legend style="font-size:17px"> <strong> Deducted Leave Types </strong> </legend>
            <table id="leaveTypeDetail">
                <tr>
                    <td>Leave Type</td> <%--<label class="labelRequired">*</label>--%>
                    <td> <%= Html.DropDownListFor(m => m.LeaveType.intLeaveTypeAddID, Model.LeaveTypeList, "...Select One...", new { @class = "selectBoxRegular" })%></td>
                    <td><a href="#" class="btnAdd" onclick="return addLeaveType();"></a> </td>
                </tr>
                <tr>
 
                </tr>
            </table>
                 <table class="contenttext" style="width: 100%;">
                    <thead style="background-color: #d2d8e8;">
                        <tr>
                            <th style="width: 69%; text-align: left; padding-left: 5px;">
                                Leave Type
                            </th>
                         
                            <th style="text-align: left; padding-left: 12px;">
                                Remove
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <div style="overflow-y: auto; overflow-x: hidden; max-height: 74px">

                        <% if (Model.LeaveTypeDeduct != null) for (int j = 0; j < Model.LeaveTypeDeduct.Count; j++)
                                { 
                        %>
                        <tr>
                            <td style="width: 70%;">
                                <%=Html.Hidden("LeaveTypeDeduct[" + j + "].intLeaveTypeDeductID", Model.LeaveTypeDeduct[j].intLeaveTypeDeductID.ToString())%>
                                <%=Html.Hidden("LeaveTypeDeduct[" + j + "].intLeaveTypeID", Model.LeaveTypeDeduct[j].intLeaveTypeID.ToString())%>
                                <%=Html.Hidden("LeaveTypeDeduct[" + j + "].intDeductLeaveTypeID", Model.LeaveTypeDeduct[j].intDeductLeaveTypeID.ToString())%>

                                <%=Html.Encode(Model.LeaveTypeDeduct[j].strLeaveType.ToString() )%>
                            </td>
                            <td align="left" style="width: 10%;">
                                <a href='#' visible="false" class="gridDelete" onclick='javascript:return deleteDeductLeaveType("<%= Model.LeaveTypeDeduct[j].intLeaveTypeDeductID %>");'>
                                </a>
                            </td>
                        </tr>
                        <%} %>

                        </div>
                    </tbody>
                </table>       
    </fieldset>

    </div>
    <div id="tblEarnLeave" style="width: 100%; visibility: hidden;">
        <table class="contenttext" style="width: 100%;">
            <colgroup>
                <col style="width: 55%" />
                <col />
            </colgroup>
            <%=Html.HiddenFor(m => m.LeaveType.IsFixed)%>
            <tr>
                <td colspan="2">
                    <%=Html.RadioButton("Model_LeaveType_IsFixed", true, Model.LeaveType.IsFixed, new { onClick = "OptionWisePageRefresh();" })%>Fixed
                    Entitlement
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%=Html.RadioButton("Model_LeaveType_IsFixed", true, !Model.LeaveType.IsFixed, new { onClick = "OptionWisePageRefresh();" })%>Calculated
                    Entitlement
                </td>
            </tr>
            <tr id="trPerUnitDays" style="visibility: hidden">
                <td colspan="2">
                    <table style="width: 100%;">
                        <tr>
<%--                            <td>
                                Per Unit in Days<label id="lblPerUnitDays" style="visibility: hidden" class="labelRequired">*</label>
                            </td>
                            <td>
                                <%=Html.TextBox("LeaveType.intEarnLeaveUnitForDays", Model.LeaveType.intEarnLeaveUnitForDays == 0 ? " " : Model.LeaveType.intEarnLeaveUnitForDays.ToString(), new { @class = "textRegularNumber integerNR", maxlength = 2 })%>
                            </td>--%>
                            <td>
                                Earn Leave Type<label id="lblEarnLeaveType" style="visibility: hidden" class="labelRequired">*</label>
                            </td>
                            <td>
                                <%= Html.DropDownListFor(m => m.LeaveType.intEarnLeaveType, Model.EarnLeaveType, "...Select One...", new { @class = "selectBoxRegular" })%>
                            </td>
                            <td>
                                Calculation Type<label id="lblCalculationType" style="visibility: hidden" class="labelRequired">*</label>
                            </td>
                            <td>
                                <%= Html.DropDownListFor(m => m.LeaveType.strEarnLeaveCalculationType, Model.CalculationType, "...Select One...", new { @class = "selectBoxRegular" })%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.LeaveType.intLeaveTypeID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveType, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveType, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.LeaveType.intLeaveTypeID > 0)
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
