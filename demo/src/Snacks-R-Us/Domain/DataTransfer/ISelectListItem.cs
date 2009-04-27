using System;
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
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> items, Func<T, string> getValue, Func<T, string> getText)
        {
            foreach (var item in items)
                yield return new SelectListItem {Value = getValue(item), Text = getText(item)};
        }
    }
}