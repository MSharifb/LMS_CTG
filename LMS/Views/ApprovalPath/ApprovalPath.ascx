<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ApprovalPathModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmApprovalPath"));

        $("#divStyleApproval").dialog({ autoOpen: false, modal: true, height: 500, width: 720, resizable: false, title: 'Approval Flow',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();

                var targetDiv = "#divDataList";
                var url = '/LMS/ApprovalPath/ApprovalPath?page=' + pg;
                var form = $("#frmApprovalPath");
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

        $("#divStyleApprover").dialog({ autoOpen: false, modal: true, height: 550, width: 820, resizable: false, title: 'Set Approver', beforeclose: function (event, ui) { Closing(); } });

        $("#divStyleEmpApprover").dialog({ autoOpen: false, modal: true, height: 600, width: 500, resizable: false, title: 'Serach Employee', beforeclose: function (event, ui) { Closing(); } });

    });


    function setData(id, name) {

        document.getElementById('styleOpenerApprover').contentDocument.getElementById('ApprovalAuthor_strAuthorID').value = id;
        document.getElementById('styleOpenerApprover').contentDocument.getElementById('ApprovalAuthor_strEmpName').value = name;        //document.getElementById('styleOpenerApprover').contentDocument.getElementById('ApprovalAuthor_strAuthorID').value = name;

        $("#divStyleEmpApprover").dialog('close');
    }



    function Closing() {

    }

    function deleteApprovalPath(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intApprovalPathID: Id }, '/LMS/ApprovalPath/Delete', 'divDataList');
        }
        return false;
    }

    function openEmployee() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/Employee/list';
        $('#styleOpenerEmpApprover').attr({ src: url });
        $("#divStyleEmpApprover").dialog('open');
        return false;
    }

    function openSetApprover(Id) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ApprovalPath/SetApprover/' + Id;
        $('#styleOpenerApprover').attr({ src: url });
        $("#divStyleApprover").dialog('open');
        return false;
    }

    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ApprovalPath/Details/' + Id;
        $('#styleOpenerApproval').attr({ src: url });
        $("#divStyleApproval").dialog('open');
        return false;
    }

    function popupStyleAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ApprovalPath/ApprovalPathAdd';
        $('#styleOpenerApproval').attr({ src: url });
        $("#divStyleApproval").dialog('open');
        return false;
    }


    function searchData() {
        var targetDiv = "#divDataList";
        var url = "/LMS/ApprovalPath/ApprovalPath";
        var form = $("#frmApprovalPath");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
</script>

<h3 class="page-title">Approval Flow</h3>

<form id="frmApprovalPath" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalPath, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td style="width: 10%;">
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td align="right" style="width: 80%;">
                Flow Name
                <%=Html.TextBox("Model.strSearchName", Model.strSearchName, new { @class = "textRegular", @maxlength = 100 })%>
            </td>
            <td align="right" style="width: 10%;">
                <a href="#" class="btnSearchData" style="text-align: center" onclick="return searchData();">
                </a>
            </td>
        </tr>
    </table>
</div>
<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
        <table class="contenttext" style="width: 100%;">
            <thead>
                <tr>
                    <th style="width: 80%;">
                        Flow Name
                    </th>
                     <th style="width: 80%;">
                       Approval Group
                    </th>
                    <%  bool isEditable = false; 
                                                
                    %>
                    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalPath, LMS.Web.Permission.MenuOperation.Edit))
                      {
                          isEditable = true; %>
                    <th style="width: 10%;">
                        Edit
                    </th>
                    <%} %>
                    <%
                        bool isSetApprove = false;
                        if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetApprover))
                        {
                            isSetApprove = true; %>
                    <th style="width: 10%;">
                        Set Approver
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <%  if (Model.LstApprovalPathMaster1 != null) foreach (LMSEntity.ApprovalPathMaster obj in Model.LstApprovalPathMaster1)
                        { 
                %>
                <tr>
                    <td style="width: 80%;">
                        <%=Html.Encode(obj.strPathName)%>
                    </td>
                     <td style="width: 80%;">
                        <%=Html.Encode(obj.ApprovalGroupName)%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td style="width: 10%;">
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intPathID%>);'>
                        </a>
                    </td>
                    <%} %>
                    <%if (isSetApprove)
                      { %>
                    <td style="width: 10%;">
                        <a href='#' class="gridEdit" onclick='javascript:return openSetApprover(<%= obj.intPathID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(ViewData.Model.LstApprovalPathMaster1.PageSize, ViewData.Model.LstApprovalPathMaster1.PageNumber, ViewData.Model.LstApprovalPathMaster1.TotalItemCount, "frmApprovalPath", "/LMS/ApprovalPath/ApprovalPath", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstApprovalPathMaster1.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstApprovalPathMaster.Count.ToString()%></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleApproval">
    <iframe id="styleOpenerApproval" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
<div id="divStyleApprover">
    <iframe id="styleOpenerApprover" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
<div id="divStyleEmpApprover">
    <iframe id="styleOpenerEmpApprover" src="" width="100%" height="100%">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
