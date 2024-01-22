<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.HolidayWeekDayModels>" %>
<script type="text/javascript">
    $(document).ready(function () {

    
        preventSubmitOnEnter($("#frmHolidayWeekDay"));

        setTitle("Weekend  Holiday");

        $("#btnSave").hide();
        $("#btnDelete").hide();
      
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });


        OptionWisePageRefresh();
        calculateDays();
        getLeaveYearInfo();

        FormatTextBox();

        checkAutomatic();

         <% if(Model.HolidayWeekDay.intHolidayWeekendMasterID >0){ %>
            AlternateWeekend();
        <%} %>

    });



    function OptionWisePageRefresh() {

        var IsHoliday = $("#Model_HolidayWeekDay_IsHoliday").attr('checked');
        $('#HolidayWeekDay_IsHoliday').val(IsHoliday);

        if (document.getElementById('HolidayWeekDay_isAutomatic').checked) {
            $(".manual").hide();
            $(".automatic").show();
        }
        else {
            $(".manual").show();
            $(".automatic").hide();
        }
    

        if (IsHoliday == true) {
         
            $("#HolidayWeekDay_strHolidayTitle").addClass('required');
            $(".holiday").show();
            $(".weekend").hide();
             $(".automatic").hide();

        }
        else {
            
            $('#HolidayWeekDay_strHolidayTitle').val("");
            $("#HolidayWeekDay_strHolidayTitle").removeClass('required');

            $(".holiday").hide();
            $(".weekend").show();
            var isEdit = $("#HolidayWeekDay_intHolidayWeekendID").val();

            if (isEdit > 0) {
                $(".hideAutomatic").hide();
            }

        }

    }

    function save() {

        if (confirm('Do you want to proceed?') == true) {

            document.getElementById('imgLoader').style.visibility = 'visible';

            if (fnValidateDateTime() == false) {
                alert("Invalid Date.");
                return false;
            }
            if (fnValidate() == true) {


                $('#btnSave').trigger('click');
            }
        }
        return false;
    }

    function Delete() {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divHolidayWeekDayDetails';
            var url = '/LMS/HolidayWeekDay/Delete';
            var form = $('#frmHolidayWeekDay');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }



    function calculateDays() {
        pStartDate = $('#HolidayWeekDay_strDateFrom').val();
        pEndDate = $('#HolidayWeekDay_strDateTo').val();

        if (pStartDate != "") {
            $.post('/LMS/HolidayWeekDay/CalculateDays', { strStartDate: pStartDate, strEndDate: pEndDate }, function (result) {
                $("#HolidayWeekDay_intDuration").val(result);
            }, "json");
        }
        else {
            $("#HolidayWeekDay_intDuration").val(0);
        }
        return false;
    }


    function getLeaveYearInfo() {

        var pintYearID = $('#HolidayWeekDay_intLeaveYearID').val();
        var targetDiv = "#divHolidayWeekDayDetails";
        var url = "/LMS/HolidayWeekDay/GetLeaveYearInfo/" + pintYearID;
        var form = $("#frmHolidayWeekDay");
        var serializedForm = form.serialize();

        

        if (pintYearID > 0) {
            $("#IntLeaveYearId").val(pintYearID);
            $.post(url, serializedForm, function (result) {
                $('#StrYearStartDate').val(result[0]);
                $('#StrYearEndDate').val(result[1]);
                $('#StrFromDate').val(result[0]);
                $('#StrToDate').val(result[1]);

                <% if(Model.HolidayWeekDay.intHolidayWeekendMasterID <1){ %>
                if (document.getElementById('HolidayWeekDay_isAutomatic').checked) {

                    $("#HolidayWeekDay_strDateFrom").val(result[0]);
                    $("#HolidayWeekDay_strDateTo").val(result[1]);
                }
                else {
                    $("#HolidayWeekDay_strDateFrom").val(result[0]);
                    $("#HolidayWeekDay_strDateTo").val(result[0]);
                }
                <%} %>

            }, "json");
        }
        else {
            $('#StrYearStartDate').val("");
            $('#StrYearEndDate').val("");
        }

        return false;


    }

    function checkAutomatic() {
        var chk = document.getElementById('HolidayWeekDay_isAutomatic').checked;

        if (chk) {
            $('.automatic').show();
            $('.manual').hide();

             <% if(Model.HolidayWeekDay.intHolidayWeekendMasterID <1){ %>
            $("#HolidayWeekDay_strDateFrom").val($('#StrYearStartDate').val());
            $("#HolidayWeekDay_strDateTo").val($('#StrYearEndDate').val());
            <%} %>
        } else {
            $('.automatic').hide();
            $('.manual').show();

             <% if(Model.HolidayWeekDay.intHolidayWeekendMasterID <1){ %>
            $("#HolidayWeekDay_strDateFrom").val($('#StrYearStartDate').val());
            $("#HolidayWeekDay_strDateTo").val($('#StrYearStartDate').val());
            <%} %>
        }

    }

    function AlternateWeekend()
    {
       
        $(".alternate").each(function(){
           
            if(this.checked)
            {
                var chkBoxId = this.id;
                chkBoxId = chkBoxId.replace('IsAlternate', 'IsWeekend_FirstDayOfYear'); 
                $("#" + chkBoxId).removeAttr("disabled"); 
                $(".alternate").attr('disabled', "disabled");
                $(this).removeAttr("disabled"); 
            }
            
        });
    }

    function chkAlternateWeekend(obj) {

        
        var chkBoxId = obj.id;
        chkBoxId = chkBoxId.replace('IsAlternate', 'IsWeekend_FirstDayOfYear');       
        if (obj.checked) {

            $(".alternate").each(function(){                
                
                if(this.id != obj.id)
                {
                $(this).attr('disabled', "disabled");
                $(this).attr('checked', false);
                }

            });


            $("#" + chkBoxId).removeAttr("disabled");                        
        }
        else {
            $("#"+chkBoxId).attr('disabled', "disabled");
            $("#"+chkBoxId).attr('checked', false);

             $(".alternate").each(function(){                
                
                if(this.id != obj.id)
                {
                     $(this).removeAttr("disabled");
                    $(this).attr('checked', false);
                }

            });

        }
    }


