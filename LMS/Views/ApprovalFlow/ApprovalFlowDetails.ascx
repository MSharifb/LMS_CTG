<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ApprovalFlowModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmApprovalFlow"));

        setTitle("Approval Flow");

        $("#btnReject").hide();

        $("#btnApprove").hide();

        $("#btnRecommend").hide();

        //$(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        //        FullDayHourly($("#ApplicationType"));

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

    jQuery(function () {

        //        $("#LeaveApplication_strApplyFromTime").timePicker({
        //            startTime: "12.00",  // Using string. Can take string or Date object.
        //            endTime: new Date(0, 0, 0, 23, 59, 0),  // Using Date object here.
        //             showOn: 'button',  buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>' , buttonImageOnly: true,
        //            show24Hours: false,
        //            separator: '.',
        //            step: 15
        //        });


        //        $("#LeaveApplication_strApplyToTime").timepicker({ 
        //                    ampm: true
        //                    , showOn: 'button'
        //                    , buttonImage: '<%= Url.Content("~/Content/img/controls/clock2.png")%>'
        //                    , buttonImageOnly: true
        //                    , stepMinute: 15
        //                    ,minDate: new Date(0, 0, 0, 12, 59, 0), maxDate: new Date(0, 0, 0, 23, 59, 0)
        //                     });


    });

    // Added For MPA
    function CalculateDuration() {
        var url = "/LMS/ApprovalFlow/CalculateDuration";
        var form = $("#frmApprovalFlow");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            if ($('#ApplicationType').val() == "Hourly") {
                if (result < 0) {
                    result = 0;
                    alert("Leave To Time must be equal or greater than Leave From Time.");
                }
            }
            $("#LeaveApplication_fltDuration").val(result);
            $("#LeaveApplication_fltWithPayDuration").val(result);
            $("#LeaveApplication_fltWithoutPayDuration").val(0);

            CalculateNetBalance();

        }, "json");

        return false;
    }

    function SplitDuration(obj) {
        var objDuration = document.getElementById('LeaveApplication_fltDuration');
        var objfltWithPayDuration = document.getElementById('LeaveApplication_fltWithPayDuration');
        var objfltWithoutPayDuration = document.getElementById('LeaveApplication_fltWithoutPayDuration');

        //if value change for With pay duration
        if (obj.id == objfltWithPayDuration.id) {
            if (objfltWithPayDuration.value == "" || objfltWithPayDuration.value == ".") {
                objfltWithPayDuration.value = 0;
            }

            if (parseFloat(objfltWithPayDuration.value) > parseFloat(objDuration.value)) {
                objfltWithoutPayDuration.value = 0;
                objfltWithPayDuration.value = objDuration.value;
            }
            else {
                if (!isNaN(objfltWithPayDuration.value)) {
                    objfltWithoutPayDuration.value = (parseFloat(objDuration.value) - parseFloat(objfltWithPayDuration.value)).toFixed(2);
                }
            }
        }

        //if value change for without pay duration
        else if (obj.id == objfltWithoutPayDuration.id) {
            if (objfltWithoutPayDuration.value == "" || objfltWithoutPayDuration.value == ".") {
                objfltWithoutPayDuration.value = 0;
            }

            if (parseFloat(objfltWithoutPayDuration.value) > parseFloat(objDuration.value)) {
                objfltWithPayDuration.value = 0;
                objfltWithoutPayDuration.value = objDuration.value;
            }
            else {
                if (!isNaN(objfltWithoutPayDuration.value)) {
                    objfltWithPayDuration.value = (parseFloat(objDuration.value) - parseFloat(objfltWithoutPayDuration.value)).toFixed(2);
                }
            }
        }
    }

    // Added For MPA
    function CalculateNetBalance() {

        var url = '/LMS/ApprovalFlow/CalculateNetBalance';
        var form = $("#frmApprovalFlow");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) {
            $("#LeaveApplication_fltNetBalance").val(result);
        }, "json");
    }

    function searchEmployee() {
        window.parent.openEmployee();
    }

    function save(type) {
        var confresult;
        var isAdjust = $('#LeaveApplication_bitIsAdjustment').val();
        var netBL = $('#LeaveApplication_fltNetBalance').val();
        var LWP = $('#LeaveApplication_fltWithoutPayDuration').val();
        var strCOM = $('#ApprovalFlow_strComments').val();

        if (type == 3) {
            if (jQuery.trim(strCOM) == "") {
                alert("Please input author comments.");
                return false;
            }

            confresult = confirm('Pressing OK will reject this application. Do you want to continue?');
            if (confresult == false) {
                return false;
            }

            $('#ApprovalFlow_strDestAuthorID').removeClass('required');
        }
        else {
            if (type == 4) {
                if ($('#ApprovalFlow_strDestAuthorID').val() == "") {
                    alert("Please select recommender.");
                    return false;
                }
            }

            if (parseInt(LWP) > 0) {
                confresult = confirm('This application has leave without pay duration. Do you want to proceed?');
                if (confresult == false) {
                    return false;
                }
            }

            if (isAdjust == false && parseInt(netBL) < 0) {
                confresult = confirm('Net balance is ' + netBL.toString() + '. Do you want to proceed?');
                if (confresult == false) {
                    return false;
                }
            }
            else {
                var fltSubDur;
                var fltDur;
                var fltOffHr = "<%= LMS.Web.LoginInfo.Current.fltOfficeTime %>";

                if ($('#LeaveApplication_strSubmittedApplicationType').val() == 'Hourly') {
                    fltSubDur = $('#LeaveApplication_fltSubmittedDuration').val();
                }
                else {
                    fltSubDur = $('#LeaveApplication_fltSubmittedDuration').val() * fltOffHr;
                }

                if ($('#ApplicationType').val() == 'Hourly') {
                    fltDur = $('#LeaveApplication_fltDuration').val();
                }
                else {
                    fltDur = $('#LeaveApplication_fltDuration').val() * fltOffHr;
                }

                if (fltDur > fltSubDur) {
                    alert("Grant duration cannot be greater than submitted duration.");
                    return false;
                }
            }
        }

        if (fnValidate() && checkDateValidation() == true) {
            if ($("#IsFromUrl").val() == "True") {
                targetDiv = "#divDataList";
            }
            else {
                targetDiv = "#divApprovalFlowDetails";
            }

            if (isAdjust.toString() == "False") {
                var url = '/LMS/ApprovalFlow/ValidateLeaveApplication';
                var form = $('#frmApprovalFlow');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) {
                    if (result[0] != null && result[0] != "") {
                        alert(result);
                        return false;
                    }
                    else {
                        url = "/LMS/ApprovalFlow/SaveApprovalFlow/" + type;
                        serializedForm = form.serialize();
                        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
                    }

                }, "json");
            }
            else {
                var url = "/LMS/ApprovalFlow/SaveApprovalFlow/" + type;
                var form = $('#frmApprovalFlow');
                var serializedForm = form.serialize();
                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
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

    // Added For MPA
    function HookupCalculation(obj) {
        if (obj.id == 'LeaveApplication_strApplyFromDate' || $('#ApplicationType').val() == "Hourly") {
            $('#LeaveApplication_strApplyToDate').val($('#LeaveApplication_strApplyFromDate').val());
        }

        if (obj.id == 'LeaveApplication_strApplyFromTime') {
            $('#LeaveApplication_strApplyToTime').val($('#LeaveApplication_strApplyFromTime').val());
        }

        CalculateDuration();
        return false;
    }

    function parseDate(str) {
        var dmy = str.split('-');
        var mdy = dmy[1] + '/' + dmy[0] + '/' + dmy[2];
        return new Date(mdy);
    }

    function checkDateValidation() {
        var isAdjust = $("#LeaveApplication_bitIsAdjustment").val();
        var isServiceLifeType = $('#LeaveApplication_isServiceLifeType').val();

        if ($('#LeaveApplication_strApplyFromDate').val() != "" && $('#LeaveApplication_strApplyToDate').val() != "") {

            var pdtSUBFrom = parseDate($('#LeaveApplication_strSubmittedApplyFromDate').val());
            var pdtSUBTo = parseDate($('#LeaveApplication_strSubmittedApplyToDate').val());

            var pdtAPFrom = parseDate($('#LeaveApplication_strApplyFromDate').val());
            var pdtAPTo = parseDate($('#LeaveApplication_strApplyToDate').val());



            var pdtYStart = parseDate($('#StrYearStartDate').val());
            var pdtYEnd = parseDate($('#StrYearEndDate').val());
        }

        return true;
    }

    // Added For MPA
    function refreshDuration() {
        document.getElementById('LeaveApplication_fltDuration').value = 0;
        document.getElementById('LeaveApplication_fltWithPayDuration').value = 0;
        document.getElementById('LeaveApplication_fltWithoutPayDuration').value = 0;
        document.getElementById('LeaveApplication_strApplyFromTime').value = '';
        document.getElementById('LeaveApplication_strApplyToTime').value = '';

        CalculateDuration();
    }

    // newly Add by Nazrul 

    $(document).ready(function () {

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

    });


    function setData(id, strEmpInitial, name) {
        var strSrc = document.getElementById('StrEmpSearch').value;
      
        if (strSrc == '0') {

            document.getElementById('ApprovalFlow_strDestAuthorInitial').value = id;
            document.getElementById('strDestAuthorName').value = name;
            document.getElementById('StrEmpSearch').value = id;

            var url = "/LMS/ApprovalFlow/GetEmployeeInfo";
            var form = $("#frmApprovalFlow");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                document.getElementById('ApprovalFlow_strDestAuthorID').value = result[6];
            }, "json");

            $("#divEmpList").dialog('close');
        }

    }

    function Closing() {

    }

    function openEmployee(id) {

        document.getElementById('StrEmpSearch').value = id;

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


    function handleEnter(evt, srcflag) {
        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;
        }

        else if (evt) keyCode = evt.which;
        else return true;

        if (keyCode == 13) {
            //handle enter
            var id = "";
            var name = "";
            document.getElementById('StrEmpSearch').value = srcflag;
            if (srcflag == '0') {
                id = document.getElementById('ApprovalFlow_strDestAuthorID').value;
            }
            setData(id, name);
        }
        return true;
    }
