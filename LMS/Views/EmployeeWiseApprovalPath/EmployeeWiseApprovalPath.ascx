<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.EmployeeWiseApprovalPathModels>" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmEmployeeWiseApprovalPath"));

        $("#divStyleEmployeeWiseApprovalPath").dialog({ autoOpen: false, modal: true, height: 600, width: 820, title: 'Assign Approval Flow',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();

                var targetDiv = "#divDataList";
                var url = '/LMS/EmployeeWiseApprovalPath/EmployeeWiseApprovalPath?page=' + pg;
                var form = $("#frmEmployeeWiseApprovalPath");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

            }
        });

    });


    function deleteLeaveType(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intEmpPathID: Id }, '/LMS/EmployeeWiseApprovalPath/Delete', 'divDataList');
        }
        return false;
    }


    function popupStyleDetails(Id, pathId) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/EmployeeWiseApprovalPath/Details/?pkid=' + Id + '&pathId=' + pathId;

        $('#styleOpenerEmployeeWiseApprovalPath').attr({ src: url });
        $("#divStyleEmployeeWiseApprovalPath").dialog('open');

        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/EmployeeWiseApprovalPath/EmployeeWiseApprovalPathAdd';

        $('#styleOpenerEmployeeWiseApprovalPath').attr({ src: url });
        $("#divStyleEmployeeWiseApprovalPath").dialog('open');

        return false;
    }

    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/EmployeeWiseApprovalPath/EmployeeWiseApprovalPath";
        var form = $("#frmEmployeeWiseApprovalPath");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        return false;
    }

    function GetInitialNode() {

        var pPathID = $('#Model_intSearchPathID').val();
        $('#Model_intSearchNodeID > option:not(:first)').remove();

        if (pPathID != "") {

            var form = $("#frmEmployeeWiseApprovalPath");
            var serializedForm = form.serialize();

            $.post('/LMS/EmployeeWiseApprovalPath/GetInitialNode', serializedForm, function (result) {

                $.each(result, function () {
                    $("#Model_intSearchNodeID").append($("<option></option>").val(this['intNodeID']).html(this['strNodeName']));
                });
                $("#Model_intSearchNodeID").val(intNodeID);
            }, "json");
        }

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

<h3 class="page-title">Assign Approval Flow</h3>

<form id="frmEmployeeWiseApprovalPath" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                Author ID
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchAuthorInitial", Model.strSearchAuthorInitial, new { @class = "textRegular", @maxlength = 50 })%>
            </td>
            <td>
                Author Name
            </td>
            <td>
                <%=Html.TextBox("Model.strSearchAuthorName", Model.strSearchAuthorName, new { @class = "textRegular", @maxlength = 100 })%>
            </td>
        </tr>
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
                Flow Name
            </td>
            <td>
                <%= Html.DropDownList("Model.intSearchPathID", Model.ApprovalPath, "...All...", new { @class = "selectBoxRegular", onchange = "return GetInitialNode();" })%>
            </td>
            <td>
                Designation
            </td>
            <td>
                <%= Html.DropDownList("Model.strSearchDesignationId", Model.Designation, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
           <%-- <td>
                Branch
            </td>
            <td>
                <%= Html.DropDownList("Model.strSearchLocationId", Model.Location, "...All...", new { @class = "selectBoxRegular" })%>
            </td>--%>
        </tr>
        <tr>
            <td>
                Department
            </td>
            <td>
                <%= Html.DropDownList("Model.strSearchDepartmentId", Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>
    </table>
</div>
<div>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.EmployeeWiseApprovalPath, LMS.Web.Permission.MenuOperation.Add))
      {%>
    <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
    <%} %>
</div>
<div id="grid">
    <div id="grid-data" style="overflow:auto; width:100%">
    <%--<div id="grid-data" style="overflow-y: auto; overflow-x: auto; height: 230px; width: 100%;">--%>
        <table style="width: 100%">
            <thead>
                <tr>
                    <th>
                        Flow Name
                    </th>
                    <th>
                        Initial Step
                    </th>
                    <th>
                        Approval Author
                    </th>
                    <%--<th>
                        Assign Type
                    </th>--%>
                    <th>
                        Applicant
                    </th>
                    
                    <th>
                       Department
                    </th>
                    <th>
                        Designation
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.EmployeeWiseApprovalPath, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.EmployeeWiseApprovalPath obj in Model.LstEmployeeWiseApprovalPath)
                   {
                   
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.strPathName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strNodeName)%>
                    </td>
                    <td>
                        <%--<%=Html.Encode(obj.strAuthorID.ToString() + '-' + obj.strAuthorName.ToString())%>--%>
                         <%=Html.Encode(obj.strAuthorInitial + " - " + obj.strAuthorName.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strEmpInitial + " - " + obj.strEmpName.ToString())%>
                    </td>
                    
                    <td>
                        <%=Html.Encode(obj.strDepartment.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strDesignation.ToString())%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick="javascript:return popupStyleDetails('<%= obj.intEmpPathID.ToString()%>','<%= obj.intPathID.ToString()%>');">
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%
               } %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize, ViewData.Model.PageNumber, ViewData.Model.numTotalRows, "frmEmployeeWiseApprovalPath", "/LMS/EmployeeWiseApprovalPath/EmployeeWiseApprovalPath", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString() %></label>
</div>
</form>
<div id="divStyleEmployeeWiseApprovalPath">
    <iframe id="styleOpenerEmployeeWiseApprovalPath" src="" width="99%" height="98%"
        style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
