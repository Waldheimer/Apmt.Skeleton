using appointmenting.Linq.CriteriaBuilder.Builders.Abstractions;
using appointmenting.Linq.CriteriaBuilder.Enums;
using appointmenting.Linq.CriteriaBuilder.Expressions;
using System.Linq.Expressions;

namespace appointmenting.Linq.CriteriaBuilder.Extensions;

public static class OrderByCriteriaBuilderExtensions
{
    public static IOrderByCriteriaBuilder<T> ThenBy<T>(this IOrderByCriteriaBuilder<T> builder,
        Expression<Func<T, object>> orderExpression)
        where T : class
    {
        builder.Query.OrderByExpressions.Add(new OrderByExpressionDescriptor<T>(orderExpression, OrderByType.ThenBy));
        return builder;
    }

    public static IOrderByCriteriaBuilder<T> ThenByDescending<T>(this IOrderByCriteriaBuilder<T> builder,
        Expression<Func<T, object>> orderExpression)
        where T : class
    {
        builder.Query.OrderByExpressions.Add(new OrderByExpressionDescriptor<T>(orderExpression, OrderByType.ThenByDescending));
        return builder;
    }
}