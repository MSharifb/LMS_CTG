<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.EmployeeWiseApprovalPathModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmEmployeeWiseApprovalPathDetails"));

        setTitle("Employee's Approval Path");

        $("#btnSave").hide();
        $("#btnDelete").hide();

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 430, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        OptionWisePageRefresh(0);

        FormatTextBox();

    });


    function OptionWisePageRefresh(flag) {

        var IsIndividual = $('#Model_EmployeeWiseApprovalPath_IsIndividual').attr('checked');

        $('#EmployeeWiseApprovalPath_IsIndividual').val(IsIndividual);

        if (flag > 0) {
            //document.getElementById('hdnEmpId').value = "";
            document.getElementById('EmployeeWiseApprovalPath_strEmpID').value = "";
            document.getElementById('EmployeeWiseApprovalPath_strEmpInitial').value = "";
            document.getElementById('EmployeeWiseApprovalPath_strEmpName').value = "";
            $("#lblIdNotFound").css('visibility', 'hidden');
        }
        else {
            //document.getElementById('hdnEmpId').value = document.getElementById('EmployeeWiseApprovalPath_strEmpID').value;
        }

        if (IsIndividual == true) {

            $('#EmployeeWiseApprovalPath_strDepartmentID').val("");
            $('#EmployeeWiseApprovalPath_strDepartmentID').attr('disabled', 'disabled');
            $('#EmployeeWiseApprovalPath_strDesignationID').val("");
            $('#EmployeeWiseApprovalPath_strDesignationID').attr('disabled', 'disabled');
//            $('#EmployeeWiseApprovalPath_strLocationID').val("");
//            $('#EmployeeWiseApprovalPath_strLocationID').attr('disabled', 'disabled');

            $('#EmployeeWiseApprovalPath_strEmpInitial').addClass("required");
            $('#EmployeeWiseApprovalPath_strEmpName').addClass("required");

            $('#EmployeeWiseApprovalPath_strEmpInitial').removeAttr('disabled');
            $('#EmployeeWiseApprovalPath_strEmpName').removeAttr('disabled');

            $("#btnElipse").css('visibility', 'visible');
            $("#lblIDReqMark").css('visibility', 'visible');
            $("#lblNameReqMark").css('visibility', 'visible');

        }
        else {

            $('#EmployeeWiseApprovalPath_strEmpInitial').val("");
            $('#EmployeeWiseApprovalPath_strEmpInitial').attr('disabled', 'disabled');
            $('#EmployeeWiseApprovalPath_strEmpName').val("");
            $('#EmployeeWiseApprovalPath_strEmpName').attr('disabled', 'disabled');

            $('#EmployeeWiseApprovalPath_strEmpInitial').removeClass("required");
            $('#EmployeeWiseApprovalPath_strEmpName').removeClass("required");

            $('#EmployeeWiseApprovalPath_strDepartmentID').removeAttr('disabled');
            $('#EmployeeWiseApprovalPath_strDesignationID').removeAttr('disabled');
//            $('#EmployeeWiseApprovalPath_strLocationID').removeAttr('disabled');


            $("#btnElipse").css('visibility', 'hidden');
            $("#lblIDReqMark").css('visibility', 'hidden');
            $("#lblNameReqMark").css('visibility', 'hidden');


        }
        //return false;
    }

    function setData(id,strEmpInitial, name) {
        document.getElementById('EmployeeWiseApprovalPath_strEmpID').value = id;
        document.getElementById('EmployeeWiseApprovalPath_strEmpInitial').value = strEmpInitial;
        document.getElementById('EmployeeWiseApprovalPath_strEmpName').value = name;

        //document.getElementById('hdnEmpId').value = id;
        $("#lblIdNotFound").css('visibility', 'hidden');
        $('#EmployeeWiseApprovalPath_strEmpInitial').removeClass("invalid");

        $("#divEmpList").dialog('close');
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


    function getApprovalPathDetails() {


        var targetDiv = '#divEmployeeWiseApprovalPathDetails';
        var url = '/LMS/EmployeeWiseApprovalPath/GetApprovalPathDetails';
        var form = $('#frmEmployeeWiseApprovalPathDetails');
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        return false;
    }


    function savedata() {

//        if (document.getElementById('hdnEmpId').value != document.getElementById('EmployeeWiseApprovalPath_strEmpID').value) {
//            $('#EmployeeWiseApprovalPath_strEmpID').addClass("invalid");
//        }
//        else {
//            $('#EmployeeWiseApprovalPath_strEmpID').removeClass("invalid");
//        }

        if (fnValidate() == true) {
            $('#btnSave').trigger('click');

        }
        return false;
    }

    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {


            var targetDiv = '#divEmployeeWiseApprovalPathDetails';

            var url = '/LMS/EmployeeWiseApprovalPath/Delete';
            var form = $('#frmEmployeeWiseApprovalPathDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }


    function selectOption(id1, intV, intNodId) {

        $("input:radio").each(function (i) {

            if (this.id != "Model_EmployeeWiseApprovalPath_IsIndividual") {
                this.checked = false;
            }
        });
        if (intV == '1') {
            id1.checked = true;
            //alert(intNodId);
            $('#EmployeeWiseApprovalPath_intNodeID').val(intNodId);
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

        $('#EmployeeWiseApprovalPath_strEmpID').removeClass("invalid");

        if (keyCode == 13) {
            //handle enter
            var id = document.getElementById('EmployeeWiseApprovalPath_strEmpID').value;
            var name = "";

            // getdata(id, name);
            var url = "/LMS/Employee/LookUpById/" + id;

            $.post(url, "", function (result) {

                if (result[0] != null && result[0] != "") {
                    document.getElementById('EmployeeWiseApprovalPath_strEmpName').value = result[1];
                    //document.getElementById('hdnEmpId').value = result[0];
                    $("#lblIdNotFound").css('visibility', 'hidden');
                }
                else {
                    document.getElementById('EmployeeWiseApprovalPath_strEmpName').value = "";
                    //document.getElementById('hdnEmpId').value = "";
                    $("#lblIdNotFound").css('visibility', 'visible');
                }
            }, "json");
            return true;
        }
        return true;
    }    
</script>
<form id="frmEmployeeWiseApprovalPathDetails" method="post" action="">
<div id="divEmpWiseAppPath">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
            <%= Html.HiddenFor(m => m.EmployeeWiseApprovalPath.intEmpPathID)%>
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.EmployeeWiseApprovalPath.intNodeID)%>
        </div>
    </div>
    <div class="divRow">
        <table class="contenttext" style="width: 100%;">
            <tr>
                <td style="width: 15%;">
                    Flow Name
                    <label class="labelRequired">
                        *</label>
                </td>
                <td>
                    <%= Html.DropDownListFor(m => m.EmployeeWiseApprovalPath.intPathID, Model.ApprovalPath, "...Select One...", new { @class = "selectBoxRegular", @style = "width:250px;", onchange = "selectOption(this,'0','0'); getApprovalPathDetails();" })%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="contenttext" style="width: 100%;">
                        <tr>
                            <%=Html.HiddenFor(m => m.EmployeeWiseApprovalPath.IsIndividual)%>
                            <td>
                                <%=Html.RadioButton("Model_EmployeeWiseApprovalPath_IsIndividual", true, Model.EmployeeWiseApprovalPath.IsIndividual, new { onClick = "OptionWisePageRefresh(1);" })%>Individual
                            </td>
                            <td>
                                <%=Html.RadioButton("Model_EmployeeWiseApprovalPath_IsIndividual", true, !Model.EmployeeWiseApprovalPath.IsIndividual, new { onClick = "OptionWisePageRefresh(1);" })%>All
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td class="contenttabletd" style="width: 30%">
                                            ID<label id="lblIDReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                        </td>
                                        <td class="contenttabletd" style="width: 70%">
                                            <%--<input type="hidden" id="hdnEmpId" />
                                            <%=Html.TextBoxFor(m => m.EmployeeWiseApprovalPath.strEmpID, new { @class = "textRegularDate required", onkeypress = "return handleEnter(event);" })%>
                                            --%>

                                            <%=Html.HiddenFor(m => m.EmployeeWiseApprovalPath.strEmpID)%>
                                            <%=Html.TextBoxFor(m => m.EmployeeWiseApprovalPath.strEmpInitial, new { @class = "textRegularDate required", @readonly = "readonly" })%>
                                            
                                            <a href="#" id="btnElipse" style="visibility: hidden" class="btnSearch" onclick="return searchEmployee();">
                                            </a>
                                            <label id="lblIdNotFound" style="visibility: hidden; vertical-align: 5px; padding-left: 10px;
                                                color: red;">
                                                Id not found !</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletd" style="width: 30%">
                                           Name<label id="lblNameReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                        </td>
                                        <td class="contenttabletd" style="width: 70%">
                                            <%=Html.TextBoxFor(m => m.EmployeeWiseApprovalPath.strEmpName, new { @class = "textRegular", @readonly = "readonly",@style="width:250px;" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Select Initial Step:
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <%-- <tr>
                                        <td class="contenttabletd">
                                            Branch
                                        </td>
                                        <td class="contenttabletd">
                                            <%= Html.DropDownListFor(m => m.EmployeeWiseApprovalPath.strLocationID, Model.Location, "...All...", new { @class = "selectBoxRegular" })%>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="contenttabletd">
                                            Department
                                        </td>
                                        <td class="contenttabletd">
                                            <%= Html.DropDownListFor(m => m.EmployeeWiseApprovalPath.strDepartmentID, Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletd">
                                            Designation
                                        </td>
                                        <td class="contenttabletd">
                                            <%= Html.DropDownListFor(m=>m.EmployeeWiseApprovalPath.strDesignationID,Model.Designation,"...All...", new { @class = "selectBoxRegular" })%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <table class="contenttext" style="width: 100%;">
                <colgroup>
                    <col style="" width="15%" />
                    <col />
                    <col style="" width="15%" />
                </colgroup>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <div id="author-data" style="overflow: auto; height: 230px; width: 100%;">
                            <table class="contenttext" style="width: 100%;">
                                <% for (int i = 0; i < Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath.Count; i++)
                                   {               
                                %>
                                <tr>
                                    <td colspan="2">
                                        <%= Html.Hidden("EmployeeWiseApprovalPath.LstEmployeeApprovalPath[" + i.ToString() + "].intNodeID", Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].intNodeID)%>
                                        <%= Html.Hidden("EmployeeWiseApprovalPath.LstEmployeeApprovalPath[" + i.ToString() + "].strNodeName", Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].strNodeName)%>
                                        <%= Html.RadioButton("EmployeeWiseApprovalPath.LstEmployeeApprovalPath[" + i.ToString() + "].intIsSelect", 1, Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].intIsSelect == 1 ? true : false, new { onclick = "return selectOption(this,'1','" + Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].intNodeID + "');" })%><%=Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].strNodeName%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <%= Html.Hidden("EmployeeWiseApprovalPath.LstEmployeeApprovalPath[" + i.ToString() +"].strAuthorID",Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].strAuthorID)%>
                                        <%= Html.Hidden("EmployeeWiseApprovalPath.LstEmployeeApprovalPath[" + i.ToString() +"].strAuthorName",Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].strAuthorName)%>
                                        <%= Html.Encode("Author: " + Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].strAuthorInitial + " - " + Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath[i].strAuthorName)%>
                                    </td>
                                </tr>
                                <% if (i < Model.EmployeeWiseApprovalPath.LstEmployeeApprovalPath.Count - 1)
                                   { %>
                                <tr>
                                    <td colspan="2" align="center">
                                        <img alt="" src="<%= Url.Content("~/Content/img/controls/Down-Arrow.gif")%>" />
                                    </td>
                                </tr>
                                <%} %>
                                <%} %>
                            </table>
                        </div>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.EmployeeWiseApprovalPath.intEmpPathID > 0)
      { %>
    <a href="#" class="btnUpdate" onclick="return savedata();"></a>
    <%}
      else
      {%>
    <a href="#" class="btnSave" onclick="return savedata();"></a>
    <%} %>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.EmployeeWiseApprovalPath, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.EmployeeWiseApprovalPath.intEmpPathID > 0)
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
