using System.Linq.Expressions;

namespace appointmenting.Linq.Specifications;

public class Specification<T> : ISpecification<T>
{
    private class SpecificationParameterVisitor : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        internal SpecificationParameterVisitor(Dictionary<ParameterExpression, ParameterExpression> map)
            => _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

        internal static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression expression)
            => new SpecificationParameterVisitor(map).Visit(expression);

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            if (_map.TryGetValue(parameter, out var replacement))
                parameter = replacement;

            return base.VisitParameter(parameter);
        }
    }

    private Expression<Func<T, bool>> _predicate = null!;

    public Specification() { }

    public Specification(Expression<Func<T, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        _predicate = predicate;
    }

    public Specification<T> Where(Expression<Func<T, bool>>? predicate)
    {
        //ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        if (_predicate == null && predicate != null)
            _predicate = predicate;
        else if (_predicate != null && predicate != null)
            _predicate = AndAlso(_predicate, predicate);
        else
            _predicate = null!;

        return this;
    }

    public Specification<T> And(Expression<Func<T, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        if (_predicate == null)
            _predicate = predicate;
        else
            _predicate = AndAlso(_predicate, predicate);

        return this;
    }

    public Specification<T> Or(Expression<Func<T, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        if (_predicate == null)
            _predicate = predicate;
        else
            _predicate = OrElse(_predicate, predicate);

        return this;
    }

    public Specification<T> AndNot(Expression<Func<T, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        if (_predicate == null)
            _predicate = predicate;
        else
            _predicate = AndAlso(_predicate, Not(predicate));

        return this;
    }

    public Specification<T> OrNot(Expression<Func<T, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        if (_predicate == null)
            _predicate = predicate;
        else
            _predicate = OrElse(_predicate, Not(predicate));

        return this;
    }

    public Specification<T> Not()
    {
        ArgumentNullException.ThrowIfNull(_predicate, nameof(_predicate));

        _predicate = Not(_predicate);

        return this;
    }

    public bool IsSatisfiedBy(T obj)
    {
        ArgumentNullException.ThrowIfNull(_predicate, nameof(_predicate));
        var predicate = _predicate.Compile();

        return predicate(obj);
    }

    public Expression<Func<T, bool>> ToExpression()
    {
        //ArgumentNullException.ThrowIfNull(_predicate, nameof(_predicate));
        _predicate ??= x => true;

        return _predicate;
    }

    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));
        return specification.ToExpression();
    }

    private static Expression<Func<T, bool>> AndAlso(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        return Compose(left, right, Expression.AndAlso);
    }

    private static Expression<Func<T, bool>> OrElse(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        return Compose(left, right, Expression.OrElse);
    }

    private static Expression<Func<T, bool>> Not(Expression<Func<T, bool>> expression)
    {
        var parameter = expression.Parameters.ElementAt(0);
        var body = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static Expression<Func<T, bool>> Compose(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right, Func<Expression, Expression, Expression> merge)
    {
        var map = left.Parameters.Select((f, i) => new { f, s = right.Parameters.ElementAt(i) }).ToDictionary(p => p.s, p => p.f);
        var rightBody = SpecificationParameterVisitor.ReplaceParameters(map, right.Body);

        return Expression.Lambda<Func<T, bool>>(merge(left.Body, rightBody), left.Parameters);
    }
}