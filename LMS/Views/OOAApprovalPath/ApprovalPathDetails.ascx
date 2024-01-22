<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OOAApprovalPathModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmOOAApprovalPath"));

        setTitle("Leave Approval Path");

        $("#btnSave").hide();

        $("#btnDelete").hide();

        ChangeType();

    });


    function deleteNode(Id, index) {
        var result = confirm('Pressing OK will remove this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = "#divApprovalPathDetails";
            var url = "/LMS/OOAApprovalPath/DeleteNode/" + Id;
            var form = $("#frmOOAApprovalPath");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function AddNode() {
        if (fnValidate() == true) {
            var targetDiv = "#divApprovalPathDetails";
            var url = "/LMS/OOAApprovalPath/AddNode";
            var form = $("#frmOOAApprovalPath");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);
                var isblank = document.getElementById("BlnTextBlank").value;
                if (isblank == 'True') {
                    document.getElementById("OOAApprovalPathDetails_strNodeName").value = '';
                    document.getElementById("OOAApprovalPathDetails_intAuthorTypeID").value = '';
                    document.getElementById("OOAApprovalPathDetails_intParentNodeID").value = '';
                }

            }, "html");

        }
        return false;
    }
    
    function save() {

        var targetDiv = '#divApprovalPathDetails';
        var url = '/LMS/OOAApprovalPath/ApprovalPathAdd';
        var form = $('#frmOOAApprovalPath');
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
    

    function Delete() {

        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {

            var targetDiv = '#divApprovalPathDetails';
            var url = '/LMS/OOAApprovalPath/Delete';
            var form = $('#frmOOAApprovalPath');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        }
        return false;
    }


    function ChangeType() {

        var flowVal = $("#OOAApprovalPathMaster_intFlowType").val();
              
            if (flowVal == 2) {
                $("#OOAApprovalPathDetails_intAuthorTypeID option[value=1]").hide();
                $(".hide").hide();               
            }
            else {
                $("#OOAApprovalPathDetails_intAuthorTypeID option[value=1]").show();
                $(".hide").show();
            }

            // do whatever you want with the value
      
    }

</script>
<form id="frmOOAApprovalPath" method="post" action="">
<div id="divApprovalPath">
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <input type="hidden" value="<%= Model.BlnTextBlank %>" name="BlnTextBlank" id="BlnTextBlank"
                class="textRegular" />
            <input type="hidden" value="<%= Model.OOAApprovalPathMaster.intPathID %>" name="OOAApprovalPathMaster.intPathID"
                id="OOAApprovalPathMaster_intPathID" class="textRegular" />
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
                        <td class="contenttabletd" style="width: 25%">
                            Flow Type
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                            <%= Html.DropDownListFor(m => m.OOAApprovalPathMaster.intFlowType, Model.GetBillType, "Select One", new { @class = "selectBoxRegular required", onchange = "return ChangeType();" })%>
                           <%-- <%= Html.DropDownList("m=> m.OOAApprovalPathMaster.intFlowType", new SelectList(Model.GetBillType, "Value", "Text", Model.OOAApprovalPathMaster.intFlowType), "Select One", new { @class = "ddlRegularDate" })%> --%>
                            
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="contenttabletd" style="width: 25%">
                            Flow Name
                            <label class="labelRequired">
                                *</label>
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                            <%=Html.TextBoxFor(m => m.OOAApprovalPathMaster.strPathName, new { @class = "textRegular required", @maxlength = "50" })%>
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                        </td>
                    </tr>
                    <tr>
                        <td class="contenttabletd" style="width: 25%">
                            Step Name<label class="labelRequired">*</label>
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                            <%=Html.TextBoxFor(m => m.OOAApprovalPathDetails.strNodeName, new { @class = "textRegular required", @maxlength = "50" })%>
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                        </td>
                    </tr>
                    <tr>
                        <td class="contenttabletd" style="width: 25%">
                            Step Type<label class="labelRequired">*</label>
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                            <%= Html.DropDownListFor(m=>m.OOAApprovalPathDetails.intAuthorTypeID,Model.OOAAuthorType,"...Select One...", new { @class = "selectBoxRegular required" })%>
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                            <div class="hide">
                                <%= Html.CheckBoxFor(m => m.OOAApprovalPathDetails.isEdit)%> Edit Permission
                            </div>
                            
                            
                        </td>
                        <td class="contenttabletd">
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="contenttabletd" style="width: 25%">
                            Step Sequence
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                            <%= Html.DropDownListFor(m => m.OOAApprovalPathDetails.intParentNodeID, Model.ParentNode, "Initial Step", new { @class = "selectBoxRegular" })%>
                        </td>
                        <td class="contenttabletd" style="width: 25%">
                            <div>
                                <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ApprovalPath, LMS.Web.Permission.MenuOperation.Add))
                                  {%>
                                <a href="#" class="btnAdd" onclick="return AddNode();"></a>
                                <%} %>
                            </div>
                        </td>
                        <%--<td class="contenttabletd" style="width: 25%">
                            <% foreach (var item in Model.GetBillType) %>
                             <%  { %>
                                  <%= Html.CheckBox(item.TYPENAME) %> <%= Html.Label(item.TYPENAME) %>
                             <%  } %>
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="grid">
                    <div id="grid-data" style="overflow: auto;max-height:190px">
                        <table class="contenttext" style="width: 780px; ">
                            <thead>
                                <tr>
                                    <th style="width: 200px; text-align: left; padding-left: 5px;">
                                        Step Name
                                    </th>
                                    <th style="width: 140px; text-align: left; padding-left: 3px;">
                                        Step Type
                                    </th>
                                    <th style="width: 200px; text-align: left; padding-left: 4px;">
                                        Step Sequence
                                    </th>
                                    <th class="hide"  style="width: 180px; text-align: left; padding-left: 4px;">
                                        Edit Permission
                                    </th>
                                    <th style="width: 15px;">
                                    </th>
                                </tr>
                            </thead>
                        </table>
                        <div>
                            <% if (Model.LstOOAApprovalPathDetails != null && Model.LstOOAApprovalPathDetails.Count >= 3)
                               { 
                            %>
                            <table class="contenttext" style="width: 780px;">
                                <%}
                               else
                               {%>
                                <table class="contenttext" style="width: 780px;">
                                    <%} %>
                                    <% if (Model.LstOOAApprovalPathDetails != null) for (int j = 0; j < Model.LstOOAApprovalPathDetails.Count; j++)
                                           { 
                                    %>
                                    <tr>
                                        <td style="width: 200px;">
                                            <%=Html.Hidden("LstOOAApprovalPathDetails[" + j + "].intNodeID", Model.LstOOAApprovalPathDetails[j].intNodeID.ToString())%>
                                            <%--<%=Html.Hidden("LstOOAApprovalPathDetails[" + j + "].strNodeName", Model.LstOOAApprovalPathDetails[j].strNodeName.ToString())%>--%>
                                            <%--<%=Html.Encode(Model.LstOOAApprovalPathDetails[j].strNodeName)%>--%>
                                            <%= Html.TextBoxFor(m => m.LstOOAApprovalPathDetails[j].strNodeName, new { @class="textRegular"})%>                                            
                                            <%=Html.Hidden("LstOOAApprovalPathDetails[" + j + "].intPathID", Model.LstOOAApprovalPathDetails[j].intPathID.ToString())%>
                                        </td>
                                        <td style="width: 140px;">
                                            <%=Html.Hidden("LstOOAApprovalPathDetails[" + j + "].intAuthorTypeID", Model.LstOOAApprovalPathDetails[j].intAuthorTypeID.ToString())%>
                                            <%=Html.Hidden("LstOOAApprovalPathDetails[" + j + "].strAuthorType", Model.LstOOAApprovalPathDetails[j].strAuthorType.ToString())%>
                                            <%=Html.Encode(Model.LstOOAApprovalPathDetails[j].strAuthorType)%>                                            
                                            <%--<%= Html.DropDownList("LstOOAApprovalPathDetails[" + j + "].strAuthorType", new SelectList(Model.OOAAuthorType, "Value", "Text", Model.LstOOAApprovalPathDetails[j].intAuthorTypeID), "Select One", new { @class = "selectBoxRegular" })%> --%>
                                        </td>
                                        <td style="width: 200px;">
                                            <%=Html.Hidden("LstOOAApprovalPathDetails[" + j + "].intParentNodeID", Model.LstOOAApprovalPathDetails[j].intParentNodeID.ToString())%>
                                            <%=Html.Hidden("LstOOAApprovalPathDetails[" + j + "].strParentNode", string.IsNullOrEmpty(Model.LstOOAApprovalPathDetails[j].strParentNode) ? "Initial Step" : Model.LstOOAApprovalPathDetails[j].strParentNode)%>
                                            <%=Html.Encode(string.IsNullOrEmpty(Model.LstOOAApprovalPathDetails[j].strParentNode) ? "Initial Step" : Model.LstOOAApprovalPathDetails[j].strParentNode)%>
                                        </td>

                                        <td style="width: 180px;" class="hide">
                                            <%= Html.CheckBoxFor(m=> m.LstOOAApprovalPathDetails[j].isEdit)%>
                                        </td>
                                        <% if (Model.LstOOAApprovalPathDetails.Count >= 3)
                                           { 
                                        %>
                                        <td style="width: 15px;">
                                            <%}
                                           else
                                           {%>
                                            <td style="width: 15px;">
                                                <%} %>
                                                <a href='#' visible="false" class="gridDelete" onclick='javascript:return deleteNode(<%= Model.LstOOAApprovalPathDetails[j].intNodeID.ToString() %>,<%= j %>);'>
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
<div id="divMsgStd" class="divMsg" style="text-align:center">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
