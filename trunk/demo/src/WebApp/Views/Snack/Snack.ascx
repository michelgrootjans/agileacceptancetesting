<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SnackDto>" %>
<%@ Import Namespace="Snacks_R_Us.Domain.DataTransfer"%>

<%= Html.Hidden(Model.Id)%>

<%= Model.Name%>
€ <%= Model.Price%>