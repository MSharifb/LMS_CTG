<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MISCApprovalPathModels>" %>
<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>

<script type="text/javascript">

    $(document).ready(function () {

       

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
        , showOn: 'button'
        , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
        , buttonImageOnly: true
        });

        $("#divDialog").dialog({ autoOpen: false, modal: true, height: 560, width: 900, resizable: false, title: 'Miscellaneous', beforeclose: function (event, ui) { Closing(); }
        });

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        $('#Model_EmployeeName').autocomplete('<%=Url.Action("LookUps", "Employee") %>', {
            dataType: 'json',
            parse: function (data) {
                var rows = new Array();
                for (var i = 0; i < data.length; i++) {
                    rows[i] = { data: data[i], value: data[i].strEmpID, result: data[i].strEmpName.trim() };

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


                $("#StrEmpID").val(data.strEmpID);

                return false;
            }

        });

        $(".btnDownArrow").hide();
        $(".hide").hide();

    });

    function Closing() {
    }

    function OpenApprovalFlow(id) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MISCAPPROVAL/Detail/' + id;
        $('#styleOpenerApprovalFlow').attr({ src: url });
        $("#divDialog").dialog('open');
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

    function searchApprovalData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/MISCApproval/ApprovalSearch";
        var form = $("#frmApprovalList");
        var serializedForm = form.serialize();

        var strEmpID = $("#StrEmpID").val();
        var strDate = $("#StrDate").val();
        var strName = $("#Model_EmployeeName").val()

        $.post(url, {  StrEmpID: strEmpID, StrName: strName, StrDate: strDate }, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }


    function ShowAll(control) {
        $(control).hide();
        var cssClass = $(control).attr('class');
        cssClass = cssClass.replace('btnRightArrow', '');

        $("." + cssClass).show();
        $("." + cssClass + " " + "btnRightArrow").show();
        $(control).hide();

    }

    function HideAll(control) {

        var cssClass = $(control).attr('class');
        cssClass = cssClass.replace('btnDownArrow', '');
        cssClass = $.trim(cssClass);

        $("." + cssClass).each(function () {
            var dwnCss = $(this).attr('class');
            if (dwnCss.search('btnRightArrow') > 0) {
                $(this).show();
            }
            else
                $(this).hide();
        });


    }


    function setData(id, name) {

        document.getElementById('Model_EmployeeName').value = name;
        document.getElementById('StrEmpID').value = id;
        $("#divEmpList").dialog('close');
    }
 
</script>

<form id="frmApprovalList" method="post" action="">

<%= Html.HiddenFor(m=> m.StrEmpID) %>

<table width="100%">
    
    <tr>
        <td>
            Employee
            <%= Html.TextBox("Model.EmployeeName", Model.EmployeeName, new { @class = "textRegCustomWidth", @Style = "width:250px", @readonly = "true" })%>
            
            <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
        </td>
        <td>
             <%= Html.TextBoxFor(m => m.StrDate, new { @class = "textRegularDate dtPicker date" })%>               
        </td>
        <td>
            <a href="#" class="btnSearchData" onclick="return searchApprovalData();"></a>
        </td>
    </tr>
</table>


<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
    <table width="100%" id="MISCtable">
            <thead>
                <tr>
                    <th>
                    </th>
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
                </tr>
            </thead>
            <tbody>
                      <% string approvalStatus = ""; %>
                   <% foreach (MISCApproval item in Model.LstMISCApproval)
                        {%>

                  

                    <%
                        string strLabel = "";

                        if (item.APPROVALSTATUS == "1")
                            strLabel = "Recommend Pending";

                        else if (item.APPROVALSTATUS == "2")
                            strLabel = "Approval Pending";

                        else if (item.APPROVALSTATUS == "3")
                            strLabel = "Reverify Pending";

                        else if (item.APPROVALSTATUS == "4")
                            strLabel = "Approved";
                           %>

                    <% if (approvalStatus != item.APPROVALSTATUS) %>
                        <%{
                            approvalStatus = item.APPROVALSTATUS.ToString();
                        %>
                            <tr>
                                <td colspan="5">
                                  <div style="width:100%;float:left">
                                        <div style="width:5%;float:left">
                                             <a href="#" id="hideGetOut"  onclick="HideAll(this);" class='<%= item.APPROVALSTATUS.ToString() %> btnDownArrow'></a> 
                                            <a href="#" id="showAll"  onclick="ShowAll(this);" class='<%= item.APPROVALSTATUS.ToString() %> btnRightArrow'></a> 
                                        </div>
                                        <div style="width:85%;float:left;padding-left:5px">
                                            <b> <%= Html.Label(strLabel)%> </b>
                                        </div>
                                   </div>
                                </td>
                            </tr>

                        <%} %>
                        <tr  class='<%= item.APPROVALSTATUS.ToString() %> hide' >
                            <td>
                                <a href="#" class="gridEdit" onclick="return OpenApprovalFlow('<%=item.INTAPPLICATIONID%>');"></a>
                            </td>                            
                            <td>
                                <%=Html.Encode(item.EMPLOYEENAME)%>
                            </td>
                            <td>
                               <%= Html.Encode(item.STRDESIGNATION) %>
                            </td>
                            <td>
                                <%= Html.Encode(item.STRDEPARTMENT) %>
                            </td>
                            <td>
                                <%=Html.Label(item.MISCDATE.ToString("dd-MMM-yyyy"))%>
                            </td>                          
                   
                        </tr>
                    <% } %>
                </tbody>

    </table>
    </div>

    <div class="pager">
      <%= Html.PagerWithScript(ViewData.Model.LstMISCApprovalPaging.PageSize, ViewData.Model.LstMISCApprovalPaging.PageNumber, ViewData.Model.numTotalRows, "frmApprovalList", "/LMS/MISCApproval/ApprovalListPaging", "divDataList")%>
      <%= Html.Hidden("txtPageNo", ViewData.Model.LstMISCApprovalPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString() %></label>

    </div>


    <div id="divDialog">
    <iframe id="styleOpenerApprovalFlow" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

  </form>
