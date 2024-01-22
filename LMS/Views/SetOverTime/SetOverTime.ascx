<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.SetOverTimeModels>" %>
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

        $("#divNew").dialog({ autoOpen: false, modal: true, height: 500, width: 900, resizable: false, title: 'Overtime Setup', beforeclose: function (event, ui) { Closing(); } });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        preventSubmitOnEnter($("#frmSetOverTime"));
        $("#dvEmpID").hide();
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });
        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        $("#divNew").dialog({ autoOpen: false, modal: true, height: 500, width: 900, resizable: false, title: 'Overtime Setup ',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/SetOverTime/SetOverTime?page=' + pg;
                var form = $('#frmSetOverTime');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });

    function setData(id, name) {

        document.getElementById('Model_SetOverTime_strEmpName').value = name;
        document.getElementById('Model_SetOverTime_strEmpID').value = id;
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
    
    function deleteSetOverTime(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intRowID: Id }, '/LMS/SetOverTime/Delete', 'divSetOverTimeDetails');
        }
    return false;
    }
        
    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/SetOverTime/Details/' + Id;

        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/SetOverTime/SetOverTimeAdd';

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
            var url = "/LMS/SetOverTime/SetOverTime";
            var form = $("#frmSetOverTime");
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
<form id="frmSetOverTime" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
             <td> Employee Name</td>
            <td class="style1"> <%= Html.TextBox("Model.SetOverTime.strEmpName", Model.SetOverTime.strEmpName, new { @class = "textRegular" })%>
             <div id="dvEmpID">                
                <%= Html.TextBox("Model.SetOverTime.StrEmpID", Model.SetOverTime.strEmpID, new { @class = "textRegular" })%>
             </div>
            <a href="#" class="btnSearch" onclick="return openEmployee();"></a>

            </td>
             <td>Company</td>
            <td class="style1"><%= Html.DropDownList("Model.SetOverTime.strCompanyID", Model.Company, "...All...", new { @class = "selectBoxRegular" })%></td>
            
        </tr>
        <tr>
            <td>
                Department
            </td>
            <td class="style1">
                <%= Html.DropDownList("Model.SetOverTime.strDepartmentID", Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
            <td>Location</td>
             <td><%= Html.DropDownList("Model.SetOverTime.strLocationID", Model.Location, "...All...", new { @class = "selectBoxRegular" })%></td>
            
        </tr>
        <tr>           
            <td>
                Designation
            </td>
            <td>
                <%= Html.DropDownList("Model.SetOverTime.strDesignationID", Model.Designation, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
             <td>
               Emp. Category
            </td>
            <td class="style1">
                <%= Html.DropDownList("Model.SetOverTime.intCategoryCode", Model.EmployeeCategory, "...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
         
        <tr>
            <td>Effective Date</td>
            <td class="style1">
                        
                    <div style="float: left; text-align: left; ">
                        <%=Html.TextBox("Model.SetOverTime.strPeriodFrom", Model.SetOverTime.strPeriodFrom, new { @class = "textRegularDate dtPicker dateNR", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                      
                        To
                        <%=Html.TextBox("Model.SetOverTime.strPeriodTo", Model.SetOverTime.strPeriodTo, new { @class = "textRegularDate dtPicker dateNR", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>
                       
                    </div>
               
            </td>
            <%--<td>
                Shift
            </td>--%>
            <%--<td>
                <%= Html.DropDownList("Model.SetOverTime.intShiftID", Model.Shift, "...All...", new { @class = "selectBoxRegular" })%>
            </td>--%>
        </tr>
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.SetOverTime, LMS.Web.Permission.MenuOperation.Add))
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
        <table class="style1" style="width:1000px">
            <colgroup>
                <col width="80px" />
                <col width="80px" />
                <col width=""/>
                <col width="" />
                <col width="120px" />
                <col width="120px" />
                <col width="120px" />
                <col width="100px" />
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
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.SetOverTime, LMS.Web.Permission.MenuOperation.Edit))
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
                <% foreach (LMSEntity.SetOverTime lr in Model.LstSetOverTimePaging)
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
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstSetOverTimePaging.PageNumber, ViewData.Model.LstSetOverTimePaging.TotalItemCount, "frmSetOverTime", "/LMS/SetOverTime/SetOverTime", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstSetOverTimePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstSetOverTime.Count.ToString() %></label>
</div>

<div id="divNew">
    <iframe id="styleNewEntry" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
</form>

