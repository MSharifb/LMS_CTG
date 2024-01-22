<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MembershipTypeNameModels>" %>

<%@ Import Namespace="LMSEntity" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcPaging" %>
<%@ Import Namespace="MvcContrib" %>


<script type="text/javascript">
    $(document).ready(function () {
        $("#dvDialogMembership").dialog({ autoOpen: false, modal: true, height: 240, width: 430, resizable: false, title: 'Organizational Structure', beforeclose: function (event, ui) { Closing(); } });
    });

    function Closing()
    { }

    function openDialog() {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MembershipTypeName/MembershipTypeNameAdd';
        $('#styleOpenerMembershipTypeName').attr({ src: url });
        $("#dvDialogMembership").dialog('open');
        return false;

    }

    function popupStyleDetails(Id) {

        var host = window.location.host;
        var url = 'http://' + host + '/LMS/MembershipTypeName/GetDetails/' + Id;
        $('#styleOpenerMembershipTypeName').attr({ src: url });
        $("#dvDialogMembership").dialog('open');
        return false;

    }

    function Refresh() {
        var targetDiv = "#divDataList";

        var url = "/LMS/MembershipTypeName/search";
        var form = $("#formMembershipTypeName");
        var serializedForm = form.serialize();
        $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

        return false;
    }
    
</script>

<form id="formMembershipTypeName" method="post" action="">

<%= Html.HiddenFor(m=> m.MembershipTypeNameObj.MEMBERSHIPTYPENAMEID) %>
<a href="#" class="btnAdd" onclick="return openDialog();"></a>


<div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
    <table width="100%" id="MISCtable">
            <thead>
                <tr>
                    <th>
                        Membership Type Name
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
                   <% foreach (MembershipTypeName item in Model.LstMembershipTypeName)
                        {%>
                        <tr>
                            <td>
                                <%=Html.Encode(item.MEMBERSHIPTYPENAME)%>
                            </td>
                            <td>
                                <%=
                                    Html.Encode(item.REMARKS)
                                    %>

                                    
                            </td>                           
                            <td style="width: 10%;">                               
                                <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= item.MEMBERSHIPTYPENAMEID %>);'>
                                </a>
                            </td>
                        </tr>
                    <% } %>
                </tbody>

    </table>
    </div>

    <div class="pager">
      <%= Html.PagerWithScript(ViewData.Model.LstMembershipTypeNamePaging.PageSize, ViewData.Model.LstMembershipTypeNamePaging.PageNumber, ViewData.Model.numTotalRows, "frmMembershipTypeName", "/LMS/MembershipTypeName/MembershipTypeNamePaging", "divDataList")%>
      <%= Html.Hidden("txtPageNo", ViewData.Model.LstMembershipTypeNamePaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString()%></label>

    </div>

<div id="dvDialogMembership">
    <iframe id="styleOpenerMembershipTypeName" src="" width="99%" height="98%" style="border: 0px solid white;
        padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>

</form>