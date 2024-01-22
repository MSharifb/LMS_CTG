<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OfficeTimeModels>" %>
<script type="text/javascript">

 


    $(document).ready(function () {

        preventSubmitOnEnter($("#frmOfficeTimeDetails"));
        $("#btnSave").hide();
        $("#btnDelete").hide();
    //    $("#Model_OfficeTimeDetails_strStartTime").timepicker({ ampm: true });
        //    $("#Model_OfficeTimeDetails_strEndTime").timepicker({ ampm: true });

//        $(".timepicker").timepicker({ dateFormat: 'dd-mm-yy', changeYear: false
//            , showOn: 'button'
//            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
//            , buttonImageOnly: true
//        });

        $(".timepicker").timepicker({ ampm: true
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/clock2.png")%>'
            , buttonImageOnly: true
         
        });

        FormatTextBox();

    });


    function AddNew() 
    {
        if (fnValidate() == true) 
        {
            if (document.getElementById("Model_OfficeTimeDetails_fltDuration").value <= 0) {
                alert("Hr. value must be greater than zero.");            
            }
            else {
                var targetDiv = '#divOfficeTimeDetails';
                var url = '/LMS/OfficeTime/AddNewTime';
                var form = $('#frmOfficeTimeDetails');
                var serializedForm = form.serialize();

                $.post(url, serializedForm, function (result) {
                    $(targetDiv).html(result);
                    var isblank = document.getElementById("BlnTextBlank").value;
                    if (isblank == 'True') {
                        document.getElementById("Model_OfficeTimeDetails_strDurationName").value = '';
                        document.getElementById("Model_OfficeTimeDetails_strStartTime").value = '';
                        document.getElementById("Model_OfficeTimeDetails_strEndTime").value = '';
                        document.getElementById("Model_OfficeTimeDetails_fltDuration").value = 0;
                    }
                }, "html");
            }

        }
        return false;
    
    }

    function DeleteTime(Id) 
    {

        //alert(Id);
        var result = confirm('Pressing OK will remove this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = '#divOfficeTimeDetails';
            var url = '/LMS/OfficeTime/DeleteTime/' + Id;
            var form = $('#frmOfficeTimeDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }


    function save() {
//        if (fnValidate() == true) {
        if (document.getElementById("Model_OfficeTime_fltTotalDuration").value <= 0) {
                alert("Total Working Hour must be greater than zero.");
            }
            else {
                Id = $('#OfficeTime_strCompanyID').val();
                $('#Model_OfficeTime_fltDuration').val($('#Model_OfficeTime_fltTotalDuration').val());
                $('#btnSave').trigger('click');
            }
//        }
        return false;
    }

    function Delete() {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            executeAction('frmOfficeTimeDetails', '/LMS/OfficeTime/Delete', 'divOfficeTimeDetails');
        }
        return false;
    }

    function HookupCalculation(obj) {
        if (obj.id == 'Model_OfficeTimeDetails_strStartTime') {
            $('#Model_OfficeTimeDetails_strEndTime').val($('#Model_OfficeTimeDetails_strStartTime').val());
        }
                
        CalculateDuration();
        return false;
    }
   
    function CalculateDuration() {
            var targetDiv = '#divOfficeTimeDetails';
            var url = '/LMS/OfficeTime/CalcutateDuration';
            var form = $('#frmOfficeTimeDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {              
                if (result < 0) {
                    result = 0;
                    alert('Working To Time must be equal or greater than Working From Time.');
                }
                $('#Model_OfficeTimeDetails_fltDuration').val(result);
            }, "json");
        return false;
    }
 
</script>

<form id="frmOfficeTimeDetails" method="post" action="">
<div id="divOfficeTime">
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
            <input type="hidden" value="<%= Model.BlnTextBlank %>" name="BlnTextBlank" id="BlnTextBlank"
                class="textRegular" />
            <%= Html.Hidden("Model.rowID", Model.rowID)%>
        </div>
        <div class="divCol2">
            <%= Html.Hidden("Model.OfficeTime.strCompanyID", Model.OfficeTime.strCompanyID)%>
            <%= Html.Hidden("Model.OfficeTime.fltDuration", Model.OfficeTime.fltDuration)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 55%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Leave Year<label class="labelRequired">*</label>
            </td>
            <td>
                <%if (Model.rowID > 0)
                  { %>
                    <%= Html.Hidden("Model.OfficeTime.intLeaveYearID", Model.OfficeTime.intLeaveYearID)%>
                    <%= Html.TextBox("Model.OfficeTime.strYearTitle", Model.OfficeTime.strYearTitle, new { @class = "textRegular required", @readonly = "readonly" })%>
                <%}
                  else
                  {%>
                    <%= Html.DropDownList("Model.OfficeTime.intLeaveYearID", Model.LeaveYear, "...Select One...", new { @class = "selectBoxRegular required" })%>
                <%} %>
            </td>
        </tr>        
       
        <tr>
            <td>
                Duration Name<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBox("Model.OfficeTimeDetails.strDurationName", Model.OfficeTimeDetails.strDurationName, new { @class = "textRegular required", maxlength = 100 })%>
            </td>
        </tr>
        <tr>
            <td>
                Working Time<label class="labelRequired">*</label>
            </td>
            <td>
                <div style="width: 100%; float: left; text-align: left;">
                    <div style="float: left; text-align: left;">
                        <%=Html.TextBox("Model.OfficeTimeDetails.strStartTime", Model.OfficeTimeDetails.strStartTime, new { @class = "textRegularTime required timepicker", @onchange = "return HookupCalculation(this);" })%>                        
                    </div>
                    <div style="float: left; text-align: left; padding-left: 5px;">                        
                        To
                        <%=Html.TextBox("Model.OfficeTimeDetails.strEndTime", Model.OfficeTimeDetails.strEndTime, new { @class = "textRegularTime required timepicker", @onchange = "return HookupCalculation(this);" })%>
                    </div>  
                    <div style="float: left; text-align: left; padding-left: 5px;">                        
                        Hr.
                        <%=Html.TextBox("Model.OfficeTimeDetails.fltDuration", Model.OfficeTimeDetails.fltDuration, new { @class = "textRegularDuration",@readonly="readonly" })%>
                    </div>  
                    <div style="float: left; text-align: left; padding-left: 3px;"> 
                        <a href="#" style="margin-top:4px;" class="btnAddNew" onclick="return AddNew();"></a>
                    </div>                    
                </div>
            </td>
        </tr>   
        <tr>
            <td colspan="2">               
                <div id="grid">
                <div id="grid-data" style="overflow: auto; width: 99%;">
                    <table style="width: 99%;">
                        <thead>
                            <tr>
                                <th>
                                    Duration Name
                                </th>
                                <th style="width: 20%;">
                                    From Time
                                </th>
                                <th style="width: 20%;">
                                    To Time
                                </th>
                                <th style="width: 12%;">
                                    Hour(s)
                                </th>                                                               
                            </tr>
                        </thead>
                    </table>
                    <div style="overflow-y: auto; overflow-x: hidden; max-height: 75px">
                        <table style="width: 99%;">
                            <% double fltDur = 0; if (Model.LstOfficeTimeDetails != null)
                              {
                                for (int j = 0; j < Model.LstOfficeTimeDetails.Count; j++)
                                {
                                    fltDur = fltDur + Model.LstOfficeTimeDetails[j].fltDuration;
                                    //Model.OfficeTime.fltDuration = fltDur;
                            %>
                                <tr>
                                    <td>
                                        <%= Html.Hidden("Model.LstOfficeTimeDetails[" + j + "].intDurationID", Model.LstOfficeTimeDetails[j].intDurationID.ToString())%>
                                        <%= Html.Hidden("Model.LstOfficeTimeDetails[" + j + "].intLeaveYearID", Model.LstOfficeTimeDetails[j].intLeaveYearID.ToString())%>
                                        <%= Html.Hidden("Model.LstOfficeTimeDetails[" + j + "].strDurationName", Model.LstOfficeTimeDetails[j].strDurationName.ToString())%>
                                        <%= Html.Encode(Model.LstOfficeTimeDetails[j].strDurationName.ToString())%>                                    
                                    </td>
                                    <td style="width: 20%;">
                                        <%= Html.Hidden("Model.LstOfficeTimeDetails[" + j + "].strStartTime", Model.LstOfficeTimeDetails[j].strStartTime.ToString())%>
                                        <%= Html.Encode(Model.LstOfficeTimeDetails[j].strStartTime.ToString())%>
                                    </td>
                                    <td style="width: 20%;">
                                        <%= Html.Hidden("Model.LstOfficeTimeDetails[" + j + "].strEndTime", Model.LstOfficeTimeDetails[j].strEndTime.ToString())%>
                                        <%= Html.Encode(Model.LstOfficeTimeDetails[j].strEndTime.ToString())%>
                                    </td>
                                    <td style="width: 12%;">
                                        <%= Html.Hidden("Model.LstOfficeTimeDetails[" + j + "].fltDuration", Model.LstOfficeTimeDetails[j].fltDuration.ToString())%>
                                        <div style="float: left; text-align: left;">
                                        <%= Html.Encode(Model.LstOfficeTimeDetails[j].fltDuration.ToString("#0.00"))%>
                                        </div>
                                        <div style="float: left; text-align: left; padding-left: 3px;"> 
                                            <a href='#' class="gridDelete" onclick="javascript:return DeleteTime('<%= Model.LstOfficeTimeDetails[j].intDurationID %>');">
                                            </a>
                                        </div>
                                    </td>                                
                                </tr>
                                <%}

                           } %>
                        </table>
                    </div>
                </div>
                </div>
            </td>
        </tr>
         <tr>
            <td>
                Total Working Hour<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBox("Model.OfficeTime.fltTotalDuration", fltDur, new { @class = "textRegular required double", @readonly = "readonly", maxlength = 5 })%>
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
    <%if (Model.rowID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.OfficeTime, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return save();"></a>
    <%} %>
    <%}
      else
      {%>
    <a href="#" class="btnSave" onclick="return save();"></a>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>

