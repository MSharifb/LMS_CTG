<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.EmployeeWiseOOAApprovalPathModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmEmployeeWiseApprovalPathDetails"));

        setTitle("Employee's Approval Path");

        $("#btnSave").hide();
        $("#btnDelete").hide();

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 430, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        OptionWisePageRefresh();

        FormatTextBox();

    });


    function OptionWisePageRefresh() {

        var IsIndividual = $('#Model_EmployeeWiseOOAApprovalPath_IsIndividual').attr('checked');
        
        $('#EmployeeWiseOOAApprovalPath_IsIndividual').val(IsIndividual);

        if (IsIndividual == true) {

            $('#EmployeeWiseOOAApprovalPath_strDepartmentID').val("");
            $('#EmployeeWiseOOAApprovalPath_strDepartmentID').attr('disabled', 'disabled');
            $('#EmployeeWiseOOAApprovalPath_strDesignationID').val("");
            $('#EmployeeWiseOOAApprovalPath_strDesignationID').attr('disabled', 'disabled');
            $('#EmployeeWiseOOAApprovalPath_strLocationID').val("");
            $('#EmployeeWiseOOAApprovalPath_strLocationID').attr('disabled', 'disabled');

            $('#EmployeeWiseOOAApprovalPath_strEmpID').addClass("required");
            $('#EmployeeWiseOOAApprovalPath_strEmpName').addClass("required");

            $('#EmployeeWiseOOAApprovalPath_strEmpID').removeAttr('disabled');
            $('#EmployeeWiseOOAApprovalPath_strEmpName').removeAttr('disabled');

            $("#btnElipse").css('visibility', 'visible');
            $("#lblIDReqMark").css('visibility', 'visible');
            $("#lblNameReqMark").css('visibility', 'visible');

        }
        else {

            $('#EmployeeWiseOOAApprovalPath_strEmpID').val("");
            $('#EmployeeWiseOOAApprovalPath_strEmpID').attr('disabled', 'disabled');
            $('#EmployeeWiseOOAApprovalPath_strEmpName').val("");
            $('#EmployeeWiseOOAApprovalPath_strEmpName').attr('disabled', 'disabled');

            $('#EmployeeWiseOOAApprovalPath_strEmpID').removeClass("required");
            $('#EmployeeWiseOOAApprovalPath_strEmpName').removeClass("required");

            $('#EmployeeWiseOOAApprovalPath_strDepartmentID').removeAttr('disabled');
            $('#EmployeeWiseOOAApprovalPath_strDesignationID').removeAttr('disabled');
            $('#EmployeeWiseOOAApprovalPath_strLocationID').removeAttr('disabled');


            $("#btnElipse").css('visibility', 'hidden');
            $("#lblIDReqMark").css('visibility', 'hidden');
            $("#lblNameReqMark").css('visibility', 'hidden');


        }
       
        $("#EmployeeWiseOOAApprovalPath_intFlowType").val($("#intFlowType").val());

        //return false;
    }

    function setData(id, name) {
        document.getElementById('EmployeeWiseOOAApprovalPath_strEmpID').value = id;
        document.getElementById('EmployeeWiseOOAApprovalPath_strEmpName').value = name;

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
        var url = '/LMS/EmployeeWiseOOAApprovalPath/GetApprovalPathDetails';
        var form = $('#frmEmployeeWiseApprovalPathDetails');
        var serializedForm = form.serialize();
        
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


        return false;
    }


    function savedata() {
        if (fnValidate() == true) {
            $('#btnSave').trigger('click');

        }
        return false;
    }

    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {


            var targetDiv = '#divEmployeeWiseApprovalPathDetails';

            var url = '/LMS/EmployeeWiseOOAApprovalPath/Delete';
            var form = $('#frmEmployeeWiseApprovalPathDetails');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }


    function selectOption(id1, intV, intNodId) {
      
        $("input:radio").each(function (i) {

            if (this.id != "Model_EmployeeWiseOOAApprovalPath_IsIndividual") {
                this.checked = false;
            }
        });
        if (intV == '1') {
            id1.checked = true;
            //alert(intNodId);
            $('#EmployeeWiseOOAApprovalPath_intNodeID').val(intNodId);
        }
    }

    function getFlow() {


        var targetDiv = '#divEmployeeWiseApprovalPathDetails';
        var Id = $("#EmployeeWiseOOAApprovalPath_intFlowType").val();
        var path = "/LMS/EmployeeWiseOOAApprovalPath/GetFlowList";

        var form = $('#frmEmployeeWiseApprovalPathDetails');
        var serializedForm = form.serialize();

        $.post(path, serializedForm, function (result) { $(targetDiv).html(result); }, "html");


    }


   
    
</script>
<form id="frmEmployeeWiseApprovalPathDetails" method="post" action="">
<%   %>

<%--<script type="text/javascript">
    function closeModalDialog() {

       
        window.parent.$("#divConveyance").dialog('close');


    }
</script>--%>

