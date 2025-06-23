using appointmenting.Linq.CriteriaBuilder.Builders;
using appointmenting.Linq.CriteriaBuilder.Builders.Abstractions;
using appointmenting.Linq.CriteriaBuilder.Expressions;
using System.Linq.Expressions;

namespace appointmenting.Linq.CriteriaBuilder.Extensions;

public static class IncludeCriteriaBuilderExtensions
{
    public static IIncludeCriteriaBuilder<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(
        this IIncludeCriteriaBuilder<T, TPreviousProperty> builder,
        Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression)
        where T : class
    {
        var descriptor = new IncludeExpressionDescriptor(thenIncludeExpression, typeof(T), typeof(TProperty), typeof(TPreviousProperty));
        builder.Query.IncludeExpressions.Add(descriptor);
        var includeBuilder = new IncludeCriteriaBuilder<T, TProperty>(builder.Query);

        return includeBuilder;
    }

    public static IIncludeCriteriaBuilder<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(
        this IIncludeCriteriaBuilder<T, IEnumerable<TPreviousProperty>> builder,
        Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression)
        where T : class
    {
        var descriptor = new IncludeExpressionDescriptor(thenIncludeExpression, typeof(T), typeof(TProperty), typeof(IEnumerable<TPreviousProperty>));
        builder.Query.IncludeExpressions.Add(descriptor);
        var includeBuilder = new IncludeCriteriaBuilder<T, TProperty>(builder.Query);

        return includeBuilder;
    }
}
