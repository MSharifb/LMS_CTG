<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<script type="text/javascript">



    $(document).ready(function () {

        preventSubmitOnEnter($("#frmOfflineLeaveAdjustmentDetails"));

        setTitle("Leave Application");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        FullDayHourly($("#ApplicationType"))

        //$(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        rowShowHide();

        document.getElementById('hdnEmpId').value = $('#LeaveApplication_strEmpID').val();
        document.getElementById('hdnEmpId_1').value = $('#LeaveApplication_strOfflineApprovedById').val();

        FormatTextBox();

    });


    function rowShowHide() {
        var intNod = document.getElementById('LeaveApplication_intDestNodeID').value;
        var intAppId = document.getElementById('LeaveApplication_intApplicationID').value;

        if (parseInt(intAppId) <= 0) {
            $('#LeaveApplication_strOfflineApprovedById').val('');
            $('#LeaveApplication_strOfflineApproverName').val('');
        }

        if (parseInt(intNod) > 0) {
            $('#LeaveApplication_strOfflineApprovedById').removeClass('required');
            $('#LeaveApplication_strOffLineAppvDate').removeClass('required');
            $('#LeaveApplication_strOffLineAppvDate').removeClass('date');
            $('#LeaveApplication_strOffLineAppvDate').addClass('dateNR');

            $('#lblRecPerson').css('visibility', 'visible');
            $('#LeaveApplication_strSupervisorID').addClass('required');

            $('#trApproved').css('display', 'none');
            $('#trRecPerson').css('display', '');

            $('#abtnSave').removeClass('btnSave');
            $('#abtnSave').addClass('btnSubmit');
        }
        else {
            $('#LeaveApplication_strOfflineApprovedById').addClass('required');
            $('#LeaveApplication_strOffLineAppvDate').addClass('required');
            $('#LeaveApplication_strOffLineAppvDate').removeClass('dateNR');
            $('#LeaveApplication_strOffLineAppvDate').addClass('date');

            $('#lblRecPerson').css('visibility', 'hidden');
            $('#LeaveApplication_strSupervisorID').removeClass('required');

            $('#trApproved').css('display', '');
            $('#trRecPerson').css('display', 'none');

            $('#abtnSave').removeClass('btnSubmit');
            $('#abtnSave').addClass('btnSave');

        }
    }

    function setData(id, name) {

        var strSrc = document.getElementById('StrEmpSearch').value;
        var confResult = true;

        if (strSrc == '0') {
            document.getElementById('LeaveApplication_strEmpID').value = id;
            //document.getElementById('LeaveApplication_strEmpName').value = name;
            $('#LeaveApplication_strEmpName').html(name);

            document.getElementById('hdnEmpId').value = id;
            $("#lblIdNotFound").css('visibility', 'hidden');
            $('#LeaveApplication_strEmpID').removeClass("invalid");

            var url = "/LMS/OfflineLeaveAdjustment/GetEmployeeInfo";
            var form = $("#frmOfflineLeaveAdjustmentDetails");
            var serializedForm = form.serialize();

            $('#LeaveApplication_strDesignationID').val('');
            $('#LeaveApplication_strDepartmentID').val('');
            $('#LeaveApplication_strSupervisorID').val('');

            $('#LeaveApplication_strDesignation').html('');
            $('#LeaveApplication_strDepartment').html('');
            $('#LeaveApplication_strBranch').html('');

            $('#strOffAuthorDesignation').val('');
            $('#strOffAuthorDepartment').val('');

            $('#intNodeID').val(0);
            $('#LeaveApplication_intDestNodeID').val(0);

            $.post(url, serializedForm, function (result) {

                if (result != null && result != '') {
                    $('#LeaveApplication_strDesignationID').val(result[0]);
                    $('#LeaveApplication_strDesignation').html(result[1]);
                    $('#LeaveApplication_strDepartmentID').val(result[2]);
                    $('#LeaveApplication_strDepartment').html(result[3]);
                    $('#LeaveApplication_strBranch').html(result[7]);
                    $('#LeaveApplication_strEmpName').html(result[8]);

                    if (result[8] == null || result[8] == "") {
                        $('#LeaveApplication_strEmpName').html("");
                        document.getElementById('hdnEmpId').value = "";
                        $("#lblIdNotFound").css('visibility', 'visible');
                    }
                    else {
                        $("#lblIdNotFound").css('visibility', 'hidden');
                    }

                    //---[populate employee Id wise leave type and next approver list]-----------------
                    $('#LeaveApplication_strSupervisorID > option:not(:first)').remove();
                    $.each(result[5], function () {
                        $("#LeaveApplication_strSupervisorID").append($("<option></option>").val(this['Value']).html(this['Text']));
                    });

                    if (result[5] != null && result[5] != "")
                    { $("select#LeaveApplication_strSupervisorID").attr('selectedIndex', 1); }

                    $('#intNodeID').val(result[6]);
                    $('#LeaveApplication_intDestNodeID').val(result[6]);

                    if (result[8] != null && result[8] != "") {
                        rowShowHide();
                        getAuthorityInfo();

                        url = '/LMS/OfflineLeaveAdjustment/IsAppliedForAdjustment';
                        serializedForm = form.serialize();

                        $.post(url, serializedForm, function (result) {
                            if (result != null) {
                                $.each(result, function (n) {
                                    alert(result[n]);
                                    confResult = false;
                                    document.getElementById('LeaveApplication_strEmpID').value = "";
                                    return false;
                                });
                            }
                            getApprovedApplication();
                            document.getElementById('LeaveApplication_strEmpID').value = id;
                        }, "json");
                    }
                }
                else {
                    alert(result);
                    $('#LeaveApplication_strDesignationID').val('');
                    $('#LeaveApplication_strDesignation').html('');
                    $('#LeaveApplication_strDepartmentID').val('');
                    $('#LeaveApplication_strDepartment').html('');
                    $('#LeaveApplication_strBranch').html('');
                    $('#LeaveApplication_strEmpName').html('');
                    $('#LeaveApplication_strEmpName').html("");
                    document.getElementById('hdnEmpId').value = "";
                    $("#lblIdNotFound").css('visibility', 'visible');
                    //---[populate employee Id wise leave type and next approver list]-----------------
                    $('#LeaveApplication_strSupervisorID > option:not(:first)').remove();
                    if (result[5] != null && result[5] != "") {
                        $("select#LeaveApplication_strSupervisorID").attr('selectedIndex', 1);
                    }
                    $('#intNodeID').val('');
                    $('#LeaveApplication_intDestNodeID').val('');
                }
                //-----------------------------------------------------------------------------




            }, "json");

            $("#divEmpList").dialog('close');
        }
        else if (strSrc == '1') {
            document.getElementById('LeaveApplication_strOfflineApprovedById').value = id;
            document.getElementById('LeaveApplication_strOfflineApproverName').value = name;

            document.getElementById('hdnEmpId_1').value = id;
            $('#LeaveApplication_strOfflineApprovedById').removeClass("invalid");

            var url = "/LMS/OfflineLeaveAdjustment/GetEmployeeInfo";
            var form = $("#frmOfflineLeaveAdjustmentDetails");
            var serializedForm = form.serialize();

            $('#LeaveApplication_strOffLineAppvDesignationID').val('');
            $('#strOffAuthorDesignation').val('');
            $('#LeaveApplication_strOffLineAppvDepartmentID').val('');
            $('#strOffAuthorDepartment').val('');

            $.post(url, serializedForm, function (result) {
                document.getElementById('LeaveApplication_strOffLineAppvDesignationID').value = result[0];
                document.getElementById('strOffAuthorDesignation').value = result[1];
                document.getElementById('LeaveApplication_strOffLineAppvDepartmentID').value = result[2];
                document.getElementById('strOffAuthorDepartment').value = result[3];
                document.getElementById('LeaveApplication_strOfflineApproverName').value = result[8];

                if (result[8] == null || result[8] == "") {
                    document.getElementById('LeaveApplication_strOfflineApproverName').value = "";
                    document.getElementById('hdnEmpId_1').value = "";
                    $("#lblIdNotFound1").css('visibility', 'visible');
                }
                else {
                    $("#lblIdNotFound1").css('visibility', 'hidden');
                }
            }, "json");

            $("#divEmpList").dialog('close');
        }
        else if (strSrc == '2') {
            document.getElementById('LeaveApplication_strSupervisorInitial').value = id;

            $('#LeaveApplication_strSupervisorID').removeClass("invalid");

            var url = "/LMS/OfflineLeaveAdjustment/GetEmployeeInfo";
            var form = $("#frmOfflineLeaveAdjustmentDetails");
            var serializedForm = form.serialize();

            $('#strAuthorDesignation').val('');
            $('#strAuthorDepartment').val('');

            $.post(url, serializedForm, function (result) {
                console.log(result);
                $('#strAuthorDesignation').val(result[1]);
                $('#strAuthorDepartment').val(result[3]);
                document.getElementById('LeaveApplication_strSupervisorID').value = result[9];
                document.getElementById('strSupervisorName').value = result[8];
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



    function getApprovedApplication() {

        var targetDiv = '#divLeaveAdjustmentDetails';
        var url = "/LMS/OfflineLeaveAdjustment/GetAdjustApplication";
        var form = $("#frmOfflineLeaveAdjustmentDetails");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {

            var strLVType = result['RefLeaveApplication']['strApplicationType'];
            var startMinValue = "<%= DateTime.MinValue.ToString(LMS.Util.DateTimeFormat.Date) %>";

            //---[For reference approved leave application information]--------------
            $('#RefLeaveApplication_intLeaveTypeID').val(result['RefLeaveApplication']['intLeaveTypeID']);
            $('#RefLeaveApplication_strLeaveType').html(result['RefLeaveApplication']['strLeaveType']);

            if (strLVType == 'FullDay')
            { $('#RefLeaveApplication_strApplicationType').html('FullDay'); }
            else if (strLVType == 'Hourly')
            { $('#RefLeaveApplication_strApplicationType').html('Hourly'); }
            else if (strLVType == 'FullDayHalfDay')
            { $('#RefLeaveApplication_strApplicationType').html('Full Day and/or Half Day'); }
            else
            { $('#RefLeaveApplication_strApplicationType').html(''); }

            $('#RefLeaveApplication_strApplyDate').html(result['RefLeaveApplication']['strApplyDate'] == startMinValue ? "" : result['RefLeaveApplication']['strApplyDate']);

            var IsHr = result['RefLeaveApplication']['IsHourly'];
            if (Boolean(IsHr) == true)
            { $('.RefLeaveApplication_IsHourly').html(' Hours'); }
            else { $('.RefLeaveApplication_IsHourly').html(' Days'); }

            $('#RefLeaveApplication_strApplyFromDate').html(result['RefLeaveApplication']['strApplyFromDate'] == startMinValue ? "" : result['RefLeaveApplication']['strApplyFromDate']);
            $('#RefLeaveApplication_strApplyFromTime').html(result['RefLeaveApplication']['strApplyFromTime']);
            $('#RefLeaveApplication_strApplyToDate').html(result['RefLeaveApplication']['strApplyToDate'] == startMinValue ? "" : result['RefLeaveApplication']['strApplyToDate']);
            $('#RefLeaveApplication_strApplyToTime').html(result['RefLeaveApplication']['strApplyToTime']);
            $('#RefLeaveApplication_fltDuration').html(result['RefLeaveApplication']['fltDuration']);
            $('#RefLeaveApplication_fltWithPayDuration').html(result['RefLeaveApplication']['fltWithPayDuration']);
            $('#RefLeaveApplication_fltWithoutPayDuration').html(result['RefLeaveApplication']['fltWithoutPayDuration']);
            $('#RefLeaveApplication_strPurpose').html(result['RefLeaveApplication']['strPurpose']);
            $('#RefLeaveApplication_strHalfDayDurationFor').html(result['RefLeaveApplication']['strHalfDayDurationFor']);

            //---[For new adjustment application information]--------------
            $('#LeaveApplication_intRefApplicationID').val(result['RefLeaveApplication']['intApplicationID']);
            $('#LeaveApplication_intLeaveTypeID').val(result['LeaveApplication']['intLeaveTypeID']);

            $('#LeaveApplication_strApplyFromDate').val(result['LeaveApplication']['strApplyFromDate']);
            $('#LeaveApplication_strApplyFromTime').val(result['LeaveApplication']['strApplyFromTime']);
            $('#LeaveApplication_strApplyToDate').val(result['LeaveApplication']['strApplyToDate']);
            $('#LeaveApplication_strApplyToTime').val(result['LeaveApplication']['strApplyToTime']);

            $('#LeaveApplication_IsFullDay').val(result['LeaveApplication']['IsFullDay']);
            $('#LeaveApplication_IsHourly').val(result['LeaveApplication']['IsHourly']);
            $('#LeaveApplication_IsFullDayHalfDay').val(result['LeaveApplication']['IsFullDayHalfDay']);

            $('#LeaveApplication_IsFullDay').attr('checked', result['LeaveApplication']['IsFullDay']);
            $('#LeaveApplication_IsHourly').attr('checked', result['LeaveApplication']['IsHourly']);
            $('#LeaveApplication_IsFullDayHalfDay').attr('checked', result['LeaveApplication']['IsFullDayHalfDay']);

            $('#ApplicationType').val(result['LeaveApplication']['strApplicationType']);
            $("#LeaveApplication_strApplicationType").val(result['LeaveApplication']['strApplicationType']);
            FullDayHourly($("#ApplicationType"));

            $('#LeaveApplication_fltDuration').val(result['LeaveApplication']['fltDuration']);
            $('#LeaveApplication_fltWithPayDuration').val(result['LeaveApplication']['fltWithPayDuration']);
            $('#LeaveApplication_fltWithoutPayDuration').val(result['LeaveApplication']['fltWithoutPayDuration']);
            $('#LeaveApplication_intDurationID').val(result['LeaveApplication']['intDurationID']);
            $('#LeaveApplication_strHalfDayFor').val(result['LeaveApplication']['strHalfDayFor']);
            $('#strHalfDayFromTime').val(result['LeaveApplication']['strApplyFromTime']);
            $('#strHalfDayToTime').val(result['LeaveApplication']['strApplyToTime']);

        }, "json");
        return false;
    }





    function parseDate(str) {
        var dmy = str.split('-');
        var mdy = dmy[1] + '/' + dmy[0] + '/' + dmy[2];
        return new Date(mdy);
    }


    function checkDateValidation() {

        if ($('#LeaveApplication_strApplyFromDate').val() != "" && $('#LeaveApplication_strApplyToDate').val() != "") {

            var pdtAPVFrom;
            var pdtAPVTo;

            if (parseInt($('#LeaveApplication_intApplicationID').val()) > 0) {
                pdtAPVFrom = parseDate($('#RefLeaveApplication_strApplyFromDate').val());
                pdtAPVTo = parseDate($('#RefLeaveApplication_strApplyToDate').val());
            }
            else {
                pdtAPVFrom = parseDate($('#RefLeaveApplication_strApplyFromDate').html());
                pdtAPVTo = parseDate($('#RefLeaveApplication_strApplyToDate').html());
            }

            var pdtAPFrom = parseDate($('#LeaveApplication_strApplyFromDate').val());
            var pdtAPTo = parseDate($('#LeaveApplication_strApplyToDate').val());

            var pdtYStart = parseDate($('#StrYearStartDate').val());
            var pdtYEnd = parseDate($('#StrYearEndDate').val());

            if (pdtAPFrom > pdtAPTo) {
                alert("Adjust To Date must be equal or greater than Adjust From Date.");
                $('#LeaveApplication_strApplyToDate').val($('#LeaveApplication_strApplyFromDate').val());
                return false;
            }

            if (pdtAPFrom < pdtYStart || pdtAPFrom > pdtYEnd) {
                alert("Adjust From Date must be between current leave year.");
                return false;
            }

            if (pdtAPTo < pdtYStart || pdtAPTo > pdtYEnd) {
                alert("Adjust To Date must be between current leave year.");
                return false;
            }

            if ($('#LeaveApplication_strOffLineAppvDate').val() != "" && $('#LeaveApplication_strOfflineApprovedById').val() != "") {
                var pdtAPVDate = parseDate($('#LeaveApplication_strOffLineAppvDate').val());
                var pdtAPDate = parseDate($('#LeaveApplication_strApplyDate').val());

                if (pdtAPVDate > pdtYEnd || pdtYStart > pdtAPVDate) {
                    alert("Approved Date must be between current leave year.");
                    return false;
                }
                if (pdtAPDate > pdtAPVDate) {
                    alert("Approved Date must be equal or greater than apply date.");
                    return false;
                }
            }

            if ($('#LeaveApplication_strApplyDate').val() != "") {
                var pdtAPDate = parseDate($('#LeaveApplication_strApplyDate').val());

                if (pdtAPDate > pdtYEnd || pdtYStart > pdtAPDate) {
                    alert("Apply Date must be between current leave year.");
                    return false;
                }
                if (pdtAPDate > pdtAPFrom && pdtAPDate < pdtAPTo) {
                    alert("Apply Date cannot be between leave from date and leave to date.");
                    return false;
                }
            }

            if (pdtAPFrom < pdtAPVFrom || pdtAPFrom > pdtAPVTo) {
                alert("Adjust From Date must be within approved leave date.");
                if (parseInt($('#LeaveApplication_intApplicationID').val()) > 0) {
                    $('#LeaveApplication_strApplyFromDate').val($('#RefLeaveApplication_strApplyFromDate').val());
                    if ($('#RefLeaveApplication_strApplicationType').val() == 'Hourly') {
                        $('#LeaveApplication_strApplyToDate').val($('#RefLeaveApplication_strApplyFromDate').val());
                    }
                }
                else {
                    $('#LeaveApplication_strApplyFromDate').val($('#RefLeaveApplication_strApplyFromDate').html());
                    if ($('#RefLeaveApplication_strApplicationType').html() == 'Hourly') {
                        $('#LeaveApplication_strApplyToDate').val($('#RefLeaveApplication_strApplyFromDate').html());
                    }
                }

                return false;
            }

            if (pdtAPTo < pdtAPVFrom || pdtAPTo > pdtAPVTo) {
                alert("Adjust To Date must be within approved leave date.");
                if (parseInt($('#LeaveApplication_intApplicationID').val()) > 0) {
                    $('#LeaveApplication_strApplyToDate').val($('#RefLeaveApplication_strApplyToDate').val());
                }
                else {
                    $('#LeaveApplication_strApplyToDate').val($('#RefLeaveApplication_strApplyToDate').html());
                }
                return false;
            }

            var fltApvDur;
            var fltDur;
            var fltOffHr = "<%= LMS.Web.LoginInfo.Current.fltOfficeTime %>";

            if (parseInt($('#LeaveApplication_intApplicationID').val()) > 0) {
                if ($('#RefLeaveApplication_strApplicationType').val() == 'Hourly') {
                    fltApvDur = $('#RefLeaveApplication_fltDuration').val();
                }
                else {
                    fltApvDur = $('#RefLeaveApplication_fltDuration').val() * fltOffHr;
                }
            }
            else {
                if ($('#RefLeaveApplication_strApplicationType').html() == 'Hourly') {
                    fltApvDur = $('#RefLeaveApplication_fltDuration').html();
                }
                else {
                    fltApvDur = $('#RefLeaveApplication_fltDuration').html() * fltOffHr;
                }
            }

            if ($('#ApplicationType').val() == 'Hourly') {
                fltDur = $('#LeaveApplication_fltDuration').val();
            }
            else {
                fltDur = $('#LeaveApplication_fltDuration').val() * fltOffHr;
            }

            //alert($('#ApplicationType').val() + ' ' + fltOffHr + '- ' + fltDur + '- ' + fltApvDur);

            if (fltDur > fltApvDur) {
                alert("Adjustment duration cannot be greater than approved leave duration.");
                return false;
            }
        }
        return true;
    }

    function validateHour() {

        var startOfficeTime = "<%= LMS.Web.LoginInfo.Current.StartOfficeTime %>";
        var endOfficeTime = "<%= LMS.Web.LoginInfo.Current.EndOfficeTime %>";

        var timeFrom = new Array();
        timeFrom = startOfficeTime.split(' ');

        timeFromArr = new Array();
        timeFromArr = timeFrom[1].split(':');

        var timeTo = new Array();
        timeTo = endOfficeTime.split(' ');
        timeToArr = new Array();
        timeToArr = timeTo[1].split(':');

        var FromTime = $('#LeaveApplication_strApplyFromTime').val();
        var ToTime = $('#LeaveApplication_strApplyToTime').val();

        var jdt1 = Date.parse('20 Aug 2000 ' + timeFrom[1] + ' ' + timeFrom[2]);
        var jdt2 = Date.parse('20 Aug 2000 ' + FromTime);

        if (jdt1 > jdt2) {
            alert('Invalid From Hour');
            $('#LeaveApplication_strApplyFromTime').val(timeFromArr[0] + ':' + timeFromArr[1] + ' ' + timeFrom[2]);
            $('#LeaveApplication_strApplyToTime').val(ToTime);
        }


        var jdtTo = Date.parse('20 Aug 2000 ' + timeTo[1] + ' ' + timeTo[2]);
        var jdtTo1 = Date.parse('20 Aug 2000 ' + ToTime);

        if (jdtTo < jdtTo1) {
            alert('Invalid To Hour');
            $('#LeaveApplication_strApplyToTime').val(timeToArr[0] + ':' + timeToArr[1] + ' ' + timeTo[2]);
        }


    }

    function CalculateDuration() {
        // alert("N2-" + $('#ApplicationType').val());
        var intLVTId = $('#LeaveApplication_intLeaveTypeID').val();
        if (intLVTId == 0) {
            alert("Approved leave type cannot be blank.");
            return false;
        }



        if (fnValidateDateTime()) {
            var targetDiv = "#divLeaveAdjustmentDetails";
            var url = "/LMS/OfflineLeaveAdjustment/CalcutateDuration";
            var form = $("#frmOfflineLeaveAdjustmentDetails");
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

                checkDateValidation();

            }, "json");
        }
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
                //alert(objfltWithPayDuration.value);
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
                //alert(objfltWithoutPayDuration.value);                
                if (!isNaN(objfltWithoutPayDuration.value)) {
                    objfltWithPayDuration.value = (parseFloat(objDuration.value) - parseFloat(objfltWithoutPayDuration.value)).toFixed(2);
                }
            }
        }
    }


    function FullDayHourly(e) {

        var strApplicationType = $(e).val();
        var startOfficeTime = "<%= LMS.Web.LoginInfo.Current.StartOfficeTime %>";
        var endOfficeTime = "<%= LMS.Web.LoginInfo.Current.EndOfficeTime %>";

        if (strApplicationType == "FullDay") {
            $('#trHalfDayFor').css('display', 'none');
            $('#LeaveApplication_intDurationID').val('');
            $('#LeaveApplication_strHalfDayFor').val('');
            $('#LeaveApplication_intDurationID').removeClass('required');
            $('#LeaveApplication_strHalfDayFor').removeClass('required');
            $('#strHalfDayFromTime').val('');
            $('#strHalfDayToTime').val('');

            $('#lblTime').css('visibility', 'hidden');
            $('#LeaveApplication_strApplyFromTime').css('visibility', 'hidden');
            $('#LeaveApplication_strApplyToTime').css('visibility', 'hidden');

            $('#btnCalculate').css('visibility', 'visible');
            $('.lblDaysHour').html("Days");

            //Apply CSS
            $('#LeaveApplication_strApplyFromTime').removeClass("required");
            $('#LeaveApplication_strApplyToTime').removeClass("required");
            $('#LeaveApplication_fltDuration').attr('readonly', true);
            $('#LeaveApplication_strApplyToDate').removeAttr('readonly');
            $("#LeaveApplication_strApplyFromTime").val('');
            $("#LeaveApplication_strApplyToTime").val('');

            $('.timepicker').next('img').hide();
        }

        if (strApplicationType == "Hourly") {
            $('#trHalfDayFor').css('display', 'none');
            $('#LeaveApplication_strHalfDayFor').val('');
            $('#LeaveApplication_intDurationID').val('');
            $('#LeaveApplication_intDurationID').removeClass('required');
            $('#LeaveApplication_strHalfDayFor').removeClass('required');
            $('#strHalfDayFromTime').val('');
            $('#strHalfDayToTime').val('');

            $("#lblTime").css('visibility', 'visible');
            $("#LeaveApplication_strApplyFromTime").css('visibility', 'visible');
            $("#LeaveApplication_strApplyToTime").css('visibility', 'visible');

            $(".lblDaysHour").html("Hours");
            $("#btnCalculate").css('visibility', 'hidden');
            $('#LeaveApplication_strApplyToDate').val($('#LeaveApplication_strApplyFromDate').val());

            //Apply CSS
            $('#LeaveApplication_strApplyFromTime').addClass("required");
            $('#LeaveApplication_strApplyToTime').addClass("required");
            $('#LeaveApplication_fltDuration').attr('readonly', true);
            $('#LeaveApplication_strApplyToDate').attr('readonly', true);

            $('#LeaveApplication_strApplyFromTime').removeAttr('disabled');
            $('#LeaveApplication_strApplyToTime').removeAttr('disabled');

            //            $("#LeaveApplication_strApplyFromTime").timepicker({ ampm: true, stepMinute: 15, minDate: new Date(startOfficeTime),
            //                maxDate: new Date(endOfficeTime)
            //            });

            //            $("#LeaveApplication_strApplyToTime").timepicker({ ampm: true, stepMinute: 15, minDate: new Date(startOfficeTime),
            //                maxDate: new Date(endOfficeTime)
            //            });

            $(".timepicker").timepicker({ ampm: true, timeFormat: 'hh:mm:TT'
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/clock2.png")%>'
            , buttonImageOnly: true
            , stepMinute: 15, minDate: new Date(startOfficeTime), maxDate: new Date(endOfficeTime)
            });
            $('.timepicker').next('img').show();
            /* ----for time picker detail: http://trentrichardson.com/examples/timepicker/ ----*/
        }

        if (strApplicationType == "FullDayHalfDay") {
            $('#trHalfDayFor').css('display', '');
            $('#LeaveApplication_intDurationID').addClass('required');
            $('#LeaveApplication_strHalfDayFor').addClass('required');

            $("#lblTime").css('visibility', 'visible');
            $("#LeaveApplication_strApplyFromTime").css('visibility', 'visible');
            $("#LeaveApplication_strApplyToTime").css('visibility', 'visible');

            $(".lblDaysHour").html("Days");
            $("#btnCalculate").css('visibility', 'visible');
            //Apply CSS

            $('#LeaveApplication_strApplyFromTime').addClass("required");
            $('#LeaveApplication_strApplyToTime').addClass("required");
            $('#LeaveApplication_fltDuration').attr('readonly', true);
            $('#LeaveApplication_strApplyToDate').removeAttr('readonly');


            $('#LeaveApplication_strApplyFromTime').attr('disabled', 'disabled');
            $('#LeaveApplication_strApplyToTime').attr('disabled', 'disabled');

            $('.timepicker').next('img').hide();
        }

        $('#ApplicationType').val(strApplicationType);
        var obj = document.getElementsByName("LeaveApplication_strApplicationType");
        for (i = 0; i < obj.length; ++i) {
            if (obj[i].value == strApplicationType) {
                obj[i].checked = true;
            }
        }

        return false;
    }



    function searchEmployee() {
        window.parent.openEmployee();
    }

    function OfflineAdjustmentApprove() {
        if (fnValidateDateTime() == false) {
            if ($('#ApplicationType').val() != "FullDay") {
                alert("Invalid Leave Date or Leave Time.");
            }
            else {
                alert("Invalid Leave Date.");
            }
            return false;
        }

        if (parseFloat($('#LeaveApplication_fltDuration').val()) > 0) {
            if (parseFloat($('#LeaveApplication_fltDuration').val()) != parseFloat($("#LeaveApplication_fltWithPayDuration").val()) + parseFloat($("#LeaveApplication_fltWithoutPayDuration").val())) {
                alert("Summation  of With Pay Duration and Without Pay Duration must be equal of Duration.");
                return false;
            }
        }
        else {
            alert("Duration must be greater than zero.");
            return false;
        }

        //---[check invalid employee ID]---------------
        if (document.getElementById('LeaveApplication_strEmpID').value != "" && document.getElementById('hdnEmpId').value != document.getElementById('LeaveApplication_strEmpID').value) {
            $('#LeaveApplication_strEmpID').addClass("invalid");
        }
        else {
            $('#LeaveApplication_strEmpID').removeClass("invalid");
        }
        if (document.getElementById('LeaveApplication_strOfflineApprovedById').value != "" && document.getElementById('hdnEmpId_1').value != document.getElementById('LeaveApplication_strOfflineApprovedById').value) {
            $('#LeaveApplication_strOfflineApprovedById').addClass("invalid");
        } else {
            $('#LeaveApplication_strOfflineApprovedById').removeClass("invalid");
        }

        if (fnValidate() && checkDateValidation() == true) {

            var confResult = true;
            var url = '/LMS/OfflineLeaveAdjustment/GetAuthorityResPersonLeaveStatus';
            var form = $('#frmOfflineLeaveAdjustmentDetails');
            var serializedForm = form.serialize();
            Id = $('#LeaveApplication_intApplicationID').val();

            $.post(url, serializedForm, function (result) {
                if (result != null) {
                    $.each(result, function (n) {
                        var resultconf = confirm(result[n]);
                        if (resultconf == false) {
                            confResult = false;
                            return false;
                        }
                    });
                }
                if (confResult != false) {

                    url = '/LMS/OfflineLeaveAdjustment/OfflineAdjustmentApprove';
                    var targetDiv = '#divLeaveAdjustmentDetails';

                    $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
                }
            }, "json");

        }

        return false;
    }


    function OfflineAdjustmentUpdate() {
        if (fnValidateDateTime() == false) {
            if ($('#ApplicationType').val() != "FullDay") {
                alert("Invalid Leave Date or Leave Time.");
            }
            else {
                alert("Invalid Leave Date.");
            }
            return false;
        }

        if (parseFloat($('#LeaveApplication_fltDuration').val()) > 0) {
            if (parseFloat($('#LeaveApplication_fltDuration').val()) != parseFloat($("#LeaveApplication_fltWithPayDuration").val()) + parseFloat($("#LeaveApplication_fltWithoutPayDuration").val())) {
                alert("Summation  of With Pay Duration and Without Pay Duration must be equal of Duration.");
                return false;
            }
        }
        else {
            alert("Duration must be greater than zero.");
            return false;
        }

        //---[check invalid employee ID]---------------        
        if (document.getElementById('LeaveApplication_strEmpID').value != "" && document.getElementById('hdnEmpId').value != document.getElementById('LeaveApplication_strEmpID').value) {
            $('#LeaveApplication_strEmpID').addClass("invalid");
        }
        if (document.getElementById('LeaveApplication_strOfflineApprovedById').value != "" && document.getElementById('hdnEmpId_1').value != document.getElementById('LeaveApplication_strOfflineApprovedById').value) {
            $('#LeaveApplication_strOfflineApprovedById').addClass("invalid");
        }

        if (fnValidate() && checkDateValidation() == true) {
            var result = confirm('Pressing OK will update this adjustment application. Do you want to continue?');
            if (result == true) {
                Id = $('#LeaveApplication_intApplicationID').val();

                var targetDiv = "#divLeaveAdjustmentDetails";
                var url = "/LMS/OfflineLeaveAdjustment/OfflineAdjustmentUpdate";
                var form = $("#frmOfflineLeaveAdjustmentDetails");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        }
        return false;
    }


    function OfflineAdjustmentDelete() {
        Id = $('#LeaveApplication_intApplicationID').val();

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            Id = $('#LeaveApplication_intApplicationID').val();

            var targetDiv = "#divLeaveAdjustmentDetails";

            var url = "/LMS/OfflineLeaveAdjustment/OfflineAdjustmentDelete";
            var form = $("#frmOfflineLeaveAdjustmentDetails");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function HookupCalculation(obj) {
        if (obj.id == 'LeaveApplication_strApplyFromDate' || $('#ApplicationType').val() == "Hourly") {
            validateHour();
            // $('#LeaveApplication_strApplyToDate').val($('#LeaveApplication_strApplyFromDate').val());
        }
        if (obj.id == 'LeaveApplication_strApplyFromTime') {
            // $('#LeaveApplication_strApplyToTime').val($('#LeaveApplication_strApplyFromTime').val());
        }

        CalculateDuration();
        return false;
    }

    function GetWoringTime() {
        //var va = $('#LeaveApplication_intDurationID :selected').text();

        var url = "/LMS/LeaveApplication/GetWorkingTimeInfo";
        var form = $("#frmOfflineLeaveAdjustmentDetails");
        var serializedForm = form.serialize();

        $('#LeaveApplication_strApplyFromTime').val('');
        $('#LeaveApplication_strApplyToTime').val('');
        $('#strHalfDayFromTime').val('');
        $('#strHalfDayToTime').val('');

        if ($('#LeaveApplication_intDurationID').val() != "") {
            $.post(url, serializedForm, function (result) {
                $("#LeaveApplication_strApplyFromTime").val(result[0]);
                $("#LeaveApplication_strApplyToTime").val(result[1]);
                $('#strHalfDayFromTime').val(result[0]);
                $('#strHalfDayToTime').val(result[1]);
            }, "json");
        }

        CalculateDuration();
        return false;
    }


    function refreshDuration() {

        document.getElementById('LeaveApplication_fltDuration').value = 0;
        document.getElementById('LeaveApplication_fltWithPayDuration').value = 0;
        document.getElementById('LeaveApplication_fltWithoutPayDuration').value = 0;
        document.getElementById('LeaveApplication_strApplyFromTime').value = '';
        document.getElementById('LeaveApplication_strApplyToTime').value = '';


        CalculateDuration();
    }


    function getAuthorityInfo() {

        document.getElementById('StrEmpSearch').value = $('#LeaveApplication_strSupervisorID').val();

        var url = '/LMS/LeaveApplication/GetEmployeeInfo';
        var form = $('#frmOfflineLeaveAdjustmentDetails');
        var serializedForm = form.serialize();

        $('#strAuthorDesignation').val('');
        $('#strAuthorDepartment').val('');

        if ($('#StrEmpSearch').val() != "") {
            $.post(url, serializedForm, function (result) {
                $('#strAuthorDesignation').val(result[1]);
                $('#strAuthorDepartment').val(result[3]);
            }, "json");
        }
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
                $('#LeaveApplication_strEmpID').removeClass("invalid");
                id = document.getElementById('LeaveApplication_strEmpID').value;
            }
            else if (srcflag == '1') {
                $('#LeaveApplication_strOfflineApprovedById').removeClass("invalid");
                id = document.getElementById('LeaveApplication_strOfflineApprovedById').value;
            }
            else if (srcflag == '2') {
                $('#LeaveApplication_strSupervisorID').removeClass("invalid");
                id = document.getElementById('LeaveApplication_strSupervisorID').value;
            }
            setData(id, name);
        }
        return true;
    }


