<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MyConveyanceModels>" %>
<%@ Import Namespace="LMSEntity" %>

<script type="text/javascript">
    $(document).ready(function () {
        
        $("#lblTest").text(convert(<%= Model.LstConveyanceDetails.Sum(c => c.AMOUNT).ToString() %>));

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

       <% if( Model.ConveyanceObj.ISAPPROVED == true) %>
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
      
    function SetForPrint()
    {
        $(".Print").show();
        $(".noPrint").hide();        
        $("#lblUnit").text($("#ConveyanceObj_UNITNAME").val());
        $("#lblVoucherNo").text($("#ConveyanceObj_VOUCHERNUMBER").val());
//        $("#lblDate").text($("#ConveyanceObj_APPROVEDDATE").val());
    
    }

    function Approve() {
           
        var targetDiv = "#divDataList";
        var url = "/LMS/Conveyance/Approve";
        var form = $("#ConveyanceFrom");
        var serializedForm = form.serialize();

        $.getJSON(url, serializedForm, function (result) {   
            
            if(result.Message.search('Successfull')>-1)
            {
                $("#divMsgStd").html(result.Message);
                $(".btnPaid").hide();
                window.parent.searchConvData();
                SetForPrint();
                $("#btnPrint").show();
                $("#lblDate").text($("#txtVoucherDate").val());
                
            }
        });

      
    return false;
}

</script>

<form id="ConveyanceFrom" method="post" action="" enctype="multipart/form-data">

<%= Html.HiddenFor(m=> m.ConveyanceObj.CONVEYANCEID) %>
<%= Html.HiddenFor(m=> m.ConveyanceObj.ISAPPROVED) %>
<%= Html.HiddenFor(m => m.LstConveyanceDetails)%>
<%= Html.HiddenFor(m=> m.ConveyanceObj.STRDEPARTMENT) %>

<div id="printable">


<table width="100%">
    <tr>
        <td>
            <table  width="100%">
                <tr>
                    <td  style="width:70%">
                        <table  width="100%">
                            <tr>
                                <td  colspan="2" align="center">
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
                                <td  colspan="2" align="right">                                    
                                    <div class="noPrint"> 
                                       <b> Name Of Unit: <%= Html.Encode(Model.ConveyanceObj.UNITNAME) %>                                       
                                     </div>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2" style=" text-align:right;padding-right:100px"  >
                                    <font size="3pt"><b>CONVEYANCE VOUCHER</b></font>
                                </td>
                            </tr>

                            <tr>                               
                                <td style="width:10%">
                                    <b> Name: </b> 
                                </td>
                                <td style="width:90%;border-bottom:1px solid black">
                                    <b> <%= Html.DisplayFor(m=> m.ConveyanceObj.STREMPNAME)%></b> 
                                </td>                                     
                            </tr>
                        </table>
                    </td>
                    <td  style="width:30%">
                        <table>
                            <tr>
                                <td>
                                
                                </td>
                                <td>
                                    Voucher Number
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                
                                </td>
                                <td>
                                    <div class="Print">
                                        <b>
                                            <label id="lblVoucherNo"></label>
                                        </b>
                                    </div>
                                    <div class="noPrint">
                                        <%= Html.Encode(Model.ConveyanceObj.VOUCHERNUMBER) %>
                                    </div>
                                        
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    Date :
                                </td>
                                <td>                                   
                                    <div class="noPrint">                                       
                                         <label id="Label1"> <% if (Model.ConveyanceObj.APPROVEDDATE != null && Model.ConveyanceObj.APPROVEDDATE != DateTime.MinValue)
                                                                    { %> <%= Model.ConveyanceObj.APPROVEDDATE.ToString("dd-MMM-yyyy")%> <%} %></label>                             
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
                                  &nbsp;&nbsp; <b> <%= Html.DisplayFor(m => m.ConveyanceObj.STRDESIGNATION)%></b>
                                </td>
                                <td  style="width:10%">
                                    <b> Department: </b>
                                </td>
                                <td  style="width:40%;border-bottom:1px solid black">
                                  &nbsp;&nbsp;  <b> <%= Html.DisplayFor(m => m.ConveyanceObj.STRDEPARTMENT)%></b>
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
                <col  width="10%" />
                <col  width="20%"  />
                <col  width="20%" />
                <col  width="15%" />
                <col  width="15%" />
                <col  width="20%" />
            </colgroup>
            <thead>
                <tr style="border-bottom:1px dotted black">
                    <th align="center"  style="border:1px solid black">
                        Date
                    </th >
                    <th align="center"  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                        From
                    </th>
                    <th align="center" style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                        To
                    </th>
                    <th align="center"  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                        Mode
                    </th>
                    <th align="center" style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                        Amount
                    </th>
                    <th align="center" style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                        Purpose/Explanation
                    </th>
                </tr>
            </thead>
            <tbody>
                <% foreach (MyConveyanceDetails item in Model.LstConveyanceDetails)
                   { %>
                 
                    <tr>
                        <td  style="border:1px solid black">
                            <%= Html.Encode(item.OUTOFOFFICEDATE.ToString("dd-MMM-yyyy"))%>
                        </td>
                        <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" align="center">
                            &nbsp;&nbsp;<%= Html.Encode(item.FROMLOCATION)%>
                        </td>
                        <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                            &nbsp;&nbsp; <%= Html.Encode(item.TOLOCATION)%>
                        </td>
                        <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black"  align="center">
                             &nbsp;&nbsp;<%= Html.Encode(item.MODE)%>
                        </td>
                        <td align="right" style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >
                            <%= Html.Encode(item.AMOUNT)%>                           
                        </td>
                        <td  style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black"  align="center">
                           &nbsp; <%= Html.Encode(item.PURPOSE)%>
                        </td>
                    </tr>
                   

                <%} %>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="3" style="border-bottom:1px dotted black">
                        <label id="lblTest" style='width:100%;'></label>                        
                    </td>
                    <td align="right" style='border-left:0px'>
                        Total &nbsp;
                    </td>
                    <td align="right" style="border:1px solid black">
                        <%= Model.LstConveyanceDetails.Sum(c=>c.AMOUNT) %>
                    </td>
                    <td style="border-top:1px solid black;border-right:1px solid black;border-bottom:1px solid black" >&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6" align="left">
                        (Attach Supporting if any)
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</div>

<div class="spacer">&nbsp;</div>
<div class="spacer">&nbsp;</div>
<div class="spacer">&nbsp;</div>


<div  class="divButton">
     <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>


<div class="spacer">&nbsp;</div>
<div class="spacer"></div>
<div class="spacer"></div>
<div class="spacer"></div>

 </div>
</form>

