<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveAdjustmentList"));

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
        , showOn: 'button'
        , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
        , buttonImageOnly: true
        });

        $("#divStyleLeaveAdjustment").dialog({ autoOpen: false, modal: true, height: 800, width: 920, resizable: false, title: 'Leave Adjustment Application',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();

                var targetDiv = '#divDataList';
                var url = '/LMS/LeaveAdjustment/LeaveAdjustment?page=' + pg;
                var form = $('#frmLeaveAdjustmentList');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

            }
        });


        //$('#datepicker1').hide();
        //$('#datepicker').hide();
        $('#Model_strSearchApplyFrom').datepicker().datepicker('disable');
        $('#Model_strSearchApplyTo').datepicker().datepicker('disable');


        $("#Model_IsDateWiseSearch").live('click', function () {
            var IsSelect = $('#Model_IsDateWiseSearch').attr('checked');
            $('#Model_IsDateWiseSearch').val(IsSelect);

            var myDate = $('#StrCurrentDate').val();
            var dtFrom = myDate.split('-');
            dtFrom = '01-' + dtFrom[1] + '-' + dtFrom[2];

            if (IsSelect == true) {
                $('#Model_strSearchApplyFrom').val(dtFrom);
                $('#Model_strSearchApplyTo').val(myDate);
                $('#Model_strSearchApplyFrom').removeAttr('disabled');
                $('#Model_strSearchApplyTo').removeAttr('disabled');
                //$('#datepicker1').show();
                //$('#datepicker').show();
                $('#Model_strSearchApplyFrom').datepicker().datepicker('enable');
                $('#Model_strSearchApplyTo').datepicker().datepicker('enable');

            }
            else {
                DeselectApplyDate();
                //$('#datepicker1').hide();
                //$('#datepicker').hide();
                $('#Model_strSearchApplyFrom').datepicker().datepicker('disable');
                $('#Model_strSearchApplyTo').datepicker().datepicker('disable');

            }
        });

        DeselectApplyDate();
        FormatTextBox();

    });



    function DeselectApplyDate() {
        var IsSelect = $('#Model_IsDateWiseSearch').attr('checked');
        $('#Model_IsDateWiseSearch').val(IsSelect);

        if (IsSelect == false) {
            $('#Model_strSearchApplyFrom').val("");
            $('#Model_strSearchApplyTo').val("");
            $('#Model_strSearchApplyFrom').attr('disabled', 'disabled');
            $('#Model_strSearchApplyTo').attr('disabled', 'disabled');
        }
    }


    function deleteLeaveApplication(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intLeaveApplicationID: Id }, '/LMS/LeaveAdjustment/Delete', 'divLeaveApplicationList');
        }
        return false;
    }

    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveAdjustment/Details/' + Id;
        $('#styleOpenerLeaveAdjustment').attr({ src: url });
        $("#divStyleLeaveAdjustment").dialog('open');
        return false;
    }

    function popupStyleAdd() 
    {

//        if ($('#strUserEmail').val() == '') 
//        {
//            alert('You must have email address for oneline leave adjustment.');
//        }
//        else 
//        {
            var confResult = true;
            var host = window.location.host;
            var protocol = window.location.protocol;
            var url = '/LMS/LeaveAdjustment/IsAppliedForAdjustment';
            var form = $('#frmLeaveAdjustmentDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                if (result != null) 
                {
                    $.each(result, function (n) {

                        alert(result[n]);
                        confResult = false;
                        return false;

                    });
                }
                if (confResult != false) 
                {
                    var url = protocol + '//' + host + '/LMS/LeaveAdjustment/LeaveAdjustmentAdd';
                    $('#styleOpenerLeaveAdjustment').attr({ src: url });
                    $("#divStyleLeaveAdjustment").dialog('open');
                }
            }, "json");

