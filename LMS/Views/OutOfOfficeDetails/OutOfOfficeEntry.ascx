<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OutOfOfficeModels>" %>
<script src="<%= Url.Content("~/Scripts/jquery-autocomplete/jquery.autocomplete.js") %>" type="text/javascript"></script>

<style type="text/css">
    .style1
    {
        width: 43%;
    }
    .style2
    {
        width: 194px;
    }
</style>
<script type="text/javascript">

  
    var isAdmin= 0;
    var msg;
    $(document).ready(function () {

               $("#advanceID").datepicker({
            changeMonth: true,
            changeYear: false
            }).click(function () { $('#AdvanceDate').datepicker('show'); });



         <% if(Model.OutOfOffice.ID < 1){ %>
            $("#OutOfOffice_PURPOSE").val("Official");
        <%} %>

        $("#adHide").hide();
        $("#AdvanceDate").datetimepicker({ dateFormat: 'dd-mm-yy',ampm: true, timeFormat:'hh:mm TT',separator: ' @ ',onClose: function(timeText, inst) {
            var d = $("#AdvanceDate").val();
            var dArr = new Array();

            if(d.search('@')>0)
            {
                    dArr = d.split('@');
                      
                    if(dArr.length>1)
                        {
                            if(checkDateTime( dArr[0]) == true)
                            {
                               
                                $("#OutOfOffice_STRGETOUTDATE").val($.trim(dArr[0]));                               
                                $("#OutOfOffice_GETOUTTIME").val($.trim(dArr[1]));
                                $("#hiddenDate").val(dArr[0]);
                            }
                            else
                            {
                                 $("#OutOfOffice_STRGETOUTDATE").val($("#hiddenDate").val()); 
                                return false;
                            }
                        }  
              }              
                    
         }  });
        preventSubmitOnEnter($("#OutOfOfficeAdd"));
        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            ,showOn: 'button'
            ,buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            ,buttonImageOnly: true  });

        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        $("#advanceDiv").dialog({ autoOpen: false, modal: true, height: 280, width: 335, resizable: false, title: 'Advance Get Out', beforeclose: function (event, ui) { Closing(); } });
        $("#otherPurpose").hide();
        $("#dvStrID").hide();
        $("#OutOfOffice_EXPGETINTIME").timepicker({ ampm: true, timeFormat:'hh:mm TT'});
        checkRadio();

        $('.LOCATION').autocomplete('<%=Url.Action("GetSearchLocation", "OutOfOfficeDetails") %>', {
              
              dataType: 'json',
              parse: function(data) {
              

              
                  var rows = new Array();
                
                  if(data.length<1) 
                  {
                    return false;
              
                  }



                  try
                  {

                  for(var i=0; i<data.length; i++){
                       rows[i] = { data: data[i], value: data[i].LOCATION, result: data[i].LOCATION };
                    
                  }

                  
                  } catch(e)
                  
                  {
                   
                  }


                  return rows;  
              },
              formatItem: function(row, i, n) {
                  return row.LOCATION;
              },
              width: 300,
              mustMatch: true,
              selectFirst: true
          });

          
          
            $(".LOCATION").result(function (event, data, formatted) {            
            if (data) {

             
            }

              return false;
        });
        
        

