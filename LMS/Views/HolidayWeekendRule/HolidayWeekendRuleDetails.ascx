<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.HolidayWeekendRuleModels>" %>
<script type="text/javascript">


    $(document).ready(function () {

        preventSubmitOnEnter($("#frmHolidayWeekendRule"));

        setTitle("Holiday and Weekend Rule");

        $("#btnSave").hide();

        $("#btnDelete").hide();


        FormatTextBox();

        <% if(Model.HolidayWeekDayRule.intHolidayRuleID>0){ %>
            $("#trDays").hide();
        <%} %>

       

    });

    function save() {

        if (fnValidate() == true && getCheckedCount() == true) {

            $('#btnSave').trigger('click');

        }
        return false;
    }

    function getHolidayWeekDayListByYearId() {
        //--[clear all rows from combo]--------------------------
        $('#HolidayWeekDayRule_intHolidayWeekendID')
        .empty()
        .append('<option value="0">...Select One...</option>')
        .find('option:first')
        .attr("selected", "selected")
        ;

        //--[Fillup combo]--------------------------
        targetDiv = "#divHolidayWeekendRuleDetails";
        var url = "/LMS/HolidayWeekendRule/GetHolidayWeekDayListByYearId";

        var form = $("#frmHolidayWeekendRule");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        //--[select first row]--------------------------
        $('#HolidayWeekDayRule_intHolidayWeekendID')
        .find('option:first')
        .attr("selected", "selected")
        ;

        return false;
    }

    function getHolidayWeekDayDetails() {

        var targetDiv = "#divHolidayWeekendRuleDetails";
        var form = $("#frmHolidayWeekendRule");
        var serializedForm = form.serialize();
        var url = "/LMS/HolidayWeekendRule/GetHolidayWeekDayRuleDetails";

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        return false;
    }


    function Delete() {
        //var Id = $('#HolidayWeekDayRule_intHolidayRuleID').val();
        $('#HolidayWeekDayRule_IsDelete').val(true);

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            //executeCustomAction({ intHolidayRuleID: Id }, '/LMS/HolidayWeekendRule/Delete', 'divHolidayWeekendRuleDetails');

            var targetDiv = '#divHolidayWeekendRuleDetails';
            var url = '/LMS/HolidayWeekendRule/Delete';
            var form = $('#frmHolidayWeekendRule');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        }
        return false;
    }


    function SelectAndDeselectAll() {
        var IsSelect = $('#Model_bitIsSelectAll').attr('checked');

        if (IsSelect == false) {
            $('#lblSelect').html('Select All');
            $(".data").each(function (i) {
                if ($(this).is(':visible')) {
                    this.checked = false;
                }
            });
        }
        else {
            $('#lblSelect').html('Deselect All');
            $(".data").each(function (i) {
                if ($(this).is(':visible')) {
                    this.checked = true;   
                }
                
            });
        }

    }

    function GetSpecificWeekend(obj) {

        var classes="";
        $(".checkBox").each(function () {

            if (this.checked) {
                var css = $(this).attr('class');
                var classArray = new Array();
                classArray = css.split(' ');
                classes = classes + ' ' + classArray[1];
            }

        });
              
       
        $("#dtData tr").each(function () {
            var rowClass = $(this).attr('class');

            if (classes.length > 0) {
                if (classes.search(rowClass) < 0) {
                    $(this).hide();
                    $(this).find(".data").each(function () {
                        this.checked = false;
                    });
                }
                else {
                    $(this).show();
                    $(this).find(".data").each(function () {
                        this.checked = false;
                       
                        $('#Model_bitIsSelectAll').attr('checked',false);
                    });
                     $('#lblSelect').html('Select All');
                }

            }
            else {
                $(this).show();
                $(this).find(".data").each(function () {
                    this.checked = false;
                    
                    $('#Model_bitIsSelectAll').attr('checked', false);
                });
                $('#lblSelect').html('Select All');
            }
        });
         
          
    }   

    function getCheckedCount()
    {
        var count = 0;
        $(".data").each(function(){
            if(this.checked)
            {
                count++;
            }
        });

        if(count < 1)
        {
            alert('Please select the required checkbox for Weekend or Holiday.');
            return false;
        }

        return true;
    }
