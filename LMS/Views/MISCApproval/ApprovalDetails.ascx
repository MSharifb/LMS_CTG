<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MISCApprovalPathModels>" %>

<script type="text/javascript">

    $(document).ready(function () {

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        $("#divDialog").dialog({ autoOpen: false, modal: true, height: 500, width: 700, resizable: false, title: 'Attachment', beforeclose: function (event, ui) { Closing(); }
        });
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

    function savedata() {

        var miscDate = $("#MISCMaster_strMISCDATE").val();
        if (checkDateTime(miscDate) == false)
            return false;

        if (fnValidate() == true) {
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

    function save() {

        if (confirm('Do you want to save?') == false) {
            return false;
        }

        if (fnValidate() == true) {

            var targetDiv = "#divApprovalDetails";
            var url = "/LMS/MISCApproval/Recommend";
            var form = $("#frmMISC");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);

            }, "html");

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


    function Recommend() {

        if (confirm('Do you want to recommend?') == false) {
            return false;
        }

        var targetDiv = "#divApprovalDetails";
        var url = "/LMS/MISCApproval/Recommend";
        var form = $("#frmMISC");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            $(targetDiv).html(result);
            window.parent.searchApprovalData();
            $(".btnHide").hide();

        }, "html");

        return false;
    }


    function Reverify() {

        if (confirm('Do you want to reverify?') == false) {
            return false;
        }

        var targetDiv = "#divApprovalDetails";
        var url = "/LMS/MISCApproval/Reverify";
        var form = $("#frmMISC");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            $(targetDiv).html(result);
            $(".btnHide").hide();
            window.parent.searchApprovalData();
        }, "html");

        return false;
    }


    function Approve() {

        if (confirm('Do you want to approve?') == false) {
            return false;
        }
        var targetDiv = "#divApprovalDetails";
        var url = "/LMS/MISCApproval/Approve";
        var form = $("#frmMISC");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) {
            $(targetDiv).html(result);           
            $(".btnHide").hide();
            window.parent.searchApprovalData();
        }, "html");

        return false;
    }


    function closeModalDialog() {


        var d = window.parent.$("#divDialog");
        window.parent.$("#divDialog").dialog('close');


    }

    function ShowFile(id) {
        
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MISCApproval/AttachmentView/' + id;
        
        $('#styleOpenerView').attr({ src: url });
        $("#divDialog").dialog('open');
        return false;

    }


    </script>

