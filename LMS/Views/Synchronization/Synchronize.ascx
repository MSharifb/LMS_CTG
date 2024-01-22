<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.DataSynchronizationModel>" %>
<%= Html.ValidationSummary("Process Failed.") %>
<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmSynchronize"));
    });

    function ProcessData() {

        document.getElementById('imgLoader').style.visibility = 'visible';
        targetDiv = "#" + 'divDataList';
        var url = '/LMS/Synchronization/Synchronize';
        var form = $("#" + 'frmSynchronize');
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
        return false;

    }
</script>
<form id="frmSynchronize" method="post" action="">
<div>
    <table>
        <tr>
            <td>
                <%=Html.CheckBox("Model.Synchronizer.bitEmployee", Model.Synchronizer.bitEmployee)%>Employee
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.CheckBox("Model.Synchronizer.bitCompany", Model.Synchronizer.bitCompany)%>Company
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.CheckBox("Model.Synchronizer.bitDepartment", Model.Synchronizer.bitDepartment)%>Department
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.CheckBox("Model.Synchronizer.bitDesignation", Model.Synchronizer.bitDesignation)%>Designation
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.CheckBox("Model.Synchronizer.bitLocation", Model.Synchronizer.bitLocation)%>Branch
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.CheckBox("Model.Synchronizer.bitReligion", Model.Synchronizer.bitReligion)%>Religion
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.CheckBox("Model.Synchronizer.bitEmployeeCategory", Model.Synchronizer.bitEmployeeCategory)%>Employee
                Category
            </td>
        </tr>
    </table>
</div>
<div style="height: 15px;">
</div>
<div>
    <a href="#" class="btnProcess" onclick="return ProcessData();"></a>
</div>
<img id="imgLoader" src="/LMS/Content/ajax-loader.gif" style="visibility: hidden;" />
<div>
    <%=Model.Message %>
</div>
</form>
