<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<script type="text/javascript">



    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveApplication"));

        setTitle("Off line Leave Application");


        $("#btnSave").hide();
        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        //FullDayHourly($("#ApplicationType"));
        rowShowHide();

        //document.getElementById('hdnEmpId').value = $('#LeaveApplication_strEmpID').val();
        //document.getElementById('hdnEmpId_1').value = $('#LeaveApplication_strOfflineApprovedById').val();
        //document.getElementById('hdnEmpId_2').value = $('#LeaveApplication_strResponsibleId').val();

        FormatTextBox();

    });


    function rowShowHide() {
        var intNod = document.getElementById('LeaveApplication_intDestNodeID').value;
        var intAppId = document.getElementById('LeaveApplication_intApplicationID').value;

        if (parseInt(intAppId) <= 0) {
            $('#LeaveApplication_strOfflineApprovedById').val('');
            //            $('#LeaveApplication_strOfflineApproverName').val('');
            //            $('#strOffAuthorDesignation').val('');
            //            $('#strOffAuthorDepartment').val('');
        }

        if (parseInt(intNod) > 0) {
            $('#LeaveApplication_strOfflineApprovedById').removeClass('required');
            $('#LeaveApplication_strApprovedByInitial').removeClass('required');
            $('#LeaveApplication_strOffLineAppvDate').removeClass('required');
            $('#LeaveApplication_strOffLineAppvDate').removeClass('date');
            $('#LeaveApplication_strOffLineAppvDate').addClass('dateNR');

            //$('#lblRecPerson').css('visibility', 'visible');
            //$('#LeaveApplication_strSupervisorID').addClass('required');

            //            $('#trApproved').css('display', 'none');
            //            $('#trRecPerson').css('display', '');

            $('#abtnSave').removeClass('btnSave');
            $('#abtnSave').addClass('btnSubmit');
        }
        else {
            $('#LeaveApplication_strOfflineApprovedById').addClass('required');
            $('#LeaveApplication_strOffLineAppvDate').addClass('required');
            $('#LeaveApplication_strOffLineAppvDate').removeClass('dateNR');
            $('#LeaveApplication_strOffLineAppvDate').addClass('date');

            //$('#lblRecPerson').css('visibility', 'hidden');
            //$('#LeaveApplication_strSupervisorID').removeClass('required');

            //            $('#trApproved').css('display', '');
            //            $('#trRecPerson').css('display', 'none');

            $('#abtnSave').removeClass('btnSubmit');
            $('#abtnSave').addClass('btnSave');

        }
    }


    function setData(id, strEmpInitial, name) {
        var strSrc = document.getElementById('StrEmpSearch').value;
        if (strSrc == '0') {
            document.getElementById('LeaveApplication_strEmpID').value = id;
            document.getElementById('LeaveApplication_strEmpInitial').value = strEmpInitial;
            document.getElementById('LeaveApplication_strEmpName').value = name;

            $("#lblIdNotFound").css('visibility', 'hidden');
            $('#LeaveApplication_strEmpID').removeClass("invalid");


            var url = "/LMS/OfflineLeaveApplication/GetEmployeeInfo";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            $('#LeaveApplication_strDesignationID').val('');
            $('#LeaveApplication_strDesignation').val('');

            $('#intNodeID').val(0);

            $.post(url, serializedForm, function (result) {
                document.getElementById('LeaveApplication_strDesignationID').value = result[0];
                document.getElementById('LeaveApplication_strDesignation').value = result[1];

                document.getElementById('LeaveApplication_strEmpName').value = result[8];

                if (result[8] == null || result[8] == "") {
                    document.getElementById('LeaveApplication_strEmpName').value = "";
                    document.getElementById('LeaveApplication_strEmpID').value = "";
                    document.getElementById('LeaveApplication_strEmpInitial').value = "";
                    $("#lblIdNotFound").css('visibility', 'visible');
                }

                //---[populate employee Id wise leave type and next approver list]-----------------------------------
                $('#LeaveApplication_intLeaveTypeID > option:not(:first)').remove();



                $.each(result[4], function () {
                    $("#LeaveApplication_intLeaveTypeID").append($("<option></option>").val(this['Value']).html(this['Text']));
                });


                document.getElementById('intNodeID').value = result[6];
                document.getElementById('LeaveApplication_intDestNodeID').value = result[6];

                $('#LeaveApplication_strSupervisorID').empty();
                $("#LeaveApplication_strSupervisorID").append("<option value> ...Select One...</option>");
                result[9].forEach(function (item) {
                    //console.log(item);
                    var newOption = "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    $("#LeaveApplication_strSupervisorID").append(newOption);
                });
                //--------------------------------------------------------------------------
                rowShowHide();
                //   getAuthorityInfo(); // Enable This Code For BEPZA 30-Jan-2017
                getEmployeeLedger();

            }, "json");

            $("#divEmpList").dialog('close');
        }
        else if (strSrc == '1') {

            document.getElementById('LeaveApplication_strOfflineApprovedById').value = id;
            document.getElementById('LeaveApplication_strApprovedByInitial').value = strEmpInitial;

            $('#LeaveApplication_strOfflineApprovedById').removeClass("invalid");

            var url = "/LMS/OfflineLeaveApplication/GetEmployeeInfo";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            //                        $('#LeaveApplication_strOffLineAppvDesignationID').val('');
            $('#strOffAuthorDesignation').val('');
            //                        $('#LeaveApplication_strOffLineAppvDepartmentID').val('');
            $('#strOffAuthorDepartment').val('');

            $.post(url, serializedForm, function (result) {

                //                            document.getElementById('LeaveApplication_strOffLineAppvDesignationID').value = result[0];
                document.getElementById('strOffAuthorDesignation').value = result[1];
                //                            document.getElementById('LeaveApplication_strOffLineAppvDepartmentID').value = result[2];
                document.getElementById('strOffAuthorDepartment').value = result[3];
                document.getElementById('strOfflineApproverName').value = result[8];

                if (result[8] == null || result[8] == "") {
                    //document.getElementById('LeaveApplication_strOfflineApproverName').value = "";
                    //document.getElementById('hdnEmpId_1').value = "";
                    document.getElementById('LeaveApplication_strApprovedByInitial').value = "";
                    //alert('Id not found !');
                    $("#lblIdNotFound1").css('visibility', 'visible');
                }
                else {
                    $("#lblIdNotFound1").css('visibility', 'hidden');
                }

            }, "json");

            $("#divEmpList").dialog('close');
        }
        else if (strSrc == '2') {

            document.getElementById('LeaveApplication_strResponsibleId').value = id;
            document.getElementById('strResponsibleName').value = name;
            document.getElementById('LeaveApplication_strResponsibleInitial').value = strEmpInitial;

            $('#LeaveApplication_strResponsibleId').removeClass("invalid");

            var url = "/LMS/OfflineLeaveApplication/GetEmployeeInfo";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();


            $('#strResDesignation').val('');

            $('#strResDepartment').val('');

            $.post(url, serializedForm, function (result) {
                document.getElementById('strResDesignation').value = result[1];
                document.getElementById('strResDepartment').value = result[3];
                document.getElementById('strResponsibleName').value = result[8];

                if (result[8] == null || result[8] == "") {
                    document.getElementById('LeaveApplication_strResponsibleInitial').value = "";
                    $("#lblIdNotFound1").css('visibility', 'visible');
                }
                else {
                    $("#lblIdNotFound1").css('visibility', 'hidden');
                }

            }, "json");

            $("#divEmpList").dialog('close');
        }
        else if (strSrc == '3') {

            document.getElementById('strSupervisorName').value = name;
            document.getElementById('LeaveApplication_strSupervisorInitial').value = strEmpInitial;
           
            $('#LeaveApplication_strSupervisorID').removeClass("invalid");

            var url = "/LMS/OfflineLeaveApplication/GetEmployeeInfo";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            $('#strAuthorDesignation').val('');
            $('#strAuthorDepartment').val('');

            $.post(url, serializedForm, function (result) {

                $('#strAuthorDesignation').val(result[1]);
                $('#strAuthorDepartment').val(result[3]);
                document.getElementById('strSupervisorName').value = result[8];
                document.getElementById('LeaveApplication_strSupervisorID').value = result[10];

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


    function SplitDuration(obj) {

        var objDuration = document.getElementById('LeaveApplication_fltDuration');
        var objfltWithPayDuration = document.getElementById('LeaveApplication_fltWithPayDuration');
        var objfltWithoutPayDuration = document.getElementById('LeaveApplication_fltWithoutPayDuration');

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

        CalculateLeaveLedger();

    }


    function getEmployeeLedger() {
        //var targetDiv = "#divOfflineLeaveApplicationDetails";
        var targetDiv = "#divLedger";
        var url = "/LMS/OfflineLeaveApplication/GetLedger";
        var form = $("#frmLeaveApplication");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }

    function getEmployeeLeaveType() {
        var targetDiv = "#divLeaveType";
        var url = "/LMS/OfflineLeaveApplication/GetLeaveType";
        var form = $("#frmLeaveApplication");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        return false;
    }






    function parseDate(str) {
        var dmy = str.split('-');
        var mdy = dmy[1] + '/' + dmy[0] + '/' + dmy[2];
        return new Date(mdy);
    }



    function checkDateValidation() {
        var pdtYStart = parseDate($('#StrYearStartDate').val());
        var pdtYEnd = parseDate($('#StrYearEndDate').val());

        var intLVTId = $('#LeaveApplication_intLeaveTypeID').val();
        var strEmpId = $('#LeaveApplication_strEmpID').val();

        if (strEmpId.toString() == "" && $('#ApplicationType').val() != "Hourly") {
            alert("Please Select Employee ID.");
            return false;
        }

        if (intLVTId == 0 && $('#ApplicationType').val() != "Hourly") {
            alert("Please Select Leave Type.");
            return false;
        }

        //        if ($('#ApplicationType').val() == "Hourly") {
        //            validateHour();
        //        }

        if ($('#LeaveApplication_strApplyFromDate').val() != "" && $('#LeaveApplication_strApplyToDate').val() != "") {

            if (fnValidateDateTime() == false) {
                alert("Invalid Date.");
                return false;
            }

            var pdtAPFrom = parseDate($('#LeaveApplication_strApplyFromDate').val());
            var pdtAPTo = parseDate($('#LeaveApplication_strApplyToDate').val());

            if (pdtAPFrom > pdtAPTo) {
                alert("Leave To Date must be equal or greater than Leave From Date.");
                return false;
            }

            if (pdtYStart > pdtAPFrom) {
                alert("Leave From Date must be between current leave year.");
                return false;
            }

            if (pdtAPTo > pdtYEnd) {
                alert("Leave To Date must be between current leave year.");
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

        if ($('#LeaveApplication_strOffLineAppvDate').val() != "" && $('#LeaveApplication_strOfflineApprovedById').val() != "") {
            var pdtAPVDate = parseDate($('#LeaveApplication_strOffLineAppvDate').val());
            var pdtAPDate = parseDate($('#LeaveApplication_strApplyDate').val());

            if (pdtAPVDate > pdtYEnd || pdtYStart > pdtAPVDate) {
                alert("Approved Date must be between current leave year.");
                return false;
            }

            if (pdtAPDate > pdtAPVDate) {
                alert("Approved Date must be equal or greater than leave apply date.");
                return false;
            }

        }


        if (document.getElementById('LeaveApplication_strEmpID').value != "" && document.getElementById('LeaveApplication_strOfflineApprovedById').value != "") {
            if (document.getElementById('LeaveApplication_strEmpID').value == document.getElementById('LeaveApplication_strOfflineApprovedById').value) {
                alert("Applicant cann't be approver.");
                return false;
            }

        }

        if (document.getElementById('LeaveApplication_strEmpID').value != "" && document.getElementById('LeaveApplication_strResponsibleId').value != "") {
            if (document.getElementById('LeaveApplication_strEmpID').value == document.getElementById('LeaveApplication_strResponsibleId').value) {
                alert("Applicant cann't be responsible person.");
                return false;
            }
        }

        return true;
    }


    function HookupCalculation(obj) {

        if (obj.id == 'LeaveApplication_strApplyFromDate' || $('#ApplicationType').val() == "Hourly") {
            $('#LeaveApplication_strApplyToDate').val($('#LeaveApplication_strApplyFromDate').val());
        }

        //        if (obj.id == 'LeaveApplication_strApplyFromTime') {
        //            $('#LeaveApplication_strApplyToTime').val($('#LeaveApplication_strApplyFromTime').val());
        //        }

        CalculateDuration();
        return false;
    }


    function CalculateDuration() {
        var intLVTId = $('#LeaveApplication_intLeaveTypeID').val();
        var strEmpId = $('#LeaveApplication_strEmpID').val();

        if (strEmpId.toString() == "" && $('#ApplicationType').val() == "FullDay") {
            $('#LeaveApplication_intLeaveTypeID').children('option[value=""]').attr("selected", true);
            alert("Please Choose Applicant Initial.");
            return false;
        }

        if (intLVTId == '' && $('#ApplicationType').val() != "Hourly") {
            alert("Please Select Leave Type.");
            return false;
        }

        if (fnValidateDateTime() == false) {

            return false;
        }

        if (checkDateValidation() == true) {

            var targetDiv = '#divOfflineLeaveApplicationDetails';
            var url = '/LMS/OfflineLeaveApplication/CalcutateDuration';
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            if (intLVTId != '') {
                $.post(url, serializedForm, function (result) {
                    $('#LeaveApplication_fltDuration').val(result[0]);
                    $('#LeaveApplication_fltWithPayDuration').val(result[1]);
                    $('#LeaveApplication_fltWithoutPayDuration').val(result[2]);
                    $('#LeaveApplication_fltNetBalance').val(result[3]);

                    $('#LeaveApplication_intLeaveYearID').val(result[4]);
                    $('#LeaveApplication_fltBeforeBalance').val(result[5]);
                    //$('#lblLeaveYear').text(result[5]);

                    targetDiv = '#divLedger';
                    url = '/LMS/OfflineLeaveApplication/GetLedger';
                    serializedForm = form.serialize();
                    $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

                }, "json");
            }
            else {
                $('#LeaveApplication_fltDuration').val(0);
                $('#LeaveApplication_fltWithPayDuration').val(0);
                $('#LeaveApplication_fltWithoutPayDuration').val(0);
                $('#LeaveApplication_fltNetBalance').val(0);
                $('#LeaveApplication_intLeaveYearID').val(0);
                $('#LeaveApplication_fltBeforeBalance').val(0);
            }
        }
        return false;
    }


    function CalculateLeaveLedger() {
        //var targetDiv = "#divOfflineLeaveApplicationDetails";
        var targetDiv = "#divLedger";
        var url = "/LMS/OfflineLeaveApplication/GetLedger";
        var form = $("#frmLeaveApplication");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

    }

    //    function refreshDuration() {
    //        $('#LeaveApplication_fltDuration').val(0);
    //        $('#LeaveApplication_fltWithPayDuration').val(0);
    //        $('#LeaveApplication_fltWithoutPayDuration').val(0);
    //        $('#LeaveApplication_strApplyFromTime').val('');
    //        $('#LeaveApplication_strApplyToTime').val('');
    //        $('#LeaveApplication_fltNetBalance').val(0.00);

    //        //--[add by shaiful 04-Nov-2010]
    //        CalculateLeaveLedger();

    //    }


    //    function FullDayHourly(e) {

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

    //            $('#lblTime').css('visibility', 'hidden');
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

    //            $('#lblTime').css('visibility', 'visible');
    //            $('#LeaveApplication_strApplyFromTime').css('visibility', 'visible');
    //            $('#LeaveApplication_strApplyToTime').css('visibility', 'visible');
    //            $('.lblDaysHour').html("Hours");
    //            $('#btnCalculate').css('visibility', 'hidden');

    //            $('#LeaveApplication_strApplyToDate').val($('#LeaveApplication_strApplyFromDate').val());

    //            //Apply CSS
    //            $('#LeaveApplication_strApplyFromTime').addClass('required');
    //            $('#LeaveApplication_strApplyToTime').addClass('required');

    //            $('#LeaveApplication_fltDuration').attr('readonly', true);

    //            $('#LeaveApplication_strApplyToDate').removeClass('dtPicker');
    //            $('#LeaveApplication_strApplyToDate').removeClass('date');
    //            $('#LeaveApplication_strApplyToDate').attr('readonly', true);

    //            $('#LeaveApplication_strApplyFromTime').removeAttr('disabled');
    //            $('#LeaveApplication_strApplyToTime').removeAttr('disabled');



    //            $(".timepicker").timepicker({ ampm: true, timeFormat: 'hh:mm:TT'
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

    //            $('#lblTime').css('visibility', 'visible');
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

    //        $('#ApplicationType').val(strApplicationType);

    //        return false;
    //    }

    function searchEmployee() {
        window.parent.openEmployee();
    }


    function OfflineApprove() {

        var intLVTId = $('#LeaveApplication_intLeaveTypeID').val();
        var strEmpId = $('#LeaveApplication_strEmpID').val();

        if (strEmpId.toString() == "") {
            alert("Please Select Employee Initial.");
            return false;
        }

        if (intLVTId == 0) {
            alert("Please Select Leave Type.");
            return false;
        }

        if (parseFloat($('#LeaveApplication_fltDuration').val()) > 0) {
            if (parseFloat($('#LeaveApplication_fltDuration').val()) != parseFloat($("#LeaveApplication_fltWithPayDuration").val()) + parseFloat($("#LeaveApplication_fltWithoutPayDuration").val())) {
                alert("Summation  of With Pay Duration and Without Pay Duration must be equal of Duration.");
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
        if (parseFloat(netBL) < 0) {
            var result = confirm('Net balance is ' + netBL.toString() + '. Do you want to proceed?');
            if (result == false) {
                return false;
            }
        }

        //---[check invalid employee ID]---------------
        //        if (document.getElementById('LeaveApplication_strEmpID').value != "" && document.getElementById('hdnEmpId').value != document.getElementById('LeaveApplication_strEmpID').value) {
        //            $('#LeaveApplication_strEmpID').addClass("invalid");
        //        }
        //        if (document.getElementById('LeaveApplication_strOfflineApprovedById').value != "" && document.getElementById('hdnEmpId_1').value != document.getElementById('LeaveApplication_strOfflineApprovedById').value) {
        //            $('#LeaveApplication_strOfflineApprovedById').addClass("invalid");
        //        }
        //        if (document.getElementById('LeaveApplication_strResponsibleId').value != "" && document.getElementById('hdnEmpId_2').value != document.getElementById('LeaveApplication_strResponsibleId').value) {
        //            $('#LeaveApplication_strResponsibleId').addClass("invalid");
        //        }


        if (fnValidate() && checkDateValidation() == true) {
            var confResult = true;
            var url = '/LMS/OfflineLeaveApplication/ValidateLeaveApplication';
            var form = $('#frmLeaveApplication');
            var serializedForm = form.serialize();


            $.post(url, serializedForm, function (result) {

                if (result[0] != null && result[0] != "") {
                    alert(result);
                    return false;
                }
                else {
                    url = '/LMS/OfflineLeaveApplication/GetAuthorityResPersonLeaveStatus';
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
                            url = "/LMS/OfflineLeaveApplication/OfflineApprove";
                            var targetDiv = "#divOfflineLeaveApplicationDetails";
                            serializedForm = form.serialize();

                            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
                        }
                    }, "json");
                }
            }, "json");
        }
        return false;
    }

    function OfflineUpdate() {

        var intLVTId = $('#LeaveApplication_intLeaveTypeID').val();
        var strEmpId = $('#LeaveApplication_strEmpID').val();

        if (strEmpId.toString() == "") {
            alert("Please Select Employee Initial.");
            return false;
        }

        if (intLVTId == 0) {
            alert("Please Select Leave Type.");
            return false;
        }

        if (parseFloat($('#LeaveApplication_fltDuration').val()) > 0) {
            if (parseFloat($('#LeaveApplication_fltDuration').val()) != parseFloat($("#LeaveApplication_fltWithPayDuration").val()) + parseFloat($("#LeaveApplication_fltWithoutPayDuration").val())) {
                alert("Summation  of With Pay Duration and Without Pay Duration must be equal of Duration.");
                return false;
            }
        }

        var LWP = $("#LeaveApplication_fltWithoutPayDuration").val();
        if (parseFloat(LWP) > 0) {
            var result = confirm('This application has leave without pay duration. Do you want to proceed?');
            if (result == false) {
                return false;
            }
        }

        var netBL = $("#LeaveApplication_fltNetBalance").val();
        if (netBL < 0) {
            var result = confirm('Net balance is ' + netBL.toString() + '. Do you want to proceed?');
            if (result == false) {
                return false;
            }
        }

        //---[check invalid employee ID]---------------
        //        if (document.getElementById('LeaveApplication_strEmpID').value !="" && document.getElementById('hdnEmpId').value != document.getElementById('LeaveApplication_strEmpID').value) {
        //            $('#LeaveApplication_strEmpID').addClass("invalid");
        //        }
        //        else {
        //            $('#LeaveApplication_strEmpID').removeClass("invalid");
        //        }
        //        if (document.getElementById('LeaveApplication_strOfflineApprovedById').value !="" && document.getElementById('hdnEmpId_1').value != document.getElementById('LeaveApplication_strOfflineApprovedById').value) {
        //            $('#LeaveApplication_strOfflineApprovedById').addClass("invalid");
        //        }
        //        else {
        //            $('#LeaveApplication_strOfflineApprovedById').removeClass("invalid");
        //        }
        //        if (document.getElementById('LeaveApplication_strResponsibleId').value !="" && document.getElementById('hdnEmpId_2').value != document.getElementById('LeaveApplication_strResponsibleId').value) {
        //            $('#LeaveApplication_strResponsibleId').addClass("invalid");
        //        }
        //        else {
        //            $('#LeaveApplication_strResponsibleId').removeClass("invalid");
        //        }

        if (fnValidate() && checkDateValidation() == true) {
            var confResult = true;

            var url = '/LMS/OfflineLeaveApplication/ValidateLeaveApplication';
            var form = $('#frmLeaveApplication');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {

                if (result[0] != null && result[0] != "") {
                    alert(result);
                    return false;
                }
                else {
                    url = '/LMS/OfflineLeaveApplication/GetAuthorityResPersonLeaveStatus';
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
                            url = '/LMS/OfflineLeaveApplication/OfflineUpdate';
                            var targetDiv = '#divOfflineLeaveApplicationDetails';
                            serializedForm = form.serialize();

                            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
                        }

                    }, "json");
                }

            }, "json");
        }
        return false;
    }


    function OfflineDelete() {
        var result = confirm('Pressing OK will delete this application. Do you want to continue?');
        if (result == true) {
            Id = $('#LeaveApplication_intApplicationID').val();

            var targetDiv = "#divOfflineLeaveApplicationDetails";
            var url = "/LMS/OfflineLeaveApplication/OfflineDelete";
            var form = $("#frmLeaveApplication");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }

        return false;
    }

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

    // Enable This Code For BEPZA 30-Jan-2017
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

    function GetResponsibleInfo() {
        document.getElementById('StrEmpSearch').value = $('#LeaveApplication_strResponsibleId').val();

        var url = '/LMS/LeaveApplication/GetEmployeeInfo';
        var form = $('#frmLeaveApplication');
        var serializedForm = form.serialize();

        $('#strResDesignation').val('');
        $('#strResDepartment').val('');

        if ($('#StrEmpSearch').val() != "") {
            $.post(url, serializedForm, function (result) {
                $('#strResDesignation').val(result[1]);
                $('#strResDepartment').val(result[3]);
                $('#LeaveApplication_strResponsibleName').val(result[5]);

                if (result[5] == null || result[5] == "") {
                    document.getElementById('LeaveApplication_strResponsibleName').value = "";
                    //                    document.getElementById('hdnEmpId_2').value = "";
                    document.getElementById('LeaveApplication_strResponsibleInitial').value = "";

                    $("#lblIdNotFound2").css('visibility', 'visible');
                }
                else {
                    $("#lblIdNotFound2").css('visibility', 'hidden');
                }
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
                $('#LeaveApplication_strResponsibleId').removeClass("invalid");
                id = document.getElementById('LeaveApplication_strResponsibleId').value;
            }
            else if (srcflag == '3') {
                $('#LeaveApplication_strSupervisorID').removeClass("invalid");
                id = document.getElementById('LeaveApplication_strSupervisorID').value;
            }

            setData(id, name);
        }
        return true;
    }


             
</script>
<form id="frmLeaveApplication" method="post" action="">
<div id="offDetails">
    <div id="divLeaveApplication">
        <div style="width: 100%">
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
                    <%= Html.HiddenFor(m => m.LeaveApplication.intDestNodeID)%>
                    <%= Html.HiddenFor(m => m.LeaveApplication.dtApplyDate) %>
                    <%= Html.HiddenFor(m => m.LeaveApplication.strCompanyID) %>
                    <%= Html.HiddenFor(m => m.LeaveApplication.strStatus) %>
                    <%= Html.HiddenFor(m => m.StrYearStartDate) %>
                    <%= Html.HiddenFor(m => m.StrYearEndDate) %>
                    <%= Html.HiddenFor(m => m.StrEmpSearch)%>
                    <%= Html.HiddenFor(m => m.Isapplicable)%>
                    <%= Html.HiddenFor(m => m.strHalfDayFromTime) %>
                    <%= Html.HiddenFor(m => m.strHalfDayToTime) %>
                    <%= Html.HiddenFor(m => m.LeaveApplication.strApplicationType)%>
<%--                    <%= Html.HiddenFor(m => m.LeaveApplication.strApplyDate)%>--%>
                    <%--     <%= Html.HiddenFor(model => model.LeaveApplication.fltWithPayDuration, new { @onchange = "SplitDuration(this);" })%>
                    <%= Html.HiddenFor(model => model.LeaveApplication.fltWithoutPayDuration,new {  @onchange = "SplitDuration(this);" })%>--%>
                </div>
            </div>
            <div class="divRow">
                <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px;">
                    <tr>
                        <td style="width: 75%;">
                            <table class="contenttable" cellspacing="0" style="width: 100%; padding: 0px;">
                                <tr>
                                    <td style="width: 19%;">
                                        Applicant ID<label class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.HiddenFor(m => m.LeaveApplication.strEmpID)%>
                                        <%if (Model.LeaveApplication.intApplicationID <= 0)
                                          { %>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strEmpInitial, new { @class = "textRegularDate required", @readonly = "readonly" })%>
                                        <a href="#" class="btnSearch" onclick="return openEmployee('0');"></a>
                                        <label id="lblIdNotFound" style="visibility: hidden; vertical-align: 5px; padding-left: 10px;
                                            color: red;">
                                            Id not found !</label>
                                        <%}
                                          else
                                          {
                                        %>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strEmpInitial, new { @class = "textRegularDate required", @readonly = "readonly" })%>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 19%;">
                                        Applicant Name
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveApplication.strEmpName, new { @class = "textRegular", @style = "width:465px; min-width:465px;", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 19%;">
                                        Designation
                                    </td>
                                    <td>
                                        <%= Html.HiddenFor(m => m.LeaveApplication.strDesignationID)%>
                                        <%= Html.TextBoxFor(m => m.LeaveApplication.strDesignation, new { @class = "textRegular", @style = "width:465px; min-width:465px;", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td style="width: 26%;">
                                        Division/Unit
                                    </td>
                                    <td>
                                        <%= Html.HiddenFor(m => m.LeaveApplication.strDepartmentID)%>
                                        <%= Html.TextBoxFor(m => m.LeaveApplication.strDepartment, new { @class = "textRegular", @style = "width:465px; min-width:465px;", @readonly = "readonly" })%>
                                    </td>
                                </tr>--%>
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
                            Leave Type<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%if (Model.LeaveApplication.intLeaveTypeID > 0)
                              { %>
                            <%= Html.HiddenFor(m => m.LeaveApplication.intLeaveTypeID)%>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strLeaveType, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                            <%}
                              else
                              {
                            %>
                            <%= Html.DropDownListFor(m => m.LeaveApplication.intLeaveTypeID, Model.OnLineLeaveType, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:240px; min-width:240px;", @onchange = "return CalculateDuration();" })%>
                            <%} %>
                        </td>
                        <td style="width: 15%;">
                            <label>
                                Beginning Balance</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltBeforeBalance, new { @class = " textRegularDuration double", @readonly = true, @maxlength = "10" })%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Leave From<label class="labelRequired">*</label>
                        </td>
                        <td style="width: 40%;">
                            <div style="float: left; text-align: left;">
                                <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyFromDate, new { @class = "textRegularDate dtPicker date", @style = "width:75px; min-width:75px;", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>
                            </div>
                        </td>
                        <td style="width: 15%;">
                            To<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <div style="float: left; text-align: left;">
                                <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyToDate, new { @class = "textRegularDate required dtPicker date", @style = "width:75px; min-width:75px;", @onchange = "return HookupCalculation(this);", @maxlength = "10" })%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Duration<label class="labelRequired">*</label>
                        </td>
                        <td style="width: 40%;">
                            <%=Html.TextBoxFor(m => m.LeaveApplication.fltDuration, new { @class = " textRegularDuration double", @maxlength = "5", @onkeyup = "return ismaxlengthPop(this)", @onchange = "return HookupCalculation(this);" })%>
                            Days
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
                    <tr>
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
                    <tr>
                        <td style="width: 15%;">
                            Apply Date<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strApplyDate, new { @class = "textRegularDate required dtPicker date", @style = "width:75px; min-width:75px;", @maxlength = "10" })%>
                        </td>
                        <td style="width: 15%;">
                            Approved Date<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strOffLineAppvDate, new { @class = "textRegularDate required dtPicker date",@style = "width:75px; min-width:75px;", @maxlength = "10" })%>
                        </td>
                    </tr>
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
                <%= Html.DropDownListFor(m => m.LeaveApplication.strSupervisorID, Model.Approver, "...Select One...", new { @class = "selectBoxRegular", @style = "width:243px; min-width:243px;", @onchange = "return GetAuthorityInfo();" })%>
                <%}
                  else
                  { %>
                <%= Html.DropDownListFor(m => m.LeaveApplication.strSupervisorID, Model.Approver, "...Select One...", new { @class = "selectBoxRegular required", @style = "width:242px; min-width:242px;", @onchange = "return GetAuthorityInfo();" })%>
                <%} %>--%>

                            <%=Html.TextBoxFor(m => m.LeaveApplication.strSupervisorInitial, new { @class = "textRegularDate required", @readonly = "readonly" })%>
                            <a href="#" class="btnSearch" onclick="return openEmployee('3');"></a>

                        </td>
                        <td style="width: 15%;">
                        Name
                        </td>
                        <td>
                            <%=Html.TextBox("strSupervisorName","", new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 15%; padding: 0px;">
                            Designation
                        </td>
                        <td>
                            <%=Html.TextBox("strAuthorDesignation", Model.strAuthorDesignation, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                        </td>

                        <td style="width: 15%; padding: 0px;">
                            Department
                        </td>
                        <td>
                            <%=Html.TextBox("strAuthorDepartment", Model.strAuthorDepartment, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                        </td>
                    </tr>
                    <%
          } %>
                </table>
                <br />
                <table class="contenttext" cellspacing="0" style="width: 100%; padding: 0px">
                    <tr>
                        <td style="width: 15%;">
                            <%=Html.HiddenFor(m => m.LeaveApplication.strResponsibleId)%>
                            Responsible Person (ID)
                        </td>
                        <td style="width: 40%;">
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strResponsibleInitial, new { @class = "textRegularDate", @readonly = "readonly" })%>
                            <a href="#" class="btnSearch" onclick="return openEmployee('2');"></a>
                        </td>
                        <td style="width: 15%;">
                            <%=Html.HiddenFor(m => m.LeaveApplication.strOfflineApprovedById)%>
                            Submit by (ID)
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.LeaveApplication.strApprovedByInitial, new { @class = "textRegularDate required", @readonly = "readonly" })%>
                            <a href="#" class="btnSearch" onclick="return openEmployee('1');"></a>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; padding: 0px;">
                            Name
                        </td>
                        <td>
                            <%=Html.TextBox("strResponsibleName","", new { @class = "textRegular", @style = "width:313px; min-width:313px;", @readonly = "readonly" })%>
                        </td>
                        <td style="width: 42%; padding: 0px;">
                            Name
                        </td>
                        <td>
                            <%=Html.TextBox("strOfflineApproverName", "",new { @class = "textRegular", @style = "width:313px; min-width:313px;", @readonly = "readonly" })%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; padding: 0px;">
                            Designation
                        </td>
                        <td>
                            <%=Html.TextBox("strResDesignation", Model.strResDesignation, new { @class = "textRegular",@style = "width:313px; min-width:313px;", @readonly = "readonly" })%>
                        </td>
                        <td style="width: 15%; padding: 0px;">
                            Designation
                        </td>
                        <td>
                            <%=Html.TextBox("strOffAuthorDesignation", Model.strOffAuthorDesignation, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; padding: 0px;">
                            Department
                        </td>
                        <td>
                            <%=Html.TextBox("strResDepartment", Model.strResDepartment, new { @class = "textRegular",@style = "width:313px; min-width:313px;", @readonly = "readonly" })%>
                        </td>
                        <td style="width: 42%; padding: 0px;">
                            Department
                        </td>
                        <td>
                            <%=Html.TextBox("strOffAuthorDepartment", Model.strOffAuthorDepartment, new { @class = "textRegular", @style = "width:240px; min-width:240px;", @readonly = "readonly" })%>
                        </td>
                    </tr>
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
                        <td>
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
                </table>
                <div class="divSpacer">
                </div>
                <div class="divRow">
                    <div id="divLedger">
                        <% Html.RenderPartial("LeaveLedger"); %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divButton">
        <%if (Model.LeaveApplication.intApplicationID <= 0)
          { %>
        <a id="abtnSave" href="#" class="btnSave" onclick="return OfflineApprove();"></a>
        <%}
          else
          { %>
        <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfflineLeaveApplication, LMS.Web.Permission.MenuOperation.Edit))
          {%>
        <%--<a href="#" class="btnUpdate" onclick="return OfflineUpdate();"></a>--%>
        <%} %>
        <%
          } %>
        <input id="btnSave" name="btnSave" style="visibility: hidden" type="submit" value="Save"
            visible="false" />
        <%if (Model.LeaveApplication.intApplicationID > 0)
          { %>
        <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfflineLeaveApplication, LMS.Web.Permission.MenuOperation.Delete))
          {%>
        <%--<a href="#" class="btnDelete" onclick="return OfflineDelete();"></a>--%>
        <%} %>
        <%} %>
        <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
    </div>
    <div id="divMsgStd" class="divMsg">
        <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
    </div>
</div>
</form>
