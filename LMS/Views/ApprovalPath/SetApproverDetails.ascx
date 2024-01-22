<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ApprovalPathModels>" %>
<script type="text/javascript">


    $(document).ready(function () {

        preventSubmitOnEnter($("#frmApproverAuth"));

        setTitle("Leave Approval Path");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 425, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        FormatTextBox();

    });

    function setData(id, strEmpInitial, name) {
        document.getElementById('ApprovalAuthor_strAuthorID').value = id;
        document.getElementById('ApprovalAuthor_strEmpInitial').value = strEmpInitial;
        document.getElementById('ApprovalAuthor_strEmpName').value = name;

//        document.getElementById('hdnEmpId').value = id;

        $("#lblIdNotFound").css('visibility', 'hidden');
        $('#ApprovalAuthor_strAuthorID').removeClass("invalid");

        $("#divEmpList").dialog('close');

    }

    function Closing() {
        //window.location = "/LMS/LeaveType";
    }


    function deleteAuthor(Id) {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = "#divSetApproverDetails";
            var url = "/LMS/ApprovalPath/deleteAuthor/" + Id;
            var form = $("#frmApproverAuth");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }


    function GetAuthors() {
        var targetDiv = "#divSetApproverDetails";
        var url = "/LMS/ApprovalPath/GetAuthors";
        var form = $("#frmApproverAuth");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            $(targetDiv).html(result);

            document.getElementById('ApprovalAuthor_strAuthorID').value = '';
            document.getElementById('ApprovalAuthor_strEmpInitial').value = '';
            document.getElementById('ApprovalAuthor_strEmpName').value = '';
            document.getElementById('ApprovalAuthor_strAuthorType').value = '';


        }, "html");

        return false;
    }

    function AddAuthor() {

//        if (document.getElementById('hdnEmpId').value != document.getElementById('ApprovalAuthor_strAuthorID').value) {
//            $('#ApprovalAuthor_strAuthorID').addClass("invalid");
//        }

        if (fnValidate() == true) {
            var targetDiv = "#divSetApproverDetails";
            var url = "/LMS/ApprovalPath/AddAuthor";
            var form = $("#frmApproverAuth");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);
                var isblank = document.getElementById("BlnTextBlank").value;
                if (isblank == 'True') {

                    document.getElementById('ApprovalAuthor_strAuthorID').value = '';
                    document.getElementById('ApprovalAuthor_strEmpInitial').value = '';
                    document.getElementById('ApprovalAuthor_strEmpName').value = '';
                    document.getElementById('ApprovalAuthor_strAuthorType').value = '';
                }

            }, "html");
        }

        return false;
    }


    function save() {
        var targetDiv = "#divSetApproverDetails";
        var url = "/LMS/ApprovalPath/SetApprover";
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
            var url = "/LMS/ApprovalPath/Delete";
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

    function handleEnter(evt) {

        var keyCode = "";
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;

        }

        else if (evt) keyCode = evt.which;
        else return true;

        $('#ApprovalAuthor_strAuthorID').removeClass("invalid");

        if (keyCode == 13) {
            //handle enter
            var id = document.getElementById('ApprovalAuthor_strAuthorID').value;
            var name = "";

            // getdata(id, name);
            var url = "/LMS/Employee/LookUpById/" + id;

            $.post(url, "", function (result) {

                if (result[0] != null && result[0] != "") {
                    document.getElementById('ApprovalAuthor_strEmpName').value = result[1];
//                    document.getElementById('hdnEmpId').value = result[0];

                    $("#lblIdNotFound").css('visibility', 'hidden');
                }
                else {
                    document.getElementById('ApprovalAuthor_strEmpName').value = "";
//                    document.getElementById('hdnEmpId').value = "";
                    $("#lblIdNotFound").css('visibility', 'visible');
                }
            }, "json");                        
            return true;
        }
        return true;
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
            <%= Html.HiddenFor(m=>m.ApprovalPathMaster.intPathID)%>
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
                                Flow Name
                            </td>
                            <td>
                                <%=Html.TextBoxFor(m => m.ApprovalPathMaster.strPathName, new { @class = "textRegular",@readonly="readonly" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Step Name<label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%=Html.DropDownListFor(m => m.ApprovalAuthor.intNodeID, Model.Node, "...Select One...",new { @class = "selectBoxRegular required" ,onchange="return GetAuthors();"})%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Author Initial<label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%--<input type="hidden"  id="hdnEmpId"/>--%>
                                <%--<%=Html.TextBox("ApprovalAuthor.strAuthorID", Model.ApprovalAuthor.strAuthorID, new { @class = "textRegularDate required", onkeypress = "return handleEnter(event);" })%>--%>
                                
                                <%=Html.HiddenFor(m => m.ApprovalAuthor.strAuthorID)%>

                                <%=Html.TextBox("ApprovalAuthor.strEmpInitial", Model.ApprovalAuthor.strEmpInitial, new { @class = "textRegular required", @readonly = "readonly" })%>
                                <a href="#" id="btnElipse" class="btnSearch" onclick="return searchEmployee();"></a>
                                <label id="lblIdNotFound" style="visibility:hidden;vertical-align:5px; padding-left:10px; color:red;">Id not found !</label> 

                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Author Name<label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%=Html.TextBoxFor(m => m.ApprovalAuthor.strEmpName, new { @class = "textRegular", @readonly = "readonly" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Author Type<label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%= Html.DropDownListFor(m=>m.ApprovalAuthor.strAuthorType,Model.AuthType,"...Select One...", new { @class = "selectBoxRegular required" })%>
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
                                            Author Initial and Name
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
                                    <% if (Model.LstApprovalAuthor != null) for (int j = 0; j < Model.LstApprovalAuthor.Count; j++)
                                           { 
                                    %>
                                    <tr>
                                        <td style="width: 70%;">
                                            <%=Html.Hidden("LstApprovalAuthor[" + j + "].intAuthorityID", Model.LstApprovalAuthor[j].intAuthorityID.ToString())%>
                                            <%=Html.Hidden("LstApprovalAuthor[" + j + "].intPathID", Model.LstApprovalAuthor[j].intPathID.ToString())%>
                                            <%=Html.Hidden("LstApprovalAuthor[" + j + "].intNodeID", Model.LstApprovalAuthor[j].intNodeID.ToString())%>
                                            <%=Html.Hidden("LstApprovalAuthor[" + j + "].strAuthorID", Model.LstApprovalAuthor[j].strAuthorID.ToString())%>
                                            <%=Html.Hidden("LstApprovalAuthor[" + j + "].strEmpName", Model.LstApprovalAuthor[j].strEmpName.ToString())%>
                                            <%=Html.Hidden("LstApprovalAuthor[" + j + "].strEmpInitial", Model.LstApprovalAuthor[j].strEmpInitial.ToString())%>
                                            <%=Html.Encode(Model.LstApprovalAuthor[j].strEmpInitial.ToString()+" - "+Model.LstApprovalAuthor[j].strEmpName)%>
                                        </td>
                                        <td style="width: 20%;">
                                            <%=Html.Hidden("LstApprovalAuthor[" + j + "].strAuthorType", Model.LstApprovalAuthor[j].strAuthorType.ToString())%>
                                            <%=Html.Encode(Model.LstApprovalAuthor[j].strAuthorType)%>
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <a href='#' visible="false" class="gridDelete" onclick='javascript:return deleteAuthor("<%= Model.LstApprovalAuthor[j].strAuthorID %>");'>
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
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
