<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MiscellaneousReportModels>" %>

<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>

<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#formConveyanceReport"));

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

<form id="formMiscellaneousReport" method="post" action="">
  
  <div style="text-align:center">
    <h2>
        Miscellaneous Report
        <hr />
    </h2>
  </div>
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>

<div id="divNewWin">
     <div id="printable" class="divRow">
            <table width="100%" class="rptcontenttext" style="width: 100%; border-style: solid; border-collapse: collapse"   border="1px">
                <thead>
                    <tr>
                        <th  class="rptrowheader">
                            Employee Name
                        </th>
                        <th  class="rptrowheader">
                            Department
                        </th>
                        <th  class="rptrowheader">
                            Designation
                        </th>
                        <th  class="rptrowheader">
                            Conveyance Date
                        </th>                        
                        <th  class="rptrowheader">
                            Amount
                        </th>
                    </tr>
                </thead>

                <tbody>
                    <% if(Model.LstMiscellaneousReport !=null) %>
                    <% foreach (MiscellaneousReport item in Model.LstMiscellaneousReport)
                       { %>
            
                        <tr>
                            <td class="rptrowdetails">
                                <%= Html.Encode(item.STREMPNAME) %>
                            </td>
                            <td class="rptrowdetails">
                                <%= Html.Encode(item.STRDEPARTMENT) %>
                            </td>
                            <td class="rptrowdetails">
                                <%= Html.Encode(item.STRDESIGNATION) %>
                            </td>
                            <td class="rptrowdetails">
                                <%= Html.Encode(item.MISCDATE.ToString("dd-MMM-yyyy")) %>
                            </td>                            
                            <td class="rptAmountrowdetails">
                                <%= Html.Encode(item.TOTALAMOUNT) %>
                            </td>
                        </tr>
                    <%} %>

                        <tr>
                            <td align="right" colspan="4">
                                Total
                            </td>
                            <td align="right">
                             <% if (Model.LstMiscellaneousReport != null)
                                {%>

                                <% string totalAmount = Model.LstMiscellaneousReport.Sum(c => c.TOTALAMOUNT).ToString(); %>                                
                                <%= Html.Encode(totalAmount)%>

                                <%} %>
                            </td>
                        </tr>
                </tbody>
            </table>
     </div>

</div>

<div class="pager"> <label>
        Total Records: <%=Model.numTotalRows.ToString() %></label>&nbsp;&nbsp;
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize20, Model.LstMiscellaneousReportPaging.PageNumber, ViewData.Model.numTotalRows, "formMiscellaneousReport", "/LMS/MiscellaneousReport/ShowReport", "divReportView")%>
</div>

     <div style="text-align:center">
        <a href="#" class="btnPrint" id="btnPrint"></a><a href="#" class="btnPrintPreview" id="btnPrintPreview"></a>
        <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
     </div>
</form>
