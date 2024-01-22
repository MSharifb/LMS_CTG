﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MembershipTypeNameModels>" %>

<script type="text/javascript">

    function savedata() {

        if (fnValidate() == true) {

            $('#btnSave').trigger('click');
            window.parent.Refresh();
        }
        return false;
    }

    function clearData() {
        $("#MembershipTypeNameObj_MEMBERSHIPTYPENAME").val("");
        $("#MembershipTypeNameObj_REMARKS").val("");
    }

</script>

<form id="formMembershipTypeAdd" method="post" action="">
    
    <div>
        <table>
            <tr>
                <td>
                    Membership Type Name
                </td>
                <td>
                    <%= Html.TextBoxFor(m => m.MembershipTypeNameObj.MEMBERSHIPTYPENAME, new {@class="textRegular required" })%>
                </td>
            </tr>

            <tr>
                <td>
                    Remarks
                </td>
                <td>
                    <%= Html.TextAreaFor(m => m.MembershipTypeNameObj.REMARKS, new { @class = "textRegular" })%>
                </td>
            </tr>
        </table>
    </div>

     <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>

   <div class="divButton">
   <a id="btnImgSave" href="#" class="btnSave" onclick="return savedata();"></a>
   
   <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />

   <a id="A2" href="#" class="btnClear" onclick="return clearData();"></a>
   <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
   </div>

<div id="divMsgStd" class="divMsg">
    <label id="msglbl" class="MSG"></label>
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>

</form>
