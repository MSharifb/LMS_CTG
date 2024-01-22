<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MISCModels>" %>


<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<link href="<%= ResolveUrl("~/Content/css/fileuploader.css") %>" rel="stylesheet" type="text/css" />
     

<script src="<%= Url.Content("~/Scripts/fileuploader.js") %>" type="text/javascript"></script>
  
<script type="text/javascript">

    $(document).ready(function () {
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });


         $("#divUpload").dialog({ autoOpen: false, modal: true, height: 40, width: 450, resizable: false, title: 'Upload File' });
          

    });


   


    function Closing()
    { }

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

    function savedata(){

        

        if (CheckAmount() == false)
            return false;
        

        var miscDate = $("#MISCMaster_strMISCDATE").val();
        if (checkDateTime(miscDate) == false)
            return false;

        if (fnValidate() == true) {

            if (confirm('Do you want to save?') == false) {
                return false;
            }

            var targetDiv = '#divMISCDetails';
            var url = "/LMS/MISC/Save";
            var form = $('#frmMISC');
          
            var serializedForm = form.serialize();
           // $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
            
            $.getJSON("/LMS/MISC/Save", serializedForm, getinSuccessData);
            window.parent.searchConvData();
        }

        return false;
    }


    getinSuccessData = function (data) {


        $("#msglbl").text(data.strMessage);
        if (data.intID > 0) {
            $("#MISCMaster_MISCMASTERID").val(data.intID);
        }

    };

    function setData(id, name) {
       
        $("#MISCMaster_STREMPID").val(id);
        $("#MISCMaster_EmpName").val(name);
        

        $("#divEmpList").dialog('close');
        //var url = "/LMS/MISC/Refresh";

        var targetDiv = "#divMISCDetails";
        var url = "/LMS/MISC/Refresh";
        var form = $("#frmMISC");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            
            $(targetDiv).html(result);

        }, "html");

        GetInfo(id);
    }

    function GetInfo(id) {
        var form = $("#frmMISC");
        var serializedForm = form.serialize();

        $.getJSON("getEmployeeInformation", serializedForm, updateFields);

    }

    updateFields = function (data) {
        $('#MISCMaster_StrDesignation').val(data.strDesignation);
        $('#MISCMaster_Strdepartment').val(data.strDepartment);
    };

    function AddNode() {

        if (1 == 1) {

            var targetDiv = "#divMISCDetails";
            var url = "/LMS/MISC/AddNode";
            var form = $("#frmMISC");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);

               // deleteRow('dtRow');

            }, "html");


        }
        return false;
    }

    function deleteNode(Id) {

        var result = confirm('Pressing OK will remove this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = "#divMISCDetails";
            var url = "/LMS/MISC/DeleteNode/" + Id;
            var form = $("#frmMISC");
            var serializedForm = form.serialize();

            $.get(url, serializedForm, function (result) { $(targetDiv).html(result);  });
        }
        return false;
    }

    function checkDateTime(dat) {

        var time;
        var date = dat;
        var dateArr = new Array();
        dateArr = date.split('-');
        var curDateobj = new Date();
        var curDate = new Date(curDateobj.getFullYear(), curDateobj.getMonth(), curDateobj.getDate());

        var newDate = new Date(dateArr[1] + '/' + dateArr[0] + '/' + dateArr[2]);
        if (newDate > curDate) {
            alert('Date should not be greater than current date.');
            return false;
        }

        return true;
    }


    function CheckAmount() {

        var count = $("#Count").val();

        for (var i = 0; i < count; i++) {

            var amount = $("#LstMISCDetails_" + i + "__AMOUNT").val();
           
            if(amount<1)
            {
                alert("Amount must be greater than 0");
                return false;
            }
        }

        return true;
        
    }

         

    function popupUpload(i) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MISC/Upload/'+i;
        $('#styleUpload').attr({ src: url });
        $("#divUpload").dialog('open');

        return false;
    }

    function closeUpload() {
        $("#divUpload").dialog('close');
    }

    function setValue(index, path,fileName) {
        var row = "LstMISCDetails_" + index + "__ATTACHMENTPATH";
        var pathName = "LstMISCDetails_" + index + "__ATTACHEDFILENAME";
        document.getElementById(row).value = path;
        document.getElementById(pathName).value = fileName;
        
        $("." + index).hide();
        
        $("#divUpload").dialog('close');
        
    }
    </script>



