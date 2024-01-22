<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Controllers.CustomerEditViewModel>" %>

<%@ Import Namespace="LMS.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=Model.IsAdd ? "New Customer" : "Edit Customer"%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Model.IsAdd ? "New Customer" : "Edit Customer" %></h2>
    <% if (Model.HasErrors)
       {%>
    <span class="validation-summary-errors">Save was unsuccessful. Please correct the errors
        and try again. </span>
    <% } %>
    <% using (Html.BeginForm(Model.IsAdd ? "AddSave" : "EditSave", "Customer", new { customerID = Model.Customer.ID }))
       {%>
    <fieldset>
        <legend>Fields</legend>
        <p>
            <label for="ID">
                ID:</label>
            <%= Html.TextBox("Customer.ID", Model.Customer.ID == 0 ? "" : Model.Customer.ID.ToString()) %>
            <%= Html.ValidationMessage("Customer.ID")%>
        </p>
        <p>
            <label for="FirstName">
                First Name:</label>
            <%= Html.TextBox("Customer.FirstName", Model.Customer.FirstName)%>
            <%= Html.ValidationMessage("Customer.FirstName")%>
        </p>
        <p>
            <label for="LastName">
                Last Name:</label>
            <%= Html.TextBox("Customer.LastName", Model.Customer.LastName)%>
            <%= Html.ValidationMessage("Customer.LastName")%>
        </p>
        <p>
            <label for="Phone">
                Phone:</label>
            <%= Html.TextBox("Customer.Phone", Model.Customer.Phone)%>
            <%= Html.ValidationMessage("Customer.Phone")%>
        </p>
        <p>
            <label for="Email">
                Email:</label>
            <%= Html.TextBox("Customer.Email", Model.Customer.Email)%>
            <%= Html.ValidationMessage("Customer.Email")%>
        </p>
        <p>
            <label for="Email">
                Orders Placed:</label>
            <%= Html.TextBox("Customer.OrdersPlaced", Model.Customer.OrdersPlaced)%>
            <%= Html.ValidationMessage("Customer.OrdersPlaced")%>
        </p>
        <p>
            <label for="Email">
                Data of Last Order:</label>
            <%= Html.TextBox("Customer.DateOfLastOrder", StringFormatter.FormatDate(Model.Customer.DateOfLastOrder))%>
            <%= Html.ValidationMessage("Customer.DateOfLastOrder")%>
        </p>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
