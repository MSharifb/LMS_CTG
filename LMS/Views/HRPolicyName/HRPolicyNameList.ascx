<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.HRPolicyNameModels>" %>
<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<%@ Import Namespace="MvcContrib" %>


<script type="text/javascript">
    $(document).ready(function () {
        $("#dvDialogHR").dialog({ autoOpen: false, modal: true, height: 240, width: 430, resizable: false, title: 'Organizational Structure', beforeclose: function (event, ui) { Closing(); } });
    });

    function Closing()
    { }

    function openDialog() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/HRPolicyName/HRPolicyNameAdd';
        $('#styleOpenerHRPolicyName').attr({ src: url });
        $("#dvDialogHR").dialog('open');
        return false;

    }

    function popupStyleDetails(Id) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/HRPolicyName/GetDetails/'+Id;
        $('#styleOpenerHRPolicyName').attr({ src: url });
        $("#dvDialogHR").dialog('open');
        return false;

    }

    function Refresh() {
        var targetDiv = "#divDataList";

        var url = "/LMS/HRPolicyName/search";
        var form = $("#formHrPolicyName");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
    
</script>

<form id="formHrPolicyName" method="post" action="">

<%= Html.HiddenFor(m=> m.HrPolicyTypeNameObj.HRPOLICYTYPENAMEID) %>
<a href="#" class="btnAdd" onclick="return openDialog();"></a>


<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
    <table width="100%" id="MISCtable">
            <thead>
                <tr>
                    <th>
                        HR Policy Type Name
                    </th>
                    <th>
                        Remarks
                    </th>                  
                    <th>
                        Edit
                    </th>
                </tr>
            </thead>
            <tbody>
                   <% foreach (HRPolicyTypeName item in Model.LstHRPolicyTypeName)
                        {%>
                        <tr>
                            <td>
                                <%=Html.Encode(item.HRPOLICYTYPENAME)%>
                            </td>
                            <td>
                                <%=
                                    Html.Encode(item.REMARKS)
                                    %>

                                    
                            </td>                           
                            <td style="width: 10%;">                               
                                <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= item.HRPOLICYTYPENAMEID %>);'>
                                </a>
                            </td>
                        </tr>
                    <% } %>
                </tbody>

    </table>
    </div>

    <div class="pager">
      <%= Html.PagerWithScript(ViewData.Model.LstHRPolicyTypeNamePaging.PageSize, ViewData.Model.LstHRPolicyTypeNamePaging.PageNumber, ViewData.Model.numTotalRows, "formHrPolicyName", "/LMS/HRPolicyName/HRPolicyTypeNamePaging", "divDataList")%>
      <%= Html.Hidden("txtPageNo", ViewData.Model.LstHRPolicyTypeNamePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString()%></label>

    </div>

<div id="dvDialogHR">
    <iframe id="styleOpenerHRPolicyName" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</form>