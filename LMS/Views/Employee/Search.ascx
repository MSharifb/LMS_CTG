<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.EmployeeModels>" %>

<script type="text/javascript">

    function searchData() {

        var targetDiv = "#dvSearch";
        var url = "/LMS/Employee/Search";
        var form = $("#formSearch");
       
        var serializedForm = form.serialize();
       
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }

</script>

<form id="formSearch" action="">
<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td class="contenttabletd">
                Zone
            </td>
            <td style="width: 45%;" class="contenttabletd">                
                <%= Html.DropDownListFor(m => m.strSearchZoneId, Model.Zone, "...Select One..", new { @class = "selectBoxRegular", @style = "width:175px", @onchange = "searchData();" })%>
            </td>
            <td class="contenttabletd">
                ID No.
            </td>
            <td style="width: 10%;" class="contenttabletd">               
                <%= Html.TextBoxFor(m => m.strSearchInitial, new { @class = "textRegular", @style = "width:50px", @maxlength = 50, @onkeyup = "searchData();" })%>
            </td>
            <td class="contenttabletd">
                Name
            </td>
            <td style="width: 25%;" class="contenttabletd">                
                <%=Html.TextBoxFor(m=> m.strSearchName, new { @class = "textRegular", @maxlength = 100, @style = "width:140px", @onkeyup = "searchData();"  })%>
            </td>
        </tr>
        <tr>
            <td class="contenttabletd">
                Department
            </td>
            <td style="width: 45%;" class="contenttabletd">                
                <%= Html.DropDownListFor(m=> m.strSearchDepartmentId, Model.Department, "...Select One..", new { @class = "selectBoxRegular",@style="width:175px" , @onchange = "searchData();"})%>
            </td>
            <td class="contenttabletd">
                Status
            </td>
            <td style="width: 10%;" class="contenttabletd">                
                <%= Html.DropDownListFor(m => m.strSearchStatus, Model.Status, new { @class = "selectBoxRegular", @style = "width:75px", @onchange = "searchData();" })%>
            </td>
            <td align="right" style="width: 5%;" class="contenttabletd">
                <a href="#" class="btnSearchData" onclick="return searchData(this);"></a>
            </td>

        </tr>
    </table>
</div>
</form>