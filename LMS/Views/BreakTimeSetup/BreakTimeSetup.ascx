<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.BreakTimeSetupModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmBreakTimeSetup"));

        $("#divStyleBreakTimeSetup").dialog({ autoOpen: false, modal: true, height: 350, width: 720, resizable: false, title: 'Break Assignment to Shift',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/BreakTimeSetup/BreakTimeSetup?page=' + pg;
                var form = $('#frmBreakTimeSetup');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });


    function Closing() {
        //window.location = "/LMS/BreakTimeSetup";    
    }

    function deleteBreakTimeSetup(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intBreakTimeSetupID: Id }, '/LMS/BreakTimeSetup/Delete', 'divBreakTimeSetupList');
        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/BreakTimeSetup/Details/' + Id;
        $('#styleOpenerBreakTimeSetup').attr({ src: url });
        $("#divStyleBreakTimeSetup").dialog('open');
        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/BreakTimeSetup/BreakTimeSetupAdd';
        $('#styleOpenerBreakTimeSetup').attr({ src: url });
        $("#divStyleBreakTimeSetup").dialog('open');
        return false;
    }


</script>
<form id="frmBreakTimeSetup" method="post" action="">
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.BreakTimeSetup, LMS.Web.Permission.MenuOperation.Add))
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
                    <%-- <th>
                       Sl. No.
                    </th>--%>
                    <th>
                        Break Name
                    </th>
                    <th>
                        Shift Name
                    </th>
                    <th>
                        Start Time
                    </th>
                    <th>
                        End Time
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.BreakTimeSetup, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.ATT_tblSetBreakTime obj in Model.LstSetBreakTimePaged)
                   {
                   
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.strBreakName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strShiftName)%>
                    </td>
                    <td style="text-align: center">
                        <%=Html.Encode(obj.strStartTime)%>
                    </td>
                    <td style="text-align: center">
                        <%=Html.Encode(obj.strEndTime)%>
                    </td style="text-align:center">
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intBreakSetID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstSetBreakTimePaged.PageNumber, ViewData.Model.LstSetBreakTimePaged.TotalItemCount, "frmBreakTimeSetup", "/LMS/BreakTimeSetup/BreakTimeSetup", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstSetBreakTimePaged.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstBreakTimeSetup.Count.ToString()%></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleBreakTimeSetup">
    <iframe id="styleOpenerBreakTimeSetup" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
