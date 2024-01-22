<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>

<style type="text/css">
    .style1
    {
        width: 413px;
    }
    .style2
    {
        width: 24%;
        height: 20px;
    }
    .style3
    {
        width: 35%;
        height: 20px;
    }
    .style4
    {
        width: 6%;
        height: 20px;
    }
    .style5
    {
        width: 30px;
        height: 20px;
    }
    .style6
    {
        height: 33px;
    }
    .style7
    {
        width: 191px;
    }
</style>

<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveApplicationList"));
        hiddenAppvDiv();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true

        });

        $("#divStyleLeaveApplication").dialog({ autoOpen: false, modal: true, height: 775, width: 930, resizable: false, title: 'Alternate Approval Process',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = "#divDataList";
                var url = '/LMS/AlternateApprovalProcess/AlternateApprovalProcess?page=' + pg;
                var form = $("#frmLeaveApplicationList");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) {
                    $(targetDiv).html(result);        
                }, "html");
            }
        });

        $("#Model_IsBulkApprove").click(function () {
            hiddenAppvDiv();
            searchData();
        });

        $("#Model_IsSendEmailToAuthority").click(function () {
            var isselect = $('#Model_IsSendEmailToAuthority').attr('checked');
            $('#Model_IsSendEmailToAuthority').val(isselect);
        });

    });


    function hiddenAppvDiv() {
        var isselect = $('#Model_IsBulkApprove').attr('checked');
        $('#Model_IsBulkApprove').val(isselect);

        if (isselect == true) 
        {
            $('#divAppvBtn').css('display', '');
            $('#divSendEmail').css('display', '');

//            $("input:checkbox").each(function (i) {
//                if ($(this).hasClass('chkBox')) {
//                    //this.checked = true;
//                }
//            });

        }
        else {
            $('#divAppvBtn').css('display', 'none');
            $('#divSendEmail').css('display', 'none');
            $('#Model_IsSendEmailToAuthority').val(false);
            $('#Model_IsSendEmailToAuthority').attr('checked',false);
        }
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
                alert("Invalid date");
            }
        }

        if (hookup == true) {
            $('#Model_IsSearch').val(true);
            var targetDiv = "#divDataList";
            var url = "/LMS/AlternateApprovalProcess/AlternateApprovalProcess";
            var form = $("#frmLeaveApplicationList");
            var serializedForm = form.serialize();            

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);              
            }, "html");           
        }
        return false;
    }



    function BulkApprove() {
        var targetDiv = "#divDataList";
        var url = '/LMS/AlternateApprovalProcess/SaveApprovalFlow';
        var form = $("#frmLeaveApplicationList");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            if (parseInt(result) == 0) {
                alert("Failure to bulk approve.");
            }
            else if (parseInt(result) == 1) {
                alert("Application approved successfully.");

                var targetDiv = "#divDataList";
                var pg = $('#txtPageNo').val();
                var url = '/LMS/AlternateApprovalProcess/AlternateApprovalProcess?page=' + pg;
                var form = $("#frmLeaveApplicationList");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) {
                    $(targetDiv).html(result);
                }, "html");
            }
            else if (parseInt(result) == 2) {
                alert("Application not fount to bulk approve.");
            }
        }, "json");

        return false;
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


            executeCustomAction({ intLeaveApplicationID: Id }, '/LMS/LeaveApplication/Delete', 'divLeaveApplicationList');


        }
        return false;
    }

    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/AlternateApprovalProcess/Details/' + Id;
        $('#styleOpenerLeaveApplication').attr({ src: url });
        $("#divStyleLeaveApplication").dialog('open');
        return false;
    }


    function popupStyleAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/AlternateApprovalProcess/LeaveApplicationAdd';
        $('#styleOpenerLeaveApplication').attr({ src: url });
        $("#divStyleLeaveApplication").dialog('open');
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

<h3 class="page-title">Alternate Approval Process</h3>

