<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveYearTypeModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveYearType"));

        $("#divStyleLeaveType").dialog({ autoOpen: false, modal: true, height: 480, width: 720, resizable: false, title: 'Leave Year Type',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/LeaveYearType/LeaveYearType?page=' + pg;
                var form = $('#frmLeaveYearType');
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

            executeCustomAction({ intLeaveTypeID: Id }, '/LMS/LeaveYearType/Delete', 'divLeaveTypeList');
        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveYearType/Details/' + Id;
        $('#styleOpenerLeaveType').attr({ src: url });
        $("#divStyleLeaveType").dialog('open');
        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveYearType/LeaveYearTypeAdd';
        $('#styleOpenerLeaveType').attr({ src: url });
        $("#divStyleLeaveType").dialog('open');
        return false;
    }


</script>

<h3 class="page-title">Leave Year Type</h3>

<form id="frmLeaveYearType" method="post" action="">
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveYearType, LMS.Web.Permission.MenuOperation.Add))
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
                        Leave Year Type
                    </th>
                    <th>
                       Start Month
                    </th>
                    <th>
                        End Month
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveYearType, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.LeaveYearType obj in Model.LstLeaveYearType)
                   {
                   
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.LeaveYearTypeName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.StartMonth)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.EndMonth)%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intLeaveYearTypeId%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstLeaveYearTypePaging.PageNumber, ViewData.Model.LstLeaveYearTypePaging.TotalItemCount, "frmLeaveYearType", "/LMS/LeaveYearType/LeaveYearType", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveYearTypePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstLeaveYearType.Count.ToString()%></label>
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
