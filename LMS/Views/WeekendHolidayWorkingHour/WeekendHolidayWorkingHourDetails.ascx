<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.WeekendHolidayWorkingHourModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        preventSubmitOnEnter($("#frmWeekendHolidayWorkingHour"));

        setTitle("Working Hour for Weekend/Holiday");

        ChangeEntryType(document.getElementById('WeekendHolidayWorkingHour_EntryType'));
        $("#btnSave").hide();

        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#WeekendHolidayWorkingHour_strInTime").timepicker({ ampm: true });
        $("#WeekendHolidayWorkingHour_strOutTime").timepicker({ ampm: true });
        $("#WeekendHolidayWorkingHour_strHalfTime").timepicker({ ampm: true });
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


    function setData(id, name) {
        document.getElementById('WeekendHolidayWorkingHour_strEmpName').value = name;
        document.getElementById('WeekendHolidayWorkingHour_strEmpID').value = id;
        GetInfo(id);
        $("#divEmpList").dialog('close');
    }

    function searchEmployee() {
        window.parent.openEmployee();
    }

    updateFields = function (data) {
        
        $('#WeekendHolidayWorkingHour_strDesignation').val(data.strDesignation);
        $('#WeekendHolidayWorkingHour_strDepartment').val(data.strDepartment);
        $('#WeekendHolidayWorkingHour_strLocation').val(data.strLocation);
        $('#WeekendHolidayWorkingHour_strCompany').val(data.strCompany);
    };

    function GetInfo(id) {
        var form = $("#frmWeekendHolidayWorkingHour");
        var serializedForm = form.serialize();

        $.getJSON("getEmployeeInformation", serializedForm, updateFields);

    }

    function save() {
        var dtFrom = $('#WeekendHolidayWorkingHour_strPeriodFrom').val();
        var dtTo = $('#WeekendHolidayWorkingHour_strPeriodTo').val();
        
        if (checkDateValidation(dtFrom,dtTo)) {
            if (fnValidate() == true) {
                $('#btnSave').trigger('click');

            }
        }
        return false;
    }

    function checkDateValidation(dtFrom, dtTo, mandatory) {
           
        if (fnValidateDateTime() == false) {
            alert("Invalid Date.");
            return false;
        }
        if (dtFrom != '' && dtTo != '') {
            if (dtFrom > dtTo) {
                alert("From Date must be smaller than or equal to 'To Date'.");
                return false;
            }
        }

        return true;
    }

    function disableEntryType() {
        if ($("#IsNew").val() == 'False') {
            $(".rbIndividual").attr("disabled", "disabled");
            $(".rbAll").attr("disabled", "disabled");

        }
    }

    function Delete() {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = '#divWeekendHolidayWorkingHourDetails';
            var url = '/LMS/WeekendHolidayWorkingHour/Delete';
            var form = $('#frmWeekendHolidayWorkingHour');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function Closing() {

    }

    function ChangeEntryType(rb) {
        $('#WeekendHolidayWorkingHour_EntryType').val(rb.value);

        if (rb.value == 1) {
            $("#tblAll").css('display', 'block');
            $("#tblIndividual").css('display', 'none');
        }
        else {
            $("#tblAll").css('display', 'none');
            $("#tblIndividual").css('display', 'block');
        }  

    }
    function WeekendType(rb) {
        $('#WeekendHolidayWorkingHour_strWHType').val(rb.value);

    }
    
   
    
</script>
<form id="frmWeekendHolidayWorkingHour" method="post" action="">
<div id="divWeekendHolidayWorkingHour">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.WeekendHolidayWorkingHour.intRowID)%>
            <%= Html.HiddenFor(m => m.IsNew)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 45%" />
            <col style="width: 55%" />
        </colgroup>
        <tr>
            <%=Html.HiddenFor(m => m.WeekendHolidayWorkingHour.EntryType)%>
            <td align="center">
                <%=Html.RadioButton("Model_WeekendHolidayWorkingHour_EntryType", 1, (Model.WeekendHolidayWorkingHour.EntryType == 1), new { onClick = "ChangeEntryType(this);", @class = "rbIndividual" })%>All
                Employees &nbsp;
                <%=Html.RadioButton("Model_WeekendHolidayWorkingHour_EntryType", 2, (Model.WeekendHolidayWorkingHour.EntryType == 2), new { onClick = "ChangeEntryType(this);", @class = "rbAll" })%>Individual
            </td>
            <%=Html.HiddenFor(m => m.WeekendHolidayWorkingHour.strWHType)%>
            <td align="center">
                <%=Html.RadioButton("Model_WeekendHolidayWorkingHour_strWHType", "w", (Model.WeekendHolidayWorkingHour.strWHType == "w"), new { onClick = "WeekendType(this);" })%>Weekend
                &nbsp;
                <%=Html.RadioButton("Model_WeekendHolidayWorkingHour_strWHType", "h", (Model.WeekendHolidayWorkingHour.strWHType == "h"), new { onClick = "WeekendType(this);" })%>Holiday
                &nbsp;
                <%=Html.RadioButton("Model_WeekendHolidayWorkingHour_strWHType", "b", (Model.WeekendHolidayWorkingHour.strWHType == "b"), new { onClick = "WeekendType(this);" })%>Both
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblIndividual" width="100%">
                    <colgroup>
                        <col width="37%" />
                        <col />
                    </colgroup>
                    <tr>
                        <td>
                            Employee Name
                            <label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strEmpName, new { @class = "textRegular", @readonly = "true" })%>
                            <% if (Model.IsNew) %>
                            <%{ %>
                            <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
                            <%}%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Employee ID
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strEmpID, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Designation
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strDesignation, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Department
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strDepartment, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strCompany, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Location
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strLocation, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>                                   
                </table>
                <table id="tblAll" width="100%">
                    <colgroup>
                        <col width="37%" />
                        <col />
                    </colgroup>
                    <tr>
                        <td>
                            Company
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.WeekendHolidayWorkingHour.strCompanyID, Model.Company, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Location
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.WeekendHolidayWorkingHour.strLocationID, Model.Location, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Department
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.WeekendHolidayWorkingHour.strDepartmentID, Model.Department, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Designation
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.WeekendHolidayWorkingHour.strDesignationID, Model.Designation, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Emp. Category
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.WeekendHolidayWorkingHour.intCategoryCode, Model.EmployeeCategory, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Religion
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.WeekendHolidayWorkingHour.intReligionID, Model.Religion, "...Select One..", new { @class = "selectBoxRegular"})%>
                        </td>
                    </tr>                   
                </table>
            </td>
            <td>
                <table class="contenttext">
                    <colgroup>
                        <col width="20%" />
                        <col width="30%"/>
                        <col width="19%"/>
                        <col width="31%"/>
                    </colgroup>
                    <tr>
                        <td>
                            From Date
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strPeriodFrom, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                            <%--<img alt="" id="datepicker" style="height:16px;"  src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  onclick="return datepicker_onclick()" />--%>
                        </td>
                        <td>
                            To Date
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strPeriodTo, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                            <%--<img alt="" id="datepicker1" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            In Time
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strInTime, new { @class = "textRegularTime selector", @readonly = "readonly"  })%>
                        </td>
                        <td>
                            Out Time
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strOutTime, new { @class = "textRegularTime selector", @readonly = "readonly" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            In Grace Time
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.intGraceInMin, new { @class = "textRegularTime required" })%>
                        </td>
                        <td>
                            Out Grace Time
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.intGraceOutMin, new { @class = "textRegularTime required" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Absent Time
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.intAbsentMin, new { @class = "textRegularTime required" })%>
                        </td>
                        <td>
                            Total Break Time
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.intBreakTime, new { @class = "textRegularTime required"})%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Half Time
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strHalfTime, new { @class = "textRegularTime selector", @readonly = "readonly" })%>
                        </td>
                        <td>
                            Alternative Holiday
                        </td>
                        <td>
                           <%=Html.TextBoxFor(m => m.WeekendHolidayWorkingHour.strAlternativeHoliday, new { @class = "textRegularDate dtPicker dateNR", maxlength = 10 })%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.WeekendHolidayWorkingHour.intRowID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendHolidayWorkingHour, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendHolidayWorkingHour, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.WeekendHolidayWorkingHour.intRowID > 0)
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
    disableEntryType();
</script>
</form>
