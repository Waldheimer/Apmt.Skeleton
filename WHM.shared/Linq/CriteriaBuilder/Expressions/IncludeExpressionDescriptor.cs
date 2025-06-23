using appointmenting.Linq.CriteriaBuilder.Enums;
using System.Linq.Expressions;

namespace appointmenting.Linq.CriteriaBuilder.Expressions;

public class IncludeExpressionDescriptor
{
    public LambdaExpression LambdaExpression { get; }

    public Type EntityType { get; }

    public Type PropertyType { get; }

    public Type PreviousPropertyType { get; }

    public IncludeType Type { get; }

    public IncludeExpressionDescriptor(LambdaExpression expression, Type entityType, Type propertyType)
        : this(expression, entityType, propertyType, null!, IncludeType.Include) { }

    public IncludeExpressionDescriptor(LambdaExpression expression, Type entityType, Type propertyType, Type previousPropertyType)
        : this(expression, entityType, propertyType, previousPropertyType, IncludeType.ThenInclude) { }

    private IncludeExpressionDescriptor(LambdaExpression expression, Type entityType, Type propertyType,
        Type previousPropertyType, IncludeType includeType)

    {
        ArgumentNullException.ThrowIfNull(expression, nameof(expression));
        ArgumentNullException.ThrowIfNull(entityType, nameof(entityType));
        ArgumentNullException.ThrowIfNull(propertyType, nameof(propertyType));

        if (includeType == IncludeType.ThenInclude)
            ArgumentNullException.ThrowIfNull(propertyType, nameof(previousPropertyType));

        LambdaExpression = expression;
        EntityType = entityType;
        PropertyType = propertyType;
        PreviousPropertyType = previousPropertyType;
        Type = includeType;
    }
}
