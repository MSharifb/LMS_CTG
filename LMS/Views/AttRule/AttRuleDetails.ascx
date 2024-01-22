<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.AttRuleModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

//        $("#datepicker").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#ATT_tblRule_strEffectiveDate').datepicker('show'); });

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 430, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        
        preventSubmitOnEnter($("#frmAttRule"));

        setTitle("Attendence Rule");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        FormatTextBox();


    });

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

            $('#ATT_tblRule_strDepartmentID').val("");
            $('#ATT_tblRule_strDepartmentID').attr('disabled', 'disabled');
            $('#ATT_tblRule_strDesignationID').val("");
            $('#ATT_tblRule_strDesignationID').attr('disabled', 'disabled');
            $('#ATT_tblRule_strLocationID').val("");
            $('#ATT_tblRule_strLocationID').attr('disabled', 'disabled');
            $('#ATT_tblRule_strCompanyID').val("");
            $('#ATT_tblRule_strCompanyID').attr('disabled', 'disabled');
            $('#ATT_tblRule_intShiftID').val("");
            $('#ATT_tblRule_intShiftID').attr('disabled', 'disabled');

            $('#ATT_tblRule_intCategoryCode').val("");
            $('#ATT_tblRule_intCategoryCode').attr('disabled', 'disabled');

            $('#ATT_tblRule_strEmpID').addClass("required");
            $('#ATT_tblRule_strEmpName').addClass("required");

            $('#ATT_tblRule_strEmpID').removeAttr('disabled');
            $('#ATT_tblRule_strEmpName').removeAttr('disabled');

            $('#btnElipse').css('visibility', 'visible');
            $('#lblIDReqMark').css('visibility', 'visible');
            $('#lblNameReqMark').css('visibility', 'visible');

        }
        else {

            $('#ATT_tblRule_strEmpID').val("");
            $('#ATT_tblRule_strEmpID').attr('disabled', 'disabled');
            $('#ATT_tblRule_strEmpName').val("");
            $('#ATT_tblRule_strEmpName').attr('disabled', 'disabled');
            $('#ATT_tblRule_strDesignation').val("");
            $('#ATT_tblRule_strDepartment').val("");
            $('#ATT_tblRule_strCompany').val("");
            $('#ATT_tblRule_strLocation').val("");
            $('#ATT_tblRule_strCategory').val("");

            $('#StrEmpId').removeClass("required");
            $('#ATT_tblRule_strEmpName').removeClass("required");

            $('#ATT_tblRule_strCompanyID').removeAttr('disabled');
            $('#ATT_tblRule_strDepartmentID').removeAttr('disabled');
            $('#ATT_tblRule_strDesignationID').removeAttr('disabled');
            $('#ATT_tblRule_strLocationID').removeAttr('disabled');
            $('#ATT_tblRule_intCategoryCode').removeAttr('disabled');
            $('#ATT_tblRule_intShiftID').removeAttr('disabled');

            $('#btnElipse').css('visibility', 'hidden');
            $('#lblIDReqMark').css('visibility', 'hidden');
            $('#lblNameReqMark').css('visibility', 'hidden');
        }

    }

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


    function setData(id, name) {
        document.getElementById('ATT_tblRule_strEmpName').value = name;
        document.getElementById('ATT_tblRule_strEmpID').value = id;        
        GetInfo(id);
        $("#divEmpList").dialog('close');
    }

    updateFields = function (data) {
        //alert(data.strDesignation);
        $('#ATT_tblRule_strDesignation').val(data.strDesignation);
        $('#ATT_tblRule_strDepartment').val(data.strDepartment);
        $('#ATT_tblRule_strLocation').val(data.strLocation);
        $('#ATT_tblRule_strCompany').val(data.strCompany);
    };

    function GetInfo(id) {        
        var form = $("#frmAttRule");
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
            var targetDiv = '#divAttRuleDetails';
            var url = '/LMS/AttRule/Delete';
            var form = $('#frmAttRule');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function IsWorkingHourRule(rb) {
        $('#ATT_tblRule_intWorkingHourRule').val(rb.value);

    }
    function IsOverTimeRule(rb) {
        $('#ATT_tblRule_intOverTimeRule').val(rb.value);

    }
    function IsEarlyArrival(rb) {
        $('#ATT_tblRule_intEarlyArrival').val(rb.value);

    }
    function IsLateArrival(rb) {
        $('#ATT_tblRule_intLateArrival').val(rb.value);

    }
    function IsEarlyDeparture(rb) {
        $('#ATT_tblRule_intEarlyDeparture').val(rb.value);

    }
    function IsLateDeparture(rb) {
        $('#ATT_tblRule_intLateDeparture').val(rb.value);

    }

    function Closing() {

    }

   
    
</script>
<form id="frmAttRule" method="post" action="">
<div id="divAttRule">
    <%--<div class="divSpacer">
    </div>--%>
    <div class="divRow">
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.ATT_tblRule.intRuleID)%>
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
                                        <%=Html.TextBoxFor(m => m.ATT_tblRule.strEmpID, new { @class = "textRegularDate", @readonly = "readonly" })%>
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
                                        <%=Html.TextBoxFor(m => m.ATT_tblRule.strEmpName, new { @class = "textRegular", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Designation
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ATT_tblRule.strDesignation, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Department
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ATT_tblRule.strDepartment, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px; " })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Company
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ATT_tblRule.strCompany, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Location
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ATT_tblRule.strLocation, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px" })%>
                                    </td>
                                </tr>
                              <%--  <tr>
                                    <td>
                                        Category
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.ATT_tblRule.strCategory, new { @class = "textRegCustomWidth textLabelLike", @readonly = "readonly", @style = "Width:200px" })%>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Company Name<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ATT_tblRule.strCompanyID, Model.Company, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Department<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ATT_tblRule.strDepartmentID, Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Designation<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ATT_tblRule.strDesignationID, Model.Designation, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Location<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ATT_tblRule.strLocationID, Model.Location, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Category<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ATT_tblRule.intCategoryCode, Model.Category, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td>
                                        Shift<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.ATT_tblRule.intShiftID, Model.Shift, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 20%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Effective Date
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.ATT_tblRule.strEffectiveDate, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                <%--<img alt="" id="datepicker" style="height: 16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>" />--%>
            </td>
        </tr>
        <tr>
            <td>
                Working Hour
                <label class="labelRequired">
                    *</label>
            </td>
            <%=Html.HiddenFor(m => m.ATT_tblRule.intWorkingHourRule)%>
            <td>
                <%=Html.RadioButton("Model_ATT_tblRule_intWorkingHourRule", 1, (Model.ATT_tblRule.intWorkingHourRule == 1), new { onClick = "IsWorkingHourRule(this);" })%>Rule-1
                ( Last OUT Time - First IN Time)
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intWorkingHourRule", 2, (Model.ATT_tblRule.intWorkingHourRule == 2), new { onClick = "IsWorkingHourRule(this);" })%>Rule-2
                ( Last OUT Time - (First IN Time + Break Time) )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intWorkingHourRule", 0, (Model.ATT_tblRule.intWorkingHourRule == 0), new { onClick = "IsWorkingHourRule(this);" })%>None
                <br />
                <%-- <%=Html.CheckBox("Model_ATT_tblRule_btConsiderEarlyArrive", Model.ATT_tblRule.btConsiderEarlyArrive)%>Consider
                Early Arrival as Working Time--%>
                <%=Html.CheckBoxFor(m => m.ATT_tblRule.btConsiderEarlyArrive)%>Consider Early Arrival
                as Working Time
            </td>
        </tr>
        <tr>
            <td>
                Overtime Rule
                <label class="labelRequired">
                    *</label>
            </td>
            <%=Html.HiddenFor(m => m.ATT_tblRule.intOverTimeRule)%>
            <td>
                <%=Html.RadioButton("Model_ATT_tblRule_intOverTimeRule", 1, (Model.ATT_tblRule.intOverTimeRule == 1), new { onClick = "IsOverTimeRule(this);" })%>Rule-1
                ( Employee Last OUT Time - Shift OUT Time )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intOverTimeRule", 2, (Model.ATT_tblRule.intOverTimeRule == 2), new { onClick = "IsOverTimeRule(this);" })%>Rule-2
                ( Employee Working Time - Shift Working Time )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intOverTimeRule", 0, (Model.ATT_tblRule.intOverTimeRule == 0), new { onClick = "IsOverTimeRule(this);" })%>None
            </td>
        </tr>
        <tr>
            <td>
                Early Arrival
                <label class="labelRequired">
                    *</label>
            </td>
            <%=Html.HiddenFor(m => m.ATT_tblRule.intEarlyArrival)%>
            <td>
                <%=Html.RadioButton("Model_ATT_tblRule_intEarlyArrival", 1, Model.ATT_tblRule.intEarlyArrival == 1, new { onClick = "IsEarlyArrival(this);" })%>Rule-1
                ( Employee First IN - Shift IN Time )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intEarlyArrival", 2, Model.ATT_tblRule.intEarlyArrival == 2, new { onClick = "IsEarlyArrival(this);" })%>Rule-2
                ( Employee First IN - Shift IN Time ) + Grace Time
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intEarlyArrival", 0, Model.ATT_tblRule.intEarlyArrival == 0, new { onClick = "IsEarlyArrival(this);" })%>None
            </td>
        </tr>
        <tr>
            <td>
                Late Arrival
                <label class="labelRequired">
                    *</label>
            </td>
            <%=Html.HiddenFor(m => m.ATT_tblRule.intLateArrival)%>
            <td>
                <%=Html.RadioButton("Model_ATT_tblRule_intLateArrival", 1, Model.ATT_tblRule.intLateArrival == 1, new { onClick = "IsLateArrival(this);" })%>Rule-1
                ( Shift IN Time - Employee First IN )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intLateArrival", 2, Model.ATT_tblRule.intLateArrival == 2, new { onClick = "IsLateArrival(this);" })%>Rule-2
                ( Shift IN Time - Employee First IN ) - Grace Time
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intLateArrival", 0, Model.ATT_tblRule.intLateArrival == 0, new { onClick = "IsLateArrival(this);" })%>None
            </td>
        </tr>
        <tr>
            <td>
                Early Departure
                <label class="labelRequired">
                    *</label>
            </td>
            <%=Html.HiddenFor(m => m.ATT_tblRule.intEarlyDeparture)%>
            <td>
                <%=Html.RadioButton("Model_ATT_tblRule_intEarlyDeparture", 1, Model.ATT_tblRule.intEarlyDeparture == 1, new { onClick = "IsEarlyDeparture(this);" })%>Rule-1
                ( Shift OUT Time - Employee Last OUT )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intEarlyDeparture", 2, Model.ATT_tblRule.intEarlyDeparture == 2, new { onClick = "IsEarlyDeparture(this);" })%>Rule-2
                ( Shift Working Time - Employee Working Time )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intEarlyDeparture", 0, Model.ATT_tblRule.intEarlyDeparture == 0, new { onClick = "IsEarlyDeparture(this);" })%>None
            </td>
        </tr>
        <tr>
            <td>
                Late Departure
                <label class="labelRequired">
                    *</label>
            </td>
            <%=Html.HiddenFor(m => m.ATT_tblRule.intLateDeparture)%>
            <td>
                <%=Html.RadioButton("Model_ATT_tblRule_intLateDeparture", 1, Model.ATT_tblRule.intLateDeparture == 1, new { onClick = "IsLateDeparture(this);" })%>Rule-1
                ( Employee Last OUT Time - Shift OUT Time )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intLateDeparture", 2, Model.ATT_tblRule.intLateDeparture == 2, new { onClick = "IsLateDeparture(this);" })%>Rule-2
                ( Employee Working Time - Shift Working Time )
                <br />
                <%=Html.RadioButton("Model_ATT_tblRule_intLateDeparture", 0, Model.ATT_tblRule.intLateDeparture == 0, new { onClick = "IsLateDeparture(this);" })%>None
            </td>
        </tr>
    </table>
</div>
<div class="divSpacer">
</div>
<%--<div class="divSpacer">
</div>
<div class="divSpacer">
</div>--%>
<div class="divButton">
    <%if (Model.ATT_tblRule.intRuleID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AttRule, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AttRule, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.ATT_tblRule.intRuleID > 0)
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
