<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Snacks_R_Us.Domain.DataTransfer.OrderDto>" %>

<%= Model.Qty %> x <%= Model.SnackName %> (<% =Html.Encode(String.Format("{0:F}", Model.UnitPrice)) %> €) = <%= Html.Encode(String.Format("{0:F}", Model.TotalPrice)) %> €