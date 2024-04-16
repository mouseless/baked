using Do.Domain.Model;

namespace Do.Domain.Configuration;

public interface IDomainModelConvention
{
    int Order { get; }
}

public interface IDomainModelConvention<TModel> : IDomainModelConvention
    where TModel : IModel
{
    void Apply(TModel model);
}