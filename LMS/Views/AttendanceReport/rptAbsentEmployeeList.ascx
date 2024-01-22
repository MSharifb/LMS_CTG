<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.AttendanceReportModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>

<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmReportView"));

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

    // When the document is ready, initialize the link so
    // that when it is clicked, the printable area of the
    // page will print.        

    $(function () {

        // Hook up the print link.
        $("#btnPrint")
        .attr("href", "javascript:void( 0 )")
        .click(function () {
            // Print the DIV.
            $("#printable").print();
            // Cancel click event.
            return (false);
        });

        $("#btnPrintPreview")
            .attr("href", "javascript:void( 0 )")
            .click(function () {
                getPrint("divNewWin");
                return (false);
            });

    });

</script>
<form id="frmReportView" method="post" action="">
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<%= Html.HiddenFor(m => m.strReportType)%>
<%= Html.HiddenFor(m => m.StrCompanyID)%>
<%= Html.HiddenFor(m => m.StrFromDate)%>
<%= Html.HiddenFor(m => m.StrToDate)%>
<%= Html.HiddenFor(m => m.StrEmpId)%>
<%= Html.HiddenFor(m => m.StrDepartmentId)%>
<%= Html.HiddenFor(m => m.StrDesignationId)%>
<%= Html.HiddenFor(m => m.intCategoryCode)%>
<%= Html.HiddenFor(m => m.StrLocationId)%>
<div>
    <% if (Model.LstAttendanceReport.Count > 0)
       { %>
    <div id="divNewWin">
        <div id="printable" class="divRow">
            <table class="contenttext" style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: larger;">
                        <%=Html.Encode(Model.LstAttendanceReport[0].strCompany.ToString())%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <%=Html.Encode("List of Absent Employee")%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; text-decoration:underline;">
                        
                        <%=Html.Encode(Model.DisplayFromDate)%>
                        &nbsp;To&nbsp;
                        <%=Html.Encode(Model.DisplayToDate)%>
                    </td>
                </tr>
            </table>
            <div class="divSpacer">
            </div>
            <%--<table class="contenttext" style="width: 100%;">
                <colgroup>
                    <col width="25%" />
                    <col />
                </colgroup>
                <tr>
                    <td style="text-align: left; font-weight:bold; ">
                        Department :
                    </td>
                    <td style="text-align: left; font-weight:bold; ">
                        <%=Html.Encode(Model.LstAttendanceReport[0].strDepartment.ToString())%>
                    </td>
                </tr>
            </table>--%>
            <table class="rptcontenttext" style="width: 100%; border-style: solid; border-collapse: collapse"
                border="1px">
                <thead>
                    <tr>
                        <td class="rptrowheader" style="width: 90px">
                            Att. Date
                        </td>
                        <td class="rptrowheader">
                            Emp. ID
                        </td>
                        <td class="rptrowheader">
                            Emp. Name
                        </td>
                        <td class="rptrowheader">
                            Designation
                        </td>                       
                        <td class="rptrowheader">
                            Shift Name
                        </td>
                        <td class="rptrowheader" style="width:90px">
                            Shift In
                        </td>
                        <td class="rptrowheader" style="width:90px">
                            Shift Out
                        </td>                        
                    </tr>
                </thead>
                <tbody style="overflow-y: auto; overflow-x: auto; max-height: 400px; width: 100%;">
                    <%  int count = 0;
                        foreach (LMSEntity.AttendanceReport obj in Model.LstAttendanceReport)
                       {
                           
                           if (count == 0 )
                           {
                                %> <tr>
                                <td colspan="7" style="text-align:left; font-weight:bold;" class="rptrowdetails">
                                   Department : <%=Html.Encode(obj.strDepartment)%>
                                </td>
                                </tr>
                          <% }
                           else if (Model.LstAttendanceReport[count-1].strDepartment != Model.LstAttendanceReport[count].strDepartment)
                           {%> <tr>
                                <td colspan="7" style="text-align:left; font-weight:bold;" class="rptrowdetails">
                                    Department: <%=Html.Encode(obj.strDepartment)%>
                                </td>
                                </tr>
                          <% }
                               count++;         
                    %>
                    <tr>
                        <td class="rptrowdetails">
                            <%=Html.Encode(obj.dtAttendDateTime.ToString("dd-MMM-yyyy"))%>
                        </td>
                        <td class="rptrowdetails">
                            <%=Html.Encode(obj.strEmpID.ToString())%>
                        </td>
                        <td class="rptrowdetails" style="text-align:left">
                            <%=Html.Encode(obj.strEmpName.ToString())%>
                        </td>
                        <td class="rptrowdetails" style="text-align:left">
                            <%=Html.Encode(obj.strDesignation.ToString())%>
                        </td>                        
                        <td class="rptrowdetails" style="text-align:left">
                            <%=Html.Encode(obj.strShiftName.ToString())%>
                        </td>
                        <td class="rptrowdetails">
                            <%=Html.Encode(obj.dtInTime.ToString("hh:mm tt"))%>
                        </td>
                        <td class="rptrowdetails">
                            <%=Html.Encode(obj.dtOutTime.ToString("hh:mm tt"))%>
                        </td>                        
                    </tr>
                    <%} %>
                </tbody>
            </table>
            <%--<%} %>--%>
        </div>
    </div>
    <div class="pager"> <label>
        Total Records:<%=Model.numTotalRows.ToString() %></label>&nbsp;&nbsp;
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize20, Model.LstAttendanceReportPaging.PageNumber, ViewData.Model.numTotalRows, "frmReportView", "/LMS/AttendanceReport/ShowReport", "divReportView")%>
    </div>
   
        <div style="text-align:center">
    <a href="#" class="btnPrint" id="btnPrint"></a><a href="#" class="btnPrintPreview"
        id="btnPrintPreview"></a><%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%></div>
    <%}
       else
       {%>
    <div id="dvRptMessage" style="text-align: center; padding-top: 50px; padding-left: 50px;">
        <label style="color: Black; font-weight: bold">
            Data not found to preview report.</label>
        <br /><%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
    </div>
    <%} %>
</div>
</form>
