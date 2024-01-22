<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.HolidayWeekDayModels>" %>

<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmHolidayWeekDay"));

        $("#divStyleHolidayWeekDay").dialog({ autoOpen: false, modal: true, height: 600, width: 720, resizable: false, title: 'Weekend & Holiday',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/HolidayWeekDay/HolidayWeekDay?page=' + pg;
                var form = $('#frmHolidayWeekDay');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

            }
        });

    });

    function Closing() {
        //window.location = "/LMS/LeaveType";
    }

    function selectView(Id) {
        window.location = "/LMS/HolidayWeekDay/Detail/" + Id;
        return false;
    }

    function deleteHolidayWeekDay(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intHolidayWeekDayID: Id }, '/LMS/HolidayWeekDay/Delete', 'divHolidayWeekDayList');

        }
        return false;
    }

    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/HolidayWeekDay/Details/' + Id;
        $('#styleOpenerHolidayWeekDay').attr({ src: url });
        $("#divStyleHolidayWeekDay").dialog('open');
        return false;
    }
    function popupStyleAdd() {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/HolidayWeekDay/HolidayWeekDayAdd';
        $('#styleOpenerHolidayWeekDay').attr({ src: url });
        $("#divStyleHolidayWeekDay").dialog('open');
        return false;
    }

    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/HolidayWeekDay/HolidayWeekDay";
        var form = $("#frmHolidayWeekDay");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        return false;
    }

    /*remove the class which made red color of the drop down list */
    $(function () {
        $("select").each(function() {
            if ($(this).hasClass('input-validation-error')) {
                $(this).removeClass('input-validation-error');
            }
        });
    });   
</script>

<h3 class="page-title">Weekend &amp; Holiday</h3>

<form id="frmHolidayWeekDay" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
<%--        <tr>
            <td colspan="2"> <a href="#">Process all weekend</a>
            </td>
            
            <td align="right" style="width: 10%;">
            </td>
        </tr>--%>
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendAndHoliday, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td align="right" style="width: 80%;">
                Leave Year
                <%= Html.DropDownList("Model.intSearchYearId", Model.SearchLeaveYear, new { @class = "selectBoxMedium" })%>
                Month
                <%= Html.DropDownList("Model.intSearchMonthId", Model.SearchMonth, "...All...", new { @class = "selectBoxMedium" })%>
                Type
                <%= Html.DropDownList("Model.strSearchType", Model.TypeList, "...All...", new { @class = "selectBoxMedium" })%>
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
                        Leave Year
                    </th>
                    <th>
                        Type
                    </th>
                    <th>
                        Holiday Title
                    </th>
                    <th>
                        From Date
                    </th>
                    <th>
                        To Date
                    </th>
                   <%-- <th>
                        Days
                    </th>--%>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendAndHoliday, LMS.Web.Permission.MenuOperation.Edit))
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
                <%  foreach (LMSEntity.HolidayWeekDay hw in Model.LstHolidayWeekDayPaging)
                    {
                  
                %>
                <tr>
                    <td>
                        <%=Html.Encode(hw.strYearTitle.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(hw.strType.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(hw.strHolidayTitle.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(hw.dtDateFrom.ToString("ddd-dd MMM-yyyy"))%>
                    </td>
                    <td>
                        <%=Html.Encode(hw.dtDateTo.ToString("ddd-dd MMM-yyyy"))%>
                    </td>
                   <%-- <td>
                        <%=Html.Encode(hw.intDuration.ToString())%>
                    </td>--%>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= hw.intHolidayWeekendMasterID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstHolidayWeekDayPaging.PageNumber, ViewData.Model.LstHolidayWeekDayPaging.TotalItemCount, "frmHolidayWeekDay", "/LMS/HolidayWeekDay/HolidayWeekDay", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstHolidayWeekDayPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstHolidayWeekDay.Count.ToString()%></label>
</div>
</form>
<div id="divStyleHolidayWeekDay">
    <iframe id="styleOpenerHolidayWeekDay" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
