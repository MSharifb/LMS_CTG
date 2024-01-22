<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OutOfOfficeModels>" %>
<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">



    $(document).ready(function () {

        $("#divapro").dialog({ autoOpen: false, modal: true, height: 590, width: 930, resizable: false, title: 'Out of Office Assistant', beforeclose: function (event, ui) { Closing(); } });
        $("#divNew").dialog({ autoOpen: false, modal: true, height: 590, width: 930, resizable: false, title: 'Out of Office Assistant', beforeclose: function (event, ui) { Closing(); } });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        preventSubmitOnEnter($("#frmOutOfOffice"));

        $("#showGetIn").hide();
        $("#showGetOut").hide();
        $("#showGetOutDraft").hide();
        $("#dvEmpID").hide();

        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });


        $('#Model_EmployeeName').autocomplete('<%=Url.Action("LookUps", "Employee") %>', {
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

        $("#Model_EmployeeName").result(function (event, data, formatted) {
            if (data) {


                $("#Model_StrEmpID").val(data.strEmpID);

                return false;
            }

        });

    });

    function setData(id, name) {

        document.getElementById('Model_EmployeeName').value = name;
        document.getElementById('Model_StrEmpID').value = id;
        $("#divEmpList").dialog('close');
    }

    function closeDialogBox1() {
            
       // $("#divNew").dialog('close');
        searchDataN();

    }


     
    
    function popupOutOfOfficeAdd() {



        var host = window.location.host;
        var url = 'http://' + host + '/LMS/OutOfOfficeDetails/OutOfOfficeAdd';
        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');

        return false;
    }

    function OpenApprovalFlow(id) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/OutOfOfficeDetails/OutOfOfficeDetailsData/' + id;
        $('#styleOpenerApprovalFlow').attr({ src: url });
        $("#divapro").dialog('open');
        return false;

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


 

    function Hide(control, type) {

        $("." + type).each(function () {
            $(this).hide();
        });

        $("#" + "show" + type).show();
        $(control).hide();
    }

    function Show(control, type) {

        $("." + type).each(function () {
            $(this).show();
        });

        $("#" + "hide" + type).show();
        $(control).hide();
    }

    function Closing() {

    }

    function searchDataN() {

        var targetDiv = "#divDataList";

        var url = "/LMS/OutOfOfficeDetails/search";
        var form = $("#frmOutOfOffice");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }

</script>
<form id="frmOutOfOffice" method="post" action="">

<table width="100%">
    <tr>
        <td colspan="3">
            <a href="#" class="btnNewGetOut" onclick="return popupOutOfOfficeAdd();"></a>
        </td>
    </tr>
    <tr>
        <td>
            Employee
            <%= Html.TextBox("Model.EmployeeName", Model.EmployeeName, new { @class = "textRegCustomWidth", @Style = "width:250px" })%>
            <div id="dvEmpID">
                <%= Html.TextBox("Model.StrEmpID", Model.StrEmpID, new { @class = "textRegular" })%>
            </div>
            <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
        </td>
        <td>
            Purpose
            <%= Html.DropDownList("Model.Purpose", new SelectList((IEnumerable)Model.GetPurpose, "Value", "Text", Model.Purpose), "Select One", new { @class = "ddlRegularDate" })%>
        </td>
        <td>
            <a href="#" class="btnSearchData" onclick="return searchDataN();"></a>
        </td>
    </tr>
