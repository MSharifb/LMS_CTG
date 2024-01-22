<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ApprovalFlowModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmApprovalFlow"));

        setTitle("Approval Flow");

        $("#btnReject").hide();

        $("#btnApprove").hide();

        $("#btnRecommend").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });

        FormatTextBox();
        checkUrl();
    });

    function checkUrl() {

        var url = document.URL.split('&')[1];
        if (url == undefined) {
            url = '';
        }

        // If the parameter exists create the message and insert into our paragraph
        if (url == 'FromMail=true') {
            $(".btnClose").hide();
        }

    }

    function CalcutateDuration() {

        var targetDiv = "#divLeavApplicationeDetails";
        var url = "/LMS/LeaveApplication/CalcutateDuration";
        var form = $("#frmLeaveApplication");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {

            $("#LeaveApplication_fltDuration").val(result);

            $("#LeaveApplication_fltWithPayDuration").val(result);

            serializedForm = form.serialize();

            url = "/LMS/LeaveApplication/GetLedger";

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }, "json");

        return false;
    }

    //    function SetCalcutateDuration() {

    //    }


    function FullDayHourly(e) {

        var IsApplicationType = $(e).attr('checked');

        var strApplicationType = $(e).val();


        if (strApplicationType == "FullDay" && IsApplicationType == true) {

            $("#lblTime").css('visibility', 'hidden');
            $("#LeaveApplication_strApplyFromTime").css('visibility', 'hidden');
            $("#LeaveApplication_strApplyToTime").css('visibility', 'hidden');
            $("#btnCalculate").css('visibility', 'visible');
            $("#lblDaysHour").html("Days");

        }


        if (strApplicationType == "Hourly" && IsApplicationType == true) {
            $("#lblTime").css('visibility', 'visible');
            $("#LeaveApplication_strApplyFromTime").css('visibility', 'visible');
            $("#LeaveApplication_strApplyToTime").css('visibility', 'visible');
            $("#lblDaysHour").html("Hours");
            $("#btnCalculate").css('visibility', 'hidden');
        }

        if (strApplicationType == "FullDayHalfDay" && IsApplicationType == true) {

            $("#lblTime").css('visibility', 'visible');
            $("#LeaveApplication_strApplyFromTime").css('visibility', 'visible');
            $("#LeaveApplication_strApplyToTime").css('visibility', 'visible');

            $("#lblDaysHour").html("Days");

            $("#btnCalculate").css('visibility', 'visible');
        }

        $("#LeaveApplication_strEmpID").trigger("click");

        return false;
    }


    //    function removeRequired() {

    //    }

    //    function addRequired() {

    //    }

    function searchEmployee() {
        window.parent.openEmployee();
    }


    function save(type) {

        if (type == 3) {
            $('#ApprovalFlow_strDestAuthorID').removeClass('required');

        }

        if (fnValidate() == true) {

            targetDiv = "#divApprovalFlowDetails";
            var url = "/LMS/ApprovalFlow/SaveApprovalFlow/" + type;
            var form = $("#frmApprovalFlow");
            var serializedForm = form.serialize();
            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }


    function Delete() {
        Id = $('#LeaveApplication_strCompanyID').val();
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ strCompanyID: Id }, '/LMS/LeaveApplication/Delete', 'divLeaveApplicationList');
            window.parent.location = "/LeaveApplication";
        }
        return false;
    }
