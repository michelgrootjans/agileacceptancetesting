<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Snacks_R_Us.Domain.DataTransfer.ViewUserDto>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>List</h2>

    <table>
        <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Credit</th>
        </tr>

    <% foreach (var userDto in Model) { %>
    
        <tr>
            <td><%= Html.Encode(userDto.Name) %></td>
            <td><%= Html.Encode(userDto.Email) %></td>
            <td>
                € <%= Html.Encode(userDto.CreditAmount) %>
                <%= Html.ActionLink("Add...", "Edit", "Credit", new { id = userDto.Id }, null) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Register", "Account", null, null) %>
    </p>

</asp:Content>

