namespace appointmenting.Linq.CriteriaBuilder.Builders.Abstractions;

public interface IIncludeCriteriaBuilder<T, out TProperty> : ICriteriaBuilder<T>
    where T : class
{ }
