<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.OutOfOfficeModels>" %>
<%@ Import Namespace="LMS.Web.Models" %>

<script type="text/javascript">

    $(document).ready(function () {

        $("#datepicker").datepicker({
            changeMonth: true,
            changeYear: false
        }).click(function () { $('#txtDate').datepicker('show'); });


        $(".dtPicker").datepicker({ dateFormat: 'dd/mm/yy', changeYear: false });
        $('#txtTime').timepicker({ ampm: true, timeFormat: 'hh:mm TT' });
        
    });

    function Closing() {

    }

   
    function closeDialog() {        
        window.parent.closeAdvanceDate();
    }
    function setDateTime() {
       
        var time;
        var date = document.getElementById('txtDate').value;
        var dateArr = new Array();
        dateArr = date.split('/');
        var dt1 = new Date(dateArr[1] + '/' + dateArr[0] + '/' + dateArr[2]);
       
        if (dt1 <= new Date()) {
            alert('Advance Date should greater than current date.');
            return;
        }

        time = $("#txtTime").val();

        window.parent.setDateTime(date, time);
    }

</script>
<% OutOfOfficeModels model = new OutOfOfficeModels(); %>
<table width="100%">
    <tr>
        <td>
            Date
        </td>
        <td>
            Time
        </td>
    </tr>

    <tr>
        <td>
             <%=Html.TextBox("txtDate",DateTime.Now.ToString("dd/MM/yyyy"), new { @class = "textRegularDate dtPicker date"})%>
            <img alt="" id="datepicker" style="height:16px;" src="<%= Url.Content("~/Content/img/controls/date.gif")%>"  />
               
        </td>
        <td>
            <%= Html.TextBox("txtTime", DateTime.Now.ToShortTimeString(), new { @class = "textRegularDate date" })%>
        </td>
    </tr>

    <tr>
        <td>
            <a href="#" class="btnOk" onclick="javascript:return setDateTime();"></a>
        </td>
        <td>
            <a href="#" class="btnCancel" onclick="closeDialog();"></a>
        </td>
    </tr>
</table>