<form id="frmMISC" method="post" action="/LMS/MISC/uploadfiles" enctype = "multipart/form-data">





    <div id="divMisc">

 <%= Html.HiddenFor(m => m.MISCMaster.STREMPID)%> 
 <%= Html.HiddenFor(m=> m.MISCMaster.MISCMASTERID) %>
 <%--<%= Html.HiddenFor(m=> m.LstMISCDetails) %>--%>
  <%--<%= Html.HiddenFor(m => m.MISCMaster.MISCDATE)%>--%>
    <table width="100%">
    <tr>
        <td>
           Employee Name
        </td>
        <td>     
            <% if (Model.MISCMaster.MISCMASTERID < 1)
               { %>    
            <%= 
                Html.TextBoxFor(m => m.MISCMaster.EmpName, new { @class = "textRegular required", @readonly = "true" })
                %> 
            <%  if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOfflineMisc))
                { %>           
                    <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
            <%  } %>

            <%} %>

            <% else
                { %>
                <%= Html.TextBoxFor(m => m.MISCMaster.EmpName, new { @class = "textRegular textLabelLike", @readonly = "true" })%>
               
            <%} %>
        </td>
    </tr>
    <tr>
        <td>
            Designation 
        </td>
        <td>
            <%= Html.TextBoxFor(m => m.MISCMaster.StrDesignation, new { @class = "textRegCustomWidth textLabelLike" ,@style="Width:400px",@readonly = "true"})%>             
        </td>
        </tr>
        <tr>
            <td>
                Department
            </td>
            <td>                  
                <%= Html.TextBoxFor(m => m.MISCMaster.Strdepartment, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:400px", @readonly = "true" })%>                   
            </td>
        </tr>
        <tr>
           <td>
               Date
           </td>
           <td>
           
               <%--<%=Html.TextBoxFor(m => m.MISCMaster.MISCDATE, new { @class = "textRegularDate dtPicker date", @style = "width:80px; min-width:80px;", @maxlength = 10 })%>--%>
               <%=Html.TextBoxFor(m => m.MISCMaster.strMISCDATE, new { @class = "textRegularDate required dtPicker date"})%>
              <%-- <%=Html.TextBox("MISCMaster.MISCDATE",Model.MISCMaster.MISCDATE, new { @class = "required", @maxlength = "10" })%>--%>

               
           </td>
       </tr>

       <tr>
            <td>
                Name of Unit
            </td>
            <td>
                 <%= Html.DropDownList("MISCMaster.UNITID", new SelectList(Model.GetCompanyUnit, "Value", "Text", Model.MISCMaster.UNITID), "Select One", new { @class = "selectBoxLarge required" })%>
            </td>
       </tr>
    </table>

    <div style="width:98%; max-height:150px; float: left;overflow:auto">
    <table width="100%" id="dtRow">
           <colgroup>
            <col width="20%"/>
            <col width="10%"/>
            <col width="25%"/>
            <col width="25%"/>  
            <col width="15%"/>                     
            <col width="5%"/>
           </colgroup>
           <thead>
              <tr>
                <th align="center">
                    Particular
                </th>                                    
                <th align="center" class="hideThis">
                    Amount
                </th>
                <th align="center">
                    Purpose
                </th>     
                <th>
                    Remarks
                </th>  
                <th>
                    Attachment
                </th> 
                <th></th>                            
             </tr>
         </thead>

         <tbody id="tblDetails">

                
                 <% if (Model.LstMISCDetails != null)
                    {
                        int count = Model.LstMISCDetails.Count;
                        for (int i = 0; i < Model.LstMISCDetails.Count; i++) %>
                                <%{ %>
                                <%= Html.Hidden("Count",count) %>
                                <tr>
                                   <td>
                                        <%= Html.TextBoxFor(m => m.LstMISCDetails[i].STRPARTICULAR, new { @class = "textRegCustomWidth required", @style = "width:100%" })%>
                                   </td>
                                   <td>
                                        <%= Html.TextBoxFor(m => m.LstMISCDetails[i].AMOUNT, new { @class = "textRegularNumber double required req", @style = "width:100%", maxlength = 10 })%>
                                   </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.LstMISCDetails[i].STRPURPOSE, new { @class = "textRegCustomWidth required", @style = "width:100%" })%>
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m=> m.LstMISCDetails[i].STRREMARKS,new{@class="textRegular"}) %>
                                    </td>
                                    <td align="center">    
                                        <%= Html.HiddenFor(m => m.LstMISCDetails[i].ATTACHMENTPATH)%>
                                        <%= Html.TextBoxFor(m => m.LstMISCDetails[i].ATTACHEDFILENAME, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:150px", @readonly = "true" })%>

                                        <% if (Model.LstMISCDetails[i].ATTACHMENTPATH != null)
                                           {

                                               if (Model.LstMISCDetails[i].ATTACHMENTPATH.Length < 1)
                                               { %>
                                            <a id="btnImport" name="btnImport" style="visibility:visible;" href="#" class="btnElipse  <%= i %>"
                                            onclick="return popupUpload(<%= i %>);" />
                                       <%} %>
                                             <%  else if (Model.LstMISCDetails[i].MISCMASTERID > 0)
                                            {%>
                                                  <%= Html.Encode(Model.LstMISCDetails[i].ATTACHMENTPATH)%> 
                                         <% }
                                           }
                                                %>
                                               <% else
                                            {%>
                                                   <a id="A1" name="btnImport" style="visibility:visible;" href="#" class="btnElipse  <%= i %>"
                                                    onclick="return popupUpload(<%= i %>);" />
                                      <%} %>
                                                                          
                                    </td>

                                    <% if (count  > 1) %>
                                    <%{ %>
                                    <td>
                                        <a href='#' class="gridDelete" onclick='javascript:return deleteNode(<%= i %>);'>
                                        </a>
                                    </td>
                                    <%} %>

                                    
                                </tr>

                               
                 <%}
                        
                         
                }%>
         </tbody>
    </table>
    </div>
    <div style="width: 2%; float: left;">
               <a href="#" id="hrfAdd" class="gridAdd" onclick="return AddNode();"></a>
    </div>

    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>

   <div class="divButton">
   <a id="btnImgSave" href="#" class="btnSave" onclick="return savedata();"></a>
   
   <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
   </div>

<div id="divMsgStd" class="divMsg">
    <label id="msglbl" class="MSG"></label>
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>

</div>

<div id="divUpload">
    <iframe id="styleUpload" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</form>

<div class="divSpacer">
</div>

<%--<div id="divImageUploader" class="divOuter">
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
</div>--%>


<%--<div id="dvfileUploader">
    <form id="formFilUpload" action="/LMS/MISC/uploadfiles" method="post" enctype="multipart/form-data">
    <label for="file">Filename:</label>
    <input type="file" name="file" id="file1" />
    <%--<a href="#" onclick="UploadFile()">Upload</a>-%>
    <input type="submit" name="submit" value="Submit" />
</form>
</div>--%>