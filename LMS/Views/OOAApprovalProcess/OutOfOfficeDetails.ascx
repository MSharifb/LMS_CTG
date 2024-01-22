<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OutOfOfficeModels>" %>
<script type="text/javascript">


    var isAdmin = 0;
    $(document).ready(function () {

        preventSubmitOnEnter($("#OutOfOfficeAdd"));

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });

        $(".timePicker").timepicker({ ampm: true, timeFormat: 'hh:mm TT' });

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        $("#otherPurpose").hide();
        $("#dvStrID").hide();
        $("#OutOfOffice_EXPGETINTIME").timepicker({ ampm: true });
        checkRadio();
        $('input:radio').attr('disabled', 'disabled');

        var roundtripVal = $("#RoundTrip").val();
        if (roundtripVal == "True") {

            $("#hrfAdd").hide();
        }

        checkUrl();

    });


    function checkUrl() {

        var url = document.URL;
        if (url == undefined) {
            url = '';
        }
    
        // If the parameter exists create the message and insert into our paragraph
        if (url.search('FromMail=true')>-1) {
            $(".btnClose").hide();
        }

    }

    function Closing() {

    }


    function timeChanged()
    {


        if ($("#EditPermission").val() == 'True') {
            
            if ($("#ApproverComment").attr('class').search('required') < 0) {
                $("#ApproverComment").addClass("required");
            
            }
        }
     }

    function closeAdvanceDate() {
        $("#advanceDiv").dialog('close');
    }

    function checkRadio() {

        var val = $("#OutOfOffice_PURPOSE").val();

        $("input:radio").each(function () {

            if ($(this).val() == val) {
                $(this).attr('checked', true);
                ShowHideModeAndAmount(this);
                if (val == "Other") {
                    $("#otherPurpose").show();
                }
            }
        });

    }




    function SetHiddenValue(rdo) {

        ShowHideModeAndAmount(rdo);
        $("#OutOfOffice_PURPOSE").val(rdo.value);
        if (rdo.value == "Other") {
            $("#otherPurpose").show();
        }
        else
            $("#otherPurpose").hide();

    }

    function ShowHideModeAndAmount(id) {

        if ($(id).attr('class') == 'hide') {
            $(".hideThis").hide();
        }
        else
            $(".hideThis").show();
    }

    function openEmployee(type) {

        isAdmin = type;

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

    function popupOutAdvanceAdd() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/OutOfOfficeDetails/Advance';
        $('#styleAdvance').attr({ src: url });
        $("#advanceDiv").dialog('open');
        return false;
    }


    function HookupCalculation(cnt) {

        alert(cnt.val());
    }


    function searchEmployee() {
        window.parent.openEmployee();
    }

    updateFields = function (data) {
        $('#OutOfOffice_Strdesignaiton').val(data.strDesignation);
        $('#OutOfOffice_Strdepartment').val(data.strDepartment);
    };

    function GetInfo(id) {
        var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();

        $.getJSON("getEmployeeInformation", serializedForm, updateFields);

    }

    function setData(id, name) {

        if (isAdmin == 0) {
            document.getElementById('OutOfOffice_RESPONSIBLEPERSONID').value = id;
            document.getElementById('OutOfOffice_RESPONSIBLEPERSON').value = name;
        }
        else {

            document.getElementById('OutOfOffice_STREMPID').value = id;
            document.getElementById('OutOfOffice_EMPNAME').value = name;
            GetInfo(id);
        }
        $("#divEmpList").dialog('close');
    }

    function setDateTime(date, time) {

        document.getElementById('OutOfOffice_STRGETOUTDATE').value = date;
        document.getElementById('OutOfOffice_GETOUTTIME').value = time;

        $("#advanceDiv").dialog('close');

    }

    function save() {

        return GetOut();

    }

    function Permission() {

        if ($("#OutOfOffice_PURPOSE").val() == "") {
            alert('Purpose can not be blank.');
            return false;
        }
        if (fnValidate() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/OOAApprovalProcess/Permission";
            var form = $("#OutOfOfficeAdd");
            var serializedForm = form.serialize();

            $.getJSON(url, serializedForm, function (result) {
                $("#msglbl").text(result.Message);
                $(".btnHide").hide();
            });

           // window.parent.closeDialogBox1();
        }
       
        return false;
    }


    function Verify() {

        if ($("#OutOfOffice_PURPOSE").val() == "") {
            alert('Purpose can not be blank.');
            return false;
        }
        if (fnValidate() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/OOAApprovalProcess/Verify";
            var form = $("#OutOfOfficeAdd");
            var serializedForm = form.serialize();

            $.getJSON(url, serializedForm, function (result) {
                $("#msglbl").text(result.Message);
                $(".btnHide").hide();

            });

           // window.parent.closeDialogBox1();
        }
        
        return false;
    }

    function Recommend() {

        if ($("#OutOfOffice_PURPOSE").val() == "") {
            alert('Purpose can not be blank.');
            return false;
        }
        if (fnValidate() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/OOAApprovalProcess/Recommend";
            var form = $("#OutOfOfficeAdd");
            var serializedForm = form.serialize();

            $.getJSON(url, serializedForm, function (result) {
                $("#msglbl").text(result.Message);
                $(".btnHide").hide();
            });

           // window.parent.closeDialogBox1();
        }
       
        return false;

    }

    function PermittedAndVerified() {
        if ($("#OutOfOffice_PURPOSE").val() == "") {
            alert('Purpose can not be blank.');
            return false;
        }
        if (fnValidate() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/OOAApprovalProcess/PermittedAndVerified";
            var form = $("#OutOfOfficeAdd");
            var serializedForm = form.serialize();

            $.getJSON(url, serializedForm, function (result) {
                $("#msglbl").text(result.Message);
                $(".btnHide").hide();
            });

        }
        //window.parent.closeDialogBox1();
        return false;
    }

    function Approve() {

        if ($("#OutOfOffice_PURPOSE").val() == "") {
            alert('Purpose can not be blank.');
            return false;
        }
        if (fnValidate() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/OOAApprovalProcess/Approve";
            var form = $("#OutOfOfficeAdd");
            var serializedForm = form.serialize();

            $.getJSON(url, serializedForm, function (result) {
               
                $("#msglbl").text(result.Message);
                $(".btnHide").hide();
            });
            //window.parent.closeDialogBox1();
        }
       
        return false;
    }

    function Reverify() {

        if ($("#OutOfOffice_PURPOSE").val() == "") {
            alert('Purpose can not be blank.');
            return false;
        }
        if (fnValidate() == true) {

            var targetDiv = "#divDataList";
            var url = "/LMS/OOAApprovalProcess/Reverify";
            var form = $("#OutOfOfficeAdd");
            var serializedForm = form.serialize();

            $.getJSON(url, serializedForm, function (result) {

                $("#msglbl").text(result.Message);
                $(".btnHide").hide();
            });
          // window.parent.closeDialogBox1();
        }

        return false;
    }


    function AddNode() {

        if (1 == 1) {

            var targetDiv = "#divApprovalPathDetails";
            var url = "/LMS/OOAApprovalProcess/AddNode";
            var form = $("#OutOfOfficeAdd");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) {
                $(targetDiv).html(result);


            }, "html");


        }
        return false;
    }

    function deleteNode(Id) {
        var result = confirm('Pressing OK will remove this record. Do you want to continue?');
        if (result == true) {
            var targetDiv = "#divApprovalPathDetails";
            var url = "/LMS/OOAApprovalProcess/DeleteNode/" + Id;
            var form = $("#OutOfOfficeAdd");
            var serializedForm = form.serialize();

            $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        }
        return false;
    }


    function closeModalDialog() {


        var d = window.parent.$("#divConveyance");
        window.parent.$("#divConveyance").dialog('close');


    }


