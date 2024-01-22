<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.AttendanceStatusModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmAttStatus"));

        setTitle("Attendence Status Setup");

        $("#btnSave").hide();
        FormatTextBox();


    });


    function save() {

        if (fnValidate() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/AttendanceStatus/AttendanceStatus";
            var form = $("#frmAttStatus");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);

            }, "html");

        }

        return false;
    }        
    
</script>
<form id="frmAttStatus" method="post" action="">
<div id="divAttStatus">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.AtdStatusSetup.intRowID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 50%" />
            <col />
        </colgroup>
        <tr>
            <td colspan="3" style="text-align: center; font-weight: bold">
                Daily Attendance Status
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Present:
            </td>
            <td style="width: 100%;" class="contenttabletd">
                <%=Html.TextBoxFor(m=> m.AtdStatusSetup.strPresent, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Early Arrival:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strEarlyArrival, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Early Departure:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strEarlyDeparture, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Late Arrival:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strLateArrival, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Late Departure:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strLateDeparture, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Absent:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strAbsent, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Leave:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strLeave, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Out Station Duty:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strOSD, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Weekend:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strWeekend, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Holiday:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.AtdStatusSetup.strHoliday, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
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
    <%if (Model.AtdStatusSetup.intRowID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.AttendanceStatus, LMS.Web.Permission.MenuOperation.Edit))
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
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
