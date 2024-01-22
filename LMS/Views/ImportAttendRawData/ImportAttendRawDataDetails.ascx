<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ImportAttendRawDataModels>" %>
<script type="text/javascript">

    $(document).ready(function () {

        $("#divImageUploader").dialog({ autoOpen: false, modal: true, height: 40, width: 450, resizable: false, title: 'Upload File' });
        initFileUploadForm();

        preventSubmitOnEnter($("#frmImportAttendRawData"));

        setTitle("Import Attendance Raw Data");

        $("#btnSave").hide();
        FormatTextBox();


    });


    function save() {

        if (fnValidate() == true) {

            var targetDiv = "#divImportAttendRawData";
            var url = "/LMS/ImportAttendRawData/ImportAttendRawData";
            var form = $("#frmImportAttendRawData");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);

            }, "html");

        }

        return false;
    }


    function openFileUploader() {

        var IsFound = false;

        var form = $('#frmImportAttendRawData');
        var serializedForm = form.serialize();
        var url = '/LMS/ImportAttendRawData/GetImportAttendValidation';
        $.post(url, serializedForm, function (result) {
            IsFound = result;

            $("#txtUser").val(document.getElementById('strDBUser').value);
            $("#txtPass").val(document.getElementById('strDBPW').value);
                      
            $("#divImageUploader").dialog('open');

        }, "json");

        return false;
    }


    function uploadFile() {
        var fieldvalue = $('#file').val();

        if (fieldvalue != "") {
            return true;
        } else {
            var msg = '<label style="font-size: 10pt; font-weight: bold; color: Red;" id="lblMsg">Please select an mdb file.</label>';
            $("#divMsgStd").html(msg);
            return false;
        }
    }



    function initFileUploadForm() {
        var IsFound = false;
        var IsReplace = false;

        $("#ajaxUploadForm").ajaxForm({
            iframe: true,
            dataType: "json",
            beforeSubmit: function () {
                $("#ajaxUploadForm").block({ message: '<h1><img src="/LMS/Content/ajax-loader.gif" /> Uploading file...</h1>' });
            },
            success: function (result) {
                $("#ajaxUploadForm").unblock();
                $("#ajaxUploadForm").resetForm();
                //$.growlUI(null, result.message);
                var msg = "";

                if (result.flag.toString().toLowerCase() == "true") {
                    msg = '<label style="font-size: 10pt; font-weight: bold; color: Green;" id="lblMsg">' + result.message + '</label>';
                }
                else {
                    msg = '<label style="font-size: 10pt; font-weight: bold; color: Red;" id="lblMsg">' + result.message + '</label>';
                }
                $("#divImageUploader").dialog('close');
                $("#divMsgStd").html(msg);


            },
            error: function (xhr, textStatus, errorThrown) {
                $("#ajaxUploadForm").unblock();
                $("#ajaxUploadForm").resetForm();

                var msg = '<label style="font-size: 10pt; font-weight: bold; color: Red;" id="lblMsg">Error uploading file!</label>';
                $("#divMsgStd").html(msg);
            }
        });


    }
        
    
</script>
<form id="frmImportAttendRawData" method="post" action="">
<div id="divImportAttendRawData">
    <div class="divSpacer">
    </div>
    <div class="divRow">
        <div class="divCol1">
        </div>
        <div class="divCol2">
            <%= Html.HiddenFor(m => m.intRowID)%>
        </div>
    </div>
    <table class="contenttext" style="width: 100%;">
        <colgroup>
            <col style="width: 40%" />
            <col />
        </colgroup>
        <tr>
            <td colspan="2" style="height: 20px; text-align: center; font-weight: bold">
                Import Attendance Raw Data
            </td>
        </tr>


         <tr>
            <td class="contenttabletd">
                Database User ID:
            </td>
            <td style="width: 40%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.strDBUser, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Database Password:
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBoxFor(m => m.strDBPW, new { @class = "textRegular", @style = "width:200px", @maxlength = 50 })%>
            </td>
        </tr>

        <tr>
             <td class="contenttabletd">
                Upload File:
            </td>
            <td class="contenttabletd">
                <a id="btnImport" name="btnImport" style="visibility:visible;" href="#" class="btnElipse"
                    onclick="return openFileUploader();" />
            </td>
        </tr>
       
    </table>
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<%--<div class="divButton">
    <%if (LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.ImportAttendRawData, LMS.Web.Permission.MenuOperation.Edit))
      {%>
    <a href="#" class="btnSave" onclick="return save();"></a>
    <%} %>
    <input id="btnSave" style="visibility: hidden;" name="btnSave" type="submit" value="Save"
        visible="false" />
</div>--%>
<div id="divMsgStd" class="divMsg">
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>


<div class="divSpacer">
</div>
<div id="divImageUploader" class="divOuter">
    <div class="divRow">
        <div class="divCol2FreeSize">
            <form id="ajaxUploadForm" action="<%= Url.Action("ImportAttendRawData", "ImportAttendRawData")%>"
            method="post" enctype="multipart/form-data">
            <div>
                <input type="file" name="file" style="width:300px" id="file" accept="mdb" />
                <input id="ajaxUploadButton"  name="ajaxUploadButton" type="submit" value="Upload"
                    onclick="return uploadFile(this);" />
                <input type="hidden" name="txtUser" id="txtUser" />
                <input type="hidden" name="txtPass" id="txtPass" />
            </div>
            </form>
        </div>
    </div>
</div>




<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
            FileUpload
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>FileUpload</h2>
    
     <% using (Html.BeginForm("ImportAttendRawData", "ImportAttendRawData", 
                    FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>
        <input name="uploadFile" type="file" />
        <input type="submit" value="Upload File" />
<%} %>
 
</asp:Content>--%>