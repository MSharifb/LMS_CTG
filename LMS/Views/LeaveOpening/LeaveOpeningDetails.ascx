<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveOpeningModels>" %>
<script type="text/javascript">


    $(document).ready(function () {

        $("#divImageUploader").dialog({ autoOpen: false, modal: true, height: 40, width: 350, resizable: false, title: 'Upload File' });
        initFileUploadForm();

        preventSubmitOnEnter($("#frmLeaveOpening"));
        setTitle("Leave Opening");
        $("#btnSave").hide();
        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 780, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        FormatTextBox();

    });

    function openFileUploader() {

        var IsFound = false;

        var form = $('#frmLeaveOpening');
        var serializedForm = form.serialize();
        var url = '/LMS/LeaveOpening/GetLeaveOpeningAll';
        $.post(url, serializedForm, function (result) {
            IsFound = result;
            if (IsFound == true) {
                var result = confirm('Leave opening balance exists. Do you want to replace?');
                if (result == false) {
                    return false;
                }
            }

            $("#txtOpeningDate").val(document.getElementById('LeaveOpening_strBalanceDate').value);
            $("#divImageUploader").dialog('open');

        }, "json");

        return false;
    }

    function uploadFile() {
        var fieldvalue = $('#file').val();

        if (fieldvalue != "") {
            return true;
        } else {
            var msg = '<label style="font-size: 10pt; font-weight: bold; color: Red;" id="lblMsg">Please select an excel file.</label>';
            $("#divMsgStd").html(msg);
            return false;
        }
    }
    function initFileUploadForm() {
        var IsFound = false;
        var IsReplace = false;

        $("#ajaxUploadForm").ajaxForm({
            iframe: true,
            dataType: "json",
            beforeSubmit: function () {
                $("#ajaxUploadForm").block({ message: '<h1><img src="/LMS/Content/ajax-loader.gif" /> Uploading file...</h1>' });
            },
            success: function (result) {
                $("#ajaxUploadForm").unblock();
                $("#ajaxUploadForm").resetForm();
                //$.growlUI(null, result.message);
                var msg = "";

                if (result.flag.toString().toLowerCase() == "true") {
                    msg = '<label style="font-size: 10pt; font-weight: bold; color: Green;" id="lblMsg">' + result.message + '</label>';
                }
                else {
                    msg = '<label style="font-size: 10pt; font-weight: bold; color: Red;" id="lblMsg">' + result.message + '</label>';
                }
                $("#divImageUploader").dialog('close');
                $("#divMsgStd").html(msg);


            },
            error: function (xhr, textStatus, errorThrown) {
                $("#ajaxUploadForm").unblock();
                $("#ajaxUploadForm").resetForm();

                var msg = '<label style="font-size: 10pt; font-weight: bold; color: Red;" id="lblMsg">Error uploading file!</label>';
                $("#divMsgStd").html(msg);
            }
        });


    }

    function setData(id, strEmpInitial, name) {

        document.getElementById('LeaveOpening_strEmpID').value = id;
        document.getElementById('LeaveOpening_strEmpInitial').value = strEmpInitial;
        document.getElementById('LeaveOpening_strEmpName').value = name;
        $("#divEmpList").dialog('close');

        getdata(id, name);
    }


    function Closing() {

    }


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


    function getdata(id, name) {

        executeCustomAction({ strEmpID: id, strEmpName: name }, '/LMS/LeaveOpening/Details', 'divDataList');

        return false;
    }

    function updatedata() {

        //        if (document.getElementById('LeaveOpening_strEmpInitial').value != document.getElementById('LeaveOpening_strEmpID').value) {
        //            $('#LeaveOpening_strEmpID').addClass("invalid");
        //        }

        if (fnValidate() == true) {
            executeAction('frmLeaveOpening', '/LMS/LeaveOpening/SaveLeaveOpening', 'divDataList');
        }
    }

    function savedata() {
        if (fnValidateDateTime() == false) {
            alert("Invalid Date.");
            return false;
        }

        /*check the invalid id */
        var isValidId = $("#IsValidId").val().toLowerCase();

        if (isValidId == "false") {
            $('#LeaveOpening_strEmpInitial').addClass("invalid");

        }
        //        if (document.getElementById('hdnEmpId').value != document.getElementById('LeaveOpening_strEmpID').value) {
        //            $('#LeaveOpening_strEmpInitial').addClass("invalid");
        //        }
        if (fnValidate() == true) {
            //$('#btnSave').trigger('click');      

            executeAction('frmLeaveOpening', '/LMS/LeaveOpening/SaveLeaveOpening', 'divDataList');
        }

        return false;
    }


    function Delete() {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            executeAction('frmLeaveOpening', '/LMS/LeaveOpening/DeleteData', 'divDataList');
        }
        return false;
    }


    //    $("#datepicker").datepicker({
    //        changeMonth: true,
    //        changeYear: false
    //    }).click(function () { $('#LeaveOpening_strBalanceDate').datepicker('show'); });

    function SelectAndDeselect() {

        var IsSelect = $('#Model_bitIsSelect').attr('checked');
        $('#Model_bitIsSelect').val(IsSelect);

        if (IsSelect == false) {
            $('#btnImgSave').css('visibility', 'visible');
            $('#btnImport').css('visibility', 'hidden');
            $("#btnElipse").css('visibility', 'visible');
            $('#LeaveOpening_strEmpInitial').addClass("required");
            $("#lblIDReqMark").css('visibility', 'visible');
        }
        else {
            $('#btnImgSave').css('visibility', 'hidden');
            $('#btnImport').css('visibility', 'visible');
            $("#btnElipse").css('visibility', 'hidden');
            $('#LeaveOpening_strEmpInitial').removeClass("required");
            $("#lblIDReqMark").css('visibility', 'hidden');
        }

    }
    // LeaveOpening_strEmpName
    function handleEnter(evt) {

        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;

        }

        else if (evt) keyCode = evt.which;
        else return true;

        $('#LeaveOpening_strEmpInitial').removeClass("invalid");

        if (keyCode == 13) {
            //handle enter
            var id = document.getElementById('LeaveOpening_strEmpID').value;
            var initial = document.getElementById('LeaveOpening_strEmpInitial').value;
            var name = "";
            getdata(id, name);
        }
        return true;
    } 

