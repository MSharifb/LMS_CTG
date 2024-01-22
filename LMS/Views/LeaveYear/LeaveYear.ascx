<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveYearModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmList"));
        dialogOpen();
    });

    function dialogOpen() {
        $("#divStyleLeaveYear").dialog({ autoOpen: false, modal: true, height: 480, width: 720, resizable: false, title: 'Leave Year',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/LeaveYear/LeaveYear?page=' + pg;
                var form = $('#frmList');
                var serializedForm = form.serialize();
                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); },
                "html");
            }
        });
    }

        function Closing() {
           
           // return false;
        }

    function selectView(Id) {
        window.location = "/LMS/LeaveYear/Detail/" + Id;

        return false;
    }

    function deleteLeaveYear(Id) {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intLeaveYearID: Id }, '/LMS/LeaveYear/Delete', 'divLeaveYearList');
        }
        return false;
    }

    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveYear/Details/' + Id;
        $('#styleOpener').attr({ src: url });
        $("#divStyleLeaveYear").dialog('open');
        return false;
    }

    function popupStyleAdd() {

        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/LeaveYear/LeaveYearAdd';
        $('#styleOpener').attr({ src: url });
        $("#divStyleLeaveYear").dialog('open');
        return false;
    }

    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/LeaveYear/LeaveYear";
        var form = $("#frmList");
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

<h3 class="page-title">Leave Year</h3>

<form id="frmList" method="post" action="">

<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveYear, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td align="right" style="width: 80%;">
                Leave Year Type
                <%= Html.DropDownList("Model.intSearchLeaveYearTypeId", Model.LeaveYearType, "...All...", new { @class = "selectBoxRegular" })%>
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
                        Leave Year Type
                    </th>
                    <th>
                        Leave Year
                    </th>
                    <th>
                        Start Date
                    </th>
                    <th>
                        End Date
                    </th>
                    <th>
                        Active Year
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveYear, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.LeaveYear ly in Model.LstLeaveYearPaging)
                   {                
                %>
                <tr>
                    <td>
                        <%=Html.Encode(ly.LeaveYearTypeName.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(ly.strYearTitle.ToString())%>
                    </td>
                    <td>
                        <%=Html.Encode(ly.strStartDate)%>
                    </td>
                    <td>
                        <%=Html.Encode(ly.strEndDate)%>
                    </td>
                    <td>
                        <%=Html.Encode(ly.bitIsActiveYear == true ? "Yes" : "No")%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= ly.intLeaveYearID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstLeaveYearPaging.PageNumber, ViewData.Model.LstLeaveYearPaging.TotalItemCount, "frmList", "/LMS/LeaveYear/LeaveYear", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveYearPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstLeaveYear.Count.ToString() %></label>
</div>
</form>
<div id="divStyleLeaveYear">
    <iframe id="styleOpener" src="" width="99%" height="95%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
