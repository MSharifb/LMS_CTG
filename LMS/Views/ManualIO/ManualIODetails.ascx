<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ManualIOModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

//        $("#datepicker").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#ManualIO_strAttendDate').datepicker('show'); });


        preventSubmitOnEnter($("#frmManualIODetails"));

        setTitle("Manual In / Out");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 735, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy'
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#ManualIO_strStartTime").timepicker({ ampm: true });
        $("#ManualIO_strEndTime").timepicker({ ampm: true });

        $("#ManualIO_strAttenTime").timepicker({ ampm: true });

        $('#ManualIO_RandomValue').attr('readonly', true);

        $('#ManualIO_intShiftID').removeClass('required');

        $('#ManualIO_intShiftID').attr('disabled', true);



        //OptionWisePageRefresh();

        FormatTextBox();

    });


    function disableRadioBtn() {

        $('#Model_ManualIO_IsSingleEmp').attr('disabled', true);
        $('#Model_ManualIO_IsShift').attr('disabled', true);

    }

    /*function save() {

    if (fnValidate() == true) {

    $('#btnSave').trigger('click');

    }
    return false;
    }*/


    function save() {

        if (fnValidate() == true) {

            var targetDiv = "#divManualIODetails";
            var url = "/LMS/ManualIO/Details";
            var form = $("#frmManualIODetails");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);

            }, "html");

        }

        return false;

    }


    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divManualIODetails';
            var url = '/LMS/ManualIO/Delete';
            var form = $('#frmManualIODetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }

    function Closing() {
        //window.location = "/LMS/LeaveType";
    }


    function openEmployee() {


        var url = '/LMS/Employee/EmployeeList';
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'text',
            timeout: 5000,
            error: function () {
                alert('System is unable to load data please try again.');
            },
            success: function (result) {
                $('#divEmpList').html(result);
            }
        });

        $("#divEmpList").dialog('open');
        return false;
    }



    function setData(id, name) {
        document.getElementById('ManualIO_strEmpName').value = name;
        document.getElementById('ManualIO_strEmpID').value = id;
        GetInfo(id);
        $("#divEmpList").dialog('close');
    }

    function searchEmployee() {
        window.parent.openEmployee();
    }

    /*updateFields = function (data) {
     
    $('#ManualIO_strDesignation').val(data.strDesignation);
                
    $('#CardAssign_strDepartment').val(data.strDepartment);
    };*/

    function GetInfo(id) {
        var form = $("#frmManualIO");
        var serializedForm = form.serialize();

        //$.getJSON("getEmployeeInformation", serializedForm, updateFields);

        var url = "/LMS/ManualIO/getEmployeeInformation";
        var form = $("#frmManualIODetails");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            //alert(result[0]);
            $("#ManualIO_strDesignation").val(result[0]);
            $("#ManualIO_strCardID").val(result[1]);

        }, "json");


    }




    function SetShiftInOutTime() {

        var url = "/LMS/ManualIO/GetShiftInOutTime";
        var form = $("#frmManualIODetails");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {

            //alert(result);
            //alert(result[0]);
            //alert(result[1]);

            $("#ManualIO_strInTime").val(result[0]);
            $("#ManualIO_strOutTime").val(result[1]);
            $("#ManualIO_strHalfTime").val(result[2]);

        }, "json");


    }


    function IsEmpOrShift(e) {


        var strApplicationType = $(e).val();
        //alert(strApplicationType);

        if (strApplicationType == "Shift") {

            //alert('Shift');
            //$('#divWorkingTime').css('visibility', 'hidden');
            $('#ManualIO_strEmpID').removeClass('required');
            $('#btnSearch').css('visibility', 'hidden');
            $('#ManualIO_intShiftID').addClass('required');

            $('#ManualIO_strEmpID').val('');
            $('#ManualIO_strEmpName').val('');
            $('#ManualIO_strDesignation').val('');

            $('#ManualIO_RandomValue').removeAttr('readonly');

            $('#ManualIO_intShiftID').attr('disabled', false);

            $('#Model_ManualIO_IsSingleEmp').attr('checked', false);

        }
        else {
            //alert('Emp');

            $('#Model_ManualIO_IsShift').attr('checked', false);

            $('#ManualIO_RandomValue').attr('readonly', true);

            $('#ManualIO_intShiftID').attr('selectedIndex', 0);

            $('#ManualIO_intShiftID').attr('disabled', true);

            $('#btnSearch').css('visibility', 'visible');
            $('#ManualIO_strEmpID').addClass('required');
            $('#ManualIO_intShiftID').removeClass('required');

            $('#ManualIO_strInTime').val('');
            $('#ManualIO_strOutTime').val('');
            $('#ManualIO_strHalfTime').val('');
        }

        return false;

    }



    
