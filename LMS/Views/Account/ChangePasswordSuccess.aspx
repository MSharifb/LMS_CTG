<%@  Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="changePasswordSuccessContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <div id="divDataList">
         <table style="width:70%;height:70%">
        <tr>
            <td>
                <h2>
                    Change Password
                </h2>
                <p>
                    <%--Your password has been changed successfully.--%>
                    <label  style='font-size:10pt;font-weight:bold;color:Green;'>Your password has been changed successfully.</label>
                </p>
                <p>
                    <%=Session["IsFirstLogin"] = "No"%>
                    <%=Html.ActionLink("Go to Main Window", "Index", "Home") %>
                </p>
            </td>
        </tr>
    </table>
    </div>
   
</asp:Content>
