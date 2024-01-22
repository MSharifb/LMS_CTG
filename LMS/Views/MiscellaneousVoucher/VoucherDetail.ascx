<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MiscellaneousVoucherModels>" %>
<%@ Import Namespace="LMSEntity" %>

<script type="text/javascript">
    $(document).ready(function () {
     $("#lblTest").text("Taka "+toWords(<%= Model.LstMISCDetails.Sum(c => c.APPROVEDAMOUNT).ToString() %>)+" Only");
        $("#divDialog").dialog({ autoOpen: false, modal: true, height: 500, width: 700, resizable: false, title: 'Attachment', beforeclose: function (event, ui) { Closing(); }
        });

        var nAgt = navigator.userAgent;
        if (nAgt.indexOf("MSIE") != -1) {
            var ver = getInternetExplorerVersion();
            if (ver == 7) {
                if (IsIE8Browser() == false) {

                    $("#btnPrint").hide();
                }
            }
        }

        $(".Print").hide();
        $(".noPrint").show();
        $(".btnPrint").hide();
        $(".btnPrintPreview").hide();

        <% if( Model.MiscellaneousVoucher.ISAPPROVED == "1") %>
       <%{ %>
             
              SetForPrint();
              
              $(".btnPrint").show();
              $(".btnPrintPreview").show();
              $(".btnPaid").hide();
       <%} %>

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
       // var htm = '<HTML><HEAD><title>Print Preview</title><LINK href=' + '"<%= Url.Content("~/Content/css/PStyles.css") %>"' + ' type="text/css" rel="stylesheet">';
        //alert(htm);
        //Adding HTML opening tag with <HEAD> … </HEAD> portion "Content/css/PStyles.css" "Content/css/PrintStyle.css"
        //pp.document.writeln(htm);
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
            $("#divMsgStd").html('');
            $("#printable").print();
            return (false);
        });

        $("#btnPrintPreview")
            .attr("href", "javascript:void( 0 )")
            .click(function () {
                getPrint("printable");
                return (false);
            });
    });


    function Closing()
    {}


    function SetForPrint() {
        $(".Print").show();
        $(".noPrint").hide();
        //$("#lblUnit").text($("#ConveyanceObj_UNITNAME").val());
        $("#lblVoucherNo").text($("#MiscellaneousVoucher_VOUCHERNUMBER").val());
        

    }

    function Approve() {

        if (confirm('Do you want to pay?') == false) {
            return false;
        }

        if(fnValidate() == true)
        {
        var targetDiv = "#divDataList";
        var url = "/LMS/MiscellaneousVoucher/Approve";
        var form = $("#MiscellaneousFrom");
        var serializedForm = form.serialize();
        
        $.getJSON(url, serializedForm, function (result) {

            if (result.Message.search('Successfull') > -1) {
                $("#divMsgStd").html(result.Message);
                $(".btnPaid").hide();
                window.parent.searchConvData();
                                
                SetForPrint();
                
               // var unitName = $("#MiscellaneousVoucher_UNITID option:selected").text();
               // $("#lblUnit").text(unitName); 
                 
                
                $("#btnPrint").show();
                $(".Print").show();
                $(".noPrint").hide();
                $("#lblDate").text($("#txtVoucherDate").val());

            }
        });

        }
        return false;
    }


     function ShowFile(id) {
        
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MISCApproval/AttachmentView/' + id;
        
        $('#styleOpenerView').attr({ src: url });
        $("#divDialog").dialog('open');
        return false;

    }

</script>


<form id="MiscellaneousFrom" method="post" action="" >

