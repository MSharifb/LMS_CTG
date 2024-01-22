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

    function closeRptDialog() {
        var len = parent.$(".ui-icon-closethick").length;
        parent.$(".ui-icon-closethick").each(function (i) {
            if (i == len - 1) {
                $(this).click();
            }
        });
    }

</script>
<form id="frmrptLeaveEncasment" method="post" action="">
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<%= Html.HiddenFor(m => m.IntLeaveYearId)%>
<%= Html.HiddenFor(m => m.StrFromDate)%>
<%= Html.HiddenFor(m => m.StrToDate)%>
<%= Html.HiddenFor(m => m.StrEmpId)%>
<%= Html.HiddenFor(m => m.StrDepartmentId)%>
<%= Html.HiddenFor(m => m.StrDesignationId)%>
<%= Html.HiddenFor(m => m.StrGender)%>
<%= Html.HiddenFor(m => m.IntCategoryId)%>
<%= Html.HiddenFor(m => m.StrLocationId)%>
<%= Html.HiddenFor(m => m.IntLeaveTypeId)%>
<%= Html.HiddenFor(m => m.EmpStatus)%>
<div>
    <% if (Model.LstRptLeaveEncasment.Count > 0)
       { %>
    <div id="divNewWin">
        <div id="printable" class="divRow">
            <table class="contenttext" style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: larger;">
                        <%=Html.Encode(Model.LstRptLeaveEncasment[0].strCompany.ToString())%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <%=Html.Encode("Leave Encashment")%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <%=Html.Encode(Model.LstRptLeaveEncasment[0].strYearTitle.ToString())%>
                    </td>
                </tr>
            </table>
            <div class="divSpacer">
            </div>
            <table class="rptcontenttext" style="width: 100%; border-style: solid; border-collapse: collapse"
                border="1px">
                <thead>
                    <tr>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                ID and Name
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Designation
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Department
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Leave Type
                            </div>
                        </td>
                        <td class="rptrowdata">
                            <div style="width: 100%; height: auto; float: left; text-align: center;">
                                Encash Day
                            </div>
                        </td>
                    </tr>
                </thead>
                <tbody style="overflow-y: auto; overflow-x: hidden; max-height: 400px; width: 100%;">
                    <% foreach (LMSEntity.rptLeaveEncasment obj in Model.LstRptLeaveEncasment)
                       {                
                    %>
                    <tr>
                        <td class="rptrowdata">
                            <%=Html.Encode(obj.strEmpID.ToString()+'-'+obj.strEmpName.ToString())%>
                        </td>
                        <td class="rptrowdata">
                            <%=Html.Encode(obj.strDesignation.ToString())%>
                        </td>
                        <td class="rptrowdata">
                            <%=Html.Encode(obj.strDepartment.ToString())%>
                        </td>
                        <td class="rptrowdata" style="width: 20%;">
                            <%=Html.Encode(obj.strLeaveType.ToString())%>
                        </td>
                        <td class="rptrowdata" align="right" style="width: 13%; padding-right: 10px;">
                            <%=Html.Encode(obj.fltEncaseDuration.ToString())%>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
            <%--<%} %>--%>
        </div>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize20, Model.LstRptLeaveEncasmentPaging.PageNumber, ViewData.Model.numTotalRows, "frmrptLeaveEncasment", "/LMS/Reports/RptLeaveEncasment", "divReportView")%>
    </div>
    <label>
        Total Records:<%=Model.numTotalRows.ToString() %></label>
    <a href="#" class="btnPrint" id="btnPrint"></a><a href="#" class="btnPrintPreview"
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
