<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveRuleModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveRule"));

        $("#divStyleLeaveRule").dialog({ autoOpen: false, modal: true, height: 650, width: 750, resizable: false, title: 'Leave Rule',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/LeaveRule/LeaveRule?page=' + pg;
                var form = $('#frmLeaveRule');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });
    
    function Closing() {
        //window.location = "/LeaveRule";
    }
    
        function deleteLeaveRule(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intLeaveRuleID: Id }, '/LMS/LeaveRule/Delete', 'divLeaveRuleDetails');

        }
        return false;
    }
        
    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveRule/Details/' + Id;

        $('#styleOpenerLeaveRule').attr({ src: url });
        $("#divStyleLeaveRule").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveRule/LeaveRuleAdd';

        $('#styleOpenerLeaveRule').attr({ src: url });
        $("#divStyleLeaveRule").dialog('open');
        return false;
    }


    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/LeaveRule/LeaveRule";
        var form = $("#frmLeaveRule");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


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

<h3 class="page-title">Leave Rules</h3>

<form id="frmLeaveRule" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveRule, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td align="right" style="width: 80%;">
                Leave Type
                <%= Html.DropDownList("Model.intSearchLeaveTypeId",Model.LeaveType, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td align="right" style="width: 10%;">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>
    </table>
</div>
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
                        Yearly Entitlement
                    </th>
                    <th>
                        Encashable
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveRule, LMS.Web.Permission.MenuOperation.Edit))
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
                <% foreach (LMSEntity.LeaveRule lr in Model.LstLeaveRulePaging)
                   { 
                %>
                <tr>
                    <td>
                        <%=Html.Encode(lr.strLeaveType)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strRuleName.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.fltEntitlement.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.bitIsEncashable == true ? "Yes" : "No")%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= lr.intRuleID  %>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstLeaveRulePaging.PageNumber, ViewData.Model.LstLeaveRulePaging.TotalItemCount,"frmLeaveRule","/LMS/LeaveRule/LeaveRule", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveRulePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstLeaveRule.Count.ToString() %></label>
</div>

</form>
<div id="divStyleLeaveRule">
    <iframe id="styleOpenerLeaveRule" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
