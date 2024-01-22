<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ShiftAssignmentModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {


        $("#divNew").dialog({ autoOpen: false, modal: true, height: 600, width: 800, resizable: false, title: 'Shift Assignment', beforeclose: function (event, ui) { Closing(); } });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        preventSubmitOnEnter($("#frmShiftAssignment"));
        $("#dvEmpID").hide();

        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        $("#divNew").dialog({ autoOpen: false, modal: true, height: 600, width: 800, resizable: false, title: 'Card Assignment',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/ShiftAssignment/ShiftAssignment?page=' + pg;
                var form = $('#frmShiftAssignment');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });

    function setData(id, name) {

        document.getElementById('Model_ShiftAssignment_StrEmpName').value = name;
        document.getElementById('Model_ShiftAssignment_StrEmpID').value = id;
        $("#divEmpList").dialog('close');
    }

    function closeDialogBox1() {

        $("#divNew").dialog('close');
        //searchCData();

    }
    function Hide(control, type) {

        $("." + type).each(function () {
            $(this).hide();
        });

        $("#" + "show" + type).show();
        $(control).hide();
    }

    function Show(control, type) {

        $("." + type).each(function () {
            $(this).show();
        });

        $("#" + "hide" + type).show();
        $(control).hide();
    }
    function Closing() {
        searchCData();
    }

    function deleteShiftAssignment(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intShiftAssignmentID: Id }, '/LMS/ShiftAssignment/Delete', 'divShiftAssignmentDetails');
        }
        return false;
    }

    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ShiftAssignment/Details/' + Id;

        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ShiftAssignment/ShiftAssignmentAdd';

        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');
        return false;
    }


    function searchCData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/ShiftAssignment/ShiftAssignment";
        var form = $("#frmShiftAssignment");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        return false;
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

    /*remove the class which made red color of the drop down list */
    $(function () {
        $("select").each(function () {
            if ($(this).hasClass('input-validation-error')) {
                $(this).removeClass('input-validation-error');
            }
        })
    });
</script>
<form id="frmShiftAssignment" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                Employee Name
            </td>
            <td>
                <%= Html.TextBox("Model.ShiftAssignment.strEmpName", Model.ShiftAssignment.strEmpName, new { @class = "textRegular" })%>
                <div id="dvEmpID">
                    <%= Html.TextBox("Model.ShiftAssignment.StrEmpID", Model.ShiftAssignment.strEmpID, new { @class = "textRegular" })%>
                </div>
                <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
            </td>
            <td>
                Shift Name
            </td>
            <td align="right">
                <%= Html.DropDownList("Model.ShiftAssignment.intShiftID", Model.Shift, ".....All....", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ShiftAssignment, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td colspan="2">
                &nbsp;
            </td>
            <td align="right">
                <a href="#" class="btnSearchData" onclick="return searchCData();"></a>
            </td>
        </tr>
    </table>
</div>
<div id="grid">
    <div id="grid-data" style="overflow: auto; height: 400px; width: 99%">
        <table>
            <thead>
                <tr>
                    <th style="width: 120px">
                        Shift Name
                    </th>
                    <th style="width: 100px; text-align: center">
                        Effective Date
                    </th>
                    <th>
                        Employee Name
                    </th>
                    <th>
                        Company
                    </th>
                    <th>
                        Location
                    </th>
                    <th>
                        Department
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        Emp. Category
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ShiftAssignment, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th style="width: 20px">
                        Edit
                    </th>
                    <%} %>
                    <%-- <th>
                        Delete
                    </th>--%>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.ShiftAssignment lr in Model.LstShiftAssignmentPaging)
                   { 
                %>
                <tr>
                    <td>
                        <%=Html.Encode(lr.strShiftName)%>
                    </td>
                    <td style="text-align: center">
                        <%=Html.Encode(lr.dtEffectiveDate.ToString("dd-MMM-yyyy"))%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strEmpName)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strCompany)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strLocation)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strDepartment)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strDesignation)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strCategory)%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= lr.intShiftAssignmentID  %>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstShiftAssignmentPaging.PageNumber, ViewData.Model.LstShiftAssignmentPaging.TotalItemCount, "frmShiftAssignment", "/LMS/ShiftAssignment/ShiftAssignment", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstShiftAssignmentPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstShiftAssignment.Count.ToString() %></label>
</div>
<div id="divNew">
    <iframe id="styleNewEntry" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
</form>