</script>
<form id="frmHolidayWeekDay" method="post" action="">
<div id="divHolidayWeekDay">
    <%=Html.HiddenFor(m=> m.StrYearStartDate)%>
    <%=Html.HiddenFor(m=> m.StrYearEndDate)%>
    <%=Html.HiddenFor(m=> m.IntLeaveYearId)%>
    <div class="divRow">
        <div class="divCol1">
            <%= Html.HiddenFor(m => m.HolidayWeekDay.intHolidayWeekendID)%>
            <%= Html.HiddenFor(m => m.HolidayWeekDay.intHolidayWeekendMasterID)%>
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.HolidayWeekDay.strCompanyID)%>
        </div>
    </div>
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td width="40%">
                Leave Year<label class="labelRequired">*</label>
            </td>
            <td>
                <%if (Model.HolidayWeekDay.intHolidayWeekendID > 0)
                  { %>
                <%= Html.HiddenFor(m => m.HolidayWeekDay.intLeaveYearID)%>
                <%= Html.TextBoxFor(m => m.HolidayWeekDay.strYearTitle, new { @class = "textRegular", @readonly = "readonly" })%>
                <%}
                  else
                  {%>
                <%= Html.DropDownListFor(m => m.HolidayWeekDay.intLeaveYearID, Model.LeaveYear, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return getLeaveYearInfo();" })%>
                <%} %>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <%=Html.HiddenFor(m => m.HolidayWeekDay.IsHoliday)%>
                <%=Html.RadioButton("Model_HolidayWeekDay_IsHoliday", true, Model.HolidayWeekDay.IsHoliday, new { onClick = "OptionWisePageRefresh();" })%>Holiday
                <%=Html.RadioButton("Model_HolidayWeekDay_IsHoliday", true, !Model.HolidayWeekDay.IsHoliday, new { onClick = "OptionWisePageRefresh();" })%>Weekend
                <span class="weekend hideAutomatic">&nbsp;
                    <%=Html.CheckBoxFor(m => m.HolidayWeekDay.isAutomatic, new { onClick = "checkAutomatic();" })%>
                    <label for="IsAutmatic">
                        Automatic</label>
                </span>
            </td>
        </tr>
        <tr class="holiday">
            <td>
                <div class="holiday">
                    Holiday Title<label class="labelRequired">*</label>
                </div>
            </td>
            <td>
                <div class="holiday">
                    <%=Html.TextBoxFor(m => m.HolidayWeekDay.strHolidayTitle, new { @class = "textRegular", maxlength=50 })%>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 0px">
                <table width="100%">
                    <tr>
                        <td width="40%">
                            From Date<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.HolidayWeekDay.strDateFrom, new { @class = "textRegularDate dtPicker date", maxlength = 10, onchange = "calculateDays();" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            To Date<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.HolidayWeekDay.strDateTo, new { @class = "textRegularDate dtPicker date", maxlength = 10, onchange = "calculateDays();" })%>
                        </td>
                    </tr>
                    <tr class="manual">
                        <td>
                            Days<label class="labelRequired">*</label>
                        </td>
                        <td>
                            <%=Html.TextBoxFor(m => m.HolidayWeekDay.intDuration, new { @class = "textRegularNumber required", @readonly = "readonly" })%>
                        </td>
                    </tr>
                </table>
                <table width="100%" class="automatic">
                    <thead>
                        <tr>
                            <th>
                                Weekend
                            </th>
                            <th>
                                Week Day
                            </th>
                            <th>
                                Full/Half Day
                            </th>
                            <th>
                                Alternate Day
                            </th>
                            <th>
                                Effective From First Weekend
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <%  if (Model.LstWeekendConfig != null) for (int i = 0; i < Model.LstWeekendConfig.Count; i++)
                                { %>
                        <tr>
                            <td style="text-align: center;">
                                <%= Html.CheckBoxFor(m=> m.LstWeekendConfig[i].IsWeekend) %>
                            </td>
                            <td>
                                <%= Html.Encode(Model.LstWeekendConfig[i].WeekDay) %>
                                <%= Html.HiddenFor(m=> m.LstWeekendConfig[i].WeekDay) %>
                            </td>
                            <td style="text-align: center;">
                                <%= Html.DropDownListFor(m=> m.LstWeekendConfig[i].intDurationID, Model.WorkingTime, "...Full Day...", new { @class = "selectBoxRegular", @style = "width:130px; min-width:130px;" })%>
                            </td>
                            <td style="text-align: center;">
                                <%= Html.CheckBoxFor(m => m.LstWeekendConfig[i].IsAlternate, new { @onclick = "chkAlternateWeekend(this);" , @class="alternate" })%>
                            </td>
                            <td style="text-align: center;">
                                <% if (Model.LstWeekendConfig[i].IsWeekend_FirstDayOfYear == true)
                                   { %>
                                   <%= Html.CheckBoxFor(m => m.LstWeekendConfig[i].IsWeekend_FirstDayOfYear)%>
                                <%} %>
                                <% else %>
                                    <%= Html.CheckBoxFor(m => m.LstWeekendConfig[i].IsWeekend_FirstDayOfYear, new { @disabled="true" })%>
                            </td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>
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
    <%if (Model.HolidayWeekDay.intHolidayWeekendID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendAndHoliday, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendAndHoliday, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.HolidayWeekDay.intHolidayWeekendMasterID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div style="position: absolute">
    <img id="imgLoader" src="/LMS/Content/ajax-loader.gif" style="visibility: hidden;" />
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
