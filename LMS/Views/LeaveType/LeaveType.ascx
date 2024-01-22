<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveTypeModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveType"));

        $("#divStyleLeaveType").dialog({ autoOpen: false, modal: true, height: 480, width: 720, resizable: false, title: 'Leave Type',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/LeaveType/LeaveType?page=' + pg;
                var form = $('#frmLeaveType');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });


    function Closing() {
        //window.location = "/LMS/LeaveType";    
    }

    function deleteLeaveType(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intLeaveTypeID: Id }, '/LMS/LeaveType/Delete', 'divLeaveTypeList');
        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveType/Details/' + Id;
        $('#styleOpenerLeaveType').attr({ src: url });
        $("#divStyleLeaveType").dialog('open');
        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveType/LeaveTypeAdd';
        $('#styleOpenerLeaveType').attr({ src: url });
        $("#divStyleLeaveType").dialog('open');
        return false;
    }


</script>

<h3 class="page-title">Leave Type</h3>

<form id="frmLeaveType" method="post" action="">
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveType, LMS.Web.Permission.MenuOperation.Add))
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
                        Short Name
                    </th>
                    <th>
                        Earn Leave
                    </th>
                    <th>
                        Entitlement Type
                    </th>
                    <th>
                        Leave Year Type
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveType, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.LeaveType obj in Model.LstLeaveType1)
                   {
                   
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.strLeaveType)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strLeaveShortName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.bitIsEarnLeave == true ? "Yes" : "No")%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.isServiceLifeType == true ? "Service Life" : "Yearly")%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.isServiceLifeType == false ? obj.LeaveYearTypeName : "N/A")%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intLeaveTypeID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstLeaveType1.PageNumber, ViewData.Model.LstLeaveType1.TotalItemCount, "frmLeaveType", "/LMS/LeaveType/LeaveType", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveType1.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstLeaveType.Count.ToString()%></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleLeaveType">
    <iframe id="styleOpenerLeaveType" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
