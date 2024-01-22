<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ReportsModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmDashboard"));

        $("#divStyleLeaveApplication").dialog({ autoOpen: false, modal: true, height: 700, width: 920, resizable: false, title: 'Online Leave Application',
            close: function (ev, ui) {

                var pg = $('#txtPageNo').val();
                var targetDiv = "#divDataList";
                var url = '/LMS/LeaveApplication/LeaveApplication?page=' + pg;
                var form = $("#frmLeaveApplicationList");
                var serializedForm = form.serialize();
                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


            }

    });
    
    function Closing() {
        //window.location = "/Dashboard";
    }
    
        function deleteDashboard(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intDashboardID: Id }, '/LMS/Dashboard/Delete', 'divDashboardDetails');

        }
        return false;
    }

    function popupStyleAdd() {
       
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/LeaveApplication/LeaveApplicationAdd';
        $('#styleOpenerLeaveApplication').attr({ src: url });
        $("#divStyleLeaveApplication").dialog('open');
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
<form id="frmDashboard" method="post" action="">
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<%= Html.HiddenFor(m=>m.IntLeaveYearId)%>
<%= Html.HiddenFor(m=>m.StrFromDate)%>
<%= Html.HiddenFor(m=>m.StrToDate)%>
<%= Html.HiddenFor(m => m.StrEmpId)%>
<%= Html.HiddenFor(m => m.StrDepartmentId)%>
<%= Html.HiddenFor(m => m.StrDesignationId)%>
<%= Html.HiddenFor(m => m.StrGender)%>
<%= Html.HiddenFor(m => m.IntCategoryId)%>
<%= Html.HiddenFor(m => m.StrLocationId)%>
<%= Html.HiddenFor(m => m.IsFromMyLeaveMenu)%>
<%= Html.HiddenFor(m => m.IsIndividual)%>
<%= Html.HiddenFor(m => m.IntLeaveTypeId)%>
<div>
    <% 
        if (Model.LstRptLeaveStatus.Count > 0)
        { %>
    <div id="divNewWin">
        <div id="printable" class="divRow" style="text-align: center;">
            <table class="contenttext" style="width: 100%;">
                <tr>
                    <td>
                        <table class="contenttext" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="text-align: left; font-weight:bold">
                                    <%=Html.Encode("Today's Status")%>
                                </td>                                
                            </tr>
                            <tr>
                                <td>Status :</td>
                                <td>&nbsp;</td>
                            </tr>
                             <tr>
                                <td>Date :</td>
                                <td><%=DateTime.Now %></td>
                            </tr>
                            <tr>
                                <td>Office In :</td>
                                 <td>&nbsp;</td>
                            </tr>
                        </table>
                        <table class="contenttext" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="text-align: left; font-weight:bold">
                                    <%=Html.Encode("My Profile")%>
                                </td>                                
                            </tr>
                            <tr>
                                <td>Employee ID :</td>
                                <td><%= Model.StrEmpId%></td>
                            </tr>
                             <tr>
                                <td>Name :</td>
                               <td><%= Model.StrEmpName%></td>
                            </tr>
                            <tr>
                                <td>Designation :</td>
                                <td><%= Model.StrDesignationId%></td>
                            </tr>
                            <tr>
                                <td>Department :</td>
                                <td><%= Model.StrDepartmentId%></td>
                            </tr>
                        </table>
                    </td>
                    <td>
                         <div style="text-align: left; font-size: large;">
                <%=Html.Encode("Leave Status")%></div>
            <div class="divSpacer">
            </div>
            <table id="tblData" class="rptcontenttext" style="width: 100%; border-style: solid; 
                border-collapse: collapse" border="1px" cellpadding="2px" cellspacing="0">
                <thead>
                    <tr>
                        <td >
                            <div style="width: 100%; height: auto; float: left; text-align: center; font-weight:bold;">
                                Leave Type
                            </div>
                        </td>
                        <td >
                            <div style="width: 100%; height: auto; float: left; text-align: center; font-weight:bold;">
                                CO
                            </div>
                        </td>
                        <td >
                            <div style="width: 100%; height: auto; float: left; text-align: center; font-weight:bold;">
                                Entitled
                            </div>
                        </td>
                        <td >
                            <div style="width: 100%; height: auto; float: left; text-align: center; font-weight:bold;">
                                Applied
                            </div>
                        </td>                        
                        <td >
                            <div style="width: 100%; height: auto; float: left; text-align: center; font-weight:bold;">
                                Availed
                            </div>
                        </td>                        
                        <td >
                            <div style="width: 100%; height: auto; float: left; text-align: center; font-weight:bold;">
                                Encashed
                            </div>
                        </td>
                        <td >
                            <div style="width: 100%; height: auto; float: left; text-align: center; font-weight:bold;">
                                Balance
                            </div>
                        </td>
                        <%--<td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Comments
                            </div>
                        </td>--%>
                    </tr>
                </thead>
                <tbody style="overflow-y: auto; overflow-x: hidden; max-height: 400px; width: 100%;">
                    <% string strID = ""; int i = 0; foreach (LMSEntity.rptLeaveStatus obj in Model.LstRptLeaveStatus)
                       {                
                    %>
                    <%--<%if (strID != obj.strEmpID.ToString())
                      {
                          strID = obj.strEmpID.ToString();
                          i = i + 1;
                    %>
                    <tr>
                        <td colspan="9" class="rptrowsection">
                            <div style="width: 52%; float: left; text-align: left; border: 0px;">
                                <%=Html.Encode("ID and Name : " + obj.strEmpID.ToString() + '-' + obj.strEmpName.ToString())%>
                            </div>
                            <div style="width: 48%; float: left; text-align: left; border: 0px;">
                                <%=Html.Encode("Department     : " + obj.strDepartment.ToString())%>
                            </div>
                            <br />
                            <div style="width: 52%; float: left; text-align: left; border: 0px;">
                                <%=Html.Encode("Designation   : " + obj.strDesignation.ToString())%>
                            </div>
                            <div style="width: 48%; float: left; text-align: left; border: 0px;">
                                <%=Html.Encode("Joining Date : " + obj.strJoiningDate.ToString())%>
                            </div>
                        </td>
                    </tr>
                    <%} %>--%>
                    <tr>
                        <td style="width: 22%">
                            <%=Html.Encode(obj.strLeaveType.ToString())%>
                        </td>
                        <td align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltOB.ToString())%>
                        </td>
                        <td  align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltEntitlement.ToString())%>
                        </td>
                        <td  align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltApplied.ToString())%>
                        </td>
                        <td align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode((obj.fltAvailed + obj.fltAvailedWOP).ToString())%>
                        </td>                       
                        <td  align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltEncased.ToString())%>
                        </td>
                        <td  align="right" style="width: 8%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltCB.ToString())%>
                        </td>
                        <%--<td class="rptrowdata" style="width: 11%">
                            <%=Html.Encode("")%>
                        </td>--%>
                    </tr>
                    <%} %>
                </tbody>
            </table>
            <div style="text-align: left;">
                <label>
                    NB: CO = Carry Over
                </label>
            </div>
             <div class="divSpacer">&nbsp;
            </div>
             <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OnlineLeaveApplication, LMS.Web.Permission.MenuOperation.Add))
                  {%>
                     <div style="text-align:center">
                        <a href="#" class="btnNewLeaveApplication" onclick="return popupStyleAdd();"></a>
                    </div>
                <%} %>
                    </td>
                </tr>
            </table>           
        </div>
    </div>   
    <%}
        else
        {%>
    <div id="dvRptMessage" style="text-align: center; padding-top: 50px; padding-left: 50px;">
        <label style="color: Black; font-weight: bold">
            Data not found to preview report.</label>
    </div>
    <%} %>
</div>
</form>
