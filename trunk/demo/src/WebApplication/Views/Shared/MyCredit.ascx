<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Snacks_R_Us.Domain.Services"%>

<%
    if (Request.IsAuthenticated) 
    {
%>
        You have <em>€ <%= Current.Credits %></em> in your account
<%
    }
%>


