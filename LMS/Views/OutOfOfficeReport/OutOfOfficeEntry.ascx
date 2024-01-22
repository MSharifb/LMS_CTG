<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OutOfOfficeModels>" %>


<script type="text/javascript">

  
    var isAdmin= 0;
    $(document).ready(function () {

       


        preventSubmitOnEnter($("#OutOfOfficeAdd"));
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yyyy', changeYear: false });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        $("#advanceDiv").dialog({ autoOpen: false, modal: true, height: 280, width: 335, resizable: false, title: 'Advance Get Out', beforeclose: function (event, ui) { Closing(); } });
        $("#otherPurpose").hide();
        $("#dvStrID").hide();
       
        checkRadio();
        DisableForm();

    });

    function Closing() {

    }


    function getAdvanceDate()
    {       
   
         $('#text1').datetimepicker();    
        return false;
    }

    function DisableForm()
    {
         $(function() {
              $('input, select, textarea').attr('readonly', 'true');
              $('input:radio').attr('disabled', 'disabled');
            });
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

    function ShowHideModeAndAmount(id)
    {
       
        if($(id).attr('class') =='hide')
        {
            $(".hideThis").hide();
            $(".req").each(function(){
                $(this).removeClass('required');
            });
        }
        else
        {
            $(".hideThis").show();
            $(".req").each(function(){
                $(this).addClass('required');
            });
        }
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

updateFields = function(data) {    
     $('#OutOfOffice_Strdesignaiton').val(data.strDesignation);
     $('#OutOfOffice_Strdepartment').val(data.strDepartment);   
};

function GetInfo(id)
{
     var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();
        
    $.getJSON("getEmployeeInformation", serializedForm, updateFields);
    
}

function setData(id, name) {
   
    if(isAdmin == 0)
    {
     document.getElementById('OutOfOffice_RESPONSIBLEPERSONID').value = id;
     document.getElementById('OutOfOffice_RESPONSIBLEPERSON').value = name;
     }
     else
     {
       
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

     if ($("#OutOfOffice_PURPOSE").val() == "") {
        alert('Purpose can not be blank.');
        return false;
    }

    if(fnValidate() == true)
    {
    return GetOut();
    }

}



function GetOut()
{
     var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();
        
    $.getJSON("/LMS/OutOfOfficeDetails/OutOfOfficeGetOut", serializedForm, getinSuccessData);
}


function GetIn()
{
     var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();
        
    $.getJSON("/LMS/OutOfOfficeDetails/OutOfOfficeGetIn", serializedForm, getinSuccessData);
}

getinSuccessData = function(data) {    
    $("#msglbl").text(data.strMessage);
    $(".ui-dialog-titlebar-close").click();
   
};

function AddNode() {
    
    if (1 == 1) {
        
        var targetDiv = "#divApprovalPathDetails";
        var url = "/LMS/OutOfOfficeDetails/AddNode";
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
        var url = "/LMS/OutOfOfficeDetails/DeleteNode/" + Id;
        var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();

        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
    }
    return false;
}
</script>

<form id="OutOfOfficeAdd" method="post" action="" enctype="multipart/form-data">

<div style="width:100%;float:left"  id="divLeaveApplication">


<div style="width:100%;border-bottom:1px solid black;float:left;height:30px">
  
  <%= Html.Hidden("hd",Model.OutOfOffice.IsNew) %>
  <%= Html.HiddenFor(m=> m.OutOfOffice.IsNew)  %>
  <%= Html.HiddenFor(m=> m.OutOfOffice.ID)  %>
  


    <div style="width:70%;float:left;font-size:large">
        Office IN-OUT 
    </div>
    <div style="width:28%;float:right;text-align:right;font-size:large">
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


<div style="width:100%;float:left">
    <table  class="contenttext contenttable" width="100%" border="0">
        <colgroup>
            <col width="150" />
            <col width="" />
        </colgroup>
        <tr>
            <td>                
               Employee Name  <label class="labelRequired"></label>
            </td>
            <td> 
                
                <div id="dvStrID">
                    <%= Html.TextBoxFor(m => m.OutOfOffice.STREMPID)%>  
                </div>  
                <%= Html.TextBoxFor(m => m.OutOfOffice.EMPNAME, new { @class = "textRegular", @readonly = "true" })%>            
                 <% if (Model.IsNew) %>
                 <%{ %>
                    
                    <%                        
                        if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA))
                        { %>
                            <% if (Model.OutOfOffice.ISGETIN != true)
                               { %>
                        <a href="#" class="btnSearch" onclick="return openEmployee(1);"></a>

                        <%      }
                        }
                        
                         %>
                <%}%>
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
                Reason for going out <label class="labelRequired"></label>
            </td>
            <td>    
                <div style="display:none">
                    <%= Html.TextBoxFor(m => m.OutOfOffice.PURPOSE)%> 
                </div>           
               
               <div style="width:60%;float:left">
                    <%= Html.RadioButton("rdp", "Official", false, new { onclick = "SetHiddenValue(this);", @class = "show" })%>Official
                    <%= Html.RadioButton("rdp", "Meeting", false, new { onclick = "SetHiddenValue(this);", @class = "show" })%>Meeting
                    <%= Html.RadioButton("rdp", "Lunch", false, new { onclick = "SetHiddenValue(this);", @class="hide"})%>Lunch
                    <%= Html.RadioButton("rdp", "Personal", false, new { onclick = "SetHiddenValue(this);", @class = "hide" })%>Personal
                    <%= Html.RadioButton("rdp", "Sick", false, new { onclick = "SetHiddenValue(this);", @class = "hide" })%>Sick
                    <%= Html.RadioButton("rdp", "Other", false, new { onclick = "SetHiddenValue(this);", @class = "show" })%>  Other              
                </div>
                <div id="otherPurpose"  style="width:40%;float:right">
                    <%= Html.TextBoxFor(m => m.OutOfOffice.OTHERPURPOSE, new { @class = "textRegCustomWidth", @style = "Width:100%" })%>
                </div>
            </td>
        </tr>
       
        <tr>
            <td>
                Responsible Person(in absence)
            </td>
            <td>
                 <div style="display:none">
                    <%=Html.TextBoxFor(m => m.OutOfOffice.RESPONSIBLEPERSONID, new { @class = "textRegular", @style = "width:65px; min-width:65px;", @readonly = "readonly" })%>
                 </div>
                 
                 <%=Html.TextBoxFor(m => m.OutOfOffice.RESPONSIBLEPERSON, new { @class = "textRegular", @readonly = "readonly" })%>
                 <% if (Model.IsNew) %>
                 <%{ %>
                    <a href="#" class="btnSearch" onclick="return openEmployee(0);"></a>
                 <%} %>
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
                <div style="width:98%; float:left;">                               
                    <table width="100%">
                    <thead>
                        <tr>
                            <th align="center"  valign="top">
                                From
                            </th>
                            <th align="center"  valign="top">
                                To
                            </th>
                            <th align="center" class="hideThis"  valign="top">
                                Mode
                            </th>

                          <%  if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA) || LMS.Web.LoginInfo.Current.strEmpID == Model.OutOfOffice.STREMPID)
                              { %>
                            <th align="center" class="hideThis"  valign="top">
                                Amount
                            </th>

                            <%} %>
                            <th align="center"  valign="top">
                                Purpose
                            </th>
                             <% if (Model.OutOfOffice.ISGETIN == false) %>
                            <%{ %>
                                <th></th>
                            <%} %>
                        </tr>
                    </thead>

                    <tbody>
                        <%if (Model.LstOutOfOfficeLocation.Count>0) for (int i = 0; i < Model.LstOutOfOfficeLocation.Count; i++) %>
                           <%{ %>
                        <tr>
                            <td>
                                <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].FROMLOCATION, new { @class = "textRegularOutOfOffice required req" })%>
                            </td>
                            <td>
                                <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].TOLOCATION, new { @class = "textRegularOutOfOffice required req" })%>
                            </td>
                            <td class="hideThis">
                                <%= Html.DropDownList("LstOutOfOfficeLocation[" + i + "].MODE", new SelectList(Model.GetTransportMode, "Value", "Text", Model.LstOutOfOfficeLocation[i].MODE), "Select One", new { @class = "ddlRegularDate required req" })%> 
                            </td>
                            
                            <%  if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA) || LMS.Web.LoginInfo.Current.strEmpID == Model.OutOfOffice.STREMPID)
                                { %>
                                <td class="hideThis">
                                    <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].AMOUNT, new { @class = "textRegularNumber double required req", @style = "width:60px; min-width:60px;", maxlength = 10 })%>
                                </td>
                            <%} %>

                            <td>
                                <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].PURPOSE, new { @class = "textRegCustomWidth", @style = "width:100%" })%>
                            </td>
                            <% if (Model.OutOfOffice.ISGETIN == false && Model.OutOfOffice.STREMPID == LMS.Web.LoginInfo.Current.strEmpID && i !=0) %>
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
                        <% if ((Model.OutOfOffice.STREMPID == LMS.Web.LoginInfo.Current.strEmpID) || (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA))) %>
                        <%{ %>
                            <div  style="width:2%; float:left;">
                                <a href="#" class="gridAdd" onclick="return AddNode();" ></a>
                            </div>
                        <%}%>
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
                            <td >    
                                <% if (Model.IsNew) %>
                                <%{ %>                            
                                <div style="width:40%; float:left">
                                    <%--<%= Html.TextBox("text1", "", new { @class = "cls"})%>--%>
                                    <%= Html.TextBoxFor(m => m.OutOfOffice.STRGETOUTDATE, new { @readonly = "true", @class = "textLabelLike", @style = "width:70px", @formatString = "dd/MM/yyyy" })%>
                                </div>
                                <div style="width:10%; float:left">
                                    at
                                </div>
                                <div style="width:40%; float:left">                                 
                                 <%= Html.TextBoxFor(m => m.OutOfOffice.GETOUTTIME, new { @readonly = "true", @class = "textLabelLike", @style = "width:60px" })%>
                                </div>
                                <div style="width:100%;float:left">    
                                
                                <%--<a href="#"  onclick="return getAdvanceDate();">Click Here for Advance out</a>--%>
                                    <a href="#" onclick="return popupOutAdvanceAdd();">Click Here for Advance out</a>
                                </div>
                                
                                <%} %>
                                <%else %> 
                                <%{ %> 
                                  <%= Html.DisplayFor(m => m.OutOfOffice.STRGETOUTDATE)%>  at 
                                  <%= Html.DisplayFor(m => m.OutOfOffice.GETOUTTIME)%>
                                <%} %>
                            </td>
                            <td>
                                  <% if (Model.IsNew) %>
                                    <%{ %>  
                                    <%=Html.Hidden("StrCurrentDate", DateTime.Today.ToString("dd/MM/yyyy"))%>
                                    
                                   
                                    <%=Html.TextBoxFor(m => m.OutOfOffice.STREXPGETINDATE, new { @class = "textRegularDate dtPicker date" })%>
                                     at                                     
                                    <%= Html.TextBoxFor(m => m.OutOfOffice.EXPGETINTIME,  new { @class = "ddlRegularDate" })%>
                                     <%} %>

                                <%else %> 
                                <%{ %> 
                                  <%= Html.DisplayFor(m => m.OutOfOffice.STREXPGETINDATE)%>  at 
                                  <%= Html.DisplayFor(m => m.OutOfOffice.EXPGETINTIME)%>
                                <%} %>
                                    
                            </td>
                            <td>

                                 <%if (Model.OutOfOffice.ISGETIN == true) %>
            
                                <%{ %>
                                    <%= Html.DisplayFor(m => m.OutOfOffice.GETINDATE)%> <%-- at 
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
                        string st1 = Model.OutOfOffice.GETINDATE.ToShortDateString() +" "+ Model.OutOfOffice.GETINTIME;
                        DateTime dtGetIn = DateTime.Parse(st1);

                       
                        string st2 = Model.OutOfOffice.GETOUTDATE.ToShortDateString() + " " + Model.OutOfOffice.GETOUTTIME;
                        DateTime dtGetOut = DateTime.Parse(st2);
                         
                
                        TimeSpan ts;
                        ts = dtGetIn - dtGetOut;

                        if(ts.Days>0)
                            totalTime = ts.Days.ToString() + " Day(s)  ";

                        if (ts.Hours > 0)
                            totalTime += ts.Hours.ToString() + " Hour(s) ";

                        if (ts.Minutes > 0)
                            totalTime += ts.Minutes.ToString() + " Minutes ";
                                    
                 %>  
                Duration:  <%= Html.Label(totalTime)%>
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
        <tr>
            
            <td align="center" colspan="2">

                
            </td>
        </tr>
    </table>

    
</div>

<%--<div class="divButton">
    <a href="#" class="btnClose" onclick="return closeDialog();"></a>
</div>--%>

<div id="advanceDiv">
     <iframe id="styleAdvance" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</div>

</form>
