using appointmenting.Linq.CriteriaBuilder.Expressions;
using System.Linq.Expressions;

namespace appointmenting.Linq.CriteriaBuilder;

public interface ICriteriaQuery<T> where T : class
{
    Expression<Func<T, bool>> Where { get; set; }

    List<OrderByExpressionDescriptor<T>> OrderByExpressions { get; }

    List<OrderByStringExpressionDescriptor> OrderByStrings { get; }

    List<IncludeExpressionDescriptor> IncludeExpressions { get; }

    List<string> IncludeStrings { get; }

    int? Take { get; }

    int? Skip { get; }

    bool IgnoreQueryFilters { get; }

    bool NoTracking { get; }

    bool Tracking { get; }

    bool SplitQuery { get; }

    string? TagWith { get; }
}
