namespace appointmenting.Repositories.Annotations;

/// <summary>
/// Indicates that an annotated class is a "Service"
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class ServiceAttribute : ComponentAttribute
{
    public ServiceAttribute()
        : this(Lifetime.Scoped) { }

    public ServiceAttribute(Lifetime lifetime = Lifetime.Scoped)
        : this("", lifetime) { }

    public ServiceAttribute(string name, Lifetime lifetime = Lifetime.Scoped)
        : base(name, lifetime) { }
}
