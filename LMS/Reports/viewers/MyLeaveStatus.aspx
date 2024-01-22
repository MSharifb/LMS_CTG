<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/viewers/ReportMaster.Master"
    AutoEventWireup="true" CodeBehind="MyLeaveStatus.aspx.cs" Inherits="LMS.Web.Reports.viewers.MyLeaveStatus" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

    $(document).ready(function () {
        preventSubmitOnEnter($("#frmReports"));

        //reportPreview();
        OptionAvtiveLeaveYear();

        $(".dtPicker").datepicker({
            dateFormat: 'dd-mm-yy'
            , changeYear: false
            , showOn: 'button'
            , buttonImage: '../../Content/img/controls/calendar-blue.gif'
            , buttonImageOnly: true
        });

    });



    function reportPreview() {

        if (fnValidate() == true) {

            return true;

        }
        else
        { return false; }

    }

    function OptionAvtiveLeaveYear() {

        var valActiveLeaveYear = $("#<%=ckbActiveLeaveYear.ClientID%>").is(':checked');

        if (valActiveLeaveYear) {
            $('#<%=ddlLeaveYear.ClientID %>').removeClass('required');
            $('#<%=ddlLeaveYear.ClientID %>').val('0');
            $('#<%=ddlLeaveYear.ClientID %>').attr('disabled', 'disabled');

        }
        else {
            $('#<%=ddlLeaveYear.ClientID %>').removeAttr('disabled');
            $('#<%=ddlLeaveYear.ClientID %>').addClass('required');
        }

    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ReportMain" runat="server">

<h3 class="page-title">My Leave Status</h3>

    <div class="divSpacer">
    </div>
    <div class="divRow">
        <table class="contenttext" style="width: 100%;">
            <tr>
                <td>
                    Applicant ID
                </td>
                <td>
                    <asp:HiddenField ID="hfEmpID" runat="server" />
                    <asp:Label ID="lblInitial" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Applicant Name:
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Leave Year<label class="labelRequired">*</label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLeaveYear" runat="server" class="selectBoxRegular required">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ckbActiveLeaveYear" runat="server" Text="Active Leave Year" Checked="true"  onchange="OptionAvtiveLeaveYear();" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:RadioButton ID="rdIsYearlyLeave" runat="server" Checked="True" GroupName="LeaveStatus"
                        Text="Yearly leave" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdIsServiceTypeLeave" runat="server" GroupName="LeaveStatus" Text="Service Type Leave" />
                </td>
            </tr>
        </table>
    </div>
    <div class="divButton">
        <asp:Button ID="btnViewReport" runat="server" class="btnPreview" OnClientClick="return reportPreview();" OnClick="btnViewReport_Click" />
    </div>
    <div id="divMsgRpt" class="divMsg">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="rvMyLeaveStatus" runat="server" Width="100%" Height="100%" SizeToReportContent="True"
                OnReportRefresh="rvMyLeaveStatus_ReportRefresh">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
