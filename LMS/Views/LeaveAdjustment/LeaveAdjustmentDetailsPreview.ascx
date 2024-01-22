<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveAdjustmentPreview"));

        setTitle("Leave Adjustment Appication");

        $("#btnSave").hide();

        $("#btnDelete").hide();

    });


    function OnlineCancel() {
        var result = confirm('Pressing OK will cancel this adjustment application. Do you want to continue?');
        if (result == true) {
            Id = $('#LeaveApplication_intApplicationID').val();

            var targetDiv = "#divLeaveAdjustmentDetails";
            var url = "/LMS/LeaveAdjustment/OnlineCancel";
            var form = $("#frmLeaveAdjustmentPreview");
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

            var targetDiv = "#divLeaveAdjustmentDetails";
            var url = "/LMS/LeaveAdjustment/OnlineDelete";
            var form = $("#frmLeaveAdjustmentPreview");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }
    

</script>
<form id="frmLeaveAdjustmentPreview" method="post" action="" style="width: 100%">
<div id="divLeaveApplication">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.StrSearchLeaveYear) %>
            <%= Html.HiddenFor(m => m.StrYearStartDate) %>
            <%= Html.HiddenFor(m => m.StrYearEndDate) %>
            <%= Html.HiddenFor(m => m.intNodeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intApplicationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsApprovalProcess)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsDiscard)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intAppStatusID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveYearID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intDestNodeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpName)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strStatus) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveTypeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsAdjustment)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intRefApplicationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDesignationID) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDepartmentID) %>
            <%--Added by Shaiful, on 02 May, 2011--%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyFromDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyFromTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyToDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyToTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltWithPayDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltWithoutPayDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strPurpose)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strRemarks)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplicationType)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToDate)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltSubmittedDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithPayDuration)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithoutPayDuration)%>
            <%--End of add--%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.intLeaveTypeID)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.strLeaveType)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.strApplyDate)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.strApplicationType)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.strApplyFromDate)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.strApplyFromTime)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.strApplyToDate)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.strApplyToTime)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.fltDuration)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.fltWithPayDuration)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.fltWithoutPayDuration)%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.strPurpose)%>
        </div>
    </div>
        <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px">
        <tr>
            <td style="width: 75%;">
                <table class="contenttable" style="width: 100%;">
                     <tr>
                        <td style="width: 25%;">
                            Applicant ID<label class="labelRequired">*</label>
                        </td>
                        <td>  
                            <%= Html.Encode(Model.LeaveApplication.strEmpID)%>
                        </td>
                    </tr>                               
                    <tr>
                        <td style="width: 25%;">
                            Name
                        </td>
                        <td>                           
                            <%=Html.Encode(Model.LeaveApplication.strEmpName)%>  
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            Designation<label></label>
                        </td>
                        <td>
                            <%=Html.Encode(Model.LeaveApplication.strDesignation)%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            Department<label></label>
                        </td>
                        <td>
                            <%=Html.Encode(Model.LeaveApplication.strDepartment)%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            Branch<label></label>
                        </td>
                        <td>
                            <%=Html.Encode(Model.LeaveApplication.strBranch)%>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" style="width: 180px;" align="right">
                <%--<asp:Image runat="server" ID="imgProfilePic" ImageUrl="~/Content/img/defaultPic.jpg"
                    Style="width: 180px; height: 130px; padding-left: 20px;" ImageAlign="Right" />--%>
                    <img style="width: 180px; height: 130px; padding-left: 20px;" imagealign="Right" alt="" src="<%= Url.Content("~/Content/img/defaultPic.jpg")%>" />
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td style="width: 50%" valign="top" >
                <table class="contenttable" style="width: 100%;">
                    <tr>
                        <td colspan="4" class="contenttabletd">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="6" style="font-weight: bold">
                                        Approved Leave Application Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Leave Type
                                    </td>
                                    <td colspan="4">
                                        <%= Html.Encode(Model.RefLeaveApplication.strLeaveType)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Duration Type
                                    </td>
                                    <td colspan="4">
                                        <%if (String.Compare(Model.RefLeaveApplication.strApplicationType, "FullDay", true) == 0)
                                          {%>
                                        Full Day<% }
                                          else if (String.Compare(Model.RefLeaveApplication.strApplicationType, "Hourly", true) == 0)
                                          { %>
                                        Hourly<%}
                                          else
                                          {%>
                                        Full Day and/or Half Day
                                        <%}%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Half Day Info.
                                    </td>
                                    <td colspan="4">                                        
                                        <%=Html.Encode(Model.RefLeaveApplication.strHalfDayDurationFor)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Applied Date
                                    </td>
                                    <td colspan="4">
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyDate)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Leave Year
                                    </td>
                                    <td colspan="4">
                                        <%--<%= Html.Encode(Model.StrSearchLeaveYear)%>--%>
                                        <%= Html.Encode(Model.StrYearStartDate +" To "+Model.StrYearEndDate)%>
                                    </td>
                                </tr>
                                <tr>                                    
                                    <td colspan="2">
                                    </td>                                    
                                    <td colspan="2" style="width: 25%;">
                                        Granted Date
                                    </td>                                    
                                    <td colspan="2" style="width: 25%;">
                                        Granted Time
                                    </td>                                   
                                </tr>
                                <tr>                                    
                                    <td colspan="2">
                                        From
                                    </td>                                    
                                    <td colspan="2" style="width: 25%;">
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyFromDate)%>
                                    </td>                                    
                                    <td colspan="2" style="width: 20%;">
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyFromTime)%>
                                    </td>                                    
                                </tr>
                                <tr>                                    
                                    <td colspan="2">
                                        To
                                    </td>                                    
                                    <td colspan="2" style="width: 25%;">
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyToDate)%>
                                    </td>                                    
                                    <td colspan="2" style="width: 20%;">
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyToTime)%>
                                    </td>                                    
                                </tr>                                
                                <tr>
                                    <td colspan="2">
                                        Duration
                                    </td>
                                    <td colspan="4">
                                        <%= Html.Encode(Model.RefLeaveApplication.fltDuration)%>
                                        <%if (Model.RefLeaveApplication.IsHourly)
                                          { %>
                                        <label>
                                            Hours</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <label>
                                            Days</label>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        With Pay Duration
                                    </td>
                                    <td colspan="4">
                                        <%= Html.Encode(Model.RefLeaveApplication.fltWithPayDuration)%>
                                        <%if (Model.RefLeaveApplication.IsHourly)
                                          { %>
                                        <label>
                                            Hours</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <label>
                                            Days</label>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 30%;">
                                        Without Pay Duration
                                    </td>
                                    <td colspan="4">
                                        <%= Html.Encode(Model.RefLeaveApplication.fltWithoutPayDuration)%>
                                        <%if (Model.RefLeaveApplication.IsHourly)
                                          { %>
                                        <label>
                                            Hours</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <label>
                                            Days</label>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr style="height:50px;">
                                    <td valign="top">
                                        Purpose
                                    </td>
                                    <td colspan="5" valign="top">
                                        <div style="height: 30px; overflow-y: auto;">
                                            <%= Html.Encode(Model.RefLeaveApplication.strPurpose)%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table class="contenttable" style="width: 100%;">
                    <tr>
                        <td colspan="4" class="contenttabletd">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="4">                                        
                                        Adjustment Application Details:                                                                             
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">                                        
                                         Application Status                                       
                                    </td>
                                    <td colspan="2">                                        
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
                                    <td colspan="2">                                        
                                          Duration Type 
                                          <%= Html.HiddenFor(m => m.LeaveApplication.strApplicationType)%>                                     
                                    </td>
                                    <td colspan="2">                                       
                                        <%if (String.Compare(Model.LeaveApplication.strApplicationType, "FullDay", true) == 0)
                                            {%>
                                        Full Day<% }
                                            else if (String.Compare(Model.LeaveApplication.strApplicationType, "Hourly", true) == 0)
                                            { %>
                                        Hourly<%}
                                            else
                                            {%>
                                        Full Day and/or Half Day
                                        <%}%>                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Half Day Info.
                                    </td>
                                    <td colspan="2">                                        
                                        <%=Html.Encode(Model.LeaveApplication.strHalfDayDurationFor)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Apply Date
                                    </td>
                                    <td colspan="2">                                        
                                        <label><%=Model.LeaveApplication.strApplyDate%></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td style="width: 25%;">
                                        Leave Date
                                    </td>
                                    <td style="width: 30%;">
                                        Leave Time
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        From
                                    </td>
                                    <td style="width: 27%;">
                                        <%= Html.Encode(Model.LeaveApplication.strApplyFromDate)%>
                                    </td>
                                    <td style="width: 27%;">
                                        <%= Html.Encode(Model.LeaveApplication.strApplyFromTime)%>
                                    </td>
                                </tr>
                                <tr>                                    
                                    <td colspan="2">
                                        To
                                    </td>                                    
                                    <td style="width: 27%">
                                        <%= Html.Encode(Model.LeaveApplication.strApplyToDate)%>
                                    </td>
                                    <td style="width: 27%">
                                        <%= Html.Encode(Model.LeaveApplication.strApplyToTime)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Duration
                                    </td>
                                    <td colspan="2">
                                        <div style="float: left; text-align: left;">
                                            <%= Html.Encode(Model.LeaveApplication.fltDuration)%>
                                        </div>
                                        <div style="float: left; text-align: left; padding-left: 5px;">
                                            <%if (Model.LeaveApplication.IsHourly)
                                              { %>
                                            <label class="lblDaysHour">
                                                Hours</label>
                                            <%}
                                              else
                                              {
                                            %>
                                            <label class="lblDaysHour">
                                                Days</label>
                                            <%} %>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        With Pay Duration
                                    </td>
                                    <td colspan="2">
                                        <div style="float: left; text-align: left;">
                                            <%= Html.Encode(Model.LeaveApplication.fltWithPayDuration)%>
                                        </div>
                                        <div style="float: left; text-align: left; padding-left: 5px;">
                                            <%if (Model.LeaveApplication.IsHourly)
                                              { %>
                                            <label class="lblDaysHour">
                                                Hours</label>
                                            <%}
                                              else
                                              {
                                            %>
                                            <label class="lblDaysHour">
                                                Days</label>
                                            <%} %>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 35%;">
                                        Without Pay Duration
                                    </td>
                                    <td colspan="2">
                                        <div style="float: left; text-align: left;">
                                            <%= Html.Encode(Model.LeaveApplication.fltWithoutPayDuration)%>
                                        </div>
                                        <div style="float: left; text-align: left; padding-left: 5px;">
                                            <%if (Model.LeaveApplication.IsHourly)
                                              { %>
                                            <label class="lblDaysHour">
                                                Hours</label>
                                            <%}
                                              else
                                              {
                                            %>
                                            <label class="lblDaysHour">
                                                Days</label>
                                            <%} %>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top">
                                        Purpose
                                    </td>
                                    <td colspan="2" valign="top">
                                        <div style="height: 30px; overflow-y: auto;">
                                            <%=Html.Encode(Model.LeaveApplication.strPurpose)%>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height:50px;">
                                    <td colspan="2" valign="top">
                                        Comments
                                    </td>
                                    <td colspan="2" valign="top">
                                        <div style="height: 30px; overflow-y: auto;">
                                            <%= Html.Encode(Model.LeaveApplication.strRemarks)%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
                <%=Html.Encode(appFlow.strAuthorID + "-" + appFlow.strAuthorName)%>
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
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
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
    <%if (Model.LeaveApplication.intAppStatusID != 1 && Model.LeaveApplication.intAppStatusID != 2 && Model.LeaveApplication.intAppStatusID != 3)
      { %>
    <%if (Model.LeaveApplication.intApplicationID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OnlineLeaveAdjustment, LMS.Web.Permission.MenuOperation.Cancel))
      {%>
    <a href="#" class="btnCancel" onclick="return OnlineCancel();"></a>
    <%} %>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
