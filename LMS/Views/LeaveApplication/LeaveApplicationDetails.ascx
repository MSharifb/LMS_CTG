<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveApplication"));

        setTitle("Leave Application");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 430, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        //GetAuthorityInfo();
        //FullDayHourly($("#ApplicationType"));
        FormatTextBox();
        //document.getElementById('hdnEmpId').value = document.getElementById('LeaveApplication_strResponsibleId').value;
        //CalculateDuration();
    });







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

    function setData(id, strEmpInitial, name) {
        var strSrc = document.getElementById('StrEmpSearch').value;
        //add by Nazrul
        if (strSrc == '0') {
            $("#LeaveApplication_strSupervisorInitial").val(strEmpInitial)
            document.getElementById('strSupervisorName').value = name;
            document.getElementById('StrEmpSearch').value = id;
            var url = '/LMS/LeaveApplication/GetEmployeeInfo';

            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            $('#strAuthorDesignation').val('');
            $('#strAuthorDepartment').val('');

            $.post(url, serializedForm, function (result) {
                $('#strAuthorDesignation').val(result[1]);
                $('#strAuthorDepartment').val(result[3]);
                $("#LeaveApplication_strSupervisorID").val(result[6]);

            }, "json");

            $("#divEmpList").dialog('close');
        }

       else if (strSrc == '1') {
            document.getElementById('LeaveApplication_strResponsibleId').value = id;
            document.getElementById('LeaveApplication_strResponsibleInitial').value = strEmpInitial;
            //GetResponsibleInfo();
        }
        else {

            document.getElementById('LeaveApplication_strPLID').value = id;
            document.getElementById('LeaveApplication_strPLInitial').value = strEmpInitial;
        }

        $("#divEmpList").dialog('close');
    }

    function Closing() {
        //window.location = "/LMS/LeaveType";
    }

    function GetResponsibleInfo() {
        document.getElementById('StrEmpSearch').value = $('#LeaveApplication_strResponsibleId').val();

        var url = '/LMS/LeaveApplication/GetEmployeeInfo';
        var form = $('#frmLeaveApplication');
        var serializedForm = form.serialize();

        if ($('#StrEmpSearch').val() != "") {
            $.post(url, serializedForm, function (result) {

                if (result != null && result != '' && result[0] != '') {

                    $("#lblIdNotFound").css('visibility', 'hidden');
                    $('#LeaveApplication_strResponsibleId').removeClass("invalid");
                    $('#LeaveApplication_strResponsibleInitial').val(result[4]);
                } else {
                    $('#LeaveApplication_strResponsibleInitial').val('');
                    $("#lblIdNotFound").css('visibility', 'visible');
                }
            }, "json");
        }
        return false;
    }


    function parseDate(str) {

        var dmy = str.split('-');
        var mdy = dmy[1] + '/' + dmy[0] + '/' + dmy[2];
        return new Date(mdy);
    }


    function checkDateValidation() {

        if ($('#LeaveApplication_strApplyFromDate').val() != "" && $('#LeaveApplication_strApplyToDate').val() != "") {

            var pdtAPFrom = parseDate($('#LeaveApplication_strApplyFromDate').val());
            var pdtAPTo = parseDate($('#LeaveApplication_strApplyToDate').val());

            var pdtYStart = parseDate($('#StrYearStartDate').val());
            var pdtYEnd = parseDate($('#StrYearEndDate').val());

            //alert('pdtAPFrom=' + pdtAPFrom + ' pdtAPTo=' + pdtAPTo + ' pdtYStart=' + pdtYStart + ' pdtYEnd=' + pdtYEnd);

            if (pdtAPFrom > pdtAPTo) {
                alert("Leave To Date must be equal or greater than Leave From Date.");
                return false;
            }

//            if (pdtAPFrom < pdtYStart && pdtAPTo > pdtYEnd {
//            alert("Leave From Date and to date must be between active leave year.");
//                return false;
//            }

//            if (pdtYStart > pdtAPFrom) {
//                alert("Leave From Date must be between active leave year.");
//                return false;
//            }
//            if (pdtAPTo > pdtYEnd) {
//                alert("Leave To Date must be between active leave year.");
//                return false;
//            }

            //            if ($('#LeaveApplication_strApplyDate').val() != "") {
            //                var pdtAPDate = parseDate($('#LeaveApplication_strApplyDate').val());

            //                if (pdtAPDate > pdtYEnd || pdtYStart > pdtAPDate) {
            //                    alert("Apply Date must be between current leave year.");
            //                    return false;
            //                }

            //                if (pdtAPDate > pdtAPFrom && pdtAPDate < pdtAPTo) {
            //                    alert("Apply Date cannot be between leave from date and leave to date.");
            //                    return false;
            //                }
            //            }

        }
        return true;

    }

    //    function validateHour() {

    //        var startOfficeTime = "<%= LMS.Web.LoginInfo.Current.StartOfficeTime %>";
    //        var endOfficeTime = "<%= LMS.Web.LoginInfo.Current.EndOfficeTime %>";

    //        var timeFrom = new Array();
    //        timeFrom = startOfficeTime.split(' ');

    //        timeFromArr = new Array();
    //        timeFromArr = timeFrom[1].split(':');

    //        var timeTo = new Array();
    //        timeTo = endOfficeTime.split(' ');
    //        timeToArr = new Array();
    //        timeToArr = timeTo[1].split(':');

    //        var FromTime = $('#LeaveApplication_strApplyFromTime').val();
    //        var ToTime = $('#LeaveApplication_strApplyToTime').val();

    //        var jdt1 = Date.parse('20 Aug 2000 ' + timeFrom[1] + ' ' + timeFrom[2]);
    //        var jdt2 = Date.parse('20 Aug 2000 ' + FromTime);

    //        if (jdt1 > jdt2) {
    //            alert('Invalid hour');
    //            $('#LeaveApplication_strApplyFromTime').val(timeFromArr[0] + ':' + timeFromArr[1] + ' ' + timeFrom[2]);
    //            $('#LeaveApplication_strApplyToTime').val(timeFromArr[0] + ':' + timeFromArr[1] + ' ' + timeFrom[2]);
    //        }


    //        var jdtTo = Date.parse('20 Aug 2000 ' + timeTo[1] + ' ' + timeTo[2]);
    //        var jdtTo1 = Date.parse('20 Aug 2000 ' + ToTime);

    //        if (jdtTo < jdtTo1) {
    //            alert('Invalid hour');
    //            $('#LeaveApplication_strApplyToTime').val(timeToArr[0] + ':' + timeToArr[1] + ' ' + timeTo[2]);
    //        }
    //    }

    function CalculateDuration() {

        var intLVTId = $('#LeaveApplication_intLeaveTypeID').val();

        //        if (intLVTId == '') {
        //            alert("Please Select Leave Type.");

        //            $("#LeaveApplication_fltDuration").val(0);
        //            $("#LeaveApplication_fltWithPayDuration").val(0);
        //            $("#LeaveApplication_fltWithoutPayDuration").val(0);
        //            $("#LeaveApplication_fltNetBalance").val(0);
        //            $('#LeaveApplication_intLeaveYearID').val(0);
        //            $('#LeaveApplication_fltBeforeBalance').val(0);

        //            return false;
        //        }

        //        if ($('#ApplicationType').val() == "Hourly") {
        //            validateHour();
        //        }

        //if (fnValidateDateTime() && checkDateValidation() == true) {

        if (fnValidateDateTime()) {

            var targetDiv = "#divLeaveApplicationDetails";
            var url = "/LMS/LeaveApplication/CalcutateDuration";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            if (intLVTId != '') {

                $.post(url, serializedForm, function (result) {

                    $("#LeaveApplication_fltDuration").val(result[0]);
                    $("#LeaveApplication_fltWithPayDuration").val(result[1]);
                    $("#LeaveApplication_fltWithoutPayDuration").val(result[2]);
                    $("#LeaveApplication_fltNetBalance").val(result[3]);

                    $('#LeaveApplication_intLeaveYearID').val(result[4]);
                    $('#LeaveApplication_fltBeforeBalance').val(result[5]);
                    $('#LeaveApplication_intDestNodeID').val(result[6]);
                    $('#intNodeID').val(result[6]);
                    $('#StrYearStartDate').val(result[7]);
                    $('#StrYearEndDate').val(result[8]);
                    $('#LeaveApplication_intEarnLeaveType').val(result[9]);


                    targetDiv = "#divLedger";
                    serializedForm = form.serialize();
                    url = "/LMS/LeaveApplication/GetLedger";
                    $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

                }, "json");
            }
            else {
                alert("Please Select Leave Type.");

                $("#LeaveApplication_fltDuration").val(0);
                $("#LeaveApplication_fltWithPayDuration").val(0);
                $("#LeaveApplication_fltWithoutPayDuration").val(0);
                $("#LeaveApplication_fltNetBalance").val(0);
                $('#LeaveApplication_intLeaveYearID').val(0);
                $('#LeaveApplication_fltBeforeBalance').val(0);
                $('#StrYearStartDate').val('');
                $('#StrYearEndDate').val('');
                $('#LeaveApplication_ intEarnLeaveType').val(0);
            }
        }
        return false;
    }


    function CalculateLeaveLedger() {
        var targetDiv = "#divLedger";
        var url = "/LMS/LeaveApplication/GetLedger";
        var form = $("#frmLeaveApplication");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

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

        CalculateLeaveLedger();

    }



    function FullDayHourly(e) {

        //        var strApplicationType = $(e).val();
        //        var startOfficeTime = "<%= LMS.Web.LoginInfo.Current.StartOfficeTime %>";
        //        var endOfficeTime = "<%= LMS.Web.LoginInfo.Current.EndOfficeTime %>";

        //        if (startOfficeTime == '1/1/0001 12:00:00 AM') {
        //            alert('Please insert office time details.');

        //        }
        //        if (strApplicationType == "FullDay") {

        //            $('#divWorkingTime').css('visibility', 'hidden');
        //            $('#LeaveApplication_intDurationID').val('');
        //            $('#divHalfDayFor').css('visibility', 'hidden');
        //            $('#LeaveApplication_strHalfDayFor').val('');
        //            $('#LeaveApplication_intDurationID').removeClass('required');
        //            $('#LeaveApplication_strHalfDayFor').removeClass('required');
        //            $('#strHalfDayFromTime').val('');
        //            $('#strHalfDayToTime').val('');

        //            //$('#lblTime').css('visibility', 'hidden');
        //            $('#LeaveApplication_strApplyFromTime').css('visibility', 'hidden');
        //            $('#LeaveApplication_strApplyToTime').css('visibility', 'hidden');
        //            $('#btnCalculate').css('visibility', 'visible');
        //            $('.lblDaysHour').html("Days");

        //            //Apply CSS
        //            $('#LeaveApplication_strApplyFromTime').removeClass('required');
        //            $('#LeaveApplication_strApplyToTime').removeClass('required');
        //            $('#LeaveApplication_strApplyFromTime').val('');
        //            $('#LeaveApplication_strApplyToTime').val('');

        //            $('#LeaveApplication_fltDuration').attr('readonly', true);

        //            $('#LeaveApplication_strApplyToDate').removeAttr('readonly');
        //            $('#LeaveApplication_strApplyToDate').addClass('dtPicker');
        //            $('#LeaveApplication_strApplyToDate').addClass('date');

        //            $('.timepicker').next('img').hide();
        //        }


        //        if (strApplicationType == "Hourly") {
        //            $('#divWorkingTime').css('visibility', 'hidden');
        //            $('#LeaveApplication_intDurationID').val('');
        //            $('#divHalfDayFor').css('visibility', 'hidden');
        //            $('#LeaveApplication_strHalfDayFor').val('');
        //            $('#LeaveApplication_intDurationID').removeClass('required');
        //            $('#LeaveApplication_strHalfDayFor').removeClass('required');
        //            $('#strHalfDayFromTime').val('');
        //            $('#strHalfDayToTime').val('');

        //            //$('#lblTime').css('visibility', 'visible');
        //            $('#LeaveApplication_strApplyFromTime').css('visibility', 'visible');
        //            $('#LeaveApplication_strApplyToTime').css('visibility', 'visible');
        //            $('.lblDaysHour').html("Hours");
        //            $('#btnCalculate').css('visibility', 'hidden');

        //            $('#LeaveApplication_strApplyToDate').val($('#LeaveApplication_strApplyFromDate').val());

        //            //Apply CSS
        //            $('#LeaveApplication_strApplyFromTime').addClass('required');
        //            $('#LeaveApplication_strApplyToTime').addClass('required');

        //            $('#LeaveApplication_fltDuration').attr('readonly', true);

        //            $('#LeaveApplication_strApplyToDate').attr('readonly', true);
        //            $('#LeaveApplication_strApplyToDate').removeClass('dtPicker');
        //            $('#LeaveApplication_strApplyToDate').removeClass('date');

        //            $('#LeaveApplication_strApplyFromTime').removeAttr('disabled');
        //            $('#LeaveApplication_strApplyToTime').removeAttr('disabled');


        //            $(".timepicker").timepicker({ ampm: true, timeFormat: 'hh:mm TT'
        //            , showOn: 'button'
        //            , buttonImage: '<%= Url.Content("~/Content/img/controls/clock2.png")%>'
        //            , buttonImageOnly: true
        //            , stepMinute: 15, minDate: new Date(startOfficeTime), maxDate: new Date(endOfficeTime)
        //            });
        //            $('.timepicker').next('img').show();

        //            /* ----for time picker detail: http://trentrichardson.com/examples/timepicker/ ----*/
        //        }

        //        if (strApplicationType == "FullDayHalfDay") {
        //            $('#divWorkingTime').css('visibility', 'visible');
        //            $('#divHalfDayFor').css('visibility', 'visible');
        //            $('#LeaveApplication_intDurationID').addClass('required');
        //            $('#LeaveApplication_strHalfDayFor').addClass('required');

        //            //$('#lblTime').css('visibility', 'visible');                     
        //            $('#LeaveApplication_strApplyFromTime').css('visibility', 'visible');
        //            $('#LeaveApplication_strApplyToTime').css('visibility', 'visible');
        //            $('.lblDaysHour').html("Days");
        //            $('#btnCalculate').css('visibility', 'visible');

        //            //Apply CSS
        //            $('#LeaveApplication_strApplyFromTime').addClass('required');
        //            $('#LeaveApplication_strApplyToTime').addClass('required');

        //            $('#LeaveApplication_fltDuration').attr('readonly', true);

        //            $('#LeaveApplication_strApplyToDate').removeAttr('readonly');
        //            $('#LeaveApplication_strApplyToDate').addClass('dtPicker');
        //            $('#LeaveApplication_strApplyToDate').addClass('date');

        //            $('#LeaveApplication_strApplyFromTime').attr('disabled', 'disabled');
        //            $('#LeaveApplication_strApplyToTime').attr('disabled', 'disabled');

        //            $('.timepicker').next('img').hide();
        //        }

        //$('#ApplicationType').val(strApplicationType); //FullDay

        return false;
    }



    function searchEmployee() {
        window.parent.openEmployee();
    }



    function OnlineSubmit() {
        var intLVTId = $('#LeaveApplication_intLeaveTypeID').val();
        var nodeID = $('#LeaveApplication_intDestNodeID').val();
        var halfAvg = $('#LeaveApplication_intEarnLeaveType').val();
        // Shamim
        var rec = $("#LeaveApplication_strSupervisorID").val();
        if (rec == "") {
            alert("Please Select Approver.");
            return false;
        }
        if (nodeID == 0) {
            alert("Approval path is not available for you. Please contact with administrator.");
            return false;
        }

        if (intLVTId == 0) {
            alert("Please Select Leave Type.");
            return false;
        }

        //        if (document.getElementById('hdnEmpId').value != document.getElementById('LeaveApplication_strResponsibleId').value) {
        //            $('#LeaveApplication_strResponsibleId').addClass("invalid");
        //        } else {
        //            $('#LeaveApplication_strResponsibleId').removeClass("invalid");
        //        }

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

        if (document.getElementById('LeaveApplication_strEmpID').value != "" && document.getElementById('LeaveApplication_strResponsibleId').value != "") {
            if (document.getElementById('LeaveApplication_strEmpID').value == document.getElementById('LeaveApplication_strResponsibleId').value) {
                alert("Applicant cann't be Responsible Person.");
                return false;
            }

        }

        var LWP = $('#LeaveApplication_fltWithoutPayDuration').val();
        if (parseFloat(LWP) > 0) {
            var result = confirm('This application has leave without pay duration. Do you want to proceed?');
            if (result == false) {
                return false;
            }
        }

        var netBL = $('#LeaveApplication_fltNetBalance').val();
        if (parseInt(halfAvg) == 2) {
            var totalDays = parseFloat($('#LeaveApplication_fltDuration').val());
            if (netBL < totalDays) {
                alert("This Leave Application Need More Balances To Proceed.!");
                return false;
            }
        }
        if (parseFloat(netBL) < 0) {
            var result = confirm('Net balance is ' + netBL.toString() + '. Do you want to proceed?');
            if (result == false) {
                return false;
            }
        }

        //        alert('OnLine111');

        if (fnValidate() && checkDateValidation() == true) {

            var confResult = true;
            var url = '/LMS/LeaveApplication/ValidateLeaveApplication';
            var form = $('#frmLeaveApplication');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {

                if (result[0] != null && result[0] != "") {
                    //                    alert('OnLine222');
                    alert(result);
                    //                    alert('OnLine333');
                    return false;
                }
                else {
                    url = '/LMS/LeaveApplication/GetAuthorityResPersonLeaveStatus';
                    serializedForm = form.serialize();
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

                            //                            if ($('#ApplicationType').val() != "FullDayHalfDay") {
                            //                                $('#LeaveApplication_strApplyFromTime').val($('#txtHalfDayFromTime').val());
                            //                                $('#LeaveApplication_strApplyToTime').val($('#txtHalfDayToTime').val());
                            //                            }

                            url = '/LMS/LeaveApplication/OnlineSubmit';
                            var targetDiv = '#divLeaveApplicationDetails';
                            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
                        }
                    }, "json");
                }
            }, "json");
        }
        return false;
    }



    function OnlineCancel() {
        if (fnValidateDateTime() == false) {
            if ($('#ApplicationType').val() != "FullDay") {
                alert("Invalid Leave Date or Leave Time.");
            }
            else {
                alert("Invalid Leave Date.");
            }
            return false;
        }

        if (fnValidate() && checkDateValidation() == true) {
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
            var targetDiv = "#divLeaveApplicationDetails";
            var url = "/LMS/LeaveApplication/OnlineDelete";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }


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



    //    function refreshDuration() {
    //        document.getElementById('LeaveApplication_fltDuration').value = 0;
    //        document.getElementById('LeaveApplication_fltWithPayDuration').value = 0;
    //        document.getElementById('LeaveApplication_fltWithoutPayDuration').value = 0;
    //        document.getElementById('LeaveApplication_strApplyFromTime').value = '';
    //        document.getElementById('LeaveApplication_strApplyToTime').value = '';
    //        document.getElementById('LeaveApplication_fltNetBalance').value = 0;

    //        //--[add by shaiful 04-Nov-2010]
    //        CalculateLeaveLedger();
    //    }

    //    function GetWoringTime() {
    //        //var va = $('#LeaveApplication_intDurationID :selected').text();
    //        var url = "/LMS/LeaveApplication/GetWorkingTimeInfo";
    //        var form = $("#frmLeaveApplication");
    //        var serializedForm = form.serialize();

    //        $('#LeaveApplication_strApplyFromTime').val('');
    //        $('#LeaveApplication_strApplyToTime').val('');
    //        $('#strHalfDayFromTime').val('');
    //        $('#strHalfDayToTime').val('');

    //        if ($('#LeaveApplication_intDurationID').val() != "") {
    //            $.post(url, serializedForm, function (result) {
    //                $('#LeaveApplication_strApplyFromTime').val(result[0]);
    //                $('#LeaveApplication_strApplyToTime').val(result[1]);
    //                $('#strHalfDayFromTime').val(result[0]);
    //                $('#strHalfDayToTime').val(result[1]);
    //            }, "json");
    //        }

    //        CalculateDuration();
    //        return false;
    //    }

        function GetAuthorityInfo() {
            document.getElementById('StrEmpSearch').value = $('#LeaveApplication_strSupervisorID').val();

            var url = '/LMS/LeaveApplication/GetEmployeeInfo';
            var form = $('#frmLeaveApplication');
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

       // $('#LeaveApplication_strSupervisorID').val(<%= Model.LeaveApplication.strSupervisorID %>);
      //  GetAuthorityInfo();

    function handleEnter(evt) {
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

            $('#LeaveApplication_strResponsibleId').removeClass("invalid");
            id = document.getElementById('LeaveApplication_strResponsibleId').value;

            setData(id, name);
        }
        return true;
    }
</script>
<form id="frmLeaveApplication" method="post" action="" style="width: 100%">
<div id="divLeaveApplication">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.LeaveApplication.intApplicationID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplicationType)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsApprovalProcess)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.bitIsDiscard)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intAppStatusID)%>
            <%= Html.HiddenFor(m => m.intNodeID)%>
            <%= Html.HiddenFor(m => m.StrEmpSearch)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveYearID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.intDestNodeID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strStatus) %>
            <%= Html.HiddenFor(m => m.StrSearchLeaveYear) %>
            <%= Html.HiddenFor(m => m.StrYearStartDate) %>
            <%= Html.HiddenFor(m => m.StrYearEndDate) %>
            <%= Html.HiddenFor(m => m.strHalfDayFromTime)%>
            <%= Html.HiddenFor(m => m.strHalfDayToTime)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDesignationID) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDesignation) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDepartmentID) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strDepartment) %>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpID)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strEmpName)%>
            <%= Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>
             <%= Html.HiddenFor(m => m.LeaveApplication.intEarnLeaveType)%>
        </div>
    </div>
    <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
        <tr>
            <td style="width: 75%;">
                <table class="contenttable" cellspacing="0" style="width: 100%; padding: 0px">
                    <tr>
                        <td style="width: 19%;">
                            ID<label class="labelRequired">*</label>
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
                            Designation<label></label>
                        </td>
                        <td>
                            <%=Html.Encode(Model.LeaveApplication.strDesignation)%>
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
                        <td style="width: 26%;">
                            Department<label></label>
                        </td>
                        <td>
                            <%=Html.Encode(Model.LeaveApplication.strDepartment)%>
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td style="width: 26%;">
                            Branch<label></label>
                        </td>
                        <td>
                            <%=Html.Encode(Model.LeaveApplication.strBranch)%>
                        </td>
                    </tr>--%>
                </table>
            </td>
            <td valign="top" style="width: 180px;" align="right">

                    <% if (Model.IsEmployeePhoto == true) %>
                    <%{%>
                           
                           <%--<%=Html.Encode(Model.LeaveApplication.strEmpID)%>--%>
                            <div class="Photo-Top">
                                <img alt="Employee Photo" height="110px" width="Auto" src="<%=Url.Action("GetImage", "LeaveApplication", new { Id = Model.LeaveApplication.strEmpID })%>"/>
                            </div>
                    <%}%> 
                   <%else%>
                    <%{%>
                        <img style="height: 110px; width: Auto; padding-left: 20px;" imagealign="Right"
                        alt="Employee Photo" src="<%= Url.Content("~/Content/img/defaultPic.jpg")%>" />
                    <%}%>

                <%--<img style="width: 180px; height: 130px; padding-left: 20px;" imagealign="Right"
                    alt="" src="<%= Url.Content("~/Content/img/defaultPic.jpg")%>" />--%>
            </td>
        </tr>
    </table>
    <%--<table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px">
        <tr>
            <td style="width: 50%">
                <table class="contenttable" cellspacing="0" style="width: 100%; padding: 0px">
                    <tr>
                        <td style="width: 40%;">
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
                    <tr>
                        <td style="width: 40%;">
                            Leave Type<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.LeaveApplication.intLeaveTypeID, Model.OnLineLeaveType, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:240px; min-width:240px;", @onchange = "return CalculateDuration();" })%>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <table class="contenttable" cellspacing="0" style="width: 100%; padding: 0px">
                    <tr>
                        <td colspan="4" class="contenttabletd">
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
                            <%=Html.RadioButton("LeaveApplication.strApplicationType", "FullDayHalfDay", Model.LeaveApplication.IsFullDayHalfDay, new { onclick = "FullDayHourly(this); refreshDuration();  " })%>Full
                            Day and/or Half Day
                            <%=Html.Hidden("ApplicationType", Model.LeaveApplication.strApplicationType)%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="contenttabletd">
                            <div style="width: 100%; float: left; text-align: left;">
                                <div id="divWorkingTime" style="float: left; text-align: left; padding-left: 8px;">
                                    <%= Html.DropDownListFor(m => m.LeaveApplication.intDurationID, Model.WorkingTime, "...Select One...", new { @class = "selectBoxRegular", @style = "width:130px; min-width:130px;",onchange = "GetWoringTime();" })%>
                                </div>
                                <div id="divHalfDayFor" style="float: left; text-align: left; padding-left: 8px;">
                                    Of
                                    <%= Html.DropDownListFor(m => m.LeaveApplication.strHalfDayFor, Model.HalfDayFor, "...Select One...", new { @class = "selectBoxRegular", @style = "width:130px; min-width:130px;" })%>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>--%>
    <br />
    <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px">
        <tr>
            <td style="width: 15%;">
                Leave Type<label class="labelRequired">*</label>
            </td>
            <td style="width: 40%;">
                <%= Html.DropDownListFor(m => m.LeaveApplication.intLeaveTypeID, Model.OnLineLeaveType, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:240px; min-width:240px;", @onchange = "return CalculateDuration();" })%>
            </td>
            <td style="width: 15%;">
                <label>
                    Beginning Balance</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveApplication.fltBeforeBalance, new { @class = " textRegularDuration double", @readonly = true, @maxlength = "10" })%>
            </td>
        </tr>
        <%--<tr>
            <td style="width: 40%; padding: 0px;">
                Apply Date<label class="labelRequired">*</label>
            </td>
            <td>
                <label>
                    <%=Model.LeaveApplication.strApplyDate%></label>
            </td>
        </tr>--%>
        <%--<tr>
            <td style="width: 40%; padding: 0px;">
                Leave Year
            </td>
            <td>
                <%--<%= Html.Encode(Model.StrYearStartDate +" To "+Model.StrYearEndDate)%>
                <label id="lblLeaveYear">
                </label>
            </td>
        </tr>--%>
        <%--<tr>
                        <td style="width: 40%; padding: 0px;">                            
                        </td>
                        <td>
                             <div style="float: left; text-align: left;">
                                Date:
                             </div>
                             <div style="float: left; text-align: left; padding-left: 70px;">
                                <label id="lblTime">Time:</label>
                            </div>
                        </td>
                    </tr>--%>
        <tr>
            <td style="width: 15%;">
                Leave From<label class="labelRequired">*</label>
            </td>
            <td style="width: 40%;">
                <div style="float: left; text-align: left;">
                    <%--<%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromDate, new { @class = "textRegularDate dtPicker date", @style = "width:75px; min-width:75px;", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>--%>
                    <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromDate, new { @class = "textRegularDate dtPicker date", @style = "width:75px; min-width:75px;", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>
                </div>
                <%-- <div style="float: left; text-align: left; padding-left: 5px;">
                    <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromTime, new { @class = "textRegularTime timepicker", @onchange = "return HookupCalculation(this);", @maxlength = "8" })%>
                </div>--%>
            </td>
            <td style="width: 15%;">
                To<label class="labelRequired">*</label>
            </td>
            <td>
                <div style="float: left; text-align: left;">
                    <%--<%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToDate, new { @class = "textRegularDate required dtPicker date", @style = "width:75px; min-width:75px;", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>--%>
                    <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToDate, new { @class = "textRegularDate required dtPicker date", @style = "width:75px; min-width:75px;", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>
                </div>
                <%-- <div style="float: left; text-align: left; padding-left: 5px;">
                    <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToTime, new { @class = "textRegularTime timepicker",  @onchange = "return HookupCalculation(this);", @maxlength = "8" })%>
                </div>--%>
            </td>
        </tr>
        <%-- <tr>
            <td style="width: 40%; padding: 0px;">
                To<label class="labelRequired">*</label>
            </td>
            <td>
                <div style="float: left; text-align: left;">
                   
                    <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToDate, new { @class = "textRegularDate required dtPicker date", @style = "width:75px; min-width:75px;", @maxlength = "10" })%>
                </div>
                 <div style="float: left; text-align: left; padding-left: 5px;">
                    <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToTime, new { @class = "textRegularTime timepicker",  @onchange = "return HookupCalculation(this);", @maxlength = "8" })%>
                </div>
            </td>
        </tr>--%>
        <tr>
            <td style="width: 15%;">
                Duration<label class="labelRequired">*</label>
            </td>
            <td style="width: 40%;">
                <%=Html.TextBoxFor(m => m.LeaveApplication.fltDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onkeyup = "return ismaxlengthPop(this)", @onchange = "return HookupCalculation(this);" })%>
                Days
                <%--<%=Html.TextBoxFor(m => m.LeaveApplication.fltDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onkeyup = "return ismaxlengthPop(this)", @onchange = "return HookupCalculation(this);" })%>
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
                            <%} %>--%>
                <%--<div style="float: right; text-align: right;">
                    <a id="btnCalculate" href="#" class="btnCalculate" onclick="return CalculateDuration();">
                    </a>
                </div>--%>
            </td>
            <td style="width: 15%;">
             With Pay Duration<label class="labelRequired">*</label>
                
            </td>
            <td>
             <%=Html.TextBoxFor(m => m.LeaveApplication.fltWithPayDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onchange = "SplitDuration(this)" })%>
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
            </td>
        </tr>
