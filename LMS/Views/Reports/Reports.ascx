
<%--View file location is ~/Reports/viewers/ReportLeaveInfo.aspx--%>
<%--<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ReportsModels>" %>
<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmReports"));
        setTitle("Leave Reports");

        $("#btnSave").hide();
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 430, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        $("#divReportView").dialog({ autoOpen: false, modal: true, height: 730, width: 800, resizable: false, title: 'Report', beforeclose: function (event, ui) { Closing(); } });
        
        OptionWisePageRefresh();

        //$(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#Model_Reports_IsWithoutPay").click(function () {
            var IsWOP = $("#Model_Reports_IsWithoutPay").attr('checked');
            $('#IsWithoutPay').val(IsWOP);
        });

        $("#Model_Reports_bitIsExcel").click(function () {
            var IsExcel = $("#Model_Reports_bitIsExcel").attr('checked');
            $('#bitIsExcel').val(IsExcel);
        });

        FormatTextBox();

    });

    function OptionWiseRefresh() {
        var IsAPD = $('#Model_Reports_IsApplyDate').attr('checked');
        $('#IsApplyDate').val(IsAPD);

    }
    
    function OptionWisePageRefresh() {

        var IsIndividual = $('#Model_Reports_IsIndividual').attr('checked');

        $('#IsIndividual').val(IsIndividual);

        if (IsIndividual == true) {
            $("#EmpStatus").val(0);
            $("#trStatus").hide();
            $('#StrDepartmentId').val("");
            $('#StrDepartmentId').attr('disabled', 'disabled');
            $('#StrDesignationId').val("");
            $('#StrDesignationId').attr('disabled', 'disabled');
            $('#StrLocationId').val("");
            $('#StrLocationId').attr('disabled', 'disabled');
            $('#StrGender').val("");
            $('#StrGender').attr('disabled', 'disabled');

            $('#IntCategoryId').val("");
            $('#IntCategoryId').attr('disabled', 'disabled');

            $('#StrEmpId').addClass("required");
            $('#StrEmpName').addClass("required");

            $('#StrEmpId').removeAttr('disabled');
            $('#StrEmpName').removeAttr('disabled');

            $('#btnElipse').css('visibility', 'visible');
            $('#lblIDReqMark').css('visibility', 'visible');
            $('#lblNameReqMark').css('visibility', 'visible');

        }
        else {
            $("#EmpStatus").val(0);
            $("#trStatus").show();
            $('#StrEmpId').val("");
            $('#StrEmpId').attr('disabled', 'disabled');
            $('#StrEmpName').val("");
            $('#StrEmpName').attr('disabled', 'disabled');

            $('#StrEmpId').removeClass("required");
            $('#StrEmpName').removeClass("required");

            $('#StrDepartmentId').removeAttr('disabled');
            $('#StrDesignationId').removeAttr('disabled');
            $('#StrLocationId').removeAttr('disabled');
            $('#StrGender').removeAttr('disabled');
            $('#IntCategoryId').removeAttr('disabled');

            $('#btnElipse').css('visibility', 'hidden');
            $('#lblIDReqMark').css('visibility', 'hidden');
            $('#lblNameReqMark').css('visibility', 'hidden');
        }

    }

    function searchEmployee() {

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

    
    function Closing() {

    }

    function setData(id, name) {


        document.getElementById('StrEmpId').value = id;
        document.getElementById('StrEmpName').value = name;

        document.getElementById('hdnEmpId').value = id;
        $("#lblIdNotFound").css('visibility', 'hidden');
        $('#strEmpID').removeClass("invalid");
    
        
        $("#divEmpList").dialog('close');

    }
    
    function hiddenDateDiv() {
        var rptName = $('#ReportId').val();

        $("#Model_Reports_IsWithoutPay").attr('checked', false);
        $('#IsWithoutPay').val(false);
        
        $("#Model_Reports_IsApplyDate").attr('checked', true);
        $('#IsApplyDate').val(true);

        if (rptName == "Leave Availed") {
            $('#lblFromDateReqMark').css('visibility', 'visible');
            $('#lblToDateReqMark').css('visibility', 'visible');
            $('#divDate').css('display', '');

            $('#StrFromDate').removeClass('dateNR');
            $('#StrFromDate').addClass('date');

            $('#StrToDate').removeClass('dateNR');
            $('#StrToDate').addClass('date');
            $('#divWithoutPay').css('visibility', 'visible');
        }
        else {
            $('#lblFromDateReqMark').css('visibility', 'hidden');
            $('#lblToDateReqMark').css('visibility', 'hidden');
            $('#divDate').css('display', 'none');

            $('#StrFromDate').removeClass('date');
            $('#StrFromDate').addClass("dateNR");

            $('#StrToDate').removeClass('date');
            $('#StrToDate').addClass("dateNR");
            $('#divWithoutPay').css('visibility', 'hidden');
        }

    }


    function reportPreview() 
    {
        var strHTML ;
        if (fnValidateDateTime() == false) 
        {
            alert("Invalid Date.");
            return false;
        }

        if (fnValidate() == true) 
        {

            var targetDiv = "#divDataList";
            var url = "/LMS/Reports/CheckHasData";
            var form = $("#frmReports");
            var serializedForm = form.serialize();


            $.post(url, serializedForm, function (result) {
                $('#IntDataCount').val(result);
                var intCount = $('#IntDataCount').val();


                if (intCount == -1) {
                    alert('From Date must be smaller than or equal to To Date.');
                }
                else if (intCount == -2) {
                    alert('From date must be within selected leave year.');
                }
                else if (intCount == -3) {
                    alert('To date must be within selected leave year.');
                }
                else {
                    var isExcel = $('#bitIsExcel').val();

                    if (isExcel.toString().toLowerCase() == "false") {                       
//                       var host = window.location.host;
//                       var url = 'http://' + host + '/LMS/Reports/ShowReport';
//                       $('#styleOpenerReportView').attr({ src: url });
//                       $('#divReportView').dialog('open');
                         
                        executeAction('frmReports', '/LMS/Reports/ShowReport', 'divReportView');
                        $('#divReportView').dialog('open');
                    }
                    else {
                        var ExportForm = document.getElementById("frmReports");
                        ExportForm.action = '/LMS/ExportToExcel/ExportToExcel';
                        ExportForm.submit();
                    }
                }

            }, "json");

        }
        
        return false;
    }


    function getLeaveYearInfo() {

        var targetDiv = "#divDataList";
        var url = "/LMS/Reports/GetLeaveYearInfo";
        var form = $("#frmReports");
        var serializedForm = form.serialize();

        var pintYearID = $('#IntLeaveYearId').val();

        if (pintYearID > 0) {
            $.post(url, serializedForm, function (result) {
                $('#StrYearStartDate').val(result[0]);
                $('#StrYearEndDate').val(result[1]);
                $('#StrFromDate').val(result[0]);
                $('#StrToDate').val(result[1]);
            }, "json");
        }
        else {
            $('#StrYearStartDate').val("");
            $('#StrYearEndDate').val("");
            $('#StrFromDate').val("");
            $('#StrToDate').val("");
        }

        return false;
    }


    function handleEnter(evt) {

        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;

        }

        else if (evt) keyCode = evt.which;
        else return true;

        $('#strEmpID').removeClass("invalid");

        if (keyCode == 13) {
            //handle enter
            var id = document.getElementById('strEmpID').value;
            var name = "";

            // getdata(id, name);
            var url = "/LMS/Employee/LookUpById/" + id;

            $.post(url, "", function (result) {
                if (result[0] != null && result[0] != "") {
                    document.getElementById('strEmpName').value = result[1];
                    document.getElementById('hdnEmpId').value = result[0];
                    $("#lblIdNotFound").css('visibility', 'hidden');
                }
                else {
                    document.getElementById('strEmpName').value = "";
                    document.getElementById('hdnEmpId').value = "";
                    $("#lblIdNotFound").css('visibility', 'visible');
                }
            }, "json");
            return true;
        }
        return true;
    }
            
