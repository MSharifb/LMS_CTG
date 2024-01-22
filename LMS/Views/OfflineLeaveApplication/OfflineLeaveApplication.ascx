<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmOfflineLeaveApplicationList"));

        $("#divStyleOfflineLeaveApplication").dialog({ autoOpen: false, modal: true, height: 800, width: 920, resizable: false, title: 'Offline Leave Application',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = "#divDataList";
                var url = '/LMS/OfflineLeaveApplication/OfflineLeaveApplication?page=' + pg;
                var form = $("#frmOfflineLeaveApplicationList");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });


        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true  

        });

    });


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
                alert("Apply From Date must be equal or smaller than Apply To Date.");
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


    function deleteLeaveApplication(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {


            executeCustomAction({ intLeaveApplicationID: Id }, '/LMS/OfflineLeaveApplication/Delete', 'divOfflineLeaveApplicationList');

        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/OfflineLeaveApplication/Details/' + Id;
        $('#styleOpenerOfflineLeaveApplication').attr({ src: url });
        $("#divStyleOfflineLeaveApplication").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/OfflineLeaveApplication/OfflineLeaveApplicationAdd';
        $('#styleOpenerOfflineLeaveApplication').attr({ src: url });
        $("#divStyleOfflineLeaveApplication").dialog('open');
        return false;
    }

    function searchData() {

        if (checkDateValidation() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/OfflineLeaveApplication/OfflineLeaveApplication";
            var form = $("#frmOfflineLeaveApplicationList");
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

<h3 class="page-title">Offline Leave Application</h3>

<form id="frmOfflineLeaveApplicationList" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                ID
            </td>
            <td>
                <%--<%= Html.TextBox("Model.strSearchID", Model.strSearchID, new { @class = "textRegular", @maxlength = 50 })%>--%>
                <%= Html.TextBox("Model.strSearchInitial", Model.strSearchInitial, new { @class = "textRegular", @maxlength = 50 })%>
            </td>
            <td>
                Name
            </td>
            <td>
                <%= Html.TextBox("Model.strSearchName", Model.strSearchName, new { @class = "textRegular", @maxlength = 100 })%>
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
                Apply From
            </td>
            <td class="contenttabletd">
                <table class="contenttable" style="width: 100%;">
                    <tr>
                        <td class="contenttabletd">
                            <%= Html.Hidden("Model.StrYearStartDate", Model.StrYearStartDate)%>
                            <%= Html.TextBox("Model.strSearchApplyFrom", Model.StrSearchApplyFrom, new { @class = "textRegularDate dtPicker date", @maxlength = 10 })%>
                        </td>
                        <td class="contenttabletd">
                            To
                        </td>
                        <td class="contenttabletd">
                            <%= Html.Hidden("Model.StrYearEndDate", Model.StrYearEndDate)%>
                            <%= Html.TextBox("Model.strSearchApplyTo", Model.StrSearchApplyTo, new { @class = "textRegularDate dtPicker date", @maxlength = 10 })%> 
                        </td>
                    </tr>
                </table>
            </td>
            <td>
               <%-- Leave Year--%>
            </td>
            <td>
                <%--<%= Html.Hidden("Model.intSearchLeaveYearId", Model.intSearchLeaveYearId)%>
                <%= Html.Hidden("Model.StrSearchLeaveYear", Model.StrSearchLeaveYear)%>
                <div style="float: left; text-align: left; margin-top:5px;">
                    <label><%=Model.StrSearchLeaveYear%></label>
                </div>--%>
                <div style="float: right; text-align: right; padding-right: 10px;">
                    <a href="#" class="btnSearchData" onclick="return searchData();"></a>
                </div>
            </td>
            <%--<td colspan="2">

            </td>--%>
        </tr>
        <%--<tr>
            <td colspan="4" style="text-align: center">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>--%>
    </table>
</div>
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfflineLeaveApplication, LMS.Web.Permission.MenuOperation.Add))
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
                        Applicant
                    </th>
                    <th>
                        Leave Type
                    </th>
                    <th style="width: 12%;">
                        Leave From
                    </th>
                    <th style="width: 12%;">
                        Leave To
                    </th>
                    <th style="width: 12%;">
                        Status
                    </th>
                    <th style="width: 12%;">
                       Approve Date
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfflineLeaveApplication, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th style="width: 3%;">                        
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.LeaveApplication obj in Model.LstLeaveApplication)
                   { 
                %>
                <tr>
                    <td style="width: 12%;">
                        <%=Html.Encode(obj.strApplyDate)%>
                    </td>
                    <td>
                        <%=Html.Hidden(obj.intApplicationID.ToString())%>
                        <%=Html.Encode(obj.strEmpInitial.ToString() + "-" + obj.strEmpName.ToString() + ", " + obj.strDesignation.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strLeaveType)%>
                    </td>
                    <td style="width: 12%;">
                        <%=Html.Encode(obj.strApplyFromDate)%>
                    </td>
                    <td style="width: 12%;">
                        <%=Html.Encode(obj.strApplyToDate)%>
                    </td>
                    <td style="width: 12%;">                        
                        <%=Html.Label(LMS.Web.Utils.GetApplicationStatus(obj.intAppStatusID))%>
                    </td>
                    <td style="width: 12%;">
                        <%=Html.Encode(string.IsNullOrEmpty(obj.strOfflineApprovedById) ? obj.ApproveDateTime.ToString(LMS.Util.DateTimeFormat.Date) : obj.dtOffLineAppvDate.ToString(LMS.Util.DateTimeFormat.Date))%>                  
                    </td>
                    <%if (isEditable)
                      { %>
                    <td style="width: 3%;">
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails( <%= obj.intApplicationID  %>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(ViewData.Model.LstLeaveApplicationPaging.PageSize, ViewData.Model.LstLeaveApplicationPaging.PageNumber, ViewData.Model.numTotalRows,"frmOfflineLeaveApplicationList","/LMS/OfflineLeaveApplication/OfflineLeaveApplication", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveApplicationPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString() %></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleOfflineLeaveApplication">
    <iframe id="styleOpenerOfflineLeaveApplication" src="" width="100%" height="98%"
        style="border: 0px solid white; margin-right: 0px; padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