</script>
<form id="frmHolidayWeekendRule" method="post" action="">
<div id="divHolidayWeekendRule">
    <div class="divRow">
        <div class="divCol1">
            <%= Html.HiddenFor(m => m.IsDelete)%>
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.HolidayWeekDayRule.intHolidayRuleID)%>
        </div>
    </div>
    <div class="divRow">
        <div class="divSpacer">
        </div>
        <div class="divSpacer">
        </div>
        <table class="contenttable" style="width: 100%;">
            <tr>
                <td>
                    <table class="contenttable">
                        <tr>
                            <td class="contenttabletd">
                                Rule Name
                                <label class="labelRequired">
                                    *</label>
                            </td>
                            <td class="contenttabletd">
                                <%=Html.TextBoxFor(m => m.HolidayWeekDayRule.strHolidayRule, new { @class = "textRegular required",maxlength=50 })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="contenttabletd">
                                Leave Year
                                <label class="labelRequired">
                                    *</label>
                            </td>
                            <td class="contenttabletd">
                                <%= Html.DropDownListFor(m => m.HolidayWeekDayRule.intLeaveYearID, Model.LeaveYearList, "...Select One...", new { onchange = "return getHolidayWeekDayDetails();", @class = "selectBoxRegular required" })%>
                            </td>
                        </tr>
                    </table>
                    <div class="divSpacer">
                    </div>
                    <div class="divSpacer">
                    </div>
                    <table style="width: 100%;" class="contenttable">
                        <tr id="trDays">
                            <td>
                                Day(s)
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <%  if (Model.LstWeekendConfig != null) for (int i = 0; i < Model.LstWeekendConfig.Count; i++)
                                                {  %>
                                        <td>
                                            <%= Html.CheckBox("checkBox", new { @class = "checkBox " + Model.LstWeekendConfig[i].WeekDay, @onclick = "return GetSpecificWeekend(this);" })%><%= Html.Encode(Model.LstWeekendConfig[i].WeekDay) %>
                                        </td>
                                        <%} %>
                                        <td>
                                            <%= Html.CheckBox("chkHoliday", new { @onclick = "GetSpecificWeekend(this);", @class = "checkBox Holiday" })%>
                                            Holiday
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="contenttabletd" colspan="2">
                                <%=Html.CheckBox("Model_bitIsSelectAll", Model.bitIsSelectAll, new { onclick = "SelectAndDeselectAll();" })%><label
                                    id="lblSelect">Select All</label>
                                <div id="grid">
                                    <div id="grid-data">
                                        <table class="contenttext" style="width: 100%;">
                                            <thead>
                                                <tr>
                                                    <th style="width: 5%;">
                                                    </th>
                                                    <th style="width: 5%;">
                                                        #
                                                    </th>
                                                    <th style="width: 29%;">
                                                        Holiday Title
                                                    </th>
                                                    <th style="width: 19%; text-align: left;">
                                                        From Date
                                                    </th>
                                                    <th style="width: 20%; text-align: left;">
                                                        <label style="padding-left: 3px;">
                                                            To Date</label>
                                                    </th>
                                                    <th style="width: 10%; text-align: center;">
                                                        Days
                                                    </th>
                                                    <th style="width: 12%; text-align: left;">
                                                        Type
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                        <div style="overflow-y: auto; overflow-x: hidden; max-height: 150px">
                                            <table class="contenttext" style="width: 100%;" id="dtData">
                                                <%= Html.Hidden("count",Model.HolidayWeekDayRule.HolidayWeekDayRuleChild.Count) %>
                                                <%int j = 0; for (int i = 0; i < Model.HolidayWeekDayRule.HolidayWeekDayRuleChild.Count; i++)
                                                  {
                                                      j = j + 1;  
                                                %>
                                                <tr class='<%= ( (Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strHolidayTitle =="" || Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strHolidayTitle ==null)?Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].dtDateFrom.ToString("dddd"):"Holiday") %>'>
                                                    <td style="width: 5%;">
                                                        <%=Html.Hidden("HolidayWeekDayRule.HolidayWeekDayRuleChild[" + i.ToString() + "].intHolidayWeekendID", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].intHolidayWeekendID)%>
                                                        <%=Html.CheckBox("HolidayWeekDayRule.HolidayWeekDayRuleChild[" + i.ToString() + "].IsChecked", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].IsChecked, new { @class="data"})%>
                                                    </td>
                                                    <td style="width: 5%;">
                                                        <%=Html.Encode(j.ToString())%>
                                                    </td>
                                                    <td style="width: 28%; text-align: left;">
                                                        <%--<%=Html.Hidden("HolidayWeekDayRule.HolidayWeekDayRuleChild[" + i.ToString() + "].strHolidayTitle", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strHolidayTitle)%>--%>
                                                        <%=Html.Encode(Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strHolidayTitle == null ? "" : Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strHolidayTitle)%>
                                                        <%=Html.Hidden("HolidayWeekDayRule.HolidayWeekDayRuleChild[" + i.ToString() + "].strHolidayTitle", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strHolidayTitle)%>
                                                    </td>
                                                    <td style="width: 19%; text-align: left;">
                                                        <%=Html.Hidden("HolidayWeekDayRule.HolidayWeekDayRuleChild[" + i.ToString() + "].strDateFrom", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strDateFrom)%>
                                                        <%=Html.Encode(Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].dtDateFrom.ToString("ddd-dd MMM-yyyy"))%>
                                                        <%=Html.Hidden("HolidayWeekDay[" + i.ToString() + "].strDateFrom", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].dtDateFrom.ToString("dddd"))%>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <%=Html.Hidden("HolidayWeekDayRule.HolidayWeekDayRuleChild[" + i.ToString() + "].strDateTo", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strDateTo)%>
                                                        <%=Html.Encode(Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].dtDateTo.ToString("ddd-dd MMM-yyyy"))%>
                                                        <%=Html.Hidden("HolidayWeekDay[" + i.ToString() + "].strDateTo", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].dtDateFrom.ToString("dddd"))%>
                                                    </td>
                                                    <td style="text-align: right; width: 8%;">
                                                        <%=Html.Hidden("HolidayWeekDayRule.HolidayWeekDayRuleChild[" + i.ToString() + "].intDuration", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].intDuration)%>
                                                        <%=Html.Encode(Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].intDuration.ToString())%>
                                                    </td>
                                                    <td style="width: 12%; text-align: left;">
                                                        <%=Html.Hidden("HolidayWeekDayRule.HolidayWeekDayRuleChild[" + i.ToString() + "].strType", Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strType)%>
                                                        <%=Html.Encode(Model.HolidayWeekDayRule.HolidayWeekDayRuleChild[i].strType)%>
                                                    </td>
                                                </tr>
                                                <%} %>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.HolidayWeekDayRule.intHolidayRuleID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendAndHolidayRule, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.WeekendAndHolidayRule, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.HolidayWeekDayRule.intHolidayRuleID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