</script>

<h3 class="page-title">Leave Opening</h3>

<form id="frmLeaveOpening" method="post" action="" enctype="multipart/form-data">
<div id="divLeaveOpening">
    <div style="width: 100%">
        <table class="contenttext" style="width: 100%;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 17%;">
                                Applicant ID<label id="lblIDReqMark" class="labelRequired">*</label>
                                <%=Html.HiddenFor(m=> m.IsExists) %>
                                <%=Html.HiddenFor(m => m.IsValidId)%>
                                <%=Html.HiddenFor(m=>m.LeaveOpening.strEmpID)%>
                            </td>
                            <td>
                                <%--<%=Html.TextBoxFor(m => m.LeaveOpening.strEmpInitial, new { @class = "textRegularDate required", onkeypress = "return handleEnter(event);"})%>--%>
                                <%=Html.TextBoxFor(m => m.LeaveOpening.strEmpInitial, new { @class = "textRegularDate required",@readonly=true})%>
                                <a href="#" id="btnElipse" class="btnSearch" onclick="return searchEmployee();">
                                </a>
                                <%if (Model.IsValidId == false)
                                  { %>
                                <label id="Label2" style="vertical-align: 5px; padding-left: 10px; color: red;">
                                    Id not found !</label>
                                <%} %>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%;">
                                Applicant Name<label class="labelRegular"></label>
                            </td>
                            <td>
                                <%=Html.TextBoxFor(m => m.LeaveOpening.strEmpName, new { @class = "textRegular", @readonly = "readonly", @style = "width:515px; min-width:515px;" })%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%;">
                                Designation<label class="labelRegular"></label>
                            </td>
                            <td>
                                <%=Html.TextBoxFor(m => m.LeaveOpening.Employee.strDesignation, new { @class = "textRegular", @readonly = "readonly", @style = "width:515px; min-width:515px;" })%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%;">
                                Department<label class="labelRegular"></label>
                            </td>
                            <td>
                                <%=Html.TextBoxFor(m => m.LeaveOpening.Employee.strDepartment, new { @class = "textRegular", @readonly = "readonly", @style = "width:515px; min-width:515px;" })%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%;">
                                Joining Date<label class="labelRegular"></label>
                            </td>
                            <td>
                                <div style="float: left; text-align: left;">
                                    <%=Html.TextBoxFor(m => m.strJoiningDate, new { @class = "textRegularDate", @readonly = "readonly" })%>
                                </div>
                                <div style="float: left; text-align: left; padding-left: 15px;">
                                    Confirmation Date<label class="labelRegular"></label>
                                    <%=Html.TextBoxFor(m => m.strConfirmationDate, new { @class = "textRegularDate", @readonly = "readonly" })%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%;">
                               <%-- <%=Html.HiddenFor(m=> m.LeaveOpening.intLeaveYearID) %>--%>
                            </td>
                            <td>
                                <%if (Model.IsExists == false)
                                  { %>
                                <div style="float: left; text-align: left; padding-left: 15px;">
                                    <%=Html.CheckBox("Model_bitIsSelect", Model.bitIsSelect, new { onclick = "SelectAndDeselect();", @style = "vertical-align:3px;" })%>
                                    <label id="Label1" style="vertical-align: 5px;">
                                        Import Opening Balance</label>
                                    <a id="btnImport" name="btnImport" style="visibility: hidden;" href="#" class="btnElipse"
                                        onclick="return openFileUploader();" />
                                </div>
                                <%} %>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="2">
                                Opening Date<label class="labelRequired">*</label>
                                <%=Html.TextBoxFor(m => m.LeaveOpening.strBalanceDate, new { @class = "textRegularDate required dtPicker date"})%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="div1">
                                    <div id="Div2">
                                        <div id="Div3" style="overflow: auto; width: 99%">
                                            <table class="contenttext" style="width: 97%;">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 30%;">
                                                            Leave Type
                                                        </th>
                                                        <th style="width: 20%;">
                                                            Leave Year
                                                        </th>
                                                        <th>
                                                            Carry Over
                                                        </th>
                                                        <th>
                                                            Availed(WP)
                                                        </th>
                                                        <th>
                                                            Availed(WOP)
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                            <div style="overflow-y: auto; overflow-x: hidden; max-height: 170px">
                                                <table class="contenttext" style="width: 97%;">
                                                    <% for (int j = 0; j < Model.LeaveOpening.LstLeaveOpening.Count; j++)
                                                       { 
                                                    %>
                                                    <tr>
                                                        <td style="width: 30%;">
                                                            <%=Html.Hidden("LeaveOpening.LstLeaveOpening[" + j + "].intLeaveYearID", Model.LeaveOpening.LstLeaveOpening[j].intLeaveYearID.ToString())%>
                                                            <%=Html.Hidden("LeaveOpening.LstLeaveOpening[" + j + "].intLeaveTypeID", Model.LeaveOpening.LstLeaveOpening[j].intLeaveTypeID.ToString())%>
                                                            <%=Html.Hidden("LeaveOpening.LstLeaveOpening[" + j + "].strLeaveType", Model.LeaveOpening.LstLeaveOpening[j].strLeaveType)%>
                                                            <%=Html.Encode(Model.LeaveOpening.LstLeaveOpening[j].strLeaveType)%>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <%=Html.Hidden("LeaveOpening.LstLeaveOpening[" + j + "].strYearTitle", Model.LeaveOpening.LstLeaveOpening[j].strYearTitle)%>
                                                            <%=Html.Hidden("LeaveOpening.LstLeaveOpening[" + j + "].isServiceLifeType", Model.LeaveOpening.LstLeaveOpening[j].isServiceLifeType)%>
                                                            <%=Html.Encode(Model.LeaveOpening.LstLeaveOpening[j].isServiceLifeType == true ? "Service Life" : Model.LeaveOpening.LstLeaveOpening[j].strYearTitle)%>
                                                        </td>
                                                        <td>
                                                            <%= Html.TextBox("LeaveOpening.LstLeaveOpening[" + j + "].fltOB", Model.LeaveOpening.LstLeaveOpening[j].fltOB, new { @class = "textRegularNumber double", @style = "width:70px; min-width:70px;", maxlength = 10 })%>
                                                        </td>
                                                        <td>
                                                            <%= Html.TextBox("LeaveOpening.LstLeaveOpening[" + j + "].fltAvailed", Model.LeaveOpening.LstLeaveOpening[j].fltAvailed, new { @class = "textRegularNumber double",@style = "width:95px; min-width:95px;", maxlength = 10 })%>
                                                        </td>
                                                        <td>
                                                            <%= Html.TextBox("LeaveOpening.LstLeaveOpening[" + j + "].fltAvailedWOP", Model.LeaveOpening.LstLeaveOpening[j].fltAvailedWOP, new { @class = "textRegularNumber double", @style = "width:95px; min-width:95px;", maxlength = 10 })%>
                                                        </td>
                                                    </tr>
                                                    <%} %>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: right;">
                        <label style="font-size: smaller; color: Navy;">
                            NB: WP = With Pay, WOP = Without Pay</label>
                    </div>
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
    <%if (Model.IsExists == true)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveOpening, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return updatedata();"></a>
    <%} %>
    <%}
      else
      {%>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveOpening, LMS.Web.Permission.MenuOperation.Add))
      {%>
    <a id="btnImgSave" href="#" class="btnSave" onclick="return savedata();"></a>
    <%} %>
    <%} %>
    <input id="btnSave" name="btnSave" style="visibility: hidden;" type="submit" value="Save"
        visible="false" />
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveOpening, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.IsExists == true)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <input id="btnDelete" name="btnDelete" type="submit" value="Delete" visible="false" />
    <%} %>
    <%} %>
</div>
<div style="position: absolute">
    <img id="imgLoader" src="/LMS/Content/ajax-loader.gif" style="visibility: hidden;"
        alt="" />
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
<div class="divSpacer">
</div>
<div id="divImageUploader" class="divOuter">
    <div class="divRow">
        <div class="divCol2FreeSize">
            <form id="ajaxUploadForm" action="<%= Url.Action("UploadLeaveOpening", "LeaveOpening")%>"
            method="post" enctype="multipart/form-data">
            <div>
                <input type="file" name="file" id="file" accept="xls" />
                <input id="ajaxUploadButton" name="ajaxUploadButton" type="submit" value="Upload"
                    onclick="return uploadFile(this);" />
                <input type="hidden" name="txtOpeningDate" id="txtOpeningDate" />
            </div>
            </form>
        </div>
    </div>
</div>
