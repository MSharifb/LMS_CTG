<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveYearModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveYear"));

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        FormatTextBox();

        if ($('#LeaveYear_strEndDate').val() == '01-01-0001') {
            $('#LeaveYear_strEndDate').val('');
        }

    });

    function save() {

        if (fnValidate() == true) {

            $('#btnSave').trigger('click');

        }
        return false;
    }


    //    function save() 
    //    {
    ////        if (CheckValidData() == true) 
    ////        {
    //            if (fnValidateDateTime() == false) 
    //            {
    //                alert("Invalid Date.");
    //                return false;
    //            }

    //            var regx = /^[0-9]{4}$/;
    //            var regx2 = /^[0-9]{4}-[0-9]{4}$/;
    //            var year = $("#LeaveYear_strYearTitle").val();

    //            if (!regx.test(year) && !regx2.test(year)) {
    //                alert("Invalid Year Name.");
    //                return false;
    //            }

    //            if (fnValidate() == true) 
    //            {
    //                Id = $('#LeaveYear_intLeaveYearID').val();
    //                $('#btnSave').trigger('click');
    //            }
    ////        }
    //        return false;
    //    }

    function Delete() {
        Id = $('#LeaveYear_intLeaveYearID').val();

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intLeaveYearID: Id }, '/LMS/LeaveYear/Delete', 'divLeaveYearDetails');

        }
        return false;
    }



    function CheckValidData() {
        var url = '/LMS/LeaveYear/CheckValidData';
        var form = $('#frmLeaveYear');
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            if (result != "") {
                alert(result);
                return false;
            }
        }, "json");

        return true;
    }

    function allowOnlyNumbers(evt) {

        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;

        }

        else if (evt) keyCode = evt.which;
        else return true;
        if ((keyCode == null) || (keyCode == 0) || (keyCode == 8) || (keyCode == 9) || (keyCode == 13) || (keyCode == 27) || (keyCode == 46) || (keyCode == 45))
            return true;

        if (keyCode < 48 || keyCode > 57) {
            evt.returnValue = false;

        }

        return true;
    }

    function GetMonthDateName() {

        var url = "/LMS/LeaveYear/GetMonthDateName";

        var intLeaveYearTypeId = $("#LeaveYear_intLeaveYearTypeId").val();

        if (intLeaveYearTypeId != '') {

            //alert(intLeaveYearTypeId);

            $.getJSON(url, { intLeaveYearTypeId: intLeaveYearTypeId }, function (result) {

                $("#LeaveYear_startDateDay").val(result.startDateDay);
                $("#LeaveYear_startDateMonth").val(result.startDateMonth);

            });
        }
        else {
            $("#LeaveYear_startDateDay").val('');
            $("#LeaveYear_startDateMonth").val('');
            $("#LeaveYear_startDateYear").val('');
            //$("#LeaveYear_strEndDate").val('');
        }

        return false;
    }

    function CreateEndDate() {
        var startDateDay = $("#LeaveYear_startDateDay").val();
        var startDateMonth = $("#LeaveYear_startDateMonth").val();
        var startDateYear = $("#LeaveYear_startDateYear").val();

        //alert(startDateYear);

        if (startDateYear != "") {
            $.getJSON('/LMS/LeaveYear/CreateEndDate', { startDateDay: startDateDay, startDateMonth: startDateMonth, startDateYear: startDateYear }, function (result) {
                $("#LeaveYear_strEndDate").val(result.EndDate);
                $("#LeaveYear_strYearTitle").val(result.leaveYear);
            });
        }
        else {
            $("#LeaveYear_strEndDate").val("");
        }
        return false;
    }
    
</script>
<form id="frmLeaveYear" method="post" action="">
<div id="divLeaveYear">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.LeaveYear.intLeaveYearID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 55%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Leave Year Type<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveYear.intLeaveYearTypeId, Model.LeaveYearType, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return GetMonthDateName();" })%>
            </td>
        </tr>
        <tr>
            <td>
                Start Date
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveYear.startDateDay, new { @class = "textRegular required", @readonly = "readonly", @style = "width:50px; min-width:50px;" })%>-
                <%=Html.TextBoxFor(m => m.LeaveYear.startDateMonth, new { @class = "textRegular required", @readonly = "readonly", @style = "width:100px; min-width:100px;" })%>-
                <%=Html.TextBoxFor(m => m.LeaveYear.startDateYear, new { @class = "textRegular required", maxlength = 4, @style = "width:50px; min-width:50px;", onchange = "return CreateEndDate();" })%>
                <%--<% if (Model.IsExists == false)
                   {
                %>
                <%=Html.TextBoxFor(m => m.LeaveYear.strStartDate, new { @class = "textRegularDate required dtPicker date",maxlength = 10, onchange = "return CreateEndDate();" })%>
                <%}%>
                <% else
                   {
                %>
                <%=Html.TextBoxFor(m => m.LeaveYear.strStartDate, new { @class = "textRegularDate required date",maxlength = 10, @readonly = "readonly" })%>
                <%}%>--%>
            </td>
        </tr>
        <tr>
            <td>
                End Date
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveYear.strEndDate, new { @class = "textRegularDate required",maxlength = 10, @readonly = "readonly" })%>
            </td>
        </tr>
        <tr>
            <td>
                Leave Year<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveYear.strYearTitle, new { @class = "textRegularDate required", maxlength = 9, onkeypress = "return allowOnlyNumbers(event);" })%>
                <span style="color: gray; font-style: italic;">XXXX or XXXX-XXXX</span>
            </td>
        </tr>
         <tr>
            <td>
            </td>
            <td>
                <%=Html.CheckBoxFor(m => m.LeaveYear.bitIsActiveYear)%>Active Year
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
<%--<div class="divButton">
    <%if (Model.LeaveYear.intLeaveYearID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveYear, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return save();"></a>
    <%} %>
    <%}
      else
      {%>
    <a href="#" class="btnSave" onclick="return save();"></a>
    <a href="#" class="btnClose" onclick="return closeDialog();"></a>
    <%} %>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
</div>--%>
<div class="divButton">
    <%if (Model.LeaveYear.intLeaveYearID > 0)
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
    <%if (Model.LeaveYear.intLeaveYearID > 0)
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
