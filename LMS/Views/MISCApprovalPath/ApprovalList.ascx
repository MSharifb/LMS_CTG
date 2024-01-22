<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.MISCApprovalPathModels>" %>
<%@ Import Namespace="LMSEntity" %>


<form id="frmMISCApproval" method="post" action="">

 <div id="grid">
    <div id="grid-data" style="overflow: auto; width: 99%">
    <table width="100%" id="MISCtable">
            <thead>
                <tr>
                    <th>
                        Employee Name
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        Department
                    </th>
                    <th>
                        Date
                    </th>
                    <th>
                        Edit
                    </th>
                </tr>
            </thead>
            <tbody>
                   <% foreach (MISCApproval item in Model.LstMISCApproval)
                        {%>
                        <tr>
                            <td>
                                <%=Html.Encode(item.EMPLOYEENAME)%>
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                <%=Html.Encode(item.DTFIRSTARRIVALDATETIME.ToString("dd-MMM-yyyy"))%>
                            </td>
                            <td style="width: 10%;">                               
                                <a href='#' class="gridEdit" onclick='javascript:return popupStyleDetails(<%= item.MISCMASTERID %>);'>
                                </a>
                            </td>
                        </tr>
                    <% } %>
                </tbody>

    </table>
    </div>

    <%--<div class="pager">
      <%= Html.PagerWithScript(ViewData.Model.LstMISCMasterPaging.PageSize, ViewData.Model.LstMISCMasterPaging.PageNumber, ViewData.Model.numTotalRows, "frmMiscList", "/LMS/MISC/MISCLIST", "divDataList")%>
      <%= Html.Hidden("txtPageNo", ViewData.Model.LstMISCMasterPaging.PageNumber)%>
    </div>
    <label id="lblTotalRows">
        Total Records:<%=Model.numTotalRows.ToString()%></label>

    </div>--%>
    
</form>