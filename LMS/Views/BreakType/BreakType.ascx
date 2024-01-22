<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.BreakTypeModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmBreakType"));

        $("#divStyleBreakType").dialog({ autoOpen: false, modal: true, height: 300, width: 500, resizable: false, title: 'Break Type',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/BreakType/BreakType?page=' + pg;
                var form = $('#frmBreakType');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });
    
    function Closing() {
        //window.location = "/BreakType";
    }
    
        function deleteBreakType(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intBreakTypeID: Id }, '/LMS/BreakType/Delete', 'divBreakTypeDetails');

        }
        return false;
    }
        
    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/BreakType/Details/' + Id;

        $('#styleOpenerBreakType').attr({ src: url });
        $("#divStyleBreakType").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/BreakType/BreakTypeAdd';

        $('#styleOpenerBreakType').attr({ src: url });
        $("#divStyleBreakType").dialog('open');
        return false;
    }


    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/BreakType/BreakType";
        var form = $("#frmBreakType");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


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
<form id="frmBreakType" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.BreakType, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td align="right" style="width: 80%;">
              &nbsp;
            </td>
            <td align="right" style="width: 10%;">
                <%--<a href="#" class="btnSearchData" onclick="return searchData();"></a>--%>
            </td>
        </tr>
    </table>
</div>
<div id="grid">
    <div id="grid-data" style="overflow: auto; height:400px; width: 99%">
        <table>
            <thead>
                <tr>
                    <th>
                        Break Name
                    </th>
                    <th>
                       Description
                    </th>                    
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.BreakType, LMS.Web.Permission.MenuOperation.Edit))
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
                <% foreach (LMSEntity.BreakType lr in Model.LstBreakTypePaging)
                   { 
                %>
                <tr>
                    <td>
                        <%=Html.Encode(lr.strBreakName)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strDescription )%>
                    </td>                           
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= lr.intBreakID  %>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstBreakTypePaging.PageNumber, ViewData.Model.LstBreakTypePaging.TotalItemCount, "frmBreakType", "/LMS/BreakType/BreakType", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstBreakTypePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstBreakType.Count.ToString() %></label>
</div>

</form>
<div id="divStyleBreakType">
    <iframe id="styleOpenerBreakType" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
