<%@ Page Language="C#" Title="Login" MasterPageFile="~/Views/Shared/Logon.Master"
    Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.LogOnModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        onload = function () {
            document.getElementById("UserName").focus();
        }


        function DoSubmit() {

            if (IsValidBrowser()) {

                document.aspnetForm.submit();
            }
        }


        document.onkeydown = function (event) {
            
            if (event.keyCode == 13) {
                DoSubmit();
            }
        }


        function createCookie(name, value, days) {
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                var expires = "; expires=" + date.toGMTString();
            }
            else var expires = "";
            document.cookie = name + "=" + value + expires + "; path=/";
        }


        function getInternetExplorerVersion() {

            var rv = -1; // Return value assumes failure.

            if (navigator.appName == 'Microsoft Internet Explorer') {

                var ua = navigator.userAgent;

                var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");

                if (re.exec(ua) != null)

                    rv = parseFloat(RegExp.$1);

            }

            return rv;

        }


        function IsValidBrowser() {
            var nVer = navigator.appVersion;
            var nAgt = navigator.userAgent;
            var browserName = navigator.appName;
            var fullVersion = '' + parseFloat(navigator.appVersion);
            var majorVersion = parseInt(navigator.appVersion, 10);
            var nameOffset, verOffset, ix;

            // In MSIE, the true version is after "MSIE" in userAgent
            if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
                browserName = "Microsoft Internet Explorer";
                fullVersion = nAgt.substring(verOffset + 5);
            }
            //        // In Opera, the true version is after "Opera" 
            //        else if ((verOffset = nAgt.indexOf("Opera")) != -1) {
            //            browserName = "Opera";
            //            fullVersion = nAgt.substring(verOffset + 6);
            //        }
            //        // In Chrome, the true version is after "Chrome" 
            //        else if ((verOffset = nAgt.indexOf("Chrome")) != -1) {
            //            browserName = "Chrome";
            //            fullVersion = nAgt.substring(verOffset + 7);
            //        }
            //        // In Safari, the true version is after "Safari" 
            //        else if ((verOffset = nAgt.indexOf("Safari")) != -1) {
            //            browserName = "Safari";
            //            fullVersion = nAgt.substring(verOffset + 7);
            //        }

            // In Firefox, the true version is after "Firefox" 
            else if ((verOffset = nAgt.indexOf("Firefox")) != -1) {
                browserName = "Firefox";
                fullVersion = nAgt.substring(verOffset + 8);
            }

            // In most other browsers, "name/version" is at the end of userAgent 
            else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) < (verOffset = nAgt.lastIndexOf('/'))) {
                browserName = nAgt.substring(nameOffset, verOffset);
                fullVersion = nAgt.substring(verOffset + 1);
                if (browserName.toLowerCase() == browserName.toUpperCase()) {
                    browserName = navigator.appName;
                }
            }

            // trim the fullVersion string at semicolon/space if present
            if ((ix = fullVersion.indexOf(";")) != -1) fullVersion = fullVersion.substring(0, ix);
            if ((ix = fullVersion.indexOf(" ")) != -1) fullVersion = fullVersion.substring(0, ix);

            majorVersion = parseInt('' + fullVersion, 10);
            if (isNaN(majorVersion)) {
                fullVersion = '' + parseFloat(navigator.appVersion);
                majorVersion = parseInt(navigator.appVersion, 10);
            }

            if (browserName == "Microsoft Internet Explorer") {

                var ver = getInternetExplorerVersion();

                if (ver > -1) {
                    if (ver >= 7)
                        return true;
                    else
                        document.getElementById('dvDownloadBrowser').style.display = "block"
                    return false;
                }

            }
            else if (browserName == "Firefox") {
                //alert(parseFloat(fullVersion));
                if (parseFloat(fullVersion) >= 3.5) {
                    return true;
                }
                else {
                    document.getElementById('dvDownloadBrowser').style.display = "block"
                    return false;
                }
            }

        }
   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLogon" runat="Server">
    <div id="dvDownloadBrowser" style="text-align: center; display: none; padding-top: 20px;
        padding-left: 50px;">
        <label style="color: White;">
            Browser version you are using is not supported by Leave Management System (LMS).<br />
            Supported browsers versions are IE 7.0 & Mozilla 3.6 and above.</label>
    </div>
    <center>
        <% using (Html.BeginForm())
           {
               if (Model.CompanyList != null)
               {
                   if (Model.CompanyList.Count > 1)
                   {%>
        <%=Html.Hidden("Model.UserName",Model.UserName) %>
        <%=Html.Hidden("Model.RememberMe", Model.RememberMe)%>
        <%=Html.Hidden("Model.ReturnUrl", Model.ReturnUrl)%>
        <%=Html.Hidden("Model.IsFromCompany", Model.IsFromCompany)%>
        <div id="divDataList">
            <table style="width: 100%">
                <tr>
                    <th>
                        Company Id
                    </th>
                    <th>
                        Company Name
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
                <% foreach (var item in Model.CompanyList)
                   { %>
<%--                <tr>
                    <td>
                        <%= Html.Encode(item.CompanyID)%>
                    </td>
                    <td>
                        <%= Html.Encode(item.CompanyName)%>
                    </td>
                    <td>
                        <a href="#" onclick="DoSubmit('<%= item.CompanyID%>');">Log on</a>
                    </td>
                </tr>--%>
                <%}
                %>
            </table>
        </div>
        <%}
               }
               else
               {%>
        <div id="loginBox">
            <div id="loginHeader">
                <h1 style="text-align: center; font-size: xx-large; color: #474747; padding-top: 5px;">
                    Login
                </h1>
            </div>
            <div style="height: 235px">
                <table style="width: 100%" class="contenttable">
                    <tr>
                        <td class="contenttabletdlarge">
                            <div id="loginControls" style="height: 235px;">
                                <table style="margin: 10px 0px 0px 10px;" class="contenttable">
                                    <tr>
                                        <td style="width: 60%" class="contenttabletdlarge">
                                            Login Id
                                        </td>
                                        <td class="contenttabletdlarge">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletdlarge">
                                            <%= Html.TextBoxFor(m => m.UserName,new { @style="width:150px;"})%>
                                        </td>
                                        <td class="contenttabletdlarge">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletdlarge">
                                            Password
                                        </td>
                                        <td class="contenttabletdlarge">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletdlarge">
                                           <%-- <%= Html.PasswordFor(m => m.Password, new { @style="width:150px;"})%>--%>
                                            <%= Html.Password("Password", Model.Password, new { @style = "width:150px;" })%>
                                         
                                        </td>
                                        <td class="contenttabletdlarge">
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="contenttabletdlarge">
                                            <%= Html.CheckBoxFor(m => m.RememberMe)%>
                                            <label  for="RememberMe">Remember me?</label>

                                          <%--  <%= Html.HiddenFor(m => m.RememberMe)%>--%>
                                        </td>
                                        <td class="contenttabletdlarge">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="contenttabletdlarge" align="right">
                                            <div style="width: 20%; float: left">
                                                <input id="btnLogin" name="btnLogin" type="submit" value="" style="visibility: hidden;" />
                                            </div>
                                            <div style="width: 80%; float: left">
                                                <a href="#" onclick="DoSubmit();" style="padding-left: 16px; text-decoration: none;"
                                                    hidefocus="hidefocus">
                                                    <img src="<%= Url.Content("~/Content/img/controls/btn_login.gif") %>" />
                                                </a>
                                            </div>
                                        </td>
                                        <td class="contenttabletdlarge">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="contenttabletdlarge" >
                                            <%--<%= Html.ValidationSummary("Login was unsuccessful.")%>--%>
                                            <%= Html.ValidationSummary("")%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td class="contenttabletdlarge">
                            <img style="padding-top: 10px; width: 150px; height: 150px" src="<%= Url.Content("~/Content/img/login.jpg") %>" />
                        </td>
                    </tr>
                </table>
                <div id="loginLogo">
                    <div style="padding-top: 12px;">
                        Copyright © <a href="http://bepza.gov.bd"><span style="color: #ff0000;font-weight: bold;">BEPZA</span></a> , Maintained By BEPZA MIS
                    </div>
                </div>
            </div>
            <%--<div id="loginFooter">
            </div>--%>
        </div>
    </center>
    <%}%>
    <%} %>
</asp:Content>
