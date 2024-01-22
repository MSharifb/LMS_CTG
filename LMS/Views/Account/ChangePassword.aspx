<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.ChangePasswordModel>" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="head" runat="server">  
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) { %>
     <div id="divDataList">
 
    <table width="80%" class="contenttable">
        <tr>
            <td style="padding-left:20px;" class="contenttabletd">
                <h2>Change Password</h2>
                <p>
                    Use the form below to change your password. 
                </p>
                <p>
                    New passwords are required to be a minimum of <%= Html.Encode(ViewData["PasswordLength"]) %> characters in length.
                </p>

                <%= Html.ValidationSummary("Password change was unsuccessful. Please correct the errors and try again.") %>
                <div>
                    <fieldset>
                <legend>Account Information</legend>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.OldPassword) %>
                </div>
                <div class="editor-field">
                   <%-- <%= Html.PasswordFor(m => m.OldPassword) %>--%>
                     <%= Html.Password("OldPassword", Model.OldPassword, new { @class = "", @minLength = "6", @maxlength = "64" })%>
                   <%-- <%= Html.ValidationMessage("Model.OldPassword") %>--%>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.NewPassword) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.NewPassword) %>
                  
                  <%--  <%= Html.ValidationMessageFor(m => m.NewPassword) %>--%>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                  <%--  <%= Html.PasswordFor(m => m.ConfirmPassword) %>--%>
                    <%= Html.Password("ConfirmPassword", Model.OldPassword, new { @class = "", @minLength = "6", @maxlength = "64" })%>
                   <%-- <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>--%>
                </div>
                
                <p>
                    <input type="submit" value="Change Password" />                  
                                    
                </p>
                <div>
                <%if (LMS.Web.LoginInfo.Current.ShowMenus == false)
                      { %>
                            <%=Html.ActionLink("Go Back", "LogOff", "Account")%>
                    <%} %>
                </div>

            </fieldset>
                </div>
            </td>
        </tr>
    </table>
       </div>
    <% } %>
</asp:Content>
