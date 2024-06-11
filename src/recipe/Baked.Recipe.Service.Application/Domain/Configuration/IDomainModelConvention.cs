namespace Baked.Domain.Configuration;

public interface IDomainModelConvention
{
    int Order { get; }
}

public interface IDomainModelConvention<TModel> : IDomainModelConvention
{
    void Apply(TModel model);
}