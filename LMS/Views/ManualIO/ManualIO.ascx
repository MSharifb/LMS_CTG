<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ManualIOModels>" %>
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
//        }).click(function () { $('#strAttendDateFrom').datepicker('show'); });


//        $("#datepicker1").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#strAttendDateTo').datepicker('show'); });

        preventSubmitOnEnter($("#frmManualIO"));

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#divStyleManualIO").dialog({ autoOpen: false, modal: true, height: 450, width: 900, resizable: false, title: 'Manual In / Out',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/ManualIO/ManualIO?page=' + pg;
                var form = $('#frmManualIO');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });


    function Closing() {
        //window.location = "/LMS/ManualIO";    
    }

    function deleteManualIO(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intManualIOID: Id }, '/LMS/ManualIO/Delete', 'divManualIOList');
        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ManualIO/Details/' + Id;
        $('#styleOpenerManualIO').attr({ src: url });
        $("#divStyleManualIO").dialog('open');
        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/ManualIO/ManualIOAdd';
        $('#styleOpenerManualIO').attr({ src: url });
        $("#divStyleManualIO").dialog('open');
        return false;
    }


    function searchData() {

        var pdtAPFrom = $('#strAttendDateFrom').val();
        var pdtAPTo = $('#strAttendDateTo').val();
        var hookup = true;

        if (fnValidate() == true) {

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
                var url = '/LMS/ManualIO/ManualIO';
                var form = $('#frmManualIO');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) {
                    $(targetDiv).html(result);
                }, "html");
            }
        }
        return false;
    }

    function checkDateValidation(pdtAPFrom, pdtAPTo) {

        if (fnValidateDateTime() == false) {
            alert("Invalid Date.");
            return false;
        }
        if (pdtAPFrom != '' && pdtAPTo != '') {
            if (pdtAPFrom > pdtAPTo) {
                alert("From Date must be smaller than or equal to 'To Date'.");
                return false;
            }
        }

        return true;
    }




</script>
<form id="frmManualIO" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td style="width:100px"  >
                Employee ID
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.strEmpID , new { @class = "textRegular", @maxlength = 100 })%>
            </td>
            <td style="width:90px">
                Name
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.ManualIO.strEmpName , new { @class = "textRegular", @maxlength = 100 })%>
            </td>
        </tr>
        
        <tr>
            <td>
                From Date
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.strAttendDateFrom, new { @class = "textRegularDate dtPicker dateNR" })%>
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td>
                To Date
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.strAttendDateTo, new { @class = "textRegularDate dtPicker dateNR" })%>
                <%--<img alt="" id="datepicker1" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>      
            </td>
        </tr>
        <tr>
            <td>
                Shift Name
            </td>
            <td style="width: 45%;" class="contenttabletd">
                <%= Html.DropDownListFor(m => m.ManualIO.intShiftID, Model.Shift, "...Select One..", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
            <td colspan="2" style="text-align: left">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>
    </table>
</div>
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ManualIO, LMS.Web.Permission.MenuOperation.Add))
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
                        Employee ID
                    </th>
                    <th>
                        Card No.
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Attn Date & Time
                    </th>
                    <th>
                        Entry Type
                    </th>
                    <th>
                        Reason
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ManualIO, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.ManualIO obj in Model.LstManualIOPaged)
                   {
                   
                %>
                <tr>
                    <td style="text-align:center">
                        <%=Html.Encode(obj.strEmpID )%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strCardID )%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strEmpName)%>
                    </td>
                    <td style="text-align:center">
                        <%=Html.Encode(obj.dtAttendDateTime.ToString("dd-MMM-yyyy HH:mm tt"))%>
                        
                    </td>
                    <td style="text-align:center">
                        <%=Html.Encode(obj.strEntryType)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strReason)%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intRowID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstManualIOPaged.PageNumber, ViewData.Model.LstManualIOPaged.TotalItemCount, "frmManualIO", "/LMS/ManualIO/ManualIO", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstManualIOPaged.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstManualIO.Count.ToString()%></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleManualIO">
    <iframe id="styleOpenerManualIO" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
