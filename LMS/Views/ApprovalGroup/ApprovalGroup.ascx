<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ApprovalGroupModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmApprovalGroup"));

        $("#divStyleLeaveType").dialog({ autoOpen: false, modal: true, height: 480, width: 720, resizable: false, title: 'Approval Group',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/ApprovalGroup/ApprovalGroup?page=' + pg;
                var form = $('#frmApprovalGroup');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });


    function Closing() {
        //window.location = "/LMS/LeaveType";    
    }

    function deleteApprovalGroup(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intLeaveTypeID: Id }, '/LMS/ApprovalGroup/Delete', 'divApprovalGroupList');
        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ApprovalGroup/Details/' + Id;
        $('#styleOpenerLeaveType').attr({ src: url });
        $("#divStyleLeaveType").dialog('open');
        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ApprovalGroup/ApprovalGroupAdd';
        $('#styleOpenerLeaveType').attr({ src: url });
        $("#divStyleLeaveType").dialog('open');
        return false;
    }


</script>

<h3 class="page-title">Approval Group</h3>

<form id="frmApprovalGroup" method="post" action="">
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalGroup, LMS.Web.Permission.MenuOperation.Add))
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
                        Approval Group
                    </th>                    
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalGroup, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.ApprovalGroup obj in Model.LstApprovalGroup)
                   {
                   
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.ApprovalGroupName)%>
                    </td>                   
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intApprovalGroupId%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstApprovalGroupPaging.PageNumber, ViewData.Model.LstApprovalGroupPaging.TotalItemCount, "frmApprovalGroup", "/LMS/ApprovalGroup/ApprovalGroup", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstApprovalGroupPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstApprovalGroup.Count.ToString()%></label>
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
