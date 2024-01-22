<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Reports/viewers/ReportMaster.Master"
    AutoEventWireup="true" CodeBehind="ReportLeaveInfo.aspx.cs" Inherits="LMS.Web.Reports.viewers.ReportLeaveInfo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            preventSubmitOnEnter($("#frmReports"));


            LavelVisible();
            GetEmpInitial();
            OptionWisePageRefresh();
            OptionAvtiveLeaveYear();

            var rptName = $("select[id$=ddlReportName]").val();
            if (rptName == "Leave Availed") {
                hiddenDateDiv();   // Enable Date: 02-Sep-2014
            }
            else if (rptName == "Yearly Leave Status") {
                hiddenDateDiv();
            }

            //LoadEncashableLeaveType();

            $(".dtPicker").datepicker({
                dateFormat: 'dd-mm-yy'
            , changeYear: false
            , showOn: 'button'
            , buttonImage: '../../Content/img/controls/calendar-blue.gif'
            , buttonImageOnly: true
            });

        });

        function hiddenDateDiv() {

            var rptName = $("select[id$=ddlReportName]").val();


            if (rptName == "Leave Availed") {

                $('#lblFromDateReqMark').css('visibility', 'visible');
                $('#lblToDateReqMark').css('visibility', 'visible');
                $('#divDate').css('visibility', 'visible');

                $('#<%=txtFromDate.ClientID %>').removeClass('dateNR');
                $('#<%=txtFromDate.ClientID %>').addClass('date');
                $('#<%=txtFromDate.ClientID %>').addClass('required');

                $('#<%=txtToDate.ClientID %>').removeClass('dateNR');
                $('#<%=txtToDate.ClientID %>').addClass('date');
                $('#<%=txtToDate.ClientID %>').addClass('required');

                //$('#divWithoutPay').css('visibility', 'visible');

                $('#<%=ddlLeaveYear.ClientID %>').val('0');
                $('#<%=ddlLeaveYear.ClientID %>').attr('disabled', 'disabled');
                $('#<%=ddlLeaveYear.ClientID %>').removeClass('required');
                $("#<%=ckbActiveLeaveYear.ClientID%>").attr('checked', false);
                $('#<%=ckbActiveLeaveYear.ClientID %>').attr('disabled', 'disabled');

            }
            else if (rptName == "Yearly Leave Status") {

                $('#<%=ddlLeaveYear.ClientID %>').val('0');
                $('#<%=ddlLeaveYear.ClientID %>').attr('disabled', 'disabled');
                $('#<%=ddlLeaveYear.ClientID %>').removeClass('required');
                $("#<%=ckbActiveLeaveYear.ClientID%>").attr('checked', false);
                $('#<%=ckbActiveLeaveYear.ClientID %>').attr('disabled', 'disabled');
            }
            else {
                $('#<%=txtFromDate.ClientID %>').removeClass('date');
                $('#<%=txtToDate.ClientID %>').removeClass('date');
                $('#<%=txtFromDate.ClientID %>').removeClass('required');
                $('#<%=txtToDate.ClientID %>').removeClass('required');

                $('#lblFromDateReqMark').css('visibility', 'hidden');
                $('#lblToDateReqMark').css('visibility', 'hidden');
                $('#divDate').css('visibility', 'hidden');

                $('#StrFromDate').removeClass('date');
                $('#StrFromDate').addClass("dateNR");

                $('#StrToDate').removeClass('date');
                $('#StrToDate').addClass("dateNR");
                //$('#divWithoutPay').css('visibility', 'hidden');



                $('#<%=ddlLeaveYear.ClientID %>').removeAttr('disabled');
                $('#<%=ddlLeaveYear.ClientID %>').addClass('required');
                $("#<%=ckbActiveLeaveYear.ClientID%>").attr('checked', false);
                $('#<%=ckbActiveLeaveYear.ClientID %>').attr('disabled', false);

            }

            if (rptName == "Leave Encashment") {
                $('#<%=ddlLeaveType.ClientID %>').addClass('required');
                $('#lblleaveType').css('visibility', 'visible');
            }
            else {
                $('#<%=ddlLeaveType.ClientID %>').removeClass('required');
                $('#lblleaveType').css('visibility', 'hidden');

            }

            if (rptName == "Leave Register" || rptName == "Recreation Leave" || rptName == "Office Order For Recreation Leave") {
                $('#<%=ddlZone.ClientID %>').val('0');
                $('#<%=ddlZone.ClientID %>').attr('disabled', 'disabled');
            }
            else {
                $('#<%=ddlZone.ClientID %>').removeAttr('disabled');
            }

            LoadEncashableLeaveType();
        }

        function LavelVisible() {

            var rptName = $("select[id$=ddlReportName]").val();

            if (rptName == "Leave Encashment") {
                $('#<%=ddlLeaveType.ClientID %>').addClass('required');
                $('#lblleaveType').css('visibility', 'visible');
            }
            else {
                $('#<%=ddlLeaveType.ClientID %>').removeClass('required');
                $('#lblleaveType').css('visibility', 'hidden');

            }
        }

        function EnableAvtiveLeaveYear(valActiveLeaveYear) {

            if (valActiveLeaveYear) {
                $('#<%=ddlLeaveYear.ClientID %>').removeClass('required');
                $('#<%=ddlLeaveYear.ClientID %>').val('0');
                $('#<%=ddlLeaveYear.ClientID %>').attr('disabled', 'disabled');
                //$("#<%=ckbActiveLeaveYear.ClientID%>").attr('checked', true);

            }
            else {
                $('#<%=ddlLeaveYear.ClientID %>').removeAttr('disabled');
                $('#<%=ddlLeaveYear.ClientID %>').addClass('required');
                //$("#<%=ckbActiveLeaveYear.ClientID%>").attr('checked', false);
            }

        }

        function LoadEncashableLeaveType() {

            var rptLeaveType = $("select[id$=ddlReportName]").val();

            //alert(rptLeaveType);

            $.ajax({
                type: "POST",
                url: "ReportLeaveInfo.aspx/GetLoadEncashableLeaveType",
                data: '{rptLeaveType: "' + rptLeaveType + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#<%=ddlLeaveType.ClientID %>").empty();
                    $("#<%=ddlLeaveType.ClientID %>").append($("<option></option>").val('').html('...ALL...'));
                    $.each(data.d, function (key, value) {
                        $("#<%=ddlLeaveType.ClientID %>").append($("<option></option>").val(value.intLeaveTypeID).html(value.strLeaveType));
                    });

                },
                failure: function (response) {
                    alert(response.d);
                }
            });

            return false;
        }

        function GetEncashableLeaveTypeWiseYear() {

            var intLeaveType = $("select[id$=ddlLeaveType]").val();
            var rptLeaveType = $("select[id$=ddlReportName]").val();

            if (rptLeaveType == 'Leave Encashment' && intLeaveType != '') {

                $.ajax({
                    type: "POST",
                    url: "ReportLeaveInfo.aspx/GetEncashableLeaveTypeWiseYear",
                    data: '{intLeaveType: "' + intLeaveType + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#<%=ddlLeaveYear.ClientID %>").empty();
                        $("#<%=ddlLeaveYear.ClientID %>").append($("<option></option>").val('').html('...ALL...'));
                        $.each(data.d, function (key, value) {
                            $("#<%=ddlLeaveYear.ClientID %>").append($("<option></option>").val(value.intLeaveYearID).html(value.strYearTitle));
                        });

                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
            else {
                var intLeaveType = 0;
                $.ajax({
                    type: "POST",
                    url: "ReportLeaveInfo.aspx/GetEncashableLeaveTypeWiseYear",
                    data: '{intLeaveType: "' + intLeaveType + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#<%=ddlLeaveYear.ClientID %>").empty();
                        $("#<%=ddlLeaveYear.ClientID %>").append($("<option></option>").val('').html('...ALL...'));
                        $.each(data.d, function (key, value) {
                            $("#<%=ddlLeaveYear.ClientID %>").append($("<option></option>").val(value.intLeaveYearID).html(value.strYearTitle));
                        });

                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }

        }


        function GetLeaveYearFromToDate() {

            var intLeaveYearID = $("#<%=ddlLeaveYear.ClientID %>").val();

            $.ajax({
                type: "POST",
                url: "ReportLeaveInfo.aspx/GetLeaveYearFromToDate",
                data: '{intLeaveYearID: "' + intLeaveYearID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    $.each(data.d, function (key, value) {

                        $("#<%=txtFromDate.ClientID %>").val(value.strStartDate);
                        $("#<%=txtToDate.ClientID %>").val(value.strEndDate);

                    });

                },
                failure: function (response) {
                    alert(response.d);
                }
            });

        }



        function OptionWisePageRefresh() {

            $(function () {
                var val = $("#<%=rblIsIndividual.ClientID%>").find(":checked").val();

                if (val == 1) {

                    $("#trStatus").hide();

                    $('#<%=ddlDepartment.ClientID %>').val('0');
                    $('#<%=ddlDepartment.ClientID %>').attr('disabled', 'disabled');

                    $('#<%=ddlDesignation.ClientID %>').val('0');
                    $('#<%=ddlDesignation.ClientID %>').attr('disabled', 'disabled');

                    $('#<%=ddlCategory.ClientID %>').val('0');
                    $('#<%=ddlCategory.ClientID %>').attr('disabled', 'disabled');

                    $('#<%=ddlGender.ClientID %>').val('0');
                    $('#<%=ddlGender.ClientID %>').attr('disabled', 'disabled');

                    $('#<%=txtEmpInitial.ClientID %>').removeAttr('disabled');
                   
                    $("#lblInitial").css('visibility', 'visible');
                    $('#<%=txtEmpInitial.ClientID %>').addClass('required');

                    $('#btnElipse').css('visibility', 'visible');

                }
                else {

                    $("#trStatus").show();

                    $('#<%=ddlDepartment.ClientID %>').removeAttr('disabled');
                    $('#<%=ddlDesignation.ClientID %>').removeAttr('disabled');
                    $('#<%=ddlCategory.ClientID %>').removeAttr('disabled');
                    $('#<%=ddlGender.ClientID %>').removeAttr('disabled');

                    $('#<%=txtEmpInitial.ClientID %>').attr('disabled', 'disabled');
                    $('#<%=txtEmpInitial.ClientID %>').val('');

                    $("#lblInitial").css('visibility', 'hidden');
                    $('#<%=txtEmpInitial.ClientID %>').removeClass('required');

                    $('#btnElipse').css('visibility', 'hidden');

                }
            });
        }

        function reportPreview() {

            //            if (fnValidateDateTime() == false) {
            //                alert("Invalid Date.");
            //                return false;
            //            }

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
    <script type="text/javascript">


        function GetEmpInitial() {

            $("#<%= txtEmpInitial.ClientID%>").autocomplete("LMSAutocomplete.aspx?SearchBy=empInitial", {
                width: 200,
                selectFirst: true
            });

            $("#<%= txtEmpInitial.ClientID%>").result(function (event, data, formatted) {
                if (data) {
                    document.getElementById('<%= txtHiddenEmpInitial.ClientID%>').value = data[0];
                }
                else {
                    document.getElementById('<%= txtHiddenEmpInitial.ClientID%>').value = '';
                    document.getElementById('<%= txtEmpInitial.ClientID%>').value = '';
                }

            });

        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ReportMain" runat="server">

<h3 class="page-title">Reports</h3>

    <div class="divSpacer">
    </div>
    <div class="divRow">
        <table class="contenttext" style="width: 100%;">
            <tr>
                <td>
                    Report Name<label class="labelRequired">*</label>
                </td>
                <td>
                    <div style="float: left; text-align: left;">
                        <asp:DropDownList ID="ddlReportName" runat="server" class="selectBoxRegular required"
                            onchange="return hiddenDateDiv();">
                        </asp:DropDownList>
                    </div>
                    <%-- <div id="divWithoutPay" style="float: left; text-align: left; padding-left: 8px;
                        visibility: hidden;">
                        <asp:CheckBox ID="cbIsWithoutPay" runat="server" />
                        Without Pay
                    </div>--%>
                </td>
            </tr>
            <tr>
                <td>
                    Leave Type
                    <label id="lblleaveType" class="labelRequired">
                        *</label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLeaveType" runat="server" class="selectBoxRegular" onchange="return GetEncashableLeaveTypeWiseYear();">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Leave Year<label id="lblLeaveYear" class="labelRequired">*</label>
                </td>
                <td>
                    <%--                    <asp:DropDownList ID="ddlLeaveYear" runat="server" class="selectBoxRegular required"
                        onchange="return GetLeaveYearFromToDate();">--%>
                    <asp:DropDownList ID="ddlLeaveYear" runat="server" class="selectBoxRegular required">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ckbActiveLeaveYear" runat="server" Text="Active Leave Year" Checked="true"
                        onchange="OptionAvtiveLeaveYear();" />
                </td>
            </tr>
            <tr id="trStatus">
                <td>
                    Status
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmpStatus" runat="server" class="selectBoxRegular">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButton ID="rdIsYearlyLeave" runat="server" Checked="True" GroupName="LeaveStatus"
                            Text="Yearly leave" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdIsServiceTypeLeave" runat="server" GroupName="LeaveStatus"
                            Text="Service Type Leave" />
                    </td>
                </tr>
        </table>
        <div class="divSpacer">
        </div>
        <div id="divDate" class="divRow" style="visibility: hidden;">
            <table class="contenttext" style="width: 100%;">
                <tr>
                    <td style="width: 36%;">
                        <div>
                            <asp:RadioButtonList ID="rbApplyDate" runat="server" class="tblApplyDate" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Apply Date" Value="1" Selected="True" />
                                <asp:ListItem Text="Leave Date" Value="2" />
                            </asp:RadioButtonList>
                        </div>
                    </td>
                    <td colspan="3">
                        <div style="width: 100%; float: left; text-align: left;">
                            <div style="float: left; text-align: left;">
                                From Date<label id="lblFromDateReqMark" style="visibility: hidden" class="labelRequired">*</label>
                            </div>
                            <div style="float: left; text-align: left; padding-left: 8px;">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textRegularDate dtPicker dateNR"
                                    MaxLength="10" Width="85px"></asp:TextBox>
                                To Date<label id="lblToDateReqMark" style="visibility: hidden" class="labelRequired">*</label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textRegularDate dtPicker dateNR"
                                    MaxLength="10" Width="85px"></asp:TextBox>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table class="contenttext" style="width: 100%;">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rblIsIndividual" runat="server" RepeatDirection="Horizontal"
                            class="tbl" onchange="OptionWisePageRefresh();">
                            <asp:ListItem Text="Individual" Value="1"></asp:ListItem>
                            <asp:ListItem Text="All" Value="2" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Applicant ID<label id="lblInitial" style="visibility: hidden" class="labelRequired">*</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmpInitial"  runat="server" class="selectBoxRegular" onfocus="return GetEmpInitial();" />
                        <asp:TextBox ID="txtHiddenEmpInitial" runat="server" Style="visibility: hidden; display: none;"></asp:TextBox>
                    </td>
                    <td>
                        Department
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" class="selectBoxRegular">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Zone
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlZone" runat="server" class="selectBoxRegular">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Designation
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDesignation" runat="server" class="selectBoxRegular">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        Category
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategory" runat="server" class="selectBoxRegular">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        Gender
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGender" runat="server" class="selectBoxRegular">
                        </asp:DropDownList>
                    </td>
                </tr>
               
            </table>
        </div>
    </div>
    <div class="divButton">
        <asp:Button ID="btnViewReport" runat="server" class="btnPreview" OnClientClick="return reportPreview();"
            OnClick="btnViewReport_Click" />
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