<form id="frmMISC" method="post" action="">

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
            <%=Html.TextBoxFor(m => m.MISCMaster.EmpName, new { @class = "textRegular textLabelLike", @readonly = "true" })%>                        
        </td>
    </tr>
    <tr>
        <td>
            Designation 
        </td>
        <td>
            <%= Html.TextBoxFor(m => m.MISCMaster.StrDesignation, new { @class = "textRegCustomWidth textLabelLike", @style = "Width:400px", @readonly = "true" })%>             
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
               <%=Html.TextBoxFor(m => m.MISCMaster.strMISCDATE, new { @class = "textRegularDate required dtPicker date", @readonly = "true"})%>
           </td>
       </tr>

       <tr>
            <td>
                Name of Unit
            </td>
            <td>
                 <%= Html.DropDownList("MISCMaster.UNITID", new SelectList(Model.GetCompanyUnit, "Value", "Text", Model.MISCMaster.UNITID), "Select One", new { @class = "selectBoxLarge required", @readonly = "true" })%>
            </td>
       </tr>

    </table>

    <div style="width:98%; max-height:150px; float: left;overflow:auto">
    <table width="100%" id="dtRow">
           <colgroup>
            <col width="20%" />
            <col width="10%"/>
            <col width="17%"/>
            <col width="23%"/>
            <col width="20%"/>
            <col width="10%"/>
           </colgroup>
           <thead>
              <tr>
                <th align="center">
                    particular
                </th>                                    
                <th align="center" class="hideThis">
                    Amount
                </th>
                 <th align="center" class="hideThis">
                    Permitted Amount
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
             </tr>
         </thead>

         <tbody id="tblDetails">
                 <% if (Model.LstMISCDetails != null)
                    {
                    for (int i = 0; i < Model.LstMISCDetails.Count; i++) %>
                    <%{ %>
                         <tr>
                            <td>
                                <%= Html.HiddenFor(m=> m.LstMISCDetails[i].MISCDETAISLID) %>
                                <%= Html.TextBoxFor(m => m.LstMISCDetails[i].STRPARTICULAR, new { @class = "textRegCustomWidth", @style = "width:100%" })%>
                            </td>
                            <td>
                                <%= Html.TextBoxFor(m => m.LstMISCDetails[i].AMOUNT, new { @class = "textRegularNumber double required req",@readonly = "true", @style = "width:100%", maxlength = 10 })%>
                            </td>
                            <td>
                                <% if (Model.LstMISCDetails[i].APPROVEDAMOUNT > 0)
                                   { %>
                                    <%= Html.TextBoxFor(m => m.LstMISCDetails[i].APPROVEDAMOUNT, new { @class = "textRegularNumber double required req", @style = "width:100%", maxlength = 10 })%>
                                <%} %>

                                <% else
                                    {
                                        Model.LstMISCDetails[i].APPROVEDAMOUNT = Model.LstMISCDetails[i].AMOUNT; %>
                                <%= Html.TextBoxFor(m => m.LstMISCDetails[i].APPROVEDAMOUNT, new { @class = "textRegularNumber double required req", @style = "width:100%", maxlength = 10 })%>
                                <%} %>
                            </td>
                            <td>
                                <%= Html.TextBoxFor(m => m.LstMISCDetails[i].STRPURPOSE, new { @class = "textRegCustomWidth", @style = "width:100%" })%>
                            </td>
                            <td>
                                <%= Html.TextBoxFor(m => m.LstMISCDetails[i].STRREMARKS, new { @class = "textRegular", @style = "width:100%" })%>
                            </td>
                            <td align="center">
                                <%if (Model.LstMISCDetails[i].ATTACHMENTPATH !=null) if (Model.LstMISCDetails[i].ATTACHMENTPATH.Length > 0)
                                   { %>
                                        <%= Html.HiddenFor(m=> m.LstMISCDetails[i].ATTACHMENTPATH) %>
                                        <a class="btnAttachment"  onclick="return ShowFile('<%=Model.LstMISCDetails[i].MISCDETAISLID %>');" href="#" ></a>
                                <%} %>
                            </td>
                         </tr>
                    <%}
                }%>
         </tbody>
    </table>
    </div>
   
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>

   <div class="divButton">
    
    <%if (LMS.Web.LoginInfo.Current.strEmpID == Model.MISCMaster.STRAUTHORID)
      { %>
     <% if (Model.ApprovalStatus.ToUpper() == "RECOMMEND")
        {%>
            <a href="#" class="btnRecommended btnHide" onclick="return Recommend();"></a>
     <% } %>

     <% if (Model.ApprovalStatus.ToUpper() == "RECOMMEND AND REVERIFY")
        {%>
            <a href="#" class="btnReverify btnHide" onclick="return Reverify();"></a>
            <a href="#" class="btnRecommended btnHide" onclick="return Recommend();"></a>
     <% } %>
    
    <% if (Model.ApprovalStatus.ToUpper() == "ONLY APPROVE")
        {%>
            <a href="#" class="btnApproved btnHide" onclick="return Approve();"></a>
     <% } %>
    
      
     <% if (Model.ApprovalStatus.ToUpper() == "APPROVE")
        {%>
            <a href="#" class="btnReverify btnHide" onclick="return Reverify();"></a>
            <a href="#" class="btnApproved btnHide" onclick="return Approve();"></a>
     <% } %>
     <%} %>

     <%--<a href="#" class="btnClose" onclick="return closeModalDialog();"></a>--%>
   </div>

<div id="divMsgStd" class="divMsg">
    <label id="msglbl" class="MSG"></label>
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>

</div>

<div id="divDialog">
    <iframe id="styleOpenerView" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</form>