</script>
<form id="frmOfflineLeaveAdjustmentDetails" method="post" action="" style="width: 100%">
<div id="divAdjustmentApplication">
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
            <%= Html.HiddenFor(m => m.StrEmpSearch)%>
            <%= Html.HiddenFor(m => m.strHalfDayFromTime)%>
            <%= Html.HiddenFor(m => m.strHalfDayToTime)%>
            <%= Html.HiddenFor(m => m.intNodeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intDestNodeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intApplicationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsApprovalProcess)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsDiscard)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intAppStatusID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveYearID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strStatus)%>
            <%--<%= Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>--%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsAdjustment)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intRefApplicationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveTypeID)%>
            <%--<%= Html.HiddenFor(m => m.LeaveApplication.IsFullDay)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.IsHourly)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.IsFullDayHalfDay)%>--%>
            <%= Html.HiddenFor(m => m.RefLeaveApplication.intLeaveTypeID)%>
        </div>
    </div>
    <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
        <tr>
            <td style="width: 75%;">
                <table class="contenttable" cellspacing="0" style="width: 100%; padding: 0px;">
                    <tr>
                        <td style="width: 25%;">
                            Applicant ID<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <input type="hidden" id="hdnEmpId" />
                            <%if (Model.LeaveApplication.intApplicationID <= 0)
                              { %>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strEmpID, new { @class = "textRegularDate required", onkeypress = "return handleEnter(event,'0');" })%>
                            <a href="#" class="btnSearch" onclick="return openEmployee('0');"></a>
                            <label id="lblIdNotFound" style="visibility: hidden; vertical-align: 5px; padding-left: 10px;
                                color: red;">
                                Id not found !</label>
                            <%}
                              else
                              {%>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strEmpID, new { @class = "textRegularDate required", @readonly = "readonly" })%>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            Name
                        </td>
                        <td>
                            <%if (Model.LeaveApplication.intApplicationID > 0)
                              { %>
                            <%=Html.Encode(Model.LeaveApplication.strEmpName)%>
                            <% }
                              else
                              { %>
                            <span id="LeaveApplication_strEmpName"></span>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            Designation<label></label>
                        </td>
                        <td>
                            <%= Html.HiddenFor(m => m.LeaveApplication.strDesignationID)%>
                            <%if (Model.LeaveApplication.intApplicationID > 0)
                              { %>
                            <%=Html.Encode(Model.LeaveApplication.strDesignation)%>
                            <% }
                              else
                              { %>
                            <span id="LeaveApplication_strDesignation"></span>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            Department<label></label>
                        </td>
                        <td>
                            <%= Html.HiddenFor(m => m.LeaveApplication.strDepartmentID)%>
                            <%if (Model.LeaveApplication.intApplicationID > 0)
                              { %>
                            <%=Html.Encode(Model.LeaveApplication.strDepartment)%>
                            <% }
                              else
                              { %>
                            <span id="LeaveApplication_strDepartment"></span>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            Branch<label></label>
                        </td>
                        <td>
                            <%if (Model.LeaveApplication.intApplicationID > 0)
                              { %>
                            <%=Html.Encode(Model.LeaveApplication.strBranch)%>
                            <% }
                              else
                              { %>
                            <span id="LeaveApplication_strBranch"></span>
                            <%} %>
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
    <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
        <tr>
            <td style="width: 50%" valign="top">
                <table class="contenttable" cellspacing="0" style="width: 100%; padding: 0px;">
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
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_intLeaveTypeID", Model.RefLeaveApplication.intLeaveTypeID)%>
                                        <%= Html.Hidden("RefLeaveApplication_strLeaveType",Model.RefLeaveApplication.strLeaveType)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.strLeaveType)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strLeaveType"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Duration Type
                                    </td>
                                    <td colspan="4">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_strApplicationType",Model.RefLeaveApplication.strApplicationType)%>
                                        <%if (String.Compare(Model.RefLeaveApplication.strApplicationType, "FullDay", true) == 0)
                                          {%>
                                        Full Day<% }
                                          else if (String.Compare(Model.RefLeaveApplication.strApplicationType, "Hourly", true) == 0)
                                          { %>
                                        Hourly<%}
                                          else if (String.Compare(Model.RefLeaveApplication.strApplicationType, "FullDayHalfDay", true) == 0)
                                          {%>
                                        Full Day and/or Half Day
                                        <%}%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strApplicationType"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Half Day Info.
                                    </td>
                                    <td colspan="4">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_strHalfDayDurationFor", Model.RefLeaveApplication.strHalfDayDurationFor)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.strHalfDayDurationFor)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strHalfDayDurationFor"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Applied Date
                                    </td>
                                    <td colspan="4">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_strApplyDate",Model.RefLeaveApplication.strApplyDate)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyDate == DateTime.MinValue.ToString(LMS.Util.DateTimeFormat.Date) ? string.Empty : Model.RefLeaveApplication.strApplyDate)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strApplyDate"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Leave Year
                                    </td>
                                    <td colspan="4">
                                        <%= Html.Encode(Model.StrYearStartDate +" To "+Model.StrYearEndDate)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_IsHourly", Model.RefLeaveApplication.IsHourly)%>
                                        <%= Html.Hidden("RefLeaveApplication_IsFullDay", Model.RefLeaveApplication.IsFullDay)%>
                                        <%= Html.Hidden("RefLeaveApplication_IsFullDayHalfDay", Model.RefLeaveApplication.IsFullDayHalfDay)%>
                                        <%} %>
                                    </td>
                                    <td colspan="2" style="width: 25%;">
                                        Granted Date
                                    </td>
                                    <td colspan="2" style="width: 20%;">
                                        Granted Time
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        From
                                    </td>
                                    <td colspan="2" style="width: 25%;">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_strApplyFromDate",Model.RefLeaveApplication.strApplyFromDate)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyFromDate == DateTime.MinValue.ToString(LMS.Util.DateTimeFormat.Date) ? string.Empty : Model.RefLeaveApplication.strApplyFromDate)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strApplyFromDate"></span>
                                        <%} %>
                                    </td>
                                    <td colspan="2" style="width: 40%;">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_strApplyFromTime", Model.RefLeaveApplication.strApplyFromTime)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyFromTime)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strApplyFromTime"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        To
                                    </td>
                                    <td colspan="2" style="width: 25%;">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_strApplyToDate",Model.RefLeaveApplication.strApplyToDate)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyToDate == DateTime.MinValue.ToString(LMS.Util.DateTimeFormat.Date) ? string.Empty : Model.RefLeaveApplication.strApplyToDate)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strApplyToDate"></span>
                                        <%} %>
                                    </td>
                                    <td colspan="2" style="width: 40%;">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_strApplyToTime",Model.RefLeaveApplication.strApplyToTime)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.strApplyToTime)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strApplyToTime"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Duration
                                    </td>
                                    <td colspan="4">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_fltDuration",Model.RefLeaveApplication.fltDuration)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.fltDuration)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_fltDuration"></span>
                                        <%} %>
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
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
                                        <% }
                                          else
                                          { %>
                                        <span class="RefLeaveApplication_IsHourly"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        With Pay Duration
                                    </td>
                                    <td colspan="4">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_fltWithPayDuration" ,Model.RefLeaveApplication.fltWithPayDuration)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.fltWithPayDuration)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_fltWithPayDuration"></span>
                                        <%} %>
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
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
                                        <% }
                                          else
                                          { %>
                                        <span class="RefLeaveApplication_IsHourly"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 35%;">
                                        Without Pay Duration
                                    </td>
                                    <td colspan="4">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_fltWithoutPayDuration",Model.RefLeaveApplication.fltWithoutPayDuration)%>
                                        <%= Html.Encode(Model.RefLeaveApplication.fltWithoutPayDuration)%>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_fltWithoutPayDuration"></span>
                                        <%} %>
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
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
                                        <% }
                                          else
                                          { %>
                                        <span class="RefLeaveApplication_IsHourly"></span>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr style="height: 125px;">
                                    <td colspan="2" valign="top">
                                        Purpose
                                    </td>
                                    <td colspan="4" valign="top">
                                        <%if (Model.LeaveApplication.intApplicationID > 0)
                                          { %>
                                        <%= Html.Hidden("RefLeaveApplication_strPurpose",Model.RefLeaveApplication.strPurpose)%>
                                        <div style="height: 30px; overflow-y: auto;">
                                            <%= Html.Label(Model.RefLeaveApplication.strPurpose)%>
                                        </div>
                                        <% }
                                          else
                                          { %>
                                        <span id="RefLeaveApplication_strPurpose"></span>
                                        <%} %>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table class="contenttable" width="100%">
                    <tr>
                        <td colspan="4" class="contenttabletd">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="4">
                                        Adjustment Application Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <%=Html.Hidden("ApplicationType", Model.LeaveApplication.strApplicationType)%>
                                        <%= Html.HiddenFor(m=> m.LeaveApplication.strApplicationType) %>
                                        <%=Html.RadioButtonFor(m=> m.LeaveApplication.IsFullDay , "FullDay", new { onclick = "FullDayHourly(this); refreshDuration();" })%>Full
                                        Day
                                        <% if (LMS.Web.LoginInfo.Current.strAllowHourlyLeave == "Yes")
                                           {%>
                                        <%=Html.RadioButtonFor(m=> m.LeaveApplication.IsHourly,  "Hourly",  new { @style = "visibility: visible", onclick = "FullDayHourly(this); refreshDuration(); " })%>Hourly
                                        <%}
                                           else
                                           {%>
                                        <%=Html.RadioButtonFor(m=> m.LeaveApplication.IsHourly ,  "Hourly", new { @style = "visibility: hidden", onclick = "FullDayHourly(this); refreshDuration(); " })%>
                                        <%} %>
                                        <%=Html.RadioButtonFor(m=> m.LeaveApplication.IsFullDayHalfDay , "FullDayHalfDay", new { onclick = "FullDayHourly(this); refreshDuration();" })%>Full
                                        Day and/or Half Day
                                    </td>
                                </tr>
                                <tr id="trHalfDayFor" style="display: none;">
                                    <td colspan="4">
                                        <div style="width: 100%; float: left; text-align: left;">
                                            <div style="float: left; text-align: left; padding-left: 8px;">
                                                <%= Html.DropDownListFor(m => m.LeaveApplication.intDurationID, Model.WorkingTime, "...Select One...", new { @class = "selectBoxRegular", @style = "width:130px; min-width:130px;",@onchange = "return GetWoringTime();" })%>
                                            </div>
                                            <div style="float: left; text-align: left; padding-left: 8px;">
                                                Of
                                                <%= Html.DropDownListFor(m => m.LeaveApplication.strHalfDayFor, Model.HalfDayFor, "...Select One...", new { @class = "selectBoxRegular", @style = "width:130px; min-width:130px;" })%>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                    </td>
                                    <td style="width: 25%">
                                        Leave Date
                                    </td>
                                    <td style="width: 30%">
                                        <label id="lblTime">
                                            Leave Time</label>
                                    </td>
                                    <td style="width: 30%">
                                        Apply Date
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        From<label class="labelRequired">*</label>
                                    </td>
                                    <td style="width: 25%">
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromDate, new { @class = "textRegularDate dtPicker date", @style = "width:80px; min-width:80px;", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>
                                    </td>
                                    <td style="width: 30%">
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromTime, new { @class = "textRegularTime timepicker",  @onchange = "return HookupCalculation(this);" })%>
                                    </td>
                                    <td style="width: 30%">
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyDate, new { @class = "textRegularDate required dtPicker date",@style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        To<label class="labelRequired">*</label>
                                    </td>
                                    <td style="width: 25%">
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToDate, new { @class = "textRegularDate required dtPicker date", @style = "width:80px; min-width:80px;", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>
                                    </td>
                                    <td style="width: 30%">
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToTime, new { @class = "textRegularTime timepicker", @onchange = "return HookupCalculation(this);" })%>
                                    </td>
                                    <td style="width: 30%">
                                        <a id="btnCalculate" href="#" class="btnCalculate" onclick="return CalculateDuration();">
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Duration<label class="labelRequired">*</label>
                                    </td>
                                    <td colspan="2">
                                        <div style="float: left; text-align: left;">
                                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onkeyup = "return ismaxlengthPop(this)", @onchange = "return HookupCalculation(this);" })%>
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
                                        With Pay Duration<label class="labelRequired">*</label>
                                    </td>
                                    <td colspan="2">
                                        <div style="float: left; text-align: left;">
                                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltWithPayDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onchange = "SplitDuration(this)" })%>
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
                                        Without Pay Duration<label class="labelRequired">*</label>
                                    </td>
                                    <td colspan="2">
                                        <div style="float: left; text-align: left;">
                                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltWithoutPayDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onchange = "SplitDuration(this);" })%>
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
                                        Purpose<label class="labelRequired">*</label>
                                    </td>
                                    <td colspan="2">
                                        <%=Html.TextAreaFor(m => m.LeaveApplication.strPurpose, new { @class = "textRegular required", @style = "width:240px;", @maxlength = 100, onkeyup = "return ismaxlengthPop(this)" })%>
                                    </td>
                                </tr>
                                <tr id="trRecPerson" style="white-space: nowrap;">
                                    <td colspan="4" valign="top">
                                        <table class="contenttable" cellspacing="0" style="width: 100%; padding: 0px;">
                                            <tr>
                                                <td style="width: 40%; padding: 0px;">
                                                <%=Html.HiddenFor(m => m.LeaveApplication.strSupervisorID)%>
                                                    Rec./Approve By
                                                    <label id="lblRecPerson" style="visibility: hidden;" class="labelRequired">
                                                        *</label>
                                                </td>
                                                <td>
                                                    <%=Html.TextBoxFor(m => m.LeaveApplication.strSupervisorInitial, new { @class = "textRegularDate required", onkeypress = "return handleEnter(event,'1');" })%>
                                                    <a href="#" class="btnSearch" onclick="return openEmployee('2');"></a>
                                                   <%-- <%= Html.DropDownListFor(m => m.LeaveApplication.strSupervisorID, Model.Approver, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:242px; min-width:242px;", @onchange = "return getAuthorityInfo();" })%>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%; padding: 0px;">
                                                    Name
                                                </td>
                                                <td>
                                                    <%=Html.TextBox("strSupervisorName", "", new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%; padding: 0px;">
                                                    Designation
                                                </td>
                                                <td>
                                                    <%=Html.TextBox("strAuthorDesignation", Model.strAuthorDesignation, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%; padding: 0px;">
                                                    Department
                                                </td>
                                                <td>
                                                    <%=Html.TextBox("strAuthorDepartment", Model.strAuthorDepartment, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trApproved" style="white-space: nowrap;">
                                    <td colspan="4" valign="top">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td style="width: 42%; padding: 0px;">
                                                    Approved Date<label class="labelRequired">*</label>
                                                </td>
                                                <td>
                                                    <%=Html.TextBoxFor(m => m.LeaveApplication.strOffLineAppvDate, new { @class = "textRegularDate required dtPicker date",@style = "width:75px; min-width:75px;", @maxlength = "10" })%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 42%; padding: 0px;">
                                                    Approved By<label class="labelRequired">*</label>
                                                </td>
                                                <td style="padding-left: 5px;">
                                                    <input type="hidden" id="hdnEmpId_1" />
                                                    <%=Html.TextBoxFor(m => m.LeaveApplication.strOfflineApprovedById, new { @class = "textRegularDate required", onkeypress = "return handleEnter(event,'1');" })%>
                                                    <a href="#" class="btnSearch" onclick="return openEmployee('1');"></a>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strOffLineAppvDepartmentID) %>
                                                    <%=Html.HiddenFor(m => m.LeaveApplication.strOffLineAppvDesignationID) %>
                                                    <label id="lblIdNotFound1" style="visibility: hidden; vertical-align: 5px; padding-left: 5px;
                                                        color: red;">
                                                        Id not found !</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 42%; padding: 0px;">
                                                    Name
                                                </td>
                                                <td>
                                                    <%=Html.TextBoxFor(m => m.LeaveApplication.strOfflineApproverName, new { @class = "textRegular", @style = "width:160px; min-width:160px;", @readonly = "readonly" })%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 42%; padding: 0px;">
                                                    Designation
                                                </td>
                                                <td>
                                                    <%=Html.TextBox("strOffAuthorDesignation", Model.strOffAuthorDesignation, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 42%; padding: 0px;">
                                                    Department
                                                </td>
                                                <td>
                                                    <%=Html.TextBox("strOffAuthorDepartment", Model.strOffAuthorDepartment, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Comments
                                    </td>
                                    <td colspan="2">
                                        <%=Html.TextAreaFor(m => m.LeaveApplication.strRemarks, new { @class = "textRegular", @style = "width:240px;", @maxlength = 200, onkeyup = "return ismaxlengthPop(this)" })%>
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
    <a id="abtnSave" href="#" class="btnSave" onclick="return OfflineAdjustmentApprove();">
    </a>
    <% }
      else
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfflineLeaveAdjustment, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return OfflineAdjustmentUpdate();"></a>
    <%} %>
    <%
      } %>
    <input id="btnSave" style="visibility: hidden" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%if (Model.LeaveApplication.intApplicationID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfflineLeaveAdjustment, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%--<a href="#" class="btnDelete" onclick="return OfflineAdjustmentDelete();"></a>--%>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
