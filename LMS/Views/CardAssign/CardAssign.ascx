<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CardAssignModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        
        $("#divNew").dialog({ autoOpen: false, modal: true, height: 600, width: 800, resizable: false, title: 'Card Assignment', beforeclose: function (event, ui) { Closing(); } });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        preventSubmitOnEnter($("#frmCardAssign"));
        $("#dvEmpID").hide();

        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        $("#divNew").dialog({ autoOpen: false, modal: true, height: 600, width: 800, resizable: false, title: 'Card Assignment',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/CardAssign/CardAssign?page=' + pg;
                var form = $('#frmCardAssign');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });

    function setData(id, name) {

        document.getElementById('Model_EmployeeName').value = name;
        document.getElementById('Model_StrEmpID').value = id;
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
    
    function deleteCardAssign(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intCardAssignID: Id }, '/LMS/CardAssign/Delete', 'divCardAssignDetails');
        }
    return false;
    }
        
    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/CardAssign/Details/' + Id;

        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/CardAssign/CardAssignAdd';

        $('#styleNewEntry').attr({ src: url });
        $("#divNew").dialog('open');
        return false;
    }


    function searchCData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/CardAssign/CardAssign";
        var form = $("#frmCardAssign");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


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
<form id="frmCardAssign" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>Employee Name</td>
             <td>            
             <%= Html.TextBox("Model.EmployeeName", Model.EmployeeName, new { @class = "textRegular" })%>
             <div id="dvEmpID">                
                <%= Html.TextBox("Model.StrEmpID", Model.CardAssign.strEmpID, new { @class = "textRegular" })%>
             </div>
            <a href="#" class="btnSearch" onclick="return openEmployee();"></a>

            </td>
            <td>Card ID</td>
            <td align="right">                
                <%= Html.TextBox("Model.StrCardID", Model.CardAssign.strCardID, new { @class = "textRegular" })%>
            </td>
        </tr>
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CardAssign, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>           
            <td colspan="2">&nbsp;</td>
            <td align="right">
                <a href="#" class="btnSearchData" onclick="return searchCData();"></a>
            </td>
        </tr>
    </table>
</div>
<div id="grid">
    <div id="grid-data" style="overflow: auto; height:400px; width: 99%">
        <table>
            <thead>
                <tr>
                    <th style="width:100px">
                        Assign ID
                    </th>
                    <th>
                        Employee Name
                    </th>
                    <th style="width:120px">
                        Card ID
                    </th>
                    <th style="width:100px; text-align:center">
                        Effective Date
                    </th>                                       
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CardAssign, LMS.Web.Permission.MenuOperation.Edit))
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
                <% foreach (LMSEntity.CardAssign lr in Model.LstCardAssignPaging)
                   { 
                %>
                <tr>
                    <td>
                        <%=Html.Encode(lr.strAssignID)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strEmpName)%>                        
                    </td>  
                    <td>
                        <%=Html.Encode(lr.strCardID)%>                        
                    </td>  
                    <td style="text-align:center">
                         <%=Html.Encode(lr.dtEffectiveDate.ToString("dd-MMM-yyyy"))%>                     
                    </td>                           
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= lr.intCardAssignID  %>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstCardAssignPaging.PageNumber, ViewData.Model.LstCardAssignPaging.TotalItemCount, "frmCardAssign", "/LMS/CardAssign/CardAssign", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstCardAssignPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstCardAssign.Count.ToString() %></label>
</div>

<div id="divNew">
    <iframe id="styleNewEntry" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
</form>

