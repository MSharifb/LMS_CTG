<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.HolidayWeekendRuleModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmHolidayWeekendRule"));

        $("#divStyleHolidayRule").dialog({ autoOpen: false, modal: true, height: 500, width: 780, resizable: false, title: 'Weekend & Holiday Rule',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/HolidayWeekendRule/HolidayWeekendRule?page=' + pg;
                var form = $('#frmHolidayWeekendRule');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });

    function Closing() {
        //window.location = "/LMS/LeaveType";
    }

    function deleteHolidayWeekendRule(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intHolidayRuleID: Id }, '/LMS/HolidayWeekendRule/Delete', 'divDataList');

        }
        return false;
    }





    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/HolidayWeekendRule/Details/' + Id;
        $('#styleOpenerHolidayRule').attr({ src: url });
        $("#divStyleHolidayRule").dialog('open');
        return false;
    }
    function popupStyleAdd() {
        //alert();
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/HolidayWeekendRule/HolidayWeekendRuleAdd';
        $('#styleOpenerHolidayRule').attr({ src: url });
        $("#divStyleHolidayRule").dialog('open');
        return false;
    }


</script>

<h3 class="page-title">Weekend &amp; Holiday Rules</h3>

<form id="frmHolidayWeekendRule" method="post" action="">
<div>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendAndHolidayRule, LMS.Web.Permission.MenuOperation.Add))
      {%>
    <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
    <%} %>
</div>
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
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendAndHolidayRule, LMS.Web.Permission.MenuOperation.Edit))
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
                <%  foreach (LMSEntity.HolidayWeekDayRule lr in Model.LstHolidayWeekDayRulePaging)
                    {
                       
                %>
                <tr>
                    <td>
                        <%=Html.Encode(lr.strHolidayRule.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strYearTitle)%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= lr.intHolidayRuleID  %>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstHolidayWeekDayRulePaging.PageNumber, ViewData.Model.LstHolidayWeekDayRulePaging.TotalItemCount, "frmHolidayWeekendRule", "/LMS/HolidayWeekendRule/HolidayWeekendRule", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstHolidayWeekDayRulePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstHolidayWeekDayRule.Count.ToString()%></label>
</div>
</form>
<div id="divStyleHolidayRule">
    <iframe id="styleOpenerHolidayRule" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
