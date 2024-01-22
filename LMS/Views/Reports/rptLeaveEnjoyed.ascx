<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ReportsModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<style type="text/css">
    .style1
    {
        width: 21%;
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmrptLeaveEnjoyed"));

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
        var htm = '<HTML><HEAD><meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"><title>Print Preview</title><LINK href=' + '"<%= Url.Content("~/Content/css/ContentLayout.css") %>"' + ' type="text/css" rel="stylesheet">';
        htm = htm + '<LINK href=' + '"<%= Url.Content("~/Content/css/PStyles.css") %>"' + ' type="text/css" rel="stylesheet">';
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
        pp.document.writeln('<TABLE width="1600px"><TR><TD></TD></TR><TR><TD align=right><INPUT ID="PRINT" type="button" value="Print" onclick="javascript:location.reload(true);window.print();"><INPUT ID="CLOSE" type="button" value="Close" onclick="window.close();"></TD></TR><TR><TD></TD></TR></TABLE>');
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

        function closeRptDialog() {
            var len = parent.$(".ui-icon-closethick").length;
            parent.$(".ui-icon-closethick").each(function (i) {
                if (i == len - 1) {
                    $(this).click();
                }
            });
        }
      
</script>
<form id="frmrptLeaveEnjoyed" method="post" action="">
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
<%= Html.HiddenFor(m => m.IsWithoutPay)%>
<%= Html.HiddenFor(m => m.IsApplyDate)%>
<%= Html.HiddenFor(m => m.EmpStatus)%>
<div>
    <% if (Model.LstRptLeaveEnjoyed.Count > 0)
       { %>
    <div id="divNewWin">
        <div id="printable" class="divRow" style="width: 1600px;">
            <table class="contenttext" style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: larger;">
                        <%=Html.Encode(Model.LstRptLeaveEnjoyed[0].strCompany.ToString())%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <%=Html.Encode("Leave Availed")%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <%if (Model.StrFromDate != null && Model.StrFromDate != "" && Model.StrToDate != null && Model.StrToDate != "")
                          { 
                        %>
                        <%if (Model.IsApplyDate == true)
                          { %>
                        <%=Html.Encode("Apply Date : " + Model.StrFromDate.ToString() + " To " + Model.StrToDate.ToString())%>
                        <%}
                          else
                          { %>
                        <%=Html.Encode("Leave Date : " + Model.StrFromDate.ToString() + " To " + Model.StrToDate.ToString())%>
                        <%} %>
                        <%}
                          else
                          {%>
                        <%=Html.Encode(Model.LstRptLeaveEnjoyed[0].strYearTitle.ToString())%>
                        <%} %>
                    </td>
                </tr>
            </table>
            <div class="divSpacer">
            </div>
            <table class="rptcontenttext" style="width: 100%; border-style: solid; border-collapse: collapse"
                border="1px" cellpadding="0" cellspacing="0">
                <thead>
                    <tr>
                        <td class="rptrowdata" align="center">
                            Designation
                        </td>
                        <td class="rptrowdata" align="center">
                            Department
                        </td>
                        <td class="rptrowdata" align="center">
                            Leave Type
                        </td>
                        <td colspan="2" class="rptrowdata">
                            <div style="width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black">
                                Leave Date
                            </div>
                            <div style="width: 100%; float: left">
                                <div style="width: 50%; height: 30px; float: left; text-align: center">
                                    From
                                </div>
                                <div style="width: 42%; height: 30px; float: left; border-left: solid 1px black;
                                    text-align: center">
                                    To
                                </div>
                            </div>
                        </td>
                        <td colspan="2" class="rptrowdata" style="width: 130px;">
                            <div style="width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black">
                                Leave Time
                            </div>
                            <div style="width: 100%; float: left">
                                <div style="width: 50%; height: 30px; float: left;  text-align: center">
                                    From
                                </div>
                                <div style="width: 42%; height: 30px; float: left; border-left: solid 1px black;
                                    text-align: center">
                                    To
                                </div>
                            </div>
                        </td>
                        <td colspan="2" class="rptrowdata" style="">
                            <div style="width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black">
                                <%if (Model.IsWithoutPay == true)
                                  { %>
                                Duration(WOP)
                                <%}
                                  else
                                  {%>
                                Duration(WP)
                                <%} %>
                            </div>
                            <div style="width: 100%; float: left">
                                <div style="width: 49%; height: 30px; float: left; text-align: center">
                                    Days
                                </div>
                                <div style="width: 42%; height: 30px; float: left; border-left: solid 1px black;
                                    text-align: center; margin-bottom: 0px;">
                                    Hours
                                </div>
                            </div>
                        </td>
                        <td class="rptrowdata" align="center">
                            Balance (Days)
                        </td>
                        <td class="rptrowdata" align="center">
                            Purpose
                        </td>
                        <td colspan="2" class="rptrowdata">
                            <div style="width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black">
                                Approved By
                            </div>
                            <div style="width: 100%; float: left">
                                <div style="width: 50%; height: 30px; float: left; text-align: center">
                                    Name
                                </div>
                                <div style="width: 42%; height: 30px; float: left; border-left: solid 1px black;
                                    text-align: center">
                                    Desig. and Dept.
                                </div>
                            </div>
                        </td>
                        <td colspan="2" class="rptrowdata">
                            <div style="width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black">
                                Approved On
                            </div>
                            <div style="width: 100%; float: left">
                                <div style="width: 56%; height: 30px; float: left; text-align: center">
                                    Date
                                </div>
                                <div style="width: 42%; height: 30px; float: left; border-left: solid 1px black;
                                    text-align: center">
                                    Time
                                </div>
                            </div>
                        </td>
                    </tr>
                </thead>
                <tbody style="overflow-y: auto; overflow-x: hidden; max-height: 400px; width: 100%;">
                    <% 
                   
                                  var mainGrp = from obj in Model.LstRptLeaveEnjoyed
                                                group obj by new
                                                {
                                                    obj.strEmpID,
                                                    obj.strEmpName
                                                } into g
                                                select new
                                                          {
                                                              strEmpID = g.Select(n => n.strEmpID).First(),
                                                              strEmpName = g.Select(n => n.strEmpName).First(),
                                                          };
                                  foreach (var mnObj in mainGrp)
                                  {
                    %>
                    <tr>
                        <td colspan="15" class="rptrowdata">
                            <div>
                                <%=Html.Encode("ID and Name : " + mnObj.strEmpID.ToString() + '-' + mnObj.strEmpName.ToString())%>
                            </div>
                        </td>
                    </tr>
                    <%

           
                                      var objs = from obj in Model.LstRptLeaveEnjoyed
                                                 where obj.strEmpID == mnObj.strEmpID
                                                 select obj;

                                      foreach (LMSEntity.rptLeaveEnjoyed obj in objs)
                                      {       
                                 
                    %>
                    <tr>
                        <td class="rptrowdata" align="left" style="width: 150px">
                            <%=Html.Encode(obj.strDesignation.ToString())%>
                        </td>
                        <td class="rptrowdata" align="left" style="width: 150px">
                            <%=Html.Encode(obj.strDepartment.ToString())%>
                        </td>
                        <td class="rptrowdata" align="left" style="width: 100px">
                            <%=Html.Encode(obj.strLeaveType.ToString())%>
                        </td>
                        <td class="rptrowdata" align="center" style="width: 110px">
                            <%=Html.Encode(obj.strApplyFromDate.ToString())%>
                        </td>
                        <td class="rptrowdata" align="center" style="width: 110px">
                            <%=Html.Encode(obj.strApplyToDate.ToString())%>
                        </td>
                        <td class="rptrowdata" align="center" style="width: 65px">
                            <%=Html.Encode(obj.strApplyFromTime.ToString())%>
                            <%-- <%=Html.Encode("10:30 AM")%>--%>
                        </td>
                        <td class="rptrowdata" align="center" style="width: 65px">
                            <%=Html.Encode(obj.strApplyToTime.ToString())%>
                            <%--  <%=Html.Encode("11:00 AM")%>--%>
                        </td>
                        <td class="rptrowdata" align="center" style="width: 50px">
                            <% if (obj.bitIsAdjustment == false)
                               { %>
                            <%=Html.Encode(obj.fltDurationDay.ToString())%>
                            <% }
                               else
                               { %>
                            <%=Html.Encode("("+obj.fltDurationDay.ToString()+")")%>
                            <% } %>
                        </td>
                        <td class="rptrowdata" align="center" style="width: 50px">
                            <% if (obj.bitIsAdjustment == false)
                               {
                                   if (Model.IsWithoutPay == true)
                                   {
                            %>
                            <%=Html.Encode(obj.fltWithoutPayDuration.ToString())%>
                            <%}
                                   else
                                   { %>
                            <%=Html.Encode(obj.fltWithPayDuration.ToString())%>
                            <%}
                               }
                               else
                               {
                                   if (Model.IsWithoutPay == true)
                                   { %>
                            <%=Html.Encode("("+obj.fltWithoutPayDuration.ToString()+")")%>
                            <%}
                                   else
                                   { %>
                            <%=Html.Encode("("+obj.fltWithPayDuration.ToString()+")")%>
                            <%}
                               } %>
                        </td>
                        <td class="rptrowdata" align="center" style="width: 70px">
                            <%=Html.Encode(obj.fltCB.ToString())%>
                        </td>
                        <td class="rptrowdata" style="">
                            <%=Html.Encode(obj.strPurpose.ToString())%>
                        </td>
                        <td class="rptrowdata" style="width: 120px">
                            <%=Html.Encode(obj.ApproverName)%>
                        </td>
                        <td class="rptrowdata" style="width: 120px">
                            <%=Html.Encode(obj.ApproverDesignation + ", " + obj.ApproverDepartment)%>
                        </td>
                        <td class="rptrowdata" style="width: 80px">
                            <%=Html.Encode(obj.ApproveDateTime!=null?obj.ApproveDateTime.ToString(LMS.Util.DateTimeFormat.Date):"")%>
                        </td>
                        <td class="rptrowdata" style="width: 65px">
                            <%=Html.Encode(obj.ApproveDateTime != null ? obj.ApproveDateTime.ToString("hh:mm tt") : "")%>
                        </td>
                    </tr>
                    <%}
                        }%>
                </tbody>
            </table>
            <div style="text-align:left;">
                <label style="font-size: 10pt; font-family: Verdana;">
                    NB: WP = With Pay, WOP = Without Pay, ( ) = Denotes leave adjustment.
                </label>
            </div>
        </div>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize20, Model.LstRptLeaveEnjoyedPaging.PageNumber, ViewData.Model.numTotalRows, "frmrptLeaveEnjoyed", "/LMS/Reports/RptLeaveEnjoyed", "divReportView")%>
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
