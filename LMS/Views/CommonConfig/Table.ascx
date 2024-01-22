<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CommonConfigModels>" %>

<script type="text/javascript">

    function savedata() 
    {
        if (fnValidate() == true) 
        {
            //executeAction('frmTableMapping', '/LMS/CommonConfig/SaveHRMTableMapping', 'divStyleCommonConfig');

            var form = $("#frmTableMapping");
            var serializedForm = form.serialize();
            var url = '/LMS/CommonConfig/SaveHRMTableMapping';
            $.post(url, serializedForm, function (result) { $("#tabs-1").html(result); }, "html");
        }        
        return false;
    }


   </script>

<form id="frmTableMapping" method="post" action="">
   <div id="grid">
    <div id="grid-data">
        <table >
            <thead>
                    <tr>
                        <th>
                            LMS Table            
                        </th>
                        <th>
                            Source Table
                        </th>
                    </tr>
                </thead>

                <tbody>
                   <%  for (int j = 0; j < Model.HRMTableMapping.LstHRMTableMapping.Count; j++)
                        {%>
                        <tr>
                            <td>
                                <%=Html.Hidden("HRMTableMapping.LstHRMTableMapping[" + j + "].TableID", Model.HRMTableMapping.LstHRMTableMapping[j].TableID.ToString())%>
                                <%=Html.Encode(Model.HRMTableMapping.LstHRMTableMapping[j].TableName)%><label class="labelRequired">*</label>
                            </td>
                            <td>
                                <%=Html.TextBox("HRMTableMapping.LstHRMTableMapping[" + j + "].SourceTableName", Model.HRMTableMapping.LstHRMTableMapping[j].SourceTableName, new { @class = "required", @style = "width:250px; min-width:350px;", maxlength = 500 })%>
                            </td>
                        </tr>
                    <% } %>
                </tbody>
        </table>

       </div> 
   </div>
   
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>

   <div class="divButton">
        <a id="btnImgSave" href="#" class="btnSave" onclick="return savedata();"></a>
        <%--<a href="#" class="btnClose" onclick="return closeDialog();"></a>--%>
   </div>  

    <div id="divMsgStd" class="divMsg">
        <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
    </div>
      
</form>

