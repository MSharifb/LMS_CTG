<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CommonConfigModels>" %>

<script type="text/javascript">

//    $(document).ready(function () {
//        $("input:textbox").each(function (i) {
//            this.value = '';            
//        });
//    });

    function CleanupText() {
        $("input:textbox").each(function (i) {
            this.value = '';
        });

    }

   </script>

<div id="grid">
    <div id="grid-data" style="overflow-y: auto; overflow-x: hidden; max-height: 320px"> 
        <table >
            <thead>
                    <tr>
                        <th>
                            LMS Column Name            
                        </th>
                        <th>
                            Source Column Name
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%--<script type="text/javascript" language="JavaScript">
                        CleanupText();
                    </script>--%>
                   <% for (int j = 0; j < Model.HRMColumnMapping.LstHRMColumnMapping.Count; j++)
                        {%>
                        <tr>
                            <td>                                 
                                <%=Html.Hidden("HRMColumnMapping.LstHRMColumnMapping[" + j + "].TableID", Model.HRMColumnMapping.LstHRMColumnMapping[j].TableID.ToString())%>
                                <%=Html.Hidden("HRMColumnMapping.LstHRMColumnMapping[" + j + "].ColumnID", Model.HRMColumnMapping.LstHRMColumnMapping[j].ColumnID.ToString())%>
                                <%=Html.Hidden("HRMColumnMapping.LstHRMColumnMapping[" + j + "].ColumnName", Model.HRMColumnMapping.LstHRMColumnMapping[j].ColumnName.ToString())%>
                                <%=Html.Encode(Model.HRMColumnMapping.LstHRMColumnMapping[j].ColumnName)%>
                            </td>
                            <td>
                                <%=Html.TextBox("HRMColumnMapping.LstHRMColumnMapping[" + j + "].SourceColumnName", Model.HRMColumnMapping.LstHRMColumnMapping[j].SourceColumnName, new { @class = "required", @style = "width:250px; min-width:350px;", maxlength = 500 })%>
                                
                            </td>
                        </tr>
                    <% } %>
                </tbody>
        </table>
      </div> 
   </div>