<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveRuleAssignmentModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveRuleAssignment"));

        setTitle("Leave Type");
        $("#btnSave").hide();
        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 425, width: 780, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });


        OptionWisePageRefresh(0);

        FormatTextBox();
        ChangeLeaveEntitlementTitle();

    });


    function OptionWisePageRefresh(flag) {

        var IsIndividual = $('#Model_LeaveRuleAssignment_IsIndividual').attr('checked');

        //alert(IsIndividual);

        $('#LeaveRuleAssignment_IsIndividual').val(IsIndividual);

        //alert(flag);

        if (flag > 0) {
            document.getElementById('LeaveRuleAssignment_strEmpID').value = "";
            document.getElementById('LeaveRuleAssignment_strEmpInitial').value = "";
            $("#lblIdNotFound").css('visibility', 'hidden');
        }
        else {
            //document.getElementById('hdnEmpId').value = document.getElementById('LeaveRuleAssignment_strEmpInitial').value;
        }

        if (IsIndividual == true) {

            $('#LeaveRuleAssignment_strDepartmentID').val("");
            $('#LeaveRuleAssignment_strDepartmentID').attr('disabled', 'disabled');
            $('#LeaveRuleAssignment_strDesignationID').val("");
            $('#LeaveRuleAssignment_strDesignationID').attr('disabled', 'disabled');
            //$('#LeaveRuleAssignment_strLocationID').val("");
            //$('#LeaveRuleAssignment_strLocationID').attr('disabled', 'disabled');
            $('#LeaveRuleAssignment_strGender').val("");
            $('#LeaveRuleAssignment_strGender').attr('disabled', 'disabled');
            $('#LeaveRuleAssignment_intCategoryCode').val("");
            $('#LeaveRuleAssignment_intCategoryCode').attr('disabled', 'disabled');


            $('#LeaveRuleAssignment_strEmpInitial').addClass("required");
            $('#LeaveRuleAssignment_strEmpName').addClass("required");

            $('#LeaveRuleAssignment_strEmpInitial').removeAttr('disabled');
            $('#LeaveRuleAssignment_strEmpName').removeAttr('disabled');

            $("#btnElipse").css('visibility', 'visible');
            $("#lblIDReqMark").css('visibility', 'visible');
            $("#lblNameReqMark").css('visibility', 'visible');

        }
        else {

            $('#LeaveRuleAssignment_strEmpInitial').val("");
            $('#LeaveRuleAssignment_strEmpInitial').attr('disabled', 'disabled');
            $('#LeaveRuleAssignment_strEmpName').val("");
            $('#LeaveRuleAssignment_strEmpName').attr('disabled', 'disabled');

            $('#LeaveRuleAssignment_strEmpInitial').removeClass("required");
            $('#LeaveRuleAssignment_strEmpName').removeClass("required");

            $('#LeaveRuleAssignment_strDepartmentID').removeAttr('disabled');
            $('#LeaveRuleAssignment_strDesignationID').removeAttr('disabled');

            $('#LeaveRuleAssignment_strGender').removeAttr('disabled');
            $('#LeaveRuleAssignment_intCategoryCode').removeAttr('disabled');

            $("#btnElipse").css('visibility', 'hidden');
            $("#lblIDReqMark").css('visibility', 'hidden');
            $("#lblNameReqMark").css('visibility', 'hidden');


        }

    }

    function removeRequired() {

        $('#LeaveRuleAssignment_strEmpInitial').val("");
        $('#LeaveRuleAssignment_strEmpName').val("");
        executeAction('frmLeaveRuleAssignment', '/LMS/LeaveRuleAssignmentOptionWisePageRefresh', 'divLeaveRuleAssignmentDetails');


        $('#LeaveRuleAssignment_strEmpInitial').removeClass("required");
        $('#LeaveRuleAssignment_strEmpName').removeClass("required");
        return false;
    }

    function addRequired() {


        $('#LeaveRuleAssignment_strDepartmentID').val("");
        $('#LeaveRuleAssignment_strDesignationID').val("");
        //$('#LeaveRuleAssignment_strLocationID').val("");
        $('#LeaveRuleAssignment_strGender').val("");

        executeAction('frmLeaveRuleAssignment', '/LMS/LeaveRuleAssignmentOptionWisePageRefresh', 'divLeaveRuleAssignmentDetails');

        $('#LeaveRuleAssignment_strEmpInitial').addClass("required");
        $('#LeaveRuleAssignment_strEmpName').addClass("required");

        return false;

    }

    function setData(id, strEmpInitial, name) {
        document.getElementById('LeaveRuleAssignment_strEmpID').value = id;
        document.getElementById('LeaveRuleAssignment_strEmpInitial').value = strEmpInitial;
        document.getElementById('LeaveRuleAssignment_strEmpName').value = name;


        $("#lblIdNotFound").css('visibility', 'hidden');
        $('#LeaveRuleAssignment_strEmpInitial').removeClass("invalid");

        $("#divEmpList").dialog('close');
    }

    function Closing() {

    }

    function searchEmployee() {

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

    function save() {

        //        if (document.getElementById('hdnEmpId').value != document.getElementById('LeaveRuleAssignment_strEmpInitial').value) {
        //            $('#LeaveRuleAssignment_strEmpInitial').addClass("invalid");
        //        }
        //        else {
        //            $('#LeaveRuleAssignment_strEmpInitial').removeClass("invalid");
        //        }

        if (fnValidate() == true) {
            $('#btnSave').trigger('click');
        }
        return false;
    }

    function GetEntitlement() {

        var targetDiv = "#divLeaveRuleAssignmentDetails";
        var url = "/LMS/LeaveRuleAssignment/GetEntitlement";
        var form = $("#frmLeaveRuleAssignment");
        var serializedForm = form.serialize();

        var pintRuleID = $('#LeaveRuleAssignment_intRuleID').val();

        if (pintRuleID > 0) {
            $.post(url, serializedForm, function (result) {
                $("#LeaveRuleAssignment_fltEntitlement").val(result);
            }, "json");
        }
        else {
            $("#LeaveRuleAssignment_fltEntitlement").val("0");
        }

        return false;
    }


    function GetDataddl() {

        $('#LeaveRuleAssignment_intRuleID')
        .empty()
        .append('<option value="0">...Select One...</option>')
        .find('option:first')
        .attr("selected", "selected")
        ;

        var targetDiv = "#divLeaveRuleAssignmentDetails";
        var url = "/LMS/LeaveRuleAssignment/GetDropDown";
        var form = $("#frmLeaveRuleAssignment");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        $('#LeaveRuleAssignment_intRuleID')
        .find('option:first')
        .attr("selected", "selected")
        ;


        // Leave type e.g Yearly and Service Life

        ChangeLeaveEntitlementTitle();


      //  $('#LeaveRuleAssignment_intEmployeeTypeID').removeClass("input-validation-error");
        return false;

    }

    function ChangeLeaveEntitlementTitle() {

        var intLeaveTypeID = $('#LeaveRuleAssignment_intLeaveTypeID').val();
        if (intLeaveTypeID != '') {
            var url = "/LMS/LeaveRuleAssignment/GetLeaveTypeStatus";
            $.getJSON(url, { Id: intLeaveTypeID }, function (result) {

                if (result.status == 'Y') {
                    $('#lblEntitlement').text('(Yearly)');
                }
                else {
                    $('#lblEntitlement').text('(Service Life)');
                }
            });
        }
        else {
            $('#LeaveRuleAssignment_fltEntitlement').val(0);
        }

        $('#LeaveRuleAssignment_intEmployeeTypeID').removeClass("input-validation-error");
        $('#LeaveRuleAssignment_intCategoryCode').removeClass("input-validation-error");  
        return false;
    }


    function Delete() {
        Id = $('#LeaveRuleAssignment_intRuleAssignID').val();

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intRuleAssignID: Id }, '/LMS/LeaveRuleAssignment/Delete', 'divLeaveRuleAssignmentDetails');
        }
        return false;
    }

    function handleEnter(evt) {

        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;
        }

        else if (evt) keyCode = evt.which;
        else return true;

        $('#LeaveRuleAssignment_strEmpInitial').removeClass("invalid");

        if (keyCode == 13) {
            //handle enter
            var id = document.getElementById('LeaveRuleAssignment_strEmpInitial').value;
            var name = "";

            // getdata(id, name);
            var url = "/LMS/Employee/LookUpById/" + id;

            $.post(url, "", function (result) {

                if (result[0] != null && result[0] != "") {
                    document.getElementById('LeaveRuleAssignment_strEmpID').value = result[0];
                    document.getElementById('LeaveRuleAssignment_strEmpName').value = result[1];
                    document.getElementById('LeaveRuleAssignment_strEmpInitial').value = result[2];

                    $("#lblIdNotFound").css('visibility', 'hidden');
                }
                else {
                    document.getElementById('LeaveRuleAssignment_strEmpName').value = "";
                    document.getElementById('LeaveRuleAssignment_strEmpID').value = "";
                    document.getElementById('LeaveRuleAssignment_strEmpInitial').value = "";

                    $("#lblIdNotFound").css('visibility', 'visible');
                }
            }, "json");
        }

        return true;
    } 
