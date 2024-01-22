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

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
        , showOn: 'button'
        , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
        , buttonImageOnly: true
        });

        $("#divapro").dialog({ autoOpen: false, modal: true, height: 560, width: 900, resizable: false, title: 'Out of Office Assistant', beforeclose: function (event, ui) { Closing(); }
        });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        preventSubmitOnEnter($("#frmOutOfOffice"));

        $("#showGetIn").hide();
        $("#showGetOut").hide();
        $("#dvEmpID").hide();

        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        initialize();


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


    function initialize() {

        $(".hide").hide();
        $(".btnDownArrow").hide();
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
        document.getElementById('Model_StrEmpID').value = id;
        $("#divEmpList").dialog('close');
    }

   

    function closeDialogBox1() {

      
       $("#divapro").dialog('close');
       searchDataN();

    }

    function popupOutOfOfficeAdd() {



        var host = window.location.host;
        var url = 'http://' + host + '/LMS/OOAApprovalProcess/Details';
        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');

        return false;
    }

    function OpenApprovalFlow(id) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/OOAApprovalProcess/Details/' + id;
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

        var url = "/LMS/OOAApprovalProcess/search";
        var form = $("#frmOutOfOfficeApproval");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }

       
</script>


<form id="frmOutOfOfficeApproval" method="post" action="">


<table width="100%">     
    <tr>
        <td>            
            Employee <%= Html.TextBox("Model.EmployeeName", Model.EmployeeName, new { @class = "textRegCustomWidth", @Style = "width:250px" })%>
             <div id="dvEmpID">                
                <%= Html.TextBox("Model.StrEmpID", Model.StrEmpID, new { @class = "textRegular" })%>
             </div>
            <a href="#" class="btnSearch" onclick="return openEmployee();"></a>

        </td>
        <td>
           Date <%= Html.TextBox("Model.FromDate", Model.FromDate, new { @class = "textRegularDate dtPicker date" }) %>
        </td> 
        <td>
            <a href="#" class="btnSearchData" onclick="return searchDataN();" ></a>
        </td>       
    </tr>

   
</table>


<div id="grid">
    <div  id="grid-data" style="overflow: auto; width: 99%">
            <table width="100%" id="link-table">
                <colgroup>      
                    <col width="20px" />              
                    <col width="150px" />
                    <col />
                    <col />
                    <col />
                    <col />
                    <col />
                    <col />
                </colgroup>
                <thead>
                    <tr>                      
                        <th>
                        </th>
                         <th>
                            Employee
                        </th>
                        <th>
                            Reason
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
                        <th>
                            Status
                        </th>
                    </tr>
                </thead>

                <tbody>

                

        <% string approvalStatus = ""; foreach (OutOfOffice item in Model.LstOutOfOffice)
                {%>
                
                <% 
                    string strStatus = "";
                    string statusLabel = "";
                    int retVal = 0;
                                       
                    retVal = Model.GetAuthorTypeID(LMS.Web.LoginInfo.Current.strEmpID, item.STREMPID);
                    strStatus = retVal.ToString();
                    
                    if (retVal == 1)
                    {
                        statusLabel = Model.GetLabelStatus(item.APPROVALSTATUS,item.ID.ToString());                        
                    }

                    else if (retVal == 2)
                    {
                        if (item.APPROVALSTATUS == 0)
                            statusLabel = "Recommendation Pending";
                        else    
                            statusLabel = Model.GetLabelStatus(item.APPROVALSTATUS, item.ID.ToString());
                    }

                    else if (retVal == 3)
                    {
                        if (item.APPROVALSTATUS == 0)
                            statusLabel = "Approval Pending";
                        else
                            statusLabel = Model.GetLabelStatus(item.APPROVALSTATUS, item.ID.ToString());                        
                    }

                    else if (retVal == 4)
                    {

                    }


                    if (item.APPROVALSTATUS == null)
                    {
                        strStatus = "0";
                    }
                    else
                        strStatus = item.APPROVALSTATUS.ToString();

                   
                    if (approvalStatus != strStatus)
                    {
                        approvalStatus = item.APPROVALSTATUS.ToString();
                    %>
                    <tr>
                        <td colspan="7">
                            <div style="width:100%;float:left">
                                <div style="width:5%;float:left">
                                     <a href="#" id="hideGetOut"  onclick="HideAll(this);" class='<%= item.APPROVALSTATUS.ToString() %> btnDownArrow'></a> 
                                    <a href="#" id="showAll"  onclick="ShowAll(this);" class='<%= item.APPROVALSTATUS.ToString() %> btnRightArrow'></a> 
                                </div>
                                <div style="width:85%;float:left;padding-left:5px">
                                    <b> <%= Html.Label(statusLabel)%> </b>
                                </div>
                            </div>
                           
                          
                        </td>                        
                    </tr>
                <%} %> 

                <tr  class='<%= item.APPROVALSTATUS.ToString() %> hide' >               
                    <td> 
                        <a href="#" class="gridEdit" onclick="return OpenApprovalFlow('<%=item.ID%>');"></a>
                    </td>            
                    <td>
                        <a href="#" onclick="return OpenApprovalFlow('<%=item.ID%>');">
                        </a>
                      
                        <%= Html.Label(item.EMPNAME) %>
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
                    </td>
                    <td>
                        <%
                            string Status = "";
               
                            if (strStatus == "0")
                                Status = "Yet to permit";
                            else if (strStatus == "1")
                                Status = "Permitted";
                            else if (strStatus == "2" && item.ISVARIFIED == true)
                                Status = "Verified";
                            else if (strStatus == "3")
                                Status = "Approved";
                            else if (strStatus == "4")
                                Status = "Recommended ";
                                                       
                         %>
                         <%= Html.Label(Status) %>
                    </td>
                </tr>

                <%}  %>
                                    
                </tbody>
            </table>
           
           <div class="pager">
                <%= Html.PagerWithScript(ViewData.Model.LstOutOfOfficePaging.PageSize, ViewData.Model.LstOutOfOfficePaging.PageNumber, ViewData.Model.numTotalRows, "frmOutOfOfficeApproval", "/LMS/OOAApprovalProcess/search", "divDataList")%>
                <%= Html.Hidden("txtPageNo", ViewData.Model.LstOutOfOfficePaging.PageNumber)%>
            </div>
            <label id="lblTotalRows">
                Total Records:<%=Model.numTotalRows.ToString() %>
            </label>

    </div>
</div>


<div id="divapro">
    <iframe id="styleOpenerApprovalFlow" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>



</form>
