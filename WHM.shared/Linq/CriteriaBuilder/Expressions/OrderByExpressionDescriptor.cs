using appointmenting.Linq.CriteriaBuilder.Enums;
using System.Linq.Expressions;

namespace appointmenting.Linq.CriteriaBuilder.Expressions;

public class OrderByExpressionDescriptor<T>
{
    public OrderByExpressionDescriptor(Expression<Func<T, object>> keySelector, OrderByType orderType)
    {
        ArgumentNullException.ThrowIfNull(keySelector, nameof(keySelector));

        KeySelector = keySelector;
        OrderType = orderType;
    }

    public Expression<Func<T, object>> KeySelector { get; }

    public OrderByType OrderType { get; }
}
