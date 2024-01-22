<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.AttRuleModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmAttRule"));

        $("#divStyleAttRule").dialog({ autoOpen: false, modal: true, height: 750, width: 800, resizable: false, title: 'Attendance Rule',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/AttRule/AttRule?page=' + pg;
                var form = $('#frmAttRule');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });
    
    function Closing() {
        //window.location = "/AttRule";
    }
    
        function deleteAttRule(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intAttRuleID: Id }, '/LMS/AttRule/Delete', 'divAttRuleDetails');

        }
        return false;
    }
        
    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/AttRule/Details/' + Id;

        $('#styleOpenerAttRule').attr({ src: url });
        $("#divStyleAttRule").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/AttRule/AttRuleAdd';

        $('#styleOpenerAttRule').attr({ src: url });
        $("#divStyleAttRule").dialog('open');
        return false;
    }


    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/AttRule/AttRule";
        var form = $("#frmAttRule");
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
<form id="frmAttRule" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AttRule, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td align="right" style="width: 80%;">
              &nbsp;
            </td>
            <td align="right" style="width: 10%;">
                <%--<a href="#" class="btnSearchData" onclick="return searchData();"></a>--%>
            </td>
        </tr>
    </table>
</div>
<div id="grid">
    <div id="grid-data" style="overflow: auto; height:400px; width: 99%">
        <table>
            <thead>
                <tr>
                    <th>
                        Effective Date
                    </th>
                    <th>
                        Working Hour
                    </th>
                    <th>
                       Overtime Rule
                    </th>
                    <th>
                        Early Arrival
                    </th>
                    <th>
                        Late Arrival
                    </th>
                    <th>
                        Early Departure
                    </th>
                    <th>
                        Late Departure
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AttRule, LMS.Web.Permission.MenuOperation.Edit))
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
                <% foreach (LMSEntity.ATT_tblRule lr in Model.LstATT_tblRulePaging)
                   { 
                %>
                <tr>
                    <td>
                        <%=Html.Encode(lr.dtEffectiveDate.ToString("dd-MMM-yyyy"))%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.intWorkingHourRule == 1? "Rule-1" : lr.intWorkingHourRule == 2 ? "Rule-2" : "None"  )%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.intOverTimeRule == 1 ? "Rule-1" : lr.intOverTimeRule == 2 ? "Rule-2" : "None")%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.intEarlyArrival == 1 ? "Rule-1" : lr.intEarlyArrival == 2 ? "Rule-2" : "None")%>
                    </td>
                     <td>
                        <%=Html.Encode(lr.intLateArrival == 1 ? "Rule-1" : lr.intLateArrival == 2 ? "Rule-2" : "None")%>
                    </td>
                     <td>
                        <%=Html.Encode(lr.intEarlyDeparture == 1 ? "Rule-1" : lr.intEarlyDeparture == 2 ? "Rule-2" : "None")%>
                    </td>
                     <td>
                        <%=Html.Encode(lr.intLateDeparture == 1 ? "Rule-1" : lr.intLateDeparture == 2 ? "Rule-2" : "None")%>
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
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstATT_tblRulePaging.PageNumber, ViewData.Model.LstATT_tblRulePaging.TotalItemCount, "frmAttRule", "/LMS/AttRule/AttRule", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstATT_tblRulePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstATT_tblRule.Count.ToString() %></label>
</div>

</form>
<div id="divStyleAttRule">
    <iframe id="styleOpenerAttRule" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
