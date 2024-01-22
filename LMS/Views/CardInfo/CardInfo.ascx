<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CardInfoModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmCardInfo"));

        $("#divStyleCardInfo").dialog({ autoOpen: false, modal: true, height: 200, width: 500, resizable: false, title: 'Card Info',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/CardInfo/CardInfo?page=' + pg;
                var form = $('#frmCardInfo');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });
    
    function Closing() {
        //window.location = "/CardInfo";
    }
    
        function deleteCardInfo(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intCardInfoID: Id }, '/LMS/CardInfo/Delete', 'divCardInfoDetails');

        }
        return false;
    }
        
    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/CardInfo/Details/' + Id;

        $('#styleOpenerCardInfo').attr({ src: url });
        $("#divStyleCardInfo").dialog('open');
        return false;
    }


    function popupStyleAdd() {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/CardInfo/CardInfoAdd';

        $('#styleOpenerCardInfo').attr({ src: url });
        $("#divStyleCardInfo").dialog('open');
        return false;
    }


    function searchData() {

        var targetDiv = "#divDataList";
        var url = "/LMS/CardInfo/CardInfo";
        var form = $("#frmCardInfo");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        return false;
    }

    /*remove the class which made red color of the drop down list */
    $(function () {
        $("select").each(function () {
            if ($(this).hasClass('input-validation-error')) {
                $(this).removeClass('input-validation-error');
            }
        })
    });
</script>
<form id="frmCardInfo" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CardInfo, LMS.Web.Permission.MenuOperation.Add))
              {%>
            <td>
                <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
            </td>
            <%} %>
            <td align="right" style="width: 80%;">
             Status
             <%= Html.DropDownList("Model.intSearchStatus", Model.CardStatus, new { @class = "selectBoxRegular", onChange="searchData();" })%>
            </td>
            
            <%--<td align="right" style="width: 10%;">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>--%>
        </tr>
    </table>
</div>
<div id="grid">
    <div id="grid-data" style="overflow: auto; height:400px; width: 99%">
        <table>
            <thead>
                <tr>
                    <th>
                        Card ID
                    </th>
                    <th>
                       Status
                    </th>                    
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CardInfo, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th style="width:20px">
                        Edit
                    </th>
                    <%} %>
                    <%-- <th>
                        Delete
                    </th>--%>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.CardInfo lr in Model.LstCardInfoPaging)
                   { 
                %>
                <tr>
                    <td>
                        <%=Html.Encode(lr.strCardID)%>
                    </td>
                    <td>
                        <%=Html.Encode(lr.intStatus == 1 ? "Assigned" : "Available")%>                        
                    </td>                           
                    <%if (isEditable)
                      {
                          if (lr.intStatus == 0)
                          { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= lr.intCardID  %>);'>
                        </a>
                    </td>
                    <%}
                      } %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstCardInfoPaging.PageNumber, ViewData.Model.LstCardInfoPaging.TotalItemCount, "frmCardInfo", "/LMS/CardInfo/CardInfo", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstCardInfoPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstCardInfo.Count.ToString() %></label>
</div>

</form>
<div id="divStyleCardInfo">
    <iframe id="styleOpenerCardInfo" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
