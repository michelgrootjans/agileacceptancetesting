<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MyOrdersDto>" %>
<%@ Import Namespace="Snacks_R_Us.Domain.DataTransfer"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MyOrders
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MyOrders</h2>
    <% foreach (var order in Model.Orders) { %>
        <% Html.RenderPartial("Order", order); %>
    <% } %>

    <h2>Order new snack</h2>
    <% using (Html.BeginForm("Order", "Order")){ %>
        <%= Html.DropDownList("SnackId", Model.Snacks) %>
        <%= Html.TextBox("Qty", "1") %>
        <input type="submit" value="Plaats bestelling" />
    <% } %>
</asp:Content>
