using appointmenting.Linq.CriteriaBuilder.Builders.Abstractions;

namespace appointmenting.Linq.CriteriaBuilder.Builders;

public class IncludeCriteriaBuilder<T, TProperty> : IIncludeCriteriaBuilder<T, TProperty>
    where T : class
{
    public CriteriaQuery<T> Query { get; }

    public IncludeCriteriaBuilder(CriteriaQuery<T> query)
        => Query = query;
}
