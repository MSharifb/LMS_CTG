<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OOAApprovalPathModels>" %>
<script type="text/javascript">


    $(document).ready(function () {

        preventSubmitOnEnter($("#frmApproverAuth"));

        setTitle("Leave Approval Path");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 425, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        FormatTextBox();

    });

    function setData(id, name) {
        document.getElementById('OOAApprovalAuthor_strAuthorID').value = id;
        document.getElementById('OOAApprovalAuthor_strEmpName').value = name;

        $("#divEmpList").dialog('close');

    }

    function Closing() {
        //window.location = "/LMS/LeaveType";
    }


    function deleteAuthor(Id) {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = "#divSetApproverDetails";
            var url = "/LMS/OOAApprovalPath/deleteAuthor/" + Id;
            var form = $("#frmApproverAuth");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }


    function GetAuthors() {
        var targetDiv = "#divSetApproverDetails";
        var url = "/LMS/OOAApprovalPath/GetAuthors";
        var form = $("#frmApproverAuth");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            $(targetDiv).html(result);

            document.getElementById('OOAApprovalAuthor_strAuthorID').value = '';
            document.getElementById('OOAApprovalAuthor_strEmpName').value = '';
            document.getElementById('OOAApprovalAuthor_strAuthorType').value = '';


        }, "html");

        return false;
    }

    function AddAuthor() {


        if (fnValidate() == true) {
            var targetDiv = "#divSetApproverDetails";
            var url = "/LMS/OOAApprovalPath/AddAuthor";
            var form = $("#frmApproverAuth");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);
                var isblank = document.getElementById("BlnTextBlank").value;
                if (isblank == 'True') {

                    document.getElementById('OOAApprovalAuthor_strAuthorID').value = '';
                    document.getElementById('OOAApprovalAuthor_strEmpName').value = '';
                    document.getElementById('OOAApprovalAuthor_strAuthorType').value = '';
                }

            }, "html");
        }

        return false;
    }


    function save() {
        var targetDiv = "#divSetApproverDetails";
        var url = "/LMS/OOAApprovalPath/SetApprover";
        var form = $("#frmApproverAuth");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }

    function Delete() {
        Id = $('#ApprovalPath_intPathID').val();


        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = "#divSetApproverDetails";
            var url = "/LMS/OOAApprovalPath/Delete";
            var form = $("#frmApproverAuth");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }

        return false;
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


</script>
<form id="frmApproverAuth" method="post" action="">
<div id="divApprovalPath">
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <input type="hidden" value="<%= Model.BlnTextBlank %>" name="BlnTextBlank" id="BlnTextBlank"
                class="textRegular" />
            <%= Html.HiddenFor(m=>m.OOAApprovalPathMaster.intPathID)%>
            <%= Html.HiddenFor(m=>m.OOAApprovalPathMaster.intFlowType) %>
        </div>
    </div>
    <div class="divSpacer">
    </div>
    <fieldset title="Node">
        <table class="contenttext" style="width: 100%;">
            <colgroup>
                <col style="width: 80%" />
                <col />
            </colgroup>
            <tr>
                <td>
                    <table class="contenttext" style="width: 100%;">
                        <colgroup>
                            <col style="width: 40%; text-align: left" />
                            <col />
                        </colgroup>
                        <tr>
                            <td>
                                Flow Type
                            </td>
                            <td>
                                <%= Html.TextBoxFor(m => m.OOAApprovalPathMaster.FlowTypeName, new { @class = "textRegular", @readonly = "readonly" })%>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Flow Name
                            </td>
                            <td>
                                <%=Html.TextBoxFor(m => m.OOAApprovalPathMaster.strPathName, new { @class = "textRegular",@readonly="readonly" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Step Name<label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%=Html.DropDownListFor(m => m.OOAApprovalAuthor.intNodeID, Model.Node, "...Select One...",new { @class = "selectBoxRegular required" ,onchange="return GetAuthors();"})%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Author ID<label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%=Html.TextBox("OOAApprovalAuthor.strAuthorID", Model.OOAApprovalAuthor.strAuthorID, new { @class = "textRegularDate required", @readonly = "readonly" })%>
                                <a href="#" class="btnSearch" onclick="return searchEmployee();"></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Author Name<label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%=Html.TextBoxFor(m => m.OOAApprovalAuthor.strEmpName, new { @class = "textRegular", @readonly = "readonly" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Author Type<label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%= Html.DropDownListFor(m=>m.OOAApprovalAuthor.strAuthorType,Model.AuthType,"...Select One...", new { @class = "selectBoxRegular required" })%>
                            </td>
                            <td>
                                <div>
                                    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalPath, LMS.Web.Permission.MenuOperation.Add))
                                      {%>
                                    <a href="#" class="btnAdd" onclick="return AddAuthor();"></a>
                                    <%} %>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="grid">
                        <div id="grid-data">
                            <table class="contenttext" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th style="width: 69%; text-align: left; padding-left: 5px;">
                                            Author ID and Name
                                        </th>
                                        <th style="width: 18%; text-align: left; padding-left: 0px;">
                                            Author Type
                                        </th>
                                        <th style="text-align: left; padding-left: 12px;">
                                            Remove
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                            <div style="overflow-y: auto; overflow-x: hidden; max-height: 74px">
                                <table class="contenttext" style="width: 100%;">
                                    <% if (Model.LstOOAApprovalAuthor != null) for (int j = 0; j < Model.LstOOAApprovalAuthor.Count; j++)
                                           { 
                                    %>
                                    <tr>
                                        <td style="width: 70%;">
                                            <%=Html.Hidden("LstOOAApprovalAuthor[" + j + "].intAuthorityID", Model.LstOOAApprovalAuthor[j].intAuthorityID.ToString())%>
                                            <%=Html.Hidden("LstOOAApprovalAuthor[" + j + "].intPathID", Model.LstOOAApprovalAuthor[j].intPathID.ToString())%>
                                            <%=Html.Hidden("LstOOAApprovalAuthor[" + j + "].intNodeID", Model.LstOOAApprovalAuthor[j].intNodeID.ToString())%>
                                            <%=Html.Hidden("LstOOAApprovalAuthor[" + j + "].strAuthorID", Model.LstOOAApprovalAuthor[j].strAuthorID.ToString())%>
                                            <%=Html.Hidden("LstOOAApprovalAuthor[" + j + "].strEmpName", Model.LstOOAApprovalAuthor[j].strEmpName.ToString())%>
                                            <%=Html.Encode(Model.LstOOAApprovalAuthor[j].strAuthorID.ToString() + '-' + Model.LstOOAApprovalAuthor[j].strEmpName)%>
                                        </td>
                                        <td style="width: 20%;">
                                            <%=Html.Hidden("LstOOAApprovalAuthor[" + j + "].strAuthorType", Model.LstOOAApprovalAuthor[j].strAuthorType.ToString())%>
                                            <%=Html.Encode(Model.LstOOAApprovalAuthor[j].strAuthorType)%>
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <a href='#' visible="false" class="gridDelete" onclick='javascript:return deleteAuthor("<%= Model.LstOOAApprovalAuthor[j].strAuthorID %>");'>
                                            </a>
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
    </fieldset>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <a href="#" class="btnSave" onclick="return save();"></a>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />

    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
