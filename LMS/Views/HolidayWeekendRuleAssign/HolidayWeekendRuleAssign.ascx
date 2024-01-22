<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.HolidayWeekendRuleAssignModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmHolidayWeekendRuleAssign"));

        $("#divStyleHolidayRuleAssign").dialog({ autoOpen: false, modal: true, height: 550, width: 820, resizable: false, title: 'Assign Weekend & Holiday Rule',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/HolidayWeekendRuleAssign/HolidayWeekendRuleAssign?page=' + pg;
                var form = $('#frmHolidayWeekendRuleAssign');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

        $("#divStyleEmpHolidayRuleAssign").dialog({ autoOpen: false, modal: true, height: 425, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

    });


    function searchData() {

        var targetDiv = '#divDataList';
        var url = '/LMS/HolidayWeekendRuleAssign/HolidayWeekendRuleAssign';
        var form = $('#frmHolidayWeekendRuleAssign');
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        return false;
    }




    function setData(id,strEmpInitial, name) {
        document.getElementById('styleOpenerHolidayRuleAssign').contentDocument.getElementById('HolidayWeekendRuleAssign_strEmpID').value = id;
        document.getElementById('styleOpenerHolidayRuleAssign').contentDocument.getElementById('HolidayWeekendRuleAssign_strEmpInitial').value = strEmpInitial;
        document.getElementById('styleOpenerHolidayRuleAssign').contentDocument.getElementById('HolidayWeekendRuleAssign_strEmpName').value = name;

        $("#divStyleEmpHolidayRuleAssign").dialog('close');
    }


    function Closing() {
        //window.location = "/LMS/LeaveType";
    }


    function selectView(Id) {
        window.location = "/LMS/HolidayWeekendRuleAssign/Detail/" + Id;

        return false;
    }


    function deleteHolidayWeekendRuleAssign(Id) {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intRuleAssignID: Id }, '/LMS/HolidayWeekendRuleAssign/Delete', 'divHolidayWeekendRuleAssignDetails');
        }
        return false;
    }

    function openEmployee() {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/Employee/list';
        $('#styleOpenerEmpHolidayRuleAssign').attr({ src: url });
        $("#divStyleEmpHolidayRuleAssign").dialog('open');
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/HolidayWeekendRuleAssign/Details/' + Id;
        $('#styleOpenerHolidayRuleAssign').attr({ src: url });
        $("#divStyleHolidayRuleAssign").dialog('open');
        return false;
    }

    function popupStyleAdd() {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/HolidayWeekendRuleAssign/HolidayWeekendRuleAssignAdd';
        $('#styleOpenerHolidayRuleAssign').attr({ src: url });
        $("#divStyleHolidayRuleAssign").dialog('open');
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

<h3 class="page-title">Assign Weekend &amp; Holiday Rules</h3>

<form id="frmHolidayWeekendRuleAssign" method="post" action="">
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
               Applicant Name
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchName", Model.strSearchName, new { @class = "textRegular", @maxlength = 100 })%>
            </td>
        </tr>
        <tr>
            <td>
                Rule Name
            </td>
            <td>
                <%= Html.DropDownList("Model.intSearchRuleID", Model.HolidayRuleList, "...All...", new { @class = "selectBoxRegular" })%>
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
                Religion
            </td>
            <td>
                <%= Html.DropDownList("Model.strSearchReligionId", Model.ReligionList, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
            <td>
                Category
            </td>
            <td>
                <%= Html.DropDownList("Model.intSearchCategoryId", Model.Category, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td colspan="4" style="text-align: left">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>
    </table>
</div>
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AssignWeekendAndHolidayRule, LMS.Web.Permission.MenuOperation.Add))
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
                        Rule Name
                    </th>
                    <th>
                        Leave Year
                    </th>
                    <th>
                        Applicant
                    </th>
                    <th>
                        Department
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        Category
                    </th>
                    <th>
                        Religion
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AssignWeekendAndHolidayRule, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                    <%-- <th>
                        Delete
                    </th>--%>
                </tr>
            </thead>
            <tbody>
                <%  foreach (LMSEntity.HolidayWeekendRuleAssign obj in Model.LstHolidayWeekendRuleAssign)
                    {
                      
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.strHolidayRule)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strYearTitle)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strEmpInitial + '-' + obj.strEmpName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strDepartment)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strDesignation)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strEmpCategory)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strReligion)%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intRuleAssignID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize, ViewData.Model.PageNumber, ViewData.Model.numTotalRows, "frmHolidayWeekendRuleAssign", "/LMS/HolidayWeekendRuleAssign/HolidayWeekendRuleAssign", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString() %></label>
</div>
</form>
<div id="divStyleHolidayRuleAssign">
    <iframe id="styleOpenerHolidayRuleAssign" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
<div id="divStyleEmpHolidayRuleAssign">
    <iframe id="styleOpenerEmpHolidayRuleAssign" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
