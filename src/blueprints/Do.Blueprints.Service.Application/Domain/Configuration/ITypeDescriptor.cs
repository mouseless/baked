namespace Do.Domain.Configuration;

public interface ITypeDescriptor
{
    bool IsDomainType { get; }
    Type ReflectedType { get; }
}