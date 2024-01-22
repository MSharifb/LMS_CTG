<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveYearTypeModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveTypeDetails"));

        setTitle("Leave Type");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        //OptionWisePageRefresh();

        FormatTextBox();

    });

    function GetEndMonthAndYearTypeName() {

        var url = "/LMS/LeaveYearType/GetMonthAndName";

        var StartMonth = $("#LeaveYearType_StartMonth").val();

        if (StartMonth != '') {
            $.getJSON(url, { startMonth: StartMonth }, function (result) {
                $("#LeaveYearType_EndMonth").val(result.startMonth);
                $("#LeaveYearType_LeaveYearTypeName").val(result.yearType);

            });
        }
        else {
            $("#LeaveYearType_EndMonth").val('');
            $("#LeaveYearType_LeaveYearTypeName").val('');
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
            var url = '/LMS/LeaveYearType/Delete';
            var form = $('#frmLeaveTypeDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }

//    function PreventHyphen(evt) 
//    {
//        var keyCode = "";
//        if (window.event) 
//        {
//            keyCode = window.event.keyCode;
//            evt = window.event;
//        }

//        else if (evt) keyCode = evt.which;
//        else return true;
//        if (keyCode == 45)
//            return false;      

//        return true;
//    } 

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
            <%= Html.HiddenFor(m => m.LeaveYearType.intLeaveYearTypeId)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 55%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Start Month<label class="labelRequired">*</label>
            </td>
            <td>
            <%= Html.DropDownListFor(m => m.LeaveYearType.StartMonth, Model.MonthList, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return GetEndMonthAndYearTypeName();" })%>
            
            <%--<%= Html.DropDownListFor(m => m.LeaveYearType.StartMonth, Model.s, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return GetEntitlement();" })%>--%>
            <%-- <%=Html.TextBoxFor(m => m.LeaveType.strLeaveType, new  {@class="textRegular required", maxlength=50 })%>--%>
            </td>
        </tr>
        <tr>
            <td>
                End Month<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveYearType.EndMonth, new { @class = "textRegular required", @readonly = "readonly" })%>
            </td>
        </tr>

         <tr>
            <td>
                Leave Year Type<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveYearType.LeaveYearTypeName, new { @class = "textRegular required", @readonly = "readonly" })%>
            </td>
        </tr>
   
       <%-- <tr>
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
        </tr>--%>
    </table>
    
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.LeaveYearType.intLeaveYearTypeId > 0)
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
    <%if (Model.LeaveYearType.intLeaveYearTypeId > 0)
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
