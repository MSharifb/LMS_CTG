<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.WeekendHolidayAsWorkingdayModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

//        $("#datepicker").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#WeekendHolidayAsWorkingday_strEffectiveDateFrom').datepicker('show'); });

//        $("#datepicker1").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#WeekendHolidayAsWorkingday_strEffectiveDateTo').datepicker('show'); });

//        $("#datepicker2").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#WeekendHolidayAsWorkingday_strDeclarationDate').datepicker('show'); });

        preventSubmitOnEnter($("#frmWeekendHolidayAsWorkingdayDetails"));

        setTitle("Weekend/Holiday as Workingday");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy'
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });
       

        FormatTextBox();

    });

    function save() {

        if (fnValidate() == true && checkDateValidation() == true) {

            $('#btnSave').trigger('click');

        }
        return false;
    }

    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divWeekendHolidayAsWorkingdayDetails';
            var url = '/LMS/WeekendHolidayAsWorkingday/Delete';
            var form = $('#frmWeekendHolidayAsWorkingdayDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }

    function checkDateValidation() {

        var pdtAPFrom = $('#WeekendHolidayAsWorkingday_strEffectiveDateFrom').val();
        var pdtAPTo = $('#WeekendHolidayAsWorkingday_strEffectiveDateTo').val();

        if (pdtAPFrom != '' && pdtAPTo != '') {
            if (pdtAPFrom > pdtAPTo) {
                alert("Effective Date From must be smaller than or equal to 'Effective Date To'.");
                return false;
            }
        }    

        return true;
    }
    
</script>
<form id="frmWeekendHolidayAsWorkingdayDetails" method="post" action="">
<div id="divWeekendHolidayAsWorkingday">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m=>m.WeekendHolidayAsWorkingday.intWeekendWorkingday)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;margin-left:5px;">
        <colgroup>
            <col style="width: 25%" />
            <col style="width: 26%" />
            <col style="width: 23%" />
            <col style="width: 26%" />
        </colgroup>
        <tr>
            <td>
                Effective Date From<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.WeekendHolidayAsWorkingday.strEffectiveDateFrom, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td>
                Effective Date To<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.WeekendHolidayAsWorkingday.strEffectiveDateTo, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                <%--<img alt="" id="datepicker1" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
        </tr>
        <tr>
            <td>
                Description<label class="labelRequired">*</label>
            </td>
            <td colspan="3">
                <%=Html.TextAreaFor(m => m.WeekendHolidayAsWorkingday.strDescription, new { @class = "textRegular required", @style = "width:440px;", maxlength = 50 })%>
            </td>
        </tr>
       
        <tr>
            <td>
                Declaration Date<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.WeekendHolidayAsWorkingday.strDeclarationDate, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>
                <%--<img alt="" id="datepicker2" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.WeekendHolidayAsWorkingday.intWeekendWorkingday > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendHolidayAsWorkingday, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return save();"></a>
    <%} %>
    <%}
      else
      {%>
    <a href="#" class="btnSave" onclick="return save();"></a>
    <%} %>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendHolidayAsWorkingday, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.WeekendHolidayAsWorkingday.intWeekendWorkingday > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <input id="btnDelete" style="visibility: hidden;" name="btnDelete" type="submit"
        value="Delete" visible="false" />
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
