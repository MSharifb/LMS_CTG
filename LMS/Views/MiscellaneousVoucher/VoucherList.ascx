<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MiscellaneousVoucherModels>" %>

<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<%@ Import Namespace="LMSEntity" %>

<script type="text/javascript">

    $(document).ready(function () {

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
        , showOn: 'button'
        , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
        , buttonImageOnly: true
        });

        $("#divMiscellaneous").dialog({ autoOpen: false, modal: true, height: 550, width: 800, resizable: false, title: 'Conveyance Details', beforeclose: function (event, ui) { Closing(); } });
        $("#dvDetailsList").dialog({ autoOpen: false, modal: true, height: 650, width: 800, resizable: false, title: 'Conveyance Details', beforeclose: function (event, ui) { Closing(); } });
        

        preventSubmitOnEnter($("#frmMiscellaneous"));
        
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 420, width: 750, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });
        
        $('#link-table tr').dblclick(function () {
            $(this).find('td a').click();
        });

        
        $('#MiscellaneousVoucher_STREMPNAME').autocomplete('<%=Url.Action("LookUps", "Employee") %>', {
            dataType: 'json',
            parse: function (data) {
                var rows = new Array();
                for (var i = 0; i < data.length; i++) {
                    rows[i] = { data: data[i], value: data[i].strEmpID, result: data[i].strEmpName.trim() };

                }
                return rows;
            },
            formatItem: function (row, i, n) {
                return row.strEmpID + ' - ' + row.strEmpName;
            },
            width: 300,
            mustMatch: true,
            selectFirst: true
        });

        $("#MiscellaneousVoucher_STREMPNAME").result(function (event, data, formatted) {
            if (data) {


                $("#MiscellaneousVoucher_STREMPID").val(data.strEmpID);

                return false;
            }

        });


        $("#btnPrintPreview").hide();
    });

    function Closing() {
    }

    function ShowDetails(id) {
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MiscellaneousVoucher/Details/' + id;
        $('#styleOpenerMisc').attr({ src: url });
        $("#divMiscellaneous").dialog('open');
        return false;
    }

    //    $("#datepicker").datepicker({
    //        changeMonth: true,
    //        changeYear: false
    //    }).click(function () { $('#ConveyanceObj_STRDATE').datepicker('show'); });

    function setData(id, name) {

        document.getElementById('StrEmpName').value = name;
        document.getElementById('StrEmpID').value = id;
        $("#divEmpList").dialog('close');
    }

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


    function searchConvData() {

        var targetDiv = "#tabs-1";
        var url = "/LMS/MiscellaneousVoucher/searchData";
        var form = $("#frmMiscellaneous");
        var serializedForm = form.serialize();

        var isPaid = $("#Model_IsPaid").val();
        var strEmpID = $("#StrEmpID").val();
        var strDate = $("#StrDate").val();
        var strName = $("#StrEmpName").val()

        $.post(url, { IsPaid: isPaid, StrEmpID: strEmpID, StrName: strName, StrDate: strDate }, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }


    function changeCheckBox(id) {

        var idList = $("#IDLIST").val();

        if (!id.checked) {

            if (idList.search(',') < -1) {
                idList = "";
                $("#IDLIST").val(idList);
                return false;
            }
            var ids = idList.split(',');
            idList = "";
          
            for (var i = 0; i < ids.length; i++) {
                if (ids[i] != id.name) {
                    if (idList.length > 0) {
                        idList += "," + ids[i];
                    }
                    else
                        idList = ids[i];
                }
            }   
          
        }
        else {
            
            if (idList.length > 0) {
                idList += "," + id.name;
            }
            else
                idList = id.name;
        }

        var totalLength = idList.split(',');
        
        if (idList.length < 1) {
            $("#btnPrintPreview").hide();
        }
        else {
            $("#btnPrintPreview").show();
        }

        if (totalLength.length > 2) {
            
            alert('Only 2 records can be printed accurately.');
        }
        $("#IDLIST").val(idList);
    }


    function ShowDetailList() {
        var id = $("#IDLIST").val();
        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MiscellaneousVoucher/GetDetailsList/' + id;
        $('#styleDetailsList').attr({ src: url });
        $("#dvDetailsList").dialog('open');
        return false;
    }
</script>

<form id="frmMiscellaneous" method="post" action="">

<%= Html.Hidden("Model.IsPaid", Model.IsPaid)%>
<%= Html.HiddenFor(m=> m.StrEmpID)%>
<%= Html.Hidden("IDLIST") %>
    <table>
        <tr>
            <td>
                Employee
            </td>
            <td>              
                    <%= Html.TextBoxFor(m => m.StrEmpName, new { @class = "textRegCustomWidth", @Style = "width:250px", @readonly = "true" })%>
                  <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
            </td>
            <td>
                Date
            </td>
            <td>
                <%= Html.TextBoxFor(m=> m.StrDate, new { @class = "textRegularDate dtPicker date" })%>               
                <%--<img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />--%>
            </td>
            <td>
                <a href="#" class="btnSearchData" onclick="return searchConvData();"></a>
            </td>
        </tr>
    </table>

    <div id="grid">
        <div id="grid-data" style="overflow: auto; width: 99%">
            <table  width="100%" id="link-table">
                <thead>
                    <tr>
                        <th></th>
                        <th>
                            Employee            
                        </th>
                        <th>
                            Date
                        </th>

                        <%if (Model.IsPaid == "1")
                          { %>
                        <th>
                            Select
                        </th>
                        <%} %>
                    </tr>
                </thead>

                <tbody>
                    <%  foreach (MiscellaneousVoucher item in Model.LstMiscellaneousVoucher){%>
                        <tr>
                            <td>
                                <a href="#" class="gridEdit" onclick="return ShowDetails('<%=item.MISCID %>')"></a>
                            </td>
                            <td> 
                                <a href="#" onclick="return ShowDetails('<%=item.MISCID %>')"></a>
                                <%= Html.Encode(item.STREMPNAME)%>
                            </td>
                            <td>
                                <%= Html.Encode(item.MISCDATE.ToString("dd-MMM-yyyy"))%>                                
                            </td>
                            <%if (Model.IsPaid == "1")
                              { %>
                               <td>
                                <%= Html.CheckBox(item.MISCID.ToString(), false, new { @onchange = "return changeCheckBox(this)" })%>                                
                               </td>
                            <%} %>
                        </tr>
                    <%   } %>
                                    
                </tbody>
            </table> 
            
            <div class="pager">
                <%= Html.PagerWithScript(ViewData.Model.LstMiscellaneousVoucherPaging.PageSize, ViewData.Model.LstMiscellaneousVoucherPaging.PageNumber, ViewData.Model.numTotalRows, "frmConveyanceDue", "/LMS/Conveyance/ConveyanceDuePaging", "tabs-1")%>
                <%= Html.Hidden("txtPageNo", ViewData.Model.LstMiscellaneousVoucherPaging.PageNumber)%>
            </div>
            <label id="lblTotalRows">
                Total Records:<%=Model.numTotalRows.ToString() %>
            </label>
               
        </div>

        <% if (Model.IsPaid == "1")
           { %>
           <div style="text-align:center">
                <a href="#" class="btnPreview"  id="btnPrintPreview" onclick="ShowDetailList()"></a> 
            </div>
        <%} %>
    </div>
        
          
 <div id="divMiscellaneous">
    <iframe id="styleOpenerMisc" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>
  
<div id="dvDetailsList">
    <iframe id="styleDetailsList" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</form>

