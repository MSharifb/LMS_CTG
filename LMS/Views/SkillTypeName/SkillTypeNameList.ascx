<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.SkillTypeNameModels>" %>

<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<%@ Import Namespace="MvcContrib" %>


<script type="text/javascript">
    $(document).ready(function () {
        $("#dvDialogSkill").dialog({ autoOpen: false, modal: true, height: 240, width: 430, resizable: false, title: 'Organizational Structure', beforeclose: function (event, ui) { Closing(); } });
    });

    function Closing()
    { }

    function openDialog() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/SkillTypeName/SkillTypeNameAdd';
        $('#styleOpenerSkillTypeName').attr({ src: url });
        $("#dvDialogSkill").dialog('open');
        return false;

    }

    function popupStyleDetails(Id) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/SkillTypeName/GetDetails/' + Id;
        $('#styleOpenerSkillTypeName').attr({ src: url });
        $("#dvDialogSkill").dialog('open');
        return false;

    }

    function Refresh() {
        var targetDiv = "#divDataList";

        var url = "/LMS/SkillTypeName/search";
        var form = $("#formSkillTypeName");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
    
</script>

<form id="formSkillTypeName" method="post" action="">

<%= Html.HiddenFor(m=> m.SkillTypeNameObj.SKILLTYPENAMEID) %>
<a href="#" class="btnAdd" onclick="return openDialog();"></a>


<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
    <table width="100%" id="MISCtable">
            <thead>
                <tr>
                    <th>
                        Skill Type Name
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
                   <% foreach (SkillTypeName item in Model.LstSkillTypeName)
                        {%>
                        <tr>
                            <td>
                                <%=Html.Encode(item.SKILLTYPENAME)%>
                            </td>
                            <td>
                                <%=
                                    Html.Encode(item.REMARKS)
                                    %>

                                    
                            </td>                           
                            <td style="width: 10%;">                               
                                <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= item.SKILLTYPENAMEID %>);'>
                                </a>
                            </td>
                        </tr>
                    <% } %>
                </tbody>

    </table>
    </div>

    <div class="pager">
      <%= Html.PagerWithScript(ViewData.Model.LstSkillTypeNamePaging.PageSize, ViewData.Model.LstSkillTypeNamePaging.PageNumber, ViewData.Model.numTotalRows, "frmSkillTypeName", "/LMS/SkillTypeName/SkillTypeNamePaging", "divDataList")%>
      <%= Html.Hidden("txtPageNo", ViewData.Model.LstSkillTypeNamePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString()%></label>

    </div>

<div id="dvDialogSkill">
    <iframe id="styleOpenerSkillTypeName" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</form>