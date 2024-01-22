<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MyConveyanceModels>" %>
<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<%@ Import Namespace="LMSEntity" %>

<script type="text/javascript">

    $(document).ready(function () {

        $("#tabs").tabs();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
        , showOn: 'button'
        , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
        , buttonImageOnly: true
        });

        $("#divConveyance").dialog({ autoOpen: false, modal: true, height: 550, width: 800, resizable: false, title: 'Conveyance Details', beforeclose: function (event, ui) { Closing(); } });
        preventSubmitOnEnter($("#frmConveyance"));
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        $("#divEmpID1").hide();


        $('#ConveyanceObj_STREMPNAME').autocomplete('<%=Url.Action("LookUps", "Employee") %>', {
            dataType: 'json',
            parse: function (data) {
                var rows = new Array();
                for (var i = 0; i < data.length; i++) {
                    rows[i] = { data: data[i], value: data[i].strEmpID, result: data[i].strEmpName };

                }
                return rows;
            },
            formatItem: function (row, i, n) {
                return row.strEmpID + ' - ' + row.strEmpName;
            },
            width: 300,
            mustMatch: true,
            selectFirst: true
        });

        $("#ConveyanceObj_STREMPNAME").result(function (event, data, formatted) {
            if (data) {


                $("#ConveyanceObj_STREMPID").val(data.strEmpID);

                return false;
            }

        });

    });

    function Closing() {
    }

    function ShowDetails(id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MyConveyance/Details/' + id;
        $('#styleOpenerConveyance').attr({ src: url });
        $("#divConveyance").dialog('open');
        return false;
    }

    //    $("#datepicker").datepicker({
    //        changeMonth: true,
    //        changeYear: false
    //    }).click(function () { $('#ConveyanceObj_STRDATE').datepicker('show'); });

    function setData(id, name) {

        document.getElementById('ConveyanceObj_STREMPNAME').value = name;
        document.getElementById('ConveyanceObj_STREMPID').value = id;
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

        var targetDiv = "#tabs-1";
        var url = "/LMS/MyConveyance/searchData";
        var form = $("#frmMyConveyance");
        var serializedForm = form.serialize();
        var isPaid = $("#Model_IsPaid").val();       
        var strDate = $("#ConveyanceObj_STRDATE").val();

        $.post(url, { IsPaid: isPaid, StrDate: strDate }, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
 
</script>

<form id="frmMyConveyance" method="post" action="">
<%= Html.Hidden("Model.IsPaid", Model.IsPaid)%>
<%= Html.HiddenFor(m=> m.ConveyanceObj.STREMPID) %>

    <table>
        <tr>            
            <td>
                Date
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.ConveyanceObj.STRDATE, new { @class = "textRegularDate dtPicker date" })%>               
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td>
                <a href="#" class="btnSearchData" onclick="return searchConvData();"></a>
            </td>
        </tr>
    </table>

    <div id="grid">
        <div id="grid-data" style="overflow: auto; width: 99%">
            <table  width="100%" id="link-table">
                <thead>
                    <tr>
                        <th></th>
                        <th>
                            Employee            
                        </th>
                        <th>
                            Out of Office Date
                        </th>
                        <th>
                            Status
                        </th>
                    </tr>
                </thead>

                <tbody>
                    <% if(Model.LstConveyanceMaster != null) foreach (MyConveyanceMaster item in Model.LstConveyanceMaster){%>
                        <tr>
                            <td>
                                <a href="#" class="gridEdit" onclick="return ShowDetails('<%= item.OUTOFOFFICEID %>')"></a>
                            </td>
                            <td>
                                
                                <%= Html.Label(item.EMPNAME)%>
                                
                            </td>
                            <td>
                                <%= Html.Label(item.OUTOFOFFICEDATE.ToString("dd-MMM-yyyy"))%> at 
                                <%= Html.Label(item.OUTOFOFFICETIME) %>
                            </td>
                            <td>
                                <% if(Model.IsPaid == "0") {%>
                                <%= Html.Encode(item.STATUS) %>
                                <% }
                                   else
                                   {%>
                                Paid
                                <%} %>
                            </td>
                        </tr>
                    <%   } %>
                                    
                </tbody>
            </table> 
            
            <div class="pager">
                <%= Html.PagerWithScript(ViewData.Model.LstConveyanceMasterPaging.PageSize, ViewData.Model.LstConveyanceMasterPaging.PageNumber, ViewData.Model.numTotalRows, "frmMyConveyance", "/LMS/MyConveyance/MyConveyancePaging", "tabs-1")%>
                <%= Html.Hidden("txtPageNo", ViewData.Model.LstConveyanceMasterPaging.PageNumber)%>
            </div>
            <label id="lblTotalRows">
                Total Records:<%=Model.numTotalRows.ToString() %>
            </label>
               
        </div>
    </div>
        
          
 <div id="divConveyance">
    <iframe id="styleOpenerConveyance" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
  
</form>



