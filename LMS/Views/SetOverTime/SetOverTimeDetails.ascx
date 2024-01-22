<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.SetOverTimeModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        preventSubmitOnEnter($("#frmSetOverTime"));

        setTitle("Working Hour for Weekend/Holiday");

        ChangeEntryType(document.getElementById('SetOverTime_EntryType'));
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


    function setData(id, name) {
        document.getElementById('SetOverTime_strEmpName').value = name;
        document.getElementById('SetOverTime_strEmpID').value = id;
        GetInfo(id);
        $("#divEmpList").dialog('close');
    }

    function searchEmployee() {
        window.parent.openEmployee();
    }

    updateFields = function (data) {
        
        $('#SetOverTime_strDesignation').val(data.strDesignation);
        $('#SetOverTime_strDepartment').val(data.strDepartment);
        $('#SetOverTime_strLocation').val(data.strLocation);
        $('#SetOverTime_strCompany').val(data.strCompany);
    };

    function GetInfo(id) {
        var form = $("#frmSetOverTime");
        var serializedForm = form.serialize();

        $.getJSON("getEmployeeInformation", serializedForm, updateFields);

    }

    function save() {
//        var dtFrom = $('#SetOverTime_strPeriodFrom').val();
        //        var dtTo = $('#SetOverTime_strPeriodTo').val();
        var strHTML;
        if (fnValidateDateTime() == false) {
            alert("Invalid Date.");
            return false;
        }

        if (fnValidateDateTime()== true) {
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
            var targetDiv = '#divSetOverTimeDetails';
            var url = '/LMS/SetOverTime/Delete';
            var form = $('#frmSetOverTime');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function Closing() {

    }

    function ChangeEntryType(rb) {
        $('#SetOverTime_EntryType').val(rb.value);

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
        $('#SetOverTime_strWHType').val(rb.value);

    }

    function onCheckChange(value, type) {       
        var enabled;
        if (value) enabled = false;
        else enabled = true

        if (type == 1) {
            $("#SetOverTime_intCalMinDuration").attr("disabled", enabled);
            if(enabled) $("#SetOverTime_intCalMinDuration").val(0);
         }
    if (type == 2) {
        $("#SetOverTime_mnyMaxOTHour").attr("disabled", enabled);
        if (enabled) $("#SetOverTime_mnyMaxOTHour").val(0);
    }
     if (type == 3) {
         $("#SetOverTime_mnyOTCeilingAmount").attr("disabled", enabled);
         if (enabled) $("#SetOverTime_mnyOTCeilingAmount").val(0);
     }
            
        

    }
    
</script>
<form id="frmSetOverTime" method="post" action="">
<div id="divSetOverTime">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.SetOverTime.intRowID)%>
            <%= Html.HiddenFor(m => m.IsNew)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 45%" />
            <col style="width: 55%" />
        </colgroup>
        <tr>
            <%=Html.HiddenFor(m => m.SetOverTime.EntryType)%>
            <td align="center">
                <%=Html.RadioButton("Model_SetOverTime_EntryType", 1, (Model.SetOverTime.EntryType == 1), new { onClick = "ChangeEntryType(this);", @class = "rbIndividual" })%>All
                Employees &nbsp;
                <%=Html.RadioButton("Model_SetOverTime_EntryType", 2, (Model.SetOverTime.EntryType == 2), new { onClick = "ChangeEntryType(this);", @class = "rbAll" })%>Individual
            </td>
            <%=Html.HiddenFor(m => m.SetOverTime.bitFromConfirmationDate)%>
            <td align="center">
                <%=Html.RadioButton("Model_SetOverTime_bitFromConfirmationDate", false, (Model.SetOverTime.bitFromConfirmationDate == false), new { onClick = "WeekendType(this);" })%>Joining Date
                &nbsp;
                <%=Html.RadioButton("Model_SetOverTime_bitFromConfirmationDate", true, (Model.SetOverTime.bitFromConfirmationDate == true), new { onClick = "WeekendType(this);" })%>Confirmation Date
               
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
                            <%= Html.TextBoxFor(m => m.SetOverTime.strEmpName, new { @class = "textRegular", @readonly = "true" })%>
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
                            <%= Html.TextBoxFor(m => m.SetOverTime.strEmpID, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Designation
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.SetOverTime.strDesignation, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Department
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.SetOverTime.strDepartment, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.SetOverTime.strCompany, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Location
                        </td>
                        <td>
                            <%= Html.TextBoxFor(m => m.SetOverTime.strLocation, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
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
                            <%= Html.DropDownListFor(m => m.SetOverTime.strCompanyID, Model.Company, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Location
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.SetOverTime.strLocationID, Model.Location, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Department
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.SetOverTime.strDepartmentID, Model.Department, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Designation
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.SetOverTime.strDesignationID, Model.Designation, "...Select One..", new { @class = "selectBoxRegular" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Emp. Category
                        </td>
                        <td>
                            <%= Html.DropDownListFor(m => m.SetOverTime.intCategoryCode, Model.EmployeeCategory, "...Select One..", new { @class = "selectBoxRegular" })%>
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
                            <%=Html.TextBoxFor(m => m.SetOverTime.strPeriodFrom, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                            
                        </td>
                        <td>
                            To Date
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.SetOverTime.strPeriodTo, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%=Html.CheckBox("cbMinOT", new { onClick = "onCheckChange(this.checked,1);" })%>&nbsp;                            
                            Min. unit to calculate Overtime (Calculate overtime if duration is more than)
                        </td>
                        <td colspan="2">
                            <%=Html.TextBoxFor(m => m.SetOverTime.intCalMinDuration, new { @class = "textRegularTime integerNR", @disabled = "true" })%>&nbsp;Minutes
                        </td>
                    </tr>
                    <tr>
                       <td colspan="2">
                             <%=Html.CheckBox("cbMaxOT", new { onClick = "onCheckChange(this.checked,2);" })%>&nbsp; 
                            Max. Overtime per day
                        </td>
                       <td colspan="2">
                            <%=Html.TextBoxFor(m => m.SetOverTime.mnyMaxOTHour, new { @class = "textRegularTime doubleNR", @disabled="true" })%>&nbsp;Hours
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                             <%=Html.CheckBox("cbOTU", new { onClick = "onCheckChange(this.checked,3);" })%>&nbsp;
                            Overtime Unit
                        </td>
                       <td colspan="2">
                            <%= Html.TextBoxFor(m => m.SetOverTime.mnyOTCeilingAmount, new { @class = "textRegularTime doubleNR", @disabled = "true" })%>&nbsp;Minutes
                            
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
    <%if (Model.SetOverTime.intRowID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.SetOverTime, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.SetOverTime, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.SetOverTime.intRowID > 0)
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
    if ($("#SetOverTime_intCalMinDuration").val() > 0) {
        $("#cbMinOT").attr("checked", true);
        onCheckChange(true, 1);
        }
        if ($("#SetOverTime_mnyMaxOTHour").val() > 0) {
            $("#cbMaxOT").attr("checked", true);
            onCheckChange(true, 2);
        }
    if ($("#SetOverTime_mnyOTCeilingAmount").val() > 0) {
        $("#cbOTU").attr("checked", true);
        onCheckChange(true, 3);
        }
    
</script>
</form>
