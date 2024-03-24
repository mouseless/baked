using Do.Domain.Model;

namespace Do.Domain.Configuration;

public interface ITypeModelFactory
{
    TypeModel Create(Type type);
}