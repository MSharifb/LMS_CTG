<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MISCModels>" %>
<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<%@ Import Namespace="MvcContrib" %>
<%--<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>--%>

<script type="text/javascript">

    $(document).ready(function () {

        $(".dtPicker1").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
        , showOn: 'button'
        , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
        , buttonImageOnly: true
        });

        $("#divNew").dialog({ autoOpen: false, modal: true, height: 590, width: 930, resizable: false, title: 'Miscellaneous', beforeclose: function (event, ui) { Closing(); } });

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
    });

    function dialogOpen() {
        $("#styleNewEntry").dialog({ autoOpen: false, modal: true, height: 480, width: 720, resizable: false, title: 'MISC List',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/MISC/MISCLIST?page=' + pg;
                var form = $('#frmMiscList');
                var serializedForm = form.serialize();
                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); },
                "html");
            }
        });
    }


    function Closing()
    { }


    function setData(id, name) {

            $("#MISCMaster_STREMPID").val(id);
            $("#MISCMaster_EmpName").val(name);
           // GetInfo(id);

            $("#divEmpList").dialog('close');
        }


        function openEmployee() {


            var url = '/LMS/Employee/EmployeeList';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'text',
                timeout: 5000,
                error: function () {
                    alert('System is unable to load data please try again.');
                },
                success: function (result) {
                    $('#divEmpList').html(result);
                }
            });

            $("#divEmpList").dialog('open');
            return false;
        }


        function searchConvData() {

            var targetDiv = "#divDataList";
            var url = "/LMS/MISC/searchData";
            var form = $("#frmMiscList");
            var serializedForm = form.serialize();

           // var isPaid = $("#Model_IsPaid").val();
            var strEmpID = $("#MISCMaster_STREMPID").val();
            var strDate = $("#MISCMaster_strMISCDATE").val();
            var strToDate = $("#MISCMaster_strMISCToDATE").val();
            var strName = $("#MISCMaster_EmpName").val()

            $.post(url, { StrEmpID: strEmpID, StrName: strName, StrDate: strDate, strToDate: strToDate }, function (result) { $(targetDiv).html(result); }, "html");

            return false;
        }

        function popupStyleDetails(Id) {
            var host = window.location.host;
            var url = 'http://' + host + '/LMS/MISC/Details/' + Id;
            $('#styleNewEntry').attr({ src: url });
            $("#divNew").dialog('open');
            return false;
        }
  

    function popupMisc() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MISC/MISC';
        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');

        return false;
    }
</script>

<form id="frmMiscList" method="post" action="">
 <%= Html.HiddenFor(m => m.MISCMaster.STREMPID)%> 

 <table width="100%">
    <tr>
        <td>
           Employee Name
        </td>
        <td colspan="3">
            <%= Html.TextBoxFor(m => m.MISCMaster.EmpName, new { @class = "textRegular", @readonly = "true" })%>            
            <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
        </td>
        </tr>
        <tr>
        <td>
                Date From
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.MISCMaster.strMISCDATE, new { @class = "textRegularDate dtPicker1 date" })%>
                To
                <%=Html.TextBoxFor(m => m.MISCMaster.strMISCToDATE, new { @class = "textRegularDate dtPicker1 date" })%>

            </td>
            <td>
                <a href="#" class="btnSearchData" onclick="return searchConvData();"></a>
            </td>
    </tr>
    </table>

<table width="100%">
    <tr>
        <td colspan="3">
            <a href="#" class="btnAdd" onclick="return popupMisc();"></a>
        </td>
        <%--<td align="right">
            <a href="#" class="btnSearchData" onclick="return searchConvData();"></a>
        </td>--%>
    </tr>

</table>
<div id="divNew">
    <iframe id="styleNewEntry" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
    <table width="100%" id="MISCtable">
            <thead>
                <tr>
                    <th>
                        Employee Name
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        Department
                    </th>
                    <th>
                        Date
                    </th>
                    <th>
                        Edit
                    </th>
                </tr>
            </thead>
            <tbody>
                   <% foreach (MISCMaster item in Model.LstMISCMaster)
                        {%>
                        <tr>
                            <td>
                                <%=Html.Encode(item.EmpName)%>
                            </td>
                            <td>
                                <%=
                                    Html.Encode(item.StrDesignation)
                                    %>
                            </td>
                            <td>
                                <%=Html.Encode(item.Strdepartment)%>
                            </td>
                            <td>
                                <%=Html.Encode(item.MISCDATE.ToString("dd-MMM-yyyy"))%>
                            </td>
                            <td style="width: 10%;">                               
                                <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= item.MISCMASTERID %>);'>
                                </a>
                            </td>
                        </tr>
                    <% } %>
                </tbody>

    </table>
    </div>

    <div class="pager">
      <%= Html.PagerWithScript(ViewData.Model.LstMISCMasterPaging.PageSize, ViewData.Model.LstMISCMasterPaging.PageNumber, ViewData.Model.numTotalRows, "frmMiscList", "/LMS/MISC/MISCLIST", "divDataList")%>
      <%= Html.Hidden("txtPageNo", ViewData.Model.LstMISCMasterPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString()%></label>

    </div>

</form>