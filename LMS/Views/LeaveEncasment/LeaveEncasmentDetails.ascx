<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveEncasmentModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveEncasment"));

        setTitle("Leave Encasment");

        $("#btnSave").hide();
        $("#btnDelete").hide();
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 430, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        getLeaveBalance();

        FormatTextBox();

        document.getElementById('hdnEmpId').value = document.getElementById('LeaveEncasment_strEmpID').value;

        var encashType = $("#LeaveEncasment_strIsIndividual").val();


        if (encashType == "Individual") {
            showApplicantType(0);
            $("#Option[value='0']").attr("checked", true);
            $("#Option[value='0']").attr("checked", "checked");
            $("#Option[value='0']").click();

            //  $("#Option").val(0);
        }
        else if (encashType == "All") {
            showApplicantType(1);
            $("#Option[value='1']").prop("checked", true);            
            // $("#Option").val("1");
        }
        else
            showApplicantType(0);
    });


    function searchEmployee() {
        var url = '/LMS/Employee/EmployeeList';
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'text',
            timeout: 5000,
            error: function () {
                alert('System is unable to load data please try again.');
            },
            success: function (result) {
                $('#divEmpList').html(result);
            }
        });

        $("#divEmpList").dialog('open');
        return false;
    }

    function Closing() {

    }

    function setData(id, name) {
        document.getElementById('LeaveEncasment_strEmpID').value = id;
        document.getElementById('LeaveEncasment_strEmpName').value = name;

        document.getElementById('hdnEmpId').value = id;
        $("#lblIdNotFound").css('visibility', 'hidden');
        $('#LeaveEncasment_strEmpID').removeClass("invalid");
        
        $("#divEmpList").dialog('close');
        getLeaveBalance();
    }


    function removeRequired() {
        $('#LeaveEncasment_strEmpID').val("");
        $('#LeaveEncasment_strEmpName').val("");
        executeAction('frmLeaveEncasment', '/LMS/LeaveEncasment/OptionWisePageRefresh', 'divLeaveEncasmentDetails');

        $('#LeaveEncasment_strEmpID').removeClass("required");
        $('#LeaveEncasment_strEmpName').removeClass("required");

    }

    function addRequired() {
        $('#LeaveEncasment_strEmpID').addClass("required");
        $('#LeaveEncasment_strEmpName').addClass("required");

    }

    function save() {

        if(confirm('Do you want to proceed?') == true ) {

            document.getElementById('imgLoader').style.visibility = 'visible';

        if (document.getElementById('hdnEmpId').value != document.getElementById('LeaveEncasment_strEmpID').value) {
            $('#LeaveEncasment_strEmpID').addClass("invalid");
        } else {
            $('#LeaveEncasment_strEmpID').removeClass("invalid");
        }
        
        if (fnValidate() == true) {

            $('#btnSave').trigger('click');


        }
        }
        return false;
    }


    function getLeaveBalance() {

        var targetDiv = '#divLeaveEncasmentDetails';
        var form = $('#frmLeaveEncasment');
        var serializedForm = form.serialize();

        var url = '/LMS/LeaveEncasment/GetLeaveBalance';
        $.post(url, serializedForm, function (result) {
            $('#LeaveEncasment_fltBeforeBalance').val(result[0].toFixed(2));
            $('#LeaveEncasment_fltMaxDaysEncashable').val(result[1]);
            $('#LeaveEncasment_fltMinDaysinhand').val(result[2]);
            $('#LeaveEncasment_fltEncashed').val(result[3]);

        }, "json");

        return false;
    }


    function Delete() {


        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divLeaveEncasmentDetails';
            var url = '/LMS/LeaveEncasment/Delete';
            var form = $('#frmLeaveEncasment');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        }
        return false;
    }

    function handleEnter(evt) {

        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;

        }

        else if (evt) keyCode = evt.which;
        else return true;

        $('#LeaveEncasment_strEmpID').removeClass("invalid");

        if (keyCode == 13) {
            //handle enter
            var id = document.getElementById('LeaveEncasment_strEmpID').value;
            var name = "";

            // getdata(id, name);
            var url = "/LMS/Employee/LookUpById/" + id;

            $.post(url, "", function (result) {

                if (result[0] != null && result[0] != "") {
                    document.getElementById('LeaveEncasment_strEmpName').value = result[1];
                    document.getElementById('hdnEmpId').value = result[0];

                    $("#lblIdNotFound").css('visibility', 'hidden');
                }
                else {
                    document.getElementById('LeaveEncasment_strEmpName').value = "";
                    document.getElementById('hdnEmpId').value = "";
                    $("#lblIdNotFound").css('visibility', 'visible');
                }
            }, "json");           
        }
        return true;
    }

    function showApplicantType(val) {

        val = parseInt(val);
               

        if(val>0 || isNaN(val)) {
            $(".dvIndividual").hide();
            $(".dvAll").show();
            $("#LeaveEncasment_strEmpID").removeClass('required');
            $("#LeaveEncasment_strIsIndividual").val("All");
        }
        else {
            $(".dvIndividual").show();
            $(".dvAll").hide();
            $("#LeaveEncasment_strEmpID").addClass('required');
            $("#LeaveEncasment_strIsIndividual").val("Individual");


        }
    }

    function getFlow(obj) {

       
        showApplicantType(obj.value);
       
        var targetDiv = '#divLeaveEncasmentDetails';
        var path = "/LMS/LeaveEncasment/GetLeaveTypeList";

        var intSearchPathID = $("#LeaveEncasment_intLeaveTypeID").val();

       
        
        var form = $('#frmLeaveEncasment');
        var serializedForm = form.serialize();

        $.getJSON(path, serializedForm, function (data) {

            var items = "<option value='0'>...All...</option>";

            $.each(data, function (i, item) {
                if (intSearchPathID == item.Value) {
                    items += "<option value='" + item.Value + "' selected='true'>" + item.Text + "</option>";
                }

                items += "<option value='" + item.Value + "'>" + item.Text + "</option>";
            });

            $("#LeaveEncasment_intLeaveTypeID").html(items);

        });



    }