//        }
        return false;
    }


    function parseDate(str) {
        var dmy = str.split('-');
        var mdy = dmy[1] + '/' + dmy[0] + '/' + dmy[2];
        return new Date(mdy);
    }

    function checkDateValidation() {
        if ($('#Model_strSearchApplyFrom').val() != "" && $('#Model_strSearchApplyTo').val() != "") {

            if (fnValidateDateTime() == false) {
                alert("Invalid Apply Date.");
                return false;
            }

            var pdtAPFrom = parseDate($('#Model_strSearchApplyFrom').val());
            var pdtAPTo = parseDate($('#Model_strSearchApplyTo').val());

            var pdtYStart = parseDate($('#Model_StrYearStartDate').val());
            var pdtYEnd = parseDate($('#Model_StrYearEndDate').val());



            if (pdtAPFrom > pdtAPTo) {
                alert("Apply From Date must be smaller than or equal to Apply To Date.");
                return false;
            }

            if (pdtYEnd < pdtAPFrom) {
                alert("Apply from date cannot be exceed current leave year.");
                return false;
            }
            if (pdtAPTo > pdtYEnd) {
                alert("Apply to date cannot be exceed current leave year.");
                return false;
            }

        }

        return true;
    }


    function searchData() {

        var pdtAPFrom = $('#Model_strSearchApplyFrom').val();
        var pdtAPTo = $('#Model_strSearchApplyTo').val();
        var hookup = true;

        if (pdtAPFrom != '' || pdtAPTo != '') {

            if (checkDateValidation() == true) {

                hookup = true;
            }
            else {
                hookup = false;
            }

        }

        if (hookup == true) {
            $('#Model_IsSearch').val(true);
            var targetDiv = "#divDataList";
            var url = "/LMS/LeaveAdjustment/LeaveAdjustment";
            var form = $("#frmLeaveAdjustmentList");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }

    /*remove the class which made red color of the drop down list */
    $(function () {
        $("select").each(function () {
            if ($(this).hasClass('input-validation-error')) {
                $(this).removeClass('input-validation-error');
            }
        })
    });   
</script>

<h3 class="page-title">Online Leave Adjustment</h3>

<form id="frmLeaveAdjustmentList" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                Applicant ID
            </td>
            <td>
                <%=Html.Hidden("Model.strSearchID", Model.strSearchID)%>
                <%=Html.Encode(Model.strSearchID)%>
            </td>
            <td>
                Name
            </td>
            <td>
                <%=Html.Hidden("Model.strSearchName", Model.strSearchName)%>
                <%=Html.Encode(Model.strSearchName)%>
            </td>
        </tr>
        <tr>
            <td>
                Leave Type
            </td>
            <td>
                <%= Html.DropDownList("Model.intSearchLeaveTypeId",Model.LeaveType, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td>
                Status
            </td>
            <td>
                <%= Html.DropDownList("Model.IntSearchApplicationStatusId", Model.ApplicationStatus, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>

            <td>
                <%=Html.Hidden("StrCurrentDate",DateTime.Today.ToString(LMS.Util.DateTimeFormat.Date))%>
                <%=Html.Hidden("Model.StrYearStartDate", Model.StrYearStartDate)%>
                <%=Html.Hidden("Model.StrYearEndDate", Model.StrYearEndDate)%>
                <%=Html.CheckBox("Model.IsDateWiseSearch", Model.IsDateWiseSearch)%>Apply From
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchApplyFrom", Model.StrSearchApplyFrom, new { @class = "textRegularDate dtPicker date", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                To
                <%=Html.TextBox("Model.strSearchApplyTo", Model.StrSearchApplyTo, new { @class = "textRegularDate dtPicker date", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>                
            </td>
            <td>
                Leave Year
            </td>
            <td>
                <%=Html.Hidden("Model.intSearchLeaveYearId", Model.intSearchLeaveYearId)%>
                <%=Html.Hidden("Model.StrSearchLeaveYear", Model.StrSearchLeaveYear)%>
                <%=Html.Hidden("Model.IsSearch", Model.IsSearch)%>
                <%=Html.Hidden("strUserEmail",LMS.Web.LoginInfo.Current.EmailAddress)%>

                <div style="float: left; text-align: left; margin-top:5px;">
                    <label><%=Model.StrSearchLeaveYear%></label>
                </div>
                <div style="float: right; text-align: right; padding-right: 23px;">
                    <a href="#" class="btnSearchData" onclick="return searchData();"></a>
                </div>
            </td>
        </tr>
        <%--<tr>
            <%=Html.Hidden("Model.IsSearch", Model.IsSearch)%>
            <%=Html.Hidden("strUserEmail",LMS.Web.LoginInfo.Current.EmailAddress)%>
            <td colspan="4" style="text-align: center">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>--%>
    </table>
</div>
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OnlineLeaveAdjustment, LMS.Web.Permission.MenuOperation.Add))
  {%>
<div>
    <a href="#" class="btnNewLeaveApplication" onclick="return popupStyleAdd();"></a>
</div>
<%} %>
<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
        <table>
            <thead>
                <tr>
                    <th style="width: 12%;">
                        Apply Date
                    </th>
                    <th>
                        Leave Type
                    </th>
                    <th>
                        Leave From
                    </th>
                    <th>
                        Leave To
                    </th>
                    <th>
                        Status
                    </th>
                    <th style="width: 12%;">
                        Date
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OnlineLeaveAdjustment, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th style="width: 3%;">                       
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% if (Model.LstLeaveApplication != null)
                   { %>
                <% foreach (LMSEntity.LeaveApplication obj in Model.LstLeaveApplication)
                   { 
                %>
                <tr>
                    <td style="width: 12%;">
                        <%=Html.Hidden(obj.intApplicationID.ToString())%>
                        <%=Html.Encode(obj.dtApplyDate.ToString(LMS.Util.DateTimeFormat.Date))%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strLeaveType)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.dtApplyFromDate.ToString(LMS.Util.DateTimeFormat.Date))%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.dtApplyToDate.ToString(LMS.Util.DateTimeFormat.Date))%>
                    </td>
                    <td>
                        <%=Html.Label(LMS.Web.Utils.GetApplicationStatus(obj.intAppStatusID))%>
                    </td>
                    <td style="width: 12%;">
                        <%=Html.Encode(obj.ApproveDateTime != null ? obj.ApproveDateTime.ToString(LMS.Util.DateTimeFormat.Date) : string.Empty)%>                  
                    </td>
                    <%if (isEditable)
                      { %>
                    <td style="width: 3%;">
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails( <%= obj.intApplicationID  %>);'>
                        </a>
                    </td>
                    <%} %>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(ViewData.Model.LstLeaveApplicationPaging.PageSize, ViewData.Model.LstLeaveApplicationPaging.PageNumber, ViewData.Model.numTotalRows, "frmLeaveAdjustmentList", "/LMS/LeaveAdjustment/LeaveAdjustment", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveApplicationPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString() %></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleLeaveAdjustment" style="border: 0px solid white; margin-right: 0px;
    padding-right: 0px;">
    <iframe id="styleOpenerLeaveAdjustment" src="" width="100%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
