namespace Baked.Domain.Configuration;

public interface IDomainModelConvention;

public interface IDomainModelConvention<TModel> : IDomainModelConvention
{
    void Apply(TModel model);
}