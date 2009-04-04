using System;
using System.Linq.Expressions;

namespace Snacks_R_Us.WebApp.Repositories
{
    public interface IQueryExpression<T>
    {
        Expression<Func<object, bool>> Expression { get; }
    }
}