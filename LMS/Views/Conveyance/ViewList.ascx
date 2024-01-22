<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ConveyanceModels>" %>

<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>

<script type="text/javascript">

    $(document).ready(function () {

        $("#tabs").tabs();
        $(".dtPicker1").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
        , showOn: 'button'
        , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
        , buttonImageOnly: true
        });

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        $("#divConveyance").dialog({ autoOpen: false, modal: true, height: 460, width: 700, resizable: false, title: 'Conveyance Details', beforeclose: function (event, ui) { Closing(); } });
        preventSubmitOnEnter($("#frmConveyance"));

        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        $("#divEmpID").hide();

    });

    function Closing() {
    }

    function ShowDetails(id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/Conveyance/ConveyanceDetails/' + id;
        $('#styleOpenerConveyance').attr({ src: url });
        $("#divConveyance").dialog('open');
        return false;
    }


    function setData(id, name) {
        alert('from view list');
        document.getElementById('ConveyanceObj_STREMPNAME').value = name;
        document.getElementById('ConveyanceObj_STREMPID').value = id;
        $("#divEmpList").dialog('close');
    }

//    $("#datepicker").datepicker({
//        changeMonth: true,
//        changeYear: false
//    }).click(function () { $('#ConveyanceObj_STRDATE').datepicker('show'); });


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

        var targetDiv = "#tabs-2";
        var url = "/LMS/Conveyance/search";
        var form = $("#frmConveyance");
        var serializedForm = form.serialize();

        var strEmpID = $("#ConveyanceObj_STREMPID").val();
        var strDate = $("#ConveyanceObj_STRDATE").val();
        var strName = $("#ConveyanceObj_STREMPNAME").val()

        $.post(url, {StrEmpID:strEmpID,StrName:strName,StrDate:strDate}, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
 
</script>



<form id="frmConveyance" method="post" action="">

    <table>
        <tr>
            <td>
                Employee
            </td>
            <td>
                <div id="divEmpID">
                    <%= Html.TextBoxFor(m => m.ConveyanceObj.STREMPID, new { @class = "textRegular" })%>
                </div>
                    <%= Html.TextBoxFor(m => m.ConveyanceObj.STREMPNAME, new { @class = "textRegular" })%>
                  <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
            </td>
            <td>
                Date
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.ConveyanceObj.STRDATE, new { @class = "textRegularDate dtPicker1 date" })%>
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>      
                <%--<%=Html.TextBox("txtOutOfficeDate","", new { @class = "textRegularDate dtPicker date" })%>--%>
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
                        <th>
                            Employee            
                        </th>
                        <th>
                            Out of Office Date
                        </th>
                    </tr>
                </thead>

                <tbody>
                    <%  foreach (ConveyanceMaster item in Model.LstConveyanceMaster ){%>
                        <tr>
                            <td>
                                <a href="#" onclick="return ShowDetails('<%= item.RECORDID %>')"></a>
                                <%= Html.Label(item.EMPNAME)%>
                            </td>
                            <td>
                                <%= Html.Label(item.OUTOFOFFICEDATE.ToString("dd/MM/yyyy"))%> at 
                                <%= Html.Label(item.OUTOFOFFICETIME) %>
                            </td>
                        </tr>
                    <% } %>
                                    
                </tbody>
            </table>   
            
              <div class="pager">
                <%= Html.PagerWithScript(ViewData.Model.LstConveyanceMasterPaging.PageSize, ViewData.Model.LstConveyanceMasterPaging.PageNumber, ViewData.Model.numTotalRows, "frmConveyance", "/LMS/Conveyance/ConveyanceViewPaging", "divDataList")%>
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


