<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveYearMappingModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveYearMappingDetails"));

        setTitle("Leave Type");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        

        //OptionWisePageRefresh();

        //OptionWiseLeaveTypeRefresh();

        FormatTextBox();

    });


    function save() {

        if (fnValidate() == true) {

            $('#btnSave').trigger('click');

        }
        return false;
    }

    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divLeaveYearMappingDetails';
            var url = '/LMS/LeaveYearMapping/Delete';
            var form = $('#frmLeaveYearMappingDetails');
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

<script type="text/javascript">

    $(function () {

//        $("#LeaveYearMapping_intLeaveTypeID").change(function () {

//            var url = '/LMS/LeaveYearMapping/GetDropDown' + '?Id=' + $(this).val();
//            var ddltarget = "#LeaveYearMapping_intLeaveYearId";

//            if ($(this).val() != '') {

//                $.getJSON(url, function (data) {
//                    $(ddltarget).empty();
//                    $.each(data, function (index, optionData) {
//                        $(ddltarget).append("<option value='" + optionData.Value + "'>" + optionData.Text + "</option>");
//                    });

//                });
//            }
//            else {

//                $('#LeaveYearMapping_intLeaveYearId')
//                .empty()
//                .append('<option value="">...Select One...</option>')
//                .find('option:first')
//                .attr("selected", "selected")
//                ;
//            
//            }
//        });

    });

    function GetLeaveYearList() {

        var Id = $("#LeaveYearMapping_intLeaveTypeID").val();
        var url = '/LMS/LeaveYearMapping/GetDropDown' + '?Id=' + Id;
        var ddltarget = "#LeaveYearMapping_intLeaveYearId";

        if (Id != '') {

            $.getJSON(url, function (data) {
                $(ddltarget).empty();
                $.each(data, function (index, optionData) {
                    $(ddltarget).append("<option value='" + optionData.Value + "'>" + optionData.Text + "</option>");
                });

            });
        }
        else {

            $('#LeaveYearMapping_intLeaveYearId')
                .empty()
                .append('<option value="">...Select One...</option>')
                .find('option:first')
                .attr("selected", "selected")
                ;

        }
    
    }

    function GetStartAndEndDate() {
        var intLeaveYearId = $('#LeaveYearMapping_intLeaveYearId').val();
        var startDateMonth = $("#LeaveYearMapping_strStartDate").val();
        var startDateYear = $("#LeaveYearMapping_strEndDate").val();

        if (intLeaveYearId != "") {
            $.getJSON('/LMS/LeaveYearMapping/GetStartEndDate', { intLeaveYearId: intLeaveYearId }, function (result) {
                $("#LeaveYearMapping_strStartDate").val(result.startDate);
                $("#LeaveYearMapping_strEndDate").val(result.endDate);
            });
        }
        else {
            $("#LeaveYearMapping_strStartDate").val('');
            $("#LeaveYearMapping_strEndDate").val('');
        }
        return false;
    }

</script>

<form id="frmLeaveYearMappingDetails" method="post" action="">
<div id="divLeaveType">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.LeaveYearMapping.intLeaveYearMapID)%>
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
                <%= Html.DropDownListFor(m => m.LeaveYearMapping.intLeaveTypeID, Model.LeaveTypeList, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return GetLeaveYearList();" })%>
            </td>
        </tr>
        <tr>
            <td>
                Leave Year<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveYearMapping.intLeaveYearId, Model.LeaveYearList, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return GetStartAndEndDate();" })%>
            </td>
        </tr>
        <tr>
            <td>
                Start Date<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveYearMapping.strStartDate, new { @class = "textRegular", @readonly = true })%>
            </td>
        </tr>
        <tr>
            <td>
                End Date<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveYearMapping.strEndDate, new { @class = "textRegular", @readonly = true })%>
            </td>
        </tr>
        <tr>
            <td>
                End Date<label class="labelRequired">*</label>
            </td>
            <td>
               <%=Html.CheckBoxFor(m => m.LeaveYearMapping.bitIsActiveYear)%>Active Year
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
    <%if (Model.LeaveYearMapping.intLeaveYearMapID > 0)
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
    <%if (Model.LeaveYearMapping.intLeaveYearMapID > 0)
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
