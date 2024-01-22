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

        $("#divapro").dialog({ autoOpen: false, modal: true, height: 560, width: 900, resizable: false, title: 'Out of Office Assistant', beforeclose: function (event, ui) { Closing(); } });
        $("#divNew").dialog({ autoOpen: false, modal: true, height: 560, width: 900, resizable: false, title: 'Out of Office Assistant', beforeclose: function (event, ui) { Closing(); } });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        preventSubmitOnEnter($("#frmOutOfOfficeReport"));

        $("#dvEmpID").hide();
        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        initialize();

        $('#Model_EmployeeName').autocomplete('<%=Url.Action("LookUps", "Employee") %>', {
              dataType: 'json',
              parse: function(data) {
                  var rows = new Array();
                  for(var i=0; i<data.length; i++){
                      rows[i] = { data: data[i], value: data[i].strEmpID, result: data[i].strEmpName };
                    
                  }
                  return rows;
              },
              formatItem: function(row, i, n) {
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

//    $("#datepicker").datepicker({
//        changeMonth: true,
//        changeYear: false
//    }).click(function () { $('#Model_FromDate').datepicker('show'); });


//    $("#datepicker1").datepicker({
//        changeMonth: true,
//        changeYear: false
//    }).click(function () { $('#Model_ToDate').datepicker('show'); });

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
        cssClass = cssClass.replace('btnDownArrow','');       
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

    function clearData() {
        document.getElementById('Model_EmployeeName').value = "";
        document.getElementById('Model_StrEmpID').value = "";
        document.getElementById('Model_FromDate').value = "";
        document.getElementById('Model_ToDate').value = "";
    }
    function setData(id, name) {

        document.getElementById('Model_EmployeeName').value = name;
        document.getElementById('Model_StrEmpID').value = id;
        $("#divEmpList").dialog('close');
    }

    function closeDialog() {
        $("#divNew").dialog('close');
    }

    function popupOutOfOfficeAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/OutOfOfficeReport/OutOfOfficeAdd';
        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');
        return false;
    }

    function OpenApprovalFlow(id) {
           
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/OutOfOfficeReport/OutOfOfficeDetailsData/' + id;
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

       

    function Closing() {

    }

    function searchDataReport() {
       
        var targetDiv = "#divDataList";
        var url = "/LMS/OutOfOfficeReport/DetailsReports";
        var form = $("#frmOutOfOfficeReport");
        var serializedForm = form.serialize();
        var id = document.getElementById('Model_StrEmpID').value;
        var Name = document.getElementById('Model_EmployeeName').value;
        var FromDate = document.getElementById('Model_FromDate').value;
        var ToDate = document.getElementById('Model_ToDate').value;
        $.get(url, { strID: id, Name: Name, FromDate: FromDate, ToDate: ToDate }, function (result) { $(targetDiv).html(result); }, "html");
        
        return false;
    }

    

</script>


<form id="frmOutOfOfficeReport" method="post" action="">

<table >
    <tr>
        <td>            
            Employee Name
        
        </td>
        <td>
             <%= Html.TextBox("Model.EmployeeName", Model.EmployeeName, new { @class = "textRegCustomWidth", @Style = "width:250px" })%>
             <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
             <div id="dvEmpID">
                
                <%= Html.TextBox("Model.StrEmpID", Model.StrEmpID, new { @class = "textRegular", @style="Width:50px"})%>
             </div>
        </td>
               
    </tr>

    <tr>
        <td>
            From 
             
        </td>
        <td>
            <%=Html.TextBox("Model.FromDate", Model.FromDate, new { @class = "textRegularDate dtPicker date" })%>
            <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
        
            To <%=Html.TextBox("Model.ToDate", Model.ToDate, new { @class = "textRegularDate dtPicker date" })%>
            <%--<img alt="" id="datepicker1" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
        </td>
    </tr>

    <tr>
        <td></td>
       <td>
            <a href="#" class="btnSearchData" onclick="return searchDataReport();" ></a>
            <a href="#" class="btnCancel" onclick="return clearData();" ></a>
        </td>
        
    </tr>
</table>


<div id="grid">
    <div  id="grid-data" style="overflow: auto; width: 100%">
            <table width="100%" id="link-table">
                <colgroup>  
                    <col width="20px" />                  
                    <col width="180px" />
                    <col width="80px"/>
                    <col width="120px"/>
                    <col width="120px" />
                    <col width="120px"/>
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
                    </tr>
                </thead>

                <tbody>

        <% string isGetoutDate = ""; foreach (OutOfOffice item in Model.LstOutOfOffice)
                {%>

                <% 
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                    DateTime dtGetOut = new DateTime();
                    try
                    {
                        dtGetOut = DateTime.Parse(item.GETOUTDATE.ToString("dd/MM/yyyy"));
                        }
                    catch(Exception ex)
                    {
                    }       
                 %>
                <% if (isGetoutDate != item.GETOUTDATE.ToString("dd/MM/yyyy"))
                    {
                        isGetoutDate = item.GETOUTDATE.ToString("dd/MM/yyyy");
                    %>
                    <tr>
                        <td colspan="7">
                            <div style="width:100%;float:left">
                                <div style="width:5%;float:left">
                                     <a href="#" id="hideGetOut"  onclick="HideAll(this);" class='<%= dtGetOut.Ticks.ToString() %> btnDownArrow'></a> 
                                    <a href="#" id="showAll"  onclick="ShowAll(this);" class='<%= dtGetOut.Ticks.ToString() %> btnRightArrow'></a> 
                                </div>
                                <div style="width:85%;float:left;padding-left:5px">
                                    <b> <%= Html.Label(item.GETOUTDATE.ToString("dd/MM/yyyy"))%> </b>
                                </div>
                            </div>
                           
                          
                        </td>                        
                    </tr>
                <%} %>
                               
                   
                   <tr class='<%= dtGetOut.Ticks.ToString() %> hide' >
                    
                    <td>
                        <a href="#" class="gridEdit" onclick="return OpenApprovalFlow('<%=item.ID%>');"></a>
                    </td>             
                    <td>    
                        
                        <div style="width:100%;float:left;padding-left:10px">
                            <div style="width:10%;float:left">
                                 <%if (item.ISGETIN == false) %>
                                <% {%>
                                    <a href="#" class="getOutImage">
                                    </a>
                                <% }%>

                                <% else if (item.ISGETIN == true) %>
                                <% {%>
                                     <a href="#" class="getInImage">
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
                    <td>
                            
                        <%= Html.Label(item.GETOUTDATE.ToString("dd-MMM-yyyy"))%>
                            <%= Html.Label(item.GETOUTTIME)%>
                    </td>
                    <td>
                        <% if (item.EXPGETINDATE != DateTime.MinValue) %>
                        <%{ %>
                            <%= Html.Label(item.EXPGETINDATE.ToString("dd-MMM-yyyy"))%>
                            <%= Html.Label(item.EXPGETINTIME)%>
                        <%} %>
                    </td>
                    <td>
                        <% if (item.GETINDATE != DateTime.MinValue) %>
                        <%{ %>
                        <%= Html.Label(item.GETINDATE.ToString("dd-MMM-yyyy"))%>
                            <%= Html.Label(item.GETINTIME)%>
                            <%} %>
                    </td>
                    <td>
                            <%= Html.Label(item.VISITLOCATION)%>
                         
                    </td>
                </tr>
                <%}  %>
                    
                 
                    
                </tbody>
            </table>
            <div class="pager">
                <%= Html.PagerWithScript(ViewData.Model.LstOutOfOfficePaging.PageSize, ViewData.Model.LstOutOfOfficePaging.PageNumber, ViewData.Model.numTotalRows, "frmOutOfOfficeReport", "/LMS/OutOfOfficeReport/OutOfOfficeReportsPaging", "divDataList")%>
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

<div id="divNew">
    <iframe id="styleNewEntry" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</form>


