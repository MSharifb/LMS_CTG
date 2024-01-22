<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ApprovalPathModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmApprovalPath"));

        setTitle("Leave Approval Path");

        $("#btnSave").hide();

        $("#btnDelete").hide();

    });


    function deleteNode(Id, index) {
        var result = confirm('Pressing OK will remove this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = "#divApprovalPathDetails";
            var url = "/LMS/ApprovalPath/DeleteNode/" + Id;
            var form = $("#frmApprovalPath");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function AddNode() {
        if (fnValidate() == true) {
            var targetDiv = "#divApprovalPathDetails";
            var url = "/LMS/ApprovalPath/AddNode";
            var form = $("#frmApprovalPath");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);
                var isblank = document.getElementById("BlnTextBlank").value;
                if (isblank == 'True') {
                    document.getElementById("ApprovalPathDetails_strNodeName").value = '';
                    document.getElementById("ApprovalPathDetails_intAuthorTypeID").value = '';
                    document.getElementById("ApprovalPathDetails_intParentNodeID").value = '';
                }

            }, "html");

        }
        return false;
    }

    function save() {

        var targetDiv = '#divApprovalPathDetails';
        var url = '/LMS/ApprovalPath/ApprovalPathAdd';
        var form = $('#frmApprovalPath');
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }


    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divApprovalPathDetails';
            var url = '/LMS/ApprovalPath/Delete';
            var form = $('#frmApprovalPath');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }

