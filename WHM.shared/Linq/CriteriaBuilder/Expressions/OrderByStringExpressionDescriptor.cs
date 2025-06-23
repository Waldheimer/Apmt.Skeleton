using appointmenting.Linq.CriteriaBuilder.Enums;

namespace appointmenting.Linq.CriteriaBuilder.Expressions;

public class OrderByStringExpressionDescriptor
{
    public string? Fieldname { get; set; }

    public OrderByStringType OrderByType { get; set; } = OrderByStringType.Ascending;
}