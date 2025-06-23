namespace appointmenting.Repositories.Annotations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public abstract class ComponentAttribute : Attribute
{
    public ComponentAttribute() { }

    public ComponentAttribute(string name)
    {
        Name = name;
    }

    public ComponentAttribute(string name, Lifetime lifetime)
    {
        Name = name;
        Lifetime = lifetime;
    }

    public string Name { get; } = "";

    public Lifetime Lifetime { get; } = Lifetime.Scoped;
}
