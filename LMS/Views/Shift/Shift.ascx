<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ShiftModels>" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmShift"));

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#divStyleShift").dialog({ autoOpen: false, modal: true, height: 500, width: 740, resizable: false, title: 'Shift Information',
            close: function (ev, ui) {
                var pg = $('#txtPageNo').val();
                var targetDiv = '#divDataList';
                var url = '/LMS/Shift/Shift?page=' + pg;
                var form = $('#frmShift');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            }
        });

    });


    function Closing() {
        //window.location = "/LMS/Shift";    
    }

    function deleteShift(Id) {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeCustomAction({ intShiftID: Id }, '/LMS/Shift/Delete', 'divShiftList');
        }
        return false;
    }

//    $("#datepicker").datepicker({
//        changeMonth: true,
//        changeYear: false
//    }).click(function () { $('#Model_StrPeriodFrom').datepicker('show'); });


//    $("#datepicker1").datepicker({
//        changeMonth: true,
//        changeYear: false
//    }).click(function () { $('#Model_StrPeriodTo').datepicker('show'); });


    function popupStyleDetails(Id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/Shift/Details/' + Id;
        $('#styleOpenerShift').attr({ src: url });
        $("#divStyleShift").dialog('open');
        return false;
    }
    function popupStyleAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/Shift/ShiftAdd';
        $('#styleOpenerShift').attr({ src: url });
        $("#divStyleShift").dialog('open');
        return false;
    }

    function searchData() {

        var pdtAPFrom = $('#Model_StrPeriodFrom').val();
        var pdtAPTo = $('#Model_StrPeriodTo').val();
        var hookup = true;

        if (fnValidate() == true) {

            if (pdtAPFrom != '' || pdtAPTo != '') {

                if (checkDateValidation(pdtAPFrom, pdtAPTo) == true) {
                    hookup = true;
                }
                else {
                    hookup = false;
                }
            }

            if (hookup == true) {
                var targetDiv = '#divDataList';
                var url = '/LMS/Shift/Shift';
                var form = $('#frmShift');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) {
                    $(targetDiv).html(result);
                }, "html");
            }
        }
        return false;
    }

    function checkDateValidation(pdtAPFrom, pdtAPTo) {

        if (fnValidateDateTime() == false) {
            alert("Invalid Apply Date.");
            return false;
        }
        if (pdtAPFrom != '' && pdtAPTo != '') {
            if (pdtAPFrom > pdtAPTo) {
                alert("From Date must be smaller than or equal to 'To Date'.");
                return false;
            }
        }

        return true;
    }

</script>
<form id="frmShift" method="post" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td>
                Shift Name
            </td>
            <td colspan="3">
                <%=Html.TextBox("Model.strShiftName", Model.strShiftName, new { @class = "textRegular", @maxlength = 100 })%>
            </td>
            <td>
                <%--Is Roastering?--%>
            </td>
            <td>
                <%--<%= Html.CheckBox("Model.IsRoaster", Model.IsRoaster)%>--%>
            </td>
        </tr>
        <tr>
            <td>
                From Date
            </td>
            <td>
                <%= Html.TextBox("Model.StrPeriodFrom", Model.StrPeriodFrom, new { @class = "textRegularDate dtPicker dateNR" })%>
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td>
                To Date
            </td>
            <td>
                <%= Html.TextBox("Model.StrPeriodTo", Model.StrPeriodTo, new { @class = "textRegularDate dtPicker dateNR" })%>
                <%--<img alt="" id="datepicker1" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>      
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
            <td colspan="2" style="text-align: left">
                <a href="#" class="btnSearchData" onclick="return searchData();"></a>
            </td>
        </tr>
    </table>
</div>
<%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.Shift, LMS.Web.Permission.MenuOperation.Add))
  {%>
<div style="margin-top: 4px; margin-bottom: 3px;">
    <a href="#" class="btnAdd" onclick="return popupStyleAdd();"></a>
</div>
<%} %>
<div id="grid" style="margin-top: 2px;">
    <div id="grid-data" style="overflow: auto; width: 99%">
        <table>
            <thead>
                <tr>
                    <th>
                        Sift Name
                    </th>
                    <th>
                        From Date
                    </th>
                    <th>
                        To Date
                    </th>
                    <th>
                        In Time
                    </th>
                    <th>
                        Out Time
                    </th>
                    <th>
                        Effective Date
                    </th>
                    <%
                        bool isEditable = false;
                        if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.Shift, LMS.Web.Permission.MenuOperation.Edit))
                        {
                            isEditable = true;%>
                    <th>
                        Edit
                    </th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.Shift obj in Model.LstShiftPaged)
                   {
                   
                %>
                <tr>
                    <td>
                        <%=Html.Encode(obj.strShiftName)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strPeriodFrom)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strPeriodTo)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strIntime)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strOuttime)%>
                    </td>
                    <td>
                        <%=Html.Encode(obj.strEffectiveDate)%>
                    </td>
                    <%if (isEditable)
                      { %>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= obj.intShiftID%>);'>
                        </a>
                    </td>
                    <%} %>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
    <div class="pager">
        <%= Html.PagerWithScript(LMS.Web.AppConstant.PageSize10, ViewData.Model.LstShiftPaged.PageNumber, ViewData.Model.LstShiftPaged.TotalItemCount, "frmShift", "/LMS/Shift/Shift", "divDataList")%>
        <%= Html.Hidden("txtPageNo", ViewData.Model.LstShiftPaged.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.LstShift.Count.ToString()%></label>
</div>
<div class="spacer">
</div>
</form>
<div id="divStyleShift">
    <iframe id="styleOpenerShift" src="" width="99%" height="98%" style="border: 0px solid white;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
