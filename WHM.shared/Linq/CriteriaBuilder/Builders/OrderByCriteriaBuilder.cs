using appointmenting.Linq.CriteriaBuilder.Builders.Abstractions;

namespace appointmenting.Linq.CriteriaBuilder.Builders;

public class OrderByCriteriaBuilder<T> : IOrderByCriteriaBuilder<T>
    where T : class
{
    public CriteriaQuery<T> Query { get; }

    public OrderByCriteriaBuilder(CriteriaQuery<T> query)
        => Query = query;
}