</script>
<form id="frmApprovalFlow" method="post" action="">
<div>
    <div id="divApprovalFlow">
        <div style="width: 100%">
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
                    <%= Html.HiddenFor(m => m.StrYearStartDate) %>
                    <%= Html.HiddenFor(m => m.StrYearEndDate) %>
                    <%= Html.HiddenFor(m => m.intNextNodeID) %>
                    <%= Html.HiddenFor(m => m.strHalfDayFromTime) %>
                    <%= Html.HiddenFor(m => m.strHalfDayToTime) %>
                    <%= Html.HiddenFor(m => m.StrEmpSearch)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedHalfDayFor)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.intSubmittedDurationID)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.isServiceLifeType)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveTypeID)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.strLeaveType)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplicationType)%>
                    <%--                    <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithPayDuration)%>
                    <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithoutPayDuration)%>--%>
                    <%--
                    <%=Html.HiddenFor(m => m.LeaveApplication.fltWithoutPayDuration, new { @onchange = "SplitDuration(this);" })%>
                    <%=Html.HiddenFor(m => m.LeaveApplication.fltWithPayDuration, new { @onchange = "SplitDuration(this)" })%>
                    <%=Html.HiddenFor(m => m.LeaveApplication.strApplyFromDate)%>
                    <%=Html.HiddenFor(m => m.LeaveApplication.strApplyToDate)%>
                    <%=Html.HiddenFor(m => m.LeaveApplication.fltDuration)%>--%>
                    <%=Html.HiddenFor(m => m.LeaveApplication.strApplicationType)%>
                </div>
            </div>
            <div class="divRow">
                <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px">
                    <tr>
                        <td style="width: 75%;">
                            <table class="contenttable" style="width: 100%;">
                                <tr>
                                    <td style="width: 19%;">
                                        Applicant ID<label></label>
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
                                <%--Added For MPA--%>
                                <tr>
                                    <td style="width: 24%;">
                                        Department<label></label>
                                    </td>
                                    <td>
                                        <%= Html.HiddenFor(m => m.LeaveApplication.strDepartmentID)%>
                                        <%=Html.Encode(Model.LeaveApplication.strDepartment)%>
                                    </td>
                                </tr>
                                <%--End For MPA--%>
                                <tr>
                                    <td style="width: 19%;">
                                        Application Status
                                    </td>
                                    <td>
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <label>
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
                            </table>
                        </td>
                        <td valign="top" style="width: 180px;" align="right">
                            <%--<asp:Image runat="server" ID="imgProfilePic" ImageUrl="~/Content/img/defaultPic.jpg"
                                Style="width: 180px; height: 130px; padding-left: 20px;" ImageAlign="Right" />--%>
                            <img style="width: 180px; height: 130px; padding-left: 20px;" imagealign="Right"
                                alt="" src="<%= Url.Content("~/Content/img/defaultPic.jpg")%>" />
                        </td>
                    </tr>
                </table>
                <br />
                <table class="contenttext" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 15%;">
                            Leave Type<label></label>
                        </td>
                        <td>
                            <%= Html.Encode(Model.LeaveApplication.strLeaveType)%>
                        </td>
                        <td style="width: 15%;">
                            Applied Date
                        </td>
                        <td>
                            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>
                            <%= Html.Encode(Model.LeaveApplication.strApplyDate)%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Applied From
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromDate)%>
                            <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromDate)%>
                            <%--&nbsp;
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyFromTime)%>
                                        <%if (Model.LeaveApplication.strSubmittedHalfDayFor == "F")
                                          { %>
                                        <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromTime + " - " + Model.LeaveApplication.strSubmittedApplyToTime)%>
                                        <%}
                                          else if (Model.LeaveApplication.strSubmittedHalfDayFor == null)
                                          { %>
                                        <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromTime)%>
                                        <%} %>--%>
                        </td>
                        <td style="width: 15%;">
                            To
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToDate)%>
                            <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyToDate)%>
                            <%-- &nbsp;
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strSubmittedApplyToTime)%>
                                        <%if (Model.LeaveApplication.strSubmittedHalfDayFor == "T")
                                          { %>
                                        <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyFromTime + " - " + Model.LeaveApplication.strSubmittedApplyToTime)%>
                                        <%}
                                          else if (Model.LeaveApplication.strSubmittedHalfDayFor == null)
                                          { %>
                                        <%=Html.Encode(Model.LeaveApplication.strSubmittedApplyToTime)%>
                                        <%} %>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Applied Duration
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedDuration)%>
                            <%=Html.Encode(Model.LeaveApplication.fltSubmittedDuration)%>
                            &nbsp;
                            <% if (String.Compare(Model.LeaveApplication.strSubmittedApplicationType, "Hourly", true) == 0)
                               { %>
                            <label id="lbl">
                                Hours</label>
                            <%}
                               else
                               { %>
                            <label id="Label1">
                                Days</label>
                            <%} %>
                        </td>
                        <td>
                            With Pay Duration
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithPayDuration)%>
                            <%=Html.Encode(Model.LeaveApplication.fltSubmittedWithPayDuration)%>
                            &nbsp;
                            <% if (String.Compare(Model.LeaveApplication.strSubmittedApplicationType, "Hourly", true) == 0)
                               { %>
                            <label id="Label6">
                                Hours</label>
                            <%}
                               else
                               { %>
                            <label id="Label7">
                                Days</label>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Without Pay Duration
                        </td>
                        <td>
                            <%=Html.HiddenFor(m => m.LeaveApplication.fltSubmittedWithoutPayDuration)%>
                            <%=Html.Encode(Model.LeaveApplication.fltSubmittedWithoutPayDuration)%>
                            &nbsp;
                            <% if (String.Compare(Model.LeaveApplication.strSubmittedApplicationType, "Hourly", true) == 0)
                               { %>
                            <label id="Label8">
                                Hours</label>
                            <%}
                               else
                               { %>
                            <label id="Label9">
                                Days</label>
                            <%} %>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="contenttext" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 15%;">
                            Purpose
                        </td>
                        <td style="width: 37%;">
                            <div style="height: 30px; overflow-y: auto;">
                                <%=Html.HiddenFor(m => m.LeaveApplication.strPurpose)%>
                                <%=Html.Encode(Model.LeaveApplication.strPurpose)%>
                            </div>
                        </td>
                        <td style="width: 15%;">
                            Address During Leave
                        </td>
                        <td>
                            <div style="height: 30px; overflow-y: auto;">
                                <%=Html.HiddenFor(m => m.LeaveApplication.strContactAddress)%>
                                <%=Html.Encode(Model.LeaveApplication.strContactAddress)%></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Contact No.
                        </td>
                        <td style="width: 37%;">
                            <%=Html.HiddenFor(m => m.LeaveApplication.strContactNo)%>
                            <%=Html.Encode(Model.LeaveApplication.strContactNo)%>
                        </td>
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
                    <%-- <tr>
                        <td style="width: 15%;">
                            Comments
                        </td>
                        <td>
                            <div style="height: 30px; overflow-y: auto;">
                                <%=Html.HiddenFor(m => m.LeaveApplication.strRemarks) %>
                                <%=Html.Encode(Model.LeaveApplication.strRemarks)%>
                            </div>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            Granted From
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromDate, new { @class = "textRegularDate dtPicker date", @onchange = "return HookupCalculation(this);" })%>
                        </td>
                        <td>
                            To<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToDate, new { @class = "textRegularDate required dtPicker date", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Duration<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onkeyup = "return ismaxlengthPop(this)", @onchange = "return HookupCalculation(this);" })%>
                            &nbsp;
                            <%if (Model.LeaveApplication.IsHourly)
                              { %>
                            <label id="lblDaysHour1" class="lblDaysHour">
                                Hours</label>
                            <%}
                              else
                              {
                            %>
                            <label id="lblDaysHour2" class="lblDaysHour">
                                Days</label>
                            <%} %>
                        </td>
                        <td>
                            With Pay Duration<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltWithPayDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onchange = "SplitDuration(this)" })%>
                            &nbsp;
                            <%if (Model.LeaveApplication.IsHourly)
                              { %>
                            <label id="lblDaysHour3" class="lblDaysHour">
                                Hours</label>
                            <%}
                              else
                              {
                            %>
                            <label id="lblDaysHour4" class="lblDaysHour">
                                Days</label>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Without Pay Duration<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltWithoutPayDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onchange = "SplitDuration(this);" })%>
                            &nbsp;
                            <%if (Model.LeaveApplication.IsHourly)
                              { %>
                            <label id="lblDaysHour5" class="lblDaysHour">
                                Hours</label>
                            <%}
                              else
                              {
                            %>
                            <label id="lblDaysHour6" class="lblDaysHour">
                                Days</label>
                            <%} %>
                        </td>
                        <%if (Model.LeaveApplication.bitIsAdjustment == false && (Model.LeaveApplication.intAppStatusID == 4 || Model.LeaveApplication.intAppStatusID == 6))
                          { %>
                        <td style="font-weight: bold;">
                            Net Balance
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltNetBalance, new { @class = " textRegularDuration double", @maxlength = "5", @readonly = "readonly" })%>
                        </td>
                        <%} %>
                    </tr>
                </table>
                <br />
                <table class="contenttext" cellspacing="0" style="width: 100%;">
                    <%--<% if (Model.intNextNodeID > 0)
                       { %>
                    <tr>
                        <td style="width: 15%;">
                            Next Recommender
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td>
                            <%if (Model.Approver.Count > 0)
                              { %>
                            <%= Html.DropDownListFor(m => m.ApprovalFlow.strDestAuthorID, Model.Approver, new { @class = "selectBoxRegular", @style = "width:250px;" })%>
                            <%}
                              else
                              { %>
                            <%= Html.DropDownListFor(m => m.ApprovalFlow.strDestAuthorID, Model.Approver, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:250px;" })%>
                            <%} %>
                        </td>
                    </tr>
                    <%} %>--%>
                    <tr>
                        <td style="width: 15%;">
                           <%=Html.HiddenFor(m => m.ApprovalFlow.strDestAuthorID)%>
                            Next Rec./Approver
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.ApprovalFlow.strDestAuthorInitial, new { @class = "textRegularDate", @readonly = "readonly" })%>
                            <a href="#" class="btnSearch" onclick="return openEmployee('0');"></a>
                        </td>                       
                    </tr>
                    <tr>                  
                    <td style="width: 15%;">
                            Name
                        </td>
                        <td>
                            <%=Html.TextBox("strDestAuthorName", "", new { @class = "textRegular", @readonly = "readonly"})%>
                        </td>
                      <%--  strSupervisorName--%>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Comments
                        </td>
                        <td>
                            <%=Html.TextAreaFor(m => m.ApprovalFlow.strComments, new { @class = "textRegular", @style = "width:250px;height:85px", @maxlength = 200, onkeyup = "return ismaxlengthPop(this)" })%>
                        </td>
                    </tr>
                </table>
                <%--<table class="contenttext" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 50%;" valign="top">
                        </td>
                        <td valign="top">
                            <table style="width: 100%;" cellspacing="0">
                                <tr>
                                    <td colspan="2">
                                        <%=Html.RadioButton("LeaveApplication.strApplicationType", "FullDay", Model.LeaveApplication.IsFullDay, new { onclick = "FullDayHourly(this); refreshDuration(); " })%>Full
                                        Day
                                        <% if (LMS.Web.LoginInfo.Current.strAllowHourlyLeave == "Yes")
                                           {%>
                                        <%=Html.RadioButton("LeaveApplication.strApplicationType", "Hourly", Model.LeaveApplication.IsHourly, new { @style = "visibility: visible", onclick = "FullDayHourly(this); refreshDuration(); " })%>Hourly
                                        <%}
                                           else
                                           {%>
                                        <%=Html.RadioButton("LeaveApplication.strApplicationType", "Hourly", Model.LeaveApplication.IsHourly, new { @style = "visibility: hidden", onclick = "FullDayHourly(this); refreshDuration(); " })%>
                                        <%} %>
                                        <%=Html.RadioButton("LeaveApplication.strApplicationType", "FullDayHalfDay", Model.LeaveApplication.IsFullDayHalfDay, new { onclick = "FullDayHourly(this); refreshDuration(); " })%>Full
                                        Day and/or Half Day
                                        <%=Html.Hidden("ApplicationType", Model.LeaveApplication.strApplicationType)%>
                                    </td>
                                </tr>
                                <tr id="trHalfDayFor" style="display: none;">
                                    <td colspan="2">
                                        <div style="width: 100%; float: left; text-align: left;">
                                            <div style="float: left; text-align: left; padding-left: 8px;">
                                                <%= Html.DropDownListFor(m => m.LeaveApplication.intDurationID, Model.WorkingTime, "...Select One...", new { @class = "selectBoxRegular", @style = "width:130px; min-width:130px;",onchange = "return GetWoringTime();" })%>
                                            </div>
                                            <div style="float: left; text-align: left; padding-left: 8px;">
                                                Of
                                                <%= Html.DropDownListFor(m => m.LeaveApplication.strHalfDayFor, Model.HalfDayFor, "...Select One...", new { @class = "selectBoxRegular", @style = "width:130px; min-width:130px;" })%>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Granted From
                                        <label class="labelRequired">
                                            *</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromDate, new { @class = "textRegularDate dtPicker date", @onchange = "return HookupCalculation(this);" })%>
                                        &nbsp;
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromTime, new { @class = "textRegularTime timepicker", @readonly = "readonly", @onchange = "return HookupCalculation(this);" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        To<label class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToDate, new { @class = "textRegularDate required dtPicker date", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>
                                        &nbsp;
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToTime, new { @class = "textRegularTime timepicker", @readonly = "readonly", @onchange = "return HookupCalculation(this);" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Duration<label class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.fltDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onkeyup = "return ismaxlengthPop(this)", @onchange = "return HookupCalculation(this);" })%>
                                        &nbsp;
                                        <%if (Model.LeaveApplication.IsHourly)
                                          { %>
                                        <label id="lblDaysHour1" class="lblDaysHour">
                                            Hours</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <label id="lblDaysHour2" class="lblDaysHour">
                                            Days</label>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        With Pay Duration<label class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.fltWithPayDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onchange = "SplitDuration(this)" })%>
                                        &nbsp;
                                        <%if (Model.LeaveApplication.IsHourly)
                                          { %>
                                        <label id="lblDaysHour3" class="lblDaysHour">
                                            Hours</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <label id="lblDaysHour4" class="lblDaysHour">
                                            Days</label>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Without Pay Duration<label class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.fltWithoutPayDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onchange = "SplitDuration(this);" })%>
                                        &nbsp;
                                        <%if (Model.LeaveApplication.IsHourly)
                                          { %>
                                        <label id="lblDaysHour5" class="lblDaysHour">
                                            Hours</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <label id="lblDaysHour6" class="lblDaysHour">
                                            Days</label>
                                        <%} %>
                                    </td>
                                </tr>
                                <%if (Model.LeaveApplication.bitIsAdjustment == false && (Model.LeaveApplication.intAppStatusID == 4 || Model.LeaveApplication.intAppStatusID == 6))
                                  { %>
                                <tr>
                                    <td style="font-weight: bold;">
                                        Net Balance
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.fltNetBalance, new { @class = " textRegularDuration double", @maxlength = "5", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                                <%} %>
                                <% if (Model.intNextNodeID > 0)
                                   { %>
                                <tr>
                                    <td>
                                        Next Recommender
                                        <label class="labelRequired">
                                            *</label>
                                    </td>
                                    <td>
                                        <%if (Model.Approver.Count > 0)
                                          { %>
                                        <%= Html.DropDownListFor(m => m.ApprovalFlow.strDestAuthorID, Model.Approver, new { @class = "selectBoxRegular", @style = "width:250px;" })%>
                                        <%}
                                          else
                                          { %>
                                        <%= Html.DropDownListFor(m => m.ApprovalFlow.strDestAuthorID, Model.Approver, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:250px;" })%>
                                        <%} %>
                                    </td>
                                </tr>
                                <%} %>
                                <tr>
                                    <td>
                                        Comments
                                    </td>
                                    <td>
                                        <%=Html.TextAreaFor(m => m.ApprovalFlow.strComments, new { @class = "textRegular", @style = "width:250px;height:85px", @maxlength = 200, onkeyup = "return ismaxlengthPop(this)" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>--%>
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
        <%if (Model.intNextNodeID > 0)
          { %>
        <a href="#" class="btnRecommend" onclick="return save(4);"></a>
        <input id="btnRecommend" style="visibility: hidden" name="btnRecommend" type="submit"
            value="Recommend" visible="false" />
        <%}
          else
          { %>
        <a href="#" class="btnApprove" onclick="return save(1);"></a>
        <input id="btnApprove" style="visibility: hidden" name="btnApprove" type="submit"
            value="Approve" visible="false" />
        <%} %>
        <a href="#" class="btnReject" onclick="return save(3);"></a><%--<a href="#" class="btnClose"
            onclick="return closeDialog();"></a>
    </div>--%>

<div class="divButton">
    <a href="#" class="btnRecommend" onclick="return save(4);"></a>
    <input id="btnRecommend" style="visibility: hidden" name="btnRecommend" type="submit"
        value="Recommend" visible="false" />
    <a href="#" class="btnApprove" onclick="return save(1);"></a>
    <input id="btnApprove" style="visibility: hidden" name="btnApprove" type="submit"
        value="Approve" visible="false" />
    <a href="#" class="btnReject" onclick="return save(3);"></a>
    <%--<a href="#" class="btnClose"
            onclick="return closeDialog();"></a>--%>
    <input id="Submit1" style="visibility: hidden" name="btnReject" type="submit" value="Reject"
        visible="false" />
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</div>
</form>
