namespace appointmenting.Repositories.Annotations;

/// <summary>
/// Indicates that an annotated class is a "Repository"
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class RepositoryAttribute : ComponentAttribute
{
    public RepositoryAttribute()
    : this(Lifetime.Scoped) { }

    public RepositoryAttribute(Lifetime lifetime = Lifetime.Scoped)
        : this("", lifetime) { }

    public RepositoryAttribute(string name, Lifetime lifetime = Lifetime.Scoped)
        : base(name, lifetime) { }
}
