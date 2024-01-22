<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmSearchApplication"));
        hiddenAppvDiv();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });



        $('#Model_strSearchApplyFrom').datepicker().datepicker('disable');
        $('#Model_strSearchApplyTo').datepicker().datepicker('disable');
        var IsDateWiseSearch = $('#Model_IsDateWiseSearch').attr('checked');

        if(IsDateWiseSearch == true)
        {
            $('#Model_strSearchApplyFrom').removeAttr('disabled');
            $('#Model_strSearchApplyTo').removeAttr('disabled');
            $('#Model_strSearchApplyFrom').datepicker().datepicker('enable');
            $('#Model_strSearchApplyTo').datepicker().datepicker('enable');
        }

        $('#Model_IsDateWiseSearch').click(function () {
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

                $('#Model_strSearchApplyFrom').datepicker().datepicker('enable');
                $('#Model_strSearchApplyTo').datepicker().datepicker('enable');
            }
            else {
                DeselectApplyDate();
                $('#Model_strSearchApplyFrom').datepicker().datepicker('disable');
                $('#Model_strSearchApplyTo').datepicker().datepicker('disable');

            }
        });

        $("#Model_IsBulkApprove").click(function () {
            hiddenAppvDiv();
            searchData();
        });

        $("#divapro").dialog({ autoOpen: false, modal: true, height: 800, width: 960, resizable: false, title: 'Online Approval',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = "#divDataList";
                var url = '/LMS/SearchApplication/SearchApplication?page=' + pg;
                var form = $("#frmSearchApplication");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) {
                    $(targetDiv).html(result);
                }
                , "html");

            }
        });

        DeselectApplyDate();
        FormatTextBox();


    });


    function OpenApprovalFlow(id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/ApprovalFlow/Details?FID=' + id;
        $('#styleOpenerApprovalFlow').attr({ src: url });
        $("#divapro").dialog('open');
        return false;
    }

    function Closing() {

    }


    function parseDate(str) {
        var dmy = str.split('-');
        var mdy = dmy[1] + '/' + dmy[0] + '/' + dmy[2];
        return new Date(mdy);
    }

    function checkDateValidation() {
        if ($('#Model_strSearchApplyFrom').val() != "" && $('#Model_strSearchApplyTo').val() != "") {

            var pdtAPFrom = parseDate($('#Model_strSearchApplyFrom').val());
            var pdtAPTo = parseDate($('#Model_strSearchApplyTo').val());

            var pdtYStart = parseDate($('#Model_StrYearStartDate').val());
            var pdtYEnd = parseDate($('#Model_StrYearEndDate').val());

            if (pdtAPFrom > pdtAPTo) {
                alert("Apply From Date must be smaller than or equal to Apply To Date.");
                return false;
            }

            if (pdtYEnd < pdtAPFrom) {
                alert("Apply From Date cannot be exceed current leave year.");
                return false;
            }
            if (pdtAPTo > pdtYEnd) {
                alert("Apply To Date cannot be exceed current leave year.");
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

            if (fnValidateDateTime() && checkDateValidation() == true) {
                hookup = true;
            }
            else {
                hookup = false;
              //  alert("Invalid date");
            }
        }

        if (hookup == true) {
            $('#Model_IsSearch').val(true);
            var targetDiv = "#divDataList";
            var url = "/LMS/SearchApplication/SearchApplication";
            var form = $("#frmSearchApplication");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);
            }
            , "html");
        }

        return false;
    }

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

    function hiddenAppvDiv() {
        var isselect = $('#Model_IsBulkApprove').attr('checked');
        $('#Model_IsBulkApprove').val(isselect);

        if (isselect == true) {
            $('#divAppvBtn').css('display', '');
        }
        else {
            $('#divAppvBtn').css('display', 'none');
        }

    }


    function BulkApprove() {
        var targetDiv = "#divDataList";
        var url = '/LMS/SearchApplication/SaveApprovalFlow';
        var form = $("#frmSearchApplication");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            if (parseInt(result) == 0) {
                alert("Failure to bulk approve.");
            }
            else if (parseInt(result) == 1) {
                alert("Application approved successfully.");

                var pg = $('#txtPageNo').val();
                var targetDiv = "#divDataList";
                var url = '/LMS/SearchApplication/SearchApplication?page=' + pg;
                var form = $("#frmSearchApplication");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) {
                    $(targetDiv).html(result);
                }
                , "html");
            }
            else if (parseInt(result) == 2) {
                alert("Application not fount to bulk approve.");
            }
        }, "json");

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

<h3 class="page-title">Requested Leave Application</h3>