</script>
<form id="frmApprovalPath" method="post" action="">
<div id="divApprovalPath">
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <input type="hidden" value="<%= Model.BlnTextBlank %>" name="BlnTextBlank" id="BlnTextBlank"
                class="textRegular" />
            <input type="hidden" value="<%= Model.ApprovalPathMaster.intPathID %>" name="ApprovalPathMaster.intPathID"
                id="ApprovalPathMaster_intPathID" class="textRegular" />
        </div>
    </div>
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <table style="width: 100%;">
        <tr>
            <td>
                <table class="contenttable" style="width: 100%;">
                    <tr>
                        <td class="contenttabletd" style="width: 30%">
                            Approval Group
                            <label class="labelRequired">*</label>
                        </td>
                        <td class="contenttabletd" style="width: 30%">
                            <%= Html.DropDownListFor(m => m.ApprovalPathMaster.intApprovalGroupId, Model.AprovalGroupType, "...Select One...", new { @class = "selectBoxRegular" })%>
                        </td>
                        <td class="contenttabletd" style="width: 30%">
                        </td>
                        <td class="contenttabletd" style="width: 30%">
                        </td>
                    </tr>
                    <tr>
                        <td class="contenttabletd">
                            Approval Process
                            <label class="labelRequired">*</label>
                        </td>
                        <td class="contenttabletd">
                            <%= Html.DropDownListFor(m => m.ApprovalPathMaster.intApprovalProcessId, Model.ApprovalProcessList, "...Select One...", new { @class = "selectBoxRegular" })%>
                        </td>
                        <td class="contenttabletd">
                        </td>
                        <td class="contenttabletd">
                        </td>
                    </tr>
                    <tr>
                        <td class="contenttabletd">
                            Flow Name
                            <label class="labelRequired">*</label>
                        </td>
                        <td class="contenttabletd">
                            <%=Html.TextBoxFor(m => m.ApprovalPathMaster.strPathName, new { @class = "textRegular required", @maxlength = "50" })%>
                        </td>
                        <td class="contenttabletd">
                        </td>
                        <td class="contenttabletd">
                        </td>
                    </tr>
                    <tr>
                        <td class="contenttabletd">
                            Step Name<label class="labelRequired">*</label>
                        </td>
                        <td class="contenttabletd">
                            <%=Html.TextBoxFor(m => m.ApprovalPathDetails.strNodeName, new { @class = "textRegular required", @maxlength = "50" })%>
                        </td>
                        <td class="contenttabletd">
                        </td>
                        <td class="contenttabletd">
                        </td>
                    </tr>
                    <tr>
                        <td class="contenttabletd">
                            Step Type<label class="labelRequired">*</label>
                        </td>
                        <td class="contenttabletd">
                            <%= Html.DropDownListFor(m=>m.ApprovalPathDetails.intAuthorTypeID,Model.AuthorType,"...Select One...", new { @class = "selectBoxRegular required" })%>
                        </td>
                        <td class="contenttabletd">
                        </td>
                        <td class="contenttabletd">
                        </td>
                    </tr>
                    <tr>
                        <td class="contenttabletd">
                            Forward From
                        </td>
                        <td class="contenttabletd">
                            <%= Html.DropDownListFor(m => m.ApprovalPathDetails.intParentNodeID, Model.ParentNode, "Initial Step", new { @class = "selectBoxRegular" })%>
                        </td>
                        <td class="contenttabletd">
                            <div>
                                <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalPath, LMS.Web.Permission.MenuOperation.Add))
                                  {%>
                                <a href="#" class="btnAdd" onclick="return AddNode();"></a>
                                <%} %>
                            </div>
                        </td>
                        <td class="contenttabletd">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="grid">
                    <div id="grid-data">
                        <table class="contenttext" style="width: 680px;">
                            <thead>
                                <tr>
                                    <th style="width: 225px; text-align: left; padding-left: 5px;">
                                        From
                                    </th>
                                    <th style="width: 150px; text-align: left; padding-left: 3px;">
                                        Step Type
                                    </th>
                                    <th style="width: 225px; text-align: left; padding-left: 4px;">
                                        Forward To
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                            </thead>
                        </table>
                        <div style="overflow: auto; max-height: 140px">
                            <% if (Model.LstApprovalPathDetails != null && Model.LstApprovalPathDetails.Count >= 3)
                               { 
                            %>
                            <table class="contenttext" style="width: 662px;">
                                <%}
                               else
                               {%>
                                <table class="contenttext" style="width: 680px;">
                                    <%} %>
                                    <% if (Model.LstApprovalPathDetails != null) for (int j = 0; j < Model.LstApprovalPathDetails.Count; j++)
                                           { 
                                    %>
                                    <tr>
                                        <td style="width: 225px;">
                                            <%=Html.Hidden("LstApprovalPathDetails[" + j + "].intParentNodeID", Model.LstApprovalPathDetails[j].intParentNodeID.ToString())%>
                                            <%=Html.Hidden("LstApprovalPathDetails[" + j + "].strParentNode", string.IsNullOrEmpty(Model.LstApprovalPathDetails[j].strParentNode) ? "Initial Step" : Model.LstApprovalPathDetails[j].strParentNode)%>
                                            <%=Html.Encode(string.IsNullOrEmpty(Model.LstApprovalPathDetails[j].strParentNode) ? "Initial Step" : Model.LstApprovalPathDetails[j].strParentNode)%>
                                        </td>
                                        <td style="width: 150px;">
                                            <%=Html.Hidden("LstApprovalPathDetails[" + j + "].intAuthorTypeID", Model.LstApprovalPathDetails[j].intAuthorTypeID.ToString())%>
                                            <%=Html.Hidden("LstApprovalPathDetails[" + j + "].strAuthorType", Model.LstApprovalPathDetails[j].strAuthorType.ToString())%>
                                            <%=Html.Encode(Model.LstApprovalPathDetails[j].strAuthorType)%>
                                        </td>
                                        <td style="width: 225px;">
                                            <%=Html.Hidden("LstApprovalPathDetails[" + j + "].intNodeID", Model.LstApprovalPathDetails[j].intNodeID.ToString())%>
                                            <%=Html.Hidden("LstApprovalPathDetails[" + j + "].strNodeName", Model.LstApprovalPathDetails[j].strNodeName.ToString())%>
                                            <%=Html.Encode(Model.LstApprovalPathDetails[j].strNodeName)%>
                                            <%=Html.Hidden("LstApprovalPathDetails[" + j + "].intPathID", Model.LstApprovalPathDetails[j].intPathID.ToString())%>
                                        </td>
                                        <% if (Model.LstApprovalPathDetails.Count >= 3)
                                           { 
                                        %>
                                        <td style="width: 25px;">
                                            <%}
                                           else
                                           {%>
                                            <td style="width: 80px;">
                                                <%} %>
                                                <a href='#' visible="false" class="gridDelete" onclick='javascript:return deleteNode(<%= Model.LstApprovalPathDetails[j].intNodeID.ToString() %>,<%= j %>);'>
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
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <% if (Model.IsModeEdit == true)
       {
    %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalPath, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return save();" visible="false"></a>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%} %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalPath, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <a href="#" class="btnDelete" onclick="return Delete();" visible="false"></a>
    <%} %>
    <%} %>
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
