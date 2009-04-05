<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Snacks_R_Us.Domain.DataTransfer.OrderDto>" %>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Qty:
            <%= Model.Qty %>
        </p>
        <p>
            Snack:
            <%= Model.SnackName %>
        </p>
        <p>
            UnitPrice:
            <%= Html.Encode(String.Format("{0:F}", Model.UnitPrice)) %>
        </p>
        <p>
            TotalPrice:
            <%= Html.Encode(String.Format("{0:F}", Model.TotalPrice)) %>
        </p>
    </fieldset>