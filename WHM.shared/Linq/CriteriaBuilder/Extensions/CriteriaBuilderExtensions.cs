using appointmenting.Linq.CriteriaBuilder.Builders;
using appointmenting.Linq.CriteriaBuilder.Builders.Abstractions;
using appointmenting.Linq.CriteriaBuilder.Enums;
using appointmenting.Linq.CriteriaBuilder.Expressions;
using System.Linq.Expressions;

namespace appointmenting.Linq.CriteriaBuilder.Extensions;

public static class CriteriaBuilderExtensions
{
    public static ICriteriaBuilder<T> Where<T>(this ICriteriaBuilder<T> builder, Expression<Func<T, bool>> predicate)
        where T : class
    {
        builder.Query.Where = predicate;

        return builder;
    }

    public static IIncludeCriteriaBuilder<T, TProperty> Include<T, TProperty>(this ICriteriaBuilder<T> builder,
        Expression<Func<T, TProperty>> includeExpression) where T : class
    {
        var info = new IncludeExpressionDescriptor(includeExpression, typeof(T), typeof(TProperty));
        builder.Query.IncludeExpressions.Add(info);

        var includeBuilder = new IncludeCriteriaBuilder<T, TProperty>(builder.Query);

        return includeBuilder;
    }

    public static ICriteriaBuilder<T> Include<T>(this ICriteriaBuilder<T> builder, string includeString)
        where T : class
    {
        builder.Query.IncludeStrings.Add(includeString);
        return builder;
    }

    public static ICriteriaBuilder<T> OrderBy<T>(this ICriteriaBuilder<T> builder, string orderBy = null!)
        where T : class
    {
        builder.Query.OrderByStrings.Add(new OrderByStringExpressionDescriptor { Fieldname = orderBy, OrderByType = OrderByStringType.Ascending });
        return builder;
    }

    public static ICriteriaBuilder<T> OrderBy<T>(this ICriteriaBuilder<T> builder, OrderByStringExpressionDescriptor orderBy = null!)
    where T : class
    {
        builder.Query.OrderByStrings.Add(orderBy);
        return builder;
    }

    public static ICriteriaBuilder<T> OrderBy<T>(this ICriteriaBuilder<T> builder, List<OrderByStringExpressionDescriptor> orderBy = null!)
        where T : class
    {
        builder.Query.OrderByStrings.AddRange(orderBy);
        return builder;
    }

    public static IOrderByCriteriaBuilder<T> OrderBy<T>(this ICriteriaBuilder<T> builder,
        Expression<Func<T, object>> orderExpression)
        where T : class
    {
        builder.Query.OrderByExpressions.Add(new OrderByExpressionDescriptor<T>(orderExpression, OrderByType.OrderBy));
        var orderByBuilder = new OrderByCriteriaBuilder<T>(builder.Query);

        return orderByBuilder;
    }

    public static ICriteriaBuilder<T> OrderByDescending<T>(this ICriteriaBuilder<T> builder, string orderBy = null!)
        where T : class
    {
        builder.Query.OrderByStrings.Add(new OrderByStringExpressionDescriptor { Fieldname = orderBy, OrderByType = OrderByStringType.Descending });
        return builder;
    }

    public static IOrderByCriteriaBuilder<T> OrderByDescending<T>(this ICriteriaBuilder<T> builder,
        Expression<Func<T, object>> orderExpression)
        where T : class
    {
        builder.Query.OrderByExpressions.Add(new OrderByExpressionDescriptor<T>(orderExpression, OrderByType.OrderByDescending));
        var orderByBuilder = new OrderByCriteriaBuilder<T>(builder.Query);

        return orderByBuilder;
    }

    public static ICriteriaBuilder<T> Take<T>(this ICriteriaBuilder<T> builder, int take)
        where T : class
    {
        builder.Query.Take = take;

        return builder;
    }

    public static ICriteriaBuilder<T> RemoveTake<T>(this ICriteriaBuilder<T> builder)
        where T : class
    {
        builder.Query.Take = null;

        return builder;
    }

    public static ICriteriaBuilder<T> Skip<T>(this ICriteriaBuilder<T> builder, int skip)
            where T : class
    {
        builder.Query.Skip = skip;

        return builder;
    }

    public static ICriteriaBuilder<T> RemoveSkip<T>(this ICriteriaBuilder<T> builder)
        where T : class
    {
        builder.Query.Skip = null;

        return builder;
    }

    public static ICriteriaBuilder<T> AsNoTracking<T>(this ICriteriaBuilder<T> builder)
        where T : class
    {
        builder.Query.NoTracking = true;
        builder.Query.Tracking = false;
        return builder;
    }

    public static ICriteriaBuilder<T> AsTracking<T>(this ICriteriaBuilder<T> builder)
        where T : class
    {
        builder.Query.NoTracking = false;
        builder.Query.Tracking = true;
        return builder;
    }

    public static ICriteriaBuilder<T> AsSplitQuery<T>(this ICriteriaBuilder<T> builder)
        where T : class
    {
        builder.Query.SplitQuery = true;
        return builder;
    }

    public static ICriteriaBuilder<T> IgnoreQueryFilters<T>(this ICriteriaBuilder<T> builder)
        where T : class
    {
        builder.Query.IgnoreQueryFilters = true;
        return builder;
    }

    public static ICriteriaBuilder<T> WithTag<T>(this ICriteriaBuilder<T> builder, string tag)
        where T : class
    {
        builder.Query.TagWith = tag;
        return builder;
    }
}
