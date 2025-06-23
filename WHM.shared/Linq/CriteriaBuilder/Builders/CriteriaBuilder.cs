using appointmenting.Linq.CriteriaBuilder.Builders.Abstractions;

namespace appointmenting.Linq.CriteriaBuilder.Builders;

public class CriteriaBuilder<T> : ICriteriaBuilder<T>
    where T : class
{
    public CriteriaQuery<T> Query { get; }

    public CriteriaBuilder()
        => Query = new CriteriaQuery<T>();
}
