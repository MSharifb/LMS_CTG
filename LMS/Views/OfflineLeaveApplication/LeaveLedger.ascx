<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.LeaveApplicationModels>" %>
<script type="text/javascript">
    $(document).ready(function () {       

        $("#LeaveApplication_fltNetBalance").val($("#fltNetBalance").val());

    });
</script>

 <div id="grid">
        <%=Html.Hidden("fltNetBalance", Model.LeaveApplication.fltNetBalance.ToString("#0.00"))%>
        <div id="grid-data" style="overflow: auto; width: 99%;">
            <table style="width: 99%;">
                <thead>
                    <tr>
                        <th>
                            Leave Type
                        </th>
                        <th style="width: 10%;">
                            Carry Over
                        </th>
                        <th style="width: 15%;">
                            Yearly Entitlement
                        </th>
                        <th style="width: 7%;">
                            Availed
                        </th>
                        <th style="width: 10%;">
                            Encashment
                        </th>
                        <th style="width: 10%;">
                            Submitted
                        </th>
                        <th style="width: 7%;">
                            Applied
                        </th>
                        <th style="width: 10%;">
                            Balance
                        </th>
                    </tr>
                </thead>
            </table>
            <div id="divLedger" style="overflow-y: auto; overflow-x: hidden; max-height: 100px">

                <table  style="width: 99%;">
                    <%  if (Model.LstLeaveLedger != null)
                        {
                            for (int j = 0; j < Model.LstLeaveLedger.Count; j++)
                            { 
                    %>
                    <tr>
                        <td>
                            <%= Html.Hidden("LstLeaveLedger[" + j + "].intLeaveTypeID", Model.LstLeaveLedger[j].intLeaveTypeID.ToString())%>
                            <%= Html.Hidden("LstLeaveLedger[" + j + "].strLeaveType", Model.LstLeaveLedger[j].strLeaveType.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].strLeaveType.ToString()%></label>
                        </td>
                        <td style="width: 10%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltOB", Model.LstLeaveLedger[j].fltOB.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltOB.ToString()%></label>
                        </td>
                        <td style="width: 15%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltEntitlement", Model.LstLeaveLedger[j].fltEntitlement.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltEntitlement.ToString()%></label>
                        </td>
                        <td style="width: 7%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltAvailed", Model.LstLeaveLedger[j].fltAvailed.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltAvailed.ToString("#0.00")%></label>
                        </td>
                        <td style="width: 10%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltEncased", Model.LstLeaveLedger[j].fltEncased.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltEncased.ToString()%></label>
                        </td>
                        <td style="width: 10%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltSubmitted", Model.LstLeaveLedger[j].fltSubmitted.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltSubmitted.ToString("#0.00")%></label>
                        </td>
                        <td style="width: 7%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltApplied", Model.LstLeaveLedger[j].fltApplied.ToString())%>
                            <label>
                                <%= Model.LstLeaveLedger[j].fltApplied.ToString("#0.00")%></label>
                        </td>
                        <td style="width: 10%;">
                            <%=Html.Hidden("LstLeaveLedger[" + j + "].fltCB", (Model.LstLeaveLedger[j].fltCB - Model.LstLeaveLedger[j].fltSubmitted).ToString())%>
                            <label>
                                <%= (Model.LstLeaveLedger[j].fltCB - Model.LstLeaveLedger[j].fltSubmitted).ToString("#0.00")%></label>
                        </td>
                    </tr>
                    <%}
                        } %>
                </table>
            </div>
        </div>
    </div>