//        $("#text1").hide();
      
        <% if(Model.OutOfOffice.STATUS == "GI")  %>
        <%{ %>
          DisableForm();

        <%} %>
        <% else if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA)) %>
        <%{ %>
            
        <%} %>

         <% else if (Model.OutOfOffice.STREMPID != LMS.Web.LoginInfo.Current.strEmpID ) %>
        <%{ %>
           DisableForm();
        <%} %>

       <% if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA))
                        { %>

         $('#OutOfOffice_EMPNAME').autocomplete('<%=Url.Action("LookUps", "Employee") %>', {
              dataType: 'json',
              parse: function(data) {
                  var rows = new Array();
                  for(var i=0; i<data.length; i++){

                       var name=  data[i].strEmpName;

                        rows[i] = { data: data[i], value: data[i].strEmpID, result:name };
                    
                  }
                  return rows;
              },
              formatItem: function(row, i, n) {
                  return row.strEmpID + ' - ' + row.strEmpName;
              },
              width: 300,
              mustMatch: true,
              selectFirst: true
          });

        $("#OutOfOffice_EMPNAME").result(function (event, data, formatted) {            
            if (data) {

                
                $("#OutOfOffice_STREMPID").val(data.strEmpID);
                GetInfo();
                GetEmpApprovalPath();
                return false;
            }

        });


        <% } %>

        <% else { %>
            $("#OutOfOffice_EMPNAME").attr('readonly','true');
        <%} %>

        $('#OutOfOffice_RESPONSIBLEPERSON').autocomplete('<%=Url.Action("LookUps", "Employee") %>', {
              dataType: 'json',
              parse: function(data) {
                  var rows = new Array();
                  for(var i=0; i<data.length; i++){
                      rows[i] = { data: data[i], value: data[i].strEmpID, result: data[i].strEmpName };
                    
                  }
                  return rows;
              },
              formatItem: function(row, i, n) {
                  return row.strEmpID + ' - ' + row.strEmpName;
              },
              width: 300,
              mustMatch: true,
              selectFirst: true
          });

        $("#OutOfOffice_RESPONSIBLEPERSON").result(function (event, data, formatted) {            
            if (data) {

                if($("#OutOfOffice_STREMPID").val() == data.strEmpID)
                {
                    alert('Applicant can not be responsible person.');
                    $("#OutOfOffice_RESPONSIBLEPERSON").val('');
                    return false;
                }
                $("#OutOfOffice_RESPONSIBLEPERSONID").val(data.strEmpID);
               
                return false;
            }

        });

        var roundtripVal = $("#RoundTrip").val();
        if(roundtripVal == "True")
        {
           
            $("#hrfAdd").hide();
        }


    });

    function Closing() {

    }

     function checkDateTime(dat) {
       
      
        var time;
        var date = dat;
        var dateArr = new Array();
        dateArr = date.split('-');
        var curDateobj = new Date();
        var curDate =new Date(curDateobj.getFullYear(),curDateobj.getMonth(),curDateobj.getDate());
        
        var newDate = new Date(dateArr[1] + '/' + dateArr[0] + '/' + dateArr[2]);
       
        if (newDate < curDate) {
            alert('Advance Date should not be smaller than current date.');
            return false;
        }

       return true;
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
              $('input:checkbox').attr('disabled', 'disabled');
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

updateHiddenFields = function(data) {      
   if(data.strMessage =='0')
   {
   // alert('Approval path is not set yet.');
   }
   $("#EmpApprovalPath").val(data.strMessage);
};

function GetInfo()
{
     var form = $("#OutOfOfficeAdd");
     var serializedForm = form.serialize();
        
    $.getJSON("getEmployeeInformation", serializedForm, updateFields);
    
}


function setData(id, name) {
   
    if(isAdmin == 0)
    {
         if($("#OutOfOffice_STREMPID").val() == id)
                {
                    alert('Applicant can not be responsible person.');                     
                    return false;
                }

     document.getElementById('OutOfOffice_RESPONSIBLEPERSONID').value = id;
     document.getElementById('OutOfOffice_RESPONSIBLEPERSON').value = name;
     }
     else
     {
       
        document.getElementById('OutOfOffice_STREMPID').value = id;
        document.getElementById('OutOfOffice_EMPNAME').value = name;
        GetInfo();
        GetEmpApprovalPath();
     }
    $("#divEmpList").dialog('close');
}


function GetEmpApprovalPath()
{
    
    var empID=$('#OutOfOffice_STREMPID').val();
    var form = $("#OutOfOfficeAdd");   
    $.getJSON("GetEmployeeWiseApprovalPath", {strEmpID:empID}, updateHiddenFields);

}

function setDateTime(date, time) {
    
    document.getElementById('OutOfOffice_STRGETOUTDATE').value = date;
    document.getElementById('OutOfOffice_GETOUTTIME').value = time;
   
    $("#advanceDiv").dialog('close');
    
}

function customDateParse (input) {
  var arr = new Array();
   arr = input.split(' ');

   var arr1 = new Array();
   arr1 = arr[0].split('/')

   var arr2 = arr[1].split(':');
   
   
   if(arr[2].toLowerCase() == 'pm')
    {
      if(parseInt(arr2[0]) == 12)
      {
      
      }
      else
      { 
        arr2[0] = parseInt(arr2[0])+12;
      }
    }


   var dt1= new Date(arr1[2],(parseInt(arr1[1])-1),arr1[0],arr2[0],arr2[1]);

   return dt1;
}


function CheckExpDate()
{   
    
   var strDate1 =$("#OutOfOffice_STRGETOUTDATE").val()+" "+$("#OutOfOffice_GETOUTTIME").val();
   var strDate2=$("#OutOfOffice_STREXPGETINDATE").val()+" "+$("#OutOfOffice_EXPGETINTIME").val();
  
   strDate1 = strDate1.replace('-','/');
   strDate2 = strDate2.replace('-','/');

   strDate1 = strDate1.replace('-','/');
   strDate2 = strDate2.replace('-','/');


   var dt1= customDateParse(strDate1);
   var dt2= customDateParse(strDate2);
   
 
   var diff = dt2-dt1;
  
   if(diff>0)
   {
    return true;
   }
   else
   {
    return false;
   }


}

function CheckGetInDate()
{   
    
   var strDate1 =$("#OutOfOffice_STRGETOUTDATE").val()+" "+$("#OutOfOffice_GETOUTTIME").val();
   var strDate2=new Date();

   
   strDate1 = strDate1.replace('-','/');
   strDate1 = strDate1.replace('-','/');
  
  

      
   var dt1= customDateParse(strDate1);
  
  
   var diff = strDate2-dt1;
  
   if(diff>0)
   {
    return true;
   }
   else
   {
    return false;
   }


}
showResult = function(data) {       
    msg = data.strMessage;    
    return msg;
    };


function save() {

    var hdVal = $("#EmpApprovalPath").val();
    var result = true;
    if(hdVal =='')
    {
        GetEmpApprovalPath();
        hdVal = $("#EmpApprovalPath").val();
    }
    var purpose = $("#OutOfOffice_PURPOSE").val();
    
    if(purpose == 'Official' || purpose == 'Meeting' || purpose == 'Other')
    {
        if(hdVal == '0')
        {
           result = confirm('This user does not have any approval path. Do you want to continue?');    
        }

        if(result == false)
        {
            //alert('Please set approval path first then try again');
            return false;    
        }
    }

    if(CheckExpDate() == false)
    {
        alert('Expected Get In time should be greater than get out time');
        return false;
    }
   
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
    return false;
}


function Draft()
{

 var hdVal = $("#EmpApprovalPath").val();
    var result = true;
    if(hdVal =='')
    {
        GetEmpApprovalPath();
        hdVal = $("#EmpApprovalPath").val();
    }
    var purpose = $("#OutOfOffice_PURPOSE").val();
    
    if(purpose == 'Official' || purpose == 'Meeting' || purpose == 'Other')
    {
        if(hdVal == '0')
        {
           result = confirm('This user does not have any approval path. Do you want to continue?');    
        }

        if(result == false)
        {
            //alert('Please set approval path first then try again');
            return false;    
        }
    }

    if(CheckExpDate() == false)
    {
        alert('Expected Get In time should be greater than get out time');
        return false;
    }
   
     if ($("#OutOfOffice_PURPOSE").val() == "") {
        alert('Purpose can not be blank.');
        return false;
    }

 
     var form = $("#OutOfOfficeAdd");
     var serializedForm = form.serialize();
        
    $.getJSON("/LMS/OutOfOfficeDetails/OutOfOfficeDraft", serializedForm, getinSuccessData);
    return false;
}

function GetIn()
{

    if(CheckGetInDate() == false)
    {
        alert('You cant Get In because get out is greater than get in time');
        return false;
    }
     var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();
        
     $.getJSON("/LMS/OutOfOfficeDetails/OutOfOfficeGetIn", serializedForm, getinSuccessData);

     return false;
}

getinSuccessData = function(data) {    

    window.parent.closeDialogBox1();  

    $("#msglbl").text(data.strMessage);
    if(data.strMessage.search('Success')>-1)
    {
        $(".btnGetOut").hide();
        $(".btnGetIn").hide();
        $(".btnDraft").hide();
            
    }

    if(data.strGetInTime != undefined)
    {
    if(data.strGetInTime.length>0)
    {
       
        $("#lblGetInTime").text(data.strGetInTime);
    }
    }
    
   
};

function AddNode() {
    
    if (1 == 1) {
        
        var targetDiv = "#divApprovalPathDetails";
        var url = "/LMS/OutOfOfficeDetails/AddNode";
        var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();
       
        $.post(url, serializedForm, function (result) {
            $(targetDiv).html(result);   
                
          deleteRow('dtLocation');

        }, "html");

        
    }
    return false;
}


function getpartial(Id) {
        var url =  "/LMS/OutOfOfficeDetails/DeleteNode/" + Id;
        var targetDiv = "#divApprovalPathDetails";
        var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();
        $.ajax({
            url: url,
            data:serializedForm,
            success: function (data) {
                
                $(targetDiv).html(data);
            }
        });

     return false; // stops the form submitting (if in a form)
    }


function deleteNode(Id) {
    
    
    var result = confirm('Pressing OK will remove this record. Do you want to continue?');
    if (result == true) {
        var targetDiv = "#divApprovalPathDetails";
        var url = "/LMS/OutOfOfficeDetails/DeleteNode/" + Id;
        var form = $("#OutOfOfficeAdd");
        var serializedForm = form.serialize();
       
        $.get(url, serializedForm, function (result) { $(targetDiv).html(result);deleteRow('dtLocation'); });
    }    
    return false;
}

    function MakeAmountDouble(val)
    {   
        var rowid = val.id.split('_')[1]; 
        var vid="LstOutOfOfficeLocation_" + rowid + "__AMOUNT";
        var am=document.getElementById(vid);
        var hrfAdd=document.getElementById('hrfAdd');
     
        if(val.checked)
        {
            am.value = am.value*2;
         
           $("#hrfAdd").hide();
        }
        else
        {
            am.value = am.value/2;
          
             $("#hrfAdd").show();            
        }
             
        deleteRow('dtLocation');
    }

    function deleteRow(tableID) {
            try {
                var table = document.getElementById(tableID);
                var rowCount = table.rows.length;
                var tmrowcount = rowCount;
                var checkCount=0;
                
                for (var i = 1; i < rowCount; i++) 
                {
                    var row = table.rows[i];
                    var chkbox = row.getElementsByTagName('td')[5];
                    chkbox=chkbox.children[0];                      
                    if (null != chkbox && true == chkbox.checked) 
                    {                                                 
                       checkCount++;                       
                    }                                
                }
              
                if(rowCount>1)
                { checkCount++; }
                else{
                checkCount=0;
                }
                
                   
                for (var i = 1; i < rowCount; i++) 
                {
                    var row = table.rows[i];
                    var chkbox = row.getElementsByTagName('td')[5];
                    chkbox=chkbox.children[0];
                                         
                    if(checkCount>0)
                    {
                        if (null != chkbox && true == chkbox.checked) 
                        {
                            chkbox.disabled=false;
                        }
                        else
                        {
                         if(rowCount>2)
                { 
                            chkbox.disabled=true;
                            }
                        } 
                    }
                    else
                        {
                            chkbox.disabled=false;
                            
                        }   
                                      
                }                
            } catch (e) {
                alert(e);
            }
        }

</script>
<form id="OutOfOfficeAdd" method="post" action="" enctype="multipart/form-data">
<div style="width: 100%; float: left" id="divLeaveApplication">
    <div style="width: 100%; border-bottom: 1px solid black; float: left; height: 30px">
        <%= Html.Hidden("hd",Model.OutOfOffice.IsNew) %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.IsNew)  %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.ID)  %>
        <%= Html.HiddenFor(m=> m.OutOfOffice.STATUS) %>
        <%=Html.Hidden("EmpApprovalPath") %>
        <%= Html.Hidden("hiddenDate",Model.OutOfOffice.STRGETOUTDATE) %>
        

       

        <% if (Model.OutOfOffice.IsNew == false) %>
        <%{ %>
            <%= Html.HiddenFor(m=> m.OutOfOffice.GETOUTDATE) %>
            <%= Html.HiddenFor(m=> m.OutOfOffice.EXPGETINDATE) %>

        <%}%>

        
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
        <table class="contenttext contenttable" width="100%" border="0">
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
                    <%= Html.TextBoxFor(m => m.OutOfOffice.EMPNAME, new { @class = "textRegCustomWidth", @Style="width:250px" })%>
                    <% if (Model.IsNew) %>
                    <%{ %>
                    <%                        
                          if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA))
                          { %>
                    <% if (Model.OutOfOffice.STATUS != "GI")
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
                    Reason for going out
                    <label class="labelRequired">
                    </label>
                </td>
                <td>
                    <div style="display: none">
                        <%= Html.TextBoxFor(m => m.OutOfOffice.PURPOSE)%>
                    </div>
                    <div style="width: 60%; float: left">
                        <%= Html.RadioButton("rdp", "Official", true, new { onclick = "SetHiddenValue(this);", @class = "show" })%>Official
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
                    Responsible Person(in absence)
                </td>
                <td>
                    <div style="display: none">
                        <%=Html.TextBoxFor(m => m.OutOfOffice.RESPONSIBLEPERSONID, new { @class = "textRegular", @style = "width:65px; min-width:65px;", @readonly = "readonly" })%>
                    </div>
                    <%=Html.TextBoxFor(m => m.OutOfOffice.RESPONSIBLEPERSON, new { @class = "textRegCustomWidth", @Style = "width:250px" })%>
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
                    <%= Html.TextBoxFor(m => m.OutOfOffice.CONTACTPHONE, new { @class = "textRegCustomWidth", @Style = "width:250px", maxlength = 15 })%>
                </td>
            </tr>
            <tr>
                <td>
                    Visit Location
                </td>
                <td>
                    <div style="width:681px; float: left; max-height:150px;overflow:auto;">
                        <table width="100%" id="dtLocation" style="height:100%">
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
                                    <% if (Model.LstOutOfOfficeLocation.Sum(m => m.PERMITTEDAMOUNT) > 0)
                                       {%>
                                    <th  valign="top">
                                        Permitted Amount
                                    </th>

                                    <%} %>
                                    <%} %>
                                    <th align="center"  valign="top">
                                        Purpose
                                    </th>
                                    <th align="center"  class="hideThis"  valign="top">
                                        Roundtrip?
                                    </th>

                                    <% if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA) || LMS.Web.LoginInfo.Current.strEmpID == Model.OutOfOffice.STREMPID)
                                        if (Model.AuthorTypeList != null) 
                                            foreach (string s in Model.AuthorTypeList)
                                           { %>
                                               <th  valign="top">
                                                    <%= Html.Encode(s)%>
                                               </th>
                                           <%} %>
                                    
                                    <% if (Model.OutOfOffice.ISGETIN == false) %>
                                    <%{ %>
                                    <th>
                                        &nbsp;
                                    </th>
                                    <%} %>
                                </tr>
                            </thead>
                            
                            <tbody id="tblLocations">
                                <% if (Model.LstOutOfOfficeLocation != null) for (int i = 0; i < Model.LstOutOfOfficeLocation.Count; i++) %>
                                <%{ %>
                                <tr>
                                    <td class="empty">
                                        <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].FROMLOCATION, new { @class = "textRegularOutOfOffice required req LOCATION" })%>
                                    </td>
                                    <td class="empty">
                                        <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].TOLOCATION, new { @class = "textRegularOutOfOffice required req LOCATION" })%>
                                    </td>
                                    <td class="hideThis empty">
                                        <%= Html.DropDownList("LstOutOfOfficeLocation[" + i + "].MODE", new SelectList(Model.GetTransportMode, "Value", "Text", Model.LstOutOfOfficeLocation[i].MODE), "Select One", new { @class = "ddlRegularDate required req" })%>
                                        
                                    </td>

                                     <%  if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA) || LMS.Web.LoginInfo.Current.strEmpID == Model.OutOfOffice.STREMPID)
                                      { %>

                                            <td class="hideThis empty">
                                                <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].AMOUNT, new { @class = "textRegularNumber double required req", @style = "width:60px; min-width:60px;", maxlength = 10 })%>
                                            </td>
                                            <% if (Model.LstOutOfOfficeLocation.Sum(m => m.PERMITTEDAMOUNT) > 0)
                                               {%>
                                            <td class="empty">
                                                <%= Html.Encode(Model.LstOutOfOfficeLocation[i].PERMITTEDAMOUNT)%>
                                                <%= Html.HiddenFor(m => m.LstOutOfOfficeLocation[i].PERMITTEDAMOUNT)%>
                                            </td>
                                            <%} %>

                                    <%} %>
                                    <td class="empty">
                                        <%= Html.TextBoxFor(m => m.LstOutOfOfficeLocation[i].PURPOSE, new { @class = "textRegCustomWidth", @style = "width:100%" })%>
                                    </td>
                                    <td class="hideThis empty">
                                        <%= Html.CheckBoxFor(m => m.LstOutOfOfficeLocation[i].bIsRoundTrip, new { @onclick = "MakeAmountDouble(this);" })%>                                        
                                        <%= Html.Hidden("RoundTrip",Model.LstOutOfOfficeLocation[i].bIsRoundTrip) %>
                                    </td>

                                    
                                    <%
                                        if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA) || LMS.Web.LoginInfo.Current.strEmpID == Model.OutOfOffice.STREMPID)
                                        {
                                            if (Model.LstOOALocationComments != null)
                                            {
                                                Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList = Model.LstOOALocationComments.Where(l => l.LOCATIONID == Model.LstOutOfOfficeLocation[i].RECORDID).ToList();

                                            }
                                     %>
                                                                       
                                    
                                    <% 
                                        if (Model.LstOOALocationComments != null)
                                            for (int j = 0; j < Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList.Count; j++)
                                       { %>                                          
                                    <td>
                                       
                                        <% if (Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].INTAUTHORTYPEID > 0) %>
                                        <%{ %>
                                            <%= Html.Encode(Model.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STRCOMMENTS)%>
                                            <%= Html.HiddenFor(m => m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STRCOMMENTS)%>
                                            <%= Html.HiddenFor(m => m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].INTAUTHORTYPEID)%>
                                            <%= Html.HiddenFor(m => m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].LOCATIONID)%>
                                            <%= Html.HiddenFor(m => m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].OUTOFOFFICEID)%>
                                            <%= Html.HiddenFor(m => m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STRAUTHORID)%>
                                            <%= Html.HiddenFor(m => m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].STREUSER)%>
                                            <%= Html.HiddenFor(m => m.LstOutOfOfficeLocation[i].LocationWiseCommentsList[j].COMMENTID)%>

                                        <%}
                                        %>
                                       
                                    </td>

                                    <%}
                                        } %>

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
                    <% if (Model.OutOfOffice.STATUS != "GI") %>
                    <%{ %>
                    <% if ((Model.OutOfOffice.STREMPID == LMS.Web.LoginInfo.Current.strEmpID) || (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA))) %>
                    <%{ %>
                    <div style="width: 16px; float: left;">
                        <a href="#" id="hrfAdd" class="gridAdd" onclick="return AddNode();"></a>
                    </div>
                    <%}%>
                    <%} %>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                        <colgroup>
                            <col />
                            <col width="38%" />
                            <col width="33%" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th align="center" class="style2">
                                    OUT OF OFFICE
                                </th>
                                <th align="center" class="style1">
                                    EXPECTED GET IN
                                </th>
                                <th align="center">
                                    GET IN
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="style2">
                                    <% if (Model.IsNew || Model.OutOfOffice.STATUS =="GD") %>
                                    <%{ %>
                                    <div style="width: 40%; float: left">
                                        <%= Html.TextBoxFor(m => m.OutOfOffice.STRGETOUTDATE, new { @readonly = "true", @class = "textLabelLike", @style = "width:70px", @formatString = "dd-MM-yyyy" })%>
                                    </div>
                                    <div style="width: 10%; float: left">
                                        at
                                    </div>
                                    <div style="width: 40%; float: left">
                                        <%= Html.TextBoxFor(m => m.OutOfOffice.GETOUTTIME, new { @readonly = "true", @class = "textLabelLike", @style = "width:60px" })%>
                                    </div>
                                    <div style="width: 100%; float: left">
                                        <div id="adHide">
                                            <%= Html.TextBoxFor(m=> m.AdvanceDate) %>
                                        </div>
                                        <a href="#" id="advanceID" style="position: relative">Click Here for Advance out</a>
                                    </div>
                                    <%} %>
                                    <%else %>
                                    <%{ %>
                                    <%= Html.DisplayFor(m => m.OutOfOffice.STRGETOUTDATE)%>
                                    at
                                    <%= Html.DisplayFor(m => m.OutOfOffice.GETOUTTIME)%>
                                    <%= Html.HiddenFor(m => m.OutOfOffice.STRGETOUTDATE)%>
                                    <%= Html.HiddenFor(m => m.OutOfOffice.GETOUTTIME)%>
                                    <%} %>
                                </td>
                                <td class="style1">
                                    <% if (Model.IsNew || Model.OutOfOffice.STATUS == "GD") %>
                                    <%{ %>
                                    <%=Html.Hidden("StrCurrentDate", DateTime.Today.ToString("dd/MM/yyyy"))%>
                                    <%=Html.TextBoxFor(m => m.OutOfOffice.STREXPGETINDATE, new { @class = "textRegularDate dtPicker date" })%>
                                    at
                                    <%= Html.TextBoxFor(m => m.OutOfOffice.EXPGETINTIME,  new { @class = "ddlRegularDate" })%>
                                    <%} %>
                                    <%else %>
                                    <%{ %>
                                    <%= Html.DisplayFor(m => m.OutOfOffice.STREXPGETINDATE, new { @class = "textRegularDate dtPicker date" })%>
                                    <%= Html.HiddenFor(m => m.OutOfOffice.STREXPGETINDATE)%>
                                    at
                                    <%= Html.DisplayFor(m => m.OutOfOffice.EXPGETINTIME)%>
                                    <%= Html.HiddenFor(m => m.OutOfOffice.EXPGETINTIME)%>
                                    <%} %>
                                </td>
                                <td>
                                    <%if (Model.OutOfOffice.STATUS == "GI") %>
                                    <%{ %>
                                        <%= Html.DisplayFor(m => m.OutOfOffice.GETINDATE)%>                                    
                                    <%} %>
                                    <label id="lblGetInTime">
                                    </label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <%if (Model.OutOfOffice.STATUS == "GI")
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

            <%  if (LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA) || LMS.Web.LoginInfo.Current.strEmpID == Model.OutOfOffice.STREMPID)
               { %>
             <%if (Model.LstOOAApprovalComments !=null) for (int i = 0; i < Model.LstOOAApprovalComments.Count; i++)
               { %>
               
               <% if(Model.LstOOAApprovalComments[i].STRCOMMENTS.Length>0){ %>
                <tr>
                    <td>
                        <% if (Model.LstOOAApprovalComments[i].INTAPPROVERTYPEID == 1) %>
                        <%{ %>
                            Permitter And Verifier Comments
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
            <%} %>
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
    <% if (Model.IsNew) %>
    <%{ %>
    <a href="#" class="btnDraft" onclick="return Draft();"></a>
    <a href="#" class="btnGetOut" onclick="return save();"></a>
    <%} %>
    <% else if (Model.OutOfOffice.STATUS =="GD") %>
    <%{ %>
        <a href="#" class="btnGetOut" onclick="return save();"></a>
    <%} %>
    <% else  %>
    <%{ %>
    <% if (Model.OutOfOffice.STATUS == "GO") %>
    <% if ((LMS.Web.Permission.IsRightPermited(HttpContext.Current.User.Identity.Name, LMS.Web.Permission.RightNamesId.SetOthersOOA)) || (Model.OutOfOffice.STREMPID == LMS.Web.LoginInfo.Current.strEmpID))%>
    <%{ %>
    <a href="#" class="btnGetIn" onclick="return GetIn();"></a>
    <%} %>
    <%} %>

    <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>

</div>
<div id="advanceDiv">
    <iframe id="styleAdvance" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
<%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
<div id="divMsgStd" class="divMsg" style="text-align: center">
    <label id="msglbl" class="MSG">
    </label>
    <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
</div>
</form>
