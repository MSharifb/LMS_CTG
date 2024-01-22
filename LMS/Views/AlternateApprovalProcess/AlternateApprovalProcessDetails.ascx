<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveApplication"));
        $("#btnSave").hide();
        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });

        FormatTextBox();
    });


    //    function CalcutateDuration() {

    //        var targetDiv = "#divLeaveApplicationDetails";
    //        var url = "/LMS/AlternateApprovalProcess/CalcutateDuration";
    //        var form = $("#frmLeaveApplication");
    //        var serializedForm = form.serialize();

    //        $.post(url, serializedForm, function (result) {

    //            $("#LeaveApplication_fltDuration").val(result);

    //            $("#LeaveApplication_fltWithPayDuration").val(result);

    //            serializedForm = form.serialize();

    //            url = "/LMS/AlternateApprovalProcess/GetLedger";

    //            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

    //        }, "json");


    //        return false;

    //    }

    //    function SetCalcutateDuration() {

    //    }

    //    function removeRequired() {

    //    }

    //    function addRequired() {

    //    }

    function searchEmployee() {
        window.parent.openEmployee();
    }


    function AlternateApprove() {
        var confresult;
        var LWP = $("#LeaveApplication_fltWithoutPayDuration").val();
        var netBL = $("#LeaveApplication_fltNetBalance").val();
        var isAdjust = $("#LeaveApplication_bitIsAdjustment").val();

        var strCOM = $("#ApprovalFlow_strComments").val();
        if (jQuery.trim(strCOM) == "") {
            alert("Please input author comments.");
            return false;
        }

        confresult = confirm('Pressing OK will approve this application. Do you want to continue?');
        if (confresult == true) {
            if (isAdjust == false) {

                if (parseInt(LWP) > 0) {
                    confresult = confirm('This application has leave without pay duration. Do you want to proceed?');
                    if (confresult == false) {
                        return false;
                    }
                }

                if (parseInt(netBL) < 0) {
                    confresult = confirm('Net balance is ' + netBL.toString() + '. Do you want to proceed?');
                    if (confresult == false) {
                        return false;
                    }
                }
            }

            if (fnValidate() == true) {
                Id = $('#LeaveApplication_intApplicationID').val();
                var targetDiv = "#divLeaveApplicationDetails";
                var url = "/LMS/AlternateApprovalProcess/AlternateApprove";
                var form = $("#frmLeaveApplication");
                var serializedForm = form.serialize();
                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        }
        return false;
    }


    function AlternateRecommend() {
        var confresult;
        var LWP = $("#LeaveApplication_fltWithoutPayDuration").val();
        var strCOM = $("#ApprovalFlow_strComments").val();

        if (fnValidate() == true) {
            if (jQuery.trim(strCOM) == "") {
                alert("Please input author comments.");
                return false;
            }

            confresult = confirm('Pressing OK will recommend this application. Do you want to continue?');
            if (confresult == true) {
                if (parseInt(LWP) > 0) {
                    confresult = confirm('This application has leave without pay duration. Do you want to proceed?');
                    if (confresult == false) {
                        return false;
                    }
                }

                Id = $('#LeaveApplication_intApplicationID').val();
                var targetDiv = "#divLeaveApplicationDetails";
                var url = "/LMS/AlternateApprovalProcess/AlternateRecommend";
                var form = $("#frmLeaveApplication");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        }
        return false;
    }



    function AlternateReject() {

        if (fnValidate() == true) {
            var strCOM = $("#ApprovalFlow_strComments").val();
            if (jQuery.trim(strCOM) == "") {
                alert("Please input author comments.");
                return false;
            }

            var confresult = confirm('Pressing OK will reject this application. Do you want to continue?');
            if (confresult == true) {
                Id = $('#LeaveApplication_intApplicationID').val();
                var targetDiv = "#divLeaveApplicationDetails";
                var url = "/LMS/AlternateApprovalProcess/AlternateReject";
                var form = $("#frmLeaveApplication");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        }
        return false;
    }

    function AlternateDelete() {

        var confresult = confirm('Pressing OK will delete this application. Do you want to continue?');
        if (confresult == true) {

            Id = $('#LeaveApplication_intApplicationID').val();
            var targetDiv = "#divLeaveApplicationDetails";
            var url = "/LMS/AlternateApprovalProcess/AlternateDelete";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

</script>
<form id="frmLeaveApplication" method="post" action="">
<div id="divLeaveApplication">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.LeaveApplication.intApplicationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsApprovalProcess)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsDiscard)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intAppStatusID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveYearID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intDestNodeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsAdjustment) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplicationType) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedHalfDayFor)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intSubmittedDurationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strHalfDayFor)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intDurationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplicationType)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltNetBalance)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.isServiceLifeType)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveTypeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strLeaveType)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strPurpose)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strContactAddress)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpName)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDesignationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDesignation)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDepartmentID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDepartment)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strBranch)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strResponsibleId)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strResponsibleName)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strRemarks) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strContactNo)%>
            <%= Html.HiddenFor(m => m.ApprovalFlow.intAppStatusID)%>
            <%= Html.HiddenFor(m => m.ApprovalFlow.intNodeID)%>
            <%= Html.HiddenFor(m => m.ApprovalFlow.strAuthorID)%>
            <%= Html.HiddenFor(m => m.ApprovalFlow.intAppFlowID)%>
            <%= Html.HiddenFor(m => m.intNodeID)%>
            <%= Html.HiddenFor(m => m.StrYearStartDate) %>
            <%= Html.HiddenFor(m => m.StrYearEndDate) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltSubmittedDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToDate)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithPayDuration)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithoutPayDuration)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.strApplyFromDate)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.strApplyFromTime)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.fltDuration)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.strApplyToDate)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.strApplyToTime)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.fltWithPayDuration)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.fltWithoutPayDuration)%>
            <%=Html.Hidden("ApplicationType", Model.LeaveApplication.strApplicationType)%>
            <%=Html.HiddenFor(m => m.LeaveApplication.strPLID)%>
        </div>
    </div>
    <div>
        <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
            <tr>
                <td style="width: 75%;">
                    <table class="contenttable" style="width: 100%; padding: 0px;">
                        <tr>
                            <td style="width: 19%;">
                                Initial
                            </td>
                            <td>
                                <%= Html.Encode(Model.LeaveApplication.strEmpInitial)%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 19%;">
                                Name
                            </td>
                            <td>
                                <%=Html.Encode(Model.LeaveApplication.strEmpName)%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 19%;">
                                Designation
                            </td>
                            <td>
                                <%=Html.Encode(Model.LeaveApplication.strDesignation)%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 19%;">
                                Application Status:
                            </td>
                            <td>
                                <%= Html.HiddenFor(m => m.LeaveApplication.intAppStatusID)%>
                                <label style="font-weight: bold;">
                                    <%= Html.Encode(LMS.Web.Utils.GetApplicationStatus(Model.LeaveApplication.intAppStatusID))%></label>
                            </td>
                        </tr>
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
                    <%= Html.Encode(Model.LeaveApplication.strLeaveType)%>
                </td>
                <td style="width: 15%;">
                    Applied Date
                </td>
                <td>
                    <%= Html.Encode(Model.LeaveApplication.strApplyDate)%>
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                    From
                </td>
                <td>
                    <%= Html.Encode(Model.LeaveApplication.strSubmittedApplyFromDate)%>
                </td>
                <td style="width: 15%">
                    To
                </td>
                <td>
                    <%= Html.Encode(Model.LeaveApplication.strSubmittedApplyToDate)%>
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                    Duration
                </td>
                <td>
                    <%= Html.Encode(Model.LeaveApplication.fltSubmittedDuration)%>
                </td>
                <td style="width: 15%">
                    Duration With Pay
                </td>
                <td>
                    <%=Html.Encode(Model.LeaveApplication.fltWithPayDuration)%>
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                    Duration Without Pay
                </td>
                <td>
                    <%= Html.Encode(Model.LeaveApplication.fltWithoutPayDuration)%>
                </td>
                <td style="width: 15%">
                    Net Balance
                </td>
                <td>
                    <%=Html.Encode(Model.LeaveApplication.fltNetBalance)%>
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
                    <%if (!string.IsNullOrEmpty(Model.LeaveApplication.strResponsibleInitial))
                      { %>
                    <%=Html.Encode(Model.LeaveApplication.strResponsibleInitial + " - " + Model.LeaveApplication.strResponsibleName)%>
                    <%} %>
                </td>
                <td style="width: 15%;">
                    Recommender / Approver
                </td>
                <td>
                    <%=Html.Encode(Model.LeaveApplication.strPLInitial + " - " + Model.LeaveApplication.strPLName)%>
                </td>
            </tr>
            <%-- <tr>
                <td style="width: 15%;">
                    Responsible Person
                </td>
                <td>
                    <%if (!string.IsNullOrEmpty(Model.LeaveApplication.strResponsibleInitial))
                      { %>
                    <%=Html.Encode(Model.LeaveApplication.strResponsibleInitial + " - " + Model.LeaveApplication.strResponsibleName)%>
                    <%} %>
                </td>
            </tr>--%>
        </table>
        <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
            <tr>
                <td style="width: 15%;">
                    Purpose
                </td>
                <td>
                    <div style="height: 30px; overflow-y: auto;">
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
                        <%=Html.Encode(Model.LeaveApplication.strContactAddress)%>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    Contact No.
                </td>
                <td>
                    <div style="height: 30px; overflow-y: auto;">
                        <%=Html.Encode(Model.LeaveApplication.strContactNo)%>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
            <tr>
                <td style="width: 30%">
                    Next Recommender
                </td>
                <td>
                    <% if (Model.intNodeID > 0 && (Model.LeaveApplication.intAppStatusID == 4 || Model.LeaveApplication.intAppStatusID == 6))
                       { %>
                    <%if (Model.Approver.Count > 0)
                      { %>
                    <%= Html.DropDownListFor(m => m.ApprovalFlow.strDestAuthorID, Model.Approver, new { @class = "selectBoxRegular", @style = "width:315px;" })%>
                    <%}
                      else
                      { %>
                    <%= Html.DropDownListFor(m => m.ApprovalFlow.strDestAuthorID, Model.Approver, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:315px;" })%>
                    <%} %>
                    <%} %>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                    Author Comments<label class="labelRequired">*</label>
                </td>
                <td>
                    <%if (Model.LeaveApplication.intAppStatusID == 1)
                      { %>
                    <div style="height: 30px; overflow-y: auto;">
                        <%=Html.HiddenFor(m => m.ApprovalFlow.strComments)%>
                        <%=Html.Encode(Model.ApprovalFlow.strComments)%>
                    </div>
                    <%}
                      else
                      { %>
                    <%=Html.TextAreaFor(m => m.ApprovalFlow.strComments, new { @class = "textRegular required", @style = "width:315px;", @maxlength = 200, onkeyup = "return ismaxlengthPop(this)" })%>
                    <%} %>
                </td>
            </tr>
        </table>
       
        <div class="divSpacer">
        </div>
        <div class="divSpacer">
        </div>
        
        <%if (Model.ApprovalFlowList != null)
          { %>
        <table width="100%">
            <tr>
            </tr>
            <tr>
                <td class="contenttabletd">
                    <label>
                        Application Flow:</label>
                    <% int intCount = 0;
                       foreach (LMSEntity.ApprovalFlow appFlow in Model.ApprovalFlowList)
                       {                        
                    %>
                    <%=Html.Encode(appFlow.strAuthorInitial + "-" + appFlow.strAuthorName)%>
                    <%
intCount++;
if (intCount != Model.ApprovalFlowList.Count)
{%>
                    <img alt="" src="<%= Url.Content("~/Content/img/controls/Right-Arrow.gif")%>" />
                    <% 
}
                       } %>
                </td>
            </tr>
        </table>
        <%
          } %>
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
                        <%if (Model.LstApprovalComments != null)
                          { %>
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
                        <%} %>
                    </table>
                </div>
            </div>
        </div>
        <div class="divSpacer">
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
                        <%if (Model.LstLeaveLedger != null)
                          { %>
                        <% for (int j = 0; j < Model.LstLeaveLedger.Count; j++)
                           { 
                        %>
                        <tr>
                            <td>
                                <%= Html.Hidden("LstLeaveLedger[" + j + "].intLeaveTypeID", Model.LstLeaveLedger[j].intLeaveTypeID.ToString())%>
                                <%= Html.Hidden("LstLeaveLedger[" + j + "].strLeaveType", Model.LstLeaveLedger[j].strLeaveType.ToString())%>
                                <label>
                                    <%= Model.LstLeaveLedger[j].strLeaveType.ToString()%></label>
                            </td>
                            <td style="width: 10%;">
                                <%=Html.Hidden("LstLeaveLedger[" + j + "].fltOB", Model.LstLeaveLedger[j].fltOB.ToString())%>
                                <label>
                                    <%= Model.LstLeaveLedger[j].fltOB.ToString("#0.00")%></label>
                            </td>
                            <td style="width: 15%;">
                                <%=Html.Hidden("LstLeaveLedger[" + j + "].fltEntitlement", Model.LstLeaveLedger[j].fltEntitlement.ToString())%>
                                <label>
                                    <%= Model.LstLeaveLedger[j].fltEntitlement.ToString("#0.00")%></label>
                            </td>
                            <td style="width: 7%;">
                                <%=Html.Hidden("LstLeaveLedger[" + j + "].fltAvailed", Model.LstLeaveLedger[j].fltAvailed.ToString())%>
                                <label>
                                    <%= Model.LstLeaveLedger[j].fltAvailed.ToString("#0.00")%></label>
                            </td>
                            <td style="width: 10%;">
                                <%=Html.Hidden("LstLeaveLedger[" + j + "].fltEncased", Model.LstLeaveLedger[j].fltEncased.ToString())%>
                                <label>
                                    <%= Model.LstLeaveLedger[j].fltEncased.ToString("#0.00")%></label>
                            </td>
                            <td style="width: 7%;">
                                <%=Html.Hidden("LstLeaveLedger[" + j + "].fltapplied", Model.LstLeaveLedger[j].fltApplied.ToString())%>
                                <label>
                                    <%= Model.LstLeaveLedger[j].fltApplied.ToString("#0.00")%></label>
                            </td>
                            <td style="width: 10%;">
                                <%=Html.Hidden("LstLeaveLedger[" + j + "].fltCB", Model.LstLeaveLedger[j].fltCB.ToString())%>
                                <label>
                                    <%= Model.LstLeaveLedger[j].fltCB.ToString("#0.00")%></label>
                            </td>
                        </tr>
                        <%} %>
                        <%} %>
                    </table>
                </div>
            </div>
        </div>
        <%} %>
    </div>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <input id="btnSave" style="visibility: hidden" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AlternateApprovalProcess, LMS.Web.Permission.MenuOperation.Add))
      {%>
    <%if (Model.LeaveApplication.intAppStatusID == 6 || Model.LeaveApplication.intAppStatusID == 4)
      { %>
    <a href="#" class="btnApprove" onclick="return AlternateApprove();"></a>
    <%if (Model.intNodeID > 0)
      { %>
    <a href="#" class="btnRecommend" onclick="return AlternateRecommend();"></a>
    <%} %>
    <a href="#" class="btnReject" onclick="return AlternateReject();"></a>
    <%} %>
    <%} %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AlternateApprovalProcess, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.LeaveApplication.intApplicationID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return AlternateDelete();"></a>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
