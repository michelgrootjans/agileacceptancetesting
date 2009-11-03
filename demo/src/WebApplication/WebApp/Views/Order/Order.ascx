<%@ Control Language="C#" Inherits="ViewUserControl<ViewOrdersDto>" %>
<%@ Import Namespace="Snacks_R_Us.Domain.DataTransfer"%>

    <table>
    <thead>
    <tr>
        <th>Qty</th>
        <th>Snack</th>
        <th>Unit</th>
        <th>Total</th>
    </tr>
    </thead>
    <tbody>
    <% foreach (var order in Model) {%>
    <tr>
        <td><%= order.Qty %></td>
        <td><%= order.Snack %></td>
        <td>€ <%= order.UnitPrice %></td>
        <td>€ <%= order.TotalPrice %></td>
    </tr>
 <% } %>
    <tr class="total">
        <td colspan="3">Total</td>
        <td>€ <%= Model.Total %></td>
    </tr> 
    </tbody>
    </table>
