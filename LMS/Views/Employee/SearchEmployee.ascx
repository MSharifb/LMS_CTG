<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.EmployeeModels>" %>
<%@ Import Namespace="MvcPaging" %>
<script type="text/javascript">

    $(document).ready(function () {

        preventSubmitOnEnter($("#frmSearchEmployee"));

      

    });

//    function searchData(obj) {

//        document.getElementById("CurrentFocusID").value = obj.id;
//        
//        var targetDiv = "#divEmpList";
//        var url = "/LMS/Employee/EmployeeList/"+obj.id;
//        var form = $("#frmSearchEmployee");
//        var serializedForm = form.serialize();

//        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");
//        
//        return false;
//    }

</script>
<form id="frmSearchEmployee" method="post" action="">


<%--<%= Html.HiddenFor(m=> m.CurrentFocusID) %>--%>
<%--<div id="divSearchData">
    <table class="contenttext" style="width: 100%;">
        <tr>
            <td class="contenttabletd">
                ID
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%=Html.TextBox("Model.strSearchID", Model.strSearchID, new { @class = "textRegular", @style = "width:50px", @maxlength = 50, @onkeyup = "searchData(this);", @onfocus = "this.value = this.value;" })%>
            </td>
            <td class="contenttabletd">
                Name
            </td>
            <td style="width: 25%;" class="contenttabletd">
                <%=Html.TextBox("Model.strSearchName", Model.strSearchName, new { @class = "textRegular", @maxlength = 100, @style = "width:140px", @onkeyup = "searchData(this);" , @onfocus="this.value = this.value;" })%>
            </td>
            <td class="contenttabletd">
                Department
            </td>
            <td style="width: 45%;" class="contenttabletd">
                <%= Html.DropDownList("Model.strSearchDepartmentId", Model.Department, "...Select One..", new { @class = "selectBoxRegular",@style="width:175px" , @onkeyup = "searchData(this);"})%>
            </td>
            <td class="contenttabletd">
                Status
            </td>
            <td style="width: 10%;" class="contenttabletd">
                <%= Html.DropDownList("Model.strSearchStatus", Model.Status, new { @class = "selectBoxRegular", @style = "width:75px" })%>
            </td>
            <td align="right" style="width: 5%;" class="contenttabletd">
                <a href="#" class="btnSearchData" onclick="return searchData(this);"></a>
            </td>
        </tr>
    </table>
</div>--%>
<div id="grid">
    <div id="grid-data">
        <table>
            <thead>
                <tr>
                    <th>
                    </th>
                    <th>
                       Employee ID
                    </th>
                    <th>
                      Employee Name
                    </th>
                    <th>
                        Designation
                    </th>
                </tr>
            </thead>
            <tbody>
                <% foreach (LMSEntity.Employee obj in Model.Employees)
                   { 
                %>
                <tr>
                    <td>
                        <a href='#' class="gridEdit" onclick='javascript:return setData("<%=obj.strEmpID%>","<%=obj.strEmpID%>","<%=obj.strEmpName%>");'>
                        </a>
                    </td>
                    <td>
                        <%= Html.Encode(obj.strEmpID)%>
                    </td>
                    <td>
                        <%= Html.Encode(obj.strEmpName) %>
                    </td>
                    <td>
                        <%= Html.Encode(obj.strDesignation)%>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
</div>
<div class="pager">
    <%= Html.PagerWithScript(10, Model.LstEmployeesPaging.PageNumber, ViewData.Model.numTotalRows, "formSearch", "/LMS/Employee/EmployeeList", "dvSearch")%>
</div>
<label id="lblTotalRows">
    Total Records:<%=Model.numTotalRows.ToString() %></label>
<%--<div>
<a href="#" class="btnClose" onclick="return closeDialog();"></a>
</div>--%>
</form>
