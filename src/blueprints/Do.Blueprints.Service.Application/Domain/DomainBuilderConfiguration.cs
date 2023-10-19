namespace Do.Domain;

public record DomainBuilderConfiguration
{
    public Func<Type, bool> PersistedTypeConvention { get; set; } = _ => false;
    public Func<Type, bool> SingletonConvention { get; set; } = _ => true;
}
