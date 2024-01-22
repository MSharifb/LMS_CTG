<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CardInfoModels>" %>
<%@ Import Namespace="LMS.Web.Models" %>
<%@ Import Namespace="MvcPaging" %>

<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmCardInfoSearch"));
    });

    function Closing() {

    }

    function setCardInfo(intCardID, strCardID) {
        window.parent.setCardInfo(intCardID, strCardID);
    }

    function closeDialog() {
        alert('sd');
        window.parent.closeCardInfo();
    }
    function searchCData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/CardInfo/CardInfo";
        var form = $("#frmCardInfoSearch");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }

</script>
<% CardInfoModels model = new CardInfoModels(); %>
<form id="frmCardInfoSearch" method="post" action="">
<div id="grid">
    <div id="grid-data">
        <table>
            <thead>
                <tr>
                    <th>
                    </th>
                    <th>
                        ID
                    </th>
                    
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.CardInfo obj in Model.LstCardInfoPaging)
                   { 
                %>
                <tr>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return setCardInfo("<%=obj.intCardID%>","<%=obj.strCardID%>");'>
                        </a>
                    </td>
                    <td>
                        <%= Html.Encode(obj.strCardID)%>
                    </td>
                    
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
</div>
<div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstCardInfoPaging.PageNumber, ViewData.Model.LstCardInfoPaging.TotalItemCount, "frmCardInfo", "/LMS/CardInfo/CardInfo", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstCardInfoPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstCardInfo.Count.ToString() %></label>
</form>