<%= Html.HiddenFor(m=> m.MiscellaneousVoucher.RECORDID) %>
<%= Html.HiddenFor(m=> m.MiscellaneousVoucher.STRAUTHORID) %>
<%= Html.HiddenFor(m=> m.MiscellaneousVoucher.MISCID) %>
<%= Html.HiddenFor(m=> m.MiscellaneousVoucher.MISCDATE) %>
<%= Html.HiddenFor(m=> m.MiscellaneousVoucher.STREMPID) %>
<%= Html.HiddenFor(m=>m.MiscellaneousVoucher.UNITID) %>
<% if (Model.IsPaid == "1")
   { %>
<%--<%= Html.HiddenFor(m => m.MiscellaneousVoucher.VOUCHERNUMBER)%>--%>
<%= Html.HiddenFor(m => m.MiscellaneousVoucher.ISAPPROVED)%>
<%} %>
<div id="printable">
    <table width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width:70%">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="center">
                                        <div style="width:100%">
                                            <div style="width:20%;float:left">
                                                <img id="Img1"  runat="server" src="~/Content/img/controls/MohammadiLogo.jpg" />    
                                            </div>

                                            <div  style="width:80%;float:left">
                                                <img id="Img2"  runat="server" src="~/Content/img/controls/MGText.gif" />    
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>                                    
                                    <td  colspan="2" align="center">
                                        <div style="width:100%">
                                           
                                            <div style="width:100%;text-align:center;">
                                                <div class="Print"> 
                                                  <b> Name of Unit:   <label id="lblUnit"> <%= Html.Encode(Model.MiscellaneousVoucher.UNITNAME) %> </label></b>
                                                 </div>
                                                <div class="noPrint">    
                                                                         
                                                   <b> Name of Unit: <%= Html.Encode(Model.MiscellaneousVoucher.UNITNAME) %>  </b>

                                                    <%--<%= Html.DropDownList("MiscellaneousVoucher.UNITID", new SelectList(Model.GetCompanyUnit, "Value", "Text", Model.MiscellaneousVoucher.UNITID), "Select One", new { @class = "selectBoxRegular required" })%>--%>
                                                 </div>
                                            </div>                                        
                                        </div>
                                        
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td></td>
                                </tr>

                                <tr>
                                    <td colspan="2" align="center">
                                        <font size="3pt"><b>MISCELLANEOUS VOUCHER</b></font>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>                               
                                    <td style="width:10%">
                                        <b> Name: </b>
                                    </td>
                                    <td style="width:90%;border-bottom:1px solid black">
                                        <%= Html.Encode(Model.MiscellaneousVoucher.STREMPNAME) %> 
                                    </td>                                     
                                </tr>
                            </table>
                        </td>

                        <td  style="width:30%">
                            <table width="100%">
                                <tr>
                                    <td>
                                
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                            
                                <tr>
                                    <td>
                                        Voucher No. :
                                    </td>
                                    <td>
                                        <div class="Print">
                                            <b>
                                                <label id="lblVoucherNo"><%= Html.Encode(Model.MiscellaneousVoucher.VOUCHERNUMBER) %></label>
                                            </b>
                                        </div>
                                        <div class="noPrint">
                                            <%= Html.TextBoxFor(m=> m.MiscellaneousVoucher.VOUCHERNUMBER, new { @class = "textRegCustomWidth", @style = "width:100px" })%>
                                        </div>
                                        
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        Date :
                                    </td>
                                    <td>
                                        <div class="Print">
                                            <b>
                                                <label id="lblDate"> <% if (Model.MiscellaneousVoucher.APPROVEDDATE != null)
                                                                        { %> <%= Model.MiscellaneousVoucher.APPROVEDDATE.ToString("dd-MMM-yyyy")%> <%} %></label>
                                            </b>
                                        </div>
                                        <div class="noPrint">                                                                          
                                            <%= Html.TextBox("txtVoucherDate", DateTime.Now.ToString("dd-MMM-yyyy"), new { @class = "textRegCustomWidth", @style = "width:100px" , @readonly = "true" })%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td style="width:10%">
                                       <b> Designation:</b>
                                    </td>
                                    <td  style="width:40%;border-bottom:1px solid black">
                                      &nbsp;&nbsp;  <%= Html.DisplayFor(m => m.MiscellaneousVoucher.STRDESIGNATION)%>
                                    </td>
                                    <td  style="width:10%">
                                        <b> Department: </b>
                                    </td>
                                    <td  style="width:40%;border-bottom:1px solid black">
                                      &nbsp;&nbsp;  <%= Html.DisplayFor(m => m.MiscellaneousVoucher.STRDEPARTMENT)%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                  </tr>
                  <tr>
                        <td>
                            &nbsp;    
                        </td>
                   </tr>
                </table>
            </td>
        </tr>
    </table>

    <div id="grid" class="MainContainer">
        <div id="grid-data">
            <table width="100%" cellpadding="0" cellspacing="0">
                <colgroup>
                    <col  width="12%" align="center" />
                    <col  width="28%"  align="center" />
                    <col  width="13%" />
                    <col  width="25%" />
                    <col  width="22%" />
                    <col />
                </colgroup>
                <thead>
                    <tr style="border-bottom:1px dotted black">
                        <th align="center"  style="border:1px solid black">
                            Date
                        </th >
                        <th align="center"  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                            Particular
                        </th>
                        <th align="center" style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                            Amount
                        </th>
                        <th align="center"  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                            Purpose/Explanation
                        </th>   
                        <th align="center"  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                            Remarks
                        </th>  
                        <th class="noPrint" align="center"  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black">
                            Attachment
                        </th>               
                    </tr>
                </thead>
                <tbody>
                    <% if( Model.LstMISCDetails !=null) foreach (MISCDetails item in Model.LstMISCDetails)
                       { %>
                 
                        <tr>
                            <td  style="border:1px solid black">
                               <%= Html.Encode(Model.MiscellaneousVoucher.MISCDATE.ToString("dd-MMM-yyyy")) %>
                            </td>
                            <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" align="center">
                                &nbsp;&nbsp;<%= Html.Encode(item.STRPARTICULAR) %>
                            </td>
                            <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" align="right">
                                &nbsp;&nbsp; <%= Html.Encode(item.APPROVEDAMOUNT) %>
                            </td>
                            <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black"  align="center">
                                 &nbsp;&nbsp;<%= Html.Encode(item.STRPURPOSE)%>
                            </td>
                            <td style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black"  align="center">
                                &nbsp;&nbsp;<%= Html.Encode(item.STRREMARKS)%>
                            </td>
                            <td class="noPrint" style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black"  align="center">
                                <% if (item.ATTACHMENTPATH.Length > 0)
                                   { %>
                                        <a class="btnAttachment" onclick="return ShowFile('<%= item.MISCDETAISLID %>');" href="#" ></a>
                                <%} %>
                                &nbsp;
                            </td>
                        </tr>
                   

                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2" style="border:1px dotted black">
                            <div style="width:100%;float:left">
                                <div style="width:85%;float:left;border-bottom:1px dotted black">
                                   ( <label id="lblTest" style='width:100%;'></label>)
                                </div>    
                                <div style="width:13%;float:right;">
                                    Total&nbsp;
                                </div>
                            </div>
                        </td>                       
                        <td valign="top" align="right" style="border:1px solid black">
                            <%= Model.LstMISCDetails.Sum(c => c.APPROVEDAMOUNT)%>
                        </td>
                        <td style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >&nbsp;</td>
                        <td style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >&nbsp;</td>
                        <td class="noPrint" style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >&nbsp;</td>
                    </tr>
                    <%--<tr>
                        <td colspan="4" align="left">
                            (Attach Supporting Papers/Documents)
                        </td>
                    </tr>--%>
                </tfoot>
            </table>
        </div>
    </div>

    <div class="spacer">&nbsp;</div>
    <div class="spacer">&nbsp;</div>
    <div class="spacer">&nbsp;</div>


    <div class="FooterDiv">

         <table width="100%">
        <tr>
            <% for (int i = 0; i < Model.LstConveyanceApproverDetails.Count; i++)
               {%>
                
                <td valign="top">
                     <div style="border-top:1px solid black">
                        <%= Html.Encode(Model.LstConveyanceApproverDetails[i].STRAUTHORTYPE) %> By
                     </div>
                     <div>
                        <%= Html.Encode(Model.LstConveyanceApproverDetails[i].STREMPNAME) %>
                     </div>
                     <div>
                        <%= Html.Encode(Model.LstConveyanceApproverDetails[i].STRDESIGNATION) %>
                     </div>
                </td>
                   
             <% } %>

             
             <td valign="top">
                              
                <div style="border-top:1px solid black">
                   Received Payment
                </div>
                
                <div>
                    <%= Html.Encode(Model.MiscellaneousVoucher.STREMPNAME)%>
                </div>
                <div>
                    <%= Html.Encode(Model.MiscellaneousVoucher.STRDESIGNATION)%>
                </div>
                
             </td>

              <td valign="top">
                              
                <div style="border-top:1px solid black">
                    Paid By
                </div>
                
                <div>
                    <%= Html.Encode(LMS.Web.LoginInfo.Current.EmployeeName) %>
                </div>
                <div>
                    <%= Html.Encode(Model.StrDesignation) %>
                </div>
             </td>
        </tr>
    </table>
    </div>

    <div class="spacer">&nbsp;</div>
    <div class="spacer"></div>
    <div class="spacer"></div>
    <div class="spacer"></div>

    <div  class="divButton">
   <%-- <% if (Model.MiscellaneousVoucher.ISAPPROVED == "0")
       { %>
--%>
        <div class="noPrint">
            <a href="#" class="btnPaid" onclick="return Approve();"></a>
            <%--<a href="#" class="btnClose" onclick="return closeModalDialog();"></a>--%>
        </div>

        <%--<%} %>
        <%else
        { %>--%>
             <div class="Print">
                <a href="#" class="btnPrint noprint" id="btnPrint"></a>
                <a href="#" class="btnPrintPreview"  id="btnPrintPreview"></a>  
                <%--<a href="#" class="btnClose" onclick="return closeModalDialog();"></a>--%> 
            </div>
            
       <%-- <%} %>--%>

    </div>

    <% if (Model.MiscellaneousVoucher.ISAPPROVED == "0")
       { %>
       <div class="spacer"></div>

    <div id="divMsgStd" class="divMsg" style="text-align:center">
    
    </div>
     <%} %>


</div>

<div id="divDialog">
    <iframe id="styleOpenerView" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>


</form>
