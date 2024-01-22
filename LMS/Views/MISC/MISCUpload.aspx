<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.MISCModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {


            var pathValue = $("#Path").val();

            var index = $("#Index").val();

            if (pathValue.length > 0) {
                var index = $("#Index").val();
                var path = "LstMISCDetails_" + index + "__ATTACHMENTPATH";
                var fileName = $("#attachedFileName").val();
                $("#lblMsg").text('Successfully uploaded');
                window.parent.setValue(index, pathValue, fileName);
            }
        });

        function upload() {

            var val = document.getElementById("fileUpload");

            if (val.value.length > 0) {
                $('#Submit').trigger('click');
            }
            else {
                alert('Please select an image file.');
            }
        }

    </script>
    
    
    <form id="formUpload" action="/LMS/MISC/uploadfiles" method="post" enctype="multipart/form-data">
        
        <%= Html.HiddenFor(m=> m.startRowIndex) %>
        <%= 
            Html.HiddenFor(m => m.LstMISCDetails[0].ATTACHMENTPATH)
            %>
        <%= Html.Hidden("Path",Model.LstMISCDetails[0].ATTACHMENTPATH) %>

        <%= Html.Hidden("Index",Model.startRowIndex) %>
        <%= Html.Hidden("attachedFileName",Model.FileName) %>
        
        <div style="margin:10% 10% 0% 13%">
            <input type="file" name="file" id="fileUpload" class="required" />
               
            <input type="button"  value="Upload" name="Click" onclick="return upload()" />
            <input type="submit"  id="Submit" style="display:none" />

            <div style="text-align:center;color:Green;font-weight:bold;margin-top:10px">
                <label id="lblMsg"></label>
            </div>
        </div>
        
    </form>

</asp:Content>
