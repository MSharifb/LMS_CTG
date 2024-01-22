<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OfficeTimeModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmOfficeTime"));

        $("#divStyleOfficeTime").dialog({ autoOpen: false, modal: true, height: 480, width: 720, resizable: false, title: 'Office Hour',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/OfficeTime/OfficeTime?page=' + pg;
                var form = $('#frmOfficeTime');
                var serializedForm = form.serialize();                
                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }

        });

    });

    function Closing() {
        //window.location = "/OfficeTime";
    }

    function selectView(Id) {
        window.location = "/LMS/OfficeTime/Detail/" + Id;

        return false;
    }



    function deleteOfficeTime(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intLeaveYearID: Id }, '/LMS/OfficeTime/Delete', 'divDataList');
        }
        return false;
    }

    function popupStyleDetails(Id) {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/OfficeTime/Details/' + Id;
        $('#styleOpenerOfficeTime').attr('src', url);
        $("#divStyleOfficeTime").dialog('open');
        return false;
    }
        
    function popupStyleAdd() {
        var host = window.location.host;
        var protocol = window.location.protocol;
        var url = protocol + '//' + host + '/LMS/OfficeTime/OfficeTimeAdd';
        $('#styleOpenerOfficeTime').attr('src', url);
        $("#divStyleOfficeTime").dialog('open');
        return false;
    }

</script>

<h3 class="page-title">Office Hour</h3>

<form id="frmOfficeTime" method="post" action="">
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfficeTime, LMS.Web.Permission.MenuOperation.Add))
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
                        Leave Year
                    </th>
                    <th>
                        Working Hour
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfficeTime, LMS.Web.Permission.MenuOperation.Edit))
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
                <% foreach (LMSEntity.OfficeTime ly in Model.LstOfficeTimePaging)
                   { 
                %>
                <tr>
                    <td>
                        <%=Html.Encode(ly.strYearTitle.ToString())%>
                    </td>
                    <td>
                        <%=ly.fltDuration.ToString()%>
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
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstOfficeTimePaging.PageNumber, ViewData.Model.LstOfficeTimePaging.TotalItemCount, "frmOfficeTime", "/LMS/OfficeTime/OfficeTime", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstOfficeTimePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstOfficeTime.Count.ToString() %></label>
</div>
</form>
<div id="divStyleOfficeTime">
    <iframe id="styleOpenerOfficeTime" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