</script>
<form id="frmLeaveRuleAssignment" method="post" action="">
<div id="divLeaveRuleAssignment">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.LeaveRuleAssignment.intRuleAssignID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                Leave Type<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveRuleAssignment.intLeaveTypeID, Model.LeaveType, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return GetDataddl();" })%>
            </td>
        </tr>
        <tr>
            <td>
                Rule Name
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveRuleAssignment.intRuleID, Model.LeaveRule, "...Select One...", new { @class = "selectBoxRegular required",onchange = "return GetEntitlement();"  })%>
            </td>
        </tr>
        <tr>
            <td>
                Entitlement
                <label id="lblEntitlement">
                </label>
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveRuleAssignment.fltEntitlement, new { @class = "selectBoxRegular required",@readonly="readonly" })%>
            </td>
        </tr>
        <tr>
            <%=Html.HiddenFor(m => m.LeaveRuleAssignment.IsIndividual)%>
            <td>
                <%=Html.RadioButton("Model_LeaveRuleAssignment_IsIndividual", true, Model.LeaveRuleAssignment.IsIndividual, new { onClick = "OptionWisePageRefresh(1);" })%>Individual
            </td>
            <td>
                <%=Html.RadioButton("Model_LeaveRuleAssignment_IsIndividual", true, !Model.LeaveRuleAssignment.IsIndividual, new { onClick = "OptionWisePageRefresh(1);" })%>All
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Applicant ID<label id="lblIDReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.HiddenFor(m=>m.LeaveRuleAssignment.strEmpID)%>
                                        <%=Html.TextBoxFor(m => m.LeaveRuleAssignment.strEmpInitial, new { @class = "textRegularDate", onkeypress = "return handleEnter(event);" })%>
                                        <a href="#" id="btnElipse" style="visibility: hidden" class="btnSearch" onclick="return searchEmployee();">
                                        </a>
                                        <label id="lblIdNotFound" style="visibility: hidden; vertical-align: 5px; padding-left: 10px;
                                            color: red;">
                                            Id not found !</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Name<label id="lblNameReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveRuleAssignment.strEmpName, new { @class = "textRegular", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Department<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.LeaveRuleAssignment.strDepartmentID, Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Employee Type<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.LeaveRuleAssignment.intEmployeeTypeID, Model.EmployeeType, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Grade From<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.LeaveRuleAssignment.GradeFrom, Model.JobGrade, "...All...", new { @class = "smallSelectBoxRegular" })%>
                                        -
                                         <%= Html.DropDownListFor(m => m.LeaveRuleAssignment.GradeTo, Model.JobGrade, "...All...", new { @class = "smallSelectBoxRegular" })%>
                                    </td>
<%--                                    <td>
                                        To<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                       
                                    </td>--%>
                                </tr>

                                <tr>
                                    <td>
                                        Category<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m=>m.LeaveRuleAssignment.intCategoryCode,Model.Category,"...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Designation<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m=>m.LeaveRuleAssignment.strDesignationID,Model.Designation,"...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gender<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m=>m.LeaveRuleAssignment.strGender,Model.Gender,"...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.LeaveRuleAssignment.intRuleAssignID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AssignLeaveRule, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return save();"></a>
    <%} %>
    <%}
      else
      {%>
    <a href="#" class="btnSave" onclick="return save();"></a>
    <%} %>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AssignLeaveRule, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.LeaveRuleAssignment.intRuleAssignID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgLRA" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
