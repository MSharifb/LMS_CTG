<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ReportsModels>" %>
<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmMyLeaveStatus"));
        setTitle("Leave Reports");

        $("#btnSave").hide();

        $("#divReportView").dialog({ autoOpen: false, modal: true, height: 730, width: 800, resizable: false, title: 'Report', beforeclose: function (event, ui) { Closing(); } });

        $("#Model_Reports_IsIndividual").live('click', function () {

            var IsIndividual = $('#Model_Reports_IsIndividual').attr('checked');

            $('#Model_IsIndividual').val(IsIndividual);

        });
        $("#Model_Reports_bitIsExcel").click(function () {
            var IsExcel = $("#Model_Reports_bitIsExcel").attr('checked');
            $('#bitIsExcel').val(IsExcel);
        });

    });


    function Closing() {

    }

    function reportPreview() {
        var isExcel = $('#bitIsExcel').val();

       // alert(isExcel);
            if (isExcel.toString().toLowerCase() == "false") {
                executeAction('frmMyLeaveStatus', '/LMS/Reports/ShowReport', 'divReportView');
                $('#divReportView').dialog('open');
            }
            else {
                var ExportForm = document.getElementById("frmMyLeaveStatus");
                ExportForm.action = '/LMS/ExportToExcel/ExportToExcel';
                ExportForm.submit();
            }




       // }
        return false;
    }
    
</script>
<form id="frmMyLeaveStatus" method="post" action="">
<div id="divRpt">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <%= Html.HiddenFor(m => m.IntDataCount)%>
        <%= Html.Hidden("Model.IsIndividual", Model.IsIndividual)%>
        <%= Html.Hidden("Model.ReportId", Model.ReportId)%>
        <%= Html.Hidden("Model.IsFromMyLeaveMenu", Model.IsFromMyLeaveMenu)%>
        <table class="contenttext" style="width: 100%;">
            <tr>
                <td>
                    Applicant ID
                </td>
                <td>
                    <%=Html.Hidden("Model.StrEmpId", Model.StrEmpId)%>
                    <%=Html.Encode(Model.StrEmpId)%>
                </td>
            </tr>
            <tr>
                <td>
                    Name
                </td>
                <td>
                    <%=Html.Hidden("Model.StrEmpName", Model.StrEmpName)%>
                    <%=Html.Encode(Model.StrEmpName)%>
                </td>
            </tr>
            <tr>
                <td>
                    Leave Year<label class="labelRequired">*</label>
                </td>
                <td>
                    <%= Html.DropDownList("Model.IntLeaveYearId", Model.LeaveYear, "...Select One...", new { @class = "selectBoxRegular required" })%>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <%=Html.RadioButton("Model_Reports_IsIndividual", true, Model.IsIndividual)%>My
                    Status
                    <%=Html.RadioButton("Model_Reports_IsIndividual", true, !Model.IsIndividual)%>Subordinate
                    Status
                </td>
            </tr>
        </table>
        <div style="text-align:left;">
        <%=Html.HiddenFor(m => m.bitIsExcel)%>
        <%=Html.CheckBox("Model_Reports_bitIsExcel", Model.bitIsExcel)%>Preview with MS Excel
    </div>
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