</script>
<form id="frmManualIODetails" method="post" action="">
<div id="divManualIO">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.ManualIO.intRowID , Model.ManualIO.intRowID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 45%" />
            <col />
        </colgroup>
        <tr>
            <td>
                <%if (Model.ManualIO.intRowID < 1)
                  { %>
                <%=Html.RadioButton("Model_ManualIO_IsSingleEmp", "SingleEmp", Model.ManualIO.IsSingleEmp, new { onclick = "IsEmpOrShift(this);" })%>Apply
                to Single Employee
                <%}%>
            </td>
            <td>
                <%if (Model.ManualIO.intRowID < 1)
                  { %>
                <%=Html.RadioButton("Model_ManualIO_IsShift", "Shift", Model.ManualIO.IsShift, new { onclick = "IsEmpOrShift(this); " })%>Apply
                to Single Shift
                <%}%>
            </td>
        </tr>
        <tr>
            <td>
                <table class="contenttext" style="width: 100%;">
                    <tr>
                        <td style="width: 110px">
                            Employee ID
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td>
                            <div id="dvEmpID">
                                <%--<%= Html.TextBoxFor(m => m.CardAssign.strEmpID)%>--%>
                            </div>
                            <%= Html.TextBoxFor(m => m.ManualIO.strEmpID, new { @class = "textRegularDate required", @readonly = "true" })%>
                            <% if (Model.ManualIO.intRowID == 0) %>
                            <%{ %>
                            <a href="#" class="btnSearch" id="btnSearch" onclick="return openEmployee();"></a>
                            <%}%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name<label class="labelRequired"></label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.ManualIO.strEmpName, new { @class = "textRegular", @readonly = "true" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Designation<label class="labelRequired"></label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.ManualIO.strDesignation, new { @class = "textRegular", @readonly = "true" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Card No.<label class="labelRequired"></label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.ManualIO.strCardID, new { @class = "textRegularDate", @readonly = "true" })%>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="contenttext" style="width: 100%;">
                    <tr>
                        <td style="width: 100px">
                            Shift Name<label class="labelRequired"></label>
                        </td>
                        <td colspan="3">
                            <%= Html.DropDownListFor(m => m.ManualIO.intShiftID, Model.Shift, "...Select One..", new { @class = "selectBoxRegular required", @disabled = "true", @Style = "width:130px;", @onchange = "return SetShiftInOutTime();" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Shift In:<label class="labelRequired"></label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.ManualIO.strInTime, new { @class = "textRegularDate", @readonly = "true" })%>
                        </td>
                        <td>
                            Shift Out:<label class="labelRequired"></label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.ManualIO.strOutTime, new { @class = "textRegularDate", @readonly = "true" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Half Time:<label class="labelRequired"></label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.ManualIO.strHalfTime, new { @class = "textRegularDate", @readonly = "true" })%>
                        </td>
                        <td style="width: 130px">
                            Random Value:<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.ManualIO.RandomValue, new { @class = "textRegularDate", @readonly = "true" })%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="height: 10px">
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 45%" />
            <col />
        </colgroup>
        <tr>
            <td style="width: 116px">
                Atten. Date<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.ManualIO.strAttendDate , new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td style="width: 80px; vertical-align: top;" rowspan="2">
                Reason<label class="labelRequired" style="vertical-align: top">
                    *</label>
            </td>
            <td rowspan="2" style="vertical-align: top">
                <%=Html.TextAreaFor(m => m.ManualIO.strReason, new { @class = "textRegular required", @style = "width:275px;height:65px", maxlength = 500, onkeyup = "return ismaxlengthPop(this)" })%>
            </td>
        </tr>
        <tr>
            <td style="width: 116px">
                Atten. Time:<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.ManualIO.strAttenTime, new { @class = "textRegularDate required" })%>
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
    <%if (Model.ManualIO.intRowID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ManualIO, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ManualIO, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.ManualIO.intRowID > 0)
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