</script>
<form id="frmLeaveEncasment" method="post" action="">

<%= Html.HiddenFor(m=> m.LeaveEncasment.strIsIndividual) %>
<%= Html.HiddenFor(m=> m.LeaveEncasment.intLeaveEncaseMasterID) %>
<%= Html.HiddenFor(m=> m.LeaveEncasment.strEncashType) %>
<div id="divEncasment">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
            <%= Html.HiddenFor(m => m.StrYearStartDate)%>
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.LeaveEncasment.intLeaveEncaseID)%>
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
                <%= Html.HiddenFor(m => m.LeaveEncasment.intLeaveYearID)%>
                <%= Html.TextBoxFor(m => m.LeaveEncasment.strYearTitle, new { @class = "textRegular", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr>
            <td>
                Payment Year<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveEncasment.intPaymentYear, Model.EnachLeaveYear, "...Select One...", new { @class = "selectBoxRegular required" })%>
            </td>
        </tr>
        <tr>
            <td>
                Payment Month<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.LeaveEncasment.strPaymentMonth, Model.PaymentMonth, "...Select One...", new { @class = "selectBoxRegular required" })%>
            </td>
        </tr>

         <tr>
            <td>
                
            </td>
            <td>

                <% 
                    bool isIndividual = false;
                    string encashType = Model.LeaveEncasment.strEncashType;
                    if (encashType == "" || encashType == "Individual" || encashType == null)
                        isIndividual = true;
                    else
                        isIndividual = false;
                    
                 %>
                <%= Html.RadioButton("Option", 0, isIndividual, new { @onclick = "return getFlow(this);" })%> Individual
                <%= Html.RadioButton("Option", 1, !isIndividual, new { @onclick = "return getFlow(this);" })%> All
            </td>
        </tr>

        <tr>
            <td valign="top">
                Applicant ID<label class="labelRequired">*</label>
            </td>
            <td><%--@readonly = "readonly"--%>
            <%-- <input type="hidden"  id="hdnEmpId"/>
                <%= Html.TextBoxFor(m => m.LeaveEncasment.strEmpID, new { @class = "textRegularDate required", onkeypress = "return handleEnter(event);" })%>
                <%if (Model.LeaveEncasment.intLeaveEncaseID == 0)
                  { %>
                <a href="#" class="btnSearch" onclick="return searchEmployee()"></a>
                <%} %>
                 <label id="lblIdNotFound" style="visibility:hidden;vertical-align:5px; padding-left:10px; color:red;">Id not found !</label> --%>

                  <div class="dvIndividual">

                    <input type="hidden"  id="hdnEmpId"/>
                    <%= Html.TextBoxFor(m => m.LeaveEncasment.strEmpID, new { @class = "textRegularDate required", onkeypress = "return handleEnter(event);" })%>
                    <%if (Model.LeaveEncasment.intLeaveEncaseID == 0)
                      { %>
                    <a href="#" class="btnSearch" onclick="return searchEmployee()"></a>
                    <%} %>
                     <label id="lblIdNotFound" style="visibility:hidden;vertical-align:5px; padding-left:10px; color:red;">Id not found !</label> 
                </div>

                <div class="dvAll">
                    <table>
                            <tr>
                                <td class="contenttabletd">
                                    Branch
                                </td>
                                <td class="contenttabletd">
                                    <%= Html.DropDownListFor(m => m.LeaveEncasment.strBranchID, Model.Location, "...All...", new { @class = "selectBoxRegular" , @onchange = "return getFlow(this);"})%>
                                </td>
                            </tr>
                            <tr>
                                <td class="contenttabletd">
                                    Department
                                </td>
                                <td class="contenttabletd">
                                    <%= Html.DropDownListFor(m => m.LeaveEncasment.strDepartmentID, Model.Department, "...All...", new { @class = "selectBoxRegular", @onchange = "return getFlow(this);" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="contenttabletd">
                                    Designation
                                </td>
                                <td class="contenttabletd">
                                    <%= Html.DropDownListFor(m => m.LeaveEncasment.strDesignationID, Model.Designation, "...All...", new { @class = "selectBoxRegular", @onchange = "return getFlow(this);" })%>
                                </td>
                            </tr>
                        </table>

                </div>

            </td>
        </tr>
        <tr class="dvIndividual">
            <td>
                Name<label class="labelRequired">*</label>
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.LeaveEncasment.strEmpName, new { @class = "textRegular", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr>
            <td>
                Leave Type<label class="labelRequired">*</label>
            </td>
            <td>
                <%if (Model.LeaveEncasment.intLeaveEncaseID <= 0)
                  { %>
                <%= Html.DropDownListFor(m => m.LeaveEncasment.intLeaveTypeID, Model.LeaveType, "...Select One...", new { @class = "selectBoxRegular required", onchange = "return getLeaveBalance();" })%>
                <%}
                  else
                  { %>
                <%= Html.HiddenFor(m => m.LeaveEncasment.intLeaveTypeID)%>
                <%= Html.TextBoxFor(m => m.LeaveEncasment.strLeaveType, new { @class = "textRegular", @readonly = "readonly" })%>
                <%}%>
            </td>
        </tr>
        <tr  class="dvIndividual">
            <td>
                Max. Days Encashable
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveEncasment.fltMaxDaysEncashable, new { @class = "textRegularNumber double", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr  class="dvIndividual">
            <td>
                Min. Days in Hand
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveEncasment.fltMinDaysinhand, new { @class = "textRegularNumber double", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr class="dvIndividual">
            <td>
                Leave Balance
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveEncasment.fltBeforeBalance, new { @class = "textRegularNumber double", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr  class="dvIndividual">
            <td>
                Leave Encashed
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveEncasment.fltEncashed, new { @class = "textRegularNumber double", @readonly = "readonly" })%>
            </td>
        </tr>
        <tr>
            <td>
                Encash Day<label class="labelRequired">*</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.LeaveEncasment.fltEncaseDuration, new { @class = "textRegularNumber required double" })%>
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
    <%if (Model.LeaveEncasment.intLeaveEncaseID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveEncasment, LMS.Web.Permission.MenuOperation.Edit))
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
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveEncasment, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.LeaveEncasment.intLeaveEncaseID > 0)
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
