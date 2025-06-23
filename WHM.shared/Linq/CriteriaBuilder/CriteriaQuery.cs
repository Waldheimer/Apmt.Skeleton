using appointmenting.Linq.CriteriaBuilder.Expressions;
using System.Linq.Expressions;

namespace appointmenting.Linq.CriteriaBuilder;
public class CriteriaQuery<T> : ICriteriaQuery<T>
    where T : class
{
    public Expression<Func<T, bool>> Where { get; set; } = null!;

    public List<OrderByStringExpressionDescriptor> OrderByStrings { get; } = new List<OrderByStringExpressionDescriptor>();

    public List<OrderByExpressionDescriptor<T>> OrderByExpressions { get; } = new List<OrderByExpressionDescriptor<T>>();

    public List<IncludeExpressionDescriptor> IncludeExpressions { get; } = new List<IncludeExpressionDescriptor>();

    public List<string> IncludeStrings { get; } = new List<string>();

    public int? Take { get; internal set; } = null;

    public int? Skip { get; internal set; } = null;

    public bool NoTracking { get; internal set; } = true;

    public bool Tracking { get; internal set; } = false;

    public bool SplitQuery { get; internal set; } = false;

    public bool IgnoreQueryFilters { get; internal set; } = false;

    public string? TagWith { get; internal set; }
}