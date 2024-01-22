<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.JobTypeNameModels>" %>

<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<%@ Import Namespace="MvcContrib" %>


<script type="text/javascript">
    $(document).ready(function () {
        $("#dvDialogJob").dialog({ autoOpen: false, modal: true, height: 240, width: 430, resizable: false, title: 'Organizational Structure', beforeclose: function (event, ui) { Closing(); } });
    });

    function Closing()
    { }

    function openDialog() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/JobTypeName/JobTypeNameAdd';
        $('#styleOpenerJobTypeName').attr({ src: url });
        $("#dvDialogJob").dialog('open');
        return false;

    }

    function popupStyleDetails(Id) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/JobTypeName/GetDetails/' + Id;
        $('#styleOpenerJobTypeName').attr({ src: url });
        $("#dvDialogJob").dialog('open');
        return false;

    }

    function Refresh() {
        var targetDiv = "#divDataList";

        var url = "/LMS/JobTypeName/search";
        var form = $("#formJobTypeName");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
    
</script>

<form id="formJobTypeName" method="post" action="">

<%= Html.HiddenFor(m=> m.JobTypeNameObj.JOBTYPENAMEID) %>
<a href="#" class="btnAdd" onclick="return openDialog();"></a>


<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
    <table width="100%" id="MISCtable">
            <thead>
                <tr>
                    <th>
                        Job Type Name
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
                   <% foreach (JobTypeName item in Model.LstJobTypeName)
                        {%>
                        <tr>
                            <td>
                                <%=Html.Encode(item.JOBTYPENAME)%>
                            </td>
                            <td>
                                <%=
                                    Html.Encode(item.REMARKS)
                                    %>

                                    
                            </td>                           
                            <td style="width: 10%;">                               
                                <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= item.JOBTYPENAMEID %>);'>
                                </a>
                            </td>
                        </tr>
                    <% } %>
                </tbody>

    </table>
    </div>

    <div class="pager">
      <%= Html.PagerWithScript(ViewData.Model.LstJobTypeNamePaging.PageSize, ViewData.Model.LstJobTypeNamePaging.PageNumber, ViewData.Model.numTotalRows, "frmJobTypeName", "/LMS/JobTypeName/JobTypeNamePaging", "divDataList")%>
      <%= Html.Hidden("txtPageNo", ViewData.Model.LstJobTypeNamePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString()%></label>

    </div>

<div id="dvDialogJob">
    <iframe id="styleOpenerJobTypeName" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</form>

