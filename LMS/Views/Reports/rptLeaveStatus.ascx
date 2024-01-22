<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ReportsModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmrptLeaveStatus"));

        var nAgt = navigator.userAgent;
        if (nAgt.indexOf("MSIE") != -1) {
            var ver = getInternetExplorerVersion();
            if (ver == 7) {
                if (IsIE8Browser() == false) {

                    $("#btnPrint").hide();
                }
            }
        }
    });
    
    
    function IsIE8Browser() {
        var rv = -1;
        var ua = navigator.userAgent;
        var re = new RegExp("Trident\/([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null) {
            rv = parseFloat(RegExp.$1);
        }
        return (rv == 4);
    }
    
    
    function getInternetExplorerVersion() {

        var rv = -1; // Return value assumes failure.

        if (navigator.appName == 'Microsoft Internet Explorer') {

            var ua = navigator.userAgent;
            //alert(navigator.appVersion +"--"+ navigator.userAgent);
            var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");

            if (re.exec(ua) != null)
                rv = parseFloat(RegExp.$1);
        }
        return rv;
    }

    //Generating Pop-up Print Preview page
    function getPrint(print_area) {
        //Creating new page
        var pp = window.open();
        var htm = '<HTML><HEAD><title>Print Preview</title><LINK href=' + '"<%= Url.Content("~/Content/css/PStyles.css") %>"' + ' type="text/css" rel="stylesheet">';
        //alert(htm);
        //Adding HTML opening tag with <HEAD> … </HEAD> portion "Content/css/PStyles.css" "Content/css/PrintStyle.css"
        pp.document.writeln(htm);
        var mda = '<LINK href=' + '"<%= Url.Content("~/Content/css/PrintStyle.css") %>"' + ' type="text/css" rel="stylesheet" media="print"><base target="_self"></HEAD>';
        // alert(mda);
        pp.document.writeln(mda);
        //Adding Body Tag
        pp.document.writeln('<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">');
        //Adding form Tag
        pp.document.writeln('<form  method="post">');
        //Creating two buttons Print and Close within a table         
        pp.document.writeln('<TABLE width=100%><TR><TD></TD></TR><TR><TD align=right><INPUT ID="PRINT" type="button" value="Print" onclick="javascript:location.reload(true);window.print();"><INPUT ID="CLOSE" type="button" value="Close" onclick="window.close();"></TD></TR><TR><TD></TD></TR></TABLE>');
        //Writing print area of the calling page
        pp.document.writeln(document.getElementById(print_area).innerHTML);
        //Ending Tag of </form>, </body> and </HTML>
        pp.document.writeln('</form></body></HTML>');

    }

    $(function () {
        // Hook up the print link.
        $("#btnPrint")
        .attr("href", "javascript:void( 0 )")
        .click(function () {

            $("#printable").print();
            return (false);
        });

        $("#btnPrintPreview")
            .attr("href", "javascript:void( 0 )")
            .click(function () {
                getPrint("divNewWin");
                return (false);
            });
    });
      
   
   function ExportExcelByID() 
   {

    var id = "LeaveStatus";
    // alert('xxxx');
    if (!$("#divNewWin")) {
        alert("data not found");
        return false;
    }

    var exportvalue = $("#divNewWin").html();
    
    exportvalue = exportvalue.replace(/\ /g, "");
    exportvalue = escape(exportvalue);

    var ExportForm = document.createElement("FORM");
    document.body.appendChild(ExportForm);
    ExportForm.method = "POST";

    //var newElement = document.createElement("<input name='exportvalue' type='hidden' id='exportvalue'>");
    var newElement = document.createElement("input");
    newElement.setAttribute("name", "exportvalue");
    newElement.setAttribute("type", "hidden");

    ExportForm.appendChild(newElement);
    newElement.value = exportvalue;
    ExportForm.action = "/LMS/Reports/ToExcel/"+ id;
    ExportForm.submit();

    return false;
}

function closeRptDialog() {
    var len = parent.$(".ui-icon-closethick").length;
    parent.$(".ui-icon-closethick").each(function (i) {
        if (i == len - 1) {
            $(this).click();
        }
    });
}

</script>


<form id="frmrptLeaveStatus" method="post" action="">
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
<%= Html.HiddenFor(m => m.EmpStatus)%>
<div>
    <% 
        if (Model.LstRptLeaveStatus.Count > 0)
        { %>
    <div id="divNewWin">
        <div id="printable" class="divRow" style="text-align: center;">
            <table class="contenttext" style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: larger;">
                        <%=Html.Encode(Model.LstRptLeaveStatus[0].strCompany.ToString())%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <%=Html.Encode("Leave Status")%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <%=Html.Encode(Model.LstRptLeaveStatus[0].strYearTitle.ToString())%>
                    </td>
                </tr>
            </table>
            <div class="divSpacer">
            </div>
            <table id="tblData" class="rptcontenttext" style="width: 100%; border-style: solid;
                border-collapse: collapse" border="1px" cellpadding="0" cellspacing="0">
                <thead>
                    <tr>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Leave Type
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                CO
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                YE
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Applied
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Approved
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Availed(WP)
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Availed(WOP)
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                EC
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
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
                    <%if (strID != obj.strEmpID.ToString())
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
                    <%} %>
                    <tr>
                        <td class="rptrowdata" style="width: 22%">
                            <%=Html.Encode(obj.strLeaveType.ToString())%>
                        </td>
                        <td class="rptrowdata" align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltOB.ToString())%>
                        </td>
                        <td class="rptrowdata" align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltEntitlement.ToString())%>
                        </td>
                        <td class="rptrowdata" align="right" style="width: 6%; padding-right: 10px;">
                           <%=Html.Encode(obj.fltApplied.ToString())%> 
                        </td>
                        <td class="rptrowdata" align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode((obj.fltAvailed + obj.fltAvailedWOP).ToString())%>
                        </td>
                        <td class="rptrowdata" align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltAvailed.ToString())%>
                        </td>
                        <td class="rptrowdata" align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltAvailedWOP.ToString())%>
                        </td>
                        <td class="rptrowdata" align="right" style="width: 6%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltEncased.ToString())%>
                        </td>
                        <td class="rptrowdata" align="right" style="width: 8%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltCB.ToString())%>
                        </td>                        
                        <%--<td class="rptrowdata" style="width: 11%">
                            <%=Html.Encode("")%>
                        </td>--%>
                    </tr>
                    <%} %>
                </tbody>
            </table>
            <div style="text-align:left;">
                <label style="font-size: 10pt; font-family: Verdana;">
                    NB: CO = Carry Over, YE = Yearly Entitled, WP = With Pay, WOP = Without Pay, EC = Encashed
                </label>
            </div>
        </div>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize20, Model.LstRptLeaveStatusPaging.PageNumber, ViewData.Model.numTotalRows, "frmrptLeaveStatus", "/LMS/Reports/RptLeaveStatus", "divReportView")%>
    </div>
    <label>
        Total Records:<%=Model.numTotalRows.ToString() %></label>
        <a href="#" class="btnPrint noprint" id="btnPrint"></a><a href="#" class="btnPrintPreview"
        id="btnPrintPreview"></a>
        <%--<a href="#" class="btnClose" onclick="return closeRptDialog();"></a>--%>

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
