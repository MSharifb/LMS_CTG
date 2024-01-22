<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.HolidayWeekendRuleAssignModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmHWRuleAssingDetails"));

        setTitle("Holiday Weekend Rule Assignment");

        $("#btnSave").hide();
        $("#btnDelete").hide();

        AddRemoveRequired(0);

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 425, width: 780, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        FormatTextBox();

    });




    function setData(id,strEmpInitial, name) {
        document.getElementById('HolidayWeekendRuleAssign_strEmpID').value = id;
        document.getElementById('HolidayWeekendRuleAssign_strEmpInitial').value = strEmpInitial;
        document.getElementById('HolidayWeekendRuleAssign_strEmpName').value = name;

        //document.getElementById('HolidayWeekendRuleAssign_strEmpID').value = id;
        $("#lblIdNotFound").css('visibility', 'hidden');
        $('#HolidayWeekendRuleAssign_strEmpInitial').removeClass("invalid");
        
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




    function AddRemoveRequired(flag) {

        var IsIndividual = $("#Model_HolidayWeekendRuleAssign_IsIndividual").attr('checked');
        ///alert(IsIndividual);

        if (flag > 0) {
            document.getElementById('HolidayWeekendRuleAssign_strEmpID').value = "";
            document.getElementById('HolidayWeekendRuleAssign_strEmpInitial').value = "";
            $("#lblIdNotFound").css('visibility', 'hidden');
        }
        else {
            //document.getElementById('hdnEmpId').value = document.getElementById('HolidayWeekendRuleAssign_strEmpID').value;
        }
        
        $('#HolidayWeekendRuleAssign_IsIndividual').val(IsIndividual);
        if (IsIndividual == true) {
            $("#btnElipse").css('visibility', 'visible');
            $("#lblIDReqMark").css('visibility', 'visible');
            $("#lblNameReqMark").css('visibility', 'visible');

            $('#HolidayWeekendRuleAssign_strEmpInitial').addClass("required");
            $('#HolidayWeekendRuleAssign_strEmpName').addClass("required");

            $('#HolidayWeekendRuleAssign_strEmpInitial').removeAttr('disabled');
            $('#HolidayWeekendRuleAssign_strEmpName').removeAttr('disabled');


            $('#HolidayWeekendRuleAssign_strDepartmentID').val("");
            $('#HolidayWeekendRuleAssign_strDepartmentID').attr('disabled', 'disabled');
            $('#HolidayWeekendRuleAssign_strDesignationID').val("");
            $('#HolidayWeekendRuleAssign_strDesignationID').attr('disabled', 'disabled');
            $('#HolidayWeekendRuleAssign_strReligionID').val("");
            $('#HolidayWeekendRuleAssign_strReligionID').attr('disabled', 'disabled');
            $('#HolidayWeekendRuleAssign_intCategoryCode').val("");
            $('#HolidayWeekendRuleAssign_intCategoryCode').attr('disabled', 'disabled');

        }
        else {

            $("#btnElipse").css('visibility', 'hidden');
            $("#lblIDReqMark").css('visibility', 'hidden');
            $("#lblNameReqMark").css('visibility', 'hidden');

            $('#HolidayWeekendRuleAssign_strEmpInitial').removeClass("required");
            $('#HolidayWeekendRuleAssign_strEmpName').removeClass("required");

            $('#HolidayWeekendRuleAssign_strEmpInitial').val("");
            $('#HolidayWeekendRuleAssign_strEmpInitial').attr('disabled', 'disabled');
            $('#HolidayWeekendRuleAssign_strEmpName').val("");
            $('#HolidayWeekendRuleAssign_strEmpName').attr('disabled', 'disabled');

            $('#HolidayWeekendRuleAssign_strDepartmentID').removeAttr('disabled');
            $('#HolidayWeekendRuleAssign_strDesignationID').removeAttr('disabled');
            $('#HolidayWeekendRuleAssign_strReligionID').removeAttr('disabled');
            $('#HolidayWeekendRuleAssign_intCategoryCode').removeAttr('disabled');

        }
        //return false;
    }

    
    function save() {

//        if (document.getElementById('HolidayWeekendRuleAssign_strEmpID').value != document.getElementById('HolidayWeekendRuleAssign_strEmpID').value) {
//            $('#HolidayWeekendRuleAssign_strEmpID').addClass("invalid");
//        }
//        else {
//            $('#HolidayWeekendRuleAssign_strEmpID').removeClass("invalid");
//        }


        if (fnValidate() == true) {
            $('#btnSave').trigger('click');
            getHolidayRuleDetails();
        }
        return false;
    }


    function getHolidayRuleListByYearId() {
        //--[clear all rows from combo]--------------------------
        $('#HolidayWeekendRuleAssign_intHolidayRuleID')
        .empty()
        .append('<option value="0">...Select One...</option>')
        .find('option:first')
        .attr("selected", "selected")
        ;

        //--[Fillup combo]--------------------------
        targetDiv = "#divHolidayWeekendRuleAssignDetails";
        var url = "/LMS/HolidayWeekendRuleAssign/GetHolidayRuleListByYearId";

        var form = $("#frmHWRuleAssingDetails");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        //--[select first row]--------------------------
        $('#HolidayWeekendRuleAssign_intHolidayRuleID')
        .find('option:first')
        .attr("selected", "selected")
        ;

        return false;
    }

    function getHolidayRuleDetails() {
        targetDiv = "#divHolidayWeekendRuleAssignDetails";
        var form = $("#frmHWRuleAssingDetails");
        var serializedForm = form.serialize();
        var url = "/LMS/HolidayWeekendRuleAssign/GetHolidayRuleDetails";
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }


    function Delete() {
        
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeAction('frmHWRuleAssingDetails', '/LMS/HolidayWeekendRuleAssign/Delete', 'divHolidayWeekendRuleAssignDetails');

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

        $('#HolidayWeekendRuleAssign_strEmpInitial').removeClass("invalid");
        
        if (keyCode == 13) {
            //handle enter
            var id = document.getElementById('HolidayWeekendRuleAssign_strEmpInitial').value;
            var name = "";
           
           // getdata(id, name);
            var url = "/LMS/Employee/LookUpById/" + id;

            $.post(url, "", function (result) {

                if (result[0] != null && result[0] != "") {

                    document.getElementById('HolidayWeekendRuleAssign_strEmpID').value = result[0];
                    document.getElementById('HolidayWeekendRuleAssign_strEmpName').value = result[1];
                    document.getElementById('HolidayWeekendRuleAssign_strEmpInitial').value = result[2];
                   
                    $("#lblIdNotFound").css('visibility', 'hidden'); 
                }
                else {
                    document.getElementById('HolidayWeekendRuleAssign_strEmpID').value = "";
                    document.getElementById('HolidayWeekendRuleAssign_strEmpName').value = "";
                    document.getElementById('HolidayWeekendRuleAssign_strEmpInitial').value = "";

                    $("#lblIdNotFound").css('visibility', 'visible'); 
                }
            }, "json");                        
        }
        return true;
    } 
</script>
<form id="frmHWRuleAssingDetails" method="post" action="">
<div id="divHWRuleAssign">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.HolidayWeekendRuleAssign.intRuleAssignID)%>
           
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 55%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Leave Year<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.HolidayWeekendRuleAssign.intLeaveYearID, Model.LeaveYearList, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return getHolidayRuleListByYearId();" })%>
            </td>
        </tr>
        <tr>
            <td>
                Rule Name<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.HolidayWeekendRuleAssign.intHolidayRuleID, Model.HolidayRuleList, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return getHolidayRuleDetails();" })%>
            </td>
        </tr>
    </table>
    <div style="overflow-y: auto; overflow-x: hidden; height: 125px; width: 100%;">
        <table class="contenttext" style="width: 100%;">
            <tr>
                <td>
                    <div id="grid">
                        <div id="grid-data">
                            <table>
                                <thead>
                                    <tr>
                                        <th>
                                            From Date
                                        </th>
                                        <th>
                                            To Date
                                        </th>
                                        <th style="text-align: center;">
                                            Days
                                        </th>
                                        <th>
                                            Type
                                        </th>
                                        <th>
                                            Holiday Title
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <% if (Model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList != null)
                                       {
                                           for (int i = 0; i < Model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList.Count; i++)
                                           {
                                    %>
                                    <tr>
                                        <td>
                                            <%=Html.Encode(Model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList[i].dtDateFrom.ToString("ddd-dd MMM-yyyy"))%>
                                        </td>
                                        <td>
                                            <%=Html.Encode(Model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList[i].dtDateTo.ToString("ddd-dd MMM-yyyy"))%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%=Html.Encode(Model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList[i].intDuration.ToString())%>
                                        </td>
                                        <td>
                                            <%=Html.Encode(Model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList[i].strType.ToString())%>
                                        </td>
                                        <td>
                                            <%=Html.Encode(Model.HolidayWeekendRuleAssign.HolidayWeekDayRuleList[i].strHolidayTitle.ToString())%>
                                        </td>
                                    </tr>
                                    <%}
                                          } %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <%=Html.HiddenFor(m => m.HolidayWeekendRuleAssign.IsIndividual)%>
                        <td>
                            <%=Html.RadioButton("Model_HolidayWeekendRuleAssign_IsIndividual", true, Model.HolidayWeekendRuleAssign.IsIndividual, new { onClick = "AddRemoveRequired(1);" })%>Individual
                        </td>
                        <td>
                            <%=Html.RadioButton("Model_HolidayWeekendRuleAssign_IsIndividual", true, !Model.HolidayWeekendRuleAssign.IsIndividual, new { onClick = "AddRemoveRequired(1);" })%>All
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Applicant ID<label id="lblIDReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.HiddenFor(m => m.HolidayWeekendRuleAssign.strEmpID)%>
                                        <%=Html.TextBoxFor(m => m.HolidayWeekendRuleAssign.strEmpInitial, new { @class = "textRegularDate", @readonly = "readonly" })%>

                                        <%--<%=Html.TextBoxFor(m => m.HolidayWeekendRuleAssign.strEmpInitial, new { @class = "textRegularDate", onkeypress = "return handleEnter(event);" })%>--%>

                                        <a href="#" id="btnElipse" style="visibility: hidden" class="btnSearch" onclick="return searchEmployee();">
                                        </a>

                                         <label id="lblIdNotFound" style="visibility:hidden;vertical-align:5px; padding-left:10px; color:red;">Id not found !</label> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Name<label id="lblNameReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.HolidayWeekendRuleAssign.strEmpName, new { @class = "textRegular", @readonly = "readonly" })%>
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
                                        <%= Html.DropDownListFor(m => m.HolidayWeekendRuleAssign.strDepartmentID, Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Designation<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.HolidayWeekendRuleAssign.strDesignationID, Model.Designation, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Category<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.HolidayWeekendRuleAssign.intCategoryCode, Model.Category, "...All...", new { @class = "selectBoxRegular" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Religion<label class="labelRegular"></label>
                                    </td>
                                    <td>
                                        <%= Html.DropDownListFor(m => m.HolidayWeekendRuleAssign.strReligionID, Model.ReligionList, "...All...", new { @class = "selectBoxRegular" })%>
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
    <%if (Model.HolidayWeekendRuleAssign.intRuleAssignID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AssignWeekendAndHolidayRule, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AssignWeekendAndHolidayRule, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.HolidayWeekendRuleAssign.intRuleAssignID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
