<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Snacks_R_Us.WebApp.Models.CreateOrderDto>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	New
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>New</h2>

    <%= Html.ValidationSummary("Order was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Order", "Order")) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Qty">Qty:</label>
                <%= Html.TextBox("Qty") %>
                <%= Html.ValidationMessage("Qty", "*") %>
            </p>
            <p>
                <label for="SnackName">SnackName:</label>
                <%= Html.TextBox("SnackName") %>
                <%= Html.ValidationMessage("SnackName", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

</asp:Content>

