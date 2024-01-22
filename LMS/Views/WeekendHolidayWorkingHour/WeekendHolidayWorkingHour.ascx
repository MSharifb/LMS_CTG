<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.WeekendHolidayWorkingHourModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<style type="text/css">
    .style1
    {
        width: 277px;
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {

        $("#divNew").dialog({ autoOpen: false, modal: true, height: 500, width: 900, resizable: false, title: 'Working Hour for Weekend/Holiday', beforeclose: function (event, ui) { Closing(); } });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        preventSubmitOnEnter($("#frmWeekendHolidayWorkingHour"));
        $("#dvEmpID").hide();
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });
        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        $("#divNew").dialog({ autoOpen: false, modal: true, height: 500, width: 900, resizable: false, title: 'Working Hour for Weekend/Holiday ',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/WeekendHolidayWorkingHour/WeekendHolidayWorkingHour?page=' + pg;
                var form = $('#frmWeekendHolidayWorkingHour');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });

    function setData(id, name) {

        document.getElementById('Model_WeekendHolidayWorkingHour_strEmpName').value = name;
        document.getElementById('Model_WeekendHolidayWorkingHour_strEmpID').value = id;
        $("#divEmpList").dialog('close');
    }

    function closeDialogBox1() {

        $("#divNew").dialog('close');
        searchCData();

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
        searchCData();
    }
    
    function deleteWeekendHolidayWorkingHour(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intRowID: Id }, '/LMS/WeekendHolidayWorkingHour/Delete', 'divWeekendHolidayWorkingHourDetails');
        }
    return false;
    }
        
    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/WeekendHolidayWorkingHour/Details/' + Id;

        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/WeekendHolidayWorkingHour/WeekendHolidayWorkingHourAdd';

        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');
        return false;
    }


    function searchCData() {
        var strHTML;
        if (fnValidateDateTime() == false) {
            alert("Invalid Date.");
            return false;
        }

         if (fnValidate() == true) {
            var targetDiv = "#divDataList";
            var url = "/LMS/WeekendHolidayWorkingHour/WeekendHolidayWorkingHour";
            var form = $("#frmWeekendHolidayWorkingHour");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }

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
    

    /*remove the class which made red color of the drop down list */
    $(function () {
        $("select").each(function () {
            if ($(this).hasClass('input-validation-error')) {
                $(this).removeClass('input-validation-error');
            }
        })
    });
</script>
<form id="frmWeekendHolidayWorkingHour" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
             <td> Employee Name</td>
            <td class="style1"> <%= Html.TextBox("Model.WeekendHolidayWorkingHour.strEmpName", Model.WeekendHolidayWorkingHour.strEmpName, new { @class = "textRegular" })%>
             <div id="dvEmpID">                
                <%= Html.TextBox("Model.WeekendHolidayWorkingHour.StrEmpID", Model.WeekendHolidayWorkingHour.strEmpID, new { @class = "textRegular" })%>
             </div>
            <a href="#" class="btnSearch" onclick="return openEmployee();"></a>

            </td>
            <td>Weekend/Holiday</td>
            <td><%= Html.DropDownList("Model.WeekendHolidayWorkingHour.strWHType", Model.WorkingHolidayType, "...All...", new { @class = "selectBoxRegular" })%></td>
            
        </tr>
        <tr>
            <td>Company</td>
            <td class="style1"><%= Html.DropDownList("Model.WeekendHolidayWorkingHour.strCompanyID", Model.Company, "...All...", new { @class = "selectBoxRegular" })%></td>
            <td>Location</td>
             <td><%= Html.DropDownList("Model.WeekendHolidayWorkingHour.strLocationID", Model.Location, "...All...", new { @class = "selectBoxRegular" })%></td>
            
        </tr>
        <tr>
            <td>
                Department
            </td>
            <td class="style1">
                <%= Html.DropDownList("Model.WeekendHolidayWorkingHour.strDepartmentID", Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td>
                Designation
            </td>
            <td>
                <%= Html.DropDownList("Model.WeekendHolidayWorkingHour.strDesignationID", Model.Designation, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
         <tr>
            <td>
               Emp. Category
            </td>
            <td class="style1">
                <%= Html.DropDownList("Model.WeekendHolidayWorkingHour.intCategoryCode", Model.EmployeeCategory, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td>
                Religion
            </td>
            <td>
                <%= Html.DropDownList("Model.WeekendHolidayWorkingHour.intReligionID", Model.Religion, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
            <td>Apply Date</td>
            <td class="style1">
                        
                    <div style="float: left; text-align: left; padding-left: 8px;">
                        <%=Html.TextBox("Model.WeekendHolidayWorkingHour.strPeriodFrom", Model.WeekendHolidayWorkingHour.strPeriodFrom, new { @class = "textRegularDate dtPicker dateNR", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                      
                        To
                        <%=Html.TextBox("Model.WeekendHolidayWorkingHour.strPeriodTo", Model.WeekendHolidayWorkingHour.strPeriodTo, new { @class = "textRegularDate dtPicker dateNR", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                       
                    </div>
               
            </td>
            <%--<td>
                Shift
            </td>--%>
            <%--<td>
                <%= Html.DropDownList("Model.WeekendHolidayWorkingHour.intShiftID", Model.Shift, "...All...", new { @class = "selectBoxRegular" })%>
            </td>--%>
        </tr>
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendHolidayWorkingHour, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>           
            <td class="style1">&nbsp;</td>
            <td>&nbsp;</td>
            <td align="right" style="width: 10%;">
                <a href="#" class="btnSearchData" onclick="return searchCData();"></a>
            </td>
        </tr>        
    </table>
</div>
<div id="grid">
    <div id="grid-data" style="overflow: auto; height:400px;">
        <table class="style1" style="width:1400px">
            <colgroup>
                <col width="80px" />
                <col width="80px" />
                <col width=""/>
                <col width="" />
                <col width="120px" />
                <col width="120px" />
                <col width="120px" />
                <col width="100px" />
                <col width="80px" />
                <col width="80px" />
                <col width="80px" />
                <col width="20px" />
            </colgroup>
            <thead>
                <tr>
                    <th >
                        From Date
                    </th>
                    <th >
                       To Date 
                    </th>
                    <th>
                       Employee Name
                    </th>
                    <th>
                        Company
                    </th>
                    <th>
                        Location
                    </th>
                    <th>
                        Department
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        Emp. Category
                    </th>
                    <th>
                        Religion
                    </th>
                    <th >
                        In Time&nbsp;&nbsp;
                    </th>  
                    <th >
                        Out Time
                    </th>                                       
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendHolidayWorkingHour, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th style="width:20px">
                        Edit
                    </th>
                    <%} %>
                    <%-- <th>
                        Delete
                    </th>--%>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.WeekendHolidayWorkingHour lr in Model.LstWeekendHolidayWorkingHourPaging)
                   { 
                %>
                <tr>
                    <td >
                        <%=Html.Encode(lr.dtPeriodFrom.ToString("dd-MMM-yyyy"))%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.dtPeriodTo.ToString("dd-MMM-yyyy"))%>                        
                    </td>  
                    <td style="text-align:left">
                        <%=Html.Encode(lr.strEmpName)%>                        
                    </td>
                    <td style="text-align:left">
                        <%=Html.Encode(lr.strCompany)%>                        
                    </td> 
                    <td style="text-align:left">
                        <%=Html.Encode(lr.strLocation)%>                        
                    </td> 
                    <td style="text-align:left">
                        <%=Html.Encode(lr.strDepartment)%>                        
                    </td>  
                    <td style="text-align:left">
                        <%=Html.Encode(lr.strDesignation)%>                        
                    </td>
                    <td style="text-align:left">
                        <%=Html.Encode(lr.strCategory)%>                        
                    </td>
                     <td style="text-align:left">
                        <%=Html.Encode(lr.strReligion)%>                        
                    </td>    
                    <td >
                         <%=Html.Encode(lr.strInTime)%>                     
                    </td>  
                     <td >
                         <%=Html.Encode(lr.strOutTime)%>                     
                    </td>                           
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= lr.intRowID  %>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstWeekendHolidayWorkingHourPaging.PageNumber, ViewData.Model.LstWeekendHolidayWorkingHourPaging.TotalItemCount, "frmWeekendHolidayWorkingHour", "/LMS/WeekendHolidayWorkingHour/WeekendHolidayWorkingHour", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstWeekendHolidayWorkingHourPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstWeekendHolidayWorkingHour.Count.ToString() %></label>
</div>

<div id="divNew">
    <iframe id="styleNewEntry" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
</form>

