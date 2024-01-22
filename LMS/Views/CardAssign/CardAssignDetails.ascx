<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CardAssignModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

//        $("#datepicker").datepicker({
//            changeMonth: true,
//            changeYear: false
//        }).click(function () { $('#CardAssign_strEffectiveDate').datepicker('show'); });

        $("#cardInfoDiv").dialog({ autoOpen: false, modal: true, height: 280, width: 335, resizable: false, title: 'Card Info Search', beforeclose: function (event, ui) { Closing(); } });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        preventSubmitOnEnter($("#frmCardAssign"));

        setTitle("Card Assignment");
        $("#dvEmpID").hide();
        $("#divCardId").hide();
        $("#btnSave").hide();

        $("#btnDelete").hide();

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });
        FormatTextBox();


    });

    function openEmployee() {


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
    
    function closeCardInfo() {
        $("#cardInfoDiv").dialog('close');
    }

    function popupCardInfoAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/CardInfo/CardInfoSearch';
        $('#styleAdvance').attr({ src: url });
        $("#cardInfoDiv").dialog('open');
        return false;
    }

    function setCardInfo(intCardID, strCardID) {
        document.getElementById('CardAssign_intCardID').value = intCardID;
        document.getElementById('CardAssign_strCardID').value = strCardID;

        $("#cardInfoDiv").dialog('close');

    }

    function setData(id, name) {
        document.getElementById('CardAssign_strEmpName').value = name;
        document.getElementById('CardAssign_strEmpID').value = id;
        GetInfo(id);
        $("#divEmpList").dialog('close');
    }

    function searchEmployee() {
        window.parent.openEmployee();
    }

    updateFields = function (data) {
        $('#CardAssign_strDesignation').val(data.strDesignation);
        $('#CardAssign_strDepartment').val(data.strDepartment);
    };

    function GetInfo(id) {
        var form = $("#frmCardAssign");
        var serializedForm = form.serialize();

        $.getJSON("getEmployeeInformation", serializedForm, updateFields);

    }

    function save() {
        if (fnValidateDateTime() == false) {
            alert("Invalid Date.");
            return false;
        }
        if (fnValidate() == true) {
            $('#btnSave').trigger('click');

        }
        return false;
    }



    function Delete() {
        var result = confirm('Pressing OK will delete this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = '#divCardAssignDetails';
            var url = '/LMS/CardAssign/Delete';
            var form = $('#frmCardAssign');
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }

    function Closing() {

    }



   
    
</script>
<form id="frmCardAssign" method="post" action="">
<div id="divCardAssign">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.CardAssign.intCardAssignID)%>
             <%= Html.HiddenFor(m => m.IsNew)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 25%" />
            <col />
        </colgroup>
        <tr>
            <td>
                Assign ID
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <%=Html.TextBoxFor(m => m.CardAssign.strAssignID, new { @class = "textRegular required", maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td>
                Employee Name
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <div id="dvEmpID">
                    <%--<%= Html.TextBoxFor(m => m.CardAssign.strEmpID)%>--%>
                </div>
                <%= Html.TextBoxFor(m => m.CardAssign.strEmpName, new { @class = "textRegular required", @readonly = "true" })%>
                <% if (Model.IsNew) %>
                <%{ %>
                <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
                <%}%>
            </td>
        </tr>
        <tr>
            <td>
                Employee ID
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.CardAssign.strEmpID, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:200px" })%>
            </td>
        </tr>
        <tr>
            <td>
                Designation
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.CardAssign.strDesignation, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:400px" })%>
            </td>
        </tr>
        <tr>
            <td>
                Department
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.CardAssign.strDepartment, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:400px" })%>
            </td>
        </tr>        
        <tr>
            <td>
                Card ID
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                <div id="divCardId">
                    <%= Html.TextBoxFor(m => m.CardAssign.intCardID)%>
                </div>
                <%= Html.TextBoxFor(m => m.CardAssign.strCardID, new { @class = "textRegular required", @readonly = "true" })%>
                <a href="#" class="btnSearch" onclick="return popupCardInfoAdd();"></a>
            </td>
        </tr>
        <tr>
            <td>
                Effective Date
                <label class="labelRequired">
                    *</label>
            </td>
            <td>
                
                <%=Html.TextBoxFor(m => m.CardAssign.strEffectiveDate, new { @class = "textRegularDate dtPicker date", maxlength = 10 })%>             
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
        </tr>
    </table>
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <%if (Model.CardAssign.intCardAssignID > 0)
      { %>
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CardAssign, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnUpdate" onclick="return save();"></a>
    <%} %>
    <%}
      else
      {%>
    <a href="#" class="btnSave" onclick="return save();"></a>
    <%} %>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CardAssign, LMS.Web.Permission.MenuOperation.Delete))
      {%>
    <%if (Model.CardAssign.intCardAssignID > 0)
      { %>
    <a href="#" class="btnDelete" onclick="return Delete();"></a>
    <%} %>
    <%} %>
     <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
</div>
<div id="cardInfoDiv">
    <iframe id="styleAdvance" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