</script>
<form id="frmApprovalFlow" method="post" action="">
<div>
    <div id="divApprovalFlow">
        <div style="width: 100%">
            <div class="divSpacer">
            </div>
            <div class="divRow">
                <div class="divCol1">
                </div>
                <div class="divCol2">
                    <%= Html.HiddenFor(m => m.ApprovalFlow.intApplicationID)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.intApplicationID)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.bitIsApprovalProcess)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.bitIsDiscard)%>
                    <%= Html.HiddenFor(m => m.ApprovalFlow.intAppStatusID)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.intAppStatusID)%>
                    <%= Html.HiddenFor(m => m.intNodeID)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveYearID)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.intDestNodeID)%>
                    <%= Html.HiddenFor(m => m.ApprovalFlow.intNodeID)%>
                    <%= Html.HiddenFor(m => m.ApprovalFlow.strAuthorID)%>
                    <%= Html.HiddenFor(m => m.ApprovalFlow.intAppFlowID)%>
                    <%= Html.HiddenFor(m => m.IsFromUrl) %>
                    <%= Html.HiddenFor(m => m.LeaveApplication.bitIsAdjustment) %>
                </div>
            </div>
            <div class="divRow">
                <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
                    <tr>
                        <td style="width: 75%;">
                            <table class="contenttable" style="width: 100%; padding: 0px;">
                                <tr>
                                    <td style="width: 19%;">
                                        Applicant Initial<label></label>
                                    </td>
                                    <td>
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strEmpID)%>
                                        <%=Html.Encode(Model.LeaveApplication.strEmpInitial)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 19%;">
                                        Applicant Name<label></label>
                                    </td>
                                    <td>
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strEmpName)%>
                                        <%=Html.Encode(Model.LeaveApplication.strEmpName)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 19%;">
                                        Designation<label></label>
                                    </td>
                                    <td>
                                        <%= Html.HiddenFor(m => m.LeaveApplication.strDesignationID)%>
                                        <%=Html.Encode(Model.LeaveApplication.strDesignation)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 19%;">
                                        Application Status<label></label>
                                    </td>
                                    <td>
                                        <%=Html.Encode(LMS.Web.Utils.GetApplicationStatus(Model.LeaveApplication.intAppStatusID))%>
                                    </td>
                                </tr>
                                <%-- <tr>
                                    <td style="width: 17%;">
                                        Division/Unit<label></label>
                                    </td>
                                    <td>
                                        <%= Html.HiddenFor(m => m.LeaveApplication.strDepartmentID)%>
                                        <%=Html.Encode(Model.LeaveApplication.strDepartment)%>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                        <td valign="top" style="width: 180px;" align="right">
                            <img style="width: 180px; height: 130px; padding-left: 20px;" imagealign="Right"
                                alt="" src="<%= Url.Content("~/Content/img/defaultPic.jpg")%>" />
                        </td>
                    </tr>
                </table>
                <br />
                <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
                    <tr>
                        <td style="width: 15%;">
                            Leave Type 
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.strLeaveType)%>
                            <%=Html.Encode(Model.LeaveApplication.strLeaveType)%>
                        </td>
                        <td style="width: 15%;">
                            Applied Date
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>
                            <%=Html.Encode(Model.LeaveApplication.strApplyDate)%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            From
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromDate)%>
                            <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromDate)%>
                        </td>
                        <td style="width: 15%">
                            To
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToDate)%>
                            <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyToDate)%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            Duration
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedDuration)%>
                            <%=Html.Encode(Model.LeaveApplication.fltSubmittedDuration)%>
                        </td>
                        <td style="width: 15%">
                            <%-- Net Balance--%>
                        </td>
                        <td>
                            <%-- <%=Html.Encode(Model.LeaveApplication.fltNetBalance)%>--%>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
                    <tr>
                        <td style="width: 15%;">
                            Responsible Person
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.strResponsibleId)%>
                            <%=Html.HiddenFor(m => m.LeaveApplication.strResponsibleName)%>
                            <%if (!string.IsNullOrEmpty(Model.LeaveApplication.strResponsibleId))
                              { %>
                            <%=Html.Encode(Model.LeaveApplication.strResponsibleInitial + " - " + Model.LeaveApplication.strResponsibleName)%>
                            <%} %>
                        </td>
                    </tr>
                    <%if (Model.LeaveApplication.strPLID != null)
                      { %>
                    <tr>
                        <td style="width: 15%;">
                            Recommender / Approver 
                        </td>
                        <td>
                            <%if (!string.IsNullOrEmpty(Model.LeaveApplication.strPLID))
                              { %>
                            <%=Html.Encode(Model.LeaveApplication.strPLInitial + " - " + Model.LeaveApplication.strPLName)%>
                            <%} %>
                        </td>
                    </tr>
                    <%
                      } %>
                    <tr>
                        <td style="width: 15%;">
                            Purpose
                        </td>
                        <td>
                            <div style="height: 30px; overflow-y: auto;">
                                <%=Html.HiddenFor(m => m.LeaveApplication.strPurpose)%>
                                <%=Html.Encode(Model.LeaveApplication.strPurpose)%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Address During Leave
                        </td>
                        <td>
                            <div style="height: 30px; overflow-y: auto;">
                                <%=Html.HiddenFor(m => m.LeaveApplication.strContactAddress)%>
                                <%=Html.Encode(Model.LeaveApplication.strContactAddress)%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Contact No.
                        </td>
                        <td>
                            <div>
                                <%=Html.HiddenFor(m => m.LeaveApplication.strContactNo)%>
                                <%=Html.Encode(Model.LeaveApplication.strContactNo)%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Comments
                        </td>
                        <td>
                            <div style="height: 30px; overflow-y: auto;">
                                <%=Html.HiddenFor(m => m.LeaveApplication.strRemarks) %>
                                <%=Html.Encode(Model.LeaveApplication.strRemarks)%>
                            </div>
                        </td>
                    </tr>
                </table>
                <%--<table class="contenttext" style="width: 100%;">
                    <tr>
                        <td style="width: 37%" valign="top">
                            <table class="contenttable" style="width: 100%; padding: 0px;">
                                <tr>
                                    <td style="width: 35%;">
                                        Applied Date
                                    </td>
                                    <td>
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>
                                        <%=Html.Encode(Model.LeaveApplication.strApplyDate)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 35%;">
                                        Leave Type
                                    </td>
                                    <td>
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strLeaveType)%>
                                        <%=Html.Encode(Model.LeaveApplication.strLeaveType)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 35%;">
                                        Purpose
                                    </td>
                                    <td>
                                        <div style="height: 30px; overflow-y: auto;">
                                            <%=Html.HiddenFor(m => m.LeaveApplication.strPurpose)%>
                                            <%=Html.Encode(Model.LeaveApplication.strPurpose)%>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 35%;">
                                        Address During Leave
                                    </td>
                                    <td>
                                        <div style="height: 30px; overflow-y: auto;">
                                            <%=Html.HiddenFor(m => m.LeaveApplication.strContactAddress)%>
                                            <%=Html.Encode(Model.LeaveApplication.strContactAddress)%>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 35%;">
                                        Contact No.
                                    </td>
                                    <td>
                                        <div style="height: 30px; overflow-y: auto;">
                                            <%=Html.HiddenFor(m => m.LeaveApplication.strContactNo)%>
                                            <%=Html.Encode(Model.LeaveApplication.strContactNo)%>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 35%;">
                                        Comments
                                    </td>
                                    <td>
                                        <div style="height: 30px; overflow-y: auto;">
                                            <%=Html.HiddenFor(m => m.LeaveApplication.strRemarks) %>
                                            <%=Html.Encode(Model.LeaveApplication.strRemarks)%>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 35%;">
                                        Responsible Person
                                    </td>
                                    <td>
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strResponsibleId)%>
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strResponsibleName)%>
                                        <%if (!string.IsNullOrEmpty(Model.LeaveApplication.strResponsibleId))
                                          { %>
                                        <%=Html.Encode(Model.LeaveApplication.strResponsibleInitial + " - " + Model.LeaveApplication.strResponsibleName)%>
                                        <%} %>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 63%" valign="top">
                            <table class="contenttable" width="100%">
                                <tr>
                                    <td colspan="2" class="contenttabletd">
                                        <table width="100%">
                                             <tr>
                                                <td colspan="5" style="text-align: right;">
                                                    <%if (Model.LeaveApplication.bitIsAdjustment == true)
                                                      { %>
                                                    Adjustment Application Status:
                                                    <%}
                                                      else
                                                      {%>
                                                    Leave Application Status:
                                                    <%} %>
                                                    <label style="font-weight: bold">
                                                        <%=Html.Encode(LMS.Web.Utils.GetApplicationStatus(Model.LeaveApplication.intAppStatusID))%></label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    Leave Date
                                                </td>
                                                <td>
                                                    Leave Time
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <% if (String.Compare(Model.LeaveApplication.strSubmittedApplicationType, "Hourly", true) == 0)
                                                       { %>
                                                    <label id="lblDaysHour">
                                                        Hours</label>
                                                    <%}
                                                       else
                                                       { %>
                                                    <label id="Label1">
                                                        Days</label>
                                                    <%} %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    From
                                                    <label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromDate)%>
                                                    <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromDate)%>
                                                </td>
                                                <td>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromTime)%>
                                                    <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromTime)%>
                                                </td>
                                                <td>
                                                    Duration<label></label>
                                                </td>
                                                <td colspan="2">
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedDuration)%>
                                                    <%=Html.Encode(Model.LeaveApplication.fltSubmittedDuration)%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    To<label></label>
                                                </td>
                                                <td>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToDate)%>
                                                    <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyToDate)%>
                                                </td>
                                                <td>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToTime)%>
                                                    <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyToTime)%>
                                                </td>
                                                <td>
                                                    With Pay Duration<label></label>
                                                </td>
                                                <td colspan="2">
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithPayDuration)%>
                                                    <%=Html.Encode(Model.LeaveApplication.fltSubmittedWithPayDuration)%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Duration Type<label></label>
                                                </td>
                                                <td colspan="2">
                                                    <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplicationType)%>
                                                    <%if (String.Compare(Model.LeaveApplication.strSubmittedApplicationType, "FullDay", true) == 0)
                                                      {%>
                                                    Full Day<% }
                                                      else if (String.Compare(Model.LeaveApplication.strSubmittedApplicationType, "Hourly", true) == 0)
                                                      { %>
                                                    Hourly<%}
                                                      else
                                                      {%>
                                                    Full Day and/or Half Day<%}%>
                                                </td>
                                                <td>
                                                    Without Pay Duration<label></label>
                                                </td>
                                                <td colspan="2">
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithoutPayDuration)%>
                                                    <%=Html.Encode(Model.LeaveApplication.fltSubmittedWithoutPayDuration)%>
                                                </td>
                                            </tr>
                                            <%if (String.Compare(Model.LeaveApplication.strSubmittedApplicationType, "FullDayHalfDay", true) == 0)
                                              {%>
                                            <tr>
                                                <td>
                                                    Half Day Info.
                                                </td>
                                                <td colspan="6">
                                                    <%=Html.Encode(Model.LeaveApplication.strSubmitHalfDayDurationFor)%>
                                                </td>
                                            </tr>
                                            <%} %>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    Granted Date
                                                </td>
                                                <td>
                                                    Granted Time
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <% if (String.Compare(Model.LeaveApplication.strApplicationType, "Hourly", true) == 0)
                                                       { %>
                                                    <label id="Label3">
                                                        Hours</label>
                                                    <%}
                                                       else
                                                       { %>
                                                    <label id="Label4">
                                                        Days</label>
                                                    <%} %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    From
                                                    <label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strApplyFromDate)%>
                                                    <%=Html.Encode(Model.LeaveApplication.strApplyFromDate)%>
                                                </td>
                                                <td>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strApplyFromTime)%>
                                                    <%=Html.Encode(Model.LeaveApplication.strApplyFromTime) %>
                                                </td>
                                                <td>
                                                    Duration<label></label>
                                                </td>
                                                <td colspan="2">
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.fltDuration)%>
                                                    <%=Html.Encode(Model.LeaveApplication.fltDuration)%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    To<label></label>
                                                </td>
                                                <td>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strApplyToDate)%>
                                                    <%=Html.Encode(Model.LeaveApplication.strApplyToDate)%>
                                                </td>
                                                <td>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strApplyToTime)%>
                                                    <%=Html.Encode(Model.LeaveApplication.strApplyToTime)%>
                                                </td>
                                                <td>
                                                    With Pay Duration<label></label>
                                                </td>
                                                <td colspan="2">
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.fltWithPayDuration)%>
                                                    <%=Html.Encode(Model.LeaveApplication.fltWithPayDuration)%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Duration Type<label></label>
                                                </td>
                                                <td colspan="2">
                                                    <%= Html.HiddenFor(m => m.LeaveApplication.strApplicationType)%>
                                                    <%if (String.Compare(Model.LeaveApplication.strApplicationType, "FullDay", true) == 0)
                                                      {%>
                                                    Full Day<% }
                                                      else if (String.Compare(Model.LeaveApplication.strApplicationType, "Hourly", true) == 0)
                                                      { %>
                                                    Hourly<%}
                                                      else
                                                      {%>
                                                    Full Day and/or Half Day<%}%>
                                                </td>
                                                <td>
                                                    Without Pay Duration<label></label>
                                                </td>
                                                <td colspan="2">
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.fltWithoutPayDuration)%>
                                                    <%=Html.Encode(Model.LeaveApplication.fltWithoutPayDuration)%>
                                                </td>
                                            </tr>
                                            <%if (String.Compare(Model.LeaveApplication.strApplicationType, "FullDayHalfDay", true) == 0)
                                              {%>
                                            <tr>
                                                <td>
                                                    Half Day Info.
                                                </td>
                                                <td colspan="6">
                                                    <%=Html.Encode(Model.LeaveApplication.strHalfDayDurationFor)%>
                                                </td>
                                            </tr>
                                            <%} %>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>--%>
                <div class="divSpacer">
                </div>
                <div class="divSpacer">
                </div>
                <%--<table style="width: 100%;" class="contenttext">
                    <tr>
                        <td style="width: 18%;">
                            Purpose<label></label>
                        </td>
                        <td style="width: 25%;">
                            <%=Html.HiddenFor(m => m.LeaveApplication.strPurpose)%>
                            <%=Html.Encode(Model.LeaveApplication.strPurpose)%>
                        </td>
                        <td style="width: 18%;">
                            Contact Address<label></label>
                        </td>
                        <td style="width: 38%;">
                            <%=Html.HiddenFor(m => m.LeaveApplication.strContactAddress)%>
                            <%=Html.Encode(Model.LeaveApplication.strContactAddress)%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%;">
                            Contact No.
                        </td>
                        <td style="width: 25%;">
                            <%=Html.HiddenFor(m => m.LeaveApplication.strContactNo)%>
                            <%=Html.Encode(Model.LeaveApplication.strContactNo)%>
                        </td>
                        <td style="width: 18%;">
                            Comments
                        </td>
                        <td style="width: 38%;">
                            <%=Html.HiddenFor(m => m.LeaveApplication.strRemarks) %>
                            <%=Html.Encode(Model.LeaveApplication.strRemarks)%>
                        </td>
                    </tr>
                </table>--%>
                <div class="divSpacer">
                </div>
                <div class="divSpacer">
                </div>
                <div>
                    <div style="overflow: auto; width: 99%;">
                        <table style="width: 99%;">
                            <thead>
                                <tr>
                                    <th style="width: 44%;">
                                        Recommender/Approver
                                    </th>
                                    <th>
                                        Comments
                                    </th>
                                </tr>
                            </thead>
                        </table>
                        <div style="overflow-y: auto; overflow-x: hidden; max-height: 70px">
                            <table style="width: 99%;">
                                <% for (int j = 0; j < Model.LstApprovalComments.Count; j++)
                                   { 
                                %>
                                <tr>
                                    <td style="width: 44%;">
                                        <label>
                                            <%= Model.LstApprovalComments[j].strEmpName%></label>
                                    </td>
                                    <td>
                                        <label>
                                            <%= Model.LstApprovalComments[j].strComments%></label>
                                    </td>
                                </tr>
                                <%} %>
                            </table>
                        </div>
                    </div>
                </div>
                <%if (Model.LeaveApplication.bitIsAdjustment == false)
                  { %>
                <div id="grid">
                    <div id="grid-data" style="overflow: auto; width: 99%;">
                        <table style="width: 99%;">
                            <thead>
                                <tr>
                                    <th>
                                        Leave Type
                                    </th>
                                    <th style="width: 10%;">
                                        Carry Over
                                    </th>
                                    <th style="width: 15%;">
                                        Yearly Entitlement
                                    </th>
                                    <th style="width: 7%;">
                                        Availed
                                    </th>
                                    <th style="width: 10%;">
                                        Encashment
                                    </th>
                                    <th style="width: 7%;">
                                        Applied
                                    </th>
                                    <th style="width: 10%;">
                                        Balance
                                    </th>
                                </tr>
                            </thead>
                        </table>
                        <div style="overflow-y: auto; overflow-x: hidden; max-height: 100px">
                            <table style="width: 99%;">
                                <% for (int j = 0; j < Model.LstLeaveLedger.Count; j++)
                                   { 
                                %>
                                <tr>
                                    <td>
                                        <label>
                                            <%= Model.LstLeaveLedger[j].strLeaveType.ToString()%></label>
                                    </td>
                                    <td style="width: 10%;">
                                        <label>
                                            <%= Model.LstLeaveLedger[j].fltOB.ToString()%></label>
                                    </td>
                                    <td style="width: 15%;">
                                        <label>
                                            <%= Model.LstLeaveLedger[j].fltEntitlement.ToString()%></label>
                                    </td>
                                    <td style="width: 7%;">
                                        <label>
                                            <%= Model.LstLeaveLedger[j].fltAvailed.ToString("#0.00")%></label>
                                    </td>
                                    <td style="width: 10%;">
                                        <label>
                                            <%= Model.LstLeaveLedger[j].fltEncased.ToString()%></label>
                                    </td>
                                    <td style="width: 7%;">
                                        <label>
                                            <%= Model.LstLeaveLedger[j].fltApplied.ToString("#0.00")%></label>
                                    </td>
                                    <td style="width: 10%;">
                                        <label>
                                            <%= Model.LstLeaveLedger[j].fltCB.ToString("#0.00")%></label>
                                    </td>
                                </tr>
                                <%} %>
                            </table>
                        </div>
                    </div>
                </div>
                <%} %>
            </div>
        </div>
    </div>
    <div class="divSpacer">
    </div>
    <%--<div class="divButton">
        <a href="#" class="btnClose" onclick="return closeDialog();"></a>
    </div>--%>
    <div id="divMsgStd" class="divMsg">
        <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
    </div>
</div>
</form>