<%--        <div style="visibility: hidden;">--%>
<%--            <%= Html.HiddenFor(model => model.LeaveApplication.fltWithPayDuration, new { @onchange = "SplitDuration(this);" })%>
            <%= Html.HiddenFor(model => model.LeaveApplication.fltWithoutPayDuration,new {  @onchange = "SplitDuration(this);" })%>--%>
          <tr >
            <td style="width: 15%;">
                Without Pay Duration<label class="labelRequired">*</label>
            </td>
            <td>
             <%=Html.TextBoxFor(m => m.LeaveApplication.fltWithoutPayDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onchange = "SplitDuration(this);" })%>
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
               
            </td>

            <td style="width: 15%;">
               <label>
                    Closing Balance</label>
            </td>
            <td>
                            <%=Html.TextBox("LeaveApplication_fltNetBalance", Model.LeaveApplication.fltNetBalance.ToString("#0.00"), new { @class = " textRegularDuration", @readonly = "readonly" })%>
                <label>
                    Days</label>
               
            </td>
        </tr>
<%--        </div>--%>
        <%-- <tr>
            <td style="width: 40%; padding: 0px;">
                <label style="font-weight: bold">
                    Net Balance</label>
            </td>
            <td>
                <%=Html.TextBox("LeaveApplication_fltNetBalance", Model.LeaveApplication.fltNetBalance.ToString("#0.00"), new { @class = " textRegularDuration", @readonly = "readonly" })%>
                <label>
                    Days</label>
            </td>
        </tr>--%>
        <%if (Model.LeaveApplication.strSupervisorID != null)
          { %>
        <tr>
            <td style="width: 15%; padding: 0px;">
                <%=Html.HiddenFor(m => m.LeaveApplication.strSupervisorID)%>
                Rec./Approve By<label class="labelRequired">*</label>              
            </td>
            <td>
                <%--<%if (Model.Approver.Count > 0)
                  { %>
                <%= Html.DropDownListFor(m => m.LeaveApplication.strSupervisorID, Model.Approver, "...Select One...", new { @class = "selectBoxRegular", @style = "width:243px; min-width:243px;", onchange = "return GetAuthorityInfo();" })%>
                <%}
                  else
                  { %>
                <%= Html.DropDownListFor(m => m.LeaveApplication.strSupervisorID, Model.Approver, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:242px; min-width:242px;" })%>
                <%} %>--%>

                <%=Html.TextBoxFor(m => m.LeaveApplication.strSupervisorInitial, new { @class = "textRegularDate required readonly", @readonly = true })%>
                <a href="#" class="btnSearch" onclick="return openEmployee('0');"></a>
                                
            </td>
            <td style="width: 15%; padding: 0px;">
                Designation
            </td>
            <td>
                <%=Html.TextBox("strAuthorDesignation", Model.strAuthorDesignation, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
            </td>
        </tr>

        <tr>
             <td style="width: 15%; padding: 0px;">
                Name
            </td>
            <td>
                <%=Html.TextBox("strSupervisorName","", new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
            </td>
            <td style="width: 15%; padding: 0px;">
                Department
            </td>
            <td>
                <%=Html.TextBox("strAuthorDepartment", Model.strAuthorDepartment, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr>
        <td style="width: 15%;">
                <%=Html.HiddenFor(m => m.LeaveApplication.strResponsibleId)%>
                Responsible Person (ID)
            </td>
            <td style="width: 40%;">
                <%=Html.TextBoxFor(m => m.LeaveApplication.strResponsibleInitial, new { @class = "textRegularDate", @readonly = true })%>
                <a href="#" class="btnSearch" onclick="return openEmployee('1');"></a>
                <%--<label id="lblIdNotFound" style="visibility: hidden; vertical-align: 5px; padding-left: 5px;
                    color: red;">
                    Id not found !</label>--%>
            </td>
        </tr>
        <%
          } %>
    </table>
    <br />
    <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px">
        <%--<tr>

            <td style="width: 15%;">
                <%=Html.HiddenFor(m => m.LeaveApplication.strPLID)%>
               PL(Initial)
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveApplication.strPLInitial, new { @class = "textRegularDate required", @readonly = true })%>
                <a href="#" class="btnSearch" onclick="return openEmployee('2');"></a>
            </td>
        </tr>--%>
        <%--<tr>
            <td style="width: 42%; padding: 0px;">
                Name
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveApplication.strResponsibleName, new { @class = "textRegular", @style = "width:313px; min-width:313px;", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr>
            <td style="width: 20%; padding: 0px;">
                Designation
            </td>
            <td>
                <%=Html.TextBox("strResDesignation", Model.strResDesignation, new { @class = "textRegular",@style = "width:313px; min-width:313px;", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr>
            <td style="width: 20%; padding: 0px;">
                Division/Unit
            </td>
            <td>
                <%=Html.TextBox("strResDepartment", Model.strResDepartment, new { @class = "textRegular",@style = "width:313px; min-width:313px;", @readonly = "readonly" })%>
            </td>
        </tr>--%>
        <tr>
            <td style="width: 15%;">
                Purpose<label class="labelRequired">*</label>
            </td>
            <td style="width: 40%;">
                <%=Html.TextAreaFor(m => m.LeaveApplication.strPurpose, new { @class = "textRegular required", @maxlength = 100, onkeyup = "return ismaxlengthPop(this)" })%>
            </td>
            <td style="width: 15%;">
                Address During Leave
            </td>
            <td>
                <%=Html.TextAreaFor(m => m.LeaveApplication.strContactAddress, new { @class = "textRegular",  @maxlength = 200, onkeyup = "return ismaxlengthPop(this)" })%>
            </td>
        </tr>
        <tr>
            <td style="width: 15%; padding: 0px;">
                Country
            </td>
            <td style="width: 40%;">
               <%= Html.DropDownListFor(m => m.LeaveApplication.intCountryID, Model.CountryList, "...Select One...", new { @class = "selectBoxRegular", @style = "width:240px; min-width:240px;" })%>
            </td>
            <td style="width: 15%;">
                Contact No.
            </td>
            <td >
                <%=Html.TextBoxFor(m => m.LeaveApplication.strContactNo, new { @class = "textRegular", @maxlength = "50", onkeyup = "return ismaxlengthPop(this)" })%>
            </td>
        </tr>
        <tr>
            <td>
                Comments
            </td>
            <td colspan="3" style="width: auto">
                <%=Html.TextAreaFor(m => m.LeaveApplication.strRemarks, new { @class = "textRegular",  @maxlength = 200, @style="width:100%", onkeyup = "return ismaxlengthPop(this)" })%>
            </td>
        </tr>
        <%--<tr>
            <td style="width: 15%; padding: 0px;">
                Comments
            </td>
            <td>
                <%=Html.TextAreaFor(m => m.LeaveApplication.strRemarks, new { @class = "textRegular", @style = "width:313px; min-width:313px;", @maxlength = 200, onkeyup = "return ismaxlengthPop(this)" })%>
            </td>
        </tr>--%>
    </table>
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div id="divLedger">
        <% Html.RenderPartial("LeaveLedger"); %>
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OnlineLeaveApplication, LMS.Web.Permission.MenuOperation.Cancel))
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