<form id="frmLeaveApplicationList" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                ID
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchInitial", Model.strSearchInitial, new { @class = "textRegular", @maxlength = 50 })%>
            </td>
            <td>
                Name
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
                Apply From
            </td>
            <td>
                <div style="width: 100%; float: left; text-align: left;">
                    <div style="float: left; text-align: left;">
                        <%=Html.Hidden("Model.StrYearStartDate", Model.StrYearStartDate)%>
                        <%=Html.TextBox("Model.strSearchApplyFrom", Model.StrSearchApplyFrom, new { @class = "textRegularDate dtPicker date", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                    </div>
                    <div style="float: left; text-align: left; padding-left: 10px;">
                        To <%=Html.Hidden("Model.StrYearEndDate", Model.StrYearEndDate)%>
                            <%=Html.TextBox("Model.strSearchApplyTo", Model.StrSearchApplyTo, new { @class = "textRegularDate dtPicker date", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                    </div>
                </div>
            </td>
            <td class="style6">
               <%-- Leave Year--%>
            </td>
            <td class="style6">
                <%=Html.Hidden("Model.intSearchLeaveYearId", Model.intSearchLeaveYearId)%>
                <%=Html.Hidden("Model.StrSearchLeaveYear", Model.StrSearchLeaveYear)%>

                <%=Html.Hidden("Model.IsSearch", Model.IsSearch)%>            
                <%=Html.HiddenFor(m => m.IsSendEmailToAuthority)%>
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
            <%=Html.Hidden("Model.IsSearch", Model.IsSearch)%>            
            <%=Html.HiddenFor(m => m.IsSendEmailToAuthority)%>
            <%=Html.HiddenFor(m=> m.IsBulkApprove)%>
            <td colspan="4" style="text-align: center">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>--%>
    </table>
</div>
<div id="divList">
<table style="width: 100%;">
        <tr>
            <td>
                <div style="float: left; text-align: left;">
                    <%=Html.CheckBox("Model.IsBulkApprove", Model.IsBulkApprove)%>Bulk Approve
                </div>
                <div id="divAppvBtn" style="float: left; text-align: left; padding-left: 8px; display: none;">
                    <a href="#" class="btnApprove" onclick="return BulkApprove();"></a>
                </div>
                <div id="divSendEmail" style="float: left; text-align: left; padding-left: 8px; display: none;">
                    <%=Html.CheckBox("Model.IsSendEmailToAuthority", Model.IsSendEmailToAuthority)%>Don't send email to authority 
                </div>
            </td>
        </tr>
    </table>
    <div id="grid">
        <div id="grid-data" style="overflow: auto; width: 99%">
            <table id="applicationList" style="width: 99%;">
                <thead>
                    <tr>
                        <%bool isEditable = false; %>
                        <%if (Model.IsBulkApprove == true)
                          { %>
                            <th style="width: 5%;"></th>
                          <%} %>
                        <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AlternateApprovalProcess, LMS.Web.Permission.MenuOperation.Edit))
                          {
                            isEditable = true; %>
                            <th style="width: 5%;"></th>
                        <%} %>      
                        <th style="width: 14%;">Apply Date</th>
                        <th>Applicant</th>
                        <th style="width: 20%;">Leave Type</th>
                        <%--<th style="width: 5%;">Adj.</th>--%>
                        <th style="width: 15%;">Status</th>
                        <%if (Model.IsBulkApprove == false)
                          { %>
                            <th style="width: 13%;">Date</th>  
                          <%} %>                                                            
                    </tr>
                </thead>               
            </table>
            <div style="overflow-y: auto; overflow-x: hidden; max-height: 230px;">
                <table style="width: 99%;">
                     <%--<tbody>--%>
                    <%if (Model.IsBulkApprove == false)
                      {%>
                        <% foreach (LMSEntity.LeaveApplication obj in Model.LstLeaveApplication)
                           { 
                        %>
                        <tr>
                            <%if (isEditable)
                              { %>
                                <td style="width: 5%;">
                                    <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails( <%= obj.intApplicationID  %>);'>
                                    </a>
                                </td>
                            <% }%>
                            <td style="width: 14%;">
                                <%=Html.Encode(obj.dtApplyDate.ToString(LMS.Util.DateTimeFormat.Date))%>
                            </td>
                            <td>
                                <%=Html.Hidden(obj.intApplicationID.ToString())%>
                                <%=Html.Encode(obj.strEmpInitial.ToString() + "-" + obj.strEmpName +", "+obj.strDesignation)%>
                            </td>
                            <td style="width: 20%;">
                                <%=Html.Encode(obj.strLeaveType)%>
                            </td>
                           <%-- <td style="width: 5%;">
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
                                <%=Html.Encode(LMS.Web.Utils.GetApplicationStatus(obj.intAppStatusID))%>
                            </td>
                            <td style="width: 13%;">
                                <%=Html.Encode(obj.ApproveDateTime != null ? obj.ApproveDateTime.ToString(LMS.Util.DateTimeFormat.Date) : string.Empty)%>                  
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
                                 <td style="width: 5%;">
                                    <%=Html.CheckBox("Model.LstLeaveApplication[" + i.ToString() + "].IsChecked", Model.LstLeaveApplication[i].IsChecked, new { @class = "chkBox" })%>
                                </td>
                                <%if (isEditable)
                                 { %>
                                    <td style="width: 5%;">
                                        <a href='#' class="gridEdit" onclick="return popupStyleDetails('<%=Model.LstLeaveApplication[i].intApplicationID%>');">
                                        </a>
                                    </td>
                                <% }%>
                                <td style="width: 13%;">
                                    <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].intAppFlowID", Model.LstLeaveApplication[i].intAppFlowID)%>
                                    <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].intApplicationID", Model.LstLeaveApplication[i].intApplicationID)%>
                                    <%=Html.Encode(Model.LstLeaveApplication[i].strApplyDate)%>
                                </td>
                                 <td>
                                    <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].strEmpID", Model.LstLeaveApplication[i].strEmpInitial)%>
                                    <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].strEmpName", Model.LstLeaveApplication[i].strEmpName)%>
                                    <%=Html.Encode(Model.LstLeaveApplication[i].strEmpInitial.ToString() + '-' + Model.LstLeaveApplication[i].strEmpName.ToString() + ", " + Model.LstLeaveApplication[i].strDesignation)%>

                                </td>
                                <td style="width: 20%;">
                                     <%=Html.Hidden("Model.LstLeaveApplication[" + i.ToString() + "].strLeaveType", Model.LstLeaveApplication[i].strLeaveType)%>
                                     <%=Html.Encode(Model.LstLeaveApplication[i].strLeaveType)%>
                                </td>
                               <%-- <td style="width: 5%;">
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
                                <%--<td style="width: 13%;">
                                    <%=Html.Encode(Model.LstLeaveApplication[i].ApproveDateTime != null ? Model.LstLeaveApplication[i].ApproveDateTime.ToString(LMS.Util.DateTimeFormat.Date) : string.Empty)%>                  
                                </td> --%>    
                            </tr>
                        <%} %>
                      <%} %>
                <%--</tbody>--%>
                </table>
            </div>
        </div>
        <div class="pager">
            <%= Html.PagerWithScript(ViewData.Model.LstLeaveApplicationPaging.PageSize, ViewData.Model.LstLeaveApplicationPaging.PageNumber, ViewData.Model.numTotalRows, "frmLeaveApplicationList", "/LMS/AlternateApprovalProcess/AlternateApprovalProcess", "divDataList")%>
            <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveApplicationPaging.PageNumber)%>
        </div>
        <label id="lblTotalRows">
            Total Records:<%=Model.numTotalRows.ToString() %></label>
    </div>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleLeaveApplication">
    <iframe id="styleOpenerLeaveApplication" src="" width="100%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
