<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.WeekendHolidayAsWorkingdayModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

//        $("#datepicker").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#Model_StrEffectiveDateFrom').datepicker('show'); });

//        $("#datepicker1").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#Model_StrEffectiveDateTo').datepicker('show'); });

//        $("#datepicker2").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#Model_StrDeclarationDate').datepicker('show'); });

        preventSubmitOnEnter($("#frmWeekendHolidayAsWorkingday"));
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy'
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });
        $("#divStyleWeekendHolidayAsWorkingday").dialog({ autoOpen: false, modal: true, height: 380, width: 720, resizable: false, title: 'Weekend/Holiday as Workingday',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/WeekendHolidayAsWorkingday/WeekendHolidayAsWorkingday?page=' + pg;
                var form = $('#frmWeekendHolidayAsWorkingday');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });


    function Closing() {
        //window.location = "/LMS/WeekendHolidayAsWorkingday";    
    }

    function deleteWeekendHolidayAsWorkingday(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intWeekendHolidayAsWorkingdayID: Id }, '/LMS/WeekendHolidayAsWorkingday/Delete', 'divWeekendHolidayAsWorkingdayList');
        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/WeekendHolidayAsWorkingday/Details/' + Id;
        $('#styleOpenerWeekendHolidayAsWorkingday').attr({ src: url });
        $("#divStyleWeekendHolidayAsWorkingday").dialog('open');

        return false;
    }
    function popupStyleAdd() {
        
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/WeekendHolidayAsWorkingday/WeekendHolidayAsWorkingdayAdd';
        $('#styleOpenerWeekendHolidayAsWorkingday').attr({ src: url });
        $("#divStyleWeekendHolidayAsWorkingday").dialog('open');

        return false;
    }

    function searchData() {

        var pdtAPFrom = $('#Model_StrEffectiveDateFrom').val();
        var pdtAPTo = $('#Model_StrEffectiveDateTo').val();
        var hookup = true;

        if (pdtAPFrom != '' || pdtAPTo != '') {

            if (checkDateValidation(pdtAPFrom, pdtAPTo) == true) {
                hookup = true;
            }
            else {
                hookup = false;
            }
        }

        if (hookup == true) {
            var targetDiv = '#divDataList';
            var url = '/LMS/WeekendHolidayAsWorkingday/WeekendHolidayAsWorkingday';
            var form = $('#frmWeekendHolidayAsWorkingday');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);
            }, "html");
        }
        return false;
    }

    function checkDateValidation(pdtAPFrom, pdtAPTo) {
        if (fnValidateDateTime() == false) {
            alert("Invalid Apply Date.");
            return false;
        }
        if (pdtAPFrom != '' && pdtAPTo != '') {
            if (pdtAPFrom > pdtAPTo) {
                alert("Effective Date From  must be smaller than or equal to 'Effective Date To'.");
                return false;
            }
        }

        return true;
    }

</script>
<form id="frmWeekendHolidayAsWorkingday" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                Effective Date From
            </td>
            <td>
                <%= Html.TextBox("Model.StrEffectiveDateFrom", Model.StrEffectiveDateFrom, new { @class = "textRegularDate dtPicker" })%>
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td>
                Effective Date To
            </td>
            <td>
                <%= Html.TextBox("Model.StrEffectiveDateTo", Model.StrEffectiveDateTo, new { @class = "textRegularDate dtPicker" })%>
                <%--<img alt="" id="datepicker1" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
        </tr>
        <tr>
            <td>
                Declaration Date
            </td>
            <td>
                <%= Html.TextBox("Model.StrDeclarationDate", Model.StrDeclarationDate, new { @class = "textRegularDate dtPicker" })%>
                <%--<img alt="" id="datepicker2" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td colspan="2" style="text-align: left">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>
    </table>
</div>
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendHolidayAsWorkingday, LMS.Web.Permission.MenuOperation.Add))
  {%>
<div style="margin-top: 4px; margin-bottom: 3px;">
    <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
</div>
<%} %>
<div id="grid" style="margin-top: 2px;">
    <div id="grid-data" style="overflow: auto; width: 99%">
        <table>
            <thead>
                <tr>                    
                    <th>
                       Effective Date From
                    </th>
                    <th>
                         Effective Date To
                    </th>
                    <th>
                        Declaration Date
                    </th>                   
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendHolidayAsWorkingday, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.WeekendHolidayAsWorkingday obj in Model.LstWeekendHolidayAsWorkingdayPaged)
                   {                   
                %>
                <tr>                    
                    <td>
                        <%=Html.Encode(obj.dtEffectiveDateFrom.ToString("dd-MMM-yyyy"))%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.dtEffectiveDateTo.ToString("dd-MMM-yyyy"))%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.dtDeclarationDate.ToString("dd-MMM-yyyy"))%>
                    </td>
                   
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intWeekendWorkingday%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstWeekendHolidayAsWorkingdayPaged.PageNumber, ViewData.Model.LstWeekendHolidayAsWorkingdayPaged.TotalItemCount, "frmShift", "/LMS/WeekendHolidayAsWorkingday/WeekendHolidayAsWorkingday", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstWeekendHolidayAsWorkingdayPaged.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstWeekendHolidayAsWorkingday.Count.ToString()%></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleWeekendHolidayAsWorkingday">
    <iframe id="styleOpenerWeekendHolidayAsWorkingday" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