<div id="divEmpWiseAppPath">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
            <%= Html.HiddenFor(m => m.EmployeeWiseOOAApprovalPath.intEmpPathID)%>
            <%= Html.Hidden("intFlowType",Model.EmployeeWiseOOAApprovalPath.intFlowType) %>
           
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.EmployeeWiseOOAApprovalPath.intNodeID)%>
        </div>
    </div>
    <div class="divRow">
        <table class="contenttext" style="width: 100%;">

            <tr>
                <td>
                     Flow Type
                    <label class="labelRequired">
                        *</label>
                </td>
                <td>                   
                    <%= Html.DropDownListFor(m => m.EmployeeWiseOOAApprovalPath.intFlowType, Model.GetBillType, "...Select One...", new { @class = "selectBoxRegular", @style = "width:250px;", onchange = "getFlow();" })%>
                </td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    Flow Name
                    <label class="labelRequired">
                        *</label>
                </td>
                <td> 
                   <%--<%= Html.DropDownListFor(m => m.EmployeeWiseOOAApprovalPath.intPathID, new SelectList(Model.OOAFLOWLIST, "Value", "Text", Model.EmployeeWiseOOAApprovalPath.intPathID), "Select One", new { @class = "ddlRegularDate", onchange = "selectOption(this,'0','0'); getApprovalPathDetails();" })%>  --%>
                    <%--<%= Html.DropDownList("ddl1", Model.OOAFLOWLIST(), "Select One", new { @class = "selectBoxRegular", @style = "width:250px;", onchange = "selectOption(this,'0','0'); getApprovalPathDetails();" })%>                  --%>
                    <%= Html.DropDownListFor(m => m.EmployeeWiseOOAApprovalPath.intPathID, Model.OOAFLOWLIST, "...Select One...", new { @class = "selectBoxRegular", @style = "width:250px;", onchange = "selectOption(this,'0','0'); getApprovalPathDetails();" })%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="contenttext" style="width: 100%;">
                        <tr>
                            <%=Html.HiddenFor(m => m.EmployeeWiseOOAApprovalPath.IsIndividual)%>
                            <td>
                                <%=Html.RadioButton("Model_EmployeeWiseOOAApprovalPath_IsIndividual", true, Model.EmployeeWiseOOAApprovalPath.IsIndividual, new { onClick = "OptionWisePageRefresh();" })%>Individual
                            </td>
                            <td>
                                <%=Html.RadioButton("Model_EmployeeWiseOOAApprovalPath_IsIndividual", true, !Model.EmployeeWiseOOAApprovalPath.IsIndividual, new { onClick = "OptionWisePageRefresh();" })%>All
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
                                            <%=Html.TextBoxFor(m => m.EmployeeWiseOOAApprovalPath.strEmpID, new { @class = "textRegularDate required", @readonly = "readonly" })%>
                                            <a href="#" id="btnElipse" style="visibility: hidden" class="btnSearch" onclick="return searchEmployee();">
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletd" style="width: 30%">
                                            Name<label id="lblNameReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                        </td>
                                        <td class="contenttabletd" style="width: 70%">
                                            <%=Html.TextBoxFor(m => m.EmployeeWiseOOAApprovalPath.strEmpName, new { @class = "textRegular", @readonly = "readonly",@style="width:250px;" })%>
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
                                    <tr>
                                        <td class="contenttabletd">
                                            Location
                                        </td>
                                        <td class="contenttabletd">
                                            <%= Html.DropDownListFor(m => m.EmployeeWiseOOAApprovalPath.strLocationID, Model.Location, "...All...", new { @class = "selectBoxRegular" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletd">
                                            Department
                                        </td>
                                        <td class="contenttabletd">
                                            <%= Html.DropDownListFor(m => m.EmployeeWiseOOAApprovalPath.strDepartmentID, Model.Department, "...All...", new { @class = "selectBoxRegular" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletd">
                                            Designation
                                        </td>
                                        <td class="contenttabletd">
                                            <%= Html.DropDownListFor(m=>m.EmployeeWiseOOAApprovalPath.strDesignationID,Model.Designation,"...All...", new { @class = "selectBoxRegular" })%>
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
                                <% for (int i = 0; i < Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath.Count; i++)
                                   {               
                                %>
                                <tr>
                                    <td colspan="2">
                                        <%= Html.Hidden("EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[" + i.ToString() + "].intNodeID", Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].intNodeID)%>
                                        <%= Html.Hidden("EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[" + i.ToString() + "].strNodeName", Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].strNodeName)%>
                                        <%= Html.RadioButton("EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[" + i.ToString() + "].intIsSelect", 1, Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].intIsSelect == 1 ? true : false, new { onclick = "return selectOption(this,'1','" + Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].intNodeID + "');" })%><%=Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].strNodeName%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <%= Html.Hidden("EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[" + i.ToString() + "].strAuthorID", Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].strAuthorID)%>
                                        <%= Html.Hidden("EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[" + i.ToString() + "].strAuthorName", Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].strAuthorName)%>
                                        <%= Html.Encode("Author: " + Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].strAuthorID + '-' + Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath[i].strAuthorName)%>
                                    </td>
                                </tr>
                                <% if (i < Model.EmployeeWiseOOAApprovalPath.LstEmployeeOOAApprovalPath.Count - 1)
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
    <%if (Model.EmployeeWiseOOAApprovalPath.intEmpPathID > 0)
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
    <%if (Model.EmployeeWiseOOAApprovalPath.intEmpPathID > 0)
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
