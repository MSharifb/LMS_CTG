<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            setTitle("Report");

            $("#Reports").click();
        });

//        function preview(ctrl) {

////            if (fnValidate() == true) {

//                $.blockUI({ css: { border: 'none', padding: '15px', backgroundColor: '#000', '-webkit-border-radius': '10px', '-moz-border-radius': '10px', opacity: .5, color: '#fff'} });

////                var dofID = "";
////                var div = $(ctrl).parent().parent().parent();
////                $(div).find(".dofID").each(function () {
////                    dofID = this.id;
////                });

//                var form = $("#frmReports");
//                var serializedForm = form.serialize();
//                var url = "/oms/DyeingOrder/SaveWithoutRedirect";

//                $.post(url, serializedForm, function (result) {

//                    $("#divDyeingOrderAdd").html(result);

//                    $("#divReport").dialog("open");

//                    var orderID = $("#" + dofID).val();

//                    var frame = "<iframe id='rptViewer' src='/OMS/DyeingOrder/ViewReport/" + orderID + "' width='100%' height='100%'><p>Your browser does not support iframes.</p></iframe>";
//                    $("#divReport").html(frame);

//                    $.unblockUI();

//                }, "html");

//            }
//            return false;
//        } 

    </script>
    <div id="divDataList">
    </div>

</asp:Content>
