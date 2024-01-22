<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.AttendanceReportModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

//        $("#datepicker").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#StrFromDate').datepicker('show'); });


//        $("#datepicker1").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#StrToDate').datepicker('show'); });


        preventSubmitOnEnter($("#frmReports"));
        setTitle("Leave Reports");

        $("#btnSave").hide();
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 430, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        $("#divReportView").dialog({ autoOpen: false, modal: true, height: 700, width: 950, resizable: false, title: 'Report View', beforeclose: function (event, ui) { Closing(); } });
        OptionWisePageRefresh();

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

    function ReportTypeWiseRefresh() {
        var rptType = $('#strReportType').val();
       
        if (rptType == 'EJC' || rptType == 'EWC' || rptType == 'OOC') {
            //$('#Model_Reports_IsIndividual').val('True');
            $('.rpt1').attr('checked', true);
            $('.rpt2').attr('disabled', true);
            OptionWisePageRefresh()
        }
        else $('.rpt2').attr('disabled', false);
    }

    function OptionWisePageRefresh() {

        var IsIndividual = $('#Model_Reports_IsIndividual').attr('checked');

        $('#IsIndividual').val(IsIndividual);

        if (IsIndividual == true) {

            $('#StrDepartmentId').val("");
            $('#StrDepartmentId').attr('disabled', 'disabled');
            $('#StrDesignationId').val("");
            $('#StrDesignationId').attr('disabled', 'disabled');
            $('#StrLocationId').val("");
            $('#StrLocationId').attr('disabled', 'disabled');
            $('#StrCompanyID').val("");
            $('#StrCompanyID').attr('disabled', 'disabled');

            $('#intCategoryCode').val("");
            $('#intCategoryCode').attr('disabled', 'disabled');

            $('#StrEmpId').addClass("required");
            $('#StrEmpName').addClass("required");

            $('#StrEmpId').removeAttr('disabled');
            $('#StrEmpName').removeAttr('disabled');

            $('#btnElipse').css('visibility', 'visible');
            $('#lblIDReqMark').css('visibility', 'visible');
            $('#lblNameReqMark').css('visibility', 'visible');

        }
        else {

            $('#StrEmpId').val("");
            $('#StrEmpId').attr('disabled', 'disabled');
            $('#StrEmpName').val("");
            $('#StrEmpName').attr('disabled', 'disabled');

            $('#StrEmpId').removeClass("required");
            $('#StrEmpName').removeClass("required");

            $('#StrCompanyID').removeAttr('disabled');
            $('#StrDepartmentId').removeAttr('disabled');
            $('#StrDesignationId').removeAttr('disabled');
            $('#StrLocationId').removeAttr('disabled');
            $('#intCategoryCode').removeAttr('disabled');

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

        $("#divEmpList").dialog('close');

    }


    function reportPreview() {
        var strHTML;
//        if (fnValidateDateTime() == false) {
//            alert("Invalid Date.");
//            return false;
//        }

        if (fnValidate() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/AttendanceReport/CheckHasData";
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
                        executeAction('frmReports', '/LMS/AttendanceReport/ShowReport', 'divReportView');
                        $('#divReportView').dialog('open');
                    }
                    
               

            }, "json");

        }

        return false;
    }



    function getReport(strRpt) {

        var id = strRpt.toString();
        //var id = "xyz";

        var ExportForm = document.createElement("FORM");
        document.body.appendChild(ExportForm);
        ExportForm.method = "POST";

        var newElement = document.createElement("input");
        newElement.setAttribute("id", "exportvalue");
        newElement.setAttribute("name", "exportvalue");
        newElement.setAttribute("type", "hidden");

        ExportForm.appendChild(newElement);
        //newElement.value = strRpt;

        //alert(newElement.value);

        ExportForm.action = '/LMS/ExportToExcel/ExportToExcel/' + id;
        ExportForm.submit();

        return false;
    }

    function closeDialog() {
        var len = $(".ui-icon-closethick").length;
        $(".ui-icon-closethick").each(function (i) {
            if (i == len - 1) {
                $(this).click();
            }
        });       
        return false;
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
                    Report Name<label class="labelRequired">*</label>
                </td>
                <td>                    
                     <%= Html.DropDownListFor(m => m.strReportType, Model.ReportList, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:260px;", onChange="ReportTypeWiseRefresh();"})%>
                   
                </td>                
            </tr>
             <tr>
                <td>
                    From Date<label id="lblFromDateReqMark" class="labelRequired">*</label>
                    </td>
                    <td>
                        <%=Html.TextBoxFor(m => m.StrFromDate, new { @class = "textRegularDate dtPicker date", @maxlength = 10 })%>
                        <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
                        To <label id="lblToDateReqMark" class="labelRequired">*</label>
                        <%=Html.TextBoxFor(m => m.StrToDate, new { @class = "textRegularDate dtPicker date", @maxlength = 10 })%>
                        <%--<img alt="" id="datepicker1" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
                    </td>
           
            </tr>
        </table>
    </div>
    <div class="divSpacer">
    </div>
    <div id="divDate" class="divRow">
        <table class="contenttext" style="width: 100%;">
            <tr>
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
                <%=Html.RadioButton("Model_Reports_IsIndividual", true, Model.IsIndividual, new { onClick = "OptionWisePageRefresh();", @class="rpt1" })%>Individual
            </td>
            <td style="width: 57%">
                <%=Html.RadioButton("Model_Reports_IsIndividual", true, !Model.IsIndividual, new { onClick = "OptionWisePageRefresh();", @class="rpt2" })%>All
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        ID<label id="lblIDReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.StrEmpId, new { @class = "textRegularDate", @readonly = "readonly" })%>
                                        <a id="btnElipse" href="#" style="visibility: hidden" class="btnSearch" onclick="return searchEmployee();">
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                        Company Name<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.StrCompanyID, Model.Company, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
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
                                        Location<label class="labelRegular"></label>
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
                                        <%= Html.DropDownListFor(m=>m.intCategoryCode,Model.Category, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--<div style="text-align:left;">
        <%=Html.HiddenFor(m => m.bitIsExcel)%>
        <%=Html.CheckBox("Model_Reports_bitIsExcel", Model.bitIsExcel)%>Preview with MS Excel
    </div>--%>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <a href="#" class="btnPreview" onclick="return reportPreview();"></a>
    <%--<input id="btnSave" name="btnSave" type="submit" value="Save" visible="false" />--%>
</div>
<div id="divMsgRpt" class="divMsg" style="display:none">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
<div id="divReportView">
</div>
</form>