</script>
<form id="OutOfOfficeAdd" method="post" action="" enctype="multipart/form-data">
<div style="width: 100%">
    <div style="width: 100%; border-bottom: 1px solid black; float: left; height: 30px">
        <%= Html.Hidden("hd",Model.OutOfOffice.IsNew) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.IsNew)  %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.ID)  %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.GETOUTDATE) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.EXPGETINDATE) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.STRGETOUTDATE) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.STRGETINDATE) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.GETINDATE)  %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.GETINTIME)  %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.ISGETIN)  %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.PERMITTEDBY) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.VERIFIEDBY) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.APPROVEDBY) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.STATUS) %>
        <%= Html.HiddenFor(m=> m.PathID) %>
        <%= Html.HiddenFor(m=> m.AuthorTypeID) %>
        <%= Html.HiddenFor(m=>m.EditPermission) %>
       

        <% if (Model.OutOfOffice.ISGETIN || !Model.EditPermission) %>
        <%{ %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.GETOUTTIME) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.EXPGETINTIME) %>
        <%} %>
        <div style="width: 70%; float: left; font-size: large">
            Office IN-OUT
        </div>
        <div style="width: 28%; float: right; text-align: right; font-size: large">
            <% if (Model.IsNew) %>
            <%{ %>
            GET OUT
            <%} %>
            <% else %>
            <%{ %>
            GET IN
            <%} %>
        </div>
    </div>
    <div style="width: 100%; float: left">
        <table class="contenttext" width="100%" border="0">
            <colgroup>
                <col width="150" />
                <col width="" />
            </colgroup>
            <tr>
                <td>
                    Employee Name
                    <label class="labelRequired">
                    </label>
                </td>
                <td>
                    <div id="dvStrID">
                        <%= Html.TextBoxFor(m => m.OutOfOffice.STREMPID)%>
                    </div>
                    <%= Html.TextBoxFor(m => m.OutOfOffice.EMPNAME, new { @class = "textRegCustomWidth", @Style = "width:250px", @readonly = "true" })%>
                </td>
            </tr>
            <tr>
                <td>
                    Designation
                </td>
                <td>
                    <%= Html.TextBoxFor(m => m.OutOfOffice.Strdesignaiton, new { @class = "textRegCustomWidth textLabelLike" ,@style="Width:400px"})%>
                </td>
            </tr>
            <tr>
                <td>
                    Department
                </td>
                <td>
                    <%= Html.TextBoxFor(m => m.OutOfOffice.Strdepartment, new { @class = "textRegCustomWidth textLabelLike" ,@style="Width:400px"})%>
                </td>
            </tr>
            <tr>
                <td>
                    Reason for going out
                    <label class="labelRequired">
                    </label>
                </td>
                <td>
                    <div style="display: none">
                        <%= Html.TextBoxFor(m => m.OutOfOffice.PURPOSE)%>
                    </div>
                    <div style="width: 60%; float: left">
                        <%= Html.RadioButton("rdp", "Official", false, new { onclick = "SetHiddenValue(this);", @class = "show" })%>Official
                        <%= Html.RadioButton("rdp", "Meeting", false, new { onclick = "SetHiddenValue(this);", @class = "show" })%>Meeting
                        <%= Html.RadioButton("rdp", "Lunch", false, new { onclick = "SetHiddenValue(this);", @class="hide"})%>Lunch
                        <%= Html.RadioButton("rdp", "Personal", false, new { onclick = "SetHiddenValue(this);", @class = "hide" })%>Personal
                        <%= Html.RadioButton("rdp", "Sick", false, new { onclick = "SetHiddenValue(this);", @class = "hide" })%>Sick
                        <%= Html.RadioButton("rdp", "Other", false, new { onclick = "SetHiddenValue(this);", @class = "show" })%>
                        Other
                    </div>
                    <div id="otherPurpose" style="width: 40%; float: right">
                        <%= Html.TextBoxFor(m => m.OutOfOffice.OTHERPURPOSE, new { @class = "textRegCustomWidth", @style = "Width:100%" })%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Responsible Person
                </td>
                <td>
                    <div style="display: none">
                        <%=Html.TextBoxFor(m => m.OutOfOffice.RESPONSIBLEPERSONID, new { @class = "textRegular", @style = "width:65px; min-width:65px;", @readonly = "readonly" })%>
                    </div>
                    <%=Html.TextBoxFor(m => m.OutOfOffice.RESPONSIBLEPERSON, new { @class = "textRegular", @readonly = "readonly" })%>
                    <a href="#" class="btnSearch" onclick="return openEmployee(0);"></a>
                </td>
            </tr>
            <tr>
                <td>
                    Contact Phone
                </td>
                <td>
                    <%= Html.TextBoxFor(m => m.OutOfOffice.CONTACTPHONE, new { @class = "textRegular", maxlength = 50 })%>
                </td>
            </tr>
            <tr>
                <td>
                    Visit Location
                </td>
                <td>
                    <div style="width:681px; height:100px; float: left;overflow:auto">
                        <table width="100%">
                            <thead>
                                <tr>
                                    <th align="center" valign="top">
                                        From
                                    </th>
                                    <th align="center"  valign="top">
                                        To
                                    </th>
                                    <th align="center" class="hideThis"  valign="top">
                                        Mode
                                    </th>
                                    <th align="center" class="hideThis"  valign="top">
                                        Amount
                                    </th>
                                    <th align="center" class="hideThis"  valign="top">
                                        Permitted Amount
                                    </th>
                                    <th align="center"  valign="top">
                                        Purpose
                                    </th>
                                    <% if (Model.LstOutOfOfficeLocation.Count(c=> c.bIsRoundTrip == true)>0 )
                                     { %>
                                       <th align="center" class="hideThis"  valign="top">
                                            Roundtrip
                                        </th>
                                    <%} %>

                                 
                                    <% if (Model.AuthorTypeList != null) foreach (string s in Model.AuthorTypeList)
                                           { %>
                                       <th  valign="top">
                                            <%= Html.Encode(s)%>
                                       </th>
                                    <%} %>
                                    <% 
                                        LMSEntity.OOALocationWiseComments comObj = new LMSEntity.OOALocationWiseComments();
                                        if ((Model.AuthorTypeList != null && Model.AuthorTypeList.Count<1) || Model.LstOOALocationComments.Count(l=> l.INTAUTHORTYPEID == Model.AuthorTypeID)<1)
                                        {
                                            
                                            comObj.INTAUTHORTYPEID = Model.AuthorTypeID;
                                            comObj.STRCOMMENTS = "";
                                            
                                            %>
                                        <th  valign="top">
                                            Comment
                                        </th>
                                    <%} %>
                                                                     
                                    <th>
                                    </th>
                                    <%-- <%} %>--%>
                                </tr>
                            </thead>
                            <tbody>
                                <% for (int i = 0; i < Model.LstOutOfOfficeLocation.Count; i++) %>
                                <% { %>
                                <tr>
                                    <td>
                                        <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].RECORDID) %>
                                        <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].FROMLOCATION, new { @class = "textRegularOutOfOffice" })%>
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].TOLOCATION, new { @class = "textRegularOutOfOffice" })%>
                                    </td>
                                    <td class="hideThis">
                                        <%= Html.DropDownList("LstOutOfOfficeLocation[" + i + "].MODE", new SelectList(Model.GetTransportMode, "Value", "Text", Model.LstOutOfOfficeLocation[i].MODE), "Select One", new { @class = "ddlRegularDate" })%>
                                    </td>
                                    <td class="hideThis">
                                        <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].AMOUNT, new { @class = "textRegularNumber double",@readonly="true", @style = "width:60px; min-width:60px;", maxlength = 10 })%>
                                    </td>
                                    <%
                                        if (Model.LstOutOfOfficeLocation[i].PERMITTEDAMOUNT == null || Model.LstOutOfOfficeLocation[i].PERMITTEDAMOUNT == 0)
                                            Model.LstOutOfOfficeLocation[i].PERMITTEDAMOUNT = Model.LstOutOfOfficeLocation[i].AMOUNT;
                                       
                                    %>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].PERMITTEDAMOUNT, new { @class = "textRegularNumber double", @style = "width:60px; min-width:60px;", maxlength = 10 })%>
                                    </td>
                                    <td>
                                        <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].PURPOSE, new { @class = "textRegCustomWidth", @style = "width:100%" })%>
                                    </td>

                                    <% if (Model.LstOutOfOfficeLocation[i].bIsRoundTrip)
                                     { %>
                                    <td class="hideThis empty">                                        
                                            Yes
                                            <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].bIsRoundTrip) %>
                                    </td>
                                    <%} %>

                                    <% if (Model.LstOOALocationComments != null)
                                       {
                                           Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList = Model.LstOOALocationComments.Where(l => l.LOCATIONID == Model.LstOutOfOfficeLocation[i].RECORDID).ToList();

                                       }
                                     %>

                                     <%if ((Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList.Count < 1) || Model.LstOOALocationComments.Count(l => l.INTAUTHORTYPEID == Model.AuthorTypeID) < 1)
                                        {
                                            Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList.Add(comObj);
                                           %>
                                     <%} %>
                                    
                                    <% for (int j = 0; j < Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList.Count; j++)
                                       { %>                                          
                                    <td>
                                       
                                        <% if (Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].INTAUTHORTYPEID < Model.AuthorTypeID) %>
                                        <%{ %>
                                            <%= Html.Encode(Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STRCOMMENTS)%>
                                            <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STRCOMMENTS)%>
                                            <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].INTAUTHORTYPEID)%>
                                            <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].LOCATIONID)%>
                                            <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].OUTOFOFFICEID)%>
                                            <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STRAUTHORID)%>
                                            <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STREUSER)%>
                                            <%= Html.HiddenFor(m=> m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].COMMENTID)%>

                                        <%} %>
                                        <% else if(Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].INTAUTHORTYPEID == Model.AuthorTypeID)%>
                                            <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STRCOMMENTS, new { @class="textRegular" ,@Style="width:120px"})%>
                                       
                                    </td>

                                    <%} %>

                                    <% if (Model.OutOfOffice.ISGETIN == false && i != 0) %>
                                    <%{ %>
                                    <td>
                                        <a href='#' class="gridDelete" onclick='javascript:return deleteNode(<%= i %>);'>
                                        </a>
                                    </td>
                                    <%} %>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                    <% if (Model.OutOfOffice.ISGETIN == false) %>
                    <%{ %>
                    <div style="width:12px; float: left;">
                        <a href="#" id="hrfAdd" class="gridAdd" onclick="return AddNode();"></a>
                    </div>
                     <%} %>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                        <colgroup>
                            <col width="33%" />
                            <col width="33%" />
                            <col width="33%" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th align="center">
                                    OUT OF OFFICE
                                </th>
                                <th align="center">
                                    EXPECTED GET IN
                                </th>
                                <th align="center">
                                    GET IN
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <% if (Model.EditPermission && Model.OutOfOffice.BITISNEWARRIVAL == 1 && Model.OutOfOffice.ISGETIN == false) %>
                                    <%{ %>
                                    <div style="width: 40%; float: left">                                       
                                        <%= Html.TextBoxFor(m => m.OutOfOffice.STRGETOUTDATE, new { @readonly = "true", @class = "textRegular dtPicker", @style = "width:80px", @formatString = "dd/MM/yyyy", onChange = "timeChanged()" })%>
                                    </div>
                                    <div style="width: 10%; float: left">
                                        at
                                    </div>
                                    <div style="width: 40%; float: left">
                                        <%= Html.TextBoxFor(m => m.OutOfOffice.GETOUTTIME, new { @readonly = "true", @class = "textRegular timePicker", @style = "width:65px" ,onChange="timeChanged()" })%>
                                    </div>
                                    <div style="width: 100%; float: left">
                                    </div>
                                    <%} %>
                                    <%else %>
                                    <%{ %>
                                    <%= Html.DisplayFor(m => m.OutOfOffice.STRGETOUTDATE)%>
                                    at
                                    <%= Html.DisplayFor(m => m.OutOfOffice.GETOUTTIME)%>
                                    <%} %>
                                </td>
                                <td>
                                    <% if (Model.EditPermission && Model.OutOfOffice.BITISNEWARRIVAL == 1 && Model.OutOfOffice.ISGETIN == false) %>
                                    <%{ %>
                                    <%=Html.Hidden("StrCurrentDate", DateTime.Today.ToString("dd/MM/yyyy"))%>
                                    <%=Html.TextBoxFor(m => m.OutOfOffice.STREXPGETINDATE, new { @class = "textRegular dtPicker date", @style = "width:80px", onChange = "timeChanged()" })%>
                                    at
                                    <%= Html.TextBoxFor(m => m.OutOfOffice.EXPGETINTIME, new { @class = "textRegular", @style = "width:65px", onChange = "timeChanged()" })%>
                                    <%} %>
                                    <%else %>
                                    <%{ %>
                                    <%= Html.DisplayFor(m => m.OutOfOffice.STREXPGETINDATE)%>
                                    at
                                    <%= Html.DisplayFor(m => m.OutOfOffice.EXPGETINTIME)%>
                                    <%} %>
                                </td>
                                <td>
                                    <%if (Model.OutOfOffice.ISGETIN == true) %>
                                    <%{ %>
                                    <%= Html.DisplayFor(m => m.OutOfOffice.GETINDATE)%>
                                    <%--at
                                    <%= Html.DisplayFor(m => m.OutOfOffice.GETINTIME)%>--%>
                                    <%} %>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <%if (Model.OutOfOffice.ISGETIN == true)
              { %>
            <tr>
                <td colspan="2" align="center">
                    <% 
                        string totalTime = "";
                        string st1 = Model.OutOfOffice.GETINDATE.ToShortDateString() + " " + Model.OutOfOffice.GETINTIME;
                        DateTime dtGetIn = DateTime.Parse(st1);


                        string st2 = Model.OutOfOffice.GETOUTDATE.ToShortDateString() + " " + Model.OutOfOffice.GETOUTTIME;
                        DateTime dtGetOut = DateTime.Parse(st2);


                        TimeSpan ts;
                        ts = dtGetIn - dtGetOut;

                        if (ts.Days > 0)
                            totalTime = ts.Days.ToString() + " Day(s)  ";

                        if (ts.Hours > 0)
                            totalTime += ts.Hours.ToString() + " Hour(s) ";

                        if (ts.Minutes > 0)
                            totalTime += ts.Minutes.ToString() + " Minutes ";
                                    
                    %>
                    Duration:
                    <%= Html.Label(totalTime)%>
                </td>
            </tr>
            <%} %>
            <tr>
                <td>
                    Comments
                </td>
                <td>
                    <%= Html.TextAreaFor(m => m.OutOfOffice.COMMENTS, new { @class = "textRegularLarge" })%>
                </td>
            </tr>
            <%if (Model.LstOOAApprovalComments != null) for (int i = 0; i < Model.LstOOAApprovalComments.Count; i++)
                  { %>
            <% if (Model.LstOOAApprovalComments[i].INTAPPROVERTYPEID < Model.AuthorTypeID && Model.LstOOAApprovalComments[i].STRCOMMENTS.Length>0)
               { %>
            <tr>
                <td>
                    <% if (Model.LstOOAApprovalComments[i].INTAPPROVERTYPEID == 1) %>
                    <%{ %>
                    Verifier Comments
                    <%} %>
                    <% if (Model.LstOOAApprovalComments[i].INTAPPROVERTYPEID == 2) %>
                    <%{ %>
                    Recommender Comments
                    <%} %>
                    <% if (Model.LstOOAApprovalComments[i].INTAPPROVERTYPEID == 3) %>
                    <%{ %>
                    Approver Comments
                    <%} %>
                </td>
                <td>
                    <%= Html.Encode(Model.LstOOAApprovalComments[i].STRCOMMENTS) %>
                </td>
            </tr>
            <%} %>
            <% else if (Model.LstOOAApprovalComments[i].INTAPPROVERTYPEID == Model.AuthorTypeID && Model.EditPermission == true)
                {
                    Model.ApproverComment = Model.LstOOAApprovalComments[i].STRCOMMENTS;
            %>
            <%} %>
            <%} %>
            <% if (Model.EditPermission == true)
               {%>
            <tr>
                <td>
                    <% if (Model.AuthorTypeID == 1) %>
                    <%{ %>
                    Comment of Verifier
                    <%} %>
                    <% if (Model.AuthorTypeID == 2) %>
                    <%{ %>
                    Comment of Recommender
                    <%} %>
                    <% if (Model.AuthorTypeID == 3) %>
                    <%{ %>
                    Comment of Approver
                    <%} %>
                </td>
                <td>
                    <%= Html.TextAreaFor(m => m.ApproverComment, new { @class = "textRegularLarge" })%>
                </td>
            </tr>
            <%} %>
            <tr>
                <td align="center" colspan="2">
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <% if (Model.OutOfOffice.BITISNEWARRIVAL == 1)
       {%>
    <%     
        int retVal = Model.GetAuthorTypeID(LMS.Web.LoginInfo.Current.strEmpID, Model.OutOfOffice.STREMPID);
        bool isReverify = Model.isReverify(LMS.Web.LoginInfo.Current.strEmpID, Model.OutOfOffice.STREMPID, 1);
    
    %>
    <% if (retVal == 1) %>
    <%{ %>
    <% if (Model.OutOfOffice.ISPERMITTED != true && Model.OutOfOffice.ISGETIN == true) %>
    <%{ %>
    <a href="#" class="btnPermittedAndVerified btnHide" onclick="return PermittedAndVerified();">
    </a>
    <%} %>
    <% else
        { %>
    <% if (Model.OutOfOffice.ISPERMITTED != true) %>
    <%{ %>
    <a href="#" class="btnPermission btnHide" onclick="return Permission();"></a>
    <%}%>
    <% if (Model.OutOfOffice.ISPERMITTED == true && Model.OutOfOffice.ISVARIFIED != true) if (Model.OutOfOffice.ISGETIN == true) %>
    <%{ %>
    <a href="#" class="btnVerify btnHide" onclick="return Verify();"></a>
    <%}%>

    
  <%--  <% else if (Model.OutOfOffice.ISPERMITTED == true && Model.OutOfOffice.ISVARIFIED != true) if (Model.OutOfOffice.ISGETIN == false)
            {%>
    <font color="red"><b>You Permitted to get out and user needs to get in to verify.</b></font>
    <%} %>
--%>
   

    <%} %>

     <% if (Model.OutOfOffice.BITISREVIEW == 1) %>
    <%{ %>
        <a href="#" class="btnVerify btnHide" onclick="return Verify();"></a>
    <%} %>

    <%} %>
    <% else if (retVal == 2) %>
    <%{ %>
    <% if (Model.OutOfOffice.ISGETIN == false)
       {%>
    <font color="red"><b>For further process user needs to get in to verify.</b></font>
    <%} %>
    <% else
        { %>
         
         <% if (isReverify)
            { %>
                <a href="#" class="btnReverify btnHide" onclick="return Reverify();"></a>
            <%} %>
        <a href="#" class="btnRecommended btnHide" onclick="return Recommend();"></a>
    <%} %>
    <%} %>
    <% else if (retVal == 3) %>
    <%{ %>
    <% if (Model.OutOfOffice.ISGETIN == false)
       {%>
    <font color="red"><b>For further process user needs to get in to verify.</b></font>
    <%} %>
    <% else
        { %>

         <% if (isReverify)
            { %>
                <a href="#" class="btnReverify btnHide" onclick="return Reverify();"></a>
          <%} %>
        <a href="#" class="btnApproved btnHide" onclick="return Approve();"></a>
    <%} %>
    <%} %>
    <% else if (retVal == 1) %>
    <%{ %>
    <%} %>
    <%} %>
        
    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>

</div>
<div class="divSpacer">
</div>
<div id="divMsgStd" class="divMsg" style="text-align: center">
    <label id="msglbl" class="MSG">
    </label>
     <% if (Model.OutOfOffice.ISPERMITTED == true && Model.OutOfOffice.ISVARIFIED != true) if (Model.OutOfOffice.ISGETIN == false)
            {%>
    <font color="red"><b>You Permitted to get out and user needs to get in to verify.</b></font>
    <%} %>

    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
