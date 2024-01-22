<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/viewers/ReportMaster.Master" AutoEventWireup="true" CodeBehind="PrintLeaveApplication.aspx.cs" Inherits="LMS.Web.Reports.viewers.PrintLeaveApplication" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ReportMain" runat="server">
<asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="rvMyLeaveStatus" runat="server" Width="100%" Height="100%" SizeToReportContent="True"
                OnReportRefresh="rvMyLeaveStatus_ReportRefresh">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
