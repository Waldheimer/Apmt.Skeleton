namespace appointmenting.Linq.CriteriaBuilder.Builders.Abstractions;

public interface ICriteriaBuilder<T>
    where T : class
{
    CriteriaQuery<T> Query { get; }
}
