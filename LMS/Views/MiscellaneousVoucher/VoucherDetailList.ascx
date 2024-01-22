<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MiscellaneousVoucherModels>" %>


<%@ Import Namespace="LMSEntity" %>

<script type="text/javascript">
    $(document).ready(function () {

             
      

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
       // var mda = '<LINK href=' + '"<%= Url.Content("~/Content/css/PrintStyle.css") %>"' + ' type="text/css" rel="stylesheet" media="print"><base target="_self"></HEAD>';
        // alert(mda);
       // pp.document.writeln(mda);
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

    function SetForPrint() {
        $(".Print").show();
        $(".noPrint").hide();
        $("#lblUnit").text($("#ConveyanceObj_UNITNAME").val());
        $("#lblVoucherNo").text($("#ConveyanceObj_VOUCHERNUMBER").val());
        //        $("#lblDate").text($("#ConveyanceObj_APPROVEDDATE").val());

    }

    function Approve() {

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
                $("#btnPrint").show();
                $(".Print").show();
                $(".noPrint").hide();
                $("#lblDate").text($("#txtVoucherDate").val());

            }
        });

        }
        return false;
    }

</script>


<form id="MiscellaneousFrom" method="post" action="" >



<div id="printable">
<% foreach (int id in Model.IDList)
   {
     LMS.Web.Models.MiscellaneousVoucherModels model =   Model.GetVoucherDetails(id);   
        %>

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
                                                <img src="../../Content/img/controls/MohammadiLogo.jpg" />
                                                <%--<img id="Img1" alt=""  runat="server" src="~/Content/img/controls/MohammadiLogo.jpg" />    --%>
                                            </div>

                                            <div  style="width:80%;float:left">
                                                <img id="Img2" alt="" src="../../Content/img/controls/MGText.gif" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>                                    
                                    <td  colspan="2" align="center">
                                        <div style="width:100%">
                                           
                                            <div style="width:100%;text-align:center;">
                                                <div class="Print"> 
                                                  <b> Name of Unit:   <label id="lblUnit"> <%= Html.Encode(model.MiscellaneousVoucher.UNITNAME)%> </label></b>
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
                                        <%= Html.Encode(model.MiscellaneousVoucher.STREMPNAME)%> 
                                    </td>                                     
                                </tr>
                            </table>
                        </td>

                        <td  style="width:30%">
                            <table>
                                
                            
                                <tr>
                                    <td>
                                        Voucher No. :
                                    </td>
                                    <td>
                                        <div class="Print">
                                            <b>
                                                <label id="lblVoucherNo"><%= Html.Encode(model.MiscellaneousVoucher.VOUCHERNUMBER)%></label>
                                            </b>
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
                                                <label id="lblDate"> <% if (model.MiscellaneousVoucher.APPROVEDDATE != null)
                                                                        { %> <%= model.MiscellaneousVoucher.APPROVEDDATE.ToString("dd-MMM-yyyy")%> <%} %></label>
                                            </b>
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
                                      &nbsp;&nbsp;  <%= Html.Encode(model.MiscellaneousVoucher.STRDESIGNATION)%>
                                    </td>
                                    <td  style="width:10%">
                                        <b> Department: </b>
                                    </td>
                                    <td  style="width:40%;border-bottom:1px solid black">
                                      &nbsp;&nbsp;  <%= Html.Encode(model.MiscellaneousVoucher.STRDEPARTMENT)%>
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
                    <col  width="12%" />
                    <col  width="28%"  />
                    <col  width="13%" />
                    <col  width="25%" />
                    <col  width="22%" />
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
                    </tr>
                </thead>
                <tbody>
                    <% if (model.LstMISCDetails != null) foreach (MISCDetails item in model.LstMISCDetails)
                           { %>
                 
                        <tr>
                            <td  style="border:1px solid black">
                               <%= Html.Encode(model.MiscellaneousVoucher.MISCDATE.ToString("dd-MMM-yyyy"))%>
                            </td>
                            <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" align="center">
                                &nbsp;&nbsp;<%= Html.Encode(item.STRPARTICULAR)%>
                            </td>
                            <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" align="right">
                                &nbsp;&nbsp; <%= Html.Encode(item.APPROVEDAMOUNT)%>
                            </td>
                            <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black"  align="center">
                                 &nbsp;&nbsp;<%= Html.Encode(item.STRPURPOSE)%>
                            </td>
                            <td style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black"  align="center">
                                &nbsp;&nbsp;<%= Html.Encode(item.STRREMARKS)%>
                            </td>
                        </tr>
                   

                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2" style="border:1px dotted black">
                            <div style="width:100%;float:left">
                                <div style="width:85%;float:left;border-bottom:1px dotted black">
                                    (Taka
                                    <script type="text/javascript">
                                        document.write(toWords(<%= model.LstMISCDetails.Sum(c => c.APPROVEDAMOUNT).ToString() %>));
                                    </script>
                                     Only)
                                </div>    
                                <div style="width:13%;float:right;">
                                    Total
                                </div>
                            </div>
                        </td>                       
                        <td valign="top" align="right" style="border:1px solid black">
                            <%= model.LstMISCDetails.Sum(c => c.APPROVEDAMOUNT)%>
                        </td>
                        <td style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >&nbsp;</td>
                        <td style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >&nbsp;</td>
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
            <% for (int i = 0; i < model.LstConveyanceApproverDetails.Count; i++)
               {%>
                
                <td valign="top">
                     <div style="border-top:1px solid black">
                        <%= Html.Encode(model.LstConveyanceApproverDetails[i].STRAUTHORTYPE)%> By
                     </div>
                     <div>
                        <%= Html.Encode(model.LstConveyanceApproverDetails[i].STREMPNAME)%>
                     </div>
                     <div>
                        <%= Html.Encode(model.LstConveyanceApproverDetails[i].STRDESIGNATION)%>
                     </div>
                </td>
                   
             <% } %>

             
             <td valign="top">
                              
                <div style="border-top:1px solid black">
                   Received Payment
                </div>
                
                <div>
                    <%= Html.Encode(model.MiscellaneousVoucher.STREMPNAME)%>
                </div>
                <div>
                    <%= Html.Encode(model.MiscellaneousVoucher.STRDESIGNATION)%>
                </div>
                
             </td>

              <td valign="top">
                              
                <div style="border-top:1px solid black">
                    Paid By
                </div>
                
                <div>
                    <%= Html.Encode(LMS.Web.LoginInfo.Current.EmployeeName)%>
                </div>
                <div>
                    <%= Html.Encode(model.StrDesignation)%>
                </div>
             </td>
        </tr>
    </table>
    </div>

    <div class="divSpacer">&nbsp;</div>
    <div class="divSpacer"></div>
    <div class="divSpacer"></div>
    <div class="divSpacer"></div>
       

    <hr />

<%} %>

 <div class="divSpacer">&nbsp;</div>
    <div class="divSpacer">&nbsp;</div>
    <div class="divSpacer">&nbsp;</div>

<div  class="divButton">
   
             <div class="Print">
                <a href="#" class="btnPrint noprint" id="btnPrint"></a>
                <a href="#" class="btnPrintPreview"  id="btnPrintPreview"></a>   
            </div>
      
    </div>

</div>


</form>


