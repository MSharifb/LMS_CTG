<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.ConveyanceReportModels>" %>

<script type="text/javascript">

    $(document).ready(function () {
        $("#divReportView").dialog({ autoOpen: false, modal: true, height: 700, width: 950, resizable: false, title: 'Report View', beforeclose: function (event, ui) { Closing(); } });
        $("#divEmpList").dialog({ autoOpen: false, modal: true, height: 430, width: 750, resizable: false, title: 'Employee', beforeclose: function (event, ui) { Closing(); } });

        $(".dtPicker").datepicker({ dateFormat: 'dd-mm-yy', changeYear: false
            , showOn: 'button'
            , buttonImage: '<%= Url.Content("~/Content/img/controls/calendar-blue.gif")%>'
            , buttonImageOnly: true
        });


        $('#StrEmpName').autocomplete('<%=Url.Action("LookUps", "Employee") %>', {
            dataType: 'json',
            parse: function (data) {
                var rows = new Array();
                for (var i = 0; i < data.length; i++) {
                    rows[i] = { data: data[i], value: data[i].strEmpID, result: data[i].strEmpName };

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

        $("#StrEmpName").result(function (event, data, formatted) {
            if (data) {


                $("#StrEmpID").val(data.strEmpID);

                return false;
            }

        });


    });

    function Closing() {

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

    function setData(id, name) {


        document.getElementById('StrEmpID').value = id;
        document.getElementById('StrEmpName').value = name;

        $("#divEmpList").dialog('close');

    }

    function reportPreview() {
       
        if (fnValidate() == true) {
  
                executeAction('formConveyanceReport', '/LMS/ConveyanceReport/ShowReport', 'divReportView');
                $('#divReportView').dialog('open');
           
        }

        return false;
    }
</script>

<form id="formConveyanceReport" method="post" action="">
    <%= Html.HiddenFor(m=> m.StrEmpID) %>
    <table>
        <tr>
            <td>
                Employee Name
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.StrEmpName, new { @class="textRegular"})%>
                <a href="#" class="btnSearch" onclick="return openEmployee();"></a>
            </td>

            <td>
                Department
            </td>

            <td>
                <%= Html.DropDownListFor(m => m.StrDepartment, Model.Department,"...All...", new { @class = "selectBoxRegular" })%>
            </td>
        </tr>

        <tr>
            <td>
                From Date
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.StrFromDate, new { @class = "textRegularDate dtPicker" })%>
            </td>
            <td>
                To Date
            </td>
            <td>
                <%= Html.TextBoxFor(m => m.StrToDate, new { @class = "textRegularDate dtPicker" })%>
            </td>
        </tr>

    </table>

    <div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divSpacer">
</div>
<div class="divButton">
    <a href="#" class="btnPreview" onclick="return reportPreview();"></a>
</div>

<div id="divReportView">
</div>

</form>
