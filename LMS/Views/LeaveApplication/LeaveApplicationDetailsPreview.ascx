<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveApplication"));

        setTitle("Online Leave Application");

        $("#btnSave").hide();

        $("#btnDelete").hide();

    });

    function OnlineCancel() {

        var result = confirm('Pressing OK will cancel this application. Do you want to continue?');
        if (result == true) {
            Id = $('#LeaveApplication_intApplicationID').val();

            var targetDiv = "#divLeaveApplicationDetails";
            var url = "/LMS/LeaveApplication/OnlineCancel";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }


    function OnlineDelete() {
        Id = $('#LeaveApplication_intApplicationID').val();


        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {


            Id = $('#LeaveApplication_intApplicationID').val();

            targetDiv = "#divLeaveApplicationDetails";

            var url = "/LMS/LeaveApplication/OnlineDelete";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function PrintApplication() {
        var applicationId = $('#LeaveApplication_intApplicationID').val();
        var url='/LMS/RdlcReport/PrintLeaveApplication'+'?applicationId='+applicationId;
        $.ajax({
            url: url,
//            data: { applicationId: applicationId },
            success: function (result) {
                console.log(url);
                window.open(url, '_blank');
          },
            error: function (msg) {
                console.log('error');
            }
            })           
    }
</script>
<form id="frmLeaveApplication" method="post" action="" style="width: 100%">
<div id="divLeaveApplication">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.LeaveApplication.intApplicationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsApprovalProcess)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsDiscard)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intAppStatusID)%>
            <%= Html.HiddenFor(m => m.intNodeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveYearID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveTypeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intDestNodeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strStatus) %>
            <%= Html.HiddenFor(m => m.StrSearchLeaveYear) %>
            <%= Html.HiddenFor(m => m.StrYearStartDate) %>
            <%= Html.HiddenFor(m => m.StrYearEndDate) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDesignationID) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDepartmentID) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpInitial)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpName)%>
            <%--Added by Mamun, on 04 April, 2011--%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strPurpose)%>
            <%= Html.HiddenFor(m=> m.LeaveApplication.strApplyDate) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyFromDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyFromTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyToDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyToTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltWithPayDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltWithoutPayDuration)%>
            <%--End of edit--%>
            <%--Added by Mamun, on 19 April, 2011--%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplicationType)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltSubmittedDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithPayDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithoutPayDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strPLID)%>
            <%--End of edit--%>
        </div>
    </div>
    <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
        <tr>
            <td style="width: 75%;">
                <table class="contenttable" style="width: 100%; padding: 0px;">
                    <tr>
                        <td style="width: 19%;">
                            Applicant ID
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
                        <td style="width: 17%;">
                            Department
                        </td>
                        <td>
                            <%=Html.Encode(Model.LeaveApplication.strDepartment)%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19%;">
                            Application Status
                        </td>
                        <td>
                            <%if (Model.LeaveApplication.intApplicationID > 0)
                              { %>
                            <label style="font-weight: bold">
                                <%= LMS.Web.Utils.GetApplicationStatus(Model.LeaveApplication.intAppStatusID)%></label>
                            <%}
                              else
                              {
                            %>
                            <label style="font-weight: bold">
                                New</label>
                            <%} %>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="width: 17%;">
                            Division/Unit
                        </td>
                        <td>
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
                <%= Html.Encode(Model.LeaveApplication.strLeaveType) %>
            </td>
            <td style="width: 15%;">
                Applied Date
            </td>
            <td>
                <label>
                    <%=Model.LeaveApplication.strApplyDate %></label>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                From
            </td>
            <td>
                <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromDate)%>
            </td>
            <td style="width: 15%">
                To
            </td>
            <td>
                <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyToDate)%>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                Duration
            </td>
            <td>
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
        <%--        <%if (Model.LeaveApplication.strPLID != null)
          { %>
        <tr>
            <td style="width: 15%;">
                PL
            </td>
            <td>
                <%if (!string.IsNullOrEmpty(Model.LeaveApplication.strPLID))
                  { %>
                <%=Html.Encode(Model.LeaveApplication.strPLInitial + " - " + Model.LeaveApplication.strPLName)%>
                <%} %>
            </td>
        </tr>
        <%
          } %>--%>
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
    <%-- <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
        <tr>
            <td style="width: 37%" valign="top">
                <table class="contenttable" style="width: 100%; padding: 0px;">
                    <tr>
                        <td style="width: 35%;">
                            Leave Year
                        </td>
                        <td>
                            <%= Html.Encode(Model.StrYearStartDate +" To "+Model.StrYearEndDate)%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 35%;">
                            Applied Date
                        </td>
                        <td>
                            <label>
                                <%=Model.LeaveApplication.strApplyDate %></label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 35%;">
                            Leave Type
                        </td>
                        <td>
                            <%= Html.Encode(Model.LeaveApplication.strLeaveType) %>
                        </td>
                    </tr>
                    <%if (Model.LeaveApplication.strSupervisorID != null)
                      { %>
                    <tr>
                        <td style="width: 35%;">
                            Rec. Person
                        </td>
                        <td>
                            <%=Html.Encode(Model.LeaveApplication.strSupervisorName)%>
                        </td>
                    </tr>
                    <%
                      } %>
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
                        </td>
                        <td colspan="2" class="contenttabletd" style="text-align: right;">
                            Application Status:
                            <%if (Model.LeaveApplication.intApplicationID > 0)
                              { %>
                            <label style="font-weight: bold">
                                <%= LMS.Web.Utils.GetApplicationStatus(Model.LeaveApplication.intAppStatusID)%></label>
                            <%}
                              else
                              {
                            %>
                            <label style="font-weight: bold">
                                New</label>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="contenttabletd">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td style="width: 20%">
                                        Leave Date
                                    </td>
                                    <td style="width: 20%">
                                        <label id="lblTime">
                                            Leave Time</label>
                                    </td>
                                    <td style="width: 30%">
                                    </td>
                                    <td colspan="2" style="width: 10%">
                                        <%if (Model.LeaveApplication.strSubmittedApplicationType == "Hourly")
                                          { %>
                                        <label id="lblDaysHour">
                                            Hour</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <label id="lblDaysHour">
                                            Days</label>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        From
                                    </td>
                                    <td style="width: 20%">
                                        <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromDate)%>
                                    </td>
                                    <td style="width: 20%">
                                        <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromTime)%>
                                    </td>
                                    <td style="width: 30%">
                                        Duration
                                    </td>
                                    <td colspan="2" style="width: 10%">
                                        <%=Html.Encode(Model.LeaveApplication.fltSubmittedDuration)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        To
                                    </td>
                                    <td style="width: 20%">
                                        <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyToDate)%>
                                    </td>
                                    <td style="width: 20%">
                                        <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyToTime)%>
                                    </td>
                                    <td style="width: 30%">
                                        With Pay Duration
                                    </td>
                                    <td colspan="2" style="width: 10%">
                                        <%=Html.Encode(Model.LeaveApplication.fltSubmittedWithPayDuration)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Duration Type
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
                                          {                                         
                                        %>Full Day and/or Half Day<%}%>
                                    </td>
                                    <td>
                                        Without Pay Duration
                                    </td>
                                    <td colspan="2">
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
                                    <td style="width: 20%">
                                    </td>
                                    <td style="width: 20%">
                                        Granted Date
                                    </td>
                                    <td style="width: 20%">
                                        Granted Time
                                    </td>
                                    <td style="width: 30%">
                                    </td>
                                    <td colspan="2" style="width: 10%">
                                        <%if (Model.LeaveApplication.strApplicationType == "Hourly")
                                          { %>
                                        <label id="Label2">
                                            Hour</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <label id="Label3">
                                            Days</label>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        From
                                    </td>
                                    <td style="width: 20%">
                                        <%=Html.Encode(Model.LeaveApplication.strApplyFromDate)%>
                                    </td>
                                    <td style="width: 20%">
                                        <%=Html.Encode(Model.LeaveApplication.strApplyFromTime)%>
                                    </td>
                                    <td style="width: 30%">
                                        Duration
                                    </td>
                                    <td colspan="2" style="width: 10%">
                                        <%=Html.Encode(Model.LeaveApplication.fltDuration)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        To
                                    </td>
                                    <td style="width: 20%">
                                        <%=Html.Encode(Model.LeaveApplication.strApplyToDate)%>
                                    </td>
                                    <td style="width: 20%">
                                        <%=Html.Encode(Model.LeaveApplication.strApplyToTime)%>
                                    </td>
                                    <td style="width: 30%">
                                        With Pay Duration
                                    </td>
                                    <td colspan="2" style="width: 10%">
                                        <%=Html.Encode(Model.LeaveApplication.fltWithPayDuration)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Duration Type
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
                                        Without Pay Duration
                                    </td>
                                    <td colspan="2">
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
                <%=Html.Encode(appFlow.strAuthorInitial + " - " + appFlow.strAuthorName)%>
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
                    <%  if (Model.LstLeaveLedger != null)
                        {
                            for (int j = 0; j < Model.LstLeaveLedger.Count; j++)
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
                                <%= Model.LstLeaveLedger[j].fltOB.ToString()%></label>
                        </td>
                        <td style="width: 15%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltEntitlement", Model.LstLeaveLedger[j].fltEntitlement.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltEntitlement.ToString()%></label>
                        </td>
                        <td style="width: 7%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltAvailed", Model.LstLeaveLedger[j].fltAvailed.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltAvailed.ToString("#0.00")%></label>
                        </td>
                        <td style="width: 10%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltEncased", Model.LstLeaveLedger[j].fltEncased.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltEncased.ToString()%></label>
                        </td>
                        <td style="width: 7%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltApplied", Model.LstLeaveLedger[j].fltApplied.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltApplied.ToString("#0.00")%></label>
                        </td>
                        <td style="width: 10%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltCB", Model.LstLeaveLedger[j].fltCB.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltCB.ToString("#0.00")%></label>
                        </td>
                    </tr>
                    <%}
                        } %>
                </table>
            </div>
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
    <%if (Model.LeaveApplication.intApplicationID <= 0)
      { %>
    <a href="#" class="btnSubmit" onclick="return OnlineSubmit();"></a>
    <% }%>
    <input id="btnSave" style="visibility: hidden" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%if (Model.LeaveApplication.intAppStatusID != 1 && Model.LeaveApplication.intAppStatusID != 2 && Model.LeaveApplication.intAppStatusID != 3 && Model.LeaveApplication.intAppStatusID != 4)
      { %>
    <%if (Model.LeaveApplication.intApplicationID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OnlineLeaveApplication, LMS.Web.Permission.MenuOperation.Cancel))
      {%>
    <a href="#" class="btnCancel" onclick="return OnlineCancel();"></a>
    <%} %>
    <%} %>
    <%} %>

    <%if (Model.LeaveApplication.intApplicationID > 0)
     { %>
          <a href="#" class="btnPrint" id="btnPrint" onclick="return PrintApplication();"></a>
     <%} %>

    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
