<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveEntitlementModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmLeaveEntitlement"));

        OptionWiseRefresh(0);

        setTitle("Leave Entitlement");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 450, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
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

        //window.location = "/LMS/LeaveType";

    }

    function setData(id, strEmpInitial, name) {

        document.getElementById('LeaveEntitlement_strEmpID').value = id;
        document.getElementById('LeaveEntitlement_strEmpInitial').value = strEmpInitial;
        document.getElementById('LeaveEntitlement_strEmpName').value = name;

        $("#lblIdNotFound").css('visibility', 'hidden');
        $('#LeaveEntitlement_strEmpInitial').removeClass("invalid");

        $("#divEmpList").dialog('close');
    }

    function save() {

        //        if (document.getElementById('hdnEmpId').value != document.getElementById('LeaveEntitlement_strEmpInitial').value) {
        //            $('#LeaveEntitlement_strEmpID').addClass("invalid");
        //        } else {
        //            $('#LeaveEntitlement_strEmpID').removeClass("invalid");
        //        }

        if (fnValidate() == true) {
            var result = confirm('Pressing OK will process leave entitlement. Do you want to continue?');
            if (result == true) {
                document.getElementById('imgLoader').style.visibility = 'visible';
                executeAction('frmLeaveEntitlement', '/LMS/LeaveEntitlement/SaveLeaveEntitlement', 'divDataList');

            }
        }
        return false;
    }



    function Delete() {

        if (fnValidate() == true) {
            var result = confirm('Pressing OK will rollback all records. Do you want to continue?');
            if (result == true) {
                executeAction('frmLeaveEntitlement', '/LMS/LeaveEntitlement/Delete', 'divDataList');
            }
        }
        return false;
    }


    function OptionWiseRefresh(flag) {

        var IsIndividual = $('#Model_LeaveEntitlement_IsIndividual').attr('checked');

        $('#LeaveEntitlement_IsIndividual').val(IsIndividual);

        if (flag > 0) {
            document.getElementById('LeaveEntitlement_strEmpID').value = "";
            document.getElementById('LeaveEntitlement_strEmpInitial').value = "";
            $("#lblIdNotFound").css('visibility', 'hidden');
        }
        else {
            //            document.getElementById('hdnEmpId').value = document.getElementById('LeaveEntitlement_strEmpID').value;
        }

        if (IsIndividual == true) {

            $('#LeaveEntitlement_strEmpInitial').addClass("required");
            $('#LeaveEntitlement_strEmpInitial').removeAttr('disabled');
            $('#LeaveEntitlement_strEmpName').removeAttr('disabled');

            $("#btnElipse").css('visibility', 'visible');
            $("#lblIDReqMark").css('visibility', 'visible');

        }
        else {

            $('#LeaveEntitlement_strEmpInitial').val("");
            $('#LeaveEntitlement_strEmpInitial').attr('disabled', 'disabled');
            $('#LeaveEntitlement_strEmpName').val("");
            $('#LeaveEntitlement_strEmpName').attr('disabled', 'disabled');

            $('#LeaveEntitlement_strEmpInitial').removeClass("required");
            $('#LeaveEntitlement_strEmpName').removeClass("required");

            $("#btnElipse").css('visibility', 'hidden');
            $("#lblIDReqMark").css('visibility', 'hidden');

        }

    }


    function handleEnter(evt) {

        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;

        }

        else if (evt) keyCode = evt.which;
        else return true;

        $('#LeaveEntitlement_strEmpID').removeClass("invalid");

        if (keyCode == 13) {
            //handle enter
            var id = document.getElementById('LeaveEntitlement_strEmpID').value;
            var name = "";

            // getdata(id, name);
            var url = "/LMS/Employee/LookUpById/" + id;

            $.post(url, "", function (result) {

                if (result[0] != null && result[0] != "") {

                    document.getElementById('LeaveEntitlement_strEmpID').value = result[0];
                    document.getElementById('LeaveEntitlement_strEmpName').value = result[1];
                    document.getElementById('LeaveEntitlement_strEmpInitial').value = result[2];
                    

                    $("#lblIdNotFound").css('visibility', 'hidden');
                }
                else {

                    document.getElementById('LeaveEntitlement_strEmpID').value = "";
                    document.getElementById('LeaveEntitlement_strEmpName').value = "";
                    document.getElementById('LeaveEntitlement_strEmpInitial').value = "";

                    $("#lblIdNotFound").css('visibility', 'visible');
                }
            }, "json");
            return true;
        }
        return true;
    }    
    
    
</script>

<h3 class="page-title">Leave Entitlement</h3>

<form id="frmLeaveEntitlement" method="post" action="">
<div id="divEntitlement">
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
                <%= Html.DropDownListFor(m => m.LeaveEntitlement.intLeaveYearID,Model.LeaveYear, new { @class = "selectBoxRegular" })%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.HiddenFor(m => m.LeaveEntitlement.IsIndividual)%>
            </td>
            <td>
                <%=Html.RadioButton("Model_LeaveEntitlement_IsIndividual", true, Model.LeaveEntitlement.IsIndividual, new { onClick = "return OptionWiseRefresh(1);" })%>Individual
                <%=Html.RadioButton("Model_LeaveEntitlement_IsIndividual", true, !Model.LeaveEntitlement.IsIndividual, new { onClick = "return OptionWiseRefresh(1);" })%>All
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Applicant ID<label id="lblIDReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                    </td>
                                    <td>
                                        <%--@readonly="readonly"--%>
                                        <%--<input type="hidden"  id="hdnEmpId"/>--%>

                                        <%=Html.HiddenFor(m => m.LeaveEntitlement.strEmpID)%>
                                        <%=Html.TextBoxFor(m => m.LeaveEntitlement.strEmpInitial, new { @class = "textRegularDate required", @readonly = "readonly"})%>

                                        <%--<%=Html.TextBoxFor(m => m.LeaveEntitlement.strEmpInitial, new { @class = "textRegularDate required", @readonly = "readonly", onkeypress = "return handleEnter(event);" })%>--%>
                                        <a href="#" id="btnElipse" class="btnSearch" onclick="return searchEmployee();">
                                        </a>
                                        <label id="lblIdNotFound" style="visibility: hidden; vertical-align: 5px; padding-left: 10px;
                                            color: red;">
                                            Id not found !</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Name
                                    </td>
                                    <td>
                                        <%=Html.TextBoxFor(m => m.LeaveEntitlement.strEmpName, new { @class = "textRegular", @style = "width:250px; min-width:250px;", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div class="divButton">
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveEntitlement, LMS.Web.Permission.MenuOperation.Add))
      {%>
    <a href="#" class="btnProcess" onclick="return save();"></a>
    <%} %>
    <input id="btnSave" name="btnSave" style="visibility: hidden;" type="submit" value="Save"
        visible="false" />
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.LeaveEntitlement, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <a href="#" class="btnRollback" onclick="return Delete();"></a>
    <%} %>
</div>
<div style="position: absolute">
    <img id="imgLoader" src="/LMS/Content/ajax-loader.gif" style="visibility: hidden;" />
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
