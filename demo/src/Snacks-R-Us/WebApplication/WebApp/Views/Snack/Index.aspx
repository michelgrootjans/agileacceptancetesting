<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Snacks_R_Us.Domain.DataTransfer.SnackDto>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>


    <% foreach (var item in Model) { %>
        <% Html.RenderPartial("Snack", item); %>
        <br />
    <% } %>

</asp:Content>

