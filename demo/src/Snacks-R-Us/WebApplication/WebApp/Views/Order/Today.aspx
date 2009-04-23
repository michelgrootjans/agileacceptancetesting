<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<ViewOrdersDto>" %>
<%@ Import Namespace="Snacks_R_Us.Domain.DataTransfer"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Today's orders
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Orders placed today</h2>
    <% Html.RenderPartial("Order", Model); %>

</asp:Content>

