<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {

            var myleavemenu = $("#firstmenuUrl").val();            
            if (myleavemenu != null) {
                
                window.parent.location = myleavemenu;
            }


        });
    
    </script>
    <div id="divDataList">
    </div>
     
    </asp:Content>
