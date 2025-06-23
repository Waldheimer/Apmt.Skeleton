using System.Linq.Expressions;

namespace appointmenting.Linq.Specifications;

public interface ISpecification<T>
{
    Specification<T> And(Expression<Func<T, bool>> predicate);

    Specification<T> AndNot(Expression<Func<T, bool>> predicate);

    bool IsSatisfiedBy(T obj);

    Specification<T> Not();

    Specification<T> Or(Expression<Func<T, bool>> predicate);

    Specification<T> OrNot(Expression<Func<T, bool>> predicate);

    Expression<Func<T, bool>> ToExpression();

    Specification<T> Where(Expression<Func<T, bool>> predicate);
}
