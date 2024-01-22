<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.HRPolicyNameModels>" %>

<script type="text/javascript">

    function savedata() {
        
        if (fnValidate() == true) {

            $('#btnSave').trigger('click');
            window.parent.Refresh();
        }
        return false;
    }

    function clearData() {
        $("#HrPolicyTypeNameObj_HRPOLICYTYPENAME").val("");
        $("#HrPolicyTypeNameObj_REMARKS").val("");
    }

</script>

<form id="formHRPolicyTypeAdd" method="post" action="">
    
    <div>
        <table>
            <tr>
                <td>
                    HR Type Name
                </td>
                <td>
                    <%= Html.TextBoxFor(m => m.HrPolicyTypeNameObj.HRPOLICYTYPENAME, new {@class="textRegular required" })%>
                </td>
            </tr>

            <tr>
                <td>
                    Remarks
                </td>
                <td>
                    <%= Html.TextAreaFor(m => m.HrPolicyTypeNameObj.REMARKS, new { @class = "textRegular" })%>
                </td>
            </tr>
        </table>
    </div>

     <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>

   <div class="divButton">
   
        <% if (Model.HrPolicyTypeNameObj.HRPOLICYTYPENAMEID < 1)
           {%>     
                <a id="btnImgSave" href="#" class="btnSave" onclick="return savedata();"></a>
        <% } %>

        <% if (Model.HrPolicyTypeNameObj.HRPOLICYTYPENAMEID > 0)
           { %>
            <a id="A1" href="#" class="btnUpdate" onclick="return UpdateData();"></a>
            <a id="A3" href="#" class="btnDelete" onclick="return DeleteData();"></a>
        <%  } %>

        <a id="A2" href="#" class="btnClear" onclick="return clearData();"></a>

        <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>

        <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
   </div>

<div id="divMsgStd" class="divMsg">
    <label id="msglbl" class="MSG"></label>
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>

</form>