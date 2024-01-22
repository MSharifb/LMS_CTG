<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveEncasmentModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmEncasmentList"));

        $("#divStyleLeaveEncasment").dialog({ autoOpen: false, modal: true, height: 580, width: 820, resizable: false, title: 'Leave Encashment',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/LeaveEncasment/LeaveEncasment?page=' + pg;
                var form = $('#frmEncasmentList');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });


    function deleteLeaveEncasment(Id) {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeCustomAction({ intHolidayRuleID: Id }, '/LMS/LeaveEncasment/Delete', 'divDataList');
        }
        return false;
    }


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/LeaveEncasment/Details/' + Id;
        $('#styleOpenerLeaveEncasment').attr({ src: url });
        $("#divStyleLeaveEncasment").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/LeaveEncasment/LeaveEncasmentAdd';
        $('#styleOpenerLeaveEncasment').attr({ src: url });
        $("#divStyleLeaveEncasment").dialog('open');
        return false;
    }


    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/LeaveEncasment/LeaveEncasment";
        var form = $("#frmEncasmentList");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
    
</script>
<form id="frmEncasmentList" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveEncasment, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td style="width: 10%;">
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
        </tr>
    </table>
</div>
<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
        <table>
            <thead>
                <tr>
                    <th>
                        Applicant
                    </th>
                    <th>
                        Branch
                    </th>
                    <th>
                        Department
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        Leave Year
                    </th>
                    <th>
                        Leave Type
                    </th>
                    <th>
                        Payment Month
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveEncasment, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.LeaveEncasment lr in Model.LstLeaveEncasmentPaging)
                   {                      
                %>
                <tr>
                    <td>
                        <% if (lr.strEmpID != "")
                           { %>
                        <%=Html.Encode(lr.strEmpID.ToString() + '-' + lr.strEmpName.ToString())%>
                        <%} %>
                    </td>
                    <td>
                        <% if (lr.strEmpID == "")
                           { %>

                            <% if (lr.strLocationName == "")
                               {%>
                               All
                            <%} else%>
                            <%= Html.Encode(lr.strLocationName)%>
                        <%} %>
                    </td>
                    <td>
                    <% if (lr.strEmpID == "")
                           { %>

                            <% if (lr.strDepartmentName == "")
                               {%>
                               All
                            <%} else%>
                            <%= Html.Encode(lr.strDepartmentName)%>
                        <%} %>
                       <%-- <%= Html.Encode(lr.strDepartmentName) %>--%>
                    </td>
                    <td>
                    <% if (lr.strEmpID == "")
                           { %>

                            <% if (lr.strDesignationName == "" || lr.strDesignationName == null)
                               {%>
                               All
                            <%} else%>
                            <%= Html.Encode(lr.strDesignationName)%>
                        <%} %>
                        <%--<%= Html.Encode(lr.strDesignationName) %>--%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strYearTitle)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strLeaveType)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.strPaymentMonth + '/' + lr.intPaymentYear.ToString())%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= lr.intLeaveEncaseMasterID  %>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstLeaveEncasmentPaging.PageNumber, ViewData.Model.LstLeaveEncasmentPaging.TotalItemCount, "frmEncasmentList", "/LMS/LeaveEncasment/LeaveEncasment", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstLeaveEncasmentPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstLeaveEncasment.Count.ToString() %></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleLeaveEncasment">
    <iframe id="styleOpenerLeaveEncasment" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
