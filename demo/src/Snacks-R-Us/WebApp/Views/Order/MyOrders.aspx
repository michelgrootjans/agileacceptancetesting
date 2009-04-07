<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MyOrdersDto>" %>
<%@ Import Namespace="Snacks_R_Us.Domain.DataTransfer"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MyOrders
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Order a new snack</h2>
    <% using (Html.BeginForm("Order", "Order")){ %>
        <%= Html.TextBox("Qty", "1") %>
        <%= Html.DropDownList("SnackId", Model.Snacks) %>
        <input type="submit" value="Save order" />
    <% } %>

    <h2>Current Orders</h2>
    <% foreach (var order in Model.Orders) { %>
        <% Html.RenderPartial("Order", order); %> <br />
    <% } %>

</asp:Content>