<form id="frmSearchApplication" method="post" action="">
<div id="divapro">
    <iframe id="styleOpenerApprovalFlow" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
               Applicant ID
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchInitial", Model.strSearchInitial, new { @class = "textRegular", @maxlength = 50 })%>
            </td>
            <td>
               Applicant Name
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchName", Model.strSearchName, new { @class = "textRegular", @maxlength = 100 })%>
            </td>
        </tr>
        <tr>
            <td>
                Designation
            </td>
            <td>
                <%= Html.DropDownList("Model.StrSearchDesignationId",Model.Designation, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td>
                Department
            </td>
            <td>
                <%= Html.DropDownList("Model.StrSearchDepartmentId", Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
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
                <%= Html.HiddenFor(m=> m.IsDateWiseSearch) %>
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchApplyFrom", Model.StrSearchApplyFrom, new { @class = "textRegularDate dtPicker date", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                To
                <%=Html.TextBox("Model.strSearchApplyTo", Model.StrSearchApplyTo, new { @class = "textRegularDate dtPicker date", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>           
            </td>
            <td>
               <%-- Leave Year--%>
            </td>
            <td>
                <%--<%=Html.Hidden("Model.intSearchLeaveYearId", Model.intSearchLeaveYearId)%>
                <%=Html.Hidden("Model.StrSearchLeaveYear", Model.StrSearchLeaveYear)%>--%>

                <%=Html.Hidden("Model.IsSearch", Model.IsSearch)%>
                <%=Html.Hidden("Model.IsCanBulkApprove", Model.IsCanBulkApprove)%>
                <%=Html.HiddenFor(m=> m.IsBulkApprove)%>
                
                <div style="float: left; text-align: left; margin-top:5px;">
                   <%-- <label><%=Model.StrSearchLeaveYear%></label>--%>
                </div>
                <div style="float: right; text-align: right; padding-right: 23px;">
                    <a href="#" class="btnSearchData" onclick="return searchData();"></a>
                </div>    
            </td>
        </tr>
        <%--<tr>            
            <td colspan="4" style="text-align: center">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>--%>
    </table>
</div>

<div id="divList">
    <%if (Model.IsCanBulkApprove == true)
      { %>
    <table style="width: 100%;">
        <tr>
            <td>
                <div style="float: left; text-align: left;">
                    <%=Html.CheckBox("Model.IsBulkApprove", Model.IsBulkApprove)%>Bulk Approve
                </div>
                <div id="divAppvBtn" style="float: left; text-align: left; padding-left: 8px; display: none;">
                    <a href="#" class="btnApprove" onclick="return BulkApprove();"></a>
                </div>
            </td>
        </tr>
    </table>
    <%} %>
    <div id="grid">
        <div id="grid-data" style="overflow: auto; width: 99%;">
            <table style="width: 99%;">
                <thead>
                    <tr>
                        <%if (Model.IsBulkApprove == true)
                          { %>
                        <th style="width: 1%;">
                        </th>
                        <th style="width: 1%;">
                        </th>
                        <th style="width: 14%; padding-left: 35px;">
                            Apply Date
                        </th>
                        <th style="width: 20%;">
                            Applicant
                        </th>
                        <th>
                            Leave Type
                        </th>
                        <th style="width: 14%;">
                            Leave From
                        </th>
                        <th style="width: 14%;">
                            Leave To
                        </th>
                        <%--<th style="width: 1%;">
                            Adj.
                        </th>--%>
                        <th style="width: 15%;">
                            Status
                        </th>
                        <%}
                          else
                          {%>
                        <th style="width: 5%;">
                        </th>
                        <th style="width: 14%;">
                            Apply Date
                        </th>
                        <th style="width: 20%;">
                            Applicant
                        </th>
                        <th>
                            Leave Type
                        </th>
                        <th style="width: 14%;">
                            Leave From
                        </th>
                        <th style="width: 14%;">
                            Leave To
                        </th>
                        <%--<th style="width: 1%;">
                            Adj.
                        </th>--%>
                        <th style="width: 15%;">
                            Status
                        </th>
                        <%} %>
                    </tr>
                </thead>
            </table>
            <div style="overflow-y: auto; overflow-x: hidden; max-height: 230px;">
                <table style="width: 99%;">
                    <%if (Model.IsBulkApprove == false)
                      {%>
                    <% foreach (LMSEntity.LeaveApplication obj in Model.LstLeaveApplication)
                       { 
                    %>
                    <tr>
                        <td style="text-align: center; width: 5%;">
                           <a href="#" class="gridEdit" onclick="return OpenApprovalFlow('<%=obj.intApplicationID%>');">  <%--<a href="#" class="gridEdit" onclick="return OpenApprovalFlow('<%=obj.intAppFlowID%>');">--%>
                            </a>
                        </td>
                        <td style="width: 14%;">
                            <%=Html.Hidden(obj.intAppFlowID.ToString())%>
                            <%=Html.Hidden(obj.intApplicationID.ToString())%>
                            <%=Html.Label(obj.strApplyDate)%>
                        </td>
                        <td style="width: 20%;">
                            <%=Html.Encode(obj.strEmpInitial.ToString() + " - " + obj.strEmpName.ToString() + ", " + obj.strDesignation.ToString())%>
                             
                        </td>
                        <td >
                            <%=Html.Encode(obj.strLeaveType)%>
                        </td>
                        <td style="width: 14%;">
                            <%=Html.Encode(obj.strApplyFromDate)%>
                        </td>
                        <td style="width: 14%;">
                            <%=Html.Encode(obj.strApplyToDate)%>
                        </td>

                        <%=Html.Hidden(obj.bitIsAdjustment.ToString())%>
                        <%--<td style="width: 1%;">
                            <%=Html.Hidden(obj.bitIsAdjustment.ToString())%>
                            <%if (obj.bitIsAdjustment == true)
                              { %>
                            <%=Html.Encode("Yes")%>
                            <%}
                              else
                              { %>
                            <%=Html.Encode("No")%>
                            <%} %>
                        </td>--%>
                        <td style="width: 15%;">
                            <%=Html.Label(LMS.Web.Utils.GetApplicationStatus(obj.intAppStatusID))%>
                        </td>
                    </tr>
                    <%} %>
                    <%}
                      else
                      { %>
                    <%int j = 0; for (int i = 0; i < Model.LstLeaveApplication.Count; i++)
                      {
                          j = j + 1;  
                    %>
                    <tr>
                        <td style="text-align: center; width: 1%;">
                            <a href="#" class="gridEdit" onclick="return OpenApprovalFlow('<%=Model.LstLeaveApplication[i].intApplicationID%>');">
                            </a>
                        </td>
                        <td style="width: 1%;">
                            <%=Html.CheckBox("Model.LstLeaveApplication[" + i.ToString() + "].IsChecked", Model.LstLeaveApplication[i].IsChecked)%>
                        </td>
                        <td style="width: 14%;">
                            <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].intAppFlowID", Model.LstLeaveApplication[i].intAppFlowID)%>
                            <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].intApplicationID", Model.LstLeaveApplication[i].intApplicationID)%>
                            <%=Html.Encode(Model.LstLeaveApplication[i].strApplyDate)%>
                        </td>
                        <td style="width: 20%;">
                            <%--<%=Html.Encode(obj.strEmpID.ToString() + '-' + obj.strEmpName.ToString())%>--%>
                            <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].strEmpID", Model.LstLeaveApplication[i].strEmpID)%>
                            <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].strEmpName", Model.LstLeaveApplication[i].strEmpName)%>
                            <%=Html.Encode(Model.LstLeaveApplication[i].strEmpInitial.ToString() + " - " + Model.LstLeaveApplication[i].strEmpName.ToString() + ", " + Model.LstLeaveApplication[i].strDesignation.ToString())%>
                        </td>
                        <td >
                            <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].strLeaveType", Model.LstLeaveApplication[i].strLeaveType)%>
                            <%=Html.Encode(Model.LstLeaveApplication[i].strLeaveType)%>
                        </td>
                        <td style="width: 14%;">
                            <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].strApplyFromDate", Model.LstLeaveApplication[i].strApplyFromDate)%>
                            <%=Html.Encode(Model.LstLeaveApplication[i].strApplyFromDate)%>
                        </td>
                        <td style="width: 14%;">
                            <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].strApplyToDate", Model.LstLeaveApplication[i].strApplyToDate)%>
                            <%=Html.Encode(Model.LstLeaveApplication[i].strApplyToDate)%>
                        </td>

                         <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].bitIsAdjustment", Model.LstLeaveApplication[i].bitIsAdjustment)%>
                        <%--<td style="width: 1%;">
                            <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].bitIsAdjustment", Model.LstLeaveApplication[i].bitIsAdjustment)%>
                            <%if (Model.LstLeaveApplication[i].bitIsAdjustment == true)
                              { %>
                            <%=Html.Encode("Yes")%>
                            <%}
                              else
                              { %>
                            <%=Html.Encode("No")%>
                            <%} %>
                        </td>--%>
                        <td style="width: 15%;">
                            <%=Html.Encode(LMS.Web.Utils.GetApplicationStatus(Model.LstLeaveApplication[i].intAppStatusID))%>
                        </td>
                    </tr>
                    <%} %>
                    <%} %>
                </table>
            </div>
        </div>
        <div class="pager">
            <%= Html.PagerWithScript(ViewData.Model.LstLeaveApplicationPaging.PageSize, ViewData.Model.LstLeaveApplicationPaging.PageNumber, ViewData.Model.numTotalRows, "frmSearchApplication", "/LMS/SearchApplication/SearchApplication", "divDataList")%>
            <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveApplicationPaging.PageNumber)%>
        </div>
        <label id="lblTotalRows">
            Total Records:<%=Model.numTotalRows.ToString() %></label>
    </div>
</div>
<div class="spacer">
</div>
</form>
