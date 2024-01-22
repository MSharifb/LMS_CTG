<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ShiftAssignmentModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        preventSubmitOnEnter($("#frmShiftAssignment"));

        setTitle("Shift Assignment");
        $("#dvEmpID").hide();
        $("#btnSave").hide();

        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });
        FormatTextBox();


    });

    function openEmployee() {


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

    function disableEntryType() {
        if ($("#IsNew").val() == 'False') {
            $(".rbIndividual").attr("disabled", "disabled");
            $(".rbAll").attr("disabled", "disabled");

        }
    }

    function OptionWisePageRefresh() {

        var IsIndividual = $('#Model_IsIndividual').attr('checked');

        $('#IsIndividual').val(IsIndividual);

        if (IsIndividual == true) {

            $('#ShiftAssignment_strDepartmentID').val("");
            $('#ShiftAssignment_strDepartmentID').attr('disabled', 'disabled');
            $('#ShiftAssignment_strDesignationID').val("");
            $('#ShiftAssignment_strDesignationID').attr('disabled', 'disabled');
            $('#ShiftAssignment_strLocationID').val("");
            $('#ShiftAssignment_strLocationID').attr('disabled', 'disabled');
            $('#ShiftAssignment_strCompanyID').val("");
            $('#ShiftAssignment_strCompanyID').attr('disabled', 'disabled');
            

            $('#ShiftAssignment_intCategoryCode').val("");
            $('#ShiftAssignment_intCategoryCode').attr('disabled', 'disabled');

            $('#ShiftAssignment_strEmpID').addClass("required");
            $('#ShiftAssignment_strEmpName').addClass("required");

            $('#ShiftAssignment_strEmpID').removeAttr('disabled');
            $('#ShiftAssignment_strEmpName').removeAttr('disabled');

            $('#btnElipse').css('visibility', 'visible');
            $('#lblIDReqMark').css('visibility', 'visible');
            $('#lblNameReqMark').css('visibility', 'visible');

        }
        else {

            $('#ShiftAssignment_strEmpID').val("");
            $('#ShiftAssignment_strEmpID').attr('disabled', 'disabled');
            $('#ShiftAssignment_strEmpName').val("");
            $('#ShiftAssignment_strEmpName').attr('disabled', 'disabled');
            $('#ShiftAssignment_strDesignation').val("");
            $('#ShiftAssignment_strDepartment').val("");
            $('#ShiftAssignment_strCompany').val("");
            $('#ShiftAssignment_strLocation').val("");
            $('#ShiftAssignment_strCategory').val("");

            $('#StrEmpId').removeClass("required");
            $('#ShiftAssignment_strEmpName').removeClass("required");

            $('#ShiftAssignment_strCompanyID').removeAttr('disabled');
            $('#ShiftAssignment_strDepartmentID').removeAttr('disabled');
            $('#ShiftAssignment_strDesignationID').removeAttr('disabled');
            $('#ShiftAssignment_strLocationID').removeAttr('disabled');
            $('#ShiftAssignment_intCategoryCode').removeAttr('disabled');
          
            $('#btnElipse').css('visibility', 'hidden');
            $('#lblIDReqMark').css('visibility', 'hidden');
            $('#lblNameReqMark').css('visibility', 'hidden');
        }

    }


    function setData(id, name) {
        document.getElementById('ShiftAssignment_strEmpName').value = name;
        document.getElementById('ShiftAssignment_strEmpID').value = id;
        GetInfo(id);
        $("#divEmpList").dialog('close');
    }

    function searchEmployee() {
        window.parent.openEmployee();
    }

    updateFields = function (data) {
        $('#ShiftAssignment_strDesignation').val(data.strDesignation);
        $('#ShiftAssignment_strDepartment').val(data.strDepartment);
        $('#ShiftAssignment_strJoiningDate').val(data.strJoiningDate);
        $('#ShiftAssignment_strLocation').val(data.strLocation);
        $('#ShiftAssignment_strCompany').val(data.strCompany);


    };

    function GetInfo(id) {
        var form = $("#frmShiftAssignment");
        var serializedForm = form.serialize();

        $.getJSON("getEmployeeInformation", serializedForm, updateFields);

    }

    function save() {
        if (fnValidateDateTime() == false) {
            alert("Invalid Date.");
            return false;
        }
        if (fnValidate() == true) {
            $('#btnSave').trigger('click');

        }
        return false;
    }



    function Delete() {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = '#divShiftAssignmentDetails';
            var url = '/LMS/ShiftAssignment/Delete';
            var form = $('#frmShiftAssignment');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function Closing() {

    }



   
    
</script>
<form id="frmShiftAssignment" method="post" action="">
<div id="divShiftAssignment">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.ShiftAssignment.intShiftAssignmentID)%>
            <%= Html.HiddenFor(m => m.IsNew)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 55%" />
            <col />
        </colgroup>
        <tr>
            <%=Html.HiddenFor(m => m.IsIndividual)%>
            <td style="width: 43%">
                <%=Html.RadioButton("Model_IsIndividual", true, Model.IsIndividual, new { onClick = "OptionWisePageRefresh();", @class="rbIndividual" })%>Individual
            </td>
            <td style="width: 57%">
                <%=Html.RadioButton("Model_IsIndividual", true, !Model.IsIndividual, new { onClick = "OptionWisePageRefresh();", @class = "rbAll" })%>All
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
                                        ID<label id="lblIDReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.ShiftAssignment.strEmpID, new { @class = "textRegularDate", @readonly = "readonly" })%>
                                        <% if (Model.IsNew) %>
                                        <%{ %>
                                        <a id="btnElipse" href="#" style="visibility: hidden" class="btnSearch" onclick="return openEmployee();">
                                        </a>
                                        <%}%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Name<label id="lblNameReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.ShiftAssignment.strEmpName, new { @class = "textRegular", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Designation
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ShiftAssignment.strDesignation, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Department
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ShiftAssignment.strDepartment, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px; " })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Company
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ShiftAssignment.strCompany, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Location
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ShiftAssignment.strLocation, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px" })%>
                                    </td>
                                </tr>
                               
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Company Name<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ShiftAssignment.strCompanyID, Model.Company, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Department<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ShiftAssignment.strDepartmentID, Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Designation<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ShiftAssignment.strDesignationID, Model.Designation, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Location<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ShiftAssignment.strLocationID, Model.Location, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Category<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ShiftAssignment.intCategoryCode, Model.Category, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                       
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 25%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Shift<label  class="labelRequired"> *</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.ShiftAssignment.intShiftID, Model.Shift, "...Select...", new { @class = "selectBoxRegular required" })%>
            </td>
        </tr>
        <tr>
            <td>
                Effective Date
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.ShiftAssignment.strEffectiveDate, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
        </tr>
    </table>
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.ShiftAssignment.intShiftAssignmentID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ShiftAssignment, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ShiftAssignment, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.ShiftAssignment.intShiftAssignmentID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>

<script type="text/javascript">
    OptionWisePageRefresh();
    disableEntryType();
</script>

</form>