</script>



<form id="frmReports" method="post" action="">
<div id="divRpt">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <%= Html.HiddenFor(m => m.IntDataCount)%>
        <table class="contenttext" style="width: 100%;">
            <tr>
                <td>
                    Report Name 123<label class="labelRequired">*</label>
                </td>
                <td>
                    <div style="float: left; text-align: left;">
                        <%= Html.DropDownListFor(m => m.ReportId, Model.ReportList, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return hiddenDateDiv();" })%>
                    </div>
                    <div id="divWithoutPay" style="float: left; text-align: left; padding-left: 8px; 
                        visibility: hidden;">
                        <%=Html.HiddenFor(m => m.IsWithoutPay)%>
                        <%=Html.CheckBox("Model_Reports_IsWithoutPay", Model.IsWithoutPay)%>Without Pay
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Leave Type
                </td>
                <td>
                    <%= Html.DropDownListFor(m => m.IntLeaveTypeId, Model.LeaveType, "...All...", new { @class = "selectBoxRegular" })%>
                </td>
            </tr>
            <tr>
                <td>
                    Leave Year<label class="labelRequired">*</label>
                </td>
                <td>
                    <%= Html.DropDownListFor(m => m.IntLeaveYearId, Model.LeaveYear, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return getLeaveYearInfo();" })%>
                </td>
            </tr>

            <tr id="trStatus">
                <td>
                    Status
                </td>
                <td>
                    <%= Html.DropDownListFor(m => m.EmpStatus,Model.EmployeeStatus , "...All...", new { @class = "selectBoxRegular"})%>
                </td>
            </tr>
        </table>
    </div>
    <div class="divSpacer">
    </div>
    <div id="divDate" style="display: none;" class="divRow">
        <table class="contenttext" style="width: 100%;">
            <tr>
                <td style="width: 36%;">
                    <%=Html.HiddenFor(m => m.IsApplyDate)%>
                    <div style="float: left; text-align: left;">
                        <%=Html.RadioButton("Model_Reports_IsApplyDate", true, Model.IsApplyDate, new { onClick = "OptionWiseRefresh();" })%>Apply
                        Date
                        <%=Html.RadioButton("Model_Reports_IsApplyDate", true, !Model.IsApplyDate, new { onClick = "OptionWiseRefresh();" })%>Leave
                        Date
                    </div>
                </td>
                <td colspan="3">
                    <div style="width: 100%; float: left; text-align: left;">
                        <%=Html.HiddenFor(m=> m.StrYearStartDate)%>
                        <%=Html.HiddenFor(m=> m.StrYearEndDate)%>
                        <div style="float: left; text-align: left;">
                            From Date<label id="lblFromDateReqMark" style="visibility: hidden" class="labelRequired">*</label>
                        </div>
                        <div style="float: left; text-align: left; padding-left: 8px;">
                            <%=Html.TextBoxFor(m => m.StrFromDate, new { @class = "textRegularDate dtPicker dateNR", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                            To Date<label id="lblToDateReqMark" style="visibility: hidden" class="labelRequired">*</label>
                            <%=Html.TextBoxFor(m => m.StrToDate, new { @class = "textRegularDate dtPicker dateNR", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 55%" />
            <col />
        </colgroup>
        <tr>
            <%=Html.HiddenFor(m => m.IsIndividual)%>
            <td style="width: 43%">
                <%=Html.RadioButton("Model_Reports_IsIndividual", true, Model.IsIndividual, new { onClick = "OptionWisePageRefresh();" })%>Individual
            </td>
            <td style="width: 57%">
                <%=Html.RadioButton("Model_Reports_IsIndividual", true, !Model.IsIndividual, new { onClick = "OptionWisePageRefresh();" })%>All
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td style="width:33%;">
                                        Applicant ID<label id="lblIDReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td >
                                     <input type="hidden"  id="hdnEmpId"/>
                                        <%=Html.TextBoxFor(m => m.StrEmpId, new { @class = "textRegularDate",@style = "width:80px; min-width:80px;", onkeypress = "return handleEnter(event);" })%>
                                        <a id="btnElipse" href="#" style="visibility: hidden" class="btnSearch" onclick="return searchEmployee();">
                                        </a>
                                        <label id="lblIdNotFound" style="text-align:right; visibility:hidden; vertical-align:5px; padding-left:5px; color:red;">Id not found !</label> 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:33%;">
                                        Name<label id="lblNameReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.StrEmpName, new { @class = "textRegular", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Department<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.StrDepartmentId, Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Designation<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m=>m.StrDesignationId,Model.Designation,"...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Branch<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m=>m.StrLocationId,Model.Location,"...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Category<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m=>m.IntCategoryId,Model.Category, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gender<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m=>m.StrGender,Model.Gender,"...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="text-align:left;">
        <%=Html.HiddenFor(m => m.bitIsExcel)%>
        <%=Html.CheckBox("Model_Reports_bitIsExcel", Model.bitIsExcel)%>Preview with MS Excel
    </div>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <a href="#" class="btnPreview" onclick="return reportPreview();"></a>
    <input id="btnSave" name="btnSave" type="submit" value="Save" visible="false" />
</div>
<div id="divMsgRpt" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
<div id="divReportView">
</div>
</form>
<%--<div id="divReportView">
    <iframe id="styleOpenerReportView" src="" width="100%" height="98%"
        style="border: 0px solid white; margin-right: 0px; padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>--%>
--%>