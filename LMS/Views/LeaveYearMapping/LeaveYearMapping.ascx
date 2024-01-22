<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveYearMappingModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveYearMapping"));

        $("#divStyleLeaveType").dialog({ autoOpen: false, modal: true, height: 380, width: 720, resizable: false, title: 'Leave Year Mapping',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/LeaveYearMapping/LeaveYearMapping?page=' + pg;
                var form = $('#frmLeaveYearMapping');
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

            executeCustomAction({ intLeaveTypeID: Id }, '/LMS/LeaveYearMapping/Delete', 'divLeaveTypeList');
        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/LeaveYearMapping/Details/' + Id;
        $('#styleOpenerLeaveType').attr({ src: url });
        $("#divStyleLeaveType").dialog('open');
        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/LeaveYearMapping/LeaveYearMappingAdd';
        $('#styleOpenerLeaveType').attr({ src: url });
        $("#divStyleLeaveType").dialog('open');
        return false;
    }

    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/LeaveYearMapping/LeaveYearMapping";
        var form = $("#frmLeaveYearMapping");
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
<form id="frmLeaveYearMapping" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                Leave Type
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveYearMapping.intLeaveTypeID, Model.LeaveTypeList, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td>
                Leave Year
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveYearMapping.intLeaveYearId, Model.LeaveYears, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveYearMapping, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td align="right" style="width: 80%;">
                <%=Html.CheckBoxFor(m => m.LeaveYearMapping.bitIsActiveYear)%>&nbsp;Active Year
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
                        Leave Year
                    </th>
                    <th>
                        Leave Year Type
                    </th>
                    <th>
                        Active Status
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveYearMapping, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.LeaveYearMapping obj in Model.LstLeaveYearMappingPaging)
                   {
                   
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.strLeaveType)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strYearTitle)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.LeaveYearTypeName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.bitIsActiveYear == true ? "Yes" : "No")%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intLeaveYearMapID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstLeaveYearMappingPaging.PageNumber, ViewData.Model.LstLeaveYearMappingPaging.TotalItemCount, "frmLeaveYearMapping", "/LMS/LeaveYearMapping/LeaveYearMapping", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveYearMappingPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstLeaveYearMapping.Count.ToString()%></label>
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
