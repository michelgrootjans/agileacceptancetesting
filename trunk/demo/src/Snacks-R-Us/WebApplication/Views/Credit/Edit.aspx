<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<ViewCreditDto>" %>
<%@ Import Namespace="Snacks_R_Us.Domain.DataTransfer"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add credit for <%= Model.UserName %></h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Update", "Credit")) {%>

        <%= Html.Hidden("userId", Model.UserId) %>
        <fieldset>
            <p>
                Current credit: € <%= Model.CreditAmount %>
            </p>
            <p>
                <label for="Amount">Add:</label>
                <%= Html.TextBox("Amount", String.Format("{0:F}", 0)) %>
                <%= Html.ValidationMessage("Amount", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

</asp:Content>