</table>
<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
        <table width="100%" id="link-table">
            <colgroup>
                <col width="20px" />
            </colgroup>
            <thead>
                <tr>
                    <th></th>
                    <th>
                        Employee
                    </th>
                    <th>
                        Reason for going
                    </th>
                    <th>
                        Out
                    </th>
                    <th>
                        Exp In
                    </th>
                    <th>
                        IN
                    </th>
                    <th>
                        Address/Location
                    </th>
                </tr>
            </thead>
            <tbody>
                <% string isGetin = ""; foreach (OutOfOffice item in Model.LstOutOfOffice)
                   {%>
                <% if (isGetin != item.STATUS)
                   {
                       isGetin = item.STATUS;
                %>
                <tr>
                    <td colspan="7">
                        <%if (item.STATUS == "GI") %>
                        <% {%>
                        <div style="width: 70px">
                            <div style="width: 20px; float: left">
                                <a href="#" id="hideGetIn" class="btnDownArrow" onclick="return Hide(this,'GetIn');">
                                </a><a href="#" id="showGetIn" class="btnRightArrow" onclick="return Show(this,'GetIn');">
                                </a>
                            </div>
                            <div style="width: 50px; float: left; font-weight: bold">
                                IN
                            </div>
                        </div>
                        <% } %>
                        <%if (item.STATUS == "GO") %>
                        <% {%>
                        <div style="width: 70px">
                            <div style="width: 20px; float: left">
                                <a href="#" id="hideGetOut" class="btnDownArrow" onclick="return Hide(this,'GetOut');">
                                </a><a href="#" id="showGetOut" class="btnRightArrow" onclick="return Show(this,'GetOut');">
                                </a>
                            </div>
                            <div style="width: 50px; float: left; font-weight: bold">
                                OUT
                            </div>
                        </div>
                        <% } %>

                        <%if (item.STATUS == "GD") %>
                        <% {%>
                        <div style="width: 70px">
                            <div style="width: 20px; float: left">
                                <a href="#" id="hideGetOutDraft" class="btnDownArrow" onclick="return Hide(this,'GetOutDraft');">
                                </a><a href="#" id="showGetOutDraft" class="btnRightArrow" onclick="return Show(this,'GetOutDraft');">
                                </a>
                            </div>
                            <div style="width: 50px; float: left; font-weight: bold">
                               DRAFT
                            </div>
                        </div>
                        <% } %>
                    </td>
                </tr>
                <%} %>
                <%if (item.STATUS == "GI") %>
                <% {%>
                    <tr class="GetIn">
                <% } %>
                
                <%if (item.STATUS == "GO") %>
                   <% {%>
                    <tr class="GetOut">
                <% } %>

                <%if (item.STATUS == "GD") %>
                   <% {%>
                    <tr class="GetOutDraft">
                <% } %>
                        <%--  <td>
                        
                    </td>--%>
                         <td>
                            <a href="#" class="gridEdit" onclick="return OpenApprovalFlow('<%=item.ID%>');"></a>
                        </td>
                        <td>

                           <div style="width:100%;float:left;padding-left:10px">
                                <div style="width:10%;float:left">
                                     <%if (item.STATUS == "GO") %>
                                    <% {%>
                                        <a href="#" class="getOutImage">
                                        </a>
                                    <% }%>

                                    <% else if (item.STATUS == "GI") %>
                                    <% {%>
                                         <a href="#" class="getInImage">
                                        </a>
                                    <% }%>

                                    <% else if (item.STATUS == "GD") %>
                                    <% {%>
                                         <a href="#"  class="getOutImage">
                                            
                                        </a>
                                    <% }%>
                     
                                </div>
                                <div style="width:80%;float:left;padding-left:5px">
                                    <%= Html.Label(item.EMPNAME) %>
                                </div>
                            </div>
                        </td>
                       
                        <td>
                            <%= Html.Label(item.PURPOSE)%>
                        </td>
                       <td style="white-space:nowrap;">
                            <%= Html.Label(item.GETOUTDATE.ToString("dd-MMM-yyyy"))%><br />
                            <%= Html.Label(item.GETOUTTIME)%>
                        </td>
                        <td style="white-space:nowrap;">
                            <% if (item.EXPGETINDATE != DateTime.MinValue) %>
                            <%{ %>
                            <%= Html.Label(item.EXPGETINDATE.ToString("dd-MMM-yyyy"))%><br />
                            <%= Html.Label(item.EXPGETINTIME)%>
                            <%} %>
                        </td>
                  <td style="white-space:nowrap;">
                            <% if (item.GETINDATE != DateTime.MinValue) %>
                            <%{ %>
                            <%= Html.Label(item.GETINDATE.ToString("dd-MMM-yyyy"))%><br />
                            <%= Html.Label(item.GETINTIME)%>
                            <%} %>
                        </td>
                        <td>
                            <%= Html.Label(item.VISITLOCATION)%>
                            <%--  <%string location=""; foreach (OutOfOfficeLocaton obj in (Model.LstOutOfOfficeLocation.Where(c=>c.OUTOFOFFICEID == item.ID))
                               {
                                   location ="From " + obj.FROMLOCATION +" To "
                               } %>

                            <%= Html.Label(item.VISITLOCATION)%>--%>
                        </td>
                    </tr>
                    <%}  %>
                    <%--  <tr>                       
                        <td colspan="7"  id="grid-data" >
                            <table width="100%" id="GetIn">

                                
                                 
                            </table>
                        </td>
                                               
                    </tr>--%>
            </tbody>
        </table>
        <div class="pager">
            <%= Html.PagerWithScript(ViewData.Model.LstOutOfOfficePaging.PageSize, ViewData.Model.LstOutOfOfficePaging.PageNumber, ViewData.Model.numTotalRows, "frmOutOfOffice", "/LMS/OutOfOfficeDetails/OutOfOfficeDetails", "divDataList")%>
            <%= Html.Hidden("txtPageNo", ViewData.Model.LstOutOfOfficePaging.PageNumber)%>
        </div>
        <label id="lblTotalRows">
            Total Records:<%=Model.numTotalRows.ToString() %>
        </label>
    
</div>
<div id="divapro">
    <iframe id="styleOpenerApprovalFlow" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
<div id="divNew">
    <iframe id="styleNewEntry" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
</form>
