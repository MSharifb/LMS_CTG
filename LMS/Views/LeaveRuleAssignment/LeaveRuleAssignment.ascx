<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveRuleAssignmentModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveRuleAssignment"));

        $("#divStyleLeaveRuleAsignment").dialog({ autoOpen: false, modal: true, height: 550, width: 820, resizable: false, title: 'Assign Leave Rule',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/LeaveRuleAssignment/LeaveRuleAssignment?page=' + pg;
                var form = $('#frmLeaveRuleAssignment');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

        $("#divStyleLeaveRuleAsignmentEmp").dialog({ autoOpen: false, modal: true, height: 425, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

    });


    function setData(id,strEmpInitial, name) {

        document.getElementById('styleOpenerLeaveRuleAsignment').contentDocument.getElementById('LeaveRuleAssignment_strEmpID').value = id;
        document.getElementById('styleOpenerLeaveRuleAsignment').contentDocument.getElementById('LeaveRuleAssignment_strEmpInitial').value = strEmpInitial;
        document.getElementById('styleOpenerLeaveRuleAsignment').contentDocument.getElementById('LeaveRuleAssignment_strEmpName').value = name;

        $("#divStyleLeaveRuleAsignmentEmp").dialog('close');

    }
    function Closing() {
        //window.location = "/LMS/LeaveType";
    }

    function selectView(Id) {
        window.location = "/LMS/LeaveRuleAssignmentDetail/" + Id;
        return false;
    }



    function deleteLeaveRuleAssignment(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intLeaveRuleAssignmentID: Id }, '/LMS/LeaveRuleAssignmentDelete', 'divDataList');

        }
        return false;
    }

    function openEmployee() {


        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/Employee/list';
        $('#styleOpenerLeaveRuleAsignmentEmp').attr({ src: url });
        $("#divStyleLeaveRuleAsignmentEmp").dialog('open');
        return false;
    }

    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/LeaveRuleAssignment/Details/' + Id;
        $('#styleOpenerLeaveRuleAsignment').attr({ src: url });
        $("#divStyleLeaveRuleAsignment").dialog('open');
        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveRuleAssignment/LeaveRuleAssignmentAdd';
        $('#styleOpenerLeaveRuleAsignment').attr({ src: url });
        $("#divStyleLeaveRuleAsignment").dialog('open');
        return false;
    }


    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/LeaveRuleAssignment/LeaveRuleAssignment";
        var form = $("#frmLeaveRuleAssignment");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        return false;
    }




    function GetRules() {

        var pLeaveTypeID = $('#Model_intSearchLeaveTypeID').val();
        $('#Model_intSearchRuleID > option:not(:first)').remove();

        if (pLeaveTypeID != "") {

            var form = $("#frmLeaveRuleAssignment");
            var serializedForm = form.serialize();

            $.post('/LMS/LeaveRuleAssignment/GetRules', serializedForm, function (result) {

                $.each(result, function () {
                    $("#Model_intSearchRuleID").append($("<option></option>").val(this['intRuleID']).html(this['strRuleName']));
                });


            }, "json");
        }

        return false;
    }

    /*remove the class which made red color of the drop down list */
    $(function () {
        $("select").each(function () {
            if ($(this).hasClass('input-validation-error')) {
                $(this).removeClass('input-validation-error');
            }
        })
    });   
</script>

<h3 class="page-title">Leave Rules Assignment</h3>

<form id="frmLeaveRuleAssignment" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                Applicant ID
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchInitial", Model.strSearchInitial, new { @class = "textRegular", @maxlength = 50 })%>
            </td>
            <td>
                Name
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchName", Model.strSearchName, new { @class = "textRegular", @maxlength = 100 })%>
            </td>
        </tr>
        <tr>
            <td>
                Leave Type
            </td>
            <td>
                <%= Html.DropDownList("Model.intSearchLeaveTypeID", Model.LeaveType, "...All...", new { @class = "selectBoxRegular", onchange = "return GetRules();" })%>
            </td>
            <td>
                Rule Name
            </td>
            <td>
                <%= Html.DropDownList("Model.intSearchRuleID", Model.LeaveRule, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
             <td>
                Category
            </td>
            <td>
                <%= Html.DropDownList("Model.intSearchCategoryId", Model.Category, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td>
                Department
            </td>
            <td>
                <%= Html.DropDownList("Model.strSearchDepartmentId", Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
            <td>
                Designation
            </td>
            <td>
                <%= Html.DropDownList("Model.strSearchDesignationId", Model.Designation, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td>
                Gender
            </td>
            <td>
                <%= Html.DropDownList("Model.strSearchGender", Model.Gender, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
            <td>
               
            </td>
            <td>
                
            </td>
            <td colspan="4" style="text-align: left">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>
    </table>
</div>
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AssignLeaveRule, LMS.Web.Permission.MenuOperation.Add))
  {%>
<div>
    <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
</div>
<%} %>
<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
        <table>
            <thead>
                <tr>
                    <th>
                        Leave Type
                    </th>
                    <th>
                        Rule Name
                    </th>
                   <th>
                   Emp. Initial
                   </th>
                    <th>
                        Department
                    </th>
                     <th>
                        Emp. Type
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        Category
                    </th>
                    <th>
                        Gender
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AssignLeaveRule, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.LeaveRuleAssignment obj in Model.LstLeaveRuleAssignment)
                   { 
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.strLeaveType)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strRuleName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strEmpInitial + '-' + obj.strEmpName)%>
                    </td>
                   
                    <td>
                        <%=Html.Encode(obj.strDepartment)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strEmployeeTypeName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strDesignation)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strEmpCategory)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strGender)%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intRuleAssignID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize, ViewData.Model.PageNumber, ViewData.Model.numTotalRows, "frmLeaveRuleAssignment", "/LMS/LeaveRuleAssignment/LeaveRuleAssignment", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString() %></label>
</div>
<div id="divStyleLeaveRuleAsignment">
    <iframe id="styleOpenerLeaveRuleAsignment" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
</form>
<div id="divStyleLeaveRuleAsignmentEmp">
    <iframe id="styleOpenerLeaveRuleAsignmentEmp" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
