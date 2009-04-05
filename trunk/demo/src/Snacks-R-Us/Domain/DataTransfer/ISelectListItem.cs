using System.Collections.Generic;
using System.Web.Mvc;

namespace Snacks_R_Us.Domain.DataTransfer
{
    public interface ISelectListItem
    {
        string Value { get; }
        string Text { get; }
    }

    public static class ISelectListItemExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> items)
            where T : ISelectListItem
        {
            foreach (var item in items)
                yield return new SelectListItem {Value = item.Value, Text = item.Text};
        }
    }